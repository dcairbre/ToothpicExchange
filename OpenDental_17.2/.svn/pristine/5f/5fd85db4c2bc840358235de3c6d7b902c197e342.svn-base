using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenDentBusiness {
	///<summary>The purpose of this class is to provide a shared read lock and an exclusive write lock on a cache.</summary>
	public abstract class CacheAbs<T> where T : TableBase {
		///<summary>The list of the actual cached objects.</summary>
		private List<T> _listLong;
		///<summary>The list of the objects that are not hidden. Will be null if _doesHaveListShort is false.</summary>
		private List<T> _listShort;
		///<summary>A lock object that allows multiple threads to obtain a read lock but allows only one thread to obtain a write lock.</summary>
		protected ReaderWriterLockSlim _lock=new ReaderWriterLockSlim();
		///<summary>Will be set to true the first time ListShortDeep or ListShallowShort is accessed.</summary>
		private bool _hasListShort=false;

		///<summary>This method queries the database for the list of objects and return it. Remoting role does not need to be checked.</summary>
		protected abstract List<T> GetCacheFromDb();
		///<summary>This method takes in a DataTable and turns it into a list of objects.</summary>
		protected abstract List<T> TableToList(DataTable table);
		///<summary></summary>
		protected abstract T Copy(T tableBase);
		///<summary></summary>
		protected abstract DataTable ListToTable(List<T> listDeep);
		///<summary>After this method has run, both the client's and the server's cache should be initialized.</summary>
		protected abstract void FillCacheIfNeeded();

		///<summary>Return true if the object should be included in ListShort and return false if it shouldn't be included.</summary>
		protected virtual bool IsInListShort(T tableBase) {
			return true;
		}
		///<summary>Called when the cache has been filled.</summary>
		protected virtual void FillCacheDone() { }

		///<summary>Returns a shallow copy of the cache. Includes hidden objects. This list instance cannot be modified (add/remove/clear) without
		///the risk of modifying the cached list. Items in this list also cannot be modified without risk. 
		///Use ListDeep if you want to modify items for any reason.</summary>
		public List<T> ListShallow {
			get {
				FillListIfNull();
				_lock.EnterReadLock();
				try {
					return _listLong;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
			private set {
				_lock.EnterWriteLock();
				try {
					_listLong=value;
				}
				finally {
					_lock.ExitWriteLock();
				}
			}
		}

		///<summary>Returns a shallow copy of the cache. Does not include hidden objects. This list instance cannot be modified (add/remove/clear) without
		///the risk of modifying the cached list. Items in this list also cannot be modified without risk. 
		///Use ListDeep if you want to modify items for any reason.</summary>
		public List<T> ListShallowShort {
			get {
				_hasListShort=true;
				FillListIfNull(true);
				_lock.EnterReadLock();
				try {
					return _listShort;
				}
				finally {
					_lock.ExitReadLock();
				}
			}
			private set {
				_lock.EnterWriteLock();
				try {
					_listShort=value;
				}
				finally {
					_lock.ExitWriteLock();
				}
			}
		}

		///<summary>Gets a deep copy of the cache including hidden objects. This list instance can then be modified (add/remove/clear) without
		///the risk of modifying the cached list. Items in this list can also be modified without risk.</summary>
		public List<T> ListDeep {
			get {
				FillListIfNull();
				_lock.EnterReadLock();
				try {
					return _listLong.Select(x => Copy(x)).Cast<T>().ToList();
				}
				finally {
					_lock.ExitReadLock();
				}
			}
		}

		///<summary>Gets a deep copy of the cache not including hidden objects. This list instance can then be modified (add/remove/clear) without
		///the risk of modifying the cached list. Items in this list can also be modified without risk.</summary>
		public List<T> ListShortDeep {
			get {
				_hasListShort=true;
				FillListIfNull(true);
				_lock.EnterReadLock();
				try {
					return _listShort.Select(x => Copy(x)).Cast<T>().ToList();
				}
				finally {
					_lock.ExitReadLock();
				}
			}
		}
	
		///<summary>Check if the dictionary is null. Used instead of DictShallow==null when the database connection has not yet been set,
		///because DictShallow attempts to fill the dictionary from the db if it is null.
		///Optionally set isShort to true to check if _dictShort is null.</summary>
		public bool ListIsNull(bool isShort=false) {
			_lock.EnterReadLock();
			bool isNull=((isShort ? _listShort : _listLong)==null);
			_lock.ExitReadLock();
			return isNull;
		}

		private void FillListIfNull(bool isShort=false) {
			if(ListIsNull(isShort)) {
				FillCacheIfNeeded();
			}
		}

		///<summary>Returns a deep copy of the first item in the cache that matches the predicate. Returns null if no match is found.
		///Optionally set isShort true to search through _listShort instead.</summary>
		public T GetOne(Func<T,bool> predicate,bool isShort=false) {
			FillListIfNull(isShort);
			_lock.EnterReadLock();
			try {
				T retVal=(isShort ? _listShort : _listLong).FirstOrDefault(predicate);
				if(retVal==null) {
					return retVal;
				}
				return Copy(retVal);
			}
			finally {
				_lock.ExitReadLock();
			}
		}

		///<summary>Returns a deep copy of the all items in the cache that match the predicate.  Returns an empty list if no matches found.
		///Optionally set isShort true in order to search through _listShort instead.</summary>
		public List<T> GetWhere(Predicate<T> match,bool isShort=false) {
			FillListIfNull(isShort);
			_lock.EnterReadLock();
			try {
				return (isShort ? _listShort : _listLong).FindAll(match).Select(x => Copy(x)).ToList();
			}
			finally {
				_lock.ExitReadLock();
			}
		}

		///<summary>Fills the cache using the specified source. If source is Database, then table can be null.</summary>
		private void FillCache(FillCacheSource source,DataTable table) {
			//Get a list that can be used later to quickly set the cache.
			List<T> listLong=new List<T>();
			if(source==FillCacheSource.Database) {
				listLong=GetCacheFromDb();
			}
			else if(source==FillCacheSource.DataTable) {
				listLong=TableToList(table);
			}
			ListShallow=listLong;
			if(_hasListShort) {
				List<T> listShort=listLong.Where(x => IsInListShort(x)).Select(x => Copy(x)).Cast<T>().ToList();
				ListShallowShort=listShort;
			}
			FillCacheDone();
		}
		
		///<summary>Fills the cache using the provided DataTable. Thread safe. This can be called from ClientWeb.</summary>
		public void FillCacheFromTable(DataTable table) {
			FillCache(FillCacheSource.DataTable,table);
		}

		///<summary>Gets the table from the cache. This should not be called from ClientWeb.</summary>
		public DataTable GetTableFromCache(bool doRefreshCache) {
			bool isCacheNull=false;
			_lock.EnterReadLock();
			isCacheNull=(_listLong==null || (_hasListShort && _listShort==null));
			_lock.ExitReadLock();
			if(isCacheNull || doRefreshCache) {
				FillCache(FillCacheSource.Database,null);
			}
			return ListToTable(ListDeep);
		}

		///<summary>The source that the cache will be filled from.</summary>
		private enum FillCacheSource {
			///<summary>Cache is to be filled from the database.</summary>
			Database,
			///<summary>Cache is to be filled using the provided DataTable.</summary>
			DataTable
		}
	}

}
