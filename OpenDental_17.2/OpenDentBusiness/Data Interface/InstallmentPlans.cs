using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InstallmentPlans{
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


		///<summary>Gets the installment plan for this family.  If none, returns null.</summary>
		public static InstallmentPlan GetOneForFam(long guarNum){
		  if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		    return Meth.GetObject<InstallmentPlan>(MethodBase.GetCurrentMethod(),guarNum);
		  }
		  string command="SELECT * FROM installmentplan WHERE PatNum = "+POut.Long(guarNum)+" LIMIT 1";
		  return Crud.InstallmentPlanCrud.SelectOne(command);
		}
		
		///<summary>Gets the installment plans for a SuperFamily.  If none, returns empty list.</summary>
		public static List<InstallmentPlan> GetForSuperFam(long superFamNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InstallmentPlan>>(MethodBase.GetCurrentMethod(),superFamNum);
			}
			string command="SELECT installmentplan.* FROM installmentplan "
				+"INNER JOIN patient ON installmentplan.PatNum=patient.PatNum "
				+"WHERE patient.SuperFamily="+POut.Long(superFamNum)+" "
				+"AND patient.HasSuperBilling=1 "
				+"GROUP BY installmentplan.PatNum";
			if(DataConnection.DBtype!=DatabaseType.MySql) {
				command+=",installmentplan.InstallmentPlanNum,installmentplan.DateAgreement,installmentplan.DateFirstPayment"
					+",installmentplan.MonthlyPayment,installmentplan.APR,installmentplan.Note";
			}
			return Crud.InstallmentPlanCrud.SelectMany(command);
		}

		///<summary>Gets one InstallmentPlan from the db.</summary>
		public static InstallmentPlan GetOne(long installmentPlanNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<InstallmentPlan>(MethodBase.GetCurrentMethod(),installmentPlanNum);
			}
			return Crud.InstallmentPlanCrud.SelectOne(installmentPlanNum);
		}

		///<summary></summary>
		public static long Insert(InstallmentPlan installmentPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				installmentPlan.InstallmentPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),installmentPlan);
				return installmentPlan.InstallmentPlanNum;
			}
			return Crud.InstallmentPlanCrud.Insert(installmentPlan);
		}

		///<summary></summary>
		public static void Update(InstallmentPlan installmentPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),installmentPlan);
				return;
			}
			Crud.InstallmentPlanCrud.Update(installmentPlan);
		}

		///<summary></summary>
		public static void Delete(long installmentPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),installmentPlanNum);
				return;
			}
			string command= "DELETE FROM installmentplan WHERE InstallmentPlanNum = "+POut.Long(installmentPlanNum);
			Db.NonQ(command);
		}

	}
}