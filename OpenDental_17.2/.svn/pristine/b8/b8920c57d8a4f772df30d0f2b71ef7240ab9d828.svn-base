using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AppointmentTypes {
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

		private class AppointmentTypeCache : CacheAbs<AppointmentType> {
			protected override List<AppointmentType> GetCacheFromDb() {
				string command="SELECT * FROM appointmenttype ORDER BY ItemOrder";
				return Crud.AppointmentTypeCrud.SelectMany(command);
			}
			protected override List<AppointmentType> TableToList(DataTable table) {
				return Crud.AppointmentTypeCrud.TableToList(table);
			}
			protected override AppointmentType Copy(AppointmentType appointmentType) {
				return appointmentType.Clone();
			}
			protected override DataTable ListToTable(List<AppointmentType> listAppointmentTypes) {
				return Crud.AppointmentTypeCrud.ListToTable(listAppointmentTypes,"AppointmentType");
			}
			protected override void FillCacheIfNeeded() {
				AppointmentTypes.GetTableFromCache(false);
			}
			protected override bool IsInListShort(AppointmentType appointmentType) {
				return !appointmentType.IsHidden;
			}
		}
		
		///<summary>The object that accesses the cache in a thread-safe manner.</summary>
		private static AppointmentTypeCache _appointmentTypeCache=new AppointmentTypeCache();

		///<summary>A list of all AppointmentTypes. Returns a deep copy.</summary>
		public static List<AppointmentType> ListDeep {
			get {
				return _appointmentTypeCache.ListDeep;
			}
		}

		///<summary>A list of all visible AppointmentTypes. Returns a deep copy.</summary>
		public static List<AppointmentType> ListShortDeep {
			get {
				return _appointmentTypeCache.ListShortDeep;
			}
		}

		///<summary>A list of all AppointmentTypes. Returns a shallow copy.</summary>
		public static List<AppointmentType> Listt {
			get {
				return _appointmentTypeCache.ListShallow;
			}
		}

		///<summary>A list of all visible AppointmentTypes. Returns a shallow copy.</summary>
		public static List<AppointmentType> ListShort {
			get {
				return _appointmentTypeCache.ListShallowShort;
			}
		}

		///<summary>Refreshes the cache and returns it as a DataTable. This will refresh the ClientWeb's cache and the ServerWeb's cache.</summary>
		public static DataTable RefreshCache() {
			return _appointmentTypeCache.GetTableFromCache(true);
		}

		///<summary>Fills the local cache with the passed in DataTable.</summary>
		public static void FillCacheFromTable(DataTable table) {
			_appointmentTypeCache.FillCacheFromTable(table);
		}

		///<summary>Always refreshes the ClientWeb's cache.</summary>
		public static DataTable GetTableFromCache(bool doRefreshCache) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DataTable table=Meth.GetTable(MethodBase.GetCurrentMethod(),doRefreshCache);
				_appointmentTypeCache.FillCacheFromTable(table);
				return table;
			}
			return _appointmentTypeCache.GetTableFromCache(doRefreshCache);
		}

		#endregion

		#region Sync Pattern

		///<summary>Inserts, updates, or deletes database rows to match supplied list.</summary>
		public static void Sync(List<AppointmentType> listNew,List<AppointmentType> listOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listNew,listOld);//never pass DB list through the web service
				return;
			}
			Crud.AppointmentTypeCrud.Sync(listNew,listOld);
		}

		#endregion

		///<summary>Gets one AppointmentType from the cache.</summary>
		public static AppointmentType GetOne(long appointmentTypeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].AppointmentTypeNum==appointmentTypeNum) {
					return Listt[i];
				}
			}
			return null;
		}

		///<summary></summary>
		public static long Insert(AppointmentType appointmentType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				appointmentType.AppointmentTypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),appointmentType);
				return appointmentType.AppointmentTypeNum;
			}
			return Crud.AppointmentTypeCrud.Insert(appointmentType);
		}

		///<summary></summary>
		public static void Update(AppointmentType appointmentType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),appointmentType);
				return;
			}
			Crud.AppointmentTypeCrud.Update(appointmentType);
		}

		///<summary>Surround with try catch.</summary>
		public static void Delete(long appointmentTypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),appointmentTypeNum);
				return;
			}
			string s=AppointmentTypes.CheckInUse(appointmentTypeNum);
			if(s!="") {
				throw new ApplicationException(Lans.g("AppointmentTypes",s));
			}
			string command="DELETE FROM appointmenttype WHERE AppointmentTypeNum = "+POut.Long(appointmentTypeNum);
			Db.NonQ(command);
		}

		///<summary>Used when attempting to delete.  Returns empty string if not in use and an untranslated string if in use.</summary>
		public static string CheckInUse(long appointmentTypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),appointmentTypeNum);
			}
			string command="SELECT COUNT(*) FROM appointment WHERE AppointmentTypeNum = "+POut.Long(appointmentTypeNum);
			if(PIn.Int(Db.GetCount(command))>0) {
				return "Not allowed to delete appointment types that are in use on an appointment.";
			}
			return "";
		}

		public static int SortItemOrder(AppointmentType a1,AppointmentType a2) {
			if(a1.ItemOrder!=a2.ItemOrder){
				return a1.ItemOrder.CompareTo(a2.ItemOrder);
			}
			return a1.AppointmentTypeNum.CompareTo(a2.AppointmentTypeNum);
		}

		///<summary>Returns true if all members are the same.</summary>
		public static bool Compare(AppointmentType a1,AppointmentType a2) {
			if(a1.AppointmentTypeColor==a2.AppointmentTypeColor
				&& a1.AppointmentTypeName==a2.AppointmentTypeName
				&& a1.IsHidden==a2.IsHidden
				&& a1.ItemOrder==a2.ItemOrder)
			{
				return true;
			}
			return false;
		}

		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/**/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.
		*/



	}
}