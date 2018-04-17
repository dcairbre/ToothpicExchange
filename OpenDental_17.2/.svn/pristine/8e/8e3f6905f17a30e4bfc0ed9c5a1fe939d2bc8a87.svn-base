using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDentBusiness;

namespace UnitTests {
	public class PaymentT {
		public static void MakePayment(long patNum,double payAmt,DateTime payDate,long payPlanNum=0) {
			Payment payment=new Payment();
			payment.PatNum=patNum;
			payment.PayDate=payDate;
			payment.PayAmt=payAmt;
			Payments.Insert(payment);
			PaySplit split=new PaySplit();
			split.PayNum=payment.PayNum;
			split.PatNum=payment.PatNum;
			split.DatePay=payDate;
			split.PayPlanNum=payPlanNum;
			split.SplitAmt=payAmt;
			PaySplits.Insert(split);
		}
	}
}
