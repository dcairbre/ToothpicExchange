using System;

namespace OpenDentBusiness{

	///<summary>Transworld Systems Inc (TSI) transaction log.  Logs communication between the Open Dental program and TSI.  Entries contain information
	///about accounts placed with TSI, payments or adjustments to accounts placed, or transactions to Suspend, Reinstate or Cancel accounts.</summary>
	[Serializable]
	public class TsiTransLog:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TsiTransLogNum;
		///<summary>FK to patient.PatNum for the guarantor of the account sent to TSI for collection services.  TSI refers to this as the Debtor Reference
		///or Responsible Party Account Number.</summary>
		public long PatNum;
		///<summary>FK to userod.UserNum.  The user who sent the account for placement with TSI or who suspended, reinstated or cancelled collection
		///services for an account placed with TSI or who created the payment/adjustment for an account placed with TSI.</summary>
		public long UserNum;
		///<summary>Enum:TsiTransType - Identifies the transaction message sent to TSI.  Can be a message for placing/cancelling/suspending/reinstating
		///collection services for an account or to notify TSI of a payment/writeoff/adjustment entered into OD.</summary>
		public TsiTransType TransType;
		///<summary>Timestamp at which this row was created. Auto generated on insert.  Identifies exactly when the action happened in OD to cause the
		///message to be sent to TSI.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime TransDateTime;
		///<summary>Enum:TsiDemandType - for placements, this is the type of collection activity that will start on the account being placed.</summary>
		public TsiDemandType DemandType;
		///<summary>Enum:TsiServiceCode - for placements, intensity of first letter sent to guarantor.  Will usually be 0 - Diplomatic.</summary>
		public TsiServiceCode ServiceCode;
		///<summary>Used for payments/writeoffs/adjustments, amount applied to the debt.</summary>
		public double TransAmt;
		///<summary>Total balance due on the account.  If this is a placement, this is the debt amount TSI is going to attempt to collect.  If this is a
		///payment/writeoff/adjustment, this is the new balance after the transaction amount is applied to the debt.</summary>
		public double AccountBalance;
		///<summary>Enum:TsiFKeyType - Used in conjunction with FKey to point to the item that this log row represents.</summary>
		public TsiFKeyType FKeyType;
		///<summary>Foreign key to the table defined by the corresponding FKeyType.  Currently supports paysplit.SplitNum, claimproc.ClaimProcNum,
		///adjustment.AdjNum.</summary>
		public long FKey;
		///<summary>Raw pipe-delimited message sent to TSI.</summary>
		public string RawMsgText;

		///<summary></summary>
		public TsiTransLog Copy() {
			return (TsiTransLog)this.MemberwiseClone();
		}
	}

	///<summary>Identifies the transaction type represented by this log entry.  Could be a for placing/cancelling/suspending/reinstating an account or
	///for a payment/writeoff/adjustment to an account balance.  Don't remove items or alter order of this enum, FKey requires this to be static.</summary>
	public enum TsiTransType {
		///<summary>0 - Cancel: cancel collection services for an account. Collection services can be restarted but will incur another TSI fee.</summary>
		CN,
		///<summary>1 - Credit Adjustment: negative adjustment to reduce balance. Example: a discount given or portion of the debt written off.</summary>
		CR,
		///<summary>2 - Debit Adjustment: positive adjustment to increase balance.  Offices are supposed to stop all finance charges once placed with TSI,
		///but there may be other transactions that require increasing the amount owed.</summary>
		DB,
		///<summary>3 - Paid in Full: payment entered that pays off account balance.  Closes account with TSI and stops collection activity.</summary>
		PF,
		///<summary>4 - Placement: account sent to TSI for Accelerator/Profit Recovery/Collection services.</summary>
		PL,
		///<summary>5 - Partial Payment: payment by either patient or ins payment/writeoff that pays a portion of the balance.</summary>
		PP,
		///<summary>6 - Paid in Full, Thank You: payment entered that pays off account balance.  Closes account with TSI and stops collection activity.
		///TSI will send a Thank You letter to the patient free of charge.</summary>
		PT,
		///<summary>7 - Reinstate: an account that has been suspended can be reinstated within 60 days and the collection services will resume where it
		///left off.  After 60 days the account is automatically cancelled and in order to restart collection services the office would have to initiate a
		///new placement, which will incur another TSI fee.</summary>
		RI,
		///<summary>Suspend - places collection services for the account on hold for up to 60 days.  Example: After an account is placed with TSI, the
		///customer comes into the office and agrees to a payment plan.  The account can be suspended and if the patient fails to make a payment within 60
		///days the account can be reinstated and the collection process will resume where it left off and TSI will not charge an additional fee.  After
		///60 days the account is automatically cancelled by TSI and in order to restart the collection process, the office would have to initiate a new
		///placement which starts the collection process over and will result in an additional TSI fee.</summary>
		SS
	}

	///<summary>Don't remove items or alter order of this enum, FKey requires this to be static.</summary>
	public enum TsiDemandType {
		///<summary>0 - Accelerator/Profit Recovery</summary>
		AcceleratorPr,
		///<summary>1 - Collection</summary>
		Collection
	}

	///<summary>The service code determines the intensity of the first letter.  According to a TSI rep during a conference call, Diplomatic will be what
	///most offices will want to use.  Bad check is rarely used.  Don't remove items or alter order of this enum, FKey requires this to be static.</summary>
	public enum TsiServiceCode {
		///<summary>0 - Diplomatic: most commonly used service code.</summary>
		Diplomatic,
		///<summary>1 - Intensive:  More intense first letter.</summary>
		Intensive,
		///<summary>3 - Bad Check: in a conference call with TSI one of the reps said this is rarely used.</summary>
		BadCheck
	}

	///<summary>Identifies the table to which FKey points.  Don't remove items or alter order of this enum, FKey requires this to be static.</summary>
	public enum TsiFKeyType {
		///<summary>0 - adjustment.AdjNum.  Can be a positive (Debit) or negative (Credit) adjustment to the amount owed.</summary>
		Adjustment,
		///<summary>1 - claimproc.ClaimProcNum. For ins payments and/or writeoffs entered after the account has been placed with TSI.</summary>
		Claimproc,
		///<summary>2 - paysplit.SplitNum.  Patient payment on an account placed with TSI.</summary>
		Paysplit
	}
}