using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;

namespace OpenDentBusiness {
	public class RpProcNote {
			
		public static DataTable GetData(List<long> listProvNums,List<long> listClinicNums,DateTime dateStart,DateTime dateEnd,bool includeNoNotes,
			bool includeUnsignedNotes,ToothNumberingNomenclature toothNumberFormat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),listProvNums,listClinicNums,dateStart,dateEnd,includeNoNotes,includeUnsignedNotes,
					toothNumberFormat);
			}
			string whereNoNote="";
			string whereUnsignedNote="";
			string whereNotesClause="";
			if(includeNoNotes) {
				whereNoNote=@"
					LEFT JOIN (
						SELECT procedurelog.PatNum,procedurelog.ProcDate
						FROM procedurelog
						INNER JOIN procnote ON procnote.ProcNum=procedurelog.ProcNum
						INNER JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum
							AND procedurecode.ProcCode NOT IN ('D9986','D9987')
						WHERE procedurelog.ProcDate BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+@"
						AND (procedurelog.ProcStatus="+POut.Int((int)ProcStat.C)+" OR (procedurelog.ProcStatus="+POut.Int((int)ProcStat.EC)
						+@" AND procedurecode.ProcCode='~GRP~'))"
						+@" GROUP BY procedurelog.PatNum,procedurelog.ProcDate
					) hasNotes ON hasNotes.PatNum=procedurelog.PatNum AND hasNotes.ProcDate=procedurelog.ProcDate ";
				whereNotesClause="AND (n1.ProcNum IS NOT NULL OR hasNotes.PatNum IS NULL) ";
			}
			if(includeUnsignedNotes) {
				if(includeNoNotes) {
					whereNotesClause="AND (n1.ProcNum IS NOT NULL OR hasNotes.PatNum IS NULL OR unsignedNotes.ProcNum IS NOT NULL)";
				}
				else {
					whereNotesClause="AND (n1.ProcNum IS NOT NULL OR unsignedNotes.ProcNum IS NOT NULL)";
				}
				whereUnsignedNote=@"
					LEFT JOIN procnote unsignedNotes ON unsignedNotes.ProcNum=procedurelog.ProcNum
						AND unsignedNotes.Signature=''
						AND unsignedNotes.EntryDateTime= (SELECT MAX(n2.EntryDateTime) 
								FROM procnote n2 
								WHERE unsignedNotes.ProcNum = n2.ProcNum) ";
			}
			string command=@"SELECT procedurelog.ProcDate,CONCAT(CONCAT(patient.LName, ', '),patient.FName),
				procedurecode.ProcCode,procedurecode.Descript,procedurelog.ToothNum,procedurelog.Surf "
				+(includeNoNotes || includeUnsignedNotes?",(CASE WHEN n1.ProcNum IS NOT NULL THEN 'X' ELSE '' END) AS Incomplete ":"")
				+(includeNoNotes?",(CASE WHEN hasNotes.PatNum IS NULL THEN 'X' ELSE '' END) AS HasNoNote ":"")
				+(includeUnsignedNotes?",(CASE WHEN unsignedNotes.ProcNum IS NOT NULL THEN 'X' ELSE '' END) AS HasUnsignedNote ":"")+@" 
				FROM procedurelog
				INNER JOIN patient ON procedurelog.PatNum = patient.PatNum 
				INNER JOIN procedurecode ON procedurelog.CodeNum = procedurecode.CodeNum 
					AND procedurecode.ProcCode NOT IN ('D9986','D9987')
				"+(includeNoNotes || includeUnsignedNotes?"LEFT":"INNER")+@" JOIN procnote n1 ON procedurelog.ProcNum = n1.ProcNum 
					AND n1.Note LIKE '%""""%' "//looks for ""
				+@" AND n1.EntryDateTime= (SELECT MAX(n2.EntryDateTime) 
				FROM procnote n2 
				WHERE n1.ProcNum = n2.ProcNum) "
				+whereNoNote+" "
				+whereUnsignedNote+@"
				WHERE procedurelog.ProcDate BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+@"
				AND (procedurelog.ProcStatus="+POut.Int((int)ProcStat.C)
				+" OR (procedurelog.ProcStatus="+POut.Int((int)ProcStat.EC)+" "
				+@" AND procedurecode.ProcCode='~GRP~')) "
				+whereNotesClause+@"
				AND procedurelog.ProvNum IN ("+string.Join(",",listProvNums)+@")
				"+(PrefC.HasClinicsEnabled?"AND procedurelog.ClinicNum IN ("+string.Join(",",listClinicNums)+") ":"")+@" 
				ORDER BY ProcDate, LName";
			DataTable table=Db.GetTable(command);
			foreach(DataRow row in table.Rows) {
				row["ToothNum"]=Tooth.ToInternat(row["ToothNum"].ToString(),toothNumberFormat);
			}
			return table;
		}

	}
}
