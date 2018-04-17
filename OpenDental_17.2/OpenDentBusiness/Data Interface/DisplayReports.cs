using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DisplayReports{
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

		///<summary>Get all display reports for the passed-in category.  Pass in true to retrieve hidden display reports.</summary>
		public static List<DisplayReport> GetForCategory(DisplayReportCategory category, bool showHidden) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DisplayReport>>(MethodBase.GetCurrentMethod(),category,showHidden);
			}
			string command="SELECT * FROM displayreport WHERE Category="+POut.Int((int)category)+" ";
			if(!showHidden) {
				command+="AND IsHidden = 0 ";
			}
			command+="ORDER BY ItemOrder";
			return Crud.DisplayReportCrud.SelectMany(command);
		}

		///<summary>Pass in true to also retrieve hidden display reports.</summary>
		public static List<DisplayReport> GetAll(bool showHidden) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DisplayReport>>(MethodBase.GetCurrentMethod(),showHidden);
			}
			string command="SELECT * FROM displayreport ";
			if(!showHidden) {
				command+="WHERE IsHidden = 0 ";
			}
			command+="ORDER BY ItemOrder";
			return Crud.DisplayReportCrud.SelectMany(command);
		}

		///Must pass in a list of all current display reports, even hidden ones.
		public static void Sync(List<DisplayReport> listDisplayReport) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),listDisplayReport);
				return;
			}
			Crud.DisplayReportCrud.Sync(listDisplayReport,GetAll(true));
		}

		///<summary>Returns -1 if the user has no report permission, 0 if they have incomplete permissions, and 1 if they have all unhidden reports available.</summary>
		public static int GetReportState(List<GroupPermission> listGroupPerms) {
			//No remotin grole check; no call to db
			if(listGroupPerms==null) {
				return -1;//No display report perms.
			}
			List<DisplayReport> listDisplayReports=GetAll(false);
			foreach(DisplayReport report in listDisplayReports) {
				if(report.InternalName=="ODDentalSealantMeasure" || report.InternalName=="ODEligibilityFile" || report.InternalName=="ODEncounterFile") {
					continue;//We don't care about UDS reports or Arizona Primary Care reports.
				} 
				if(!listGroupPerms.Exists(x => x.FKey==report.DisplayReportNum)) {
					return 0;//Has incomplete permissions
				}
			}
			return 1;//Has all unhidden reports available
		}

	}
}