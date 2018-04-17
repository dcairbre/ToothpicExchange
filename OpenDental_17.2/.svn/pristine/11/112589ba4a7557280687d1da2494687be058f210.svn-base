using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcCodeNotes{
		///<summary>All notes for all procedurecodes.</summary>
		private static List<ProcCodeNote> list;

		public static List<ProcCodeNote> Listt {
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


		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM proccodenote";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProcCodeNote";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.ProcCodeNoteCrud.TableToList(table);
		}

		///<summary></summary>
		public static List<ProcCodeNote> GetList(long codeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcCodeNote>>(MethodBase.GetCurrentMethod(),codeNum);
			}
			string command="SELECT * FROM proccodenote WHERE CodeNum="+POut.Long(codeNum);
			return Crud.ProcCodeNoteCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(ProcCodeNote note) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				note.ProcCodeNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),note);
				return note.ProcCodeNoteNum;
			}
			return Crud.ProcCodeNoteCrud.Insert(note);
		}

		///<summary></summary>
		public static void Update(ProcCodeNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			Crud.ProcCodeNoteCrud.Update(note);
		}

		public static void Delete(long procCodeNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procCodeNoteNum);
				return;
			}
			string command="DELETE FROM proccodenote WHERE ProcCodeNoteNum = "+POut.Long(procCodeNoteNum);
			Db.NonQ(command);
		}

		///<summary>Gets the note for the given provider, if one exists.  Otherwise, gets the proccode.defaultnote.
		///Currently procStatus only supports TP or C statuses.</summary>
		public static string GetNote(long provNum,long codeNum, ProcStat procStatus, bool isGroupNote=false) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ProvNum!=provNum) {
					continue;
				}
				if(Listt[i].CodeNum!=codeNum) {
					continue;
				}
				//Skip provider specific notes if this is a group note and the procedure is not complete
				// OR if this is NOT a group note and the procedure does not have the desired status.
				if((isGroupNote && Listt[i].ProcStatus!=ProcStat.C) 
					|| (!isGroupNote && Listt[i].ProcStatus!=procStatus))
				{
					continue;
				}
				return Listt[i].Note;
			}
			//A provider specific procedure code note could not be found, use the default for the procedure code.
			if(procStatus==ProcStat.TP) {
					return ProcedureCodes.GetProcCode(codeNum).DefaultTPNote;
				}
			return ProcedureCodes.GetProcCode(codeNum).DefaultNote;
		}

		///<summary>Gets the time pattern for the given provider, if one exists.  Otherwise, gets the proccode.ProcTime.
		///Optionally pass in a copy of the procedure code cache to save time from making another deep copy of it.</summary>
		public static string GetTimePattern(long provNum,long codeNum,List<ProcedureCode> listProcedureCodes=null) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ProvNum!=provNum) {
					continue;
				}
				if(Listt[i].CodeNum!=codeNum) {
					continue;
				}
				return Listt[i].ProcTime;
			}
			return ProcedureCodes.GetProcCode(codeNum,listProcedureCodes).ProcTime;
		}



	}

	
	
	


}