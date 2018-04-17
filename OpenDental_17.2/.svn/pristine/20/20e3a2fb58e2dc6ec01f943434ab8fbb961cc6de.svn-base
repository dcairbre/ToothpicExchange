using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	///<summary></summary>
	public class FormBillingOptions : ODForm {
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		//private FormQuery FormQuery2;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSaveDefault;
		private OpenDental.ValidDouble textExcludeLessThan;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODGrid gridDun;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.CheckBox checkBadAddress;
		private System.Windows.Forms.CheckBox checkShowNegative;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkIncludeChanged;
		private OpenDental.ValidDate textLastStatement;
		private OpenDental.UI.Button butCreate;
		private CheckBox checkExcludeInsPending;
		private GroupBox groupDateRange;
		private ValidDate textDateStart;
		private Label labelStartDate;
		private Label labelEndDate;
		private ValidDate textDateEnd;
		private OpenDental.UI.Button but45days;
		private OpenDental.UI.Button but90days;
		private OpenDental.UI.Button butDatesAll;
		private CheckBox checkIntermingled;
		private OpenDental.UI.Button butDefaults;
		private OpenDental.UI.Button but30days;
		private ComboBox comboAge;
		private Label label6;
		private Label label7;
		private ListBox listBillType;
		private Label labelSaveDefaults;
		private OpenDental.UI.Button butUndo;
		private CheckBox checkIgnoreInPerson;
		private CheckBox checkExcludeIfProcs;
		private Label labelClinic;
		private ComboBox comboClinic;
		private List<Dunning> _listDunnings;
		public long ClinicNum;
		private CheckBox checkSuperFam;
		private CheckBox checkUseClinicDefaults;
		///<summary>Key: ClinicNum, Value: List of ClinicPrefs for clinic.
		///List contains all existing ClinicPrefs.</summary>
		private Dictionary<long,List<ClinicPref>> _dictClinicPrefsOld;
		///<summary>Key: ClinicNum, Value: List of ClinicPrefs for clinic.
		///Starts off as a copy of _ListClinicPrefsOld, then is modified.</summary>
		private Dictionary<long,List<ClinicPref>> _dictClinicPrefsNew;
		///<summary>When creating list for all clinics, this stores text we show after completed.</summary>
		private string _popUpMessage;
		///<summary>Do not pass a list of clinics in.  This list gets filled on load based on the user logged in.  ListClinics is used in other forms so it is public.</summary>
		private List<Clinic> _listClinics;

		///<summary></summary>
		public FormBillingOptions(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillingOptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butCreate = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butSaveDefault = new OpenDental.UI.Button();
			this.textExcludeLessThan = new OpenDental.ValidDouble();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkUseClinicDefaults = new System.Windows.Forms.CheckBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.checkExcludeIfProcs = new System.Windows.Forms.CheckBox();
			this.checkIgnoreInPerson = new System.Windows.Forms.CheckBox();
			this.labelSaveDefaults = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboAge = new System.Windows.Forms.ComboBox();
			this.checkExcludeInsPending = new System.Windows.Forms.CheckBox();
			this.checkIncludeChanged = new System.Windows.Forms.CheckBox();
			this.textLastStatement = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.checkShowNegative = new System.Windows.Forms.CheckBox();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.gridDun = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.groupDateRange = new System.Windows.Forms.GroupBox();
			this.but30days = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.textDateEnd = new OpenDental.ValidDate();
			this.but45days = new OpenDental.UI.Button();
			this.but90days = new OpenDental.UI.Button();
			this.butDatesAll = new OpenDental.UI.Button();
			this.checkIntermingled = new System.Windows.Forms.CheckBox();
			this.butDefaults = new OpenDental.UI.Button();
			this.butUndo = new OpenDental.UI.Button();
			this.checkSuperFam = new System.Windows.Forms.CheckBox();
			this.groupBox2.SuspendLayout();
			this.groupDateRange.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(806, 651);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79, 24);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butCreate
			// 
			this.butCreate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCreate.Autosize = true;
			this.butCreate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreate.CornerRadius = 4F;
			this.butCreate.Location = new System.Drawing.Point(693, 651);
			this.butCreate.Name = "butCreate";
			this.butCreate.Size = new System.Drawing.Size(92, 24);
			this.butCreate.TabIndex = 8;
			this.butCreate.Text = "Create &List";
			this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(5, 186);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192, 16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Exclude if Balance is less than";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSaveDefault
			// 
			this.butSaveDefault.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSaveDefault.Autosize = true;
			this.butSaveDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveDefault.CornerRadius = 4F;
			this.butSaveDefault.Location = new System.Drawing.Point(169, 578);
			this.butSaveDefault.Name = "butSaveDefault";
			this.butSaveDefault.Size = new System.Drawing.Size(108, 24);
			this.butSaveDefault.TabIndex = 12;
			this.butSaveDefault.Text = "&Save As Default";
			this.butSaveDefault.Click += new System.EventHandler(this.butSaveDefault_Click);
			// 
			// textExcludeLessThan
			// 
			this.textExcludeLessThan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textExcludeLessThan.Location = new System.Drawing.Point(199, 185);
			this.textExcludeLessThan.MaxVal = 100000000D;
			this.textExcludeLessThan.MinVal = -100000000D;
			this.textExcludeLessThan.Name = "textExcludeLessThan";
			this.textExcludeLessThan.Size = new System.Drawing.Size(77, 20);
			this.textExcludeLessThan.TabIndex = 8;
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeInactive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(45, 122);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(231, 18);
			this.checkExcludeInactive.TabIndex = 4;
			this.checkExcludeInactive.Text = "Exclude inactive families";
			this.checkExcludeInactive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkUseClinicDefaults);
			this.groupBox2.Controls.Add(this.labelClinic);
			this.groupBox2.Controls.Add(this.comboClinic);
			this.groupBox2.Controls.Add(this.checkExcludeIfProcs);
			this.groupBox2.Controls.Add(this.checkIgnoreInPerson);
			this.groupBox2.Controls.Add(this.labelSaveDefaults);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.comboAge);
			this.groupBox2.Controls.Add(this.checkExcludeInsPending);
			this.groupBox2.Controls.Add(this.checkIncludeChanged);
			this.groupBox2.Controls.Add(this.textLastStatement);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.checkShowNegative);
			this.groupBox2.Controls.Add(this.checkBadAddress);
			this.groupBox2.Controls.Add(this.checkExcludeInactive);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textExcludeLessThan);
			this.groupBox2.Controls.Add(this.butSaveDefault);
			this.groupBox2.Controls.Add(this.listBillType);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(283, 631);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter";
			// 
			// checkUseClinicDefaults
			// 
			this.checkUseClinicDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkUseClinicDefaults.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkUseClinicDefaults.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUseClinicDefaults.Location = new System.Drawing.Point(45, 514);
			this.checkUseClinicDefaults.Name = "checkUseClinicDefaults";
			this.checkUseClinicDefaults.Size = new System.Drawing.Size(231, 18);
			this.checkUseClinicDefaults.TabIndex = 251;
			this.checkUseClinicDefaults.Text = "Use clinic default billing options";
			this.checkUseClinicDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelClinic
			// 
			this.labelClinic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelClinic.Location = new System.Drawing.Point(3, 487);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(128, 16);
			this.labelClinic.TabIndex = 250;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelClinic.Visible = false;
			// 
			// comboClinic
			// 
			this.comboClinic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.FormattingEnabled = true;
			this.comboClinic.Location = new System.Drawing.Point(132, 486);
			this.comboClinic.MaxDropDownItems = 40;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(145, 21);
			this.comboClinic.TabIndex = 11;
			this.comboClinic.Visible = false;
			this.comboClinic.SelectedIndexChanged += new System.EventHandler(this.comboClinic_SelectedIndexChanged);
			// 
			// checkExcludeIfProcs
			// 
			this.checkExcludeIfProcs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeIfProcs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeIfProcs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeIfProcs.Location = new System.Drawing.Point(45, 162);
			this.checkExcludeIfProcs.Name = "checkExcludeIfProcs";
			this.checkExcludeIfProcs.Size = new System.Drawing.Size(231, 18);
			this.checkExcludeIfProcs.TabIndex = 7;
			this.checkExcludeIfProcs.Text = "Exclude if unsent procedures";
			this.checkExcludeIfProcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIgnoreInPerson
			// 
			this.checkIgnoreInPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkIgnoreInPerson.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIgnoreInPerson.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIgnoreInPerson.Location = new System.Drawing.Point(2, 229);
			this.checkIgnoreInPerson.Name = "checkIgnoreInPerson";
			this.checkIgnoreInPerson.Size = new System.Drawing.Size(274, 18);
			this.checkIgnoreInPerson.TabIndex = 9;
			this.checkIgnoreInPerson.Text = "Ignore walkout (InPerson) statements";
			this.checkIgnoreInPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSaveDefaults
			// 
			this.labelSaveDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSaveDefaults.Location = new System.Drawing.Point(7, 607);
			this.labelSaveDefaults.Name = "labelSaveDefaults";
			this.labelSaveDefaults.Size = new System.Drawing.Size(270, 16);
			this.labelSaveDefaults.TabIndex = 246;
			this.labelSaveDefaults.Text = "(except the date at the top)";
			this.labelSaveDefaults.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(5, 253);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 16);
			this.label7.TabIndex = 245;
			this.label7.Text = "Billing Types";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(3, 75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 16);
			this.label6.TabIndex = 243;
			this.label6.Text = "Age of Account";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAge
			// 
			this.comboAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAge.FormattingEnabled = true;
			this.comboAge.Location = new System.Drawing.Point(132, 73);
			this.comboAge.Name = "comboAge";
			this.comboAge.Size = new System.Drawing.Size(145, 21);
			this.comboAge.TabIndex = 2;
			// 
			// checkExcludeInsPending
			// 
			this.checkExcludeInsPending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkExcludeInsPending.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExcludeInsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInsPending.Location = new System.Drawing.Point(45, 142);
			this.checkExcludeInsPending.Name = "checkExcludeInsPending";
			this.checkExcludeInsPending.Size = new System.Drawing.Size(231, 18);
			this.checkExcludeInsPending.TabIndex = 6;
			this.checkExcludeInsPending.Text = "Exclude if insurance pending";
			this.checkExcludeInsPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIncludeChanged
			// 
			this.checkIncludeChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkIncludeChanged.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeChanged.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeChanged.Location = new System.Drawing.Point(3, 39);
			this.checkIncludeChanged.Name = "checkIncludeChanged";
			this.checkIncludeChanged.Size = new System.Drawing.Size(273, 28);
			this.checkIncludeChanged.TabIndex = 1;
			this.checkIncludeChanged.Text = "Include any accounts with insurance payments, procedures, or payplan charges sinc" +
    "e the last bill";
			this.checkIncludeChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLastStatement
			// 
			this.textLastStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textLastStatement.Location = new System.Drawing.Point(183, 13);
			this.textLastStatement.Name = "textLastStatement";
			this.textLastStatement.Size = new System.Drawing.Size(94, 20);
			this.textLastStatement.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(6, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(176, 16);
			this.label5.TabIndex = 24;
			this.label5.Text = "Include anyone not billed since";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkShowNegative
			// 
			this.checkShowNegative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkShowNegative.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowNegative.Location = new System.Drawing.Point(45, 209);
			this.checkShowNegative.Name = "checkShowNegative";
			this.checkShowNegative.Size = new System.Drawing.Size(231, 18);
			this.checkShowNegative.TabIndex = 5;
			this.checkShowNegative.Text = "Show negative balances (credits)";
			this.checkShowNegative.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBadAddress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(45, 102);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(231, 18);
			this.checkBadAddress.TabIndex = 3;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			this.checkBadAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(118, 253);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158, 225);
			this.listBillType.TabIndex = 10;
			// 
			// gridDun
			// 
			this.gridDun.HasAddButton = false;
			this.gridDun.HasMultilineHeaders = false;
			this.gridDun.HeaderHeight = 15;
			this.gridDun.HScrollVisible = false;
			this.gridDun.Location = new System.Drawing.Point(331, 31);
			this.gridDun.Name = "gridDun";
			this.gridDun.ScrollValue = 0;
			this.gridDun.Size = new System.Drawing.Size(561, 366);
			this.gridDun.TabIndex = 1;
			this.gridDun.Title = "Dunning Messages";
			this.gridDun.TitleHeight = 18;
			this.gridDun.TranslationName = "TableBillingMessages";
			this.gridDun.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDun_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(768, 403);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(124, 24);
			this.butAdd.TabIndex = 2;
			this.butAdd.Text = "Add Dunning Msg";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(328, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(564, 16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Items higher in the list are more general.  Items lower in the list take preceden" +
    "ce .";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(328, 522);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(564, 16);
			this.label4.TabIndex = 26;
			this.label4.Text = "General Message (in addition to any dunning messages and appointment reminders, [" +
    "InstallmentPlanTerms] allowed)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsTab = true;
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.DetectLinksEnabled = false;
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(331, 541);
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(561, 102);
			this.textNote.TabIndex = 7;
			this.textNote.Text = "";
			// 
			// groupDateRange
			// 
			this.groupDateRange.Controls.Add(this.but30days);
			this.groupDateRange.Controls.Add(this.textDateStart);
			this.groupDateRange.Controls.Add(this.labelStartDate);
			this.groupDateRange.Controls.Add(this.labelEndDate);
			this.groupDateRange.Controls.Add(this.textDateEnd);
			this.groupDateRange.Controls.Add(this.but45days);
			this.groupDateRange.Controls.Add(this.but90days);
			this.groupDateRange.Controls.Add(this.butDatesAll);
			this.groupDateRange.Location = new System.Drawing.Point(331, 403);
			this.groupDateRange.Name = "groupDateRange";
			this.groupDateRange.Size = new System.Drawing.Size(319, 69);
			this.groupDateRange.TabIndex = 3;
			this.groupDateRange.TabStop = false;
			this.groupDateRange.Text = "Account History Date Range";
			// 
			// but30days
			// 
			this.but30days.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but30days.Autosize = true;
			this.but30days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but30days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but30days.CornerRadius = 4F;
			this.but30days.Location = new System.Drawing.Point(154, 13);
			this.but30days.Name = "but30days";
			this.but30days.Size = new System.Drawing.Size(77, 24);
			this.but30days.TabIndex = 2;
			this.but30days.Text = "Last 30 Days";
			this.but30days.Click += new System.EventHandler(this.but30days_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.BackColor = System.Drawing.SystemColors.Window;
			this.textDateStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.textDateStart.Location = new System.Drawing.Point(75, 16);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77, 20);
			this.textDateStart.TabIndex = 0;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(6, 19);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.Size = new System.Drawing.Size(69, 14);
			this.labelStartDate.TabIndex = 221;
			this.labelStartDate.Text = "Start Date";
			this.labelStartDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(6, 42);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.Size = new System.Drawing.Size(69, 14);
			this.labelEndDate.TabIndex = 222;
			this.labelEndDate.Text = "End Date";
			this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(75, 39);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77, 20);
			this.textDateEnd.TabIndex = 1;
			// 
			// but45days
			// 
			this.but45days.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but45days.Autosize = true;
			this.but45days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but45days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but45days.CornerRadius = 4F;
			this.but45days.Location = new System.Drawing.Point(154, 37);
			this.but45days.Name = "but45days";
			this.but45days.Size = new System.Drawing.Size(77, 24);
			this.but45days.TabIndex = 3;
			this.but45days.Text = "Last 45 Days";
			this.but45days.Click += new System.EventHandler(this.but45days_Click);
			// 
			// but90days
			// 
			this.but90days.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.but90days.Autosize = true;
			this.but90days.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but90days.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but90days.CornerRadius = 4F;
			this.but90days.Location = new System.Drawing.Point(233, 37);
			this.but90days.Name = "but90days";
			this.but90days.Size = new System.Drawing.Size(77, 24);
			this.but90days.TabIndex = 5;
			this.but90days.Text = "Last 90 Days";
			this.but90days.Click += new System.EventHandler(this.but90days_Click);
			// 
			// butDatesAll
			// 
			this.butDatesAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDatesAll.Autosize = true;
			this.butDatesAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDatesAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDatesAll.CornerRadius = 4F;
			this.butDatesAll.Location = new System.Drawing.Point(233, 13);
			this.butDatesAll.Name = "butDatesAll";
			this.butDatesAll.Size = new System.Drawing.Size(77, 24);
			this.butDatesAll.TabIndex = 4;
			this.butDatesAll.Text = "All Dates";
			this.butDatesAll.Click += new System.EventHandler(this.butDatesAll_Click);
			// 
			// checkIntermingled
			// 
			this.checkIntermingled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIntermingled.Location = new System.Drawing.Point(331, 478);
			this.checkIntermingled.Name = "checkIntermingled";
			this.checkIntermingled.Size = new System.Drawing.Size(319, 20);
			this.checkIntermingled.TabIndex = 5;
			this.checkIntermingled.Text = "Intermingle family members";
			// 
			// butDefaults
			// 
			this.butDefaults.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDefaults.Autosize = true;
			this.butDefaults.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaults.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaults.CornerRadius = 4F;
			this.butDefaults.Location = new System.Drawing.Point(656, 440);
			this.butDefaults.Name = "butDefaults";
			this.butDefaults.Size = new System.Drawing.Size(76, 24);
			this.butDefaults.TabIndex = 4;
			this.butDefaults.Text = "Defaults";
			this.butDefaults.Click += new System.EventHandler(this.butDefaults_Click);
			// 
			// butUndo
			// 
			this.butUndo.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUndo.Autosize = true;
			this.butUndo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUndo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUndo.CornerRadius = 4F;
			this.butUndo.Location = new System.Drawing.Point(7, 651);
			this.butUndo.Name = "butUndo";
			this.butUndo.Size = new System.Drawing.Size(88, 24);
			this.butUndo.TabIndex = 10;
			this.butUndo.Text = "Undo Billing";
			this.butUndo.Click += new System.EventHandler(this.butUndo_Click);
			// 
			// checkSuperFam
			// 
			this.checkSuperFam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSuperFam.Location = new System.Drawing.Point(331, 499);
			this.checkSuperFam.Name = "checkSuperFam";
			this.checkSuperFam.Size = new System.Drawing.Size(319, 20);
			this.checkSuperFam.TabIndex = 6;
			this.checkSuperFam.Text = "Group by Super Family";
			this.checkSuperFam.UseVisualStyleBackColor = true;
			this.checkSuperFam.CheckedChanged += new System.EventHandler(this.checkSuperFam_CheckedChanged);
			// 
			// FormBillingOptions
			// 
			this.AcceptButton = this.butCreate;
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(898, 686);
			this.Controls.Add(this.checkSuperFam);
			this.Controls.Add(this.butUndo);
			this.Controls.Add(this.butDefaults);
			this.Controls.Add(this.checkIntermingled);
			this.Controls.Add(this.groupDateRange);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCreate);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridDun);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBillingOptions";
			this.ShowInTaskbar = false;
			this.Text = "Billing Options";
			this.Load += new System.EventHandler(this.FormBillingOptions_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupDateRange.ResumeLayout(false);
			this.groupDateRange.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBillingOptions_Load(object sender, System.EventArgs e) {
			textLastStatement.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
			checkUseClinicDefaults.Visible=false;
			if(PrefC.HasClinicsEnabled) {
				RefreshClinicPrefs();
				labelSaveDefaults.Text="("+Lan.g(this,"except the date at the top and clinic at the bottom")+")";
				labelClinic.Visible=true;
				comboClinic.Visible=true;
				comboClinic.Items.Add(Lan.g(this,"All"));
				comboClinic.Items.Add(Lan.g(this,"Unassigned"));
				comboClinic.SelectedIndex=0;
				_listClinics=Clinics.GetForUserod(Security.CurUser);
				for(int i = 0;i<_listClinics.Count;i++) {
					comboClinic.Items.Add(_listClinics[i].Abbr);
					if(_listClinics[i].ClinicNum==ClinicNum) {
						comboClinic.SelectedIndex=i+2;
					}
				}
			}
			SetFiltersForClinicNum(GetSelectedClinicNum());
			if(!PrefC.HasSuperStatementsEnabled) {
				checkSuperFam.Visible=false;
			}
			//blank is allowed
			FillDunning();
			SetDefaults();
		}

		///<summary>Call when you want to populate/update _dicClinicPrefsOld and _dicClinicPrefsNew.</summary>
		private void RefreshClinicPrefs() {
			List<PrefName> listBillingPrefs=new List<PrefName>() {
				PrefName.BillingIncludeChanged,
				PrefName.BillingSelectBillingTypes,
				PrefName.BillingAgeOfAccount,
				PrefName.BillingExcludeBadAddresses,
				PrefName.BillingExcludeInactive,
				PrefName.BillingExcludeNegative,
				PrefName.BillingExcludeInsPending,
				PrefName.BillingExcludeIfUnsentProcs,
				PrefName.BillingExcludeLessThan,
				PrefName.BillingIgnoreInPerson
			};
			_dictClinicPrefsOld=ClinicPrefs.List.Where(x => listBillingPrefs.Contains(x.PrefName))
				.GroupBy(x => x.ClinicNum)
				.ToDictionary(x => x.Key,x => x.ToList());
			_dictClinicPrefsNew=_dictClinicPrefsOld.ToDictionary(x => x.Key,x => x.Value.Select(y => y.Clone()).ToList());
		}
		
		///<summary>Called when we need to update the filter options.
		///If All is selected or _dicClinicPrefsNew does not contain a key for the current selected clinic, the standard preference based defaults will load.</summary>
		private void SetFiltersForClinicNum(long clinicNum) {
			if(clinicNum.In(-2,-1,0) //Clinics not enabled or 'All' selected or 'Unassigned' selected. So defaults to standard pref table.
				|| !_dictClinicPrefsNew.ContainsKey(clinicNum))//They have not saved their default filter options for the selected clinic. Use default prefs.
			{
				checkIncludeChanged.Checked=PrefC.GetBool(PrefName.BillingIncludeChanged);
				if(comboAge.Items.Count>0) {//Already initialized.
					comboAge.Items.Clear();
				}
				comboAge.Items.Add(Lan.g(this,"Any Balance"));
				comboAge.Items.Add(Lan.g(this,"Over 30 Days"));
				comboAge.Items.Add(Lan.g(this,"Over 60 Days"));
				comboAge.Items.Add(Lan.g(this,"Over 90 Days"));
				switch(PrefC.GetString(PrefName.BillingAgeOfAccount)){
					default:
						comboAge.SelectedIndex=0;
						break;
					case "30":
						comboAge.SelectedIndex=1;
						break;
					case "60":
						comboAge.SelectedIndex=2;
						break;
					case "90":
						comboAge.SelectedIndex=3;
						break;
				}
				checkBadAddress.Checked=PrefC.GetBool(PrefName.BillingExcludeBadAddresses);
				checkExcludeInactive.Checked=PrefC.GetBool(PrefName.BillingExcludeInactive);
				checkShowNegative.Checked=!PrefC.GetBool(PrefName.BillingExcludeNegative);
				checkExcludeInsPending.Checked=PrefC.GetBool(PrefName.BillingExcludeInsPending);
				checkExcludeIfProcs.Checked=PrefC.GetBool(PrefName.BillingExcludeIfUnsentProcs);
				textExcludeLessThan.Text=PrefC.GetString(PrefName.BillingExcludeLessThan);
				checkIgnoreInPerson.Checked=PrefC.GetBool(PrefName.BillingIgnoreInPerson);
				if(listBillType.Items.Count>0) {//Already initialized.
					listBillType.Items.Clear();
				}
				listBillType.Items.Add(Lan.g(this,"(all)"));
				listBillType.Items.AddRange(DefC.Short[(int)DefCat.BillingTypes].Select(x => x.ItemName).ToArray());
				string[] selectedBillTypes=PrefC.GetString(PrefName.BillingSelectBillingTypes).Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
				foreach(string billTypeDefNum in selectedBillTypes) {
					try{
						int order=DefC.GetOrder(DefCat.BillingTypes,Convert.ToInt64(billTypeDefNum));
						if(order==-1) {
							continue;
						}
						listBillType.SetSelected(order+1,true);
					}
					catch(Exception) {//cannot convert string to int, just continue
						continue;
					}
				}
				if(listBillType.SelectedIndices.Count==0){
					listBillType.SelectedIndex=0;
				}
				return;
			}
			else {//Update filter UI to reflect ClinicPrefs.
				List<ClinicPref> listClinicPrefs=_dictClinicPrefsNew[clinicNum];//By definition of how ClinicPrefs are created, First will always return a result.
				checkIncludeChanged.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingIncludeChanged).ValueString);
				#region BillTypes
				listBillType.ClearSelected();
				string[] selectedBillTypes=listClinicPrefs.First(x => x.PrefName==PrefName.BillingSelectBillingTypes).ValueString.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
				foreach(string billTypeDefNum in selectedBillTypes) {
					try {
						int order=DefC.GetOrder(DefCat.BillingTypes,Convert.ToInt64(billTypeDefNum));
						if(order==-1) {
							continue;
						}
						listBillType.SetSelected(order+1,true);
					}
					catch(Exception) {//cannot convert string to int, just continue
						continue;
					}
				}
				if(listBillType.SelectedIndices.Count==0) {
					listBillType.SelectedIndex=0;
				}
				#endregion
				#region Age
				switch(listClinicPrefs.First(x => x.PrefName==PrefName.BillingAgeOfAccount).ValueString) {
					default:
						comboAge.SelectedIndex=0;
						break;
					case "30":
						comboAge.SelectedIndex=1;
						break;
					case "60":
						comboAge.SelectedIndex=2;
						break;
					case "90":
						comboAge.SelectedIndex=3;
						break;
				}
				#endregion
				checkBadAddress.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeBadAddresses).ValueString);
				checkExcludeInactive.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeInactive).ValueString);
				checkShowNegative.Checked=!PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeNegative).ValueString);
				checkExcludeInsPending.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeInsPending).ValueString);
				checkExcludeIfProcs.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeIfUnsentProcs).ValueString);
				textExcludeLessThan.Text=listClinicPrefs.First(x => x.PrefName==PrefName.BillingExcludeLessThan).ValueString;
				checkIgnoreInPerson.Checked=PIn.Bool(listClinicPrefs.First(x => x.PrefName==PrefName.BillingIgnoreInPerson).ValueString);
			}
		}

		///<summary> If clinics are not enabled, returns -2.
		///Returns the ClinicNum for the selected clinic in comboClinic.
		///If All is selected, returns -1.
		///If Unassigned is selected, returns 0</summary>
		private long GetSelectedClinicNum() {
			if(!PrefC.HasClinicsEnabled) {
				return -2;
			}
			switch(comboClinic.SelectedIndex) {
				case 0://All
					return -1;
				case 1://Unassigned
					return 0;
				default://Individual clinic selected
					return _listClinics[comboClinic.SelectedIndex-2].ClinicNum;
			}
		}

		private void butSaveDefault_Click(object sender,System.EventArgs e) {
			if(textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listBillType.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select at least one billing type first.");
				return;
			}
			string selectedBillingTypes="";//indicates all.
			if(listBillType.SelectedIndices.Count>0 && !listBillType.SelectedIndices.Contains(0)) {
				selectedBillingTypes=string.Join(",",listBillType.SelectedIndices.OfType<int>().Select(x => DefC.Short[(int)DefCat.BillingTypes][x-1].DefNum));
			}
			string ageOfAccount="";
			if(comboAge.SelectedIndex.In(1,2,3)) {
				ageOfAccount=(30*comboAge.SelectedIndex).ToString();//ageOfAccount is 30, 60, or 90
			}
			long clinicNum=GetSelectedClinicNum();
			if(clinicNum.In(-2,-1,0))//Clinics not enabled or 'All' selected or 'Unassigned' selected.
			{
				if(Prefs.UpdateBool(PrefName.BillingIncludeChanged,checkIncludeChanged.Checked)
					|Prefs.UpdateString(PrefName.BillingSelectBillingTypes,selectedBillingTypes)
					|Prefs.UpdateString(PrefName.BillingAgeOfAccount,ageOfAccount)
					|Prefs.UpdateBool(PrefName.BillingExcludeBadAddresses,checkBadAddress.Checked)
					|Prefs.UpdateBool(PrefName.BillingExcludeInactive,checkExcludeInactive.Checked)
					|Prefs.UpdateBool(PrefName.BillingExcludeNegative,!checkShowNegative.Checked)
					|Prefs.UpdateBool(PrefName.BillingExcludeInsPending,checkExcludeInsPending.Checked)
					|Prefs.UpdateBool(PrefName.BillingExcludeIfUnsentProcs,checkExcludeIfProcs.Checked)
					|Prefs.UpdateString(PrefName.BillingExcludeLessThan,textExcludeLessThan.Text)
					|Prefs.UpdateBool(PrefName.BillingIgnoreInPerson,checkIgnoreInPerson.Checked))
				{
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				return;
			}
			else if(_dictClinicPrefsNew.Keys.Contains(clinicNum)) {//ClincPrefs exist, update them.
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingIncludeChanged).ValueString=POut.Bool(checkIncludeChanged.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingSelectBillingTypes).ValueString=selectedBillingTypes;
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingAgeOfAccount).ValueString=ageOfAccount;
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeBadAddresses).ValueString=POut.Bool(checkBadAddress.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeInactive).ValueString=POut.Bool(checkExcludeInactive.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeNegative).ValueString=POut.Bool(!checkShowNegative.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeInsPending).ValueString=POut.Bool(checkExcludeInsPending.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeIfUnsentProcs).ValueString=POut.Bool(checkExcludeIfProcs.Checked);
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingExcludeLessThan).ValueString=textExcludeLessThan.Text;
				_dictClinicPrefsNew[clinicNum].First(x => x.PrefName==PrefName.BillingIgnoreInPerson).ValueString=POut.Bool(checkIgnoreInPerson.Checked);
			}
			else {//No existing ClinicPrefs for the currently selected clinic in comboClinics.
				_dictClinicPrefsNew.Add(clinicNum,new List<ClinicPref>());
				//Inserts new ClinicPrefs for each of the Filter options for the currently selected clinic.
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingIncludeChanged,checkIncludeChanged.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingSelectBillingTypes,selectedBillingTypes));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingAgeOfAccount,ageOfAccount));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeBadAddresses,checkBadAddress.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeInactive,checkExcludeInactive.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeNegative,!checkShowNegative.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeInsPending,checkExcludeInsPending.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeIfUnsentProcs,checkExcludeIfProcs.Checked));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingExcludeLessThan,textExcludeLessThan.Text));
				_dictClinicPrefsNew[clinicNum].Add(new ClinicPref(clinicNum,PrefName.BillingIgnoreInPerson,checkIgnoreInPerson.Checked));
			}
			if(ClinicPrefs.Sync(_dictClinicPrefsNew.SelectMany(x => x.Value).ToList(),_dictClinicPrefsOld.SelectMany(x => x.Value).ToList())) {
				DataValid.SetInvalid(InvalidType.ClinicPrefs);
				RefreshClinicPrefs();
			}
		}

		private void comboClinic_SelectedIndexChanged(object sender,EventArgs e) {
			if(comboClinic.Items.Count==0) {//combo box has no been initialized yet.
				return;
			}
			checkUseClinicDefaults.Visible=(comboClinic.SelectedIndex==0);//Only visible whe 'All' is selected in comboClinic.
			SetFiltersForClinicNum(GetSelectedClinicNum());
		}

		private void FillDunning(){
			_listDunnings=Dunnings.Refresh();
			gridDun.BeginUpdate();
			gridDun.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Billing Type",80);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Aging",70);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Ins",40);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Message",150);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Bold Message",150);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Email",30, HorizontalAlignment.Center);
			gridDun.Columns.Add(col);
			gridDun.Rows.Clear();
			ODGridRow row;
			foreach(Dunning dunnCur in _listDunnings) {
				row=new ODGridRow();
				if(dunnCur.BillingType==0){
					row.Cells.Add(Lan.g(this,"all"));
				}
				else{
					row.Cells.Add(DefC.GetName(DefCat.BillingTypes,dunnCur.BillingType));
				}
				if(dunnCur.AgeAccount==0){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else{
					row.Cells.Add(Lan.g(this,"Over ")+dunnCur.AgeAccount.ToString());
				}
				if(dunnCur.InsIsPending==YN.Yes) {
					row.Cells.Add(Lan.g(this,"Y"));
				}
				else if(dunnCur.InsIsPending==YN.No) {
					row.Cells.Add(Lan.g(this,"N"));
				}
				else {//YN.Unknown
					row.Cells.Add(Lan.g(this,"any"));
				}
				row.Cells.Add(dunnCur.DunMessage);
				row.Cells.Add(new ODGridCell(dunnCur.MessageBold) { Bold=YN.Yes,ColorText=Color.DarkRed });
				if(dunnCur.EmailBody!="" || dunnCur.EmailSubject!="") {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				gridDun.Rows.Add(row);
			}
			gridDun.EndUpdate();
		}

		private void gridDun_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormDunningEdit formD=new FormDunningEdit(_listDunnings[e.Row]);
			formD.ShowDialog();
			FillDunning();
		}

		private void checkSuperFam_CheckedChanged(object sender,EventArgs e) {
			if(checkSuperFam.Checked) {
				checkIntermingled.Checked=false;
				checkIntermingled.Enabled=false;
			}
			else {
				checkIntermingled.Enabled=true;
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Dunning dun=new Dunning();
			FormDunningEdit FormD=new FormDunningEdit(dun);
			FormD.IsNew=true;
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillDunning();
		}

		private void butDefaults_Click(object sender,EventArgs e) {
			FormBillingDefaults FormB=new FormBillingDefaults();
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.OK){
				SetDefaults();
			}
		}

		private void SetDefaults(){
			textDateStart.Text=DateTime.Today.AddDays(-PrefC.GetLong(PrefName.BillingDefaultsLastDays)).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
			checkIntermingled.Checked=PrefC.GetBool(PrefName.BillingDefaultsIntermingle);
			textNote.Text=PrefC.GetString(PrefName.BillingDefaultsNote);
		}

		private void but30days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-30).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void but45days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void but90days_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.AddDays(-90).ToShortDateString();
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void butDatesAll_Click(object sender,EventArgs e) {
			textDateStart.Text="";
			textDateEnd.Text=DateTime.Today.ToShortDateString();
		}

		private void butUndo_Click(object sender,EventArgs e) {
			MsgBox.Show(this,"When the billing list comes up, use the radio button at the top to show the 'Sent' bills.\r\nThen, change their status back to unsent.\r\nYou can edit them as a group using the button at the right.");
			DialogResult=DialogResult.OK;//will trigger ContrStaff to bring up the billing window
		}

		private bool RunAgingEnterprise() {
			DateTime dtNow=MiscData.GetNowDateTime();
			DateTime dtToday=dtNow.Date;
			DateTime dateLastAging=PrefC.GetDate(PrefName.DateLastAging);
			if(dateLastAging.Date==dtToday) {
				return true;//already ran aging for this date, just move on
			}
			Prefs.RefreshCache();
			DateTime dateTAgingBeganPref=PrefC.GetDateT(PrefName.AgingBeginDateTime);
			if(dateTAgingBeganPref>DateTime.MinValue) {
				MessageBox.Show(this,Lan.g(this,"In order to create statments, aging must be calculated, but you cannot run aging until it has finished the "
					+"current calculations which began on")+" "+dateTAgingBeganPref.ToString()+".\r\n"+Lans.g(this,"If you believe the current aging process "
					+"has finished, a user with SecurityAdmin permission can manually clear the date and time by going to Setup | Miscellaneous and pressing "
					+"the 'Clear' button."));
				return false;
			}
			Prefs.UpdateString(PrefName.AgingBeginDateTime,POut.DateT(dtNow,false));//get lock on pref to block others
			Signalods.SetInvalid(InvalidType.Prefs);//signal a cache refresh so other computers will have the updated pref as quickly as possible
			Action actionCloseAgingProgress=null;
			Cursor=Cursors.WaitCursor;
			try {
				actionCloseAgingProgress=ODProgress.ShowProgressStatus("ComputeAging",Lan.g(this,"Calculating enterprise aging for all patients as of")+" "
					+dtToday.ToShortDateString()+"...");
				Ledgers.ComputeAging(0,dtToday);
				Prefs.UpdateString(PrefName.DateLastAging,POut.Date(dtToday,false));
			}
			catch(MySqlException ex) {
				actionCloseAgingProgress?.Invoke();//terminates progress bar
				Cursor=Cursors.Default;
				if(ex==null || ex.Number!=1213) {//not a deadlock error, just throw
					throw;
				}
				MsgBox.Show(this,"Deadlock error detected in aging transaction and rolled back. Try again later.");
				return false;
			}
			finally {
				actionCloseAgingProgress?.Invoke();//terminates progress bar
				Cursor=Cursors.Default;
				Prefs.UpdateString(PrefName.AgingBeginDateTime,"");//clear lock on pref whether aging was successful or not
				Signalods.SetInvalid(InvalidType.Prefs);
			}
			return true;
		}

		private void butCreate_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				|| textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			Action actionCloseAgingProgress=null;
			if(PrefC.GetBool(PrefName.AgingIsEnterprise)) {
				if(!RunAgingEnterprise()) {
					return;
				}
			}
			else if(!PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)) {
				try {
					DateTime asOfDate=(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)?PrefC.GetDate(PrefName.DateLastAging):DateTime.Today);
					actionCloseAgingProgress=ODProgress.ShowProgressStatus("ComputeAging",Lan.g(this,"Calculating aging for all patients as of")+" "
						+asOfDate.ToShortDateString()+"...");
					Cursor=Cursors.WaitCursor;
					Ledgers.RunAging();
				}
				catch(MySqlException ex) {
					actionCloseAgingProgress?.Invoke();//terminates progress bar
					Cursor=Cursors.Default;
					if(ex==null || ex.Number!=1213) {//not a deadlock error, just throw
						throw;
					}
					MsgBox.Show(this,"Deadlock error detected in aging transaction and rolled back. Try again later.");
					return;
				}
				finally {
					actionCloseAgingProgress?.Invoke();//terminates progress bar
					Cursor=Cursors.Default;
				}
			}
			Cursor=Cursors.WaitCursor;
			//All places in the program that have the ability to run aging against the entire database require the Setup permission because it can take a long time.
			if(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)
				&& Security.IsAuthorized(Permissions.Setup,true)
				&& PrefC.GetDate(PrefName.DateLastAging) < DateTime.Today.AddDays(-15)) 
				{
				MsgBox.Show(this,"Last aging date seems old, so you will now be given a chance to update it.  The billing process will continue whether or not aging gets updated.");
				FormAging FormA=new FormAging();
				FormA.BringToFront();
				FormA.ShowDialog();
			}
			long clinicNum=GetSelectedClinicNum();
			if(clinicNum==-1) {//'All' selected
				CreateAllHelper();
			}
			else {
				//At this point one of the following is true:
				// - Clinics not enabled (clinicNum=-2), filters will be set to standard pref table values.
				// - Running for specific clinic.
				//For either case the filters have already been set for each and we will use the current selections.
				CreateHelper(clinicNum);
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}
	
		///<summary>Used to loop through all clinics in comboClinics.</summary>
		private void CreateAllHelper() {
			List<long> listClinicNums=new List<long>();
			listClinicNums.Add(0);//Unassigned
			listClinicNums.AddRange(_listClinics.Select(x => x.ClinicNum));
			_popUpMessage="";
			foreach(long clinicNum in listClinicNums) {
				CreateHelper(clinicNum,checkUseClinicDefaults.Checked,true);
			}
			if(string.IsNullOrEmpty(_popUpMessage)) {
				return;
			}
			MsgBoxCopyPaste msgBox=new MsgBoxCopyPaste(_popUpMessage);
			msgBox.Text=Lan.g(this,"Billing List Results");
			msgBox.ShowDialog();
		}
		
		///<summary></summary>
		private void CreateHelper(long clinicNum, bool useClinicPrefs=false, bool suppressPopup=false) {
			if(useClinicPrefs) {
				SetFiltersForClinicNum(clinicNum);
			}
			else {
				//Maintain current filter selections.
			}
			List<long> listClinicNums=new List<long>();
			if(!clinicNum.In(-2,-1)) {//-2=Clinic not enabled, -1 running for 'All'
				listClinicNums.Add(clinicNum);
			}
			DateTime lastStatement=PIn.Date(textLastStatement.Text);
			if(textLastStatement.Text=="") {
				lastStatement=DateTimeOD.Today;
			}
			string getAge="";
			if(comboAge.SelectedIndex==1) getAge="30";
			else if(comboAge.SelectedIndex==2) getAge="60";
			else if(comboAge.SelectedIndex==3) getAge="90";
			List<long> billingNums=new List<long>();//[listBillType.SelectedIndices.Count];
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){
				if(listBillType.SelectedIndices[i]==0){//if (all) is selected, then ignore any other selections
					billingNums.Clear();
					break;
				}
				billingNums.Add(DefC.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]-1].DefNum);
			}
			List<PatAging> listPatAging=Patients.GetAgingList(getAge,lastStatement,billingNums,checkBadAddress.Checked,
				!checkShowNegative.Checked,PIn.Double(textExcludeLessThan.Text),
				checkExcludeInactive.Checked,checkIncludeChanged.Checked,checkExcludeInsPending.Checked,
				checkExcludeIfProcs.Checked,checkIgnoreInPerson.Checked,listClinicNums,checkSuperFam.Checked);
			List<Patient> listSuperHeadPats=new List<Patient>();
			//If making a super family bill, we need to manipulate the agingList to only contain super family head members.
			//It can also contain regular family members, but should not contain any individual super family members other than the head.
			if(checkSuperFam.Checked) {
				List<PatAging> listSuperAgings=new List<PatAging>();
				for(int i=listPatAging.Count-1;i>=0;i--) {//Go through each PatAging in the retrieved list
					if(listPatAging[i].SuperFamily==0 || !listPatAging[i].HasSuperBilling) {
						continue;//It is okay to leave non-super billing PatAgings in the list.
					}
					Patient superHead=listSuperHeadPats.FirstOrDefault(x => x.PatNum==listPatAging[i].SuperFamily);
					if(superHead==null) {
						superHead=Patients.GetPat(listPatAging[i].SuperFamily);
						listSuperHeadPats.Add(superHead);
					}
					if(!superHead.HasSuperBilling) {
						listPatAging[i].HasSuperBilling=false;//Family guarantor has super billing but superhead doesn't, so no super bill.  Mark statement as no superbill.
						continue;
					}
					//If the guar has super billing enabled and the superhead has superbilling, this entry needs to be merged to superbill.
					if(listPatAging[i].HasSuperBilling && superHead.HasSuperBilling) {
						PatAging patA=listSuperAgings.FirstOrDefault(x => x.PatNum==superHead.PatNum);//Attempt to find an existing PatAging for the superhead.
						if(patA==null) {
							//Create new PatAging object using SuperHead "credentials" but the guarantor's balance information.
							patA=new PatAging();
							patA.AmountDue=listPatAging[i].AmountDue;
							patA.BalTotal=listPatAging[i].BalTotal;
							patA.Bal_0_30=listPatAging[i].Bal_0_30;
							patA.Bal_31_60=listPatAging[i].Bal_31_60;
							patA.Bal_61_90=listPatAging[i].Bal_61_90;
							patA.BalOver90=listPatAging[i].BalOver90;
							patA.InsEst=listPatAging[i].InsEst;
							patA.PatName=superHead.GetNameLF();
							patA.DateLastStatement=listPatAging[i].DateLastStatement;
							patA.BillingType=superHead.BillingType;
							patA.PayPlanDue=listPatAging[i].PayPlanDue;
							patA.PatNum=superHead.PatNum;
							patA.HasSuperBilling=listPatAging[i].HasSuperBilling;//true
							patA.SuperFamily=listPatAging[i].SuperFamily;//Same as superHead.PatNum
							listSuperAgings.Add(patA);
						}
						else {
							//Sum the information together for all guarantors of the superfamily.
							patA.AmountDue+=listPatAging[i].AmountDue;
							patA.BalTotal+=listPatAging[i].BalTotal;
							patA.Bal_0_30+=listPatAging[i].Bal_0_30;
							patA.Bal_31_60+=listPatAging[i].Bal_31_60;
							patA.Bal_61_90+=listPatAging[i].Bal_61_90;
							patA.BalOver90+=listPatAging[i].BalOver90;
							patA.InsEst+=listPatAging[i].InsEst;
							patA.PayPlanDue+=listPatAging[i].PayPlanDue;
						}
						listPatAging.RemoveAt(i);//Remove the individual guarantor statement from the aging list since it's been combined with a superstatement.
					}
				}
				listPatAging.AddRange(listSuperAgings);
			}
			#region Message Construction
			if(PrefC.HasClinicsEnabled) {
				string clinicAbbr;
				switch(clinicNum) {
					case -1://All
						clinicAbbr=Lan.g(this,"All");
						break;
					case 0://Unassigned
						clinicAbbr=Lan.g(this,"Unassigned");
						break;
					default:
						clinicAbbr=_listClinics.First(x => x.ClinicNum==clinicNum).Abbr;
						break;
				}
				_popUpMessage+=Lan.g(this,clinicAbbr)+" - ";
			}
			if(listPatAging.Count==0){
				if(!suppressPopup) {
					MsgBox.Show(this,"List of created bills is empty.");
				}
				else {
					_popUpMessage+=Lan.g(this,"List of created bills is empty.")+"\r\n";
				}
				return;
			}
			else {
				_popUpMessage+=Lan.g(this,"Statements created")+": "+POut.Int(listPatAging.Count)+"\r\n";
			}
			#endregion
			DateTime dateRangeFrom=PIn.Date(textDateStart.Text);
			DateTime dateRangeTo=DateTimeOD.Today;//Needed for payplan accuracy.//new DateTime(2200,1,1);
			if(textDateEnd.Text!=""){
				dateRangeTo=PIn.Date(textDateEnd.Text);
			}
			Statement stmt;
			Dunning dunning;
			List<Dunning> dunList=Dunnings.Refresh();
			DateTime dateAsOf=DateTime.Today;//used to determine when the balance on this date began
			if(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)) {//if aging calculated monthly, use the last aging date instead of today
				dateAsOf=PrefC.GetDate(PrefName.DateLastAging);
			}
			//make lookup dict of key=PatNum, value=DateBalBegan
			Dictionary<long,DateTime> dictPatNumDateBalBegan=Ledgers.GetDateBalanceBegan(listPatAging,dateAsOf,checkSuperFam.Checked).Rows.OfType<DataRow>()
				.ToDictionary(x => PIn.Long(x["PatNum"].ToString()),x => PIn.Date(x["DateAccountAge"].ToString()));
			DateTime dateBalBeganCur;
			foreach(PatAging patAgeCur in listPatAging) {
				stmt=new Statement();
				stmt.DateRangeFrom=dateRangeFrom;
				stmt.DateRangeTo=dateRangeTo;
				stmt.DateSent=DateTimeOD.Today;
				stmt.DocNum=0;
				stmt.HidePayment=false;
				stmt.Intermingled=checkIntermingled.Checked;
				stmt.IsSent=false;
				if(PrefC.GetInt(PrefName.BillingUseElectronic).In(1,2,3,4))
				{
					stmt.Mode_=StatementMode.Electronic;
					stmt.Intermingled=true;
				}
				else {
					stmt.Mode_=StatementMode.Mail;
				}
				Def billingType=DefC.GetDef(DefCat.BillingTypes,patAgeCur.BillingType);
				if(billingType != null && billingType.ItemValue=="E") {
					stmt.Mode_=StatementMode.Email;
				}
				InstallmentPlan installPlan=InstallmentPlans.GetOneForFam(patAgeCur.PatNum);
				stmt.Note=textNote.Text;
				if(installPlan!=null) {
					stmt.Note=textNote.Text.Replace("[InstallmentPlanTerms]","Installment Plan\r\n"
						+"Date First Payment: "+installPlan.DateFirstPayment.ToShortDateString()+"\r\n"
						+"Monthly Payment: "+installPlan.MonthlyPayment.ToString("c")+"\r\n"
						+"APR: "+(installPlan.APR/100).ToString("P")+"\r\n"
						+"Note: "+installPlan.Note);
				}
				//appointment reminders are not handled here since it would be too slow.
				//dateBalBegan is first transaction date for a charge that consumed the last of the credits for the account, so first transaction that isn't
				//fully paid for based on oldest paid first logic
				dateBalBeganCur=DateTime.MinValue;
				dictPatNumDateBalBegan.TryGetValue(patAgeCur.PatNum,out dateBalBeganCur);//dateBalBeganCur will be DateTime.MinValue if PatNum isn't in dict
				int ageAccount=0;
				//ageAccount is number of days between the day the account first started to have a positive bal and the asOf date
				if(dateBalBeganCur>DateTime.MinValue) {
					ageAccount=(dateAsOf-dateBalBeganCur).Days;
				}
				dunning=dunList.LastOrDefault(x => (x.BillingType==0 || x.BillingType==patAgeCur.BillingType) //same billing type
					&& ageAccount>=x.AgeAccount-x.DaysInAdvance //old enough to qualify for this dunning message, taking into account DaysInAdvance
					&& (x.InsIsPending==YN.Unknown || x.InsIsPending==(patAgeCur.InsEst>0?YN.Yes:YN.No)));//dunning msg ins pending=unkown or matches this acct
				if(dunning!=null){
					if(stmt.Note!=""){
						stmt.Note+="\r\n\r\n";//leave one empty line
					}
					stmt.Note+=dunning.DunMessage;
					stmt.NoteBold=dunning.MessageBold;
					stmt.EmailSubject=dunning.EmailSubject;					
					stmt.EmailBody=dunning.EmailBody;
				}
				stmt.PatNum=patAgeCur.PatNum;
				stmt.SinglePatient=false;
				//If this bill is for the superhead and has superbill enabled, it's a superbill.  Flag it as such.
				if(patAgeCur.HasSuperBilling && patAgeCur.PatNum==patAgeCur.SuperFamily && checkSuperFam.Checked) {
					stmt.SuperFamily=patAgeCur.SuperFamily;
				}
				stmt.IsBalValid=true;
				stmt.BalTotal=patAgeCur.BalTotal;
				stmt.InsEst=patAgeCur.InsEst;
				Statements.Insert(stmt);
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}




}
