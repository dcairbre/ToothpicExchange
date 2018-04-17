using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDentBusiness {
	///<summary>Provides a cache pattern for S-classes that use a Dictionary to store data instead of (or in addition to) a regular list.
	///New cache classes should use this class over CacheHashtableAbs. Dictionaries offer better performance than Hashtables because
	///they do not need to box and unbox.</summary>
	public abstract class CacheDictAbs<T,KEY_TYPE,VALUE_TYPE> : CacheAbs<T> where T : TableBase {
		private Dictionary<KEY_TYPE,VALUE_TYPE> _dictLong;
		private Dictionary<KEY_TYPE,VALUE_TYPE> _dictShort;
		///<summary>Will be set to true the first time DictShortDeep or DictShortShallow is accessed.</summary>
		private bool _hasDictShort=false;

		///<summary>Returns a shallow copy of the dictionary. If the dictionary is null, attempts to fill it. Includes hidden objects.</summary>
		public Dictionary<KEY_TYPE,VALUE_TYPE> DictShallow {
			get {
				FillDictIfNull();
				_lock.EnterReadLock();
				try {
					return _dictLong;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
			protected set {
				_lock.EnterWriteLock();
				try {
					_dictLong=value;
				}
				finally {
					_lock.ExitWriteLock();
				}
			}
		}

		///<summary>Returns a shallow copy of the cache. Does not include hidden objects. This dict instance cannot be modified (add/remove/clear) without
		///the risk of modifying the cached dict. Items in this dict also cannot be modified without risk. 
		///Use DictShortDeep if you want to modify items for any reason.</summary>
		public Dictionary<KEY_TYPE,VALUE_TYPE> DictShortShallow {
			get {
				_hasDictShort=true;
				FillDictIfNull(true);
				_lock.EnterReadLock();
				try {
					return _dictShort;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
			protected set {
				_lock.EnterWriteLock();
				try {
					_dictShort=value;
				}
				finally {
					_lock.ExitWriteLock();
				}
			}
		}

		///<summary>Returns a deep copy of the dictionary. Includes hidden objects.</summary>
		public Dictionary<KEY_TYPE,VALUE_TYPE> DictDeep {
			get {
				FillDictIfNull();
				_lock.EnterReadLock();
				try {
					Dictionary<KEY_TYPE,VALUE_TYPE> hlist=new Dictionary<KEY_TYPE,VALUE_TYPE>();
					foreach(KEY_TYPE key in _dictLong.Keys) {
						hlist.Add(key,CopyDictValue(_dictLong[key]));
					}
					return hlist;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
		}

		///<summary>Gets a deep copy of the cache not including hidden objects. This dict instance can then be modified (add/remove/clear) without
		///the risk of modifying the cached dict. Items in this dict can also be modified without risk.</summary>
		public Dictionary<KEY_TYPE,VALUE_TYPE> DictShortDeep {
			get {
				_hasDictShort=true;
				FillDictIfNull(true);
				_lock.EnterReadLock();
				try {
					Dictionary<KEY_TYPE,VALUE_TYPE> hlist=new Dictionary<KEY_TYPE,VALUE_TYPE>();
					foreach(KEY_TYPE key in _dictShort.Keys) {
						hlist.Add(key,CopyDictValue(_dictShort[key]));
					}
					return hlist;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
		}	
	
		///<summary>Check if the dictionary is null. Used instead of DictShallow==null when the database connection has not yet been set,
		///because DictShallow attempts to fill the dictionary from the db if it is null.
		///Optionally set isShort to true to check if _dictShort is null.</summary>
		public bool DictIsNull(bool isShort=false) {
			_lock.EnterReadLock();
			bool isNull=((isShort ? _dictShort : _dictLong)==null);
			_lock.ExitReadLock();
			return isNull;
		}

		///<summary>Fill the dictionary with the data from the base List. 
		///Override if using a &lt;key,value&gt; pair that is not &lt;PKey,Tablebase&gt;.</summary>
		protected virtual void FillDict() {
			//This will passively fill ListDeep if it is null.
			List<T> listDeep=ListDeep;
			//New instance will be filled and then become DictShallow. No read/write lock necessary in this context.
			Dictionary<KEY_TYPE, VALUE_TYPE> dict=new Dictionary<KEY_TYPE, VALUE_TYPE>();
			foreach(T tableBase in listDeep) {
				if(!dict.ContainsKey(GetDictKey(tableBase))) {
					dict.Add(GetDictKey(tableBase),GetDictValue(tableBase));
				}
			}
			if(_hasDictShort) {
				//New instance will be filled and then become DictShortShallow. No read/write lock necessary in this context.
				Dictionary<KEY_TYPE, VALUE_TYPE> dictShort=new Dictionary<KEY_TYPE, VALUE_TYPE>();
				foreach(T tableBase in listDeep.FindAll(x => IsInListShort(x))) {
					if(!dictShort.ContainsKey(GetDictKey(tableBase))) {
						dictShort.Add(GetDictKey(tableBase),GetDictValue(tableBase));
					}
				}
				DictShortShallow=dictShort;
			}
			DictShallow=dict;
		}

		private void FillDictIfNull(bool isShort=false) {
			if(DictIsNull(isShort)) {
				FillDict();
			}
		}

		///<summary>Now fill the hashtable with the new data.</summary>
		protected override void FillCacheDone() {
			FillDict();
		}

		///<summary>Executes the base ContainsKey method in a thread safe manner.
		///Optionally set isShort true to check _dictShort for the corresponding key.</summary>
		public bool ContainsKey(KEY_TYPE key,bool isShort=false) {
			FillDictIfNull(isShort);
			_lock.EnterReadLock();
			try {
				return (isShort ? _dictShort : _dictLong).ContainsKey(key);
			}
			finally {
				_lock.ExitReadLock();
			}
		}

		///<summary>Returns a deep copy of the dictionary's value for the corresponding key. Throws KeyNotFoundException if no match is found.</summary>
		public VALUE_TYPE GetOne(KEY_TYPE key,bool isShort=false) {
			FillDictIfNull(isShort);
			_lock.EnterReadLock();
			try {
				return CopyDictValue((isShort ? _dictShort : _dictLong)[key]);
			}
			finally {
				_lock.ExitReadLock();
			}
		}

		///<summary>Return the expected key for the dictionary (typically the primary key).</summary>
		protected abstract KEY_TYPE GetDictKey(T tableBase);

		///<summary>Return the expected key for the dictionary (typically the tablebase).</summary>
		protected abstract VALUE_TYPE GetDictValue(T tableBase);

		///<summary>Return a copy of the expected value for the dictionary (typically the tablebase).</summary>
		protected abstract VALUE_TYPE CopyDictValue(VALUE_TYPE value);

		///<summary>Returns true if the key is successfully found and removed; otherwise, false.
		///Optionally set isShort true to remove from _dictShort.</summary>
		public bool RemoveKey(KEY_TYPE key,bool isShort=false) {
			FillDictIfNull(isShort);
			_lock.EnterWriteLock();
			try {
				return (isShort ? _dictShort : _dictLong).Remove(key);
			}
			finally {
				_lock.ExitWriteLock();
			}
		}

		///<summary>Tries to add the key value pair to the dictionary.  Returns true if successfully added; otherwise, false.
		///Optionally set isShort true to add the key value pair to _dictShort.</summary>
		public bool AddValueForKey(KEY_TYPE key,VALUE_TYPE value,bool isShort=false) {
			FillDictIfNull(isShort);
			bool wasKeyAdded=false;
			_lock.EnterWriteLock();
			try {
				if(!(isShort ? _dictShort : _dictLong).ContainsKey(key)) {
					wasKeyAdded=true;
					(isShort ? _dictShort : _dictLong).Add(key,value);
				}
			}
			finally {
				_lock.ExitWriteLock();
			}
			return wasKeyAdded;
		}

		///<summary>Forces the key to be set to the value passed in.  Optionally set isShort true to set the corresponding key for _dictShort.</summary>
		public void SetValueForKey(KEY_TYPE key,VALUE_TYPE value,bool isShort=false) {
			FillDictIfNull(isShort);
			_lock.EnterWriteLock();
			try {
				(isShort ? _dictShort : _dictLong)[key]=value;
			}
			finally {
				_lock.ExitWriteLock();
			}
		}

	}
}
