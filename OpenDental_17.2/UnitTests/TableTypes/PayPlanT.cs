using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDentBusiness;

namespace UnitTests {
	public class PayPlanT {
		public static PayPlan CreatePayPlan(long patNum,double totalAmt,double payAmt,DateTime datePayStart,long provNum) {
			PayPlan payPlan=new PayPlan();
			payPlan.Guarantor=patNum;
			payPlan.PatNum=patNum;
			payPlan.PayAmt=totalAmt;
			payPlan.PayPlanDate=DateTime.Today;
			payPlan.PayAmt=totalAmt;
			PayPlans.Insert(payPlan);
			PayPlanCharge charge=new PayPlanCharge();
			charge.PayPlanNum=payPlan.PayPlanNum;
			charge.PatNum=patNum;
			charge.ChargeDate=datePayStart;
			charge.Principal=totalAmt;
			charge.ChargeType=PayPlanChargeType.Credit;
			double sumCharges=0;
			int countPayments=0;
			while(sumCharges < totalAmt) { 
				charge=new PayPlanCharge();
				charge.ChargeDate=datePayStart.AddMonths(countPayments);
				charge.PatNum=patNum;
				charge.Guarantor=patNum;
				charge.PayPlanNum=payPlan.PayPlanNum;
				charge.Principal=Math.Min(payAmt,totalAmt-sumCharges);
				charge.ProvNum=provNum;
				sumCharges+=charge.Principal;
				charge.ChargeType=PayPlanChargeType.Debit;
				PayPlanCharges.Insert(charge);
				countPayments++;
			}
			return payPlan;
		}

	}
}
