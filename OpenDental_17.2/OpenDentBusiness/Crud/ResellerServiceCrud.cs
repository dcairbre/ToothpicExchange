//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ResellerServiceCrud {
		///<summary>Gets one ResellerService object from the database using the primary key.  Returns null if not found.</summary>
		public static ResellerService SelectOne(long resellerServiceNum){
			string command="SELECT * FROM resellerservice "
				+"WHERE ResellerServiceNum = "+POut.Long(resellerServiceNum);
			List<ResellerService> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ResellerService object from the database using a query.</summary>
		public static ResellerService SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ResellerService> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ResellerService objects from the database using a query.</summary>
		public static List<ResellerService> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ResellerService> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ResellerService> TableToList(DataTable table){
			List<ResellerService> retVal=new List<ResellerService>();
			ResellerService resellerService;
			foreach(DataRow row in table.Rows) {
				resellerService=new ResellerService();
				resellerService.ResellerServiceNum= PIn.Long  (row["ResellerServiceNum"].ToString());
				resellerService.ResellerNum       = PIn.Long  (row["ResellerNum"].ToString());
				resellerService.CodeNum           = PIn.Long  (row["CodeNum"].ToString());
				resellerService.Fee               = PIn.Double(row["Fee"].ToString());
				retVal.Add(resellerService);
			}
			return retVal;
		}

		///<summary>Converts a list of ResellerService into a DataTable.</summary>
		public static DataTable ListToTable(List<ResellerService> listResellerServices,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="ResellerService";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("ResellerServiceNum");
			table.Columns.Add("ResellerNum");
			table.Columns.Add("CodeNum");
			table.Columns.Add("Fee");
			foreach(ResellerService resellerService in listResellerServices) {
				table.Rows.Add(new object[] {
					POut.Long  (resellerService.ResellerServiceNum),
					POut.Long  (resellerService.ResellerNum),
					POut.Long  (resellerService.CodeNum),
					POut.Double(resellerService.Fee),
				});
			}
			return table;
		}

		///<summary>Inserts one ResellerService into the database.  Returns the new priKey.</summary>
		public static long Insert(ResellerService resellerService){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				resellerService.ResellerServiceNum=DbHelper.GetNextOracleKey("resellerservice","ResellerServiceNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(resellerService,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							resellerService.ResellerServiceNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(resellerService,false);
			}
		}

		///<summary>Inserts one ResellerService into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ResellerService resellerService,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				resellerService.ResellerServiceNum=ReplicationServers.GetKey("resellerservice","ResellerServiceNum");
			}
			string command="INSERT INTO resellerservice (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ResellerServiceNum,";
			}
			command+="ResellerNum,CodeNum,Fee) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(resellerService.ResellerServiceNum)+",";
			}
			command+=
				     POut.Long  (resellerService.ResellerNum)+","
				+    POut.Long  (resellerService.CodeNum)+","
				+"'"+POut.Double(resellerService.Fee)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				resellerService.ResellerServiceNum=Db.NonQ(command,true);
			}
			return resellerService.ResellerServiceNum;
		}

		///<summary>Inserts one ResellerService into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ResellerService resellerService){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(resellerService,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					resellerService.ResellerServiceNum=DbHelper.GetNextOracleKey("resellerservice","ResellerServiceNum"); //Cacheless method
				}
				return InsertNoCache(resellerService,true);
			}
		}

		///<summary>Inserts one ResellerService into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ResellerService resellerService,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO resellerservice (";
			if(!useExistingPK && isRandomKeys) {
				resellerService.ResellerServiceNum=ReplicationServers.GetKeyNoCache("resellerservice","ResellerServiceNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ResellerServiceNum,";
			}
			command+="ResellerNum,CodeNum,Fee) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(resellerService.ResellerServiceNum)+",";
			}
			command+=
				     POut.Long  (resellerService.ResellerNum)+","
				+    POut.Long  (resellerService.CodeNum)+","
				+"'"+POut.Double(resellerService.Fee)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				resellerService.ResellerServiceNum=Db.NonQ(command,true);
			}
			return resellerService.ResellerServiceNum;
		}

		///<summary>Updates one ResellerService in the database.</summary>
		public static void Update(ResellerService resellerService){
			string command="UPDATE resellerservice SET "
				+"ResellerNum       =  "+POut.Long  (resellerService.ResellerNum)+", "
				+"CodeNum           =  "+POut.Long  (resellerService.CodeNum)+", "
				+"Fee               = '"+POut.Double(resellerService.Fee)+"' "
				+"WHERE ResellerServiceNum = "+POut.Long(resellerService.ResellerServiceNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ResellerService in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ResellerService resellerService,ResellerService oldResellerService){
			string command="";
			if(resellerService.ResellerNum != oldResellerService.ResellerNum) {
				if(command!=""){ command+=",";}
				command+="ResellerNum = "+POut.Long(resellerService.ResellerNum)+"";
			}
			if(resellerService.CodeNum != oldResellerService.CodeNum) {
				if(command!=""){ command+=",";}
				command+="CodeNum = "+POut.Long(resellerService.CodeNum)+"";
			}
			if(resellerService.Fee != oldResellerService.Fee) {
				if(command!=""){ command+=",";}
				command+="Fee = '"+POut.Double(resellerService.Fee)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE resellerservice SET "+command
				+" WHERE ResellerServiceNum = "+POut.Long(resellerService.ResellerServiceNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ResellerService,ResellerService) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ResellerService resellerService,ResellerService oldResellerService) {
			if(resellerService.ResellerNum != oldResellerService.ResellerNum) {
				return true;
			}
			if(resellerService.CodeNum != oldResellerService.CodeNum) {
				return true;
			}
			if(resellerService.Fee != oldResellerService.Fee) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ResellerService from the database.</summary>
		public static void Delete(long resellerServiceNum){
			string command="DELETE FROM resellerservice "
				+"WHERE ResellerServiceNum = "+POut.Long(resellerServiceNum);
			Db.NonQ(command);
		}

	}
}