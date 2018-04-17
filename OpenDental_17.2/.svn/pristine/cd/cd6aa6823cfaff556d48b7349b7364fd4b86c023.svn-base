using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DiscountPlans{
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


		///<summary></summary>
		public static List<DiscountPlan> GetAll(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DiscountPlan>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM discountplan";
			return Crud.DiscountPlanCrud.SelectMany(command);
		}

		///<summary>Gets one DiscountPlan from the db.</summary>
		public static DiscountPlan GetPlan(long discountPlanNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DiscountPlan>(MethodBase.GetCurrentMethod(),discountPlanNum);
			}
			return Crud.DiscountPlanCrud.SelectOne(discountPlanNum);
		}

		///<summary></summary>
		public static long Insert(DiscountPlan discountPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				discountPlan.DiscountPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),discountPlan);
				return discountPlan.DiscountPlanNum;
			}
			return Crud.DiscountPlanCrud.Insert(discountPlan);
		}

		///<summary></summary>
		public static void Update(DiscountPlan discountPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),discountPlan);
				return;
			}
			Crud.DiscountPlanCrud.Update(discountPlan);
		}

		///<summary>Sets DiscountPlanNum to 0 for specified PatNum.</summary>
		public static void DropForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="UPDATE patient SET DiscountPlanNum=0 WHERE PatNum="+POut.Long(patNum);
			Db.NonQ(command);
		}



	}
}