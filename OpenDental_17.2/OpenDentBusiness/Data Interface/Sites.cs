using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sites{
		#region Get Methods
		#endregion

		#region Modification Methods
		
		#region Insert
		#endregion

		#region Update
		#endregion

		#region Delete
		#endregion

		#endregion

		#region Misc Methods
		#endregion


		#region CachePattern

		private class SiteCache : CacheAbs<Site> {
			protected override List<Site> GetCacheFromDb() {
				string command="SELECT * FROM site ORDER BY Description";
				return Crud.SiteCrud.SelectMany(command);
			}
			protected override List<Site> TableToList(DataTable table) {
				return Crud.SiteCrud.TableToList(table);
			}
			protected override Site Copy(Site site) {
				return site.Copy();
			}
			protected override DataTable ListToTable(List<Site> listSites) {
				return Crud.SiteCrud.ListToTable(listSites,"Site");
			}
			protected override void FillCacheIfNeeded() {
				Sites.GetTableFromCache(false);
			}
		}

		///<summary>The object that accesses the cache in a thread-safe manner.</summary>
		private static SiteCache _siteCache=new SiteCache();

		///<summary>A list of all Sites.</summary>
		public static List<Site> ListShallow {
			get {
				return _siteCache.ListShallow;
			}
		}

		///<summary>A list of all Sites.</summary>
		public static List<Site> ListDeep {
			get {
				return _siteCache.ListDeep;
			}
		}

		///<summary>Refreshes the cache and returns it as a DataTable. This will refresh the ClientWeb's cache and the ServerWeb's cache.</summary>
		public static DataTable RefreshCache() {
			return GetTableFromCache(true);
		}

		///<summary>Fills the local cache with the passed in DataTable.</summary>
		public static void FillCacheFromTable(DataTable table) {
			//No need to check RemotingRole; no call to db.
			_siteCache.FillCacheFromTable(table);
		}

		///<summary>Always refreshes the ClientWeb's cache.</summary>
		public static DataTable GetTableFromCache(bool doRefreshCache) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DataTable table=Meth.GetTable(MethodBase.GetCurrentMethod(),doRefreshCache);
				_siteCache.FillCacheFromTable(table);
				return table;
			}
			return _siteCache.GetTableFromCache(doRefreshCache);
		}
		
		#endregion

		///<Summary>Gets one Site from the database.</Summary>
		public static Site CreateObject(long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Site>(MethodBase.GetCurrentMethod(),siteNum);
			}
			return Crud.SiteCrud.SelectOne(siteNum);
		}

		///<summary></summary>
		public static long Insert(Site site) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				site.SiteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),site);
				return site.SiteNum;
			}
			return Crud.SiteCrud.Insert(site);
		}

		///<summary></summary>
		public static void Update(Site site) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),site);
				return;
			}
			Crud.SiteCrud.Update(site);
		}

		///<summary></summary>
		public static void DeleteObject(long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),siteNum);
				return;
			}
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE SiteNum="+POut.Long(siteNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("Sites","Site is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			Crud.SiteCrud.Delete(siteNum);
		}

		public static string GetDescription(long siteNum) {
			//No need to check RemotingRole; no call to db.
			if(siteNum==0){
				return "";
			}
			for(int i=0;i<ListShallow.Count;i++){
				if(ListShallow[i].SiteNum==siteNum){
					return ListShallow[i].Description;
				}
			}
			return "";
		}

		public static List<Site> GetListFiltered(string snippet) {
			//No need to check RemotingRole; no call to db.
			List<Site> retVal=new List<Site>();
			if(snippet=="") {
				return retVal;
			}
			for(int i=0;i<ListShallow.Count;i++) {
				if(ListShallow[i].Description.ToLower().Contains(snippet.ToLower())) {
					retVal.Add(ListShallow[i]);
				}
			}
			return retVal;
		}

		///<summary>Will return -1 if no match.</summary>
		public static long FindMatchSiteNum(string description) {
			//No need to check RemotingRole; no call to db.
			if(description=="") {
				return 0;
			}
			for(int i=0;i<ListShallow.Count;i++) {
				if(ListShallow[i].Description.ToLower()==description.ToLower()) {
					return ListShallow[i].SiteNum;
				}
			}
			return -1;
		}


	}
}