using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDentBusiness {
	public class RpUnearnedIncome {

		public static DataTable GetUnearnedIncomeData(bool checkAllClin,List<long> listClinics,bool hasDateRange,DateTime date1Start,DateTime date2Start) {
			string command="";
			string whereClin="";
			if(!checkAllClin && listClinics.Count>0) {
				whereClin="AND paysplit.ClinicNum IN ("+String.Join(",",listClinics)+") ";
			}
			if(hasDateRange) {
				command="SELECT "+DbHelper.Concat("patient.LName","', '","patient.FName","' '","patient.MiddleI")+",ItemName";
				if(PrefC.HasClinicsEnabled) {
					command+=",clinic.Description";
				}
				command+=",SplitAmt FROM paysplit "
					+"INNER JOIN patient ON patient.PatNum=paysplit.PatNum "
					+"INNER JOIN definition ON definition.DefNum=paysplit.UnearnedType ";
				if(PrefC.HasClinicsEnabled) {
					command+="LEFT JOIN clinic ON clinic.ClinicNum=paysplit.ClinicNum ";
				}
				command+="WHERE paysplit.DatePay >= "+POut.Date(date1Start)+" "
					+"AND paysplit.DatePay <= "+POut.Date(date2Start)+" "
					+whereClin
					+"AND UnearnedType > 0 GROUP BY paysplit.SplitNum "
					+"ORDER BY DatePay";
				string dateRange=date1Start.ToShortDateString()+" - "+date2Start.ToShortDateString();
			}
			else {
				command="SELECT "+DbHelper.Concat("patient.LName","', '","patient.FName","' '","patient.MiddleI")+",";
				command+=DbHelper.GroupConcat("ItemName",true);
				if(PrefC.HasClinicsEnabled) {
					command+=",clinic.Description";
				}
				command+=",SUM(SplitAmt) Amount ";
				command+="FROM paysplit "
				+"INNER JOIN patient ON patient.PatNum=paysplit.PatNum "
				+"INNER JOIN definition ON definition.DefNum=paysplit.UnearnedType ";
				if(PrefC.HasClinicsEnabled) {
					command+="LEFT JOIN clinic ON clinic.ClinicNum=paysplit.ClinicNum ";
				}
				command+="WHERE UnearnedType > 0 "
					+whereClin
					+"GROUP BY paysplit.PatNum HAVING Amount != 0";//still won't work for oracle
			}
			DataTable raw=ReportsComplex.GetTable(command);
			return raw;
		}
	}
}
