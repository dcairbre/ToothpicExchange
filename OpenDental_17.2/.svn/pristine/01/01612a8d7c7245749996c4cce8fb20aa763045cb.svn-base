using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenDentBusiness {
	public class TreatmentPlanModules {

		///<summary>Gets a good chunk of the data used in the TP Module.</summary>
		public static TPModuleData GetModuleData(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {//Remoting role check here to reduce round-trips to the server.
				return Meth.GetObject<TPModuleData>(MethodBase.GetCurrentMethod(),patNum);
			}
			TPModuleData tpData=new TPModuleData();
			tpData.Fam=Patients.GetFamily(patNum);
			tpData.Pat=tpData.Fam.GetPatient(patNum);
			tpData.SubList=InsSubs.RefreshForFam(tpData.Fam);
			tpData.InsPlanList=InsPlans.RefreshForSubList(tpData.SubList);
			tpData.PatPlanList=PatPlans.Refresh(patNum);
			tpData.BenefitList=Benefits.Refresh(tpData.PatPlanList,tpData.SubList);
			tpData.ClaimList=Claims.Refresh(tpData.Pat.PatNum);
			tpData.HistList=ClaimProcs.GetHistList(tpData.Pat.PatNum,tpData.BenefitList,tpData.PatPlanList,tpData.InsPlanList,DateTime.Today,
				tpData.SubList);
			return tpData;
		}

		///<summary>Gets most of the data needed to load the active treatment plan.</summary>
		///<param name="doFillHistList">If false, then LoadActiveTPData.HistList will be null.</param>
		public static LoadActiveTPData GetLoadActiveTpData(long patNum,long treatPlanNum,List<Benefit> listBenefits,List<PatPlan> listPatPlans,
			List<InsPlan> listInsPlans,DateTime dateTimeTP,List<InsSub> listInsSubs,bool doFillHistList) 
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {//Remoting role check here to reduce round-trips to the server.
				return Meth.GetObject<LoadActiveTPData>(MethodBase.GetCurrentMethod(),patNum,treatPlanNum,listBenefits,listPatPlans,listInsPlans,dateTimeTP,
					listInsSubs,doFillHistList);
			}
			LoadActiveTPData data=new LoadActiveTPData();
			data.ListTreatPlanAttaches=TreatPlanAttaches.GetAllForTreatPlan(treatPlanNum);
			data.listProcForTP=Procedures.GetListTPandTPi(Procedures.GetManyProc(data.ListTreatPlanAttaches.Select(x=>x.ProcNum).ToList(),false),
				data.ListTreatPlanAttaches).ToList();
			//One thing to watch out for here is that we must be absolutely sure to include all claimprocs for the procedures listed,
			//regardless of status.  Needed for Procedures.ComputeEstimates.  This should be fine.
			data.ClaimProcList=ClaimProcs.RefreshForTP(patNum);
			if(doFillHistList) {
				data.HistList=ClaimProcs.GetHistList(patNum,listBenefits,listPatPlans,listInsPlans,-1,dateTimeTP,listInsSubs);
			}
			return data;
		}
	}

	[Serializable]
	public class TPModuleData {
		public Family Fam;
		public Patient Pat;
		public List<InsSub> SubList;
		public List<InsPlan> InsPlanList;
		public List<PatPlan> PatPlanList;
		public List<Benefit> BenefitList;
		public List<Claim> ClaimList;
		public List<ClaimProcHist> HistList;
	}

	[Serializable]
	public class LoadActiveTPData {
		public List<TreatPlanAttach> ListTreatPlanAttaches;
		public List<Procedure> listProcForTP;
		public List<ClaimProc> ClaimProcList;
		public List<ClaimProcHist> HistList;
	}
}
