using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace OpenDentBusiness.Crud{
	///<summary>Does not support Oracle. Uses customer connection strings. NOT AUTO GENERATED.</summary>
	public class BugCrud {
		///<summary>Gets one Bug object from the database using the primary key.  Returns null if not found.</summary>
		public static Bug SelectOne(long bugId){
			string command="SELECT * FROM bug "
				+"WHERE BugId = "+POut.Long(bugId);
			List<Bug> list=TableToList(Db.GetTable(command));//Specifically using datacore since this could be called on the middle tier
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Bug object from the database using a query.</summary>
		public static Bug SelectOne(string command){
			List<Bug> list=TableToList(Db.GetTable(command));//Specifically using datacore since this could be called on the middle tier
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Bug objects from the database using a query.</summary>
		public static List<Bug> SelectMany(string command){
			List<Bug> list=TableToList(Db.GetTable(command));//Specifically using datacore since this could be called on the middle tier
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Bug> TableToList(DataTable table){
			List<Bug> retVal=new List<Bug>();
			Bug bug;
			foreach(DataRow row in table.Rows) {
				bug=new Bug();
				bug.BugId        = PIn.Long  (row["BugId"].ToString());
				bug.CreationDate = PIn.DateT (row["CreationDate"].ToString());
				bug.Status_      = (BugStatus)Enum.Parse(typeof(OpenDentBusiness.BugStatus),row["Status_"].ToString());
				bug.Type_        = (BugType)Enum.Parse(typeof(OpenDentBusiness.BugType),row["Type_"].ToString());
				bug.PriorityLevel= PIn.Int   (row["PriorityLevel"].ToString());
				bug.VersionsFound= PIn.String(row["VersionsFound"].ToString());
				bug.VersionsFixed= PIn.String(row["VersionsFixed"].ToString());
				bug.Description  = PIn.String(row["Description"].ToString());
				bug.LongDesc     = PIn.String(row["LongDesc"].ToString());
				bug.PrivateDesc  = PIn.String(row["PrivateDesc"].ToString());
				bug.Discussion   = PIn.String(row["Discussion"].ToString());
				bug.Submitter    = PIn.Long  (row["Submitter"].ToString());
				retVal.Add(bug);
			}
			return retVal;
		}

		///<summary>Updates one Bug in the database.</summary>
		public static void Update(Bug bug){
			string command="UPDATE bug SET "
				+"CreationDate =  "+POut.DateT (bug.CreationDate)+", "
				+"Status_      =  '"+POut.String(bug.Status_.ToString())+"', "
				+"Type_        =  '"+POut.String(bug.Type_.ToString())+"', "
				+"PriorityLevel=  "+POut.Int	 (bug.PriorityLevel)+", "
				+"VersionsFound= '"+POut.String(bug.VersionsFound)+"', "
				+"VersionsFixed= '"+POut.String(bug.VersionsFixed)+"', "
				+"Description  = '"+POut.String(bug.Description)+"', "
				+"LongDesc     = '"+POut.String(bug.LongDesc)+"', "
				+"PrivateDesc  = '"+POut.String(bug.PrivateDesc)+"', "
				+"Discussion   = '"+POut.String(bug.Discussion)+"', "
				+"Submitter    =  "+POut.Long  (bug.Submitter)+" "
				+"WHERE BugId = "+POut.Long(bug.BugId);
			Db.NonQ(command);
		}

		///<summary>Updates one Bug in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Bug bug,Bug oldBug){
			string command="";
			if(bug.CreationDate != oldBug.CreationDate) {
				if(command!=""){ command+=",";}
				command+="CreationDate = "+POut.DateT(bug.CreationDate)+"";
			}
			if(bug.Status_ != oldBug.Status_) {
				if(command!=""){ command+=",";}
				command+="Status_ = '"+POut.String   (bug.Status_.ToString())+"'";
			}
			if(bug.Type_ != oldBug.Type_) {
				if(command!=""){ command+=",";}
				command+="Type_ = '"+POut.String   (bug.Type_.ToString())+"'";
			}
			if(bug.PriorityLevel != oldBug.PriorityLevel) {
				if(command!=""){ command+=",";}
				command+="PriorityLevel = "+POut.Int(bug.PriorityLevel)+"";
			}
			if(bug.VersionsFound != oldBug.VersionsFound) {
				if(command!=""){ command+=",";}
				command+="VersionsFound = '"+POut.String(bug.VersionsFound)+"'";
			}
			if(bug.VersionsFixed != oldBug.VersionsFixed) {
				if(command!=""){ command+=",";}
				command+="VersionsFixed = '"+POut.String(bug.VersionsFixed)+"'";
			}
			if(bug.Description != oldBug.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(bug.Description)+"'";
			}
			if(bug.LongDesc != oldBug.LongDesc) {
				if(command!=""){ command+=",";}
				command+="LongDesc = '"+POut.String(bug.LongDesc)+"'";
			}
			if(bug.PrivateDesc != oldBug.PrivateDesc) {
				if(command!=""){ command+=",";}
				command+="PrivateDesc = '"+POut.String(bug.PrivateDesc)+"'";
			}
			if(bug.Discussion != oldBug.Discussion) {
				if(command!=""){ command+=",";}
				command+="Discussion = '"+POut.String(bug.Discussion)+"'";
			}
			if(bug.Submitter != oldBug.Submitter) {
				if(command!=""){ command+=",";}
				command+="Submitter = "+POut.Long(bug.Submitter)+"";
			}
			if(command==""){
				return false;
			}
			command="UPDATE bug SET "+command
				+" WHERE BugId = "+POut.Long(bug.BugId);
			Db.NonQ(command);
			return true;
		}

		///<summary>Deletes one Bug from the database.</summary>
		public static void Delete(long bugId){
			string command="DELETE FROM bug "
				+"WHERE BugId = "+POut.Long(bugId);
			Db.NonQ(command);
		}


	}
}