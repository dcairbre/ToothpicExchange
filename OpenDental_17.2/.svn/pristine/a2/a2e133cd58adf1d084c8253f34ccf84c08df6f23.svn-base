using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetDefs{
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

		///<summary>Returns true if this sheet def is allowed to bypass the global lock date.</summary>
		public static bool CanBypassLockDate(long sheetDefNum) {
			SheetDef sheetDef=SheetDefC.Listt.FirstOrDefault(x => x.SheetDefNum==sheetDefNum);
			if(sheetDef==null) {
				return false;
			}
			return (sheetDef.BypassGlobalLock==BypassLockStatus.BypassAlways);
		}

		#endregion

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM sheetdef ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="sheetdef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			SheetDefC.Listt=Crud.SheetDefCrud.TableToList(table);
		}

		///<Summary>Gets one SheetDef from the cache.  Also includes the fields and parameters for the sheetdef.</Summary>
		public static SheetDef GetSheetDef(long sheetDefNum,bool hasExceptions=true) {
			//No need to check RemotingRole; no call to db.
			SheetDef sheetdef=null;
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetDefNum==sheetDefNum){
					sheetdef=SheetDefC.Listt[i].Copy();
					break;
				}
			}
			if(hasExceptions || sheetdef!=null) {
				GetFieldsAndParameters(sheetdef);
			}
			return sheetdef;
		}

		///<summary>Includes all attached fields.  Intelligently inserts, updates, or deletes old fields.</summary>
		public static long InsertOrUpdate(SheetDef sheetDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheetDef.SheetDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sheetDef);
				return sheetDef.SheetDefNum;
			}
			if(sheetDef.IsNew){
				sheetDef.SheetDefNum=Crud.SheetDefCrud.Insert(sheetDef);
			}
			else{
				Crud.SheetDefCrud.Update(sheetDef);
			}
			foreach(SheetFieldDef field in sheetDef.SheetFieldDefs){
				field.SheetDefNum=sheetDef.SheetDefNum;
			}
			SheetFieldDefs.Sync(sheetDef.SheetFieldDefs,sheetDef.SheetDefNum);
			return sheetDef.SheetDefNum;
		}

		///<summary></summary>
		public static void DeleteObject(long sheetDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetDefNum);
				return;
			}
			//validate that not already in use by a refferral.
			string command="SELECT LName,FName FROM referral WHERE Slip="+POut.Long(sheetDefNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string referralNames="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					referralNames+=", ";
				}
				referralNames+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("sheetDefs","SheetDef is already in use by referrals. Not allowed to delete.")+" "+referralNames);
			}
			//validate that not already in use by automation.
			command="SELECT AutomationNum FROM automation WHERE SheetDefNum="+POut.Long(sheetDefNum);
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("sheetDefs","SheetDef is in use by automation. Not allowed to delete."));
			}
			//validate that not already in use by a laboratory
			command="SELECT Description FROM laboratory WHERE Slip="+POut.Long(sheetDefNum);
			table=Db.GetTable(command);
			if(table.Rows.Count > 0) {
				throw new ApplicationException(Lans.g("sheetDefs","SheetDef is in use by laboratories. Not allowed to delete.")
					+"\r\n"+string.Join(", ",table.Select().Select(x => x["Description"].ToString())));
			}
			//validate that not already in use by clinicPref.
			command="SELECT ClinicNum FROM clinicpref WHERE ValueString='"+POut.Long(sheetDefNum)+ "' AND PrefName='"+POut.String(PrefName.SheetsDefaultRx.ToString())+"'";
			table=Db.GetTable(command);
			if(table.Rows.Count>0) {
				throw new ApplicationException(Lans.g("sheetDefs","SheetDef is in use by clinics. Not allowed to delete.")
					+"\r\n"+string.Join(", ",table.Select().Select(x => Clinics.GetDesc(PIn.Long(x["ClinicNum"].ToString())))));
			}
			command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.Long(sheetDefNum);
			Db.NonQ(command);
			Crud.SheetDefCrud.Delete(sheetDefNum);
		}

		///<summary>Sheetdefs and sheetfielddefs are archived separately.  So when we need to use a sheetdef, we must run this method to pull all the associated fields from the archive.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(SheetDef sheetdef){
			//No need to check RemotingRole; no call to db.
			sheetdef.SheetFieldDefs=new List<SheetFieldDef>();
			sheetdef.Parameters=SheetParameter.GetForType(sheetdef.SheetType);
			//images first
			for(int i=0;i<SheetFieldDefC.Listt.Count;i++){
				if(SheetFieldDefC.Listt[i].SheetDefNum!=sheetdef.SheetDefNum){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType!=SheetFieldType.Image){
					continue;
				}
				sheetdef.SheetFieldDefs.Add(SheetFieldDefC.Listt[i].Copy());
			}
			//then all other fields
			for(int i=0;i<SheetFieldDefC.Listt.Count;i++){
				if(SheetFieldDefC.Listt[i].SheetDefNum!=sheetdef.SheetDefNum){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType==SheetFieldType.Image){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType==SheetFieldType.Parameter){
					continue;
					//sheetfielddefs never store parameters.
					//sheetfields do store filled parameters, but that's different.
				}
				//else{
				sheetdef.SheetFieldDefs.Add(SheetFieldDefC.Listt[i].Copy());
				//}
			}
		}

		///<summary>Gets all custom sheetdefs(without fields or parameters) for a particular type.</summary>
		public static List<SheetDef> GetCustomForType(SheetTypeEnum sheettype){
			//No need to check RemotingRole; no call to db.
			List<SheetDef> retVal=new List<SheetDef>();
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetType==sheettype){
					retVal.Add(SheetDefC.Listt[i].Copy());
				}
			}
			return retVal;
		}

		///<summary>Gets the description from the cache.</summary>
		public static string GetDescription(long sheetDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<SheetDefC.Listt.Count;i++) {
				if(SheetDefC.Listt[i].SheetDefNum==sheetDefNum) {
					return SheetDefC.Listt[i].Description;
				}
			}
			return "";
		}

		public static SheetDef GetInternalOrCustom(SheetInternalType sheetInternalType) {
			SheetDef retVal=SheetsInternal.GetSheetDef(sheetInternalType);
			SheetDef custom=GetCustomForType(retVal.SheetType).OrderBy(x => x.Description).ThenBy(x => x.SheetDefNum).FirstOrDefault();
			if(custom!=null) {
				retVal=GetSheetDef(custom.SheetDefNum);
			}
			return retVal;
		}

		public static Dictionary<SheetTypeEnum,SheetDef> GetDefaultSheetDefs(Dictionary<string,Pref> dictPrefs=null) {
			//No need to check RemotingRole; no call to db.
			if(dictPrefs==null) {
				dictPrefs=PrefC.GetDict();
			}
			Dictionary<SheetTypeEnum,SheetDef> retVal=new Dictionary<SheetTypeEnum,SheetDef>();
			SheetDef defaultRxDef=GetSheetDef(PrefC.GetDefaultSheetDefNum(SheetTypeEnum.Rx,dictPrefs),false);
			if(defaultRxDef==null) {
				defaultRxDef=SheetsInternal.GetSheetDef(SheetTypeEnum.Rx);
			}
			retVal.Add(SheetTypeEnum.Rx,defaultRxDef);
			return retVal;
		}

		public static Dictionary<SheetTypeEnum,SheetDef> GetDefaultSheetDefsForClinic(long clinicNum,Dictionary<string,Pref> dictPrefs=null) {
			//No need to check RemotingRole; no call to db.
			if(dictPrefs==null) {
				dictPrefs=PrefC.GetDict();
			}
			Dictionary<SheetTypeEnum,SheetDef> retVal=new Dictionary<SheetTypeEnum,SheetDef>();
			ClinicPref clinicPrefCur=ClinicPrefs.GetPref(Prefs.GetSheetDefPref(SheetTypeEnum.Rx),clinicNum);
			SheetDef defaultRxDef=SheetsInternal.GetSheetDef(SheetTypeEnum.Rx);
			if(clinicPrefCur!=null && PIn.Long(clinicPrefCur.ValueString)!=0) {//If ValueString is 0 then we want to keep it as the internal sheet def.
				defaultRxDef=GetSheetDef(PIn.Long(clinicPrefCur.ValueString),false);
			}
			if(clinicPrefCur!=null) {//If there was a row in the clinicpref table, add whatever the sheetdef was to the retval dictionary.
				retVal.Add(SheetTypeEnum.Rx,defaultRxDef);
			}
			return retVal;
		}

		///<summary>Passing in a clinicNum of 0 will use the base default sheet def.  Otherwise returns the clinic specific default sheetdef.</summary>
		public static SheetDef GetSheetsDefault(SheetTypeEnum sheetType,long clinicNum=0) {
			//No need to check RemotingRole; no call to db.
			ClinicPref clinicPrefCur=ClinicPrefs.GetPref(Prefs.GetSheetDefPref(sheetType),clinicNum);
			SheetDef defaultSheetDef;
			if(clinicPrefCur==null) {//If there wasn't a row for the specific clinic, use the base default sheetdef
				defaultSheetDef=GetSheetDef(PrefC.GetDefaultSheetDefNum(sheetType),false);
				if(defaultSheetDef==null) {
					defaultSheetDef=SheetsInternal.GetSheetDef(sheetType);
				}
				return defaultSheetDef;//Return the base default sheetdef
			}
			//Clinic specific sheet def found
			if(PIn.Long(clinicPrefCur.ValueString)==0) {//If ValueString is 0 then we want to keep it as the internal sheet def.
				defaultSheetDef=SheetsInternal.GetSheetDef(sheetType);
			}
			else {
				defaultSheetDef=GetSheetDef(PIn.Long(clinicPrefCur.ValueString),false);
			}
			return defaultSheetDef;
		}

		public static Dictionary<long,Dictionary<SheetTypeEnum,SheetDef>> GetAllSheetDefDefaults(List<long> listClinicNums) {
			//No need to check RemotingRole; no call to db.
			Dictionary<long,Dictionary<SheetTypeEnum,SheetDef>> retVal=new Dictionary<long,Dictionary<SheetTypeEnum,SheetDef>>();
			retVal.Add(0,GetDefaultSheetDefs());
			foreach(long clinicNum in listClinicNums) {
				if(retVal.ContainsKey(clinicNum)) {
					continue;//No need to do anything special if they list happened to contain the same clinicnum multiple times.
				}
				Dictionary<SheetTypeEnum,SheetDef> dictDefaultSheetDefs=GetDefaultSheetDefsForClinic(clinicNum);
				if(dictDefaultSheetDefs.Count>0) {
					retVal.Add(clinicNum,dictDefaultSheetDefs);
				}
			}
			return retVal;
		}

	}
}