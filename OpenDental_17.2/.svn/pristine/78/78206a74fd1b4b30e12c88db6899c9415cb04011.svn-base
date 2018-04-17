using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TsiTransLogs{
		//If this table type will exist as cached data, uncomment the Cache Pattern region below and edit.
		/*
		#region Cache Pattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add GetTableFromCache and FillCacheFromTable to the Cache.cs file with all the other Cache types.
		//Also, consider making an invalid type for this class in Cache.GetAllCachedInvalidTypes() if needed.

		private class TsiTransLogCache : CacheAbs<TsiTransLog> {
			protected override List<TsiTransLog> GetCacheFromDb() {
				string command="SELECT * FROM tsitranslog";
				return Crud.TsiTransLogCrud.SelectMany(command);
			}
			protected override List<TsiTransLog> TableToList(DataTable table) {
				return Crud.TsiTransLogCrud.TableToList(table);
			}
			protected override TsiTransLog Copy(TsiTransLog tsiTransLog) {
				return tsiTransLog.Copy();
			}
			protected override DataTable ListToTable(List<TsiTransLog> listTsiTransLogs) {
				return Crud.TsiTransLogCrud.ListToTable(listTsiTransLogs,"TsiTransLog");
			}
			protected override void FillCacheIfNeeded() {
				TsiTransLogs.GetTableFromCache(false);
			}
			protected override bool IsInListShort(TsiTransLog tsiTransLog) {
				return true;//Either change this method or delete it.
			}
		}

		///<summary>The object that accesses the cache in a thread-safe manner.</summary>
		private static TsiTransLogCache _tsiTransLogCache=new TsiTransLogCache();

		///<summary>A list of all TsiTransLogs. Returns a deep copy.</summary>
		public static List<TsiTransLog> ListDeep {
			get {
				return _tsiTransLogCache.ListDeep;
			}
		}

		///<summary>A list of all non-hidden TsiTransLogs. Returns a deep copy.</summary>
		public static List<TsiTransLog> ListShortDeep {
			get {
				return _tsiTransLogCache.ListShortDeep;
			}
		}

		///<summary>A list of all TsiTransLogs. Returns a shallow copy.</summary>
		public static List<TsiTransLog> ListShallow {
			get {
				return _tsiTransLogCache.ListShallow;
			}
		}

		///<summary>A list of all non-hidden TsiTransLogs. Returns a shallow copy.</summary>
		public static List<TsiTransLog> ListShortShallow {
			get {
				return _tsiTransLogCache.ListShallowShort;
			}
		}

		///<summary>Fills the local cache with the passed in DataTable.</summary>
		public static void FillCacheFromTable(DataTable table) {
			_tsiTransLogCache.FillCacheFromTable(table);
		}

		///<summary>Returns the cache in the form of a DataTable. Always refreshes the ClientWeb's cache.</summary>
		///<param name="doRefreshCache">If true, will refresh the cache if RemotingRole is ClientDirect or ServerWeb.</param> 
		public static DataTable GetTableFromCache(bool doRefreshCache) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DataTable table=Meth.GetTable(MethodBase.GetCurrentMethod(),doRefreshCache);
				_tsiTransLogCache.FillCacheFromTable(table);
				return table;
			}
			return _tsiTransLogCache.GetTableFromCache(doRefreshCache);
		}
		#endregion Cache Pattern
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.
		#region Get Methods
		///<summary></summary>
		public static List<TsiTransLog> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TsiTransLog>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM tsitranslog WHERE PatNum = "+POut.Long(patNum);
			return Crud.TsiTransLogCrud.SelectMany(command);
		}
		
		///<summary>Gets one TsiTransLog from the db.</summary>
		public static TsiTransLog GetOne(long tsiTransLogNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<TsiTransLog>(MethodBase.GetCurrentMethod(),tsiTransLogNum);
			}
			return Crud.TsiTransLogCrud.SelectOne(tsiTransLogNum);
		}
		#endregion
		#region Modification Methods
			#region Insert
		///<summary></summary>
		public static long Insert(TsiTransLog tsiTransLog){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				tsiTransLog.TsiTransLogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),tsiTransLog);
				return tsiTransLog.TsiTransLogNum;
			}
			return Crud.TsiTransLogCrud.Insert(tsiTransLog);
		}
			#endregion
			#region Update
		///<summary></summary>
		public static void Update(TsiTransLog tsiTransLog){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tsiTransLog);
				return;
			}
			Crud.TsiTransLogCrud.Update(tsiTransLog);
		}
			#endregion
			#region Delete
		///<summary></summary>
		public static void Delete(long tsiTransLogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tsiTransLogNum);
				return;
			}
			Crud.TsiTransLogCrud.Delete(tsiTransLogNum);
		}
			#endregion
		#endregion
		#region Misc Methods
		

		
		#endregion
		*/



	}
}