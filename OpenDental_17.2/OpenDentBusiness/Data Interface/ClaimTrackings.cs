using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimTrackings{
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

		/*
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.
		//Also, make sure to consider making an invalid type for this class in Cache.GetAllCachedInvalidTypes() if needed.

		///<summary>A list of all ClaimTrackings.</summary>
		private static List<ClaimTracking> _list;

		///<summary>A list of all ClaimTrackings.</summary>
		public static List<ClaimTracking> List {
			get {
				if(_list==null) {
					RefreshCache();
				}
				return _list;
			}
			set {
				_list=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM claimtracking ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClaimTracking";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			_list=Crud.ClaimTrackingCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static List<ClaimTracking> Refresh(long usernum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimTracking>>(MethodBase.GetCurrentMethod(),usernum);
			}
			string command="SELECT * FROM claimtracking WHERE UserNum = "+POut.Long(usernum);
			return Crud.ClaimTrackingCrud.SelectMany(command);
		}

		///<summary></summary>
		public static List<ClaimTracking> RefreshForUsers(ClaimTrackingType type,List<long> listUserNums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimTracking>>(MethodBase.GetCurrentMethod(),type,listUserNums);
			}
			if(listUserNums==null || listUserNums.Count==0) {
				return new List<ClaimTracking>();
			}
			string command="SELECT * FROM claimtracking WHERE TrackingType='"+POut.String(type.ToString())+"' "
				+"AND UserNum IN ("+String.Join(",",listUserNums)+")";
			return Crud.ClaimTrackingCrud.SelectMany(command);
		}

		///<summary></summary>
		public static List<ClaimTracking> RefreshForClaim(ClaimTrackingType type,long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimTracking>>(MethodBase.GetCurrentMethod(),type,claimNum);
			}
			if(claimNum==0) {
				return new List<ClaimTracking>();
			}
			string command="SELECT * FROM claimtracking WHERE TrackingType='"+POut.String(type.ToString())+"' "
				+"AND ClaimNum="+POut.Long(claimNum);
			return Crud.ClaimTrackingCrud.SelectMany(command);
		}

		///<summary>Gets one ClaimTracking from the db.</summary>
		public static ClaimTracking GetOne(long claimTrackingNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ClaimTracking>(MethodBase.GetCurrentMethod(),claimTrackingNum);
			}
			return Crud.ClaimTrackingCrud.SelectOne(claimTrackingNum);
		}

		///<summary></summary>
		public static long Insert(ClaimTracking claimTracking){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				claimTracking.ClaimTrackingNum=Meth.GetLong(MethodBase.GetCurrentMethod(),claimTracking);
				return claimTracking.ClaimTrackingNum;
			}
			return Crud.ClaimTrackingCrud.Insert(claimTracking);
		}

		public static long InsertClaimProcReceived(long claimNum,long userNum,string note="") {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetLong(MethodBase.GetCurrentMethod(),claimNum,userNum,note);
			}
			string command="SELECT COUNT(*) FROM claimtracking WHERE TrackingType='"+POut.String(ClaimTrackingType.ClaimProcReceived.ToString())
				+"' AND ClaimNum="+POut.Long(claimNum)+" AND UserNum='"+userNum+"'";
			if(Db.GetCount(command)!="0") {
				return 0;//Do nothing.
			}
			ClaimTracking claimTracking=new ClaimTracking();
			claimTracking.TrackingType=ClaimTrackingType.ClaimProcReceived;
			claimTracking.ClaimNum=claimNum;
			claimTracking.UserNum=userNum;
			claimTracking.Note=note;
			return Crud.ClaimTrackingCrud.Insert(claimTracking);
		}

		///<summary></summary>
		public static void Update(ClaimTracking claimTracking){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),claimTracking);
				return;
			}
			Crud.ClaimTrackingCrud.Update(claimTracking);
		}

		///<summary></summary>
		public static void Delete(long claimTrackingNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),claimTrackingNum);
				return;
			}
			Crud.ClaimTrackingCrud.Delete(claimTrackingNum);
		}

		///<summary></summary>
		public static void Sync(List<ClaimTracking> listNew,List<ClaimTracking> listOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listNew,listOld);
				return;
			}
			Crud.ClaimTrackingCrud.Sync(listNew,listOld);
		}

	}
}