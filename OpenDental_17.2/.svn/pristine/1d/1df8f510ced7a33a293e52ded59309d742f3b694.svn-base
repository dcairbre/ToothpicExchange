using System;
using System.ComponentModel;

namespace OpenDentBusiness {
	///<summary>Any row in this table will show up in the main menu of Open Dental to get the attention of the user.
	///The user will be able to click on the alert and take an action.  The actions available to the user are also determined in this row.</summary>
	[Serializable()]
	[CrudTable(IsSynchable=true)]
	public class AlertItem:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AlertItemNum;
		///<summary>FK to clinic.ClinicNum. Can be 0.</summary>
		public long ClinicNum;
		///<summary>What is displayed in the menu item.</summary>
		public String Description;
		///<summary>Enum:AlertType Identifies what type of alert this row is.</summary>
		public AlertType Type;
		///<summary>Enum:SeverityType The severity will help determine what color this alert should be in the main menu.</summary>
		public SeverityType Severity;
		///<summary>Enum:ActionType Bitwise flag that represents what actions are available for this alert.</summary>
		public ActionType Actions;
		///<summary>The form to open when the user clicks "Open Form".</summary>
		public FormType FormToOpen;
		///<summary>A FK to a table associated with the AlertType.  0 indicates not in use.</summary>
		public long FKey;

		public AlertItem() {
			
		}

		///<summary></summary>
		public AlertItem Copy() {
			return (AlertItem)this.MemberwiseClone();
		}

		public override bool Equals(object obj) {
			AlertItem alert=obj as AlertItem;
			if(alert==null) {
				return false;
			}
			return this.AlertItemNum==alert.AlertItemNum
				&& this.ClinicNum==alert.ClinicNum
				&& this.Description==alert.Description
				&& this.Type==alert.Type
				&& this.Severity==alert.Severity
				&& this.Actions==alert.Actions;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}

	///<summary>Enum representing different alert types.</summary>
	public enum AlertType {
		///<summary>Generic. Informational, has no action associated with it</summary>
		Generic,
		///<summary>Opens the Online Payments Window when clicked</summary>
		[Description("Online Payments Pending")]
		OnlinePaymentsPending,
		///<summary>Only used by Open Dental HQ. The server monitoring incoming voicemails is not working.</summary>
		[Description("Voice Mail Monitor Issues")]
		[Alert(IsODHQ=true)]
		VoiceMailMonitor,
		///<summary>Opens the Radiology Order List window when clicked.</summary>
		[Description("Radiology Orders")]
		RadiologyProcedures,
	}

	///<summary>Represents the urgency of the alert.  Also determines the color for the menu item in the main menu.</summary>
	public enum SeverityType {
		///<summary>0 - White</summary>
		Normal,
		///<summary>1 - Yellow</summary>
		Low,
		///<summary>2 - Orange</summary>
		Medium,
		///<summary>3 - Red</summary>
		High
	}

	///<summary>The possible actions that can be taken on this alert.  Multiple actions can be available for one alert.</summary>
	[Flags]
	public enum ActionType {
		///<summary></summary>
		None=0,
		///<summary></summary>
		MarkAsRead=1,
		///<summary></summary>
		OpenForm=2,
		///<summary></summary>
		Delete=4
	}

	///<summary>Add this.</summary>
	public enum FormType {
		///<summary>0 - No form.</summary>
		None,
		///<summary>1 - FormEServicesWebSchedRecall.</summary>
		[Description("eServices Web Sched Recall")]
		FormEServicesWebSchedRecall,
		///<summary>2 - FormPendingPayments.</summary>
		[Description("Pending Payments")]
		FormPendingPayments,
		///<summary>3 - FormRadOrderList.</summary>
		[Description("Radiology Orders")]
		FormRadOrderList,
		///<summary>4 - FormEServicesSetup.</summary>
		[Description("eServices Signup Portal")]
		FormEServicesSignupPortal,
	}

	///<summary>Alert-related attributes.</summary>
	public class AlertAttribute:Attribute {
		private bool _isODHQ=false;

		///<summary>The alert is only used at OD HQ. Defaults to false.</summary>
		public bool IsODHQ {
			get {
				return _isODHQ;
			}
			set {
				_isODHQ=value;
			}
		}
	}

}
