//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ProviderIdentCrud {
		///<summary>Gets one ProviderIdent object from the database using the primary key.  Returns null if not found.</summary>
		public static ProviderIdent SelectOne(long providerIdentNum){
			string command="SELECT * FROM providerident "
				+"WHERE ProviderIdentNum = "+POut.Long(providerIdentNum);
			List<ProviderIdent> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ProviderIdent object from the database using a query.</summary>
		public static ProviderIdent SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ProviderIdent> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ProviderIdent objects from the database using a query.</summary>
		public static List<ProviderIdent> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ProviderIdent> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ProviderIdent> TableToList(DataTable table){
			List<ProviderIdent> retVal=new List<ProviderIdent>();
			ProviderIdent providerIdent;
			foreach(DataRow row in table.Rows) {
				providerIdent=new ProviderIdent();
				providerIdent.ProviderIdentNum= PIn.Long  (row["ProviderIdentNum"].ToString());
				providerIdent.ProvNum         = PIn.Long  (row["ProvNum"].ToString());
				providerIdent.PayorID         = PIn.String(row["PayorID"].ToString());
				providerIdent.SuppIDType      = (OpenDentBusiness.ProviderSupplementalID)PIn.Int(row["SuppIDType"].ToString());
				providerIdent.IDNumber        = PIn.String(row["IDNumber"].ToString());
				retVal.Add(providerIdent);
			}
			return retVal;
		}

		///<summary>Converts a list of ProviderIdent into a DataTable.</summary>
		public static DataTable ListToTable(List<ProviderIdent> listProviderIdents,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="ProviderIdent";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("ProviderIdentNum");
			table.Columns.Add("ProvNum");
			table.Columns.Add("PayorID");
			table.Columns.Add("SuppIDType");
			table.Columns.Add("IDNumber");
			foreach(ProviderIdent providerIdent in listProviderIdents) {
				table.Rows.Add(new object[] {
					POut.Long  (providerIdent.ProviderIdentNum),
					POut.Long  (providerIdent.ProvNum),
					            providerIdent.PayorID,
					POut.Int   ((int)providerIdent.SuppIDType),
					            providerIdent.IDNumber,
				});
			}
			return table;
		}

		///<summary>Inserts one ProviderIdent into the database.  Returns the new priKey.</summary>
		public static long Insert(ProviderIdent providerIdent){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				providerIdent.ProviderIdentNum=DbHelper.GetNextOracleKey("providerident","ProviderIdentNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(providerIdent,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							providerIdent.ProviderIdentNum++;
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
				return Insert(providerIdent,false);
			}
		}

		///<summary>Inserts one ProviderIdent into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ProviderIdent providerIdent,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				providerIdent.ProviderIdentNum=ReplicationServers.GetKey("providerident","ProviderIdentNum");
			}
			string command="INSERT INTO providerident (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ProviderIdentNum,";
			}
			command+="ProvNum,PayorID,SuppIDType,IDNumber) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(providerIdent.ProviderIdentNum)+",";
			}
			command+=
				     POut.Long  (providerIdent.ProvNum)+","
				+"'"+POut.String(providerIdent.PayorID)+"',"
				+    POut.Int   ((int)providerIdent.SuppIDType)+","
				+"'"+POut.String(providerIdent.IDNumber)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				providerIdent.ProviderIdentNum=Db.NonQ(command,true);
			}
			return providerIdent.ProviderIdentNum;
		}

		///<summary>Inserts one ProviderIdent into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ProviderIdent providerIdent){
			if(DataConnection.DBtype==DatabaseType.MySql) {
				return InsertNoCache(providerIdent,false);
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					providerIdent.ProviderIdentNum=DbHelper.GetNextOracleKey("providerident","ProviderIdentNum"); //Cacheless method
				}
				return InsertNoCache(providerIdent,true);
			}
		}

		///<summary>Inserts one ProviderIdent into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ProviderIdent providerIdent,bool useExistingPK){
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO providerident (";
			if(!useExistingPK && isRandomKeys) {
				providerIdent.ProviderIdentNum=ReplicationServers.GetKeyNoCache("providerident","ProviderIdentNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ProviderIdentNum,";
			}
			command+="ProvNum,PayorID,SuppIDType,IDNumber) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(providerIdent.ProviderIdentNum)+",";
			}
			command+=
				     POut.Long  (providerIdent.ProvNum)+","
				+"'"+POut.String(providerIdent.PayorID)+"',"
				+    POut.Int   ((int)providerIdent.SuppIDType)+","
				+"'"+POut.String(providerIdent.IDNumber)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				providerIdent.ProviderIdentNum=Db.NonQ(command,true);
			}
			return providerIdent.ProviderIdentNum;
		}

		///<summary>Updates one ProviderIdent in the database.</summary>
		public static void Update(ProviderIdent providerIdent){
			string command="UPDATE providerident SET "
				+"ProvNum         =  "+POut.Long  (providerIdent.ProvNum)+", "
				+"PayorID         = '"+POut.String(providerIdent.PayorID)+"', "
				+"SuppIDType      =  "+POut.Int   ((int)providerIdent.SuppIDType)+", "
				+"IDNumber        = '"+POut.String(providerIdent.IDNumber)+"' "
				+"WHERE ProviderIdentNum = "+POut.Long(providerIdent.ProviderIdentNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ProviderIdent in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ProviderIdent providerIdent,ProviderIdent oldProviderIdent){
			string command="";
			if(providerIdent.ProvNum != oldProviderIdent.ProvNum) {
				if(command!=""){ command+=",";}
				command+="ProvNum = "+POut.Long(providerIdent.ProvNum)+"";
			}
			if(providerIdent.PayorID != oldProviderIdent.PayorID) {
				if(command!=""){ command+=",";}
				command+="PayorID = '"+POut.String(providerIdent.PayorID)+"'";
			}
			if(providerIdent.SuppIDType != oldProviderIdent.SuppIDType) {
				if(command!=""){ command+=",";}
				command+="SuppIDType = "+POut.Int   ((int)providerIdent.SuppIDType)+"";
			}
			if(providerIdent.IDNumber != oldProviderIdent.IDNumber) {
				if(command!=""){ command+=",";}
				command+="IDNumber = '"+POut.String(providerIdent.IDNumber)+"'";
			}
			if(command==""){
				return false;
			}
			command="UPDATE providerident SET "+command
				+" WHERE ProviderIdentNum = "+POut.Long(providerIdent.ProviderIdentNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ProviderIdent,ProviderIdent) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ProviderIdent providerIdent,ProviderIdent oldProviderIdent) {
			if(providerIdent.ProvNum != oldProviderIdent.ProvNum) {
				return true;
			}
			if(providerIdent.PayorID != oldProviderIdent.PayorID) {
				return true;
			}
			if(providerIdent.SuppIDType != oldProviderIdent.SuppIDType) {
				return true;
			}
			if(providerIdent.IDNumber != oldProviderIdent.IDNumber) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ProviderIdent from the database.</summary>
		public static void Delete(long providerIdentNum){
			string command="DELETE FROM providerident "
				+"WHERE ProviderIdentNum = "+POut.Long(providerIdentNum);
			Db.NonQ(command);
		}

	}
}