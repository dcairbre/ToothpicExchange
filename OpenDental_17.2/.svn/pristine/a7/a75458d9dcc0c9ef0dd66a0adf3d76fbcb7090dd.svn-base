using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class RpOutstandingIns {

		///<summary>Called from FormRpOutstandingIns. Gets outstanding insurance claims. Requires all fields. provNumList may be empty (but will return null if isAllProv is false).  listClinicNums may be empty.  dateMin and dateMax will not be used if they are set to DateTime.MinValue() (01/01/0001). If isPreauth is true only claims of type preauth will be returned.</summary>
		public static DataTable GetOutInsClaims(bool isAllProv,List<long> listProvNums,DateTime dateMin,DateTime dateMax,bool isPreauth,List<long> listClinicNums,string carrierName,List<long> listUserNums,bool useDateSentOrig){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) { 
				return Meth.GetTable(MethodBase.GetCurrentMethod(),isAllProv,listProvNums,dateMin,dateMax,isPreauth,listClinicNums,carrierName,listUserNums,useDateSentOrig);
			} 
			string command;
			command = "SELECT carrier.CarrierName,carrier.Phone,claim.ClaimType,patient.FName,patient.LName,patient.MiddleI,patient.PatNum,"
				+"claim.DateService,claim.DateSent,claim.DateSentOrig,claim.ClaimFee,claim.ClaimNum,claim.ClinicNum,"
				+"definition.ItemValue DaysSuppressed,"+DbHelper.DtimeToDate("statusHistory.DateTimeEntry")+" DateLog,"
				+"definition.DefNum DefNum, statusHistory.TrackingErrorDefNum,COALESCE(claimtracking.UserNum,0) UserNum "
				+"FROM carrier "
				+"INNER JOIN insplan ON insplan.CarrierNum=carrier.CarrierNum "
				+"INNER JOIN claim ON claim.PlanNum=insplan.PlanNum "
				+"AND claim.ClaimStatus='S' ";
			if(dateMin!=DateTime.MinValue) {
				if(useDateSentOrig) {
					command+="AND claim.DateSentOrig <= "+POut.Date(dateMin)+" ";
				}
				else { 
					command+="AND claim.DateSent <= "+POut.Date(dateMin)+" ";
				}
			}
			if(dateMax!=DateTime.MinValue) {
				if(useDateSentOrig) {
					command+="AND claim.DateSentOrig >= "+POut.Date(dateMax)+" ";
				}
				else { 
					command+="AND claim.DateSent >= "+POut.Date(dateMax)+" ";
				}
			}
			if(!isAllProv) {
				if(listProvNums.Count>0) {
					command+="AND claim.ProvTreat IN ("+String.Join(",",listProvNums)+") ";
				}
			}
			if(listClinicNums.Count>0) {
				command+="AND claim.ClinicNum IN ("+String.Join(",",listClinicNums)+") ";
			}
			if(!isPreauth) {
				command+="AND claim.ClaimType!='Preauth' ";
			}
			command+="LEFT JOIN claimtracking ON claimtracking.ClaimNum=claim.ClaimNum AND TrackingType='"+POut.String(ClaimTrackingType.ClaimUser.ToString())+"' ";
			command+="LEFT JOIN definition ON definition.DefNum=claim.CustomTracking "
				+"LEFT JOIN claimtracking statusHistory ON statusHistory.ClaimNum=claim.ClaimNum "
					+"AND statusHistory.TrackingDefNum=definition.DefNum "
					+"AND statusHistory.DateTimeEntry=(SELECT MAX(ct.DateTimeEntry) FROM claimtracking ct WHERE ct.ClaimNum=claim.ClaimNum AND ct.TrackingDefNum!=0) "
					+"AND statusHistory.TrackingType='"+POut.String(ClaimTrackingType.StatusHistory.ToString())+"' "
				+"INNER JOIN patient ON patient.PatNum=claim.PatNum "
				+"WHERE carrier.CarrierName LIKE '%"+POut.String(carrierName.Trim())+"%' ";
			if(listUserNums.Count!=0) {
				command+="AND (claimtracking.UserNum IN ("+String.Join(",",listUserNums)+") ";
				if(listUserNums.Contains(0)) {
					//Selected users includes 'Unassigned' so we want to allow claims without associated claimTracking rows to show.
					command+=" OR claimtracking.UserNum IS NULL";
				}
				command+=") ";
			}
			command+="ORDER BY carrier.CarrierName,claim.DateService,patient.LName,patient.FName,claim.ClaimType";
			object[] parameters={command};
			Plugins.HookAddCode(null,"Claims.GetOutInsClaims_beforequeryrun",parameters);//Moved entire method from Claims.cs
			command=(string)parameters[0];
			DataTable table=Db.GetTable(command);
			return table;
		}

	}
}
