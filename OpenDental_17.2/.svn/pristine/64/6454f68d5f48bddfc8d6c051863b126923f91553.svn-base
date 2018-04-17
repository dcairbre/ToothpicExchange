using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClinicPrefs{
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

		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.
		//Also, make sure to consider making an invalid type for this class in Cache.GetAllCachedInvalidTypes() if needed.

		///<summary>A list of all ClinicPrefs.</summary>
		private static List<ClinicPref> _list;

		///<summary>A list of all ClinicPrefs.</summary>
		public static List<ClinicPref> List {
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
			string command="SELECT * FROM clinicpref";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClinicPref";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			_list=Crud.ClinicPrefCrud.TableToList(table);
		}

		///<summary>Gets one ClinicPref from the db.</summary>
		public static ClinicPref GetOne(long clinicPrefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ClinicPref>(MethodBase.GetCurrentMethod(),clinicPrefNum);
			}
			return Crud.ClinicPrefCrud.SelectOne(clinicPrefNum);
		}

		///<summary></summary>
		public static long Insert(ClinicPref clinicPref){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				clinicPref.ClinicPrefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),clinicPref);
				return clinicPref.ClinicPrefNum;
			}
			return Crud.ClinicPrefCrud.Insert(clinicPref);
		}

		///<summary></summary>
		public static void Update(ClinicPref clinicPref){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clinicPref);
				return;
			}
			Crud.ClinicPrefCrud.Update(clinicPref);
		}

		///<summary></summary>
		public static void Delete(long clinicPrefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clinicPrefNum);
				return;
			}
			Crud.ClinicPrefCrud.Delete(clinicPrefNum);
		}

		///<summary>Inserts, updates, or deletes db rows to match listNew.  No need to pass in userNum, it's set before remoting role check and passed to
		///the server if necessary.  Doesn't create ApptComm items, but will delete them.  If you use Sync, you must create new Apptcomm items.</summary>
		public static bool Sync(List<ClinicPref> listNew,List<ClinicPref> listOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),listNew,listOld);
			}
			return Crud.ClinicPrefCrud.Sync(listNew,listOld);
		}

		///<summary></summary>
		public static List<ClinicPref> GetAllPrefs(long clinicNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClinicPref>>(MethodBase.GetCurrentMethod(),clinicNum);
			}
			string command="SELECT * FROM clinicpref WHERE ClinicNum="+POut.Long(clinicNum);
			return Crud.ClinicPrefCrud.SelectMany(command);
		}

		///<summary></summary>
		public static List<ClinicPref> GetPrefAllClinics(PrefName pref) {
			//No need to check RemotingRole; no call to db.
			return List.FindAll(x => x.PrefName==pref);
		}

		///<summary></summary>
		public static ClinicPref GetPref(PrefName pref,long clinicNum) {
			//No need to check RemotingRole; no call to db.
			return GetPrefAllClinics(pref).FirstOrDefault(x => x.ClinicNum==clinicNum);
		}

		///<summary>Returns 0 if there is no clinicpref entry for the specified pref.</summary>
		public static long GetLong(PrefName prefName,long clinicNum) {
			//No need to check RemotingRole; no call to db.
			ClinicPref pref=GetPref(prefName,clinicNum);
			if(pref==null) {
				return 0;
			}
			return PIn.Long(pref.ValueString);
		}

		///<summary>Inserts a pref of type long for the specified clinic.  Returns true if a change was required, or false if no change needed.</summary>
		public static void InsertPref(PrefName prefName,long clinicNum,string valueAsString) {
			//No need to check RemotingRole; no call to db.
			if(List.FirstOrDefault(x => x.ClinicNum==clinicNum && x.PrefName==prefName)!=null) {
				throw new ApplicationException("The PrefName "+prefName+" already exists for ClinicNum: "+clinicNum);
			}
			ClinicPref clinicPrefToInsert=new ClinicPref();
			clinicPrefToInsert.PrefName=prefName;
			clinicPrefToInsert.ValueString=valueAsString;
			clinicPrefToInsert.ClinicNum=clinicNum;
			Insert(clinicPrefToInsert);
			List.Add(clinicPrefToInsert);
		}

		///<summary>Updates a pref of type long for the specified clinic.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateLong(PrefName prefName,long clinicNum,long newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			ClinicPref clinicPref=List.FirstOrDefault(x => x.ClinicNum==clinicNum && x.PrefName==prefName);
			if(clinicPref==null) {
				throw new ApplicationException("The PrefName "+prefName+" does not exist for ClinicNum: "+clinicNum);
			}
			if(PIn.Long(clinicPref.ValueString)==newValue) {
				return false;//no change needed
			}
			string command="UPDATE clinicpref SET ValueString='"+POut.Long(newValue)+"' "
				+"WHERE PrefName='"+POut.String(prefName.ToString())+"' "
				+"AND ClinicNum='"+POut.Long(clinicNum)+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else {
				Db.NonQ(command);
			}
			//Update local cache even though we should be invalidating the cache outside of this method.
			ClinicPref cachedClinicPref=clinicPref;
			cachedClinicPref.PrefName=prefName;
			cachedClinicPref.ValueString=newValue.ToString();
			cachedClinicPref.ClinicNum=clinicNum;
			return retVal;
		}

	}
}