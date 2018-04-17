/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CodeBase;
using DentalXChange.Dps.Pos;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.Rendering.Printing;
using OpenDental.Bridges;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDentBusiness.WebTypes.Shared.XWeb;
using PdfSharp.Pdf;

namespace OpenDental {
	///<summary></summary>
	public class FormPayment:ODForm {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textTotal;
		private IContainer components=null;
		///<summary></summary>
		public bool IsNew=false;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		//private Adjustments Adjustments=new Adjustments();
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPayType;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butDeleteAll;
		//private double[] startBal;
		//private double[] newBal;
		//private int patI;
		//private int paymentCount;
		private OpenDental.UI.Button butAdd;
		private OpenDental.ODtextBox textNote;//(not including discounts)
		//private bool NoPermission=false;
		//private PaySplit[] PaySplitPaymentList;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textPaidBy;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label label12;
		private Label labelDepositAccount;
		private ComboBox comboDepositAccount;
		private OpenDental.UI.ODGrid gridMain;
		private Panel panelXcharge;
		private ContextMenu contextMenuXcharge;
		private MenuItem menuXcharge;
		private TextBox textDepositAccount;
		private ODGrid gridBal;
		private TextBox textFamStart;
		private Label label10;
		private TextBox textFamEnd;
		private OpenDental.UI.Button butPay;
		private TextBox textDeposit;
		private Label labelDeposit;
		private TextBox textFamAfterIns;
		private CheckBox checkPayTypeNone;
		private OpenDental.UI.Button butPayConnect;
		private ContextMenu contextMenuPayConnect;
		private MenuItem menuPayConnect;
		private ComboBox comboCreditCards;
		private Label labelCreditCards;
		private CheckBox checkRecurring;
		private CheckBox checkBalanceGroupByProv;
		private UI.Button butSplitManage;
		///<summary>Set this value to a PaySplitNum if you want one of the splits highlighted when opening this form.</summary>
		public long InitialPaySplitNum;
		private Patient _patCur;
		private Family _famCur;
		private Payment _paymentCur;
		///<summary>A current list of splits showing on the left grid.</summary>
		private List<PaySplit> _listPaySplits;
		///<summary>The original splits that existed when this window was opened.  Empty for new payments.</summary>
		private List<PaySplit> _listPaySplitsOld;
		//private double _splitTotal=0;
		private long[] _arrayDepositAcctNums;
		///<summary>This table gets created and filled once at the beginning.  After that, only the last column gets carefully updated.</summary>
		private DataTable _tableBalances;
		///<summary>Program X-Charge.</summary>
		private Program _xProg;
		///<summary>The local override path or normal path for X-Charge.</summary>
		private string _xPath;
		///<summary>Stored CreditCards for _patCur.</summary>
		private List<CreditCard> _listCreditCards;
		///<summary>Set to true when X-Charge or PayConnect makes a successful transaction, except for voids.</summary>
		private bool _wasCreditCardSuccessful;
		private PayConnectService.creditCardRequest _payConnectRequest;
		private System.Drawing.Printing.PrintDocument _pd2;
		private Payment _paymentOld;
		private bool _promptSignature;
		private bool _printReceipt;
		private UI.Button butPrintReceipt;
		///<summary>Local cache of all of the clinic nums the current user has permission to access at the time the form loads.  Filled at the same time
		///as comboClinic and is used to set payment.ClinicNum when saving.</summary>
		private List<long> _listUserClinicNums;
		private bool _isCCDeclined;
		///<summary>Set to a positive amount if there is an unearned amount for the patient and they want to use it.</summary>
		public double UnearnedAmt;
		private UI.Button butPrePay;
		private CheckBox checkProcessed;
		///<summary>Fill this list with procedures prior to loading that need to have paysplits created and attached.</summary>
		public List<Procedure> ListProcs;
		private GroupBox groupXWeb;
		private UI.Button butReturn;
		private UI.Button butVoid;
		///<summary>Used to track position inside the MakeXChargeTransaction(), for troubleshooting purposes.</summary>
		private string _xChargeMilestone;
		private UI.Button butEmailReceipt;
		private List<PayPlan> _listValidPayPlans;
		private Label labelPayPlan;
		private TabControl tabControl1;
		private TabPage tabPageSplits;
		private TabPage tabPageAllocated;
		private ODGrid gridAllocated;
		private List<PaySplit> _listPaySplitAllocations;

		///<summary>The XWebResponse that created this payment. Will only be set if the payment originated from XWeb..</summary>
		private XWebResponse _xWebResponse;

		public string XchargeMilestone {
			get {
				return _xChargeMilestone;
			}
		}

		///<summary>PatCur and FamCur are not for the PatCur of the payment.  They are for the patient and family from which this window was accessed.</summary>
		public FormPayment(Patient patCur,Family famCur,Payment paymentCur) {
			InitializeComponent();// Required for Windows Form Designer support
			_patCur=patCur;
			_famCur=famCur;
			_paymentCur=paymentCur;
			Lan.F(this);
			panelXcharge.ContextMenu=contextMenuXcharge;
			butPayConnect.ContextMenu=contextMenuPayConnect;
			_paymentOld=paymentCur.Clone();
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayment));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textPaidBy = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labelDepositAccount = new System.Windows.Forms.Label();
			this.comboDepositAccount = new System.Windows.Forms.ComboBox();
			this.panelXcharge = new System.Windows.Forms.Panel();
			this.contextMenuXcharge = new System.Windows.Forms.ContextMenu();
			this.menuXcharge = new System.Windows.Forms.MenuItem();
			this.textDepositAccount = new System.Windows.Forms.TextBox();
			this.textFamStart = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textFamEnd = new System.Windows.Forms.TextBox();
			this.textDeposit = new System.Windows.Forms.TextBox();
			this.labelDeposit = new System.Windows.Forms.Label();
			this.textFamAfterIns = new System.Windows.Forms.TextBox();
			this.checkPayTypeNone = new System.Windows.Forms.CheckBox();
			this.contextMenuPayConnect = new System.Windows.Forms.ContextMenu();
			this.menuPayConnect = new System.Windows.Forms.MenuItem();
			this.comboCreditCards = new System.Windows.Forms.ComboBox();
			this.labelCreditCards = new System.Windows.Forms.Label();
			this.checkRecurring = new System.Windows.Forms.CheckBox();
			this.checkBalanceGroupByProv = new System.Windows.Forms.CheckBox();
			this.gridBal = new OpenDental.UI.ODGrid();
			this.gridMain = new OpenDental.UI.ODGrid();
			this._pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrintReceipt = new OpenDental.UI.Button();
			this.butSplitManage = new OpenDental.UI.Button();
			this.butPayConnect = new OpenDental.UI.Button();
			this.butPay = new OpenDental.UI.Button();
			this.textDateEntry = new OpenDental.ValidDate();
			this.textNote = new OpenDental.ODtextBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDeleteAll = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butPrePay = new OpenDental.UI.Button();
			this.checkProcessed = new System.Windows.Forms.CheckBox();
			this.groupXWeb = new System.Windows.Forms.GroupBox();
			this.butReturn = new OpenDental.UI.Button();
			this.butVoid = new OpenDental.UI.Button();
			this.butEmailReceipt = new OpenDental.UI.Button();
			this.labelPayPlan = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageSplits = new System.Windows.Forms.TabPage();
			this.tabPageAllocated = new System.Windows.Forms.TabPage();
			this.gridAllocated = new OpenDental.UI.ODGrid();
			this.groupXWeb.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageSplits.SuspendLayout();
			this.tabPageAllocated.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(404, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Payment Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 134);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Bank-Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(106, 110);
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.Size = new System.Drawing.Size(100, 20);
			this.textCheckNum.TabIndex = 1;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(106, 130);
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.Size = new System.Drawing.Size(100, 20);
			this.textBankBranch.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(281, 490);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(286, 14);
			this.label7.TabIndex = 18;
			this.label7.Text = "(must match total amount of payment)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTotal
			// 
			this.textTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textTotal.Location = new System.Drawing.Point(494, 464);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(67, 20);
			this.textTotal.TabIndex = 19;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.Location = new System.Drawing.Point(393, 465);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 18);
			this.label8.TabIndex = 22;
			this.label8.Text = "Total Splits";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(407, 39);
			this.listPayType.Name = "listPayType";
			this.listPayType.Size = new System.Drawing.Size(120, 95);
			this.listPayType.TabIndex = 4;
			this.listPayType.Click += new System.EventHandler(this.listPayType_Click);
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.Location = new System.Drawing.Point(102, 514);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(126, 37);
			this.label9.TabIndex = 28;
			this.label9.Text = "Deletes entire payment and all splits";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPaidBy
			// 
			this.textPaidBy.Location = new System.Drawing.Point(106, 30);
			this.textPaidBy.Name = "textPaidBy";
			this.textPaidBy.ReadOnly = true;
			this.textPaidBy.Size = new System.Drawing.Size(242, 20);
			this.textPaidBy.TabIndex = 32;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 33;
			this.label11.Text = "Paid By";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(106, 8);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(198, 21);
			this.comboClinic.TabIndex = 92;
			this.comboClinic.SelectionChangeCommitted += new System.EventHandler(this.comboClinic_SelectionChangeCommitted);
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(16, 12);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(86, 14);
			this.labelClinic.TabIndex = 91;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4, 54);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 94;
			this.label12.Text = "Entry Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDepositAccount
			// 
			this.labelDepositAccount.Location = new System.Drawing.Point(407, 138);
			this.labelDepositAccount.Name = "labelDepositAccount";
			this.labelDepositAccount.Size = new System.Drawing.Size(260, 17);
			this.labelDepositAccount.TabIndex = 114;
			this.labelDepositAccount.Text = "Pay into Account";
			this.labelDepositAccount.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboDepositAccount
			// 
			this.comboDepositAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDepositAccount.FormattingEnabled = true;
			this.comboDepositAccount.Location = new System.Drawing.Point(407, 157);
			this.comboDepositAccount.Name = "comboDepositAccount";
			this.comboDepositAccount.Size = new System.Drawing.Size(260, 21);
			this.comboDepositAccount.TabIndex = 113;
			// 
			// panelXcharge
			// 
			this.panelXcharge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelXcharge.BackgroundImage")));
			this.panelXcharge.Location = new System.Drawing.Point(694, 12);
			this.panelXcharge.Name = "panelXcharge";
			this.panelXcharge.Size = new System.Drawing.Size(59, 26);
			this.panelXcharge.TabIndex = 118;
			this.panelXcharge.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelXcharge_MouseClick);
			// 
			// contextMenuXcharge
			// 
			this.contextMenuXcharge.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuXcharge});
			// 
			// menuXcharge
			// 
			this.menuXcharge.Index = 0;
			this.menuXcharge.Text = "Settings";
			this.menuXcharge.Click += new System.EventHandler(this.menuXcharge_Click);
			// 
			// textDepositAccount
			// 
			this.textDepositAccount.Location = new System.Drawing.Point(407, 181);
			this.textDepositAccount.Name = "textDepositAccount";
			this.textDepositAccount.ReadOnly = true;
			this.textDepositAccount.Size = new System.Drawing.Size(260, 20);
			this.textDepositAccount.TabIndex = 119;
			// 
			// textFamStart
			// 
			this.textFamStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textFamStart.Location = new System.Drawing.Point(766, 462);
			this.textFamStart.Name = "textFamStart";
			this.textFamStart.ReadOnly = true;
			this.textFamStart.Size = new System.Drawing.Size(60, 20);
			this.textFamStart.TabIndex = 121;
			this.textFamStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(665, 465);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 122;
			this.label10.Text = "Family Total";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFamEnd
			// 
			this.textFamEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textFamEnd.Location = new System.Drawing.Point(886, 462);
			this.textFamEnd.Name = "textFamEnd";
			this.textFamEnd.ReadOnly = true;
			this.textFamEnd.Size = new System.Drawing.Size(60, 20);
			this.textFamEnd.TabIndex = 123;
			this.textFamEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDeposit
			// 
			this.textDeposit.Location = new System.Drawing.Point(694, 181);
			this.textDeposit.Name = "textDeposit";
			this.textDeposit.ReadOnly = true;
			this.textDeposit.Size = new System.Drawing.Size(100, 20);
			this.textDeposit.TabIndex = 125;
			// 
			// labelDeposit
			// 
			this.labelDeposit.ForeColor = System.Drawing.Color.Firebrick;
			this.labelDeposit.Location = new System.Drawing.Point(691, 162);
			this.labelDeposit.Name = "labelDeposit";
			this.labelDeposit.Size = new System.Drawing.Size(199, 16);
			this.labelDeposit.TabIndex = 126;
			this.labelDeposit.Text = "Attached to deposit";
			this.labelDeposit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textFamAfterIns
			// 
			this.textFamAfterIns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textFamAfterIns.Location = new System.Drawing.Point(826, 462);
			this.textFamAfterIns.Name = "textFamAfterIns";
			this.textFamAfterIns.ReadOnly = true;
			this.textFamAfterIns.Size = new System.Drawing.Size(60, 20);
			this.textFamAfterIns.TabIndex = 127;
			this.textFamAfterIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkPayTypeNone
			// 
			this.checkPayTypeNone.Location = new System.Drawing.Point(407, 21);
			this.checkPayTypeNone.Name = "checkPayTypeNone";
			this.checkPayTypeNone.Size = new System.Drawing.Size(204, 18);
			this.checkPayTypeNone.TabIndex = 128;
			this.checkPayTypeNone.Text = "None (Income Transfer)";
			this.checkPayTypeNone.UseVisualStyleBackColor = true;
			this.checkPayTypeNone.CheckedChanged += new System.EventHandler(this.checkPayTypeNone_CheckedChanged);
			this.checkPayTypeNone.Click += new System.EventHandler(this.checkPayTypeNone_Click);
			// 
			// contextMenuPayConnect
			// 
			this.contextMenuPayConnect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuPayConnect});
			// 
			// menuPayConnect
			// 
			this.menuPayConnect.Index = 0;
			this.menuPayConnect.Text = "Settings";
			this.menuPayConnect.Click += new System.EventHandler(this.menuPayConnect_Click);
			// 
			// comboCreditCards
			// 
			this.comboCreditCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCreditCards.Location = new System.Drawing.Point(694, 65);
			this.comboCreditCards.MaxDropDownItems = 30;
			this.comboCreditCards.Name = "comboCreditCards";
			this.comboCreditCards.Size = new System.Drawing.Size(198, 21);
			this.comboCreditCards.TabIndex = 130;
			// 
			// labelCreditCards
			// 
			this.labelCreditCards.Location = new System.Drawing.Point(694, 45);
			this.labelCreditCards.Name = "labelCreditCards";
			this.labelCreditCards.Size = new System.Drawing.Size(198, 17);
			this.labelCreditCards.TabIndex = 131;
			this.labelCreditCards.Text = "Credit Card";
			this.labelCreditCards.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkRecurring
			// 
			this.checkRecurring.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRecurring.Location = new System.Drawing.Point(694, 97);
			this.checkRecurring.Name = "checkRecurring";
			this.checkRecurring.Size = new System.Drawing.Size(196, 18);
			this.checkRecurring.TabIndex = 132;
			this.checkRecurring.Text = "Apply to Recurring Charge";
			// 
			// checkBalanceGroupByProv
			// 
			this.checkBalanceGroupByProv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBalanceGroupByProv.Location = new System.Drawing.Point(668, 236);
			this.checkBalanceGroupByProv.Name = "checkBalanceGroupByProv";
			this.checkBalanceGroupByProv.Size = new System.Drawing.Size(294, 20);
			this.checkBalanceGroupByProv.TabIndex = 133;
			this.checkBalanceGroupByProv.Text = "Group balances by provider instead of clinic, provider";
			this.checkBalanceGroupByProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBalanceGroupByProv.UseVisualStyleBackColor = true;
			this.checkBalanceGroupByProv.CheckedChanged += new System.EventHandler(this.checkBalanceGroupByProv_CheckedChanged);
			// 
			// gridBal
			// 
			this.gridBal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridBal.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridBal.HasAddButton = false;
			this.gridBal.HasDropDowns = false;
			this.gridBal.HasMultilineHeaders = false;
			this.gridBal.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridBal.HeaderHeight = 15;
			this.gridBal.HScrollVisible = false;
			this.gridBal.Location = new System.Drawing.Point(588, 262);
			this.gridBal.Name = "gridBal";
			this.gridBal.ScrollValue = 0;
			this.gridBal.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridBal.Size = new System.Drawing.Size(374, 193);
			this.gridBal.TabIndex = 120;
			this.gridBal.Title = "Family Balances";
			this.gridBal.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridBal.TitleHeight = 18;
			this.gridBal.TranslationName = "TablePaymentBal";
			this.gridBal.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBal_CellDoubleClick);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridMain.HasAddButton = false;
			this.gridMain.HasDropDowns = false;
			this.gridMain.HasMultilineHeaders = false;
			this.gridMain.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridMain.HeaderHeight = 15;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(2, 2);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(568, 193);
			this.gridMain.TabIndex = 116;
			this.gridMain.Title = "Payment Splits (optional)";
			this.gridMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridMain.TitleHeight = 18;
			this.gridMain.TranslationName = "TablePaySplits";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butPrintReceipt
			// 
			this.butPrintReceipt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintReceipt.Autosize = true;
			this.butPrintReceipt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintReceipt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintReceipt.CornerRadius = 4F;
			this.butPrintReceipt.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrintReceipt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintReceipt.Location = new System.Drawing.Point(383, 525);
			this.butPrintReceipt.Name = "butPrintReceipt";
			this.butPrintReceipt.Size = new System.Drawing.Size(101, 24);
			this.butPrintReceipt.TabIndex = 135;
			this.butPrintReceipt.TabStop = false;
			this.butPrintReceipt.Text = "&Print Receipt";
			this.butPrintReceipt.Visible = false;
			this.butPrintReceipt.Click += new System.EventHandler(this.butPrintReceipt_Click);
			// 
			// butSplitManage
			// 
			this.butSplitManage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSplitManage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSplitManage.Autosize = true;
			this.butSplitManage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSplitManage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSplitManage.CornerRadius = 4F;
			this.butSplitManage.Image = global::OpenDental.Properties.Resources.Add;
			this.butSplitManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSplitManage.Location = new System.Drawing.Point(113, 463);
			this.butSplitManage.Name = "butSplitManage";
			this.butSplitManage.Size = new System.Drawing.Size(101, 24);
			this.butSplitManage.TabIndex = 134;
			this.butSplitManage.Text = "Split Manager";
			this.butSplitManage.Click += new System.EventHandler(this.butSplitManage_Click);
			// 
			// butPayConnect
			// 
			this.butPayConnect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPayConnect.Autosize = false;
			this.butPayConnect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayConnect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayConnect.CornerRadius = 4F;
			this.butPayConnect.Location = new System.Drawing.Point(782, 13);
			this.butPayConnect.Name = "butPayConnect";
			this.butPayConnect.Size = new System.Drawing.Size(75, 24);
			this.butPayConnect.TabIndex = 129;
			this.butPayConnect.Text = "PayConnect";
			this.butPayConnect.Click += new System.EventHandler(this.butPayConnect_Click);
			// 
			// butPay
			// 
			this.butPay.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPay.Autosize = true;
			this.butPay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPay.CornerRadius = 4F;
			this.butPay.Image = global::OpenDental.Properties.Resources.Left;
			this.butPay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPay.Location = new System.Drawing.Point(588, 236);
			this.butPay.Name = "butPay";
			this.butPay.Size = new System.Drawing.Size(79, 24);
			this.butPay.TabIndex = 124;
			this.butPay.Text = "Pay";
			this.butPay.Click += new System.EventHandler(this.butPay_Click);
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(106, 50);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(100, 20);
			this.textDateEntry.TabIndex = 93;
			// 
			// textNote
			// 
			this.textNote.AcceptsTab = true;
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.DetectLinksEnabled = false;
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(106, 152);
			this.textNote.MaxLength = 4000;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Payment;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(290, 80);
			this.textNote.SpellCheckIsEnabled = false;
			this.textNote.TabIndex = 3;
			this.textNote.Text = "";
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(106, 90);
			this.textAmount.MaxVal = 100000000D;
			this.textAmount.MinVal = -100000000D;
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(100, 20);
			this.textAmount.TabIndex = 0;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106, 70);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100, 20);
			this.textDate.TabIndex = 4;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(887, 525);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(806, 525);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDeleteAll
			// 
			this.butDeleteAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDeleteAll.Autosize = true;
			this.butDeleteAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteAll.CornerRadius = 4F;
			this.butDeleteAll.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteAll.Location = new System.Drawing.Point(12, 525);
			this.butDeleteAll.Name = "butDeleteAll";
			this.butDeleteAll.Size = new System.Drawing.Size(84, 24);
			this.butDeleteAll.TabIndex = 7;
			this.butDeleteAll.Text = "&Delete";
			this.butDeleteAll.Click += new System.EventHandler(this.butDeleteAll_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(15, 463);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92, 24);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "&Add Split";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butPrePay
			// 
			this.butPrePay.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrePay.Autosize = true;
			this.butPrePay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrePay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrePay.CornerRadius = 4F;
			this.butPrePay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrePay.Location = new System.Drawing.Point(207, 90);
			this.butPrePay.Name = "butPrePay";
			this.butPrePay.Size = new System.Drawing.Size(61, 20);
			this.butPrePay.TabIndex = 136;
			this.butPrePay.Text = "Prepay";
			this.butPrePay.Click += new System.EventHandler(this.butPrePay_Click);
			// 
			// checkProcessed
			// 
			this.checkProcessed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProcessed.Location = new System.Drawing.Point(694, 116);
			this.checkProcessed.Name = "checkProcessed";
			this.checkProcessed.Size = new System.Drawing.Size(196, 18);
			this.checkProcessed.TabIndex = 137;
			this.checkProcessed.Text = "Mark as Processed";
			// 
			// groupXWeb
			// 
			this.groupXWeb.Controls.Add(this.butReturn);
			this.groupXWeb.Controls.Add(this.butVoid);
			this.groupXWeb.Location = new System.Drawing.Point(557, 13);
			this.groupXWeb.Name = "groupXWeb";
			this.groupXWeb.Size = new System.Drawing.Size(110, 85);
			this.groupXWeb.TabIndex = 138;
			this.groupXWeb.TabStop = false;
			this.groupXWeb.Text = "XWeb";
			// 
			// butReturn
			// 
			this.butReturn.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReturn.Autosize = false;
			this.butReturn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReturn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReturn.CornerRadius = 4F;
			this.butReturn.Location = new System.Drawing.Point(17, 20);
			this.butReturn.Name = "butReturn";
			this.butReturn.Size = new System.Drawing.Size(75, 24);
			this.butReturn.TabIndex = 140;
			this.butReturn.Text = "Return";
			this.butReturn.Click += new System.EventHandler(this.butReturn_Click);
			// 
			// butVoid
			// 
			this.butVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butVoid.Autosize = false;
			this.butVoid.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butVoid.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butVoid.CornerRadius = 4F;
			this.butVoid.Location = new System.Drawing.Point(17, 49);
			this.butVoid.Name = "butVoid";
			this.butVoid.Size = new System.Drawing.Size(75, 24);
			this.butVoid.TabIndex = 139;
			this.butVoid.Text = "Void";
			this.butVoid.Click += new System.EventHandler(this.butVoid_Click);
			// 
			// butEmailReceipt
			// 
			this.butEmailReceipt.AdjustImageLocation = new System.Drawing.Point(-3, 0);
			this.butEmailReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEmailReceipt.Autosize = true;
			this.butEmailReceipt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmailReceipt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmailReceipt.CornerRadius = 4F;
			this.butEmailReceipt.Image = global::OpenDental.Properties.Resources.email1;
			this.butEmailReceipt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEmailReceipt.Location = new System.Drawing.Point(490, 525);
			this.butEmailReceipt.Name = "butEmailReceipt";
			this.butEmailReceipt.Size = new System.Drawing.Size(110, 24);
			this.butEmailReceipt.TabIndex = 140;
			this.butEmailReceipt.Text = "&E-Mail Receipt";
			this.butEmailReceipt.Visible = false;
			this.butEmailReceipt.Click += new System.EventHandler(this.butEmailReceipt_Click);
			// 
			// labelPayPlan
			// 
			this.labelPayPlan.Location = new System.Drawing.Point(691, 135);
			this.labelPayPlan.Name = "labelPayPlan";
			this.labelPayPlan.Size = new System.Drawing.Size(231, 17);
			this.labelPayPlan.TabIndex = 141;
			this.labelPayPlan.Text = "splits attached to payment plan.";
			this.labelPayPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelPayPlan.Visible = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tabControl1.Controls.Add(this.tabPageSplits);
			this.tabControl1.Controls.Add(this.tabPageAllocated);
			this.tabControl1.Location = new System.Drawing.Point(7, 238);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(580, 223);
			this.tabControl1.TabIndex = 142;
			// 
			// tabPageSplits
			// 
			this.tabPageSplits.Controls.Add(this.gridMain);
			this.tabPageSplits.Location = new System.Drawing.Point(4, 22);
			this.tabPageSplits.Name = "tabPageSplits";
			this.tabPageSplits.Size = new System.Drawing.Size(572, 197);
			this.tabPageSplits.TabIndex = 0;
			this.tabPageSplits.Text = "Splits";
			this.tabPageSplits.UseVisualStyleBackColor = true;
			// 
			// tabPageAllocated
			// 
			this.tabPageAllocated.Controls.Add(this.gridAllocated);
			this.tabPageAllocated.Location = new System.Drawing.Point(4, 22);
			this.tabPageAllocated.Name = "tabPageAllocated";
			this.tabPageAllocated.Size = new System.Drawing.Size(572, 197);
			this.tabPageAllocated.TabIndex = 1;
			this.tabPageAllocated.Text = "Allocated";
			this.tabPageAllocated.UseVisualStyleBackColor = true;
			// 
			// gridAllocated
			// 
			this.gridAllocated.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridAllocated.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridAllocated.HasAddButton = false;
			this.gridAllocated.HasDropDowns = false;
			this.gridAllocated.HasMultilineHeaders = false;
			this.gridAllocated.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridAllocated.HeaderHeight = 15;
			this.gridAllocated.HScrollVisible = false;
			this.gridAllocated.Location = new System.Drawing.Point(2, 2);
			this.gridAllocated.Name = "gridAllocated";
			this.gridAllocated.ScrollValue = 0;
			this.gridAllocated.Size = new System.Drawing.Size(568, 193);
			this.gridAllocated.TabIndex = 117;
			this.gridAllocated.Title = "Split Allocations";
			this.gridAllocated.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridAllocated.TitleHeight = 18;
			this.gridAllocated.TranslationName = "TablePaySplitAllocations";
			// 
			// FormPayment
			// 
			this.ClientSize = new System.Drawing.Size(974, 561);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.labelPayPlan);
			this.Controls.Add(this.butEmailReceipt);
			this.Controls.Add(this.groupXWeb);
			this.Controls.Add(this.checkProcessed);
			this.Controls.Add(this.butPrePay);
			this.Controls.Add(this.butPrintReceipt);
			this.Controls.Add(this.checkBalanceGroupByProv);
			this.Controls.Add(this.butSplitManage);
			this.Controls.Add(this.checkRecurring);
			this.Controls.Add(this.labelCreditCards);
			this.Controls.Add(this.comboCreditCards);
			this.Controls.Add(this.butPayConnect);
			this.Controls.Add(this.checkPayTypeNone);
			this.Controls.Add(this.textFamAfterIns);
			this.Controls.Add(this.textDeposit);
			this.Controls.Add(this.labelDeposit);
			this.Controls.Add(this.butPay);
			this.Controls.Add(this.textFamEnd);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textFamStart);
			this.Controls.Add(this.gridBal);
			this.Controls.Add(this.textDepositAccount);
			this.Controls.Add(this.panelXcharge);
			this.Controls.Add(this.labelDepositAccount);
			this.Controls.Add(this.comboDepositAccount);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.textPaidBy);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDeleteAll);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(990, 585);
			this.Name = "FormPayment";
			this.ShowInTaskbar = false;
			this.Text = "Payment";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPayment_FormClosing);
			this.Load += new System.EventHandler(this.FormPayment_Load);
			this.groupXWeb.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageSplits.ResumeLayout(false);
			this.tabPageAllocated.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayment_Load(object sender,System.EventArgs e) {
			if(IsNew) {
				checkPayTypeNone.Enabled=true;
				if(!Security.IsAuthorized(Permissions.PaymentCreate)) {//date not checked here
					DialogResult=DialogResult.Cancel;
					return;
				}
				butDeleteAll.Enabled=false;
			}
			else {
				checkPayTypeNone.Enabled=false;
				checkRecurring.Checked=_paymentCur.IsRecurringCC;
				if(!Security.IsAuthorized(Permissions.PaymentEdit,_paymentCur.PayDate)) {
					butOK.Enabled=false;
					butDeleteAll.Enabled=false;
					butAdd.Enabled=false;
					gridMain.Enabled=false;
					butPay.Enabled=false;
					checkRecurring.Enabled=false;
					butSplitManage.Enabled=false;
					panelXcharge.Enabled=false;
					butPayConnect.Enabled=false;
					if(Security.IsAuthorized(Permissions.SplitCreatePastLockDate,true)) {
						//Since we are enabling the OK button, we need to make sure everything else is disabled (except for Add Split and Split Manager).
						butOK.Enabled=true;
						butAdd.Enabled=true;
						butSplitManage.Enabled=true;
						comboClinic.Enabled=false;
						textDate.ReadOnly=true;
						textAmount.ReadOnly=true;
						butPrePay.Enabled=false;
						textCheckNum.ReadOnly=true;
						textBankBranch.ReadOnly=true;
						textNote.ReadOnly=true;
						checkPayTypeNone.Enabled=false;
						listPayType.Enabled=false;
						comboDepositAccount.Enabled=false;
						comboCreditCards.Enabled=false;
						checkProcessed.Enabled=false;
						gridMain.Enabled=true;//To be able to edit splits past the lock date
					}
				}
			}
			if(PrefC.HasClinicsEnabled) {
				_listUserClinicNums=new List<long>();
				List<Clinic> listClinics=Clinics.GetForUserod(Security.CurUser);
				comboClinic.Items.Clear();
				comboClinic.Items.Add(Lan.g(this,"None"));
				_listUserClinicNums.Add(0);//this way both lists have the same number of items in it
				comboClinic.SelectedIndex=0;
				for(int i=0;i<listClinics.Count;i++) {
					comboClinic.Items.Add(listClinics[i].Abbr);
					_listUserClinicNums.Add(listClinics[i].ClinicNum);
					if(listClinics[i].ClinicNum==_paymentCur.ClinicNum) {
						comboClinic.SelectedIndex=i+1;
					}
				}
			}
			else {//clinics not enabled
				comboClinic.Visible=false;
				labelClinic.Visible=false;
				checkBalanceGroupByProv.Visible=false;
			}
			if(_paymentCur.ProcessStatus==ProcessStat.OfficeProcessed) {
				checkProcessed.Visible=false;//This checkbox will only show if the payment originated online.
			}
			else if(_paymentCur.ProcessStatus==ProcessStat.OnlineProcessed) {
				checkProcessed.Checked=true;
			}
			_listCreditCards=CreditCards.Refresh(_patCur.PatNum);
			bool isXWebCardPresent=false;
			for(int i=0;i<_listCreditCards.Count;i++) {
				string cardNum=_listCreditCards[i].CCNumberMasked;
				if(_listCreditCards[i].IsXWeb()) {
					cardNum+=" (XWeb)";
					isXWebCardPresent=true;
				}
				comboCreditCards.Items.Add(cardNum);
			}
			_xWebResponse=XWebResponses.GetOneByPaymentNum(_paymentCur.PayNum);
			groupXWeb.Visible=false;
			if(isXWebCardPresent || _xWebResponse!=null) {
				groupXWeb.Visible=true;
			}
			if(_xWebResponse==null || _xWebResponse.XTransactionType==XWebTransactionType.CreditVoidTransaction) {
				//Can't run an XWeb void unless this payment is attached to a non-void XWeb transaction.
				butVoid.Visible=false;
				groupXWeb.Height=55;
			}
			if(!isXWebCardPresent) {
				butReturn.Visible=false;
				butVoid.Location=butReturn.Location;
				groupXWeb.Height=55;
			}
			comboCreditCards.Items.Add(Lan.g(this,"New card"));
			comboCreditCards.SelectedIndex=0;
			_tableBalances=Patients.GetPaymentStartingBalances(_patCur.Guarantor,_paymentCur.PayNum);
			//this works even if patient not in family
			textPaidBy.Text=_famCur.GetNameInFamFL(_paymentCur.PatNum);
			textDateEntry.Text=_paymentCur.DateEntry.ToShortDateString();
			textDate.Text=_paymentCur.PayDate.ToShortDateString();
			textAmount.Text=_paymentCur.PayAmt.ToString("F");
			textCheckNum.Text=_paymentCur.CheckNum;
			textBankBranch.Text=_paymentCur.BankBranch;
			for(int i=0;i<DefC.Short[(int)DefCat.PaymentTypes].Length;i++) {
				listPayType.Items.Add(DefC.Short[(int)DefCat.PaymentTypes][i].ItemName);
				if(IsNew && PrefC.GetBool(PrefName.PaymentsPromptForPayType)) {//skip auto selecting payment type if preference is enabled and payment is new
					continue;//user will be forced to selectan indexbefore closing or clicking ok
				}
				if(DefC.Short[(int)DefCat.PaymentTypes][i].DefNum==_paymentCur.PayType) {
					listPayType.SelectedIndex=i;
				}
			}
			if(_paymentCur.PayType==0) {
				checkPayTypeNone.Checked=true;
			}
			textNote.Text=_paymentCur.PayNote;
			if(_paymentCur.DepositNum==0) {
				labelDeposit.Visible=false;
				textDeposit.Visible=false;
			}
			else {
				textDeposit.Text=Deposits.GetOne(_paymentCur.DepositNum).DateDeposit.ToShortDateString();
				textAmount.ReadOnly=true;
				textAmount.BackColor=SystemColors.Control;
				butPay.Enabled=false;
			}
			_listPaySplits=PaySplits.GetForPayment(_paymentCur.PayNum);//Count might be 0
			_listPaySplitsOld=new List<PaySplit>();
			for(int i=0;i<_listPaySplits.Count;i++) {
				_listPaySplitsOld.Add(_listPaySplits[i].Copy());
			}
			_listPaySplitAllocations=PaySplits.GetSplitsForPrepay(_listPaySplits);
			if((SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.Force 
        ||(SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.ForceProc) 
      {
        //Force PSM, don't show Pay button
				butPay.Visible=false;
			}
			if(IsNew && UnearnedAmt>0) {
				List<PaySplit> listPrePaySplits=PaySplits.GetPrepayForFam(_famCur);
				foreach(PaySplit prePaySplit in listPrePaySplits) {
					List<PaySplit> listSplitsForPrePay=PaySplits.GetSplitsForPrepay(new List<PaySplit>() { prePaySplit });//Find all splits for the pre-payment.
					foreach(PaySplit splitForPrePay in listSplitsForPrePay) {
						prePaySplit.SplitAmt+=splitForPrePay.SplitAmt;//Sum the amount of the pre-payment that's used. (balancing splits are negative usually)
					}
				}
				//Get all claimprocs for the proc
				List<ClaimProc> listClaimProcs=ClaimProcs.GetForProcs(ListProcs.Select(x => x.ProcNum).Distinct().ToList());
				List<Adjustment> listAdjusts=Adjustments.GetForProcs(ListProcs.Select(x => x.ProcNum).Distinct().ToList());
				foreach(Procedure proc in ListProcs) {//For each proc see how much of the Unearned we use per proc
					if(UnearnedAmt<=0) {
						break;
					}
					//Calculate the amount remaining on the procedure so we can know how much of the remaining pre-payment amount we can use.
					double patPortion=ClaimProcs.GetPatPortion(proc,listClaimProcs,listAdjusts);
					foreach(PaySplit prePaySplit in listPrePaySplits) {
						//First we need to decide how much of each pre-payment split we can use per proc.
						if(patPortion<=0) {//Proc has been paid for, go to next proc
							break;
						}
						if(prePaySplit.SplitAmt<=0) {//Split has been used, go to next split
							continue;
						}
						if(patPortion<=0) {
							break;
						}
						decimal splitTotal=0;
						if(splitTotal<(decimal)prePaySplit.SplitAmt) {//If the sum indicates there's pre-payment amount left over, let's use it.
							double amtToUse=0;
							if(prePaySplit.SplitAmt<patPortion) {
								amtToUse=prePaySplit.SplitAmt;
							}
							else {
								amtToUse=patPortion;
							}
							UnearnedAmt-=amtToUse;//Reflect the new unearned amount available for future proc use.
							PaySplit split=new PaySplit();
							split.PatNum=prePaySplit.PatNum;
							split.PayNum=_paymentCur.PayNum;
							split.PrepaymentNum=prePaySplit.SplitNum;
							split.ClinicNum=prePaySplit.ClinicNum;
							split.ProvNum=0;
							split.SplitAmt=0-amtToUse;
							split.UnearnedType=prePaySplit.UnearnedType;
							split.DatePay=DateTime.Now;
							split.PrepaymentNum=prePaySplit.SplitNum;
							_listPaySplits.Add(split);
							//Make a different paysplit attached to proc and prov they want to use it for.
							split=new PaySplit();
							split.PatNum=_patCur.PatNum;
							split.PayNum=_paymentCur.PayNum;
							split.PrepaymentNum=0;
							split.ProvNum=proc.ProvNum;
							split.ClinicNum=proc.ClinicNum;
							split.SplitAmt=amtToUse;
							split.DatePay=DateTime.Now;
							split.ProcNum=proc.ProcNum;
							_listPaySplits.Add(split);
							checkPayTypeNone.Checked=true;//This is strictly an Income Transfer payment.
							prePaySplit.SplitAmt-=amtToUse;
							patPortion-=amtToUse;
						}
					}
				}
			}
			FillMain();
			if(InitialPaySplitNum!=0) {
				for(int i=0;i<_listPaySplits.Count;i++) {
					if(InitialPaySplitNum==_listPaySplits[i].SplitNum) {
						gridMain.SetSelected(i,true);
					}
				}
			}
			if(IsNew) {
				//Fill comboDepositAccount based on autopay for listPayType.SelectedIndex
				SetComboDepositAccounts();
				textDepositAccount.Visible=false;
			}
			else {
				//put a description in the textbox.  If the user clicks on the same or another item in listPayType,
				//then the textbox will go away, and be replaced by comboDepositAccount.
				labelDepositAccount.Visible=false;
				comboDepositAccount.Visible=false;
				Transaction trans=Transactions.GetAttachedToPayment(_paymentCur.PayNum);
				if(trans==null) {
					textDepositAccount.Visible=false;
				}
				else {
					//add only the description based on PaymentCur attached to transaction
					List<JournalEntry> jeL=JournalEntries.GetForTrans(trans.TransactionNum);
					for(int i=0;i<jeL.Count;i++) {
						Account account=Accounts.GetAccount(jeL[i].AccountNum);
						//The account could be null if the AccountNum was never set correctly due to the automatic payment entry setup missing an income account from older versions.
						if(account!=null && account.AcctType==AccountType.Asset) {
							textDepositAccount.Text=jeL[i].DateDisplayed.ToShortDateString();
							if(jeL[i].DebitAmt>0) {
								textDepositAccount.Text+=" "+jeL[i].DebitAmt.ToString("c");
							}
							else {//negative
								textDepositAccount.Text+=" "+(-jeL[i].CreditAmt).ToString("c");
							}
							break;
						}
					}
				}
			}
			if(!string.IsNullOrEmpty(_paymentCur.Receipt)) {
				butPrintReceipt.Visible=true;
				butEmailReceipt.Visible=true;
			}
			_listValidPayPlans=PayPlans.GetValidPlansNoIns(_patCur.PatNum);
			//if there were no payment plans found where this patient is the guarantor, find payment plans in the family.
			if(_listValidPayPlans.Count == 0) {
				//Do not include insurance payment plans.
				_listValidPayPlans=PayPlans.GetForPats(_famCur.ListPats.Select(x => x.PatNum).ToList(),_patCur.PatNum).FindAll(x => x.PlanNum==0).ToList();
			}
			CheckUIState();
			Plugins.HookAddCode(this,"FormPayment.Load_end",_paymentCur,IsNew);
		}

		///<summary>Mimics FormClaimPayEdit.CheckUIState().</summary>
		private void CheckUIState() {
			_xProg=Programs.GetCur(ProgramName.Xcharge);
			_xPath=Programs.GetProgramPath(_xProg);
			Program progPayConnect=Programs.GetCur(ProgramName.PayConnect);
			if(_xProg==null || progPayConnect==null) {//Should not happen.
				panelXcharge.Visible=(_xProg!=null);
				butPayConnect.Visible=(progPayConnect!=null);
				return;
			}
			panelXcharge.Visible=false;
			butPayConnect.Visible=false;
			if(!progPayConnect.Enabled && !_xProg.Enabled) {//if neither enabled
				//show both so user can pick
				panelXcharge.Visible=true;
				butPayConnect.Visible=true;
				return;
			}
			//show if enabled.  User could have both enabled.
			if(progPayConnect.Enabled) {
				//if clinics are disabled, PayConnect is enabled if marked enabled
				if(!PrefC.HasClinicsEnabled) {
					butPayConnect.Visible=true;
				}
				else {//if clinics are enabled, PayConnect is enabled if the PaymentType is valid and the Username and Password are not blank
					string paymentType=ProgramProperties.GetPropVal(progPayConnect.ProgramNum,"PaymentType",_paymentCur.ClinicNum);
					if(!string.IsNullOrEmpty(ProgramProperties.GetPropVal(progPayConnect.ProgramNum,"Username",_paymentCur.ClinicNum))
						&& !string.IsNullOrEmpty(ProgramProperties.GetPropVal(progPayConnect.ProgramNum,"Password",_paymentCur.ClinicNum))
						&& DefC.Short[(int)DefCat.PaymentTypes].Any(x => x.DefNum.ToString()==paymentType))
					{
						butPayConnect.Visible=true;
					}
				}
			}
			//show if enabled.  User could have both enabled.
			if(_xProg.Enabled) {
				//if clinics are disabled, X-Charge is enabled if marked enabled
				if(!PrefC.HasClinicsEnabled) {
					panelXcharge.Visible=true;
				}
				else {//if clinics are enabled, X-Charge is enabled if the PaymentType is valid and the Username and Password are not blank
					string paymentType=ProgramProperties.GetPropVal(_xProg.ProgramNum,"PaymentType",_paymentCur.ClinicNum);
					if(!string.IsNullOrEmpty(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Username",_paymentCur.ClinicNum))
						&& !string.IsNullOrEmpty(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Password",_paymentCur.ClinicNum))
						&& DefC.Short[(int)DefCat.PaymentTypes].Any(x => x.DefNum.ToString()==paymentType))
					{
						panelXcharge.Visible=true;
					}
				}
			}
			if(panelXcharge.Visible==false && butPayConnect.Visible==false) {
				//This is an office with clinics and one of the payment processing bridges is enabled but this particular clinic doesn't have one set up.
				if(_xProg.Enabled) {
					panelXcharge.Visible=true;
				}
				if(progPayConnect.Enabled) {
					butPayConnect.Visible=true;
				}
			}
		}

		///<summary>This does not make any calls to db (except one tiny one).  Simply refreshes screen for SplitList.</summary>
		private void FillMain() {
			UpdateLabelPayPlan();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaySplits","Proc Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Prov"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Clinic"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Patient"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Procedure"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Amount"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplits","Unearned"),50);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			double splitTotal=0;
			Procedure proc=null;
			string procDesc;
			for(int i=0;i<_listPaySplits.Count;i++) {
				row=new ODGridRow();
				if(_listPaySplits[i].ProcNum==0) {
					row.Cells.Add("");
				}
				else {
					proc=Procedures.GetOneProc(_listPaySplits[i].ProcNum,false);
					row.Cells.Add(proc.ProcDate.ToShortDateString());
				}
				row.Cells.Add(Providers.GetAbbr(_listPaySplits[i].ProvNum));
				row.Cells.Add(Clinics.GetAbbr(_listPaySplits[i].ClinicNum));
				row.Cells.Add(_famCur.GetNameInFamFL(_listPaySplits[i].PatNum));
				if(_listPaySplits[i].ProcNum>0) {
					procDesc=Procedures.GetDescription(proc);
					row.Cells.Add(procDesc);
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(_listPaySplits[i].SplitAmt.ToString("F"));
				row.Cells.Add(DefC.GetName(DefCat.PaySplitUnearnedType,_listPaySplits[i].UnearnedType));//handles 0 just fine
				splitTotal+=_listPaySplits[i].SplitAmt;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			textTotal.Text=splitTotal.ToString("F");
			FillGridBal();
			FillGridAllocated();
		}

		///<summary></summary>
		private void FillGridBal() {
			//can't do this: SplitList=PaySplits.GetForPayment(PaymentCur.PayNum);//Count might be 0
			//too slow: tableBalances=Patients.GetPaymentStartingBalances(PatCur.Guarantor,PaymentCur.PayNum,checkBalanceGroupByProv.Checked);
			double famstart=0;
			for(int i=0;i<_tableBalances.Rows.Count;i++) {
				famstart+=PIn.Double(_tableBalances.Rows[i]["StartBal"].ToString());
			}
			textFamStart.Text=famstart.ToString("N");
			double famafterins=0;
			for(int i=0;i<_tableBalances.Rows.Count;i++) {
				famafterins+=PIn.Double(_tableBalances.Rows[i]["AfterIns"].ToString());
			}
			if(!PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
				textFamAfterIns.Text=famafterins.ToString("N");
			}
			//compute ending balances-----------------------------------------------------------------------------
			for(int i=0;i<_tableBalances.Rows.Count;i++) {
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
					_tableBalances.Rows[i]["EndBal"]=_tableBalances.Rows[i]["StartBal"].ToString();
				}
				else {
					_tableBalances.Rows[i]["EndBal"]=_tableBalances.Rows[i]["AfterIns"].ToString();
				}
			}
			double amt;
			for(int i=0;i<_listPaySplits.Count;i++) {//loop through each current paysplit that's showing
				PaySplit paySplit=_listPaySplits[i];
				bool hasSplitApplied=false;
				for(int f=0;f<_tableBalances.Rows.Count;f++) {//loop through the balances on the right
					if(_tableBalances.Rows[f]["PatNum"].ToString()!=_listPaySplits[i].PatNum.ToString()) {
						continue;
					}
					if(_tableBalances.Rows[f]["ProvNum"].ToString()!=_listPaySplits[i].ProvNum.ToString()) {
						continue;
					}
					if(checkBalanceGroupByProv.Checked) {
						//more inclusive.  Multiple clinics from left will be included as long as the prov matches.
					}
					else{//box not checked, so filter by clinic
						if(_tableBalances.Rows[f]["ClinicNum"].ToString()!=_listPaySplits[i].ClinicNum.ToString()) {
							continue;
						}
					}
					//sum up the amounts from the grid at the left which we want to apply to the grid on the right.
					amt=PIn.Double(_tableBalances.Rows[f]["EndBal"].ToString())-_listPaySplits[i].SplitAmt;
					//this is summing over multiple i and f loops.  NOT elegantly.
					_tableBalances.Rows[f]["EndBal"]=amt.ToString("N");
					hasSplitApplied=true;
				}
				if(hasSplitApplied) {
					continue;
				}
				Patient patInFam=_famCur.ListPats.FirstOrDefault(x => x.PatNum==paySplit.PatNum);//Paysplits can point to patients outside the family.
				if(patInFam!=null) {//No matching row was found and the pay split is for a family member.  Create a row and populate it.
					DataRow rowUnallocated=rowUnallocated=_tableBalances.NewRow();
					_tableBalances.Rows.Add(rowUnallocated);
					rowUnallocated["ProvNum"]=paySplit.ProvNum;
					rowUnallocated["ClinicNum"]=paySplit.ClinicNum;
					rowUnallocated["PatNum"]=paySplit.PatNum;
					rowUnallocated["Preferred"]=patInFam.Preferred;
					rowUnallocated["FName"]=patInFam.FName;
					rowUnallocated["StartBal"]=0;
					rowUnallocated["AfterIns"]=0;
					rowUnallocated["EndBal"]=-paySplit.SplitAmt;
				}
			}
			double famend=0;
			for(int i=0;i<_tableBalances.Rows.Count;i++) {
				famend+=PIn.Double(_tableBalances.Rows[i]["EndBal"].ToString());
			}
			textFamEnd.Text=famend.ToString("N");
			//fill grid--------------------------------------------------------------------------------------------
			gridBal.BeginUpdate();
			gridBal.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaymentBal","Prov"),60);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Clinic"),60);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Patient"),62);
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","Start"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
				col=new ODGridColumn("",60);
			}
			else {
				col=new ODGridColumn(Lan.g("TablePaymentBal","After Ins"),60,HorizontalAlignment.Right);
			}
			gridBal.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaymentBal","End"),60,HorizontalAlignment.Right);
			gridBal.Columns.Add(col);
			gridBal.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_tableBalances.Rows.Count;i++) {
				double startBal=PIn.Double(_tableBalances.Rows[i]["StartBal"].ToString());
				double afterInsAmt=PIn.Double(_tableBalances.Rows[i]["AfterIns"].ToString());
				double endBal=PIn.Double(_tableBalances.Rows[i]["EndBal"].ToString());
				if(startBal==0 && afterInsAmt==0 && endBal==0) {
					continue;//This can only happen if a row was added manually above and then the user changed the split amount corresponding to the row.
				}
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(PIn.Long(_tableBalances.Rows[i]["ProvNum"].ToString())));
				if(checkBalanceGroupByProv.Checked) {
					row.Cells.Add("");//show blank.  Value in datatable will be a random clinic.
				}
				else{
					row.Cells.Add(Clinics.GetAbbr(PIn.Long(_tableBalances.Rows[i]["ClinicNum"].ToString())));
				}
				if(_tableBalances.Rows[i]["Preferred"].ToString()=="") {
					row.Cells.Add(_tableBalances.Rows[i]["FName"].ToString());
				}
				else {
					row.Cells.Add("'"+_tableBalances.Rows[i]["Preferred"].ToString()+"'");
				}
				row.Cells.Add(startBal.ToString("N"));
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(afterInsAmt.ToString("N"));
				}
				row.Cells.Add(endBal.ToString("N"));
				//row.ColorBackG=SystemColors.Control;//Color.FromArgb(240,240,240);
				gridBal.Rows.Add(row);
			}
			gridBal.EndUpdate();
		}

		private void FillGridAllocated() {
			gridAllocated.BeginUpdate();
			gridAllocated.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Date"),80);
			gridAllocated.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Prov"),60);
			gridAllocated.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Clinic"),80);
			gridAllocated.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Patient"),140);
			gridAllocated.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Amount"),80,HorizontalAlignment.Right);
			gridAllocated.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePaySplitAllocations","Unearned"),50);
			gridAllocated.Columns.Add(col);
			gridAllocated.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_listPaySplitAllocations.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_listPaySplitAllocations[i].DatePay.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(_listPaySplitAllocations[i].ProvNum));
				row.Cells.Add(Clinics.GetAbbr(_listPaySplitAllocations[i].ClinicNum));
				row.Cells.Add(_famCur.GetNameInFamFL(_listPaySplitAllocations[i].PatNum));
				row.Cells.Add(_listPaySplitAllocations[i].SplitAmt.ToString("F"));
				row.Cells.Add(DefC.GetName(DefCat.PaySplitUnearnedType,_listPaySplitAllocations[i].UnearnedType));//handles 0 just fine
				gridAllocated.Rows.Add(row);
			}
			gridAllocated.EndUpdate();
		}

		private void checkBalanceGroupByProv_CheckedChanged(object sender,EventArgs e) {
			if(checkBalanceGroupByProv.Checked) {
				butPay.Enabled=false;
			}
			else {
				if(Security.IsAuthorized(Permissions.PaymentEdit,_paymentCur.PayDate,true)) {
					butPay.Enabled=true;
				}
			}
			_tableBalances=Patients.GetPaymentStartingBalances(_patCur.Guarantor,_paymentCur.PayNum,checkBalanceGroupByProv.Checked);
			FillGridBal();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPaySplitEdit FormPS=new FormPaySplitEdit(_famCur);
      FormPS.ListSplitsCur=_listPaySplits;
			FormPS.PaySplitCur=_listPaySplits[e.Row];
			if(FormPS.PaySplitCur.DateEntry!=DateTime.MinValue && !Security.IsAuthorized(Permissions.PaymentEdit,FormPS.PaySplitCur.DateEntry,false)) {
				return;//Don't allow user to edit splits entered before the global lock date
			}
			FormPS.Remain=_paymentCur.PayAmt-PIn.Double(textTotal.Text)+_listPaySplits[e.Row].SplitAmt;
			FormPS.ShowDialog();
			if(FormPS.PaySplitCur==null) {//user deleted
				List<PaySplit> listAllocated=_listPaySplitAllocations.FindAll(x => x.PrepaymentNum==_listPaySplits[e.Row].SplitNum);
				listAllocated.ForEach(x => _listPaySplitAllocations.Remove(x));
				_listPaySplits.RemoveAt(e.Row);
			}
			FillMain();
		}

		private void butAdd_Click(object sender,System.EventArgs e) {
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PayNum=_paymentCur.PayNum;
			PaySplitCur.DateEntry=MiscData.GetNowDateTime();//just a nicity for the user.  Insert uses server time.
			PaySplitCur.DatePay=PIn.Date(textDate.Text);//this may be updated upon closing
			PaySplitCur.ProvNum=Patients.GetProvNum(_patCur);
			PaySplitCur.PatNum=_patCur.PatNum;
			PaySplitCur.ClinicNum=_paymentCur.ClinicNum;
			FormPaySplitEdit FormPS=new FormPaySplitEdit(_famCur);
      FormPS.ListSplitsCur=_listPaySplits;
			FormPS.PaySplitCur=PaySplitCur;
			FormPS.IsNew=true;
			FormPS.Remain=_paymentCur.PayAmt-PIn.Double(textTotal.Text);
			if(FormPS.ShowDialog()!=DialogResult.OK) {
				return;
			}
			_listPaySplits.Add(PaySplitCur);
			UpdateLabelPayPlan();
			FillMain();
		}

		private void butPay_Click(object sender,EventArgs e) {
			if(gridBal.SelectedIndices.Length==0) {
				gridBal.SetSelected(true);
			}
			_listPaySplits.Clear();
			double amt;
			PaySplit split;
			for(int i=0;i<gridBal.SelectedIndices.Length;i++) {
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
					amt=PIn.Double(_tableBalances.Rows[gridBal.SelectedIndices[i]]["StartBal"].ToString());
				}
				else {
					amt=PIn.Double(_tableBalances.Rows[gridBal.SelectedIndices[i]]["AfterIns"].ToString());
				}
				if(amt==0) {
					continue;
				}
				split=new PaySplit();
				split.PatNum=PIn.Long(_tableBalances.Rows[gridBal.SelectedIndices[i]]["PatNum"].ToString());
				split.PayNum=_paymentCur.PayNum;
				split.DatePay=_paymentCur.PayDate;//this may be updated upon closing
				split.ProvNum=PIn.Long(_tableBalances.Rows[gridBal.SelectedIndices[i]]["ProvNum"].ToString());
				split.ClinicNum=PIn.Long(_tableBalances.Rows[gridBal.SelectedIndices[i]]["ClinicNum"].ToString());
				split.SplitAmt=amt;
				split.UnearnedType=PIn.Long(_tableBalances.Rows[gridBal.SelectedIndices[i]]["UnearnedType"].ToString());				
				_listPaySplits.Add(split);
			}
			FillMain();
			textAmount.Text=textTotal.Text;
		}

		///<summary>Adds one split to _listPaySplits to work with.  Does not link the payment plan, that must be done outside this method.
		///Called when checkPayPlan click, or upon load if auto attaching to payplan, or upon OK click if no splits were created.</summary>
		private bool AddOneSplit(bool promptForPayPlan = false) {
			PaySplit paySplitCur=new PaySplit();
			paySplitCur.PatNum=_patCur.PatNum;
			paySplitCur.PayNum=_paymentCur.PayNum;
			paySplitCur.DatePay=_paymentCur.PayDate;//this may be updated upon closing
			paySplitCur.ProvNum=Patients.GetProvNum(_patCur);
			paySplitCur.ClinicNum=_paymentCur.ClinicNum;
			paySplitCur.SplitAmt=PIn.Double(textAmount.Text);
			if(promptForPayPlan && _listValidPayPlans.Count > 0) {
				FormPayPlanSelect FormPPS = new FormPayPlanSelect(_listValidPayPlans,true);
				FormPPS.ShowDialog();
				if(FormPPS.DialogResult!=DialogResult.OK) {
					return false;
				}
				paySplitCur.PayPlanNum=FormPPS.SelectedPayPlanNum;
			}
			_listPaySplits.Add(paySplitCur);
			_paymentCur.PayAmt=PIn.Double(textAmount.Text);
			return true;
		}

		///<summary>Updates the passed in paysplit with the provider and clinic that is set for the payment plan charges. 
		///PayPlanNum should already be set for the split.  Also, corrects the split.PatNum in a rare scenario.</summary>
		private void SetPaySplitProvAndClinicForPayPlan(PaySplit split) {
			if(split.PayPlanNum==0) {//PayPlanNum was not set, this should never happen.
				return;
			}
			PayPlan payPlanCur=PayPlans.GetOne(split.PayPlanNum);
			//Get the family of the guarantor on the payment plan.  This might be a different family than the current patient's family.
			Family payPlanFam=Patients.GetFamily(payPlanCur.Guarantor);
			List<PayPlanCharge> listDebitCharges=PayPlanCharges.GetChargesForPayPlanChargeType(split.PayPlanNum,PayPlanChargeType.Debit);//The payment plan doesn't save/store this information
			if(listDebitCharges.Count>0) {//All charges linked to a payplan share the same clinic and provider.  Just use the first one in the list.
				if(listDebitCharges[0].ProvNum>0) {//This should never fail.
					split.ProvNum=listDebitCharges[0].ProvNum;
				}
				if(listDebitCharges[0].ClinicNum>0) {//It is possible to not set a clinic for pay plan charges.
					split.ClinicNum=listDebitCharges[0].ClinicNum;
				}
			}
			//Always set the split's patnum to the guarantor of the first debit in the list so that it shows up in the correct account.
			split.PatNum=listDebitCharges[0].Guarantor;
		}

		private void listPayType_Click(object sender,EventArgs e) {
			textDepositAccount.Visible=false;
			SetComboDepositAccounts();
		}

		///<summary>Called from all 3 places where listPayType gets changed.</summary>
		private void SetComboDepositAccounts() {
			if(listPayType.SelectedIndex==-1) {
				if(IsNew && (PayClinicSetting)PrefC.GetInt(PrefName.PaymentClinicSetting)==PayClinicSetting.PatientDefaultClinic) {
					labelDepositAccount.Visible=false;
					comboDepositAccount.Visible=false;
				}
				return;
			}
			AccountingAutoPay autoPay=AccountingAutoPays.GetForPayType(
				DefC.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum);
			if(autoPay==null) {
				labelDepositAccount.Visible=false;
				comboDepositAccount.Visible=false;
			}
			else {
				labelDepositAccount.Visible=true;
				comboDepositAccount.Visible=true;
				_arrayDepositAcctNums=AccountingAutoPays.GetPickListAccounts(autoPay);
				comboDepositAccount.Items.Clear();
				for(int i=0;i<_arrayDepositAcctNums.Length;i++) {
					comboDepositAccount.Items.Add(Accounts.GetDescript(_arrayDepositAcctNums[i]));
				}
				if(comboDepositAccount.Items.Count>0) {
					comboDepositAccount.SelectedIndex=0;
				}
			}
		}

		private void panelXcharge_MouseClick(object sender,MouseEventArgs e) {
			if(e.Button != MouseButtons.Left) {
				return;
			}
			_xChargeMilestone="";
			try {
				MakeXChargeTransaction();
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error processing transaction.\r\n\r\nPlease contact support with the following information:")
					//The rest of the message is not translated on purpose because we here at HQ need to always be able to quickly read this part.
					+"\r\nLast valid milestone reached: "+_xChargeMilestone
					+"\r\nException Message: "+ex.Message);
			}
		}

		///<summary>Launches the XCharge transaction window and then executes whatever type of transaction was selected for the current payment.
		///This is to help troubleshooting. Returns null upon failure, otherwise returns the transaction detail as a string.
		///If prepaidAmt is not zero, then will show the xcharge window with the given prepaid amount and let the user enter card # and exp.
		///A patient is not required for prepaid cards.</summary>
		public string MakeXChargeTransaction(double prepaidAmt=0) {
			_xChargeMilestone="Validation";
			CreditCard cc=null;
			List<CreditCard> creditCards=null;
			if(prepaidAmt!=0) {
				CheckUIState();//To ensure that _xProg is set and _xPath is set.  Normally this would happen when loading.  Needed for HasXCharge().
			}
			if(!HasXCharge()) {//Will show setup window if xcharge is not enabled or not completely setup yet.
				return null;
			}
			if(prepaidAmt==0) {//Validation for regular credit cards (not prepaid cards).
				if(textAmount.Text=="" || PIn.Double(textAmount.Text)==0) {
					MsgBox.Show(this,"Please enter an amount first.");
					textAmount.Focus();
					return null;
				}
				creditCards=CreditCards.Refresh(_patCur.PatNum);
				if(comboCreditCards.SelectedIndex<creditCards.Count && comboCreditCards.SelectedIndex>-1) {
					cc=creditCards[comboCreditCards.SelectedIndex];
				}
				if(cc!=null && cc.IsXWeb()) {
					MsgBox.Show(this,"Cards saved through XWeb cannot be used with the XCharge client program.");
					return null;
				}
				if(_listPaySplits.Count>0 && PIn.Double(textAmount.Text)!=PIn.Double(textTotal.Text)
					&& (_listPaySplits.Count!=1 || _listPaySplits[0].PayPlanNum==0)) //Not one paysplit attached to payplan
				{
					MsgBox.Show(this,"Split totals must equal payment amount before running a credit card transaction.");
					return null;
				}
			}
			_xChargeMilestone="XResult File";
			string resultfile=PrefC.GetRandomTempFile("txt");
			try {
				File.Delete(resultfile);//delete the old result file.
			}
			catch {
				MsgBox.Show(this,"Could not delete XResult.txt file.  It may be in use by another program, flagged as read-only, or you might not have "
					+"sufficient permissions.");
				return null;
			}
			_xChargeMilestone="Properties";
			bool needToken=false;
			bool newCard=false;
			bool hasXToken=false;
			bool notRecurring=false;
			if(prepaidAmt==0) {
				//These UI changes only need to happen for regular credit cards when the payment window is displayed.
				string xPayTypeNum=ProgramProperties.GetPropVal(_xProg.ProgramNum,"PaymentType",_paymentCur.ClinicNum);
				//still need to add functionality for accountingAutoPay
				listPayType.SelectedIndex=DefC.GetOrder(DefCat.PaymentTypes,PIn.Long(xPayTypeNum));
				SetComboDepositAccounts();
			}
			/*XCharge.exe [/TRANSACTIONTYPE:type] [/AMOUNT:amount] [/ACCOUNT:account] [/EXP:exp]
				[“/TRACK:track”] [/ZIP:zip] [/ADDRESS:address] [/RECEIPT:receipt] [/CLERK:clerk]
				[/APPROVALCODE:approval] [/AUTOPROCESS] [/AUTOCLOSE] [/STAYONTOP] [/MID]
				[/RESULTFILE:”C:\Program Files\X-Charge\LocalTran\XCResult.txt”*/
			ProcessStartInfo info=new ProcessStartInfo(_xPath);
			Patient pat=null;
			if(prepaidAmt==0) {
				pat=Patients.GetPat(_paymentCur.PatNum);
				if(pat==null) {
					MsgBox.Show(this,"Invalid patient associated to this payment.");
					return null;
				}
			}
			info.Arguments="";
			double amt=PIn.Double(textAmount.Text);
			if(prepaidAmt != 0) {
				amt=prepaidAmt;
			}
			if(amt<0) {//X-Charge always wants a positive number, even for returns.
				amt*=-1;
			}
			info.Arguments+="/AMOUNT:"+amt.ToString("F2")+" ";
			_xChargeMilestone="Get Selected Credit Card";
			FormXchargeTrans FormXT=null;
			int tranType=0;//Default to 0 "Purchase" for prepaid cards.
			string cashBack=null;
			if(prepaidAmt==0) {//All regular cards (not prepaid)
				_xChargeMilestone="Transaction Window Launch";
				//Show window to lock in the transaction type.
				FormXT=new FormXchargeTrans();
				FormXT.PrintReceipt=PIn.Bool(ProgramProperties.GetPropVal(_xProg.ProgramNum,"PrintReceipt",_paymentCur.ClinicNum));
				FormXT.PromptSignature=PIn.Bool(ProgramProperties.GetPropVal(_xProg.ProgramNum,"PromptSignature",_paymentCur.ClinicNum));
				FormXT.ShowDialog();
				if(FormXT.DialogResult!=DialogResult.OK) {
					return null;
				}
				_xChargeMilestone="Transaction Window Digest";
				_paymentCur.PaymentSource=CreditCardSource.XServer;
				_paymentCur.ProcessStatus=ProcessStat.OfficeProcessed;
				tranType=FormXT.TransactionType;
				decimal cashAmt=FormXT.CashBackAmount;
				cashBack=cashAmt.ToString("F2");
				_promptSignature=FormXT.PromptSignature;
				_printReceipt=FormXT.PrintReceipt;
			}
			_xChargeMilestone="Check Duplicate Cards";
			if(cc!=null && !string.IsNullOrEmpty(cc.XChargeToken)) {//Have CC on file with an XChargeToken
				hasXToken=true;
				if(CreditCards.GetTokenCount(cc.XChargeToken,new List<CreditCardSource> { CreditCardSource.XServer,CreditCardSource.XServerPayConnect })!=1) {
					MsgBox.Show(this,"This card shares a token with another card. Delete it from the Credit Card Manage window and re-add it.");
					return null;
				}
				/*       ***** An example of how recurring charges work***** 
				C:\Program Files\X-Charge\XCharge.exe /TRANSACTIONTYPE:Purchase /LOCKTRANTYPE
				/AMOUNT:10.00 /LOCKAMOUNT /XCACCOUNTID:XAW0JWtx5kjG8 /RECEIPT:RC001
				/LOCKRECEIPT /CLERK:Clerk /LOCKCLERK /RESULTFILE:C:\ResultFile.txt /USERID:system
				/PASSWORD:system /STAYONTOP /AUTOPROCESS /AUTOCLOSE /HIDEMAINWINDOW
				/RECURRING /SMALLWINDOW /NORESULTDIALOG
				*/
			}
			else if(cc!=null) {//Have CC on file, no XChargeToken so not a recurring charge, and might need a token.
				notRecurring=true;
				if(!PrefC.GetBool(PrefName.StoreCCnumbers)) {//Use token only if user has has pref unchecked in module setup (allow store credit card nums).
					needToken=true;//Will create a token from result file so credit card info isn't saved in our db.
				}
			}
			else {//CC is null, add card option was selected in credit card drop down, no other possibility.
				newCard=true;
			}
			_xChargeMilestone="Arguments Fill Card Info";
			info.Arguments+=GetXChargeTransactionTypeCommands(tranType,hasXToken,notRecurring,cc,cashBack);
			if(prepaidAmt!=0) {
				//Zip and address are optional fields and for prepaid cards this information is probably not provided to the user.
			}
			else if(newCard) {
				info.Arguments+="\"/ZIP:"+pat.Zip+"\" ";
				info.Arguments+="\"/ADDRESS:"+pat.Address+"\" ";
			}
			else {
				if(cc.CCExpiration!=null && cc.CCExpiration.Year>2005) {
					info.Arguments+="/EXP:"+cc.CCExpiration.ToString("MMyy")+" ";
				}
				if(!string.IsNullOrEmpty(cc.Zip)) {
					info.Arguments+="\"/ZIP:"+cc.Zip+"\" ";
				}
				else {
					info.Arguments+="\"/ZIP:"+pat.Zip+"\" ";
				}
				if(!string.IsNullOrEmpty(cc.Address)) {
					info.Arguments+="\"/ADDRESS:"+cc.Address+"\" ";
				}
				else {
					info.Arguments+="\"/ADDRESS:"+pat.Address+"\" ";
				}
				if(hasXToken) {//Special parameter for tokens.
					info.Arguments+="/RECURRING ";
				}
			}
			_xChargeMilestone="Arguments Fill X-Charge Settings";
			if(prepaidAmt==0) {
				info.Arguments+="/RECEIPT:Pat"+_paymentCur.PatNum.ToString()+" ";//aka invoice#
			}
			else {
				info.Arguments+="/RECEIPT:PREPAID ";//aka invoice#
			}
			info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" /LOCKCLERK ";
			info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
			info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(_xProg.ProgramNum,"Username",_paymentCur.ClinicNum)+" ";
			info.Arguments+="/PASSWORD:"+CodeBase.MiscUtils.Decrypt(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Password",_paymentCur.ClinicNum))+" ";
			info.Arguments+="/PARTIALAPPROVALSUPPORT:T ";
			info.Arguments+="/AUTOCLOSE ";
			info.Arguments+="/HIDEMAINWINDOW ";
			info.Arguments+="/SMALLWINDOW ";
			info.Arguments+="/GETXCACCOUNTID ";
			info.Arguments+="/NORESULTDIALOG ";
			_xChargeMilestone="X-Charge Launch";
			Cursor=Cursors.WaitCursor;
			Process process=new Process();
			process.StartInfo=info;
			process.EnableRaisingEvents=true;
			process.Start();
			process.WaitForExit();
			_xChargeMilestone="X-Charge Complete";
			Thread.Sleep(200);//Wait 2/10 second to give time for file to be created.
			Cursor=Cursors.Default;
			string resulttext="";
			string line="";
			bool showApprovedAmtNotice=false;
			bool xAdjust=false;
			bool xReturn=false;
			bool xVoid=false;
			double approvedAmt=0;
			double additionalFunds=0;
			string xChargeToken="";
			string accountMasked="";
			string expiration="";
			string signatureResult="";
			string receipt="";
			bool isDigitallySigned=false;
			bool updateCard=false;
			string newAccount="";
			long creditCardNum;
			DateTime newExpiration=new DateTime();
			_xChargeMilestone="Digest XResult";
			try {
				using(TextReader reader=new StreamReader(resultfile)) {
					line=reader.ReadLine();
					/*Example of successful transaction:
						RESULT=SUCCESS
						TYPE=Purchase
						APPROVALCODE=000064
						ACCOUNT=XXXXXXXXXXXX6781
						ACCOUNTTYPE=VISA*
						AMOUNT=1.77
						AVSRESULT=Y
						CVRESULT=M
					*/
					while(line!=null) {
						if(!line.StartsWith("RECEIPT=")) {//Don't include the receipt string in the PayNote
							if(resulttext!="") {
								resulttext+="\r\n";
							}
							resulttext+=line;
						}
						if(line.StartsWith("RESULT=")) {
							if(line!="RESULT=SUCCESS") {
								//Charge was a failure and there might be a description as to why it failed. Continue to loop through line.
								while(line!=null) {
									line=reader.ReadLine();
									if(line!=null && !line.StartsWith("RECEIPT=")) {//Don't include the receipt string in the PayNote
										resulttext+="\r\n"+line;
									}
								}
								needToken=false;//Don't update CCard due to failure
								newCard=false;//Don't insert CCard due to failure
								_isCCDeclined=true;
								break;
							}
							if(tranType==1) {
								xReturn=true;
							}
							if(tranType==6) {
								xAdjust=true;
							}
							if(tranType==7) {
								xVoid=true;
							}
							_isCCDeclined=false;
						}
						if(line.StartsWith("APPROVEDAMOUNT=")) {
							approvedAmt=PIn.Double(line.Substring(15));
							if(approvedAmt != amt) {
								showApprovedAmtNotice=true;
							}
						}
						if(line.StartsWith("XCACCOUNTID=")) {
							xChargeToken=PIn.String(line.Substring(12));
						}
						if(line.StartsWith("ACCOUNT=")) {
							accountMasked=PIn.String(line.Substring(8));
						}
						if(line.StartsWith("EXPIRATION=")) {
							expiration=PIn.String(line.Substring(11));
						}
						if(line.StartsWith("ADDITIONALFUNDSREQUIRED=")) {
							additionalFunds=PIn.Double(line.Substring(24));
						}
						if(line.StartsWith("SIGNATURE=") && line.Length>10) {
							signatureResult=PIn.String(line.Substring(10));
							//A successful digitally signed signature will say SIGNATURE=C:\Users\Folder\Where\The\Signature\Is\Stored.bmp
							if(signatureResult!="NOT SUPPORTED" && signatureResult!="FAILED") {
								isDigitallySigned=true;
							}
						}
						if(line.StartsWith("RECEIPT=")) {
							receipt=PIn.String(line.Replace("RECEIPT=","").Replace("\\n","\n"));//The receipt from X-Charge escapes the newline characters
							if(isDigitallySigned) {
								//Replace X____________________________ with 'Electronically signed'
								receipt.Split('\n').ToList().FindAll(x => x.StartsWith("X___")).ForEach(x => x="Electronically signed");
							}
							receipt=receipt.Replace("\r","").Replace("\n","\r\n");//remove any existing \r's before replacing \n's with \r\n's
						}
						if(line=="XCACCOUNTIDUPDATED=T") {//Decline minimizer updated the account information since the last time this card was charged
							updateCard=true;
						}
						if(line.StartsWith("ACCOUNT=")) {
							newAccount=line.Substring("ACCOUNT=".Length);
						}
						if(line.StartsWith("EXPIRATION=")) {
							string expStr=line.Substring("EXPIRATION=".Length);//Expiration should be MMYY
							newExpiration=new DateTime(PIn.Int("20"+expStr.Substring(2)),PIn.Int(expStr.Substring(0,2)),1);//First day of the month
						}
						line=reader.ReadLine();
					}
					if(needToken && !string.IsNullOrEmpty(xChargeToken) && prepaidAmt==0) {//never save token for prepaid cards
						_xChargeMilestone="Update Token";
						DateTime expDate=new DateTime(PIn.Int("20"+expiration.Right(2)),PIn.Int(expiration.Left(2)),1);
						//If the stored CC used for this X-Charge payment has a PayConnect token, and X-Charge returns a different masked number or exp date, we
						//will clear out the PayConnect token since this CC no longer refers to the same card that was used to generate the PayConnect token.
						if(!string.IsNullOrEmpty(cc.PayConnectToken) //there is a PayConnect token for this saved CC
							&& Regex.IsMatch(cc.CCNumberMasked,@"X+[0-9]{4}") //the saved CC has a masked number with the pattern XXXXXXXXXXXX1234
							&& (cc.CCNumberMasked.Right(4)!=accountMasked.Right(4) //and either the last four digits don't match what X-Charge returned
									|| cc.CCExpiration.Year!=expDate.Year //or the exp year doesn't match that returned by X-Charge
									|| cc.CCExpiration.Month!=expDate.Month)) //or the exp month doesn't match that returned by X-Charge
						{
							cc.PayConnectToken="";
							cc.PayConnectTokenExp=DateTime.MinValue;
						}
						//Only way this code can be hit is if they have set up a credit card and it does not have a token.
						//So we'll use the created token from result file and assign it to the coresponding account.
						//Also will delete the credit card number and replace it with secure masked number.
						cc.XChargeToken=xChargeToken;
						cc.CCNumberMasked=accountMasked;
						cc.CCExpiration=expDate;
						cc.Procedures=PrefC.GetString(PrefName.DefaultCCProcs);
						cc.CCSource=CreditCardSource.XServer;
						CreditCards.Update(cc);
					}
					if(newCard && prepaidAmt==0) {//never save card information to the patient account for prepaid cards
						if(!string.IsNullOrEmpty(xChargeToken) && FormXT.SaveToken) {
							_xChargeMilestone="Create New Credit Card Entry";
							cc=new CreditCard();
							List<CreditCard> itemOrderCount=CreditCards.Refresh(_patCur.PatNum);
							cc.ItemOrder=itemOrderCount.Count;
							cc.PatNum=_patCur.PatNum;
							cc.CCExpiration=new DateTime(Convert.ToInt32("20"+expiration.Substring(2,2)),Convert.ToInt32(expiration.Substring(0,2)),1);
							cc.XChargeToken=xChargeToken;
							cc.CCNumberMasked=accountMasked;
							cc.Procedures=PrefC.GetString(PrefName.DefaultCCProcs);
							cc.CCSource=CreditCardSource.XServer;
							cc.ClinicNum=_paymentCur.ClinicNum;
							creditCardNum=CreditCards.Insert(cc);
						}
						else if(string.IsNullOrEmpty(xChargeToken)) {//Shouldn't happen again but leaving just in case.
							MsgBox.Show(this,"X-Charge didn't return a token so credit card information couldn't be saved.");
						}
					}
					if(updateCard && newAccount!="" && newExpiration.Year>1880 && prepaidAmt==0) {//Never save credit card info to patient for prepaid cards.
						if(textNote.Text!="") {
							textNote.Text+="\r\n";
						}
						if(cc.CCNumberMasked != newAccount) {
							textNote.Text+=Lan.g(this,"Account number changed from")+" "+cc.CCNumberMasked+" "
								+Lan.g(this,"to")+" "+newAccount;
						}
						if(cc.CCExpiration != newExpiration) {
							textNote.Text+=Lan.g(this,"Expiration changed from")+" "+cc.CCExpiration.ToString("MMyy")+" "
								+Lan.g(this,"to")+" "+newExpiration.ToString("MMyy");
						}
						cc.CCNumberMasked=newAccount;
						cc.CCExpiration=newExpiration;
						CreditCards.Update(cc);
					}
				}
			}
			catch {
				MessageBox.Show(Lan.g(this,"There was a problem charging the card.  Please run the credit card report from inside X-Charge to verify that "
					+"the card was not actually charged.")+"\r\n"+Lan.g(this,"If the card was charged, you need to make sure that the payment amount matches.")
					+"\r\n"+Lan.g(this,"If the card was not charged, please try again."));
				return null;
			}
			_xChargeMilestone="Check Approved Amount";
			if(showApprovedAmtNotice && !xVoid && !xAdjust && !xReturn) {
				MessageBox.Show(Lan.g(this,"The amount you typed in")+": "+amt.ToString("C")+"\r\n"+Lan.g(this,"does not match the approved amount returned")
					+": "+approvedAmt.ToString("C")+".\r\n"+Lan.g(this,"The amount will be changed to reflect the approved amount charged."),"Alert",
					MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				textAmount.Text=approvedAmt.ToString("F");
			}
			if(xAdjust) {
				_xChargeMilestone="Check Adjust";
				MessageBox.Show(Lan.g(this,"The amount will be changed to the X-Charge approved amount")+": "+approvedAmt.ToString("C"));
				textNote.Text="";
				textAmount.Text=approvedAmt.ToString("F");
			}
			else if(xReturn) {
				_xChargeMilestone="Check Return";
				textAmount.Text="-"+approvedAmt.ToString("F");
			}
			else if(xVoid) {//For prepaid cards, tranType is set to 0 "Purchase", therefore xVoid will be false.
				_xChargeMilestone="Check Void";
				if(IsNew) {
					if(!_wasCreditCardSuccessful) {
						textAmount.Text="-"+approvedAmt.ToString("F");
						textNote.Text+=resulttext;
					}
					_paymentCur.Receipt=receipt;
					if(_printReceipt && receipt!="") {
						PrintReceipt(receipt);
						_printReceipt=false;
					}
					SavePaymentToDb();//This function assumes _patCur is not null.
				}
				_xChargeMilestone="Create Negative Payment";
				if(!IsNew || _wasCreditCardSuccessful) {//Create a new negative payment if the void is being run from an existing payment
					if(_listPaySplits.Count==0) {
						AddOneSplit();
						FillMain();
					}
					else if(_listPaySplits.Count==1//if one split
						&& _listPaySplits[0].PayPlanNum!=0//and split is on a payment plan
						&& _listPaySplits[0].SplitAmt!=_paymentCur.PayAmt)//and amount doesn't match payment
					{
						_listPaySplits[0].SplitAmt=_paymentCur.PayAmt;//make amounts match automatically
						textTotal.Text=textAmount.Text;
					}
					_paymentCur.IsSplit=_listPaySplits.Count>1;
					Payment voidPayment=_paymentCur.Clone();
					voidPayment.PayAmt*=-1;//the negation of the original amount
					voidPayment.PayNote=resulttext;
					voidPayment.Receipt=receipt;
					if(_printReceipt && receipt!="") {
						PrintReceipt(receipt);
					}
					voidPayment.PaymentSource=CreditCardSource.XServer;
					voidPayment.ProcessStatus=ProcessStat.OfficeProcessed;
					voidPayment.PayNum=Payments.Insert(voidPayment);
					foreach(PaySplit splitCur in _listPaySplits) {//Modify the paysplits for the original transaction to work for the void transaction
						PaySplit split=splitCur.Copy();
						split.SplitAmt*=-1;
						split.PayNum=voidPayment.PayNum;
						PaySplits.Insert(split);
					}
				}
				DialogResult=DialogResult.OK;//Close FormPayment window now so the user will not have the option to hit Cancel
				return resulttext;
			}
			_xChargeMilestone="Check Additional Funds";
			_wasCreditCardSuccessful=!_isCCDeclined;//If the transaction is not a void transaction, we will void this transaction if the user hits Cancel
			if(additionalFunds>0) {
				MessageBox.Show(Lan.g(this,"Additional funds required")+": "+additionalFunds.ToString("C"));
			}
			if(textNote.Text!="") {
				textNote.Text+="\r\n";
			}
			textNote.Text+=resulttext;
			_xChargeMilestone="Receipt";
			_paymentCur.Receipt=receipt;
			if(!string.IsNullOrEmpty(receipt)) {
				butPrintReceipt.Visible=true;
				butEmailReceipt.Visible=true;
				if(_printReceipt && prepaidAmt==0) {
					PrintReceipt(receipt);
				}
			}
			_xChargeMilestone="Reselect Credit Card in Combo";
			if(cc!=null && !string.IsNullOrEmpty(cc.XChargeToken) && cc.CCExpiration!=null) {
				//Refresh comboCreditCards and select the index of the card used for this payment if the token was saved
				creditCards=CreditCards.Refresh(_patCur.PatNum);
				comboCreditCards.Items.Clear();
				comboCreditCards.SelectedIndex=-1;
				for(int i=0;i<creditCards.Count;i++) {
					comboCreditCards.Items.Add(creditCards[i].CCNumberMasked);
					if(creditCards[i].XChargeToken==cc.XChargeToken
						&& creditCards[i].CCExpiration.Year==cc.CCExpiration.Year
						&& creditCards[i].CCExpiration.Month==cc.CCExpiration.Month)
					{
						comboCreditCards.SelectedIndex=i;
					}
				}
				comboCreditCards.Items.Add(Lan.g(this,"New card"));
				if(comboCreditCards.SelectedIndex==-1) {
					comboCreditCards.SelectedIndex=comboCreditCards.Items.Count-1;
				}
			}
			if(_isCCDeclined) {
				return null;
			}
			return resulttext;
		}

		///<summary>Only used to void a transaction that has just been completed when the user hits Cancel. Uses the same Print Receipt settings as the 
		///original transaction.</summary>
		private void VoidXChargeTransaction(string transID,string amount,bool isDebit) {
			ProcessStartInfo info=new ProcessStartInfo(_xProg.Path);
			string resultfile=PrefC.GetRandomTempFile("txt");
			File.Delete(resultfile);//delete the old result file.
			info.Arguments="";
			if(isDebit) {
				info.Arguments+="/TRANSACTIONTYPE:DEBITRETURN /LOCKTRANTYPE ";
			}
			else {
				info.Arguments+="/TRANSACTIONTYPE:VOID /LOCKTRANTYPE ";
			}
			info.Arguments+="/XCTRANSACTIONID:"+transID+" /LOCKXCTRANSACTIONID ";
			info.Arguments+="/AMOUNT:"+amount+" /LOCKAMOUNT ";
			info.Arguments+="/RECEIPT:Pat"+_paymentCur.PatNum.ToString()+" ";//aka invoice#
			info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" /LOCKCLERK ";
			info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
			info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(_xProg.ProgramNum,"Username",_paymentCur.ClinicNum)+" ";
			info.Arguments+="/PASSWORD:"+CodeBase.MiscUtils.Decrypt(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Password",_paymentCur.ClinicNum))+" ";
			info.Arguments+="/AUTOCLOSE ";
			info.Arguments+="/HIDEMAINWINDOW /SMALLWINDOW ";
			if(!isDebit) {
				info.Arguments+="/AUTOPROCESS ";
			}
			info.Arguments+="/PROMPTSIGNATURE:F ";
			info.Arguments+="/RECEIPTINRESULT ";
			Cursor=Cursors.WaitCursor;
			Process process=new Process();
			process.StartInfo=info;
			process.EnableRaisingEvents=true;
			process.Start();
			process.WaitForExit();
			Thread.Sleep(200);//Wait 2/10 second to give time for file to be created.
			Cursor=Cursors.Default;
			//Next, record the voided payment within Open Dental.  We use to delete the payment but Nathan wants us to negate voids with another payment.
			string resulttext="";
			string line="";
			bool showApprovedAmtNotice=false;
			double approvedAmt=0;
			string receipt="";
			Payment voidPayment=_paymentCur.Clone();
			voidPayment.PayAmt*=-1;//the negation of the original amount
			try {
				using(TextReader reader=new StreamReader(resultfile)) {
					line=reader.ReadLine();
					/*Example of successful void transaction:
						RESULT=SUCCESS
						TYPE=Void
						APPROVALCODE=000000
						SWIPED=F
						CLERK=Admin
						XCACCOUNTID=XAWpQPwLm7MXZ
						XCTRANSACTIONID=15042616
						ACCOUNT=XXXXXXXXXXXX6781
						EXPIRATION=1215
						ACCOUNTTYPE=VISA
						APPROVEDAMOUNT=11.00
					*/
					while(line!=null) {
						if(!line.StartsWith("RECEIPT=")) {//Don't include the receipt string in the PayNote
							if(resulttext!="") {
								resulttext+="\r\n";
							}
							resulttext+=line;
						}
						if(line.StartsWith("RESULT=")) {
							if(line!="RESULT=SUCCESS") {
								//Void was a failure and there might be a description as to why it failed. Continue to loop through line.
								while(line!=null) {
									line=reader.ReadLine();
									resulttext+="\r\n"+line;
								}
								break;
							}
						}
						if(line.StartsWith("APPROVEDAMOUNT=")) {
							approvedAmt=PIn.Double(line.Substring(15));
							if(approvedAmt != _paymentCur.PayAmt) {
								showApprovedAmtNotice=true;
							}
						}
						if(line.StartsWith("RECEIPT=") && line.Length>8) {
							receipt=PIn.String(line.Substring(8));
							receipt=receipt.Replace("\\n","\r\n");//The receipt from X-Charge escapes the newline characters
						}
						line=reader.ReadLine();
					}
				}
			}
			catch {
				MessageBox.Show(Lan.g(this,"There was a problem voiding this transaction.")+"\r\n"+Lan.g(this,"Please run the credit card report from inside "
					+"X-Charge to verify that the transaction was voided.")+"\r\n"+Lan.g(this,"If the transaction was not voided, please create a new payment "
					+"to void the transaction."));
				return;
			}
			if(showApprovedAmtNotice) {
				MessageBox.Show(Lan.g(this,"The amount of the original transaction")+": "+_paymentCur.PayAmt.ToString("C")+"\r\n"+Lan.g(this,"does not match "
					+"the approved amount returned")+": "+approvedAmt.ToString("C")+".\r\n"+Lan.g(this,"The amount will be changed to reflect the approved "
					+"amount charged."),"Alert",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				voidPayment.PayAmt=approvedAmt;
			}
			if(textNote.Text!="") {
				textNote.Text+="\r\n";
			}
			voidPayment.PayNote=resulttext;
			voidPayment.Receipt=receipt;
			if(_printReceipt && receipt!="") {
				PrintReceipt(receipt);
			}
			voidPayment.PaymentSource=CreditCardSource.XServer;
			voidPayment.ProcessStatus=ProcessStat.OfficeProcessed;
			voidPayment.PayNum=Payments.Insert(voidPayment);
			for(int i=0;i<_listPaySplits.Count;i++) {//Modify the paysplits for the original transaction to work for the void transaction
				PaySplit split=_listPaySplits[i].Copy();
				split.SplitAmt*=-1;
				split.PayNum=voidPayment.PayNum;
				PaySplits.Insert(split);
			}
			SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,voidPayment.PatNum,Patients.GetLim(voidPayment.PatNum).GetNameLF()+", "
				+voidPayment.PayAmt.ToString("c"));
		}

		private bool HasXCharge() {
			if(_xProg==null) {
				MsgBox.Show(this,"X-Charge entry is missing from the database.");//should never happen
				return false;
			}
			bool isSetupRequired=!_xProg.Enabled;//if X-Charge is disabled, setup is required
			//if X-Charge is enabled, but the Username or Password are blank or the PaymentType is not a valid DefNum, setup is required
			if(_xProg.Enabled) {
				//X-Charge is enabled if the username and password are set and the PaymentType is a valid DefNum
				//If clinics are disabled, _paymentCur.ClinicNum will be 0 and the Username and Password will be the 'Headquarters' or practice credentials
				string paymentType=ProgramProperties.GetPropVal(_xProg.ProgramNum,"PaymentType",_paymentCur.ClinicNum);
				if(string.IsNullOrEmpty(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Username",_paymentCur.ClinicNum))
					|| string.IsNullOrEmpty(ProgramProperties.GetPropVal(_xProg.ProgramNum,"Password",_paymentCur.ClinicNum))
					|| !DefC.Short[(int)DefCat.PaymentTypes].Any(x => x.DefNum.ToString()==paymentType))
				{
					isSetupRequired=true;
				}
			}
			//if X-Charge is enabled and the Username and Password is set and the PaymentType is a valid DefNum,
			//make sure the path (either local override or program path) is valid
			if(!isSetupRequired && !File.Exists(_xPath)) {
				MsgBox.Show(this,"Path is not valid.");
				isSetupRequired=true;
			}
			//if setup is required and the user is authorized for setup, load the X-Charge setup form, but return false so the validation can happen again
			if(isSetupRequired && Security.IsAuthorized(Permissions.Setup)) {
				FormXchargeSetup FormX=new FormXchargeSetup();
				FormX.ShowDialog();
				CheckUIState();//user may have made a change in setup that affects the state of the UI, e.g. X-Charge is no longer enabled for this clinic
				return false;
			}
			return true;
		}

		private string GetXChargeTransactionTypeCommands(int tranType,bool hasXToken,bool notRecurring,CreditCard CCard,string cashBack) {
			string tranText="";
			switch(tranType) {
				case 0:
					tranText+="/TRANSACTIONTYPE:PURCHASE /LOCKTRANTYPE /LOCKAMOUNT ";
					if(hasXToken && CCard!=null) {
						tranText+="/XCACCOUNTID:"+CCard.XChargeToken+" ";
						tranText+="/AUTOPROCESS ";
						tranText+="/GETXCACCOUNTIDSTATUS ";
					}
					if(notRecurring && CCard!=null) {
						tranText+="/ACCOUNT:"+CCard.CCNumberMasked+" ";
						tranText+="/AUTOPROCESS ";
					}
					break;
				case 1:
					tranText+="/TRANSACTIONTYPE:RETURN /LOCKTRANTYPE /LOCKAMOUNT ";
					if(hasXToken) {
						tranText+="/XCACCOUNTID:"+CCard.XChargeToken+" ";
						tranText+="/AUTOPROCESS ";
						tranText+="/GETXCACCOUNTIDSTATUS ";
					}
					if(notRecurring) {
						tranText+="/ACCOUNT:"+CCard.CCNumberMasked+" ";
						tranText+="/AUTOPROCESS ";
					}
					break;
				case 2:
					tranText+="/TRANSACTIONTYPE:DEBITPURCHASE /LOCKTRANTYPE /LOCKAMOUNT ";
					tranText+="/CASHBACK:"+cashBack+" ";
					break;
				case 3:
					tranText+="/TRANSACTIONTYPE:DEBITRETURN /LOCKTRANTYPE /LOCKAMOUNT ";
					break;
				case 4:
					tranText+="/TRANSACTIONTYPE:FORCE /LOCKTRANTYPE /LOCKAMOUNT ";
					break;
				case 5:
					tranText+="/TRANSACTIONTYPE:PREAUTH /LOCKTRANTYPE /LOCKAMOUNT ";
					if(hasXToken) {
						tranText+="/XCACCOUNTID:"+CCard.XChargeToken+" ";
						tranText+="/AUTOPROCESS ";
						tranText+="/GETXCACCOUNTIDSTATUS ";
					}
					if(notRecurring) {
						tranText+="/ACCOUNT:"+CCard.CCNumberMasked+" ";
						tranText+="/AUTOPROCESS ";
					}
					break;
				case 6:
					tranText+="/TRANSACTIONTYPE:ADJUSTMENT /LOCKTRANTYPE ";//excluding /LOCKAMOUNT, amount must be editable in X-Charge to make an adjustment
					string adjustTransactionID="";
					string[] noteSplit=Regex.Split(textNote.Text,"\r\n");
					foreach(string XCTrans in noteSplit) {
						if(XCTrans.StartsWith("XCTRANSACTIONID=")) {
							adjustTransactionID=XCTrans.Substring(16);
						}
					}
					if(adjustTransactionID!="") {
						tranText+="/XCTRANSACTIONID:"+adjustTransactionID+" ";
						tranText+="/AUTOPROCESS ";
					}
					break;
				case 7:
					tranText+="/TRANSACTIONTYPE:VOID /LOCKTRANTYPE /LOCKAMOUNT ";
					break;
			}
			if(_promptSignature) {
				tranText+="/PROMPTSIGNATURE:T /SAVESIGNATURE:T ";
			}
			else {
				tranText+="/PROMPTSIGNATURE:F ";
			}
			tranText+="/RECEIPTINRESULT ";//So that we can make a few changes to the receipt ourselves
			return tranText;
		}
		
		private void butReturn_Click(object sender,EventArgs e) {
			CreditCard cc=null;
			List<CreditCard> creditCards=CreditCards.Refresh(_patCur.PatNum);
			if(comboCreditCards.SelectedIndex<creditCards.Count && comboCreditCards.SelectedIndex>-1) {
				cc=creditCards[comboCreditCards.SelectedIndex];
			}
			if(cc==null) {
				MsgBox.Show(this,"Card no longer available. Return cannot be processed.");
				return;
			}
			if(!cc.IsXWeb()) {
				MsgBox.Show(this,"Only cards that were created from XWeb can process an XWeb return.");
				return;
			}
			FormXWeb FormXW=new FormXWeb(_patCur.PatNum,cc,XWebTransactionType.CreditReturnTransaction,createPayment:false);
			FormXW.LockCardInfo=true;
			if(FormXW.ShowDialog()==DialogResult.OK) {
				if(FormXW.ResponseResult!=null) {
					textNote.Text=FormXW.ResponseResult.GetFormattedNote(false);
					textAmount.Text=(-FormXW.ResponseResult.Amount).ToString();//XWeb amounts are always positive even for returns and voids.
					_xWebResponse=FormXW.ResponseResult;
					_xWebResponse.PaymentNum=_paymentCur.PayNum;
					XWebResponses.Update(_xWebResponse);
					butVoid.Visible=true;
					groupXWeb.Height=85;
				}
				MsgBox.Show(this,"Return successful.");
			}
		}

		private void butVoid_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.PaymentCreate)) {
				return;
			}
			double amount=_xWebResponse.Amount;
			if(_xWebResponse.XTransactionType==XWebTransactionType.CreditReturnTransaction 
				|| _xWebResponse.XTransactionType==XWebTransactionType.DebitReturnTransaction) 
			{
				amount=-amount;//The amount in an xwebresponse is always stored as a positive number.
			}
			if(MessageBox.Show(Lan.g(this,"Void the XWeb transaction of amount")+" "+amount.ToString("f")+" "+Lan.g(this,"attached to this payment?"),
				"",MessageBoxButtons.YesNo)==DialogResult.No)
			{
				return;
			}
			try {
				Cursor=Cursors.WaitCursor;
				string payNote=Lan.g(this,"Void XWeb payment made from within Open Dental");
				XWebs.VoidPayment(_patCur.PatNum,payNote,_xWebResponse.XWebResponseNum);
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Void successful. A new payment has been created for this void transaction.");
			}
			catch(ODException ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}

		private void butPayConnect_Click(object sender,EventArgs e) {
			MakePayConnectTransaction();
		}

		public string MakePayConnectTransaction(double prepaidAmt=0) {
			if(!HasPayConnect()) {
				return null;
			}
			if(prepaidAmt==0) {//Validation for regular credit cards (not prepaid cards).
				if(textAmount.Text=="") {
					MsgBox.Show(this,"Please enter an amount first.");
					textAmount.Focus();
					return null;
				}
				if(_listPaySplits.Count>0 && PIn.Double(textAmount.Text)!=PIn.Double(textTotal.Text)
					&& (_listPaySplits.Count!=1 || _listPaySplits[0].PayPlanNum==0)) //Not one paysplit attached to payplan
				{
					MsgBox.Show(this, "Split totals must equal payment amount before running a credit card transaction.");
					return null;
				}
			}
			CreditCard cc=null;
			List<CreditCard> creditCards=null;
			string amountStr=textAmount.Text;
			if(prepaidAmt==0) {
				creditCards=CreditCards.Refresh(_patCur.PatNum);
				if(comboCreditCards.SelectedIndex<creditCards.Count) {
					cc=creditCards[comboCreditCards.SelectedIndex];
				}
			}
			else {//Prepaid card
				amountStr=prepaidAmt.ToString("F");
			}
			FormPayConnect FormP=new FormPayConnect(_paymentCur.ClinicNum,_patCur,amountStr,cc);
			FormP.ShowDialog();
			if(prepaidAmt==0) {//Regular credit cards (not prepaid cards).
				//If PayConnect response is not null, refresh comboCreditCards and select the index of the card used for this payment if the token was saved
				creditCards=CreditCards.Refresh(_patCur.PatNum);
				comboCreditCards.Items.Clear();
				comboCreditCards.SelectedIndex=-1;
				for(int i=0;i<creditCards.Count;i++) {
					comboCreditCards.Items.Add(creditCards[i].CCNumberMasked);
					if(FormP.Response==null || string.IsNullOrEmpty(FormP.Response.PaymentToken)) {
						continue;
					}
					if(creditCards[i].PayConnectToken==FormP.Response.PaymentToken
						&& creditCards[i].PayConnectTokenExp.Year==FormP.Response.TokenExpiration.Year
						&& creditCards[i].PayConnectTokenExp.Month==FormP.Response.TokenExpiration.Month)
					{
						comboCreditCards.SelectedIndex=i;
					}
				}
				comboCreditCards.Items.Add(Lan.g(this,"New card"));
				if(comboCreditCards.SelectedIndex==-1) {
					comboCreditCards.SelectedIndex=comboCreditCards.Items.Count-1;
				}
				Program prog=Programs.GetCur(ProgramName.PayConnect);
				//still need to add functionality for accountingAutoPay
				string paytype=ProgramProperties.GetPropVal(prog.ProgramNum,"PaymentType",_paymentCur.ClinicNum);//paytype could be an empty string
				listPayType.SelectedIndex=DefC.GetOrder(DefCat.PaymentTypes,PIn.Long(paytype));
				SetComboDepositAccounts();
			}
			string resultNote=null;
			if(FormP.Response!=null) {
				resultNote=Lan.g(this,"Transaction Type")+": "+Enum.GetName(typeof(PayConnectService.transType),FormP.TranType)+Environment.NewLine+
					Lan.g(this,"Status")+": "+FormP.Response.Description+Environment.NewLine+
					Lan.g(this,"Amount")+": "+FormP.AmountCharged+Environment.NewLine+
					Lan.g(this,"Card Type")+": "+FormP.Response.CardType+Environment.NewLine+
					Lan.g(this,"Account")+": "+FormP.CardNumber.Right(4).PadLeft(FormP.CardNumber.Length,'X');
			}
			if(prepaidAmt!=0) {
				if(FormP.Response!=null && FormP.Response.StatusCode=="0") { //The transaction succeeded.
					return resultNote;
				}
				return null;
			}
			if(FormP.Response!=null) {
				if(FormP.Response.StatusCode=="0") { //The transaction succeeded.					
					_isCCDeclined=false;
					resultNote+=Environment.NewLine
						+Lan.g(this,"Auth Code")+": "+FormP.Response.AuthCode+Environment.NewLine
						+Lan.g(this,"Ref Number")+": "+FormP.Response.RefNumber;
					if(FormP.TranType==PayConnectService.transType.RETURN) {
						textAmount.Text="-"+FormP.AmountCharged;
					}
					else if(FormP.TranType==PayConnectService.transType.AUTH) {
						textAmount.Text=FormP.AmountCharged;
					}
					else if(FormP.TranType==PayConnectService.transType.SALE) {
						textAmount.Text=FormP.AmountCharged;
						_paymentCur.Receipt=FormP.ReceiptStr; //There is only a receipt when a sale takes place.
					}
					if(FormP.TranType==PayConnectService.transType.VOID) {//Close FormPayment window now so the user will not have the option to hit Cancel
						if(IsNew) {
							if(!_wasCreditCardSuccessful) {
								textAmount.Text="-"+FormP.AmountCharged;
								textNote.Text+=((textNote.Text=="")?"":Environment.NewLine)+resultNote;
							}
							_paymentCur.Receipt=FormP.ReceiptStr;
							SavePaymentToDb();
						}
						if(!IsNew || _wasCreditCardSuccessful) {//Create a new negative payment if the void is being run from an existing payment
							if(_listPaySplits.Count==0) {
								AddOneSplit();
								FillMain();
							}
							else if(_listPaySplits.Count==1//if one split
								&& _listPaySplits[0].PayPlanNum!=0//and split is on a payment plan
								&& _listPaySplits[0].SplitAmt!=_paymentCur.PayAmt)//and amount doesn't match payment
							{
								_listPaySplits[0].SplitAmt=_paymentCur.PayAmt;//make amounts match automatically
								textTotal.Text=textAmount.Text;
							}
							_paymentCur.IsSplit=_listPaySplits.Count>1;
							Payment voidPayment=_paymentCur.Clone();
							voidPayment.PayAmt*=-1;//the negation of the original amount
							voidPayment.PayNote=resultNote;
							voidPayment.Receipt=FormP.ReceiptStr;
							voidPayment.PaymentSource=CreditCardSource.PayConnect;
							voidPayment.ProcessStatus=ProcessStat.OfficeProcessed;
							voidPayment.PayNum=Payments.Insert(voidPayment);
							foreach(PaySplit splitCur in _listPaySplits) {//Modify the paysplits for the original transaction to work for the void transaction
								PaySplit split=splitCur.Copy();
								split.SplitAmt*=-1;
								split.PayNum=voidPayment.PayNum;
								PaySplits.Insert(split);
							}
						}
						MsgBox.Show(this,"Void successful.");
						DialogResult=DialogResult.OK;//Close FormPayment window now so the user will not have the option to hit Cancel
						return resultNote;
					}
					else {//Not Void
						_wasCreditCardSuccessful=true; //Will void the transaction if user cancels out of window.
					}
					_payConnectRequest=FormP.Request;					
				}
				textNote.Text+=((textNote.Text=="")?"":Environment.NewLine)+resultNote;
				textNote.Select(textNote.Text.Length-1,0);
				textNote.ScrollToCaret();//Scroll to the end of the text box to see the newest notes.
				_paymentOld.PayNote=textNote.Text;
				_paymentOld.PaymentSource=CreditCardSource.PayConnect;
				_paymentOld.ProcessStatus=ProcessStat.OfficeProcessed;
				Payments.Update(_paymentOld,true);
			}
			if(!string.IsNullOrEmpty(_paymentCur.Receipt)) {
				butPrintReceipt.Visible=true;
				butEmailReceipt.Visible=true;
			}
			if(FormP.Response==null || FormP.Response.StatusCode!="0") { //The transaction failed.
				if(FormP.TranType==PayConnectService.transType.SALE || FormP.TranType==PayConnectService.transType.AUTH) {
					textAmount.Text=FormP.AmountCharged;//Preserve the amount so the user can try the payment again more easily.
				}
				_isCCDeclined=true;
				_wasCreditCardSuccessful=false;
				return null;
			}
			return resultNote;
		}

		///<summary>Returns true if payconnect is enabled and completely setup.</summary>
		private bool HasPayConnect() {
			Program prog=Programs.GetCur(ProgramName.PayConnect);
			bool isSetupRequired=false;
			if(prog.Enabled) {
				//If clinics are disabled, _paymentCur.ClinicNum will be 0 and the Username and Password will be the 'Headquarters' or practice credentials
				string paymentType=ProgramProperties.GetPropVal(prog.ProgramNum,"PaymentType",_paymentCur.ClinicNum);
				if(string.IsNullOrEmpty(ProgramProperties.GetPropVal(prog.ProgramNum,"Username",_paymentCur.ClinicNum))
					|| string.IsNullOrEmpty(ProgramProperties.GetPropVal(prog.ProgramNum,"Password",_paymentCur.ClinicNum))
					|| !DefC.Short[(int)DefCat.PaymentTypes].Any(x => x.DefNum.ToString()==paymentType)) {
					isSetupRequired=true;
				}
			}
			else {//Program link not enabled.  They need to enable it first.
				isSetupRequired=true;
			}
			if(isSetupRequired) {
				if(!Security.IsAuthorized(Permissions.Setup)) {
					return false;
				}
				FormPayConnectSetup FormPCS=new FormPayConnectSetup();
				FormPCS.ShowDialog();
				if(FormPCS.DialogResult!=DialogResult.OK) {
					return false;
				}
				//The user could have corrected the PayConnect bridge, recursively try again.
				return HasPayConnect();
			}
			return true;
		}

		private void VoidPayConnectTransaction(string refNum,string amount) {
			PayConnectResponse response=null;
			Cursor=Cursors.WaitCursor;
			if(_payConnectRequest==null) {//The payment was made through the terminal.
				Action actionCloseProgress=null;
				try {
					PosRequest posRequest=PosRequest.CreateVoidByReference(refNum);
					actionCloseProgress=ODProgress.ShowProgressStatus("PayConnectProcessing","Processing void on terminal");
					PosResponse posResponse=DpsPos.ProcessCreditCard(posRequest);
					response=new PayConnectResponse(posResponse);
				}
				catch(Exception ex) {
					Cursor=Cursors.Default;
					MessageBox.Show(Lan.g(this,"Error voiding payment:")+" "+ex.Message);
				}
				finally {
					actionCloseProgress?.Invoke();
				}
			}
			else {//The payment was made through the web service.
				_payConnectRequest.TransType=PayConnectService.transType.VOID;
				_payConnectRequest.RefNumber=refNum;
				_payConnectRequest.Amount=PIn.Decimal(amount);
				PayConnectService.transResponse transResponse=PayConnect.ProcessCreditCard(_payConnectRequest,_paymentCur.ClinicNum);
				response=new PayConnectResponse(transResponse,_payConnectRequest);
			}
			Cursor=Cursors.Default;
			if(response==null || response.StatusCode!="0") {//error in transaction
				MsgBox.Show(this,"This credit card payment has already been processed and will have to be voided manually through the web interface.");
				return;
			}
			else {//Record a new payment for the voided transaction
				Payment voidPayment=_paymentCur.Clone();
				voidPayment.PayAmt*=-1; //The negated amount of the original payment
				voidPayment.Receipt=""; //Only SALE transactions have receipts
				voidPayment.PayNote=Lan.g(this,"Transaction Type")+": "+Enum.GetName(typeof(PayConnectService.transType),PayConnectService.transType.VOID)
					+Environment.NewLine+Lan.g(this,"Status")+": "+response.Description+Environment.NewLine
					+Lan.g(this,"Amount")+": "+voidPayment.PayAmt+Environment.NewLine
					+Lan.g(this,"Auth Code")+": "+response.AuthCode+Environment.NewLine
					+Lan.g(this,"Ref Number")+": "+response.RefNumber;
				voidPayment.PaymentSource=CreditCardSource.PayConnect;
				voidPayment.ProcessStatus=ProcessStat.OfficeProcessed;
				voidPayment.PayNum=Payments.Insert(voidPayment);
				for(int i=0;i<_listPaySplits.Count;i++) {//Modify the paysplits for the original transaction to work for the void transaction
					PaySplit split=_listPaySplits[i].Copy();
					split.SplitAmt*=-1;
					split.PayNum=voidPayment.PayNum;
					PaySplits.Insert(split);
				}
				SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,voidPayment.PatNum,
					Patients.GetLim(voidPayment.PatNum).GetNameLF()+", "+voidPayment.PayAmt.ToString("c"));
			}
		}

		private void menuXcharge_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup)) {
				FormXchargeSetup FormX=new FormXchargeSetup();
				FormX.ShowDialog();
				CheckUIState();
			}
		}

		private void menuPayConnect_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup)) {
				FormPayConnectSetup fpcs=new FormPayConnectSetup();
				fpcs.ShowDialog();
				CheckUIState();
			}
		}

		private void gridBal_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			MsgBox.Show(this,"This grid is not editable.  Family balances are altered by using splits in the grid to the left.");
		}

		private void comboClinic_SelectionChangeCommitted(object sender,EventArgs e) {
			//_listUserClinicNums contains all clinics the user has access to as well as ClinicNum 0 for 'none'
			_paymentCur.ClinicNum=_listUserClinicNums[comboClinic.SelectedIndex];
			if(_listPaySplits.Count>0) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Change clinic for all splits?")) {
					return;
				}
				for(int i=0;i<_listPaySplits.Count;i++) {
					_listPaySplits[i].ClinicNum=_paymentCur.ClinicNum;
				}
				FillMain();
			}
			CheckUIState();
		}

		private void checkPayTypeNone_CheckedChanged(object sender,EventArgs e) {
			//this fires before the click event.  The Checked property also reflects the new value.
			if(checkPayTypeNone.Checked) {
				listPayType.Visible=false;
				panelXcharge.Visible=false;
				butPay.Text=Lan.g(this,"Transfer");
			}
			else {
				listPayType.Visible=true;
				panelXcharge.Visible=true;
				butPay.Text=Lan.g(this,"Pay");
			}
		}

		private void checkPayTypeNone_Click(object sender,EventArgs e) {
			//The Checked property reflects the new value.
			//Only possible if IsNew.

		}

		private void butSplitManage_Click(object sender,EventArgs e) {
			FormPaySplitManage FormPSM=new FormPaySplitManage();
			FormPSM.PaymentAmt=PIn.Decimal(textAmount.Text);
			FormPSM.FamCur=Patients.GetFamily(_patCur.PatNum);
			FormPSM.PatCur=_patCur;
			FormPSM.PaymentCur=_paymentCur.Clone();
			FormPSM.PayDate=PIn.DateT(textDate.Text);
			FormPSM.IsNew=IsNew;
			List<PaySplit> listSplits=new List<PaySplit>();
			for(int i=0;i<_listPaySplits.Count;i++) {
				listSplits.Add(_listPaySplits[i].Copy());
			}
			FormPSM.ListSplitsCur=listSplits;
			FormPSM.ShowDialog();
			if(FormPSM.DialogResult==DialogResult.OK) {
				_listPaySplits=FormPSM.ListSplitsCur;
				decimal amount=FormPSM.AmtTotal;
				textAmount.Text=amount.ToString("F");
			}
			FillMain();
		}

		private void butDeleteAll_Click(object sender,System.EventArgs e) {
			if(textDeposit.Visible) {//this will get checked again by the middle layer
				MsgBox.Show(this,"This payment is attached to a deposit.  Not allowed to delete.");
				return;
			}
			if(PaySplits.GetSplitsForPrepay(_listPaySplits).Count>0) {
				MsgBox.Show(this,"This prepayment has been allocated.  Not allowed to delete.");
				return;
			}
			if(!MsgBox.Show(this,true,"This will delete the entire payment and all splits.")) {
				return;
			}
			//If payment is attached to a transaction which is more than 48 hours old, then not allowed to delete.
			//This is hard coded.  User would have to delete or detach from within transaction rather than here.
			Transaction trans=Transactions.GetAttachedToPayment(_paymentCur.PayNum);
			if(trans != null) {
				if(trans.DateTimeEntry < MiscData.GetNowDateTime().AddDays(-2)) {
					MsgBox.Show(this,"Not allowed to delete.  This payment is already attached to an accounting transaction.  You will need to detach it from "
						+"within the accounting section of the program.");
					return;
				}
				if(Transactions.IsReconciled(trans)) {
					MsgBox.Show(this,"Not allowed to delete.  This payment is attached to an accounting transaction that has been reconciled.  You will need "
						+"to detach it from within the accounting section of the program.");
					return;
				}
				try {
					Transactions.Delete(trans);
				}
				catch(ApplicationException ex) {
					MessageBox.Show(ex.Message);
					return;
				}
			}
			try {
				Payments.Delete(_paymentCur);
			}
			catch(ApplicationException ex) {//error if attached to deposit slip
				MessageBox.Show(ex.Message);
				return;
			}
			if(!IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,_paymentCur.PatNum,"Delete for: "+Patients.GetLim(_paymentCur.PatNum).GetNameLF()+", "
					+_paymentCur.PayAmt.ToString("c"));
			}
			DialogResult=DialogResult.OK;
		}

		private void butPrintReceipt_Click(object sender,EventArgs e) {
			PrintReceipt(_paymentCur.Receipt);
		}

		private void butEmailReceipt_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.EmailSend)){
				return;
			}
			if(string.IsNullOrWhiteSpace(_paymentCur.Receipt)) {
				MsgBox.Show(this,"There is no receipt to send for this payment.");
				return;
			}
			List<string> errors=new List<string>();
			if(!EmailAddresses.ExistsValidEmail()) {
				errors.Add(Lan.g(this,"SMTP server name missing in e-mail setup."));
			}
			if(PrefC.AtoZfolderUsed==DataStorageType.InDatabase){
				errors.Add(Lan.g(this,"No AtoZ folder."));
			}
			if(errors.Count>0) {
				MessageBox.Show(this,Lan.g(this,"The following errors need to be resolved before creating an email")+":\r\n"+string.Join("\r\n",errors));
				return;
			}
			string attachPath=EmailAttaches.GetAttachPath();
			Random rnd=new Random();
			string tempFile=ODFileUtils.CombinePaths(PrefC.GetTempFolderPath(),
				DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf");
			PdfDocumentRenderer pdfRenderer=new PdfDocumentRenderer(true,PdfFontEmbedding.Always);
			pdfRenderer.Document=CreatePDFDoc(_paymentCur.Receipt);
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(tempFile);
			FileAtoZ.Copy(tempFile,FileAtoZ.CombinePaths(attachPath,Path.GetFileName(tempFile)),FileAtoZSourceDestination.LocalToAtoZ);
			EmailMessage message=new EmailMessage();
			message.PatNum=_paymentCur.PatNum;
			message.ToAddress=_patCur.Email;
			EmailAddress address=EmailAddresses.GetByClinic(_patCur.ClinicNum);
			message.FromAddress=address.SenderAddress;
			message.Subject=Lan.g(this,"Receipt for payment received ")+_paymentCur.PayDate.ToShortDateString();
			EmailAttach attachRcpt=new EmailAttach() {
				DisplayedFileName="Receipt.pdf",
				ActualFileName=Path.GetFileName(tempFile)
			};
			message.Attachments=new List<EmailAttach>() { attachRcpt };
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
		}

		private void PrintReceipt(string receiptStr) {
			MigraDocPrintDocument printdoc=new MigraDocPrintDocument(new DocumentRenderer(CreatePDFDoc(receiptStr)));
			printdoc.Renderer.PrepareDocument();
#if DEBUG
			FormRpPrintPreview pView=new FormRpPrintPreview();
			pView.printPreviewControl2.Document=printdoc;
			pView.ShowDialog();
#else
			if(PrinterL.SetPrinter(_pd2,PrintSituation.Receipt,_patCur.PatNum,"X-Charge receipt printed")) {
				printdoc.PrinterSettings=_pd2.PrinterSettings;
				try {
					printdoc.Print();
				}
				catch(Exception ex) {
					MessageBox.Show(Lan.g(this,"Unable to print receipt")+". "+ex.Message);
				}
			}
#endif
		}

		private MigraDoc.DocumentObjectModel.Document CreatePDFDoc(string receiptStr) {
			string[] receiptLines=receiptStr.Split(new string[] { "\r\n" },StringSplitOptions.None);
			MigraDoc.DocumentObjectModel.Document doc=new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch(3.0);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch(0.181*receiptLines.Length+0.56);//enough to print text plus 9/16 in. (0.56) extra space at bottom.
			doc.DefaultPageSetup.TopMargin=Unit.FromInch(0.25);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch(0.25);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch(0.25);
			MigraDoc.DocumentObjectModel.Font bodyFontx=MigraDocHelper.CreateFont(8,false);
			bodyFontx.Name=FontFamily.GenericMonospace.Name;
			Section section=doc.AddSection();
			Paragraph par=section.AddParagraph();
			ParagraphFormat parformat=new ParagraphFormat();
			parformat.Alignment=ParagraphAlignment.Left;
			parformat.Font=bodyFontx;
			par.Format=parformat;
			par.AddFormattedText(receiptStr,bodyFontx);
			return doc;
		}

		private void butPrePay_Click(object sender,EventArgs e) {
			if(PIn.Double(textAmount.Text)==0) {
				MsgBox.Show(this,"Amount cannot be zero.");
				return;
			}
			if(_listPaySplits.Count>0) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"This will replace all Payment Splits with one split for the total amount.  Continue?")) {
					return;
				}
			}
			_listPaySplits.Clear();
			PaySplit split=new PaySplit();
			split.PatNum=_patCur.PatNum;
			split.PayNum=_paymentCur.PayNum;
			split.PrepaymentNum=0;
			split.SplitAmt=PIn.Double(textAmount.Text);
			split.DatePay=DateTime.Now;
			split.ClinicNum=_paymentCur.ClinicNum;
			split.UnearnedType=PrefC.GetLong(PrefName.PrepaymentUnearnedType);
			_listPaySplits.Add(split); 
			FillMain();
			if(!SavePaymentToDb()) {
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private bool SavePaymentToDb() {
			if(textDate.errorProvider1.GetError(textDate)!="" || textAmount.errorProvider1.GetError(textAmount)!="") {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(checkPayTypeNone.Checked) {
				if(PIn.Double(textAmount.Text)!=0) {
					MsgBox.Show(this,"Amount must be zero for a transfer.");
					return false;
				}
			}
			else {
				if(textAmount.Text=="") {
					MessageBox.Show(Lan.g(this,"Please enter an amount."));
					return false;
				}
				if(PIn.Double(textAmount.Text)==0 
          && (SplitManagerPromptType) PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)== SplitManagerPromptType.DoNotUse) {
					MessageBox.Show(Lan.g(this,"Amount must not be zero unless this is a transfer."));
					return false;
				}
				if(listPayType.SelectedIndex==-1) {
					MsgBox.Show(this,"A payment type must be selected.");
					return false;
				}
			}
			if(_isCCDeclined) {
				textAmount.Text="0.00";//So that a declined transaction does not affect account balance
				_listPaySplits.ForEach(x => x.SplitAmt=0);
				textTotal.Text="0.00";
			}
			if(IsNew) {
				//prevents backdating of initial payment
				if(!Security.IsAuthorized(Permissions.PaymentCreate,PIn.Date(textDate.Text))) {
					return false;
				}
			}
			else {
				//Editing an old entry will already be blocked if the date was too old, and user will not be able to click OK button
				//This catches it if user changed the date to be older. If user has SplitCreatePastLockDate permission and has not changed the date, then
				//it is okay to save the payment.
				if((!Security.IsAuthorized(Permissions.SplitCreatePastLockDate,true)
					|| _paymentOld.PayDate!=PIn.Date(textDate.Text))
					&& !Security.IsAuthorized(Permissions.PaymentEdit,PIn.Date(textDate.Text))) {
					return false;
				}
			}
			bool accountingSynchRequired=false;
			double accountingOldAmt=_paymentCur.PayAmt;
			long accountingNewAcct=-1;//the old acctNum will be retrieved inside the validation code.
			if(textDepositAccount.Visible) {
				accountingNewAcct=-1;//indicates no change
			}
			else if(comboDepositAccount.Visible && comboDepositAccount.Items.Count>0 && comboDepositAccount.SelectedIndex!=-1) {
				accountingNewAcct=_arrayDepositAcctNums[comboDepositAccount.SelectedIndex];
			}
			else {//neither textbox nor combo visible. Or something's wrong with combobox
				accountingNewAcct=0;
			}
			try {
				accountingSynchRequired=Payments.ValidateLinkedEntries(accountingOldAmt,PIn.Double(textAmount.Text),IsNew,
					_paymentCur.PayNum,accountingNewAcct);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);//not able to alter, so must not allow user to continue.
				return false;
			}
			if(_paymentCur.ProcessStatus!=ProcessStat.OfficeProcessed) {
				if(checkProcessed.Checked) {
					_paymentCur.ProcessStatus=ProcessStat.OnlineProcessed;
				}
				else {
					_paymentCur.ProcessStatus=ProcessStat.OnlinePending;
				}
			}
			_paymentCur.PayAmt=PIn.Double(textAmount.Text);//handles blank
			_paymentCur.PayDate=PIn.Date(textDate.Text);
			#region Recurring charge logic
			//User chose to have a recurring payment so we need to know if the card has recurring setup and which month to apply the payment to.
			if(IsNew && checkRecurring.Checked && comboCreditCards.SelectedIndex!=_listCreditCards.Count) {
				//Check if a recurring charge is setup for the selected card.
				if(_listCreditCards[comboCreditCards.SelectedIndex].ChargeAmt==0 
					|| _listCreditCards[comboCreditCards.SelectedIndex].DateStart.Year < 1880) 
				{
					MsgBox.Show(this,"The selected credit card has not been setup for recurring charges.");
					return false;
				}
				//Check if a stop date was set and if that date falls in on today or in the past.
				if(_listCreditCards[comboCreditCards.SelectedIndex].DateStop.Year > 1880
					&& _listCreditCards[comboCreditCards.SelectedIndex].DateStop<=DateTime.Now) 
				{
					MsgBox.Show(this,"This card is no longer accepting recurring charges based on the stop date.");
					return false;
				}
				//Have the user decide what month to apply the recurring charge towards.
				FormCreditRecurringDateChoose formDateChoose=new FormCreditRecurringDateChoose(_listCreditCards[comboCreditCards.SelectedIndex]);
				formDateChoose.ShowDialog();
				if(formDateChoose.DialogResult!=DialogResult.OK) {
					MsgBox.Show(this,"Uncheck the \"Apply to Recurring Charge\" box.");
					return false;
				}
				if(!PrefC.GetBool(PrefName.RecurringChargesUseTransDate)) {
					//This will change the PayDate to work better with the recurring charge automation.  User was notified in previous window.
					_paymentCur.PayDate=formDateChoose.PayDate;
				}
				_paymentCur.RecurringChargeDate=formDateChoose.PayDate;
			}
			else if(IsNew && checkRecurring.Checked && comboCreditCards.SelectedIndex==_listCreditCards.Count) {
				MsgBox.Show(this,"Cannot apply a recurring charge to a new card.");
				return false;
			}
			#endregion
			_paymentCur.CheckNum=textCheckNum.Text;
			_paymentCur.BankBranch=textBankBranch.Text;
			_paymentCur.PayNote=textNote.Text;
			_paymentCur.IsRecurringCC=checkRecurring.Checked;
			if(checkPayTypeNone.Checked) {
				_paymentCur.PayType=0;
			}
			else {
				_paymentCur.PayType=DefC.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum;
			}
			//PaymentCur.PatNum=PatCur.PatNum;//this is already done before opening this window.
			//PaymentCur.ClinicNum already handled
			if(_listPaySplits.Count==0 
        && (SplitManagerPromptType) PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)!=SplitManagerPromptType.DoNotUse) {
				//The user has no splits and is trying to submit a payment.
				//We need to ask if they want to autosplit the payment to start getting procedures associated to splits.
				if((SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.Prompt 
					&& !MsgBox.Show(this,MsgBoxButtons.YesNo,"Would you like to autosplit the payment to outstanding family balances?"))
				{
					//The PSM is prompted and they clicked no they don't want to autosplit so we will finalize the payment.
					if(!AddOneSplit(true)) {
						return false;
					}
				}
				else {//Either the PSM is forced or they want to use the PSM to autosplit.
					FormPaySplitManage FormPSM=new FormPaySplitManage();
					FormPSM.PaymentAmt=PIn.Decimal(textAmount.Text);
					FormPSM.FamCur=Patients.GetFamily(_patCur.PatNum);
					FormPSM.PatCur=_patCur;
					FormPSM.PaymentCur=_paymentCur.Clone();
					FormPSM.PayDate=PIn.DateT(textDate.Text);
					FormPSM.IsNew=IsNew;
					FormPSM.ListSplitsCur=new List<PaySplit>();
					if(FormPSM.ShowDialog()==DialogResult.OK) {
						_listPaySplits=FormPSM.ListSplitsCur;
						_paymentCur.PayAmt=(double)FormPSM.AmtTotal;
						if(_listPaySplits.Count==0) {//If they clicked OK without any splits being added, add one split.
              if((SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.Force
                ||(SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.ForceProc) 
              {//If PSM is forced, don't allow no splits.
                return false;
              }
							if(_paymentCur.PayAmt==0) {//No splits, no amount.
								return false;
							}
							if(!AddOneSplit(true)) {
								return false;
							}
						}
					}
					else {//Cancel
            if((SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.Force
              ||(SplitManagerPromptType)PrefC.GetInt(PrefName.PaymentsPromptForAutoSplit)==SplitManagerPromptType.ForceProc) 
            {
              return false;
            }
						if(!AddOneSplit(true)) {
							return false;
						}
					}
				}
			}
			else {
				if(_listPaySplits.Count==0) {//Existing payment with no splits.
					if(!_isCCDeclined
						&& Payments.AllocationRequired(_paymentCur.PayAmt,_paymentCur.PatNum)
						&& _famCur.ListPats.Length>1 //Has other family members
						&& MsgBox.Show(this,MsgBoxButtons.YesNo,"Apply part of payment to other family members?"))
					{
						_listPaySplits=Payments.Allocate(_paymentCur);//PayAmt needs to be set first
					}
					else {//Either no allocation required, or user does not want to allocate.  Just add one split.
						if(!AddOneSplit(true)) {
							return false;
						}
					}
				}
				else {//A new or existing payment with splits.
					if(_listPaySplits.Count==1//if one split
						&& _listPaySplits[0].PayPlanNum!=0//and split is on a payment plan
						&& PIn.Double(textAmount.Text) != _listPaySplits[0].SplitAmt)//and amount doesn't match payment
					{
						_listPaySplits[0].SplitAmt=PIn.Double(textAmount.Text);//make amounts match automatically
						textTotal.Text=textAmount.Text;
					}
					if(_paymentCur.PayAmt!=PIn.Double(textTotal.Text)) {
						MsgBox.Show(this,"Split totals must equal payment amount.");
						//work on reallocation schemes here later
						return false;
					}
				}
			}
			if(_listPaySplits.Count>1) {
				_paymentCur.IsSplit=true;
			}
			else {
				_paymentCur.IsSplit=false;
			}
			try {
				Payments.Update(_paymentCur,true);
			}
			catch(ApplicationException ex) {//this catches bad dates.
				MessageBox.Show(ex.Message);
				return false;
			}
			//Set all DatePays the same.
			for(int i=0;i<_listPaySplits.Count;i++) {
				_listPaySplits[i].DatePay=_paymentCur.PayDate;
			}
			PaySplits.Sync(_listPaySplits,_listPaySplitsOld);
			//Accounting synch is done here.  All validation was done further up
			//If user is trying to change the amount or linked account of an entry that was already copied and linked to accounting section
			if(accountingSynchRequired) {
				Payments.AlterLinkedEntries(accountingOldAmt,_paymentCur.PayAmt,IsNew,_paymentCur.PayNum,accountingNewAcct,_paymentCur.PayDate,
					_famCur.GetNameInFamFL(_paymentCur.PatNum));
			}
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,_paymentCur.PatNum,Patients.GetLim(_paymentCur.PatNum).GetNameLF()+", "
					+_paymentCur.PayAmt.ToString("c"));
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,_paymentCur.PatNum,Patients.GetLim(_paymentCur.PatNum).GetNameLF()+", "
					+_paymentCur.PayAmt.ToString("c"));
			}
			return true;
		}

		private void UpdateLabelPayPlan() {
			if(_listPaySplits == null || _listPaySplits.Count == 0) {
				labelPayPlan.Visible=false;
				return;
			}
			int numPayPlanSplits=_listPaySplits.Where(x => x.PayPlanNum != 0).ToList().Count;
			int numSplitsTotal=_listPaySplits.Count;
			if(numPayPlanSplits==0) {
				labelPayPlan.Visible=false;
				return;
			}
			labelPayPlan.Visible=true;
			labelPayPlan.Text = numPayPlanSplits + " / " + numSplitsTotal + Lan.g(this," splits attached to payment plan.");
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			if(!SavePaymentToDb()) {
				return;
			}
			DialogResult=DialogResult.OK;
			Plugins.HookAddCode(this,"FormPayment.butOK_Click_end",_paymentCur,_listPaySplits);
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
		private void FormPayment_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(!IsNew && !_wasCreditCardSuccessful) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!_wasCreditCardSuccessful) {//new payment that was not a credit card payment that has already been processed
				Payments.Delete(_paymentCur);
				DialogResult=DialogResult.Cancel;
				return;
			}
			//Successful CC payment
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"This will void the transaction that has just been completed. Are you sure you want to continue?")) {
				e.Cancel=true;//Stop the form from closing
				return;
			}
			DateTime payDateCur=PIn.Date(textDate.Text);
			if(payDateCur==null || payDateCur==DateTime.MinValue) {
				MsgBox.Show(this,"Invalid Payment Date");
				e.Cancel=true;//Stop the form from closing
				return;
			}
			if(!PrefC.GetBool(PrefName.AccountAllowFutureDebits) && payDateCur.Date > DateTime.Today) {
				MsgBox.Show(this,"Payment Date must not be a future date.");
				e.Cancel=true;//Stop the form from closing
				return;
			}
			//Save the credit card transaction as a new payment
			_paymentCur.PayAmt=PIn.Double(textAmount.Text);//handles blank
			_paymentCur.PayDate=payDateCur;
			_paymentCur.CheckNum=textCheckNum.Text;
			_paymentCur.BankBranch=textBankBranch.Text;
			_paymentCur.IsRecurringCC=false;
			_paymentCur.PayNote=textNote.Text;
			if(checkPayTypeNone.Checked) {
				_paymentCur.PayType=0;
			}
			else {
				_paymentCur.PayType=DefC.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum;
			}
			if(_listPaySplits.Count==0) {
				AddOneSplit();
				FillMain();
			}
			else if(_listPaySplits.Count==1//if one split
				&& _listPaySplits[0].PayPlanNum!=0//and split is on a payment plan
				&& _listPaySplits[0].SplitAmt!=_paymentCur.PayAmt)//and amount doesn't match payment
			{
				_listPaySplits[0].SplitAmt=_paymentCur.PayAmt;//make amounts match automatically
				textTotal.Text=textAmount.Text;
			}
			if(_paymentCur.PayAmt!=PIn.Double(textTotal.Text)) {
				MsgBox.Show(this,"Split totals must equal payment amount.");
				DialogResult=DialogResult.None;
				return;
			}
			if(_listPaySplits.Count>1) {
				_paymentCur.IsSplit=true;
			}
			else {
				_paymentCur.IsSplit=false;
			}
			try {
				Payments.Update(_paymentCur,true);
			}
			catch(ApplicationException ex) {//this catches bad dates.
				MessageBox.Show(ex.Message);
				e.Cancel=true;
				return;
			}
			//Set all DatePays the same.
			for(int i=0;i<_listPaySplits.Count;i++) {
				_listPaySplits[i].DatePay=_paymentCur.PayDate;
			}
			PaySplits.Sync(_listPaySplits,_listPaySplitsOld);
			if(IsNew) {
				SecurityLogs.MakeLogEntry(Permissions.PaymentCreate,_paymentCur.PatNum,Patients.GetLim(_paymentCur.PatNum).GetNameLF()+", "+
					_paymentCur.PayAmt.ToString("c"));
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.PaymentEdit,_paymentCur.PatNum,Patients.GetLim(_paymentCur.PatNum).GetNameLF()+", "+
					_paymentCur.PayAmt.ToString("c"));
			}
			string refNum="";
			string amount="";
			string transactionID="";
			bool isDebit=false;
			string[] arrayTrans=textNote.Text.Replace("\r\n","\n").Replace("\r","\n").Split(new string[] { "\n" },StringSplitOptions.RemoveEmptyEntries);
			for(int i=0;i<arrayTrans.Length;i++) {
				if(arrayTrans[i].StartsWith("Amount: ")) {
					amount=arrayTrans[i].Substring(8);
				}
				if(arrayTrans[i].StartsWith("Ref Number: ")) {
					refNum=arrayTrans[i].Substring(12);
				}
				if(arrayTrans[i].StartsWith("XCTRANSACTIONID=")) {
					transactionID=arrayTrans[i].Substring(16);
				}
				if(arrayTrans[i].StartsWith("APPROVEDAMOUNT=")) {
					amount=arrayTrans[i].Substring(15);
				}
				if(arrayTrans[i].StartsWith("TYPE=") && arrayTrans[i].Substring(5)=="Debit Purchase") {
					isDebit=true;
				}
			}
			if(refNum!="") {//Void the PayConnect transaction if there is one
				VoidPayConnectTransaction(refNum,amount);
			}
			else if(transactionID!="" && HasXCharge()) {//Void the X-Charge transaction if there is one
				VoidXChargeTransaction(transactionID,amount,isDebit);
			}
			else {
				MsgBox.Show(this,"Unable to void transaction");
			}
			DialogResult=DialogResult.Cancel;
		}
	}
}
