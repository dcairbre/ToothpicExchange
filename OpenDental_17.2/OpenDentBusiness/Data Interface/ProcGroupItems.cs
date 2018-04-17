using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>In ProcGroupItems the ProcNum is a procedure in a group and GroupNum is the group the procedure is in. GroupNum is a FK to the Procedure table. There is a special type of procedure with the procedure code "~GRP~" that is used to indicate this is a group Procedure.</summary>
	public class ProcGroupItems{
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


		///<summary></summary>
		public static List<ProcGroupItem> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcGroupItem>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT procgroupitem.* FROM procgroupitem "
					+"INNER JOIN procedurelog ON procedurelog.ProcNum=procgroupitem.GroupNum AND procedurelog.PatNum="+POut.Long(patNum);
			return Crud.ProcGroupItemCrud.SelectMany(command);
		}

		///<summary>Gets all the ProcGroupItems for a Procedure Group.</summary>
		public static List<ProcGroupItem> GetForGroup(long groupNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcGroupItem>>(MethodBase.GetCurrentMethod(),groupNum);
			}
			string command="SELECT * FROM procgroupitem WHERE GroupNum = "+POut.Long(groupNum)+" ORDER BY ProcNum ASC";//Order is important for creating signature key in FormProcGroup.cs.
			return Crud.ProcGroupItemCrud.SelectMany(command);
		}

		///<summary>Adds a procedure to a group.</summary>
		public static long Insert(ProcGroupItem procGroupItem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				procGroupItem.ProcGroupItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),procGroupItem);
				return procGroupItem.ProcGroupItemNum;
			}
			return Crud.ProcGroupItemCrud.Insert(procGroupItem);
		}

		///<summary>Deletes a ProcGroupItem based on its procGroupItemNum.</summary>
		public static void Delete(long procGroupItemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procGroupItemNum);
				return;
			}
			string command= "DELETE FROM procgroupitem WHERE ProcGroupItemNum = "+POut.Long(procGroupItemNum);
			Db.NonQ(command);
		}

		///<summary>Returns a count of the number of C, EC, and EO procedures attached to a group note.  Takes the ProcNum of a group note.
		///Used when deleting group notes to determine which permission to check.</summary>
		public static int GetCountCompletedProcsForGroup(long groupNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),groupNum);
			}
			string command = "SELECT COUNT(*) FROM procgroupitem "
				+"INNER JOIN procedurelog ON procedurelog.ProcNum=procgroupitem.ProcNum "
				+"AND procedurelog.ProcStatus IN ("+POut.Int((int)ProcStat.C)+", "+POut.Int((int)ProcStat.EO)+", "+POut.Int((int)ProcStat.EC)+") "
				+"WHERE GroupNum = "+POut.Long(groupNum);
			return PIn.Int(Db.GetCount(command));
		}

		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ProcGroupItems.</summary>
		private static List<ProcGroupItem> listt;

		///<summary>A list of all ProcGroupItems.</summary>
		public static List<ProcGroupItem> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM procgroupitem ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProcGroupItem";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ProcGroupItemCrud.TableToList(table);
		}
		#endregion*/

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		

		///<summary>Gets one ProcGroupItem from the db.</summary>
		public static ProcGroupItem GetOne(long procGroupItemNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ProcGroupItem>(MethodBase.GetCurrentMethod(),procGroupItemNum);
			}
			return Crud.ProcGroupItemCrud.SelectOne(procGroupItemNum);
		}

		///<summary></summary>
		public static void Update(ProcGroupItem procGroupItem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procGroupItem);
				return;
			}
			Crud.ProcGroupItemCrud.Update(procGroupItem);
		}

		///<summary></summary>
		public static void Delete(long procGroupItemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procGroupItemNum);
				return;
			}
			string command= "DELETE FROM procgroupitem WHERE ProcGroupItemNum = "+POut.Long(procGroupItemNum);
			Db.NonQ(command);
		}
		*/



	}
}