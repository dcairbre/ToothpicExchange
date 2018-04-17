using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;

namespace OpenDentBusiness{
	///<summary>Employers are refreshed as needed. A full refresh is frequently triggered if an employerNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
	public class Employers{
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

		private static Employer[] list;
		private static Hashtable hList;

		public static Employer[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>A hashtable of all employers.</summary>
		public static Hashtable HList {
			//No need to check RemotingRole; no call to db.
			get {
				if(hList==null) {
					RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			//As of 12/15/2011 we are not making use of Address info.  Selecting empty strings makes loading the cache much faster. 
			string command="SELECT EmployerNum,EmpName,'' Address,'' Address2,'' City,'' State,'' Zip,'' Phone FROM employer";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Employer";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.EmployerCrud.TableToList(table).ToArray();
			HList=new Hashtable();
			for(int i=0;i<list.Length;i++) {
				HList.Add(list[i].EmployerNum,list[i]);
			}
		}

		/*
		 * Not using this because it turned out to be more efficient to refresh the whole
		 * list if an empnum could not be found.
		///<summary>Just refreshes Cur from the db with info for one employer.</summary>
		public static void Refresh(int employerNum){
			Cur=new Employer();//just in case no rows are returned
			if(employerNum==0) return;
			string command="SELECT * FROM employer WHERE EmployerNum = '"+employerNum+"'";
			DataTable table=Db.GetTable(command);;
			for(int i=0;i<table.Rows.Count;i++){//almost always just 1 row, but sometimes 0
				Cur.EmployerNum   =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.EmpName       =PIn.PString(table.Rows[i][1].ToString());
			}
		}*/

		///<summary>No need to pass in usernum, it is set before the remoting role and passed in for logging.</summary>
		public static void Update(Employer empCur, Employer empOld, long userNum = 0) {
			if(RemotingClient.RemotingRole!=RemotingRole.ServerWeb) {
				userNum=Security.CurUser.UserNum;//must be before normal remoting role check to get user at workstation
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),empCur,empOld,userNum);
				return;
			}
			Crud.EmployerCrud.Update(empCur,empOld);
			InsEditLogs.MakeLogEntry(empCur,empOld,InsEditLogType.Employer,userNum);
		}

		///<summary>No need to pass in usernum, it is set before the remoting role and passed in for logging.</summary>
		public static long Insert(Employer Cur, long userNum = 0) {
			if(RemotingClient.RemotingRole!=RemotingRole.ServerWeb) {
				userNum=Security.CurUser.UserNum;//must be before normal remoting role check to get user at workstation
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.EmployerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur,userNum);
				return Cur.EmployerNum;
			}
			InsEditLogs.MakeLogEntry(Cur,null,InsEditLogType.Employer,userNum);
			return Crud.EmployerCrud.Insert(Cur);
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  
		///This is only called from FormEmployers after proper validation.
		///No need to pass in usernum, it is set before the remoting role and passed in for logging.</summary>
		public static void Delete(Employer Cur,long userNum=0) {
			if(RemotingClient.RemotingRole!=RemotingRole.ServerWeb) {
				userNum=Security.CurUser.UserNum;//must be before normal remoting role check to get user at workstation
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur,userNum);
				return;
			}
			string command="DELETE from employer WHERE EmployerNum = '"+Cur.EmployerNum.ToString()+"'";
			Db.NonQ(command);
			InsEditLogs.MakeLogEntry(null,Cur,InsEditLogType.Employer,userNum);
		}

		///<summary>Returns a list of patients that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentPatients(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient" 
				+" WHERE EmployerNum = '"+POut.Long(Cur.EmployerNum)+"'";
			DataTable table=Db.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Returns a list of insplans that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentInsPlans(Employer Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT carrier.CarrierName,CONCAT(CONCAT(patient.LName,', '),patient.FName) "
				+"FROM insplan "
				+"LEFT JOIN inssub ON insplan.PlanNum=inssub.PlanNum "
				+"LEFT JOIN patient ON inssub.Subscriber=patient.PatNum "
				+"LEFT JOIN carrier ON insplan.CarrierNum=carrier.CarrierNum "
				+"WHERE insplan.EmployerNum = "+POut.Long(Cur.EmployerNum);
			DataTable table=Db.GetTable(command);
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.String(table.Rows[i][1].ToString())+": "+PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of an employer based on the employerNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(long employerNum) {
			//No need to check RemotingRole; no call to db.
			if(employerNum==0){
				return "";
			}
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//if the employerNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//this could only happen if corrupted or we're looking up an employer that no longer exists:
			return "";
		}

		///<summary>Gets an employer based on the employerNum. This will work even if the list has not been refreshed recently, but if you are going to need a lot of names all at once, then it is faster to refresh first.</summary>
		public static Employer GetEmployer(long employerNum) {
			//No need to check RemotingRole; no call to db.
			if(employerNum==0){
				return new Employer();
			}
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//if the employerNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//this could only happen if corrupted:
			return new Employer();
		}

		///<summary>Gets an employerNum from the database based on the supplied name.  If that empName does not exist, then a new employer is created, and the employerNum for the new employer is returned.</summary>
		public static long GetEmployerNum(string empName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),empName);
			}
			if(empName==""){
				return 0;
			}
			string command="SELECT EmployerNum FROM employer" 
				+" WHERE EmpName = '"+POut.String(empName)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				return PIn.Long(table.Rows[0][0].ToString());
			}
			Employer Cur=new Employer();
			Cur.EmpName=empName;
			Insert(Cur);
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return Cur.EmployerNum;
		}

		///<summary>Returns an employer if an exact match is found for the text supplied in the database.  Returns null if nothing found.</summary>
		public static Employer GetByName(string empName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Employer>(MethodBase.GetCurrentMethod(),empName);
			}
			string command="SELECT * FROM employer WHERE EmpName = '"+POut.String(empName)+"'";
			return Crud.EmployerCrud.SelectOne(command);
		}

		///<summary>Returns an arraylist of Employers with names similar to the supplied string.  Used in dropdown list from employer field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one employer doesn't show in dropdown.</summary>
		public static List<Employer> GetSimilarNames(string empName){
			//No need to check RemotingRole; no call to db.
			List<Employer> retVal=new List<Employer>();
			for(int i=0;i<List.Length;i++){
				//if(Regex.IsMatch(List[i].EmpName,"^"+empName,RegexOptions.IgnoreCase))
				if(List[i].EmpName.StartsWith(empName,StringComparison.CurrentCultureIgnoreCase)){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Combines all the given employers into one. Updates patient and insplan. Then deletes all the others.
		///No need to pass in usernum, it is set before the remoting role and passed in for logging.</summary>
		public static void Combine(List<long> employerNums, long userNum=0) {
			if(RemotingClient.RemotingRole!=RemotingRole.ServerWeb) {
				userNum=Security.CurUser.UserNum;//must be before normal remoting role check to get user at workstation
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employerNums,userNum);
				return;
			}
			long newNum=employerNums[0];
			for(int i=1;i<employerNums.Count;i++) {
				string command="UPDATE patient SET EmployerNum = "+POut.Long(newNum)
					+" WHERE EmployerNum = "+POut.Long(employerNums[i]);
				Db.NonQ(command);
				command="SELECT PlanNum FROM insplan WHERE EmployerNum = "+POut.Long(employerNums[i]);
				List<long> listPlanNums=Db.GetListLong(command);
				command="UPDATE insplan SET EmployerNum = "+POut.Long(newNum)
					+" WHERE EmployerNum = "+POut.Long(employerNums[i]);
				Db.NonQ(command);
				listPlanNums.ForEach(x => { //log updated employernums for insplan.
					InsEditLogs.MakeLogEntry("EmployerNum",userNum,employerNums[i].ToString(),newNum.ToString(),InsEditLogType.InsPlan,x,0);
				});
				Employer employerCur=Employers.GetEmployer(employerNums[i]);//from the cache
				Employers.Delete(employerCur); //logging taken care of in Delete method.
			}
		}

	}

	
	

}













