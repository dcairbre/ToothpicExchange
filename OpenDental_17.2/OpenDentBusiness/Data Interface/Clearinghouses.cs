using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Globalization;
using CodeBase;
using System.Diagnostics;
using OpenDentBusiness.Eclaims;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Clearinghouses {

		///<summary>List of all HQ-level clearinghouses.</summary>
		private static Clearinghouse[] _hqListt;
		///<summary>Key=PayorID. Value=ClearingHouseNum.</summary>
		private static Hashtable _hqHList;
		private static object _lockObj=new object();

		#region Get Methods
		public static Clearinghouse[] HqListt{
			//No need to check RemotingRole; no call to db.
			get{
				return GetHqListt();
			}
			set{
				lock(_lockObj) {
					_hqListt=value;
				}
			}
		}

		///<summary>key:PayorID, value:ClearingHouseNum</summary>
		public static Hashtable HqHList {
			get {
				return GetHqHList();
			}
			set {
				lock(_lockObj) {
					_hqHList=value;
				}
			}
		}

		///<summary>Gets the datatable of clearinghouses from the DB. Takes care of middle tier.</summary>
		public static DataTable GetTableFromDb() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM clearinghouse WHERE ClinicNum=0 ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Clearinghouse";
			return table;
		}

		///<summary>Refreshes the cache, which only contains HQ-level clearinghouses.</summary>
		public static DataTable RefreshCacheHq() {
			//No need to check RemotingRole; no call to db.
			DataTable table = GetTableFromDb();
			for(int i=0;i<table.Rows.Count;i++) {
				table.Rows[i]["Password"]=Clearinghouses.GetRevealPassword(table.Rows[i]["Password"].ToString());
			}
			FillCacheHq(table);
			return table;
		}

		///<summary></summary>
		public static Clearinghouse[] GetHqListt() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_hqListt==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				RefreshCacheHq();
			}
			Clearinghouse[] arrayClearinghouse=new Clearinghouse[_hqListt.Length];
			lock(_lockObj) {
				for(int i=0;i<_hqListt.Length;i++) {
					arrayClearinghouse[i]=_hqListt[i].Copy();
				}
			}
			return arrayClearinghouse;
		}

		///<summary>key:PayorID, value:ClearingHouseNum</summary>
		public static Hashtable GetHqHList() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_hqHList==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				RefreshCacheHq();
			}
			Hashtable hashClearinghouses=new Hashtable();
			lock(_lockObj) {
				foreach(DictionaryEntry entry in _hqHList) {
					hashClearinghouses.Add(entry.Key,(long)entry.Value);
				}
			}
			return hashClearinghouses;
		}

		///<summary>Gets all clearinghouses for the specified clinic.  Returns an empty list if clinicNum=0.  
		///Use the cache if you want all HQ Clearinghouses.</summary>
		public static List<Clearinghouse> GetAllNonHq() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Clearinghouse>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM clearinghouse WHERE ClinicNum!=0 ORDER BY Description";
			List<Clearinghouse> clearinghouseRetVal=Crud.ClearinghouseCrud.SelectMany(command);
			clearinghouseRetVal.ForEach(x => x.Password=GetRevealPassword(x.Password));
			return clearinghouseRetVal;
		}

		///<summary>Returns a list of clearinghouses that filter out clearinghouses we no longer want to display.
		///Only includes HQ-level clearinghouses.</summary>
		public static List<Clearinghouse> GetHqListShort() {
			List<Clearinghouse> listClearinghouses=new List<Clearinghouse>(GetHqListt());
			listClearinghouses=listClearinghouses.Where(x => x.CommBridge!=EclaimsCommBridge.MercuryDE).ToList();
			return listClearinghouses;
		}

		///<summary>Returns the HQ-level default clearinghouse.  You must manually override using OverrideFields if needed.  If no default present, returns null.</summary>
		public static Clearinghouse GetDefaultEligibility() {
			//No need to check RemotingRole; no call to db.
			return GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultEligibility));
		}

		///<summary>Gets the last batch number from db for the HQ version of this clearinghouseClin and increments it by one.
		///Then saves the new value to db and returns it.  So even if the new value is not used for some reason, it will have already been incremented.
		///Remember that LastBatchNumber is never accurate with local data in memory.</summary>
		public static int GetNextBatchNumber(Clearinghouse clearinghouseClin){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),clearinghouseClin);
			}
			//get last batch number
			string command="SELECT LastBatchNumber FROM clearinghouse "
				+"WHERE ClearinghouseNum = "+POut.Long(clearinghouseClin.HqClearinghouseNum);
			DataTable table=Db.GetTable(command);
			int batchNum=PIn.Int(table.Rows[0][0].ToString());
			//and increment it by one
			if(clearinghouseClin.Eformat==ElectronicClaimFormat.Canadian){
				if(batchNum==999999){
					batchNum=1;
				}
				else{
					batchNum++;
				}
			}
			else{
				if(batchNum==999){
					batchNum=1;
				}
				else{
					batchNum++;
				}
			}
			//save the new batch number. Even if user cancels, it will have incremented.
			command="UPDATE clearinghouse SET LastBatchNumber="+batchNum.ToString()
				+" WHERE ClearinghouseNum = "+POut.Long(clearinghouseClin.HqClearinghouseNum);
			Db.NonQ(command);
			return batchNum;
		}

		///<summary>Returns the clearinghouseNum for claims for the supplied payorID.  If the payorID was not entered or if no default was set, then 0 is returned.</summary>
		public static long AutomateClearinghouseHqSelection(string payorID,EnumClaimMedType medType){
			//No need to check RemotingRole; no call to db.
			//payorID can be blank.  For example, Renaissance does not require payorID.
			if(HqHList==null) {
				RefreshCacheHq();
			}
			Clearinghouse clearinghouseHq=null;
			if(medType==EnumClaimMedType.Dental){
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultDent)==0){
					return 0;
				}
				clearinghouseHq=GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultDent));
			}
			if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional){
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultMed)==0){
					return 0;
				}
				clearinghouseHq=GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultMed));
			}
			if(clearinghouseHq==null){//we couldn't find a default clearinghouse for that medType.  Needs to always be a default.
				return 0;
			}
			if(payorID!="" && HqHList.ContainsKey(payorID)){//an override exists for this payorID
				Clearinghouse ch=GetClearinghouse((long)HqHList[payorID]);
				if(ch.Eformat==ElectronicClaimFormat.x837D_4010 || ch.Eformat==ElectronicClaimFormat.x837D_5010_dental
					|| ch.Eformat==ElectronicClaimFormat.Canadian || ch.Eformat==ElectronicClaimFormat.Ramq)
					{//all dental formats
					if(medType==EnumClaimMedType.Dental){//med type matches
						return ch.ClearinghouseNum;
					}
				}
				if(ch.Eformat==ElectronicClaimFormat.x837_5010_med_inst){
					if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional){//med type matches
						return ch.ClearinghouseNum;
					}
				}
			}
			//no override, so just return the default.
			return clearinghouseHq.ClearinghouseNum;
		}

		///<summary>Returns the HQ-level default clearinghouse.  You must manually override using OverrideFields if needed.  If no default present, returns null.</summary>
		public static Clearinghouse GetDefaultDental(){
			//No need to check RemotingRole; no call to db.
			return GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultDent));
		}

		///<summary>Gets an HQ clearinghouse from cache.  Will return null if invalid.</summary>
		public static Clearinghouse GetClearinghouse(long clearinghouseNum){
			//No need to check RemotingRole; no call to db.
			Clearinghouse[] arrayClearinghouses=Clearinghouses.GetHqListt();
			for(int i=0;i<arrayClearinghouses.Length;i++){
				if(clearinghouseNum==arrayClearinghouses[i].ClearinghouseNum){
					return arrayClearinghouses[i];
				}
			}
			return null;
		}

		///<summary>Gets revealed password for a clearinghouse password.</summary>
		public static string GetRevealPassword(string concealPassword) {
			string revealPassword="";
			CDT.Class1.RevealClearinghouse(concealPassword,out revealPassword);
			return revealPassword;
		}

		///<summary>Returns the clinic-level clearinghouse for the passed in Clearinghouse.  Usually used in conjunction with ReplaceFields().
		///Can return null.</summary>
		public static Clearinghouse GetForClinic(Clearinghouse clearinghouseHq,long clinicNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Clearinghouse>(MethodBase.GetCurrentMethod(),clearinghouseHq,clinicNum);
			}
			if(clinicNum==0) { //HQ
				return null;
			}
			string command="SELECT * FROM clearinghouse WHERE HqClearinghouseNum="+clearinghouseHq.ClearinghouseNum+" AND ClinicNum="+clinicNum;
			Clearinghouse clearinghouseRetVal=Crud.ClearinghouseCrud.SelectOne(command);
			if(clearinghouseRetVal!=null) {
				clearinghouseRetVal.Password=GetRevealPassword(clearinghouseRetVal.Password);
			}
			return clearinghouseRetVal;
		}
		#endregion

		#region Modification Methods

		#region Insert
		///<summary>Inserts one clearinghouse into the database.  Use this if you know that your clearinghouse will be inserted at the HQ-level.</summary>
		public static long Insert(Clearinghouse clearinghouse) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				clearinghouse.ClearinghouseNum=Meth.GetLong(MethodBase.GetCurrentMethod(),clearinghouse);
				return clearinghouse.ClearinghouseNum;
			}
			long clearinghouseNum=Crud.ClearinghouseCrud.Insert(clearinghouse);
			clearinghouse.HqClearinghouseNum=clearinghouseNum;
			Crud.ClearinghouseCrud.Update(clearinghouse);
			return clearinghouseNum;
		}
		#endregion

		#region Update

		///<summary>Updates the clearinghouse in the database that has the same primary key as the passed-in clearinghouse.   
		///Use this if you know that your clearinghouse will be updated at the HQ-level, 
		///or if you already have a well-defined clinic-level clearinghouse.  For lists of clearinghouses, use the Sync method instead.</summary>
		public static void Update(Clearinghouse clearinghouse) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearinghouse);
				return;
			}
			Crud.ClearinghouseCrud.Update(clearinghouse);
		}

		public static void Update(Clearinghouse clearinghouse,Clearinghouse oldClearinghouse) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearinghouse,oldClearinghouse);
				return;
			}
			Crud.ClearinghouseCrud.Update(clearinghouse,oldClearinghouse);
		}

		///<summary>Syncs a given list of clinic-level clearinghouses to a list of old clinic-level clearinghouses.</summary>
		public static void Sync(List<Clearinghouse> listClearinghouseNew,List<Clearinghouse> listClearinghouseOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listClearinghouseNew,listClearinghouseOld);
				return;
			}
			Crud.ClearinghouseCrud.Sync(listClearinghouseNew,listClearinghouseOld);
		}
		#endregion

		#region Delete
		///<summary>Deletes the passed-in Hq clearinghouse for all clinics.  Only pass in clearinghouses with ClinicNum==0.</summary>
		public static void Delete(Clearinghouse clearinghouseHq) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearinghouseHq);
				return;
			}
			string command="DELETE FROM clearinghouse WHERE ClearinghouseNum = '"+POut.Long(clearinghouseHq.ClearinghouseNum)+"'";
			Db.NonQ(command);
			command="DELETE FROM clearinghouse WHERE HqClearinghouseNum='"+POut.Long(clearinghouseHq.ClearinghouseNum)+"'";
			Db.NonQ(command);
		}
		#endregion

		#endregion

		#region Misc Methods
		///<summary>Fills the cache, which only contains HQ-level clearinghouses.</summary>
		public static void FillCacheHq(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Clearinghouse> hqListt=Crud.ClearinghouseCrud.TableToList(table);
			Hashtable hqHList=new Hashtable();
			foreach(Clearinghouse cHouse in hqListt) {
				foreach(string payorID in cHouse.Payors.Split(',')) {
					if(!hqHList.ContainsKey(payorID)) {
						hqHList.Add(payorID,cHouse.ClearinghouseNum);
					}
				}
			}
			//Possible race condition. Mitigated by using local lists and waiting until the last moment to assign them to the private lists.
			_hqHList=hqHList;
			_hqListt=hqListt.ToArray();
		}

		///<summary>Replaces all clinic-level fields in ClearinghouseHq with non-blank fields 
		///from the clinic-level clearinghouse for the passed-in clinicNum. Non clinic-level fields are not replaced.</summary>
		public static Clearinghouse OverrideFields(Clearinghouse clearinghouseHq,long clinicNum) {
			//No need to check RemotingRole; no call to db.
			Clearinghouse clearinghouseClin=Clearinghouses.GetForClinic(clearinghouseHq,clinicNum);
			return OverrideFields(clearinghouseHq,clearinghouseClin);
		}

		///<summary>Replaces all clinic-level fields in ClearinghouseHq with non-blank fields in clearinghouseClin.
		///Non clinic-level fields are commented out and not replaced.</summary>
		public static Clearinghouse OverrideFields(Clearinghouse clearinghouseHq,Clearinghouse clearinghouseClin) {
			//No need to check RemotingRole; no call to db.
			if(clearinghouseHq==null) {
				return null;
			}
			Clearinghouse clearinghouseRetVal=clearinghouseHq.Copy();
			if(clearinghouseClin==null) { //if a null clearingHouseClin was passed in, just return clearinghouseHq.
				return clearinghouseRetVal;
			}
			//HqClearinghouseNum must be set for refreshing the cache when deleting.
			clearinghouseRetVal.HqClearinghouseNum=clearinghouseClin.HqClearinghouseNum;
			//ClearinghouseNum must be set so that updates do not create new entries every time.
			clearinghouseRetVal.ClearinghouseNum=clearinghouseClin.ClearinghouseNum;
			//ClinicNum must be set so that the correct clinic is assigned when inserting new clinic level clearinghouses.
			clearinghouseRetVal.ClinicNum=clearinghouseClin.ClinicNum;
			clearinghouseRetVal.IsEraDownloadAllowed=clearinghouseClin.IsEraDownloadAllowed;
			clearinghouseRetVal.IsClaimExportAllowed=clearinghouseClin.IsClaimExportAllowed;
			//fields that should not be replaced are commented out.
			//if(!String.IsNullOrEmpty(clearinghouseClin.Description)) {
			//	clearinghouseRetVal.Description=clearinghouseClin.Description;
			//}
			if(!String.IsNullOrEmpty(clearinghouseClin.ExportPath)) {
				clearinghouseRetVal.ExportPath=clearinghouseClin.ExportPath;
			}
			//if(!String.IsNullOrEmpty(clearinghouseClin.Payors)) {
			//	clearinghouseRetVal.Payors=clearinghouseClin.Payors;
			//}
			//if(clearinghouseClin.Eformat!=0 && clearinghouseClin.Eformat!=null) {
			//	clearinghouseRetVal.Eformat=clearinghouseClin.Eformat;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA05)) {
			//	clearinghouseRetVal.ISA05=clearinghouseClin.ISA05;
			//}
			if(!String.IsNullOrEmpty(clearinghouseClin.SenderTIN)) {
				clearinghouseRetVal.SenderTIN=clearinghouseClin.SenderTIN;
			}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA07)) {
			//	clearinghouseRetVal.ISA07=clearinghouseClin.ISA07;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA08)) {
			//	clearinghouseRetVal.ISA08=clearinghouseClin.ISA08;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA15)) {
			//	clearinghouseRetVal.ISA15=clearinghouseClin.ISA15;
			//}
			if(!String.IsNullOrEmpty(clearinghouseClin.Password)) {
				clearinghouseRetVal.Password=clearinghouseClin.Password;
			}
			if(!String.IsNullOrEmpty(clearinghouseClin.ResponsePath)) {
				clearinghouseRetVal.ResponsePath=clearinghouseClin.ResponsePath;
			}
			//if(clearinghouseClin.CommBridge!=0 && clearinghouseClin.CommBridge!=null) {
			//	clearinghouseRetVal.CommBridge=clearinghouseClin.CommBridge;
			//}
			if(!String.IsNullOrEmpty(clearinghouseClin.ClientProgram)) {
				clearinghouseRetVal.ClientProgram=clearinghouseClin.ClientProgram;
			}
			//clearinghouseRetVal.LastBatchNumber=;//Not editable is UI and should not be updated here.  See GetNextBatchNumber() above.
			//if(clearinghouseClin.ModemPort!=0 && clearinghouseClin.ModemPort!=null) {
			//	clearinghouseRetVal.ModemPort=clearinghouseClin.ModemPort;
			//}
			if(!String.IsNullOrEmpty(clearinghouseClin.LoginID)) {
				clearinghouseRetVal.LoginID=clearinghouseClin.LoginID;
			}
			if(!String.IsNullOrEmpty(clearinghouseClin.SenderName)) {
				clearinghouseRetVal.SenderName=clearinghouseClin.SenderName;
			}
			if(!String.IsNullOrEmpty(clearinghouseClin.SenderTelephone)) {
				clearinghouseRetVal.SenderTelephone=clearinghouseClin.SenderTelephone;
			}
			//if(!String.IsNullOrEmpty(clearinghouseClin.GS03)) {
			//	clearinghouseRetVal.GS03=clearinghouseClin.GS03;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA02)) {
			//	clearinghouseRetVal.ISA02=clearinghouseClin.ISA02;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA04)) {
			//	clearinghouseRetVal.ISA04=clearinghouseClin.ISA04;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.ISA16)) {
			//	clearinghouseRetVal.ISA16=clearinghouseClin.ISA16;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.SeparatorData)) {
			//	clearinghouseRetVal.SeparatorData=clearinghouseClin.SeparatorData;
			//}
			//if(!String.IsNullOrEmpty(clearinghouseClin.SeparatorSegment)) {
			//	clearinghouseRetVal.SeparatorSegment=clearinghouseClin.SeparatorSegment;
			//}
			return clearinghouseRetVal;
		}

		public static void RetrieveReportsAutomatic() {
			Clearinghouse[] arrayClearinghousesHq=GetHqListt();
			long defaultClearingHouseNum=PrefC.GetLong(PrefName.ClearinghouseDefaultDent);
			for(int i=0;i<arrayClearinghousesHq.Length;i++) {
				Clearinghouse clearinghouseHq=arrayClearinghousesHq[i];
				Clearinghouse clearinghouseClin=OverrideFields(clearinghouseHq,Clinics.ClinicNum);
				if(!Directory.Exists(clearinghouseClin.ResponsePath)) {
					continue;
				}
				if(clearinghouseHq.ClearinghouseNum==defaultClearingHouseNum) {//If it's the default dental clearinghouse
					RetrieveAndImport(clearinghouseClin,true);
				}
				else if(clearinghouseHq.Eformat==ElectronicClaimFormat.None) {//And the format is "None" (accessed from all regions)
					RetrieveAndImport(clearinghouseClin,true);
				}
				else if(clearinghouseHq.CommBridge==EclaimsCommBridge.BCBSGA) {
					BCBSGA.Retrieve(clearinghouseClin,true,new TerminalConnector());
				}
				else if(clearinghouseHq.Eformat==ElectronicClaimFormat.Canadian && CultureInfo.CurrentCulture.Name.EndsWith("CA")) {
					//Or the Eformat is Canadian and the region is Canadian.  In Canada, the "Outstanding Reports" are received upon request.
					//Canadian reports must be retrieved using an office num and valid provider number for the office,
					//which will cause all reports for that office to be returned.
					//Here we loop through all providers and find CDAnet providers with a valid provider number and office number, and we only send
					//one report download request for one provider from each office.  For most offices, the loop will only send a single request.
					List<Provider> listProvs=ProviderC.GetListShort();
					List<string> listOfficeNums=new List<string>();
					for(int j=0;j<listProvs.Count;j++) {//Get all unique office numbers from the providers.
						if(!listProvs[j].IsCDAnet || listProvs[j].NationalProvID=="" || listProvs[j].CanadianOfficeNum=="") {
							continue;
						}
						if(!listOfficeNums.Contains(listProvs[j].CanadianOfficeNum)) {//Ignore duplicate office numbers.
							listOfficeNums.Add(listProvs[j].CanadianOfficeNum);
							try {
								clearinghouseHq=Eclaims.Canadian.GetCanadianClearinghouseHq(null);
								clearinghouseClin=Clearinghouses.OverrideFields(clearinghouseHq,Clinics.ClinicNum);
								Eclaims.CanadianOutput.GetOutstandingTransactions(clearinghouseClin,false,true,null,listProvs[j],true,null,null);
							}
							catch {
								//Supress errors importing reports.
							}
						}
					}
				}
				else if(clearinghouseHq.Eformat==ElectronicClaimFormat.Dutch && CultureInfo.CurrentCulture.Name.EndsWith("DE")) {
					//Or the Eformat is German and the region is German
					RetrieveAndImport(clearinghouseClin,true);
				}
				else if(clearinghouseHq.Eformat!=ElectronicClaimFormat.Canadian
					&& clearinghouseHq.Eformat!=ElectronicClaimFormat.Dutch
					&& CultureInfo.CurrentCulture.Name.EndsWith("US")) //Or the Eformat is in any other format and the region is US
				{
					RetrieveAndImport(clearinghouseClin,true);
				}
			}
		}

		private static string RetrieveReports(Clearinghouse clearinghouseClin,bool isAutomaticMode) {
			if(clearinghouseClin.ISA08=="113504607") {//TesiaLink
																								//But the import will still happen
				return "";
			}
			if(clearinghouseClin.CommBridge==EclaimsCommBridge.None
				|| clearinghouseClin.CommBridge==EclaimsCommBridge.Renaissance
				|| clearinghouseClin.CommBridge==EclaimsCommBridge.RECS) 
			{
				return "";
			}
			if(clearinghouseClin.CommBridge==EclaimsCommBridge.WebMD) {
				if(!WebMD.Launch(clearinghouseClin,0,isAutomaticMode)) {
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+WebMD.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.BCBSGA) {
				if(!BCBSGA.Retrieve(clearinghouseClin,true,new TerminalConnector())) {
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+BCBSGA.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.ClaimConnect) {
				if(!Directory.Exists(clearinghouseClin.ResponsePath)) {
					//The clearinghouse report path is not setup.  Therefore, the customer does not use ClaimConnect reports via web services.
					if(isAutomaticMode) {//The user opened FormClaimsSend, or FormOpenDental called this function automatically.
						return "";//Suppress error message.
					}
					else {//The user pressed the Get Reports button manually.
								//This cannot happen, because the user is blocked by the UI before they get to this point.
					}
				}
				else if(!ClaimConnect.Retrieve(clearinghouseClin)) {
					if(ClaimConnect.ErrorMessage.Contains(": 150\r\n")) {//Error message 150 "Service Not Contracted"
						if(isAutomaticMode) {//The user opened FormClaimsSend, or FormOpenDental called this function automatically.
							return "";//Pretend that there is no error when loading FormClaimsSend for those customers who do not pay for ERA service.
						}
						else {//The user pressed the Get Reports button manually.
									//The old way.  Some customers still prefer to go to the dentalxchange web portal to view reports because the ERA service costs money.
							try {
								Process.Start(@"http://www.dentalxchange.com");
							}
							catch(Exception ex) {
								ex.DoNothing();
								return Lans.g("FormClaimReports","Could not locate the site.");
							}
							return "";
						}
					}
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+ClaimConnect.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.AOS) {
				try {
					//This path would never exist on Unix, so no need to handle back slashes.
					Process.Start(@"C:\Program files\AOS\AOSCommunicator\AOSCommunicator.exe");
				}
				catch {
					return Lans.g("FormClaimReports","Could not locate the file.");
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.MercuryDE) {
				if(!MercuryDE.Launch(clearinghouseClin,0)) {
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+MercuryDE.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.EmdeonMedical) {
				if(!EmdeonMedical.Retrieve(clearinghouseClin)) {
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+EmdeonMedical.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.DentiCal) {
				if(!DentiCal.Launch(clearinghouseClin,0)) {
					return Lans.g("FormClaimReports","Error retrieving.")+"\r\n"+DentiCal.ErrorMessage;
				}
			}
			else if(clearinghouseClin.CommBridge==EclaimsCommBridge.EDS) {
				if(!EDS.Retrieve(clearinghouseClin)) {
					return Lans.g("FormClaimReports","Error retrieving. ")+"\r\n"+EDS.ErrorMessage;
				}
			}
			return "";
		}

		///<summary>Takes any files found in the reports folder for the clearinghouse, and imports them into the database.  Deletes the original files.
		///No longer any such thing as archive.</summary>
		private static void ImportReportFiles(Clearinghouse clearinghouseClin) { //uses clinic-level clearinghouse where necessary.
			if(!Directory.Exists(clearinghouseClin.ResponsePath)) {
				return;
			}
			if(clearinghouseClin.Eformat==ElectronicClaimFormat.Canadian || clearinghouseClin.Eformat==ElectronicClaimFormat.Ramq) {
				//the report path is shared with many other important files.  Do not process anything.  Comm is synchronous only.
				return;
			}
			string[] files=Directory.GetFiles(clearinghouseClin.ResponsePath);
			string archiveDir=ODFileUtils.CombinePaths(clearinghouseClin.ResponsePath,"Archive");
			if(!Directory.Exists(archiveDir)) {
				Directory.CreateDirectory(archiveDir);
			}
			for(int i=0;i<files.Length;i++) {
				try {
					Etranss.ProcessIncomingReport(
						File.GetCreationTime(files[i]),
						clearinghouseClin.HqClearinghouseNum,
						File.ReadAllText(files[i]),
						Security.CurUser.UserNum);
				}
				catch(Exception ex) {
					ex.DoNothing();
					continue;//Skip current report file and leave in folder to processing later.
				}
				try {
					File.Copy(files[i],ODFileUtils.CombinePaths(archiveDir,Path.GetFileName(files[i])));
				}
				catch(Exception ex) {
					ex.DoNothing();//OK to continue and delete file, since ProcessIncomingReport() above saved the raw report into the etrans table.
				}
				File.Delete(files[i]);
			}
		}

		///<summary></summary>
		public static string RetrieveAndImport(Clearinghouse clearinghouseClin,bool isAutomaticMode) {
			DateTime timeLastReport=PIn.DateT(PrefC.GetStringNoCache(PrefName.ClaimReportReceiveLastDateTime));
			double timeReceiveInternal=PIn.Double(PrefC.GetStringNoCache(PrefName.ClaimReportReceiveInterval));//Interval in minutes.
			double timeDiff=DateTime.Now.Subtract(timeLastReport).TotalMinutes;
			string errorMessage="";
			if(isAutomaticMode && timeDiff < timeReceiveInternal) {
				//Automatically retrieving reports from this computer and the report interval has not passed yet.
			}
			else if(!isAutomaticMode && timeDiff < 1) {
				//When the user presses the Get Reports button manually we allow them to get reports up to once per minute.
			}
			else {//Timer interval OK.  Now we can retrieve the reports from web services.
				Prefs.UpdateDateT(PrefName.ClaimReportReceiveLastDateTime,DateTime.Now);
				errorMessage=RetrieveReports(clearinghouseClin,isAutomaticMode);
				//Don't return yet even if there was an error. This is so that Open Dental will automatically import reports that have been manually
				//downloaded to the Reports folder.
			}
			ImportReportFiles(clearinghouseClin);
			return errorMessage;
		}

		#endregion

	}
}