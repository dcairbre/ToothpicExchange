using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace OpenDentBusiness {
	public class ProcNotes{
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


		public static long Insert(ProcNote procNote){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				procNote.ProcNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),procNote);
				return procNote.ProcNoteNum;
			}
			return Crud.ProcNoteCrud.Insert(procNote);
		}

		public static ProcNote GetProcNotesForPat(long patNum, DateTime dateStart, DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ProcNote>(MethodBase.GetCurrentMethod(),patNum,dateStart,dateEnd);
			}
			string query="SELECT procnote.* FROM procnote "
				+"INNER JOIN procedurelog ON procedurelog.ProcNum=procnote.ProcNum "
				+"WHERE procnote.PatNum="+POut.Long(patNum)+" "
				+"AND procnote.EntryDateTime BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"AND procedurelog.ProcStatus!="+POut.Int((int)ProcStat.D)+" "
				+"ORDER BY procnote.EntryDateTime DESC";
			string command=DbHelper.LimitOrderBy(query,1);
			return Crud.ProcNoteCrud.SelectOne(command);
		}

		///<summary>Returns a list of ProcNums from listProcNums where the most recent ProcNote for the proc is signed.</summary>
		public static List<long> GetIsProcNoteSigned(List<long> listProcNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),listProcNums);
			}
			if(listProcNums.Count==0) {
				return new List<long>();
			}
			string command="SELECT * FROM procnote WHERE ProcNum IN ("+string.Join(",",listProcNums.Select(x => POut.Long(x)))+")";
			List<ProcNote> listProcNotes=Crud.ProcNoteCrud.SelectMany(command);//get all ProcNotes with ProcNum in the supplied list
			if(listProcNotes.Count==0) {
				return new List<long>();
			}
			return listProcNotes
				.GroupBy(x => x.ProcNum,(x,y) => y.Aggregate((y1,y2) => y1.EntryDateTime>y2.EntryDateTime?y1:y2))//group by ProcNum, get most recent ProcNote
				.Where(x => !string.IsNullOrWhiteSpace(x.Signature))//where the most recent ProcNote is signed
				.Select(x => x.ProcNum).ToList();//return list of ProcNums
		}
		
		/*
		///<summary></summary>
		internal static bool PreviousNoteExists(int procNum){
			string command="SELECT COUNT(*) FROM procnote WHERE ProcNum="+POut.PInt(procNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0"){
				return false;
			}
			return true;
		}*/






	}



}
