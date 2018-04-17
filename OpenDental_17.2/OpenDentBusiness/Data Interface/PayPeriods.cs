using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPeriods {
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

		///<summary>A list of all payperiods.</summary>
		private static PayPeriod[] list;

		public static PayPeriod[] List {
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

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from payperiod ORDER BY DateStart";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PayPeriod";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.PayPeriodCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pp.PayPeriodNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pp);
				return pp.PayPeriodNum;
			}
			return Crud.PayPeriodCrud.Insert(pp);
		}

		///<summary></summary>
		public static void Update(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			Crud.PayPeriodCrud.Update(pp);
		}

		///<summary></summary>
		public static void Delete(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			string command= "DELETE FROM payperiod WHERE PayPeriodNum = "+POut.Long(pp.PayPeriodNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int GetForDate(DateTime date){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(date.Date >= List[i].DateStart.Date && date.Date <= List[i].DateStop.Date){
					return i;
				}
			}
			return List.Length-1;//if we can't find a match, just return the last index
		}

		///<summary></summary>
		public static bool HasPayPeriodForDate(DateTime date) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(date.Date >= List[i].DateStart.Date 
					&& date.Date <= List[i].DateStop.Date) 
				{
					return true;
				}
			}
			return false;//if we can't find a match, return false;
		}

		///<summary>Returns the most recent payperiod object or null if none were found.</summary>
		public static PayPeriod GetMostRecent() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PayPeriod>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM payperiod WHERE DateStop=(SELECT MAX(DateStop) FROM payperiod)";
			return Crud.PayPeriodCrud.SelectOne(command);
		}


		///<summary>Determines whether there is any overlap in dates between the two passed-in list of pay periods.
		///Same-date overlaps are not allowed (eg you cannot have a pay period that ends the same day as the next one starts).</summary>
		public static bool AreAnyOverlapping(List<PayPeriod> listFirst,List<PayPeriod> listSecond) {
			//no remoting role check; no call to db.
			foreach(PayPeriod payPeriodFirst in listFirst) {
				if(listSecond.Where(payPeriodSecond => payPeriodFirst.PayPeriodNum!=payPeriodSecond.PayPeriodNum
					&& ((payPeriodFirst.DateStop >= payPeriodSecond.DateStart && payPeriodFirst.DateStop <= payPeriodSecond.DateStop) //the bottom of first overlaps
					|| (payPeriodFirst.DateStart >= payPeriodSecond.DateStart && payPeriodFirst.DateStart <= payPeriodSecond.DateStop))) //the top of first overlaps
				.Count() > 0) 
				{
					return true;
				}
			}
			return false;
		}


	}

}




