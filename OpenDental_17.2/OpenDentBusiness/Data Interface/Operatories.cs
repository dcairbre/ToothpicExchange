using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using OpenDentBusiness.Crud;
using CodeBase;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Operatories {
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

		#region CachePattern
		///<summary>Refresh all operatories.  IsInHQView is true in the query if the Operatory is apart of an HQ Appointment View.
		///IsInHQView is handled in the FillCache() method.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command= @"
				SELECT operatory.*, CASE WHEN apptviewop.OpNum IS NULL THEN 0 ELSE 1 END IsInHQView
				FROM operatory
				LEFT JOIN (
					SELECT apptviewitem.OpNum
					FROM apptviewitem
					INNER JOIN apptview ON apptview.ApptViewNum = apptviewitem.ApptViewNum
						AND apptview.ClinicNum = 0
					GROUP BY apptviewitem.OpNum
				)apptviewop ON operatory.OperatoryNum = apptviewop.OpNum
				ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Operatory";
			FillCache(table);
			return table;
		}

		///<summary>Will set the IsInHQView flag if such a column is present within the table passed in.</summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=new List<Operatory>();
			List<Operatory> listOpsLong=Crud.OperatoryCrud.TableToList(table);
			for(int i=0;i<listOpsLong.Count;i++) {
				//The IsInHQView flag is not important enough to cause filling the cache to fail.
				try {
					listOpsLong[i].IsInHQView=PIn.Bool(table.Rows[i]["IsInHQView"].ToString());
				}
				catch(Exception e) {
					e.DoNothing();
				}
				if(!listOpsLong[i].IsHidden) {
					listOpsShort.Add(listOpsLong[i]);
				}
			}
			OperatoryC.Listt=listOpsLong;
			OperatoryC.ListShort=listOpsShort;
		}
		#endregion

		#region Sync Pattern

		///<summary>Inserts, updates, or deletes database rows to match supplied list.</summary>
		public static void Sync(List<Operatory> listNew,List<Operatory> listOld) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listNew,listOld);//never pass DB list through the web service
				return;
			}
			Crud.OperatoryCrud.Sync(listNew,listOld);
		}

		#endregion

		///<summary></summary>
		public static long Insert(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				operatory.OperatoryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),operatory);
				return operatory.OperatoryNum;
			}
			return Crud.OperatoryCrud.Insert(operatory);
		}

		///<summary></summary>
		public static void Update(Operatory operatory) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),operatory);
				return;
			}
			Crud.OperatoryCrud.Update(operatory);
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

		public static List<Operatory> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Operatory>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM operatory WHERE DateTStamp > "+POut.DateT(changedSince);
			return Crud.OperatoryCrud.SelectMany(command);
		}

		public static string GetAbbrev(long operatoryNum,List<Operatory> listOpsLong=null) {
			//No need to check RemotingRole; no call to db.
			if(listOpsLong==null) {
				listOpsLong=OperatoryC.GetListt();
			}
			for(int i=0;i<listOpsLong.Count;i++) {
				if(listOpsLong[i].OperatoryNum==operatoryNum) {
					return listOpsLong[i].Abbrev;
				}
			}
			return "";
		}

		public static string GetOpName(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsLong=OperatoryC.GetListt();
			for(int i=0;i<listOpsLong.Count;i++) {
				if(listOpsLong[i].OperatoryNum==operatoryNum) {
					return listOpsLong[i].OpName;
				}
			}
			return "";
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(long opNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			for(int i=0;i<listOpsShort.Count;i++) {
				if(listOpsShort[i].OperatoryNum==opNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets operatory from the cache.  Optionally pass in a list of operatories if you don't want to get a deep copy of the cache.</summary>
		public static Operatory GetOperatory(long operatoryNum,List<Operatory> listOperatories=null) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsLong=listOperatories;
			if(listOperatories==null) {
				listOpsLong=OperatoryC.GetListt();
			}
			for(int i=0;i<listOpsLong.Count;i++) {
				if(listOpsLong[i].OperatoryNum==operatoryNum) {
					return listOpsLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Get all non-hidden operatories for the clinic passed in.</summary>
		public static List<Operatory> GetOpsForClinic(long clinicNum) {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listRetVal=new List<Operatory>();
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			for(int i=0;i<listOpsShort.Count;i++) {
				if(listOpsShort[i].ClinicNum==clinicNum) {
					listRetVal.Add(listOpsShort[i].Copy());
				}
			}
			return listRetVal;
		}

		public static List<Operatory> GetOpsForWebSched() {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			//Only return the ops flagged as IsWebSched.
			return listOpsShort.FindAll(x => x.IsWebSched);
		}

		public static List<Operatory> GetOpsForWebSchedNewPatAppts() {
			//No need to check RemotingRole; no call to db.
			List<Operatory> listOpsShort=OperatoryC.GetListShort();
			//Only return the ops flagged as IsNewPatAppt.
			return listOpsShort.FindAll(x => x.IsNewPatAppt);
		}

		///<summary>Gets a list of all future appointments for a given Operatory.  Ordered by dateTime</summary>
		public static bool HasFutureApts(long operatoryNum,params ApptStatus[] arrayIgnoreStatuses) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),operatoryNum,arrayIgnoreStatuses);
			}
			string command="SELECT COUNT(*) FROM appointment "
				+"WHERE Op = "+POut.Long(operatoryNum)+" ";
			if(arrayIgnoreStatuses.Length > 0) {
				command+="AND AptStatus NOT IN (";
				for(int i=0;i<arrayIgnoreStatuses.Length;i++) {
					if(i > 0) {
						command+=",";
					}
					command+=POut.Int((int)arrayIgnoreStatuses[i]);
				}
				command+=") ";
			}
			command+="AND AptDateTime > "+DbHelper.Now();
			return PIn.Int(Db.GetScalar(command))>0;
		}

		///<summary>Returns a list of all appointments for the given listChildOpNums.
		///Used to determine if there are any overlapping appointments for ALL time between a 'master' op appointments and the 'child' ops appointments.
		///If an appointment from one of the give child ops has a confilict with the master op, then the appointment.Tag will be true.
		///Throws exceptions.</summary>
		public static List<Appointment> MergeApptCheck(long masterOpNum,List<long> listChildOpNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Appointment>>(MethodBase.GetCurrentMethod(),masterOpNum,listChildOpNums);
			}
			if(listChildOpNums==null || listChildOpNums.Count==0) {
				return new List<Appointment>();
			}
			if(listChildOpNums.Contains(masterOpNum)) {
				throw new ApplicationException(Lans.g("Operatories","The operatory to keep cannot be within the selected list of operatories to combine."));
			}
			List<int> listApptStatus=new List<int>();
			listApptStatus.Add((int)ApptStatus.Scheduled);
			listApptStatus.Add((int)ApptStatus.Complete);
			listApptStatus.Add((int)ApptStatus.ASAP);
			listApptStatus.Add((int)ApptStatus.Broken);
			listApptStatus.Add((int)ApptStatus.PtNote);
			//12/09/2016 - Query below originally created by Allen and moddified for job by Joe.
			string command=
			"SELECT childAppointments.*, "+
				"MAX(CASE WHEN "+
				"(childAppointments.AptDateTime <= MasterAppointments.AptDateTime AND childAppointments.AptDateTime + INTERVAL (LENGTH(childAppointments.Pattern) * 5) MINUTE > MasterAppointments.AptDateTime) "+
				"OR "+
				"(MasterAppointments.AptDateTime <= childAppointments.AptDateTime AND MasterAppointments.AptDateTime + INTERVAL (LENGTH(MasterAppointments.Pattern) * 5) MINUTE > childAppointments.AptDateTime) "+
				"THEN 1 ELSE 0 END) AS HasConflict "+
				"FROM ( "+
					"SELECT * "+
					"FROM appointment "+
					"WHERE Op ="+POut.Long(masterOpNum)+" "+
					"AND aptstatus IN ("+String.Join(",",listApptStatus)+") "+
				") MasterAppointments "+
				"CROSS JOIN ( "+
					"SELECT * "+
					"FROM appointment "+
					"WHERE Op IN ("+String.Join(",",listChildOpNums)+") "+
					"AND aptstatus IN ("+String.Join(",",listApptStatus)+") "+
				") childAppointments "+
				"GROUP BY childAppointments.AptNum";
			DataTable table=Db.GetTable(command);
			List<Appointment> list=AppointmentCrud.TableToList(table);
			for(int i=0; i<table.Rows.Count; i++) {
				Appointment appt=list.First(x => x.AptNum==PIn.Long(table.Rows[i]["AptNum"].ToString()));//Safe
				bool hasConflict=PIn.Bool(table.Rows[i]["HasConflict"].ToString());
				appt.TagOD=hasConflict;
			}
			return list;
		}

		///<summary>Hides all operatories that are not the master op and moves any appointments passed in into the master op.
		///Throws exceptions</summary>
		public static void MergeOperatoriesIntoMaster(long masterOpNum,List<long> listOpNumsToMerge,List<Appointment> listApptsToMerge) {
			//No need to check RemotingRole; No db call.
			List<Operatory> listOps=OperatoryC.GetListt();
			Operatory masterOp=listOps.FirstOrDefault(x => x.OperatoryNum==masterOpNum);
			if(masterOp==null) {
				throw new ApplicationException(Lans.g("Operatories","Operatory to merge into no longer exists."));
			}
			if(listApptsToMerge.Count>0) {
				//All appts in listAppts are appts that we are going to move to new op.
				List<Appointment> listApptsNew=listApptsToMerge.Select(x => x.Copy()).ToList();//Copy object so that we do not change original object in memory.
				listApptsNew.ForEach(x => x.Op=masterOpNum);//Associate to new op selection
				Appointments.Sync(listApptsNew,listApptsToMerge,0);
			}
			List<Operatory> listOpsToMerge=listOps.Select(x=> x.Copy()).ToList();//Copy object so that we do not change original object in memory.
			listOpsToMerge.FindAll(x => x.OperatoryNum!=masterOpNum && listOpNumsToMerge.Contains(x.OperatoryNum))
				.ForEach(x => x.IsHidden=true);
			Operatories.Sync(listOpsToMerge,listOps);
			SecurityLogs.MakeLogEntry(Permissions.Setup,0
				,Lans.g("Operatories","The following operatories and all of their appointments were merged into the")
					+" "+masterOp.Abbrev+" "+Lans.g("Operatories","operatory;")+" "
					+string.Join(", ",listOpsToMerge.FindAll(x => x.OperatoryNum!=masterOpNum && listOpNumsToMerge.Contains(x.OperatoryNum)).Select(x => x.Abbrev)));
		}
	}
	


}













