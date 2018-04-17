using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using OpenDentBusiness;
using System.Collections;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormRpOutstandingIns:ODForm {
		private ODGrid gridMain;
		private CheckBox checkPreauth;
		private Label labelProv;
		private ValidNum textDaysOldMin;
		private Label labelDaysOldMin;
		private UI.Button butCancel;
		private ValidNum textDaysOldMax;
		private Label labelDaysOldMax;
    private DateTime dateMin;
    private DateTime dateMax;
    private List<long> provNumList;
    private bool isAllProv;
    private bool isPreauth;
		private DataTable _table;
		private UI.Button butPrint;
		private ComboBoxMulti comboBoxMultiProv;
		private bool headingPrinted;
		private int pagesPrinted;
		private Label label1;
		private Label label2;
		private TextBox textBox1;
		private UI.Button butExport;
		private int headingPrintH;
		private UI.Button butRefresh;
		private ComboBoxMulti comboBoxMultiClinics;
		private Label labelClinic;
		private decimal total;
		private CheckBox checkIgnoreCustom;
		private List<Clinic> _listClinics;
		public TextBox textCarrier;
		private Label labelCarrier;
		private GroupBox groupBox2;
		private UI.Button buttonUpdateCustomTrack;
		private Label labelClaimCount;
		private Label label4;
		private ComboBox comboLastClaimTrack;
		private GroupBox groupBoxUpdateCustomTracking;
		private Label labelCustomTracking;
		private Label label3;
		private ComboBox comboErrorDef;
		private List<Provider> _listProviders;
		private Label labelForUser;
		private UI.Button butPickUser;
		private UI.Button butAssignUser;
		///<summary>List of non-hidden users with ClaimSentEdit permission.</summary>
		private List<Userod> _listClaimSentEditUsers=new List<Userod>();
		private ComboBoxMulti comboUserAssigned;
		private List<ClaimTracking> _listNewClaimTrackings=new List<ClaimTracking>();
		private List<ClaimTracking> _listOldClaimTrackings=new List<ClaimTracking>();
		private UI.Button butMine;
		private CheckBox checkDateOrigSent;
		private ContextMenu rightClickMenu=new ContextMenu();

		public FormRpOutstandingIns() {
			InitializeComponent();
			gridMain.ContextMenu=rightClickMenu;
			Lan.F(this);
		}

		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpOutstandingIns));
			this.checkPreauth = new System.Windows.Forms.CheckBox();
			this.labelProv = new System.Windows.Forms.Label();
			this.labelDaysOldMin = new System.Windows.Forms.Label();
			this.labelDaysOldMax = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.butExport = new OpenDental.UI.Button();
			this.comboBoxMultiProv = new OpenDental.UI.ComboBoxMulti();
			this.butPrint = new OpenDental.UI.Button();
			this.textDaysOldMax = new OpenDental.ValidNum();
			this.textDaysOldMin = new OpenDental.ValidNum();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.comboBoxMultiClinics = new OpenDental.UI.ComboBoxMulti();
			this.labelClinic = new System.Windows.Forms.Label();
			this.checkIgnoreCustom = new System.Windows.Forms.CheckBox();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.labelCarrier = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkDateOrigSent = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboErrorDef = new System.Windows.Forms.ComboBox();
			this.butMine = new OpenDental.UI.Button();
			this.comboUserAssigned = new OpenDental.UI.ComboBoxMulti();
			this.butPickUser = new OpenDental.UI.Button();
			this.labelForUser = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboLastClaimTrack = new System.Windows.Forms.ComboBox();
			this.buttonUpdateCustomTrack = new OpenDental.UI.Button();
			this.labelClaimCount = new System.Windows.Forms.Label();
			this.groupBoxUpdateCustomTracking = new System.Windows.Forms.GroupBox();
			this.labelCustomTracking = new System.Windows.Forms.Label();
			this.butAssignUser = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.groupBoxUpdateCustomTracking.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkPreauth
			// 
			this.checkPreauth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreauth.Checked = true;
			this.checkPreauth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPreauth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPreauth.Location = new System.Drawing.Point(148, 18);
			this.checkPreauth.Name = "checkPreauth";
			this.checkPreauth.Size = new System.Drawing.Size(140, 18);
			this.checkPreauth.TabIndex = 51;
			this.checkPreauth.Text = "Include Preauths";
			this.checkPreauth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreauth.CheckedChanged += new System.EventHandler(this.checkPreauth_CheckedChanged);
			// 
			// labelProv
			// 
			this.labelProv.Location = new System.Drawing.Point(291, 17);
			this.labelProv.Name = "labelProv";
			this.labelProv.Size = new System.Drawing.Size(70, 16);
			this.labelProv.TabIndex = 48;
			this.labelProv.Text = "Treat Provs";
			this.labelProv.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// labelDaysOldMin
			// 
			this.labelDaysOldMin.Location = new System.Drawing.Point(11, 17);
			this.labelDaysOldMin.Name = "labelDaysOldMin";
			this.labelDaysOldMin.Size = new System.Drawing.Size(75, 18);
			this.labelDaysOldMin.TabIndex = 46;
			this.labelDaysOldMin.Text = "Days Old (min)";
			this.labelDaysOldMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDaysOldMax
			// 
			this.labelDaysOldMax.Location = new System.Drawing.Point(46, 38);
			this.labelDaysOldMax.Name = "labelDaysOldMax";
			this.labelDaysOldMax.Size = new System.Drawing.Size(40, 18);
			this.labelDaysOldMax.TabIndex = 46;
			this.labelDaysOldMax.Text = "(max)";
			this.labelDaysOldMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(150, 19);
			this.label1.TabIndex = 54;
			this.label1.Text = "(leave both blank to show all)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(776, 528);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 18);
			this.label2.TabIndex = 46;
			this.label2.Text = "Total";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(851, 527);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(75, 20);
			this.textBox1.TabIndex = 56;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(960, 65);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(82, 24);
			this.butRefresh.TabIndex = 58;
			this.butRefresh.Text = "&Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.CornerRadius = 4F;
			this.butExport.Image = global::OpenDental.Properties.Resources.butExport;
			this.butExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butExport.Location = new System.Drawing.Point(97, 525);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(79, 24);
			this.butExport.TabIndex = 57;
			this.butExport.Text = "&Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// comboBoxMultiProv
			// 
			this.comboBoxMultiProv.ArraySelectedIndices = new int[0];
			this.comboBoxMultiProv.BackColor = System.Drawing.SystemColors.Window;
			this.comboBoxMultiProv.Items = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiProv.Items")));
			this.comboBoxMultiProv.Location = new System.Drawing.Point(362, 16);
			this.comboBoxMultiProv.Name = "comboBoxMultiProv";
			this.comboBoxMultiProv.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiProv.SelectedIndices")));
			this.comboBoxMultiProv.Size = new System.Drawing.Size(160, 21);
			this.comboBoxMultiProv.TabIndex = 53;
			this.comboBoxMultiProv.Leave += new System.EventHandler(this.comboBoxMultiProv_Leave);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(12, 525);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79, 24);
			this.butPrint.TabIndex = 52;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// textDaysOldMax
			// 
			this.textDaysOldMax.Location = new System.Drawing.Point(86, 38);
			this.textDaysOldMax.MaxVal = 50000;
			this.textDaysOldMax.MinVal = 0;
			this.textDaysOldMax.Name = "textDaysOldMax";
			this.textDaysOldMax.Size = new System.Drawing.Size(60, 20);
			this.textDaysOldMax.TabIndex = 47;
			this.textDaysOldMax.TextChanged += new System.EventHandler(this.textDaysOldMax_TextChanged);
			// 
			// textDaysOldMin
			// 
			this.textDaysOldMin.Location = new System.Drawing.Point(86, 17);
			this.textDaysOldMin.MaxVal = 50000;
			this.textDaysOldMin.MinVal = 0;
			this.textDaysOldMin.Name = "textDaysOldMin";
			this.textDaysOldMin.Size = new System.Drawing.Size(60, 20);
			this.textDaysOldMin.TabIndex = 47;
			this.textDaysOldMin.Text = "30";
			this.textDaysOldMin.TextChanged += new System.EventHandler(this.textDaysOldMin_TextChanged);
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
			this.butCancel.Location = new System.Drawing.Point(967, 525);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 45;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.AllowSortingByColumn = true;
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
			this.gridMain.Location = new System.Drawing.Point(12, 95);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(1041, 361);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Claims";
			this.gridMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridMain.TitleHeight = 18;
			this.gridMain.TranslationName = "TableOustandingInsClaims";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// comboBoxMultiClinics
			// 
			this.comboBoxMultiClinics.ArraySelectedIndices = new int[0];
			this.comboBoxMultiClinics.BackColor = System.Drawing.SystemColors.Window;
			this.comboBoxMultiClinics.Items = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiClinics.Items")));
			this.comboBoxMultiClinics.Location = new System.Drawing.Point(362, 38);
			this.comboBoxMultiClinics.Name = "comboBoxMultiClinics";
			this.comboBoxMultiClinics.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboBoxMultiClinics.SelectedIndices")));
			this.comboBoxMultiClinics.Size = new System.Drawing.Size(160, 21);
			this.comboBoxMultiClinics.TabIndex = 59;
			this.comboBoxMultiClinics.Visible = false;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(291, 38);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(70, 16);
			this.labelClinic.TabIndex = 60;
			this.labelClinic.Text = "Clinics";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.labelClinic.Visible = false;
			// 
			// checkIgnoreCustom
			// 
			this.checkIgnoreCustom.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIgnoreCustom.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIgnoreCustom.Location = new System.Drawing.Point(148, 39);
			this.checkIgnoreCustom.Name = "checkIgnoreCustom";
			this.checkIgnoreCustom.Size = new System.Drawing.Size(140, 18);
			this.checkIgnoreCustom.TabIndex = 61;
			this.checkIgnoreCustom.Text = "Ignore Custom Tracking";
			this.checkIgnoreCustom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIgnoreCustom.Click += new System.EventHandler(this.checkIgnoreCustom_Click);
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(736, 38);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(160, 20);
			this.textCarrier.TabIndex = 105;
			// 
			// labelCarrier
			// 
			this.labelCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelCarrier.Location = new System.Drawing.Point(663, 40);
			this.labelCarrier.Name = "labelCarrier";
			this.labelCarrier.Size = new System.Drawing.Size(65, 16);
			this.labelCarrier.TabIndex = 106;
			this.labelCarrier.Text = "Carrier";
			this.labelCarrier.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkDateOrigSent);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.comboErrorDef);
			this.groupBox2.Controls.Add(this.butMine);
			this.groupBox2.Controls.Add(this.comboUserAssigned);
			this.groupBox2.Controls.Add(this.butPickUser);
			this.groupBox2.Controls.Add(this.labelForUser);
			this.groupBox2.Controls.Add(this.textDaysOldMax);
			this.groupBox2.Controls.Add(this.textCarrier);
			this.groupBox2.Controls.Add(this.labelDaysOldMax);
			this.groupBox2.Controls.Add(this.labelCarrier);
			this.groupBox2.Controls.Add(this.textDaysOldMin);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.comboLastClaimTrack);
			this.groupBox2.Controls.Add(this.labelProv);
			this.groupBox2.Controls.Add(this.checkPreauth);
			this.groupBox2.Controls.Add(this.checkIgnoreCustom);
			this.groupBox2.Controls.Add(this.comboBoxMultiProv);
			this.groupBox2.Controls.Add(this.labelClinic);
			this.groupBox2.Controls.Add(this.labelDaysOldMin);
			this.groupBox2.Controls.Add(this.comboBoxMultiClinics);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(12, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(901, 87);
			this.groupBox2.TabIndex = 248;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filters";
			// 
			// checkDateOrigSent
			// 
			this.checkDateOrigSent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDateOrigSent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDateOrigSent.Location = new System.Drawing.Point(170, 60);
			this.checkDateOrigSent.Name = "checkDateOrigSent";
			this.checkDateOrigSent.Size = new System.Drawing.Size(118, 18);
			this.checkDateOrigSent.TabIndex = 112;
			this.checkDateOrigSent.Text = "Use Date Orig Sent";
			this.checkDateOrigSent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkDateOrigSent.Click += new System.EventHandler(this.checkDateOrigSent_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(580, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(155, 16);
			this.label3.TabIndex = 108;
			this.label3.Text = "Last Error Definition";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// comboErrorDef
			// 
			this.comboErrorDef.FormattingEnabled = true;
			this.comboErrorDef.Location = new System.Drawing.Point(736, 59);
			this.comboErrorDef.Name = "comboErrorDef";
			this.comboErrorDef.Size = new System.Drawing.Size(160, 21);
			this.comboErrorDef.TabIndex = 107;
			// 
			// butMine
			// 
			this.butMine.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMine.Autosize = false;
			this.butMine.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMine.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMine.CornerRadius = 2F;
			this.butMine.Location = new System.Drawing.Point(523, 60);
			this.butMine.Name = "butMine";
			this.butMine.Size = new System.Drawing.Size(51, 21);
			this.butMine.TabIndex = 111;
			this.butMine.Text = "Mine";
			this.butMine.Click += new System.EventHandler(this.butMine_Click);
			// 
			// comboUserAssigned
			// 
			this.comboUserAssigned.ArraySelectedIndices = new int[0];
			this.comboUserAssigned.BackColor = System.Drawing.SystemColors.Window;
			this.comboUserAssigned.Items = ((System.Collections.ArrayList)(resources.GetObject("comboUserAssigned.Items")));
			this.comboUserAssigned.Location = new System.Drawing.Point(362, 60);
			this.comboUserAssigned.Name = "comboUserAssigned";
			this.comboUserAssigned.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboUserAssigned.SelectedIndices")));
			this.comboUserAssigned.Size = new System.Drawing.Size(136, 21);
			this.comboUserAssigned.TabIndex = 110;
			// 
			// butPickUser
			// 
			this.butPickUser.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickUser.Autosize = false;
			this.butPickUser.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickUser.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickUser.CornerRadius = 2F;
			this.butPickUser.Location = new System.Drawing.Point(499, 60);
			this.butPickUser.Name = "butPickUser";
			this.butPickUser.Size = new System.Drawing.Size(23, 21);
			this.butPickUser.TabIndex = 109;
			this.butPickUser.Text = "...";
			this.butPickUser.Click += new System.EventHandler(this.butPickUser_Click);
			// 
			// labelForUser
			// 
			this.labelForUser.Location = new System.Drawing.Point(291, 60);
			this.labelForUser.Name = "labelForUser";
			this.labelForUser.Size = new System.Drawing.Size(70, 16);
			this.labelForUser.TabIndex = 108;
			this.labelForUser.Text = "For User";
			this.labelForUser.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(580, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(155, 16);
			this.label4.TabIndex = 63;
			this.label4.Text = "Last Custom Tracking Status";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// comboLastClaimTrack
			// 
			this.comboLastClaimTrack.FormattingEnabled = true;
			this.comboLastClaimTrack.Location = new System.Drawing.Point(736, 16);
			this.comboLastClaimTrack.Name = "comboLastClaimTrack";
			this.comboLastClaimTrack.Size = new System.Drawing.Size(160, 21);
			this.comboLastClaimTrack.TabIndex = 62;
			// 
			// buttonUpdateCustomTrack
			// 
			this.buttonUpdateCustomTrack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonUpdateCustomTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUpdateCustomTrack.Autosize = true;
			this.buttonUpdateCustomTrack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUpdateCustomTrack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUpdateCustomTrack.CornerRadius = 4F;
			this.buttonUpdateCustomTrack.Location = new System.Drawing.Point(78, 46);
			this.buttonUpdateCustomTrack.Name = "buttonUpdateCustomTrack";
			this.buttonUpdateCustomTrack.Size = new System.Drawing.Size(134, 24);
			this.buttonUpdateCustomTrack.TabIndex = 249;
			this.buttonUpdateCustomTrack.Text = "Update Custom Tracking";
			this.buttonUpdateCustomTrack.Click += new System.EventHandler(this.buttonUpdateCustomTrack_Click);
			// 
			// labelClaimCount
			// 
			this.labelClaimCount.Location = new System.Drawing.Point(211, 46);
			this.labelClaimCount.Name = "labelClaimCount";
			this.labelClaimCount.Size = new System.Drawing.Size(60, 24);
			this.labelClaimCount.TabIndex = 250;
			this.labelClaimCount.Text = "claims";
			this.labelClaimCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBoxUpdateCustomTracking
			// 
			this.groupBoxUpdateCustomTracking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxUpdateCustomTracking.Controls.Add(this.labelCustomTracking);
			this.groupBoxUpdateCustomTracking.Controls.Add(this.buttonUpdateCustomTrack);
			this.groupBoxUpdateCustomTracking.Controls.Add(this.labelClaimCount);
			this.groupBoxUpdateCustomTracking.Location = new System.Drawing.Point(352, 462);
			this.groupBoxUpdateCustomTracking.Name = "groupBoxUpdateCustomTracking";
			this.groupBoxUpdateCustomTracking.Size = new System.Drawing.Size(351, 83);
			this.groupBoxUpdateCustomTracking.TabIndex = 254;
			this.groupBoxUpdateCustomTracking.TabStop = false;
			this.groupBoxUpdateCustomTracking.Text = "Custom Tracking";
			// 
			// labelCustomTracking
			// 
			this.labelCustomTracking.Location = new System.Drawing.Point(10, 14);
			this.labelCustomTracking.Name = "labelCustomTracking";
			this.labelCustomTracking.Size = new System.Drawing.Size(337, 24);
			this.labelCustomTracking.TabIndex = 252;
			this.labelCustomTracking.Text = "Clicking Update will change the status of all of the claims in the grid.";
			this.labelCustomTracking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butAssignUser
			// 
			this.butAssignUser.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAssignUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAssignUser.Autosize = true;
			this.butAssignUser.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAssignUser.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAssignUser.CornerRadius = 4F;
			this.butAssignUser.Location = new System.Drawing.Point(12, 462);
			this.butAssignUser.Name = "butAssignUser";
			this.butAssignUser.Size = new System.Drawing.Size(79, 24);
			this.butAssignUser.TabIndex = 110;
			this.butAssignUser.Text = "Assign User";
			this.butAssignUser.Click += new System.EventHandler(this.butAssignUser_Click);
			// 
			// FormRpOutstandingIns
			// 
			this.ClientSize = new System.Drawing.Size(1065, 561);
			this.Controls.Add(this.butAssignUser);
			this.Controls.Add(this.groupBoxUpdateCustomTracking);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butExport);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.gridMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1065, 561);
			this.Name = "FormRpOutstandingIns";
			this.Text = "Outstanding Insurance Claims";
			this.Load += new System.EventHandler(this.FormRpOutIns_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBoxUpdateCustomTracking.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void FormRpOutIns_Load(object sender,EventArgs e) {
			_listProviders=ProviderC.GetListReports();
			FillProvs();
			_listClaimSentEditUsers=Userods.GetUsersByPermission(Permissions.ClaimSentEdit,false);
			FillUsers();
			_listOldClaimTrackings=ClaimTrackings.RefreshForUsers(ClaimTrackingType.ClaimUser,_listClaimSentEditUsers.Select(x => x.UserNum).ToList());
			_listNewClaimTrackings=_listOldClaimTrackings.Select(x => x.Copy()).ToList();
			if(PrefC.HasClinicsEnabled) {
				comboBoxMultiClinics.Visible=true;
				labelClinic.Visible=true;
				FillClinics();
			}
			if(!Security.IsAuthorized(Permissions.UpdateCustomTracking,true)) {
				buttonUpdateCustomTrack.Enabled=false;
			}
			List<MenuItem> listMenuItems=new List<MenuItem>();
			//The first item in the list will always exists, but we toggle it's visibility to only show when 1 row is selected.
			listMenuItems.Add(new MenuItem(Lan.g(this,"Go to Account"),new EventHandler(gridMain_RightClickHelper)));
			listMenuItems[0].Tag=0;//Tags are used to identify what to do in gridMain_RightClickHelper.
			listMenuItems.Add(new MenuItem(Lan.g(this,"Assign to Me"),new EventHandler(gridMain_RightClickHelper)));
			listMenuItems[1].Tag=1;
			listMenuItems.Add(new MenuItem(Lan.g(this,"Assign to User")));
			List<MenuItem> listSubUserMenu=new List<MenuItem>();
			_listClaimSentEditUsers.ForEach(x => { 
				listSubUserMenu.Add(new MenuItem(x.UserName,new EventHandler(gridMain_RightClickHelper)));
				listSubUserMenu[listSubUserMenu.Count-1].Tag=2;
			});
			listMenuItems[2].MenuItems.AddRange(listSubUserMenu.ToArray());
			Menu.MenuItemCollection menuItemCollection=new Menu.MenuItemCollection(rightClickMenu);
			menuItemCollection.AddRange(listMenuItems.ToArray());
			rightClickMenu.Popup+=new EventHandler((o,ea) => {
				rightClickMenu.MenuItems[0].Visible=(gridMain.SelectedIndices.Count()==1);//Only show 'Go to Account' when there is exactly 1 row selected.
			});
			FillCustomTrack();
			FillErrorDef();
			FillGrid(true);
		}

		private void FillProvs() {
			comboBoxMultiProv.Items.Add("All");
			for(int i=0;i<_listProviders.Count;i++) {
				comboBoxMultiProv.Items.Add(_listProviders[i].GetLongDesc());
			}
			comboBoxMultiProv.SetSelected(0,true);
			isAllProv=true;
		}

		private void FillUsers() {
			comboUserAssigned.Items=new ArrayList();
			comboUserAssigned.Items.Add(Lans.g(this,"All"));
			comboUserAssigned.Items.Add(Lans.g(this,"Unassigned"));
			_listClaimSentEditUsers.ForEach(x => comboUserAssigned.Items.Add(x.UserName));
			comboUserAssigned.SetSelected(0,true);//Default to all
		}

		private void FillClinics() {
			comboBoxMultiClinics.Items.Clear();
			List <int> listSelectedItems=new List<int>();
			if(_listClinics==null) {//Not initialized yet
				_listClinics=Clinics.GetForUserod(Security.CurUser);
			}
			comboBoxMultiClinics.Items.Add(Lan.g(this,"All"));
			if(!Security.CurUser.ClinicIsRestricted) {
				comboBoxMultiClinics.Items.Add(Lan.g(this,"Unassigned"));
				listSelectedItems.Add(1);
			}
			for(int i=0;i<_listClinics.Count;i++) {
				int curIndex=comboBoxMultiClinics.Items.Add(_listClinics[i].Abbr);
				if(Clinics.ClinicNum==0) {
					listSelectedItems.Add(curIndex);
				}
				if(_listClinics[i].ClinicNum==Clinics.ClinicNum) {
					listSelectedItems.Clear();
					listSelectedItems.Add(curIndex);
				}
			}
			foreach(int index in listSelectedItems) {
				comboBoxMultiClinics.SetSelected(index,true);
			}
		}

		private void FillCustomTrack() {
			comboLastClaimTrack.Items.Add("All");
			Def[] arrayDefs=DefC.GetList(DefCat.ClaimCustomTracking);
			foreach(Def definition in arrayDefs) {
				comboLastClaimTrack.Items.Add(definition.ItemName);
			}
			comboLastClaimTrack.SelectedIndex=0;
		}

		private void FillErrorDef() {
			List<Def> listErrorDefs = DefC.GetList(DefCat.ClaimErrorCode).ToList();
			ODBoxItem<Def> errorItem=new ODBoxItem<Def>(Lan.g(this,"None"));
			comboErrorDef.Items.Add(errorItem);
			comboErrorDef.SelectedIndex=0;
			foreach(Def errorDef in listErrorDefs) {
				errorItem=new ODBoxItem<Def>(errorDef.ItemName,errorDef);
				comboErrorDef.Items.Add(errorItem);
			}
		}

		private void FillGrid(bool isOnLoad=false) {
			dateMin=DateTime.MinValue;
			dateMax=DateTime.MinValue;
			int daysOldMin=0;
			int daysOldMax=0;
			int.TryParse(textDaysOldMin.Text.Trim(),out daysOldMin);
			int.TryParse(textDaysOldMax.Text.Trim(),out daysOldMax);
			//can't use error provider here because this fires on text changed and cursor may not have left the control, so there is no error message yet
			if(daysOldMin>0 && daysOldMin>=textDaysOldMin.MinVal && daysOldMin<=textDaysOldMin.MaxVal) {
				dateMin=DateTimeOD.Today.AddDays(-1*daysOldMin);
			}
			if(daysOldMax>0 && daysOldMax>=textDaysOldMax.MinVal && daysOldMax<=textDaysOldMax.MaxVal) {
				dateMax=DateTimeOD.Today.AddDays(-1*daysOldMax);
			}
			if(comboBoxMultiProv.SelectedIndices[0].ToString()=="0") {
				isAllProv=true;
			}
			else {
				isAllProv=false;
				provNumList=new List<long>();
				for(int i=0;i<comboBoxMultiProv.SelectedIndices.Count;i++) {
					provNumList.Add((long)_listProviders[(int)comboBoxMultiProv.SelectedIndices[i]-1].ProvNum);
				}
			}
			List<long> listClinicNums=new List<long>();
			if(PrefC.HasClinicsEnabled) {
				if(comboBoxMultiClinics.ListSelectedIndices.Contains(0)) {
					for(int j=0;j<_listClinics.Count;j++) {
						listClinicNums.Add(_listClinics[j].ClinicNum);//Add all clinics this person has access to.
					}
					if(!Security.CurUser.ClinicIsRestricted) {
						listClinicNums.Add(0);
					}
				}
				else {
					for(int i=0;i<comboBoxMultiClinics.ListSelectedIndices.Count;i++) {
						if(Security.CurUser.ClinicIsRestricted) {
							listClinicNums.Add(_listClinics[comboBoxMultiClinics.ListSelectedIndices[i]-1].ClinicNum);
						}
						else if(comboBoxMultiClinics.ListSelectedIndices[i]==1) {
							listClinicNums.Add(0);
						}
						else {
							listClinicNums.Add(_listClinics[comboBoxMultiClinics.ListSelectedIndices[i]-2].ClinicNum);
						}
					}
				}
			}
			isPreauth=checkPreauth.Checked;
			_table=RpOutstandingIns.GetOutInsClaims(isAllProv,provNumList,dateMin,dateMax,isPreauth,listClinicNums,textCarrier.Text,GetSelectedUsers(),checkDateOrigSent.Checked);
			if(isOnLoad) {
				for(int i=0;i<_table.Rows.Count;i++) {
					if(_table.Rows[i]["DefNum"].ToString()!="") {
						//If on load and the results have custom tracking entries, uncheck the "Ignore custom tracking" box so we can show it.
						//If it's not on load don't do this check as the user manually set filters.
						checkIgnoreCustom.Checked=false;
						break;
					}
				}
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			int carrierColWidth=170;
			int dateOfServColWidth=70;
			int patNameColWidth=140;
			if(checkIgnoreCustom.Checked) {
				carrierColWidth=265;
				dateOfServColWidth=75;
				patNameColWidth=240;
			}
			col=new ODGridColumn(Lan.g(this,"Carrier"),carrierColWidth,GridSortingStrategy.StringCompare);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Phone"),98,GridSortingStrategy.StringCompare);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Type"),50,GridSortingStrategy.StringCompare);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"User"),75,GridSortingStrategy.StringCompare);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient Name"),patNameColWidth,GridSortingStrategy.StringCompare);
			gridMain.Columns.Add(col);
			if(PrefC.HasClinicsEnabled) {
				col=new ODGridColumn(Lan.g(this,"Clinic"),90,GridSortingStrategy.StringCompare);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g(this,"Date of Serv"),dateOfServColWidth,GridSortingStrategy.DateParse);
			gridMain.Columns.Add(col);
			if(checkDateOrigSent.Checked) {
				col=new ODGridColumn(Lan.g(this,"Orig Sent"),70,GridSortingStrategy.DateParse);
			}
			else { 
				col=new ODGridColumn(Lan.g(this,"Date Sent"),70,GridSortingStrategy.DateParse);
			}
			gridMain.Columns.Add(col);
			if(!checkIgnoreCustom.Checked) {
				col=new ODGridColumn(Lan.g(this,"TrackStat"),80,GridSortingStrategy.StringCompare);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"DateStat"),55,GridSortingStrategy.DateParse);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Error"),65,GridSortingStrategy.StringCompare);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g(this,"Amount"),75,HorizontalAlignment.Right,GridSortingStrategy.AmountParse);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string type;
			total=0;
			List<Def> listErrorDefs = DefC.GetList(DefCat.ClaimErrorCode).ToList();
			for(int i=0;i<_table.Rows.Count;i++){
				DateTime dateLastLog=PIn.Date(_table.Rows[i]["DateLog"].ToString());
				int daysSuppressed=PIn.Int(_table.Rows[i]["DaysSuppressed"].ToString());
				if(!checkIgnoreCustom.Checked && dateLastLog.AddDays(daysSuppressed)>DateTime.Today) {
					continue;
				}
				long customTrackDefNum=PIn.Long(_table.Rows[i]["DefNum"].ToString()); // should checkIgnoreCustom make this filter get ignored too?
				if(comboLastClaimTrack.SelectedIndex!=0 && customTrackDefNum!=DefC.GetList(DefCat.ClaimCustomTracking)[comboLastClaimTrack.SelectedIndex-1].DefNum) {
					continue;
				}
				if(((ODBoxItem<Def>)comboErrorDef.SelectedItem).Tag != null // should checkIgnoreCustom make this filter get ignored too?
					&& ((ODBoxItem<Def>)comboErrorDef.SelectedItem).Tag.DefNum.ToString() != _table.Rows[i]["TrackingErrorDefNum"].ToString()) {
					continue;
				}
				string userNumString=_table.Rows[i]["UserNum"].ToString();
				long userNum=PIn.Long(userNumString);
				row=new ODGridRow();
				row.Cells.Add(_table.Rows[i]["CarrierName"].ToString());
				row.Cells.Add(_table.Rows[i]["Phone"].ToString());
				type=_table.Rows[i]["ClaimType"].ToString();
				switch(type){
					case "P":
						type="Pri";
						break;
					case "S":
						type="Sec";
						break;
					case "PreAuth":
						type="Preauth";
						break;
					case "Other":
						type="Other";
						break;
					case "Cap":
						type="Cap";
						break;
					case "Med":
						type="Medical";//For possible future use.
						break;
					default:
						type="Error";//Not allowed to be blank.
						break;
				}
				row.Cells.Add(type);
				string userName="";
				if(userNum==0) {
					userName=Lans.g(this,"Unassigned");
				}
				else { 
					Userod person=_listClaimSentEditUsers.FirstOrDefault(x => x.UserNum==userNum);
					if(person!=null) {
						userName=person.UserName;
					}
					else {
						userName=Userods.GetUser(userNum).UserName;
					}
				}
				row.Cells.Add(userName);
				if(PrefC.GetBool(PrefName.ReportsShowPatNum)) {
					row.Cells.Add(_table.Rows[i]["PatNum"].ToString()+"-"+_table.Rows[i]["LName"].ToString()+", "+_table.Rows[i]["FName"].ToString()+" "+_table.Rows[i]["MiddleI"].ToString());
				}
				else {
					row.Cells.Add(_table.Rows[i]["LName"].ToString()+", "+_table.Rows[i]["FName"].ToString()+" "+_table.Rows[i]["MiddleI"].ToString());
				}
				if(PrefC.HasClinicsEnabled) {
					string clinicName="Unassigned";
					for(int j=0;j<_listClinics.Count;j++) {
						if(_listClinics[j].ClinicNum==PIn.Long(_table.Rows[i]["ClinicNum"].ToString())) {
							clinicName=_listClinics[j].Abbr;
							break;
						}
					}
					row.Cells.Add(clinicName);
				}
				DateTime dateService=PIn.Date(_table.Rows[i]["DateService"].ToString());
				if(dateService.Year<1880) {
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(dateService.ToShortDateString());
				}
				if(checkDateOrigSent.Checked) {
					row.Cells.Add(PIn.Date(_table.Rows[i]["DateSentOrig"].ToString()).ToShortDateString());
				}
				else {
					row.Cells.Add(PIn.Date(_table.Rows[i]["DateSent"].ToString()).ToShortDateString());
				}
				if(!checkIgnoreCustom.Checked) {
					if(customTrackDefNum==0) {
						row.Cells.Add("-");
					}
					else {
						row.Cells.Add(DefC.GetList(DefCat.ClaimCustomTracking).First(x => x.DefNum==customTrackDefNum).ItemName);
					}
					DateTime dateLog=PIn.Date(_table.Rows[i]["DateLog"].ToString());
					if(dateLog.Year<1880) {
						row.Cells.Add("-");
					}
					else { 
						row.Cells.Add(dateLog.ToShortDateString());
					}
					Def defCur=listErrorDefs.Where(x => x.DefNum.ToString() == _table.Rows[i]["TrackingErrorDefNum"].ToString()).FirstOrDefault();
					if(defCur!=null) {
						row.Cells.Add(defCur.ItemName);
					}
					else {
						row.Cells.Add("-");
					}
				}
				row.Cells.Add(PIn.Double(_table.Rows[i]["ClaimFee"].ToString()).ToString("c"));
				OutstandingInsClaim outstandingClaim=new OutstandingInsClaim();
				outstandingClaim.ClaimNum=PIn.Long(_table.Rows[i]["ClaimNum"].ToString());
				outstandingClaim.PatNum=PIn.Long(_table.Rows[i]["PatNum"].ToString());
				ClaimTracking claimTrackingCur=_listNewClaimTrackings.FirstOrDefault(x => x.ClaimNum==outstandingClaim.ClaimNum);
				if(claimTrackingCur!=null) {
					outstandingClaim.ClaimTrackingNum=claimTrackingCur.ClaimTrackingNum;
				}
				row.Tag=outstandingClaim;
				gridMain.Rows.Add(row);
				total+=PIn.Decimal(_table.Rows[i]["ClaimFee"].ToString());
			}
			textBox1.Text=total.ToString("c");
			gridMain.EndUpdate();
			labelClaimCount.Text=string.Format("{0} {1}",gridMain.Rows.Count,gridMain.Rows.Count==1?Lan.g(this,"claim"):Lan.g(this,"claims"));
		}

		///<summary>Returns a list of UserNums based on the selections made in comboUserAssigned.
		///List will be empty when 'All' is selected.
		///0 represents 'Unassigned'.</summary>
		private List<long> GetSelectedUsers() {
			List<long> listUserNums=new List<long>();
			if(!comboUserAssigned.ListSelectedIndices.Contains(0)) {//Ignore if 'All' is selected
				comboUserAssigned.ListSelectedIndices.ForEach(x => {
					if(x==1) {//'Unassigned'
						listUserNums.Add(0);
					}
					else {
						listUserNums.Add(_listClaimSentEditUsers[x-2].UserNum);
					}
				});
			}
			return listUserNums;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			Plugins.HookAddCode(this,"FormRpOutstandingIns.butRefresh_begin");
			FillGrid();
		}

		private void textDaysOldMin_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkPreauth_CheckedChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkIgnoreCustom_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkDateOrigSent_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void textDaysOldMax_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void comboBoxMultiProv_Leave(object sender,EventArgs e) {
			if(comboBoxMultiProv.SelectedIndices.Count==0) {
				comboBoxMultiProv.SetSelected(0,true);
			}
			else if(comboBoxMultiProv.SelectedIndices.Contains(0)) {
				comboBoxMultiProv.SelectedIndicesClear();
				comboBoxMultiProv.SetSelected(0,true);
			}
		}

		///<summary>Click method used by all gridMain right click options.
		///We identify what logic to run by the menuItem.Tag.</summary>
		private void gridMain_RightClickHelper(object sender,EventArgs e) {
			int index=gridMain.GetSelectedIndex();
			if(index==-1) {
				return;
			}
			int menuCode=(int)((MenuItem)sender).Tag;
			switch(menuCode) {
				case 0://Go to Account
					GotoModule.GotoAccount(((OutstandingInsClaim)gridMain.Rows[index].Tag).PatNum);
				break;
				case 1://Assign to Me
					AssignUserHelper(Security.CurUser.UserNum);
				break;
				case 2://Assign to User
					AssignUserHelper(_listClaimSentEditUsers[((MenuItem)sender).Index].UserNum);
				break;
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Claim claim=Claims.GetClaim(((OutstandingInsClaim)gridMain.Rows[e.Row].Tag).ClaimNum);
			if(claim==null) {
				MsgBox.Show(this,"The claim has been deleted.");
				FillGrid();
				return;
			}
			Patient pat=Patients.GetPat(claim.PatNum);
			Family fam=Patients.GetFamily(pat.PatNum);
			FormClaimEdit FormCE=new FormClaimEdit(claim,pat,fam);
			FormCE.IsNew=false;
			FormCE.ShowDialog();
		}

		private void buttonUpdateCustomTrack_Click(object sender,EventArgs e) {			
			List<long> listClaimNum=new List<long>();
			for(int i = 0;i<gridMain.Rows.Count;i++) {
				listClaimNum.Add(((OutstandingInsClaim)gridMain.Rows[i].Tag).ClaimNum);
			}
			if(listClaimNum.Count==0) {
				MsgBox.Show(this,"No claims in list. Must have at least one claim.");
				return;
			}
			List<Claim> listClaims=Claims.GetClaimsFromClaimNums(listClaimNum);
			FormClaimCustomTrackingUpdate FormCT=new FormClaimCustomTrackingUpdate(listClaims);
			if(FormCT.ShowDialog()==DialogResult.OK) {
				FillGrid();
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
			#if DEBUG
				FormRpPrintPreview pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=pd;
				pView.ShowDialog();
			#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default,0,"Outstanding insurance report printed")) {
						pd.Print();
					}
			#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void butAssignUser_Click(object sender,EventArgs e) {
			long userNum=PickUser(true);
			if(userNum==-2) {//User canceled selection.
				return;
			}
			AssignUserHelper(userNum);
		}

		///<summary>Loops through gridMain.SelectedIndices to create or update ClaimTracking rows.</summary>
		private void AssignUserHelper(long assignUserNum) {
			foreach(int index in gridMain.SelectedIndices) {
				ClaimTracking claimTracking=new ClaimTracking();
				long claimTrackingNum=((OutstandingInsClaim)gridMain.Rows[index].Tag).ClaimTrackingNum;
				if(claimTrackingNum==0) {//Row is not associated to a ClaimTracking row.
					if(assignUserNum==0) {//User selected 'None', do not create new ClaimTracking row.
						continue;	
					}
					claimTracking.UserNum=assignUserNum;
					claimTracking.ClaimNum=((OutstandingInsClaim)gridMain.Rows[index].Tag).ClaimNum;
					claimTracking.TrackingType=ClaimTrackingType.ClaimUser;
					_listNewClaimTrackings.Add(claimTracking);
				}
				else {
					claimTracking=_listNewClaimTrackings.FirstOrDefault(x => x.ClaimTrackingNum==claimTrackingNum);
					if(assignUserNum==0) {//User selected 'None', we need to remove the ClaimTracking row.
						_listNewClaimTrackings.Remove(claimTracking);
					}
					else {
						claimTracking.UserNum=assignUserNum;
					}
				}
			}
			ClaimTrackings.Sync(_listNewClaimTrackings,_listOldClaimTrackings);
			_listOldClaimTrackings.Clear();//After sync, the old list is updated.
			_listNewClaimTrackings.ForEach(x => _listOldClaimTrackings.Add(x.Copy()));
			FillGrid();
		}

		private void butPickUser_Click(object sender,EventArgs e) {
			long userNum=PickUser(false);
			if(userNum==-2) {//User canceled selection.
				return;
			}
			ComboUserPickHelper(userNum);
		}

		///<summary>After calling PickUser(false) this is used to set comboUserAssigneds selection.
		///Also calls FillGrid() to reflect new selection.</summary>
		private void ComboUserPickHelper(long filterUserNum) {
			int selectedIndex=0;//Defaults to 'All', filterUserNum will be -1 in this case.
			if(filterUserNum==0) {//None or 'Unassigned'
				selectedIndex=1;//'Unassigned'
			}
			else if(filterUserNum!=-1){//Not 'All', this is a specific user selection.
				int index=_listClaimSentEditUsers.FindIndex(x => x.UserNum==filterUserNum);
				if(index==-1) {//Just in case.
					return;
				}
				selectedIndex=index+2;
			}
			comboUserAssigned.SelectedIndicesClear();
			comboUserAssigned.SetSelected(selectedIndex,true);//Offset by 2 for 'All' and 'Unassigned'
			FillGrid();
		}

		///<summary>Opens FormUserPick to allow user to select a user.
		///Returns UserNum associated to selection.
		///0 represents Unassigned
		///-1 represents All
		///-2 represents a canceled selection</summary>
		private long PickUser(bool isAssigning) {
			FormUserPick FormUP=new FormUserPick();
			FormUP.IsSelectionmode=true;
			FormUP.ListUserodsFiltered=_listClaimSentEditUsers;
			if(!isAssigning) {
				FormUP.IsPickAllAllowed=true;
			}
			FormUP.IsPickNoneAllowed=true;
			FormUP.ShowDialog();
			if(FormUP.DialogResult!=DialogResult.OK) {
				return -2;
			}
			return FormUP.SelectedUserNum;
		}
		
		private void butMine_Click(object sender,EventArgs e) {
			FillClinics();
			ComboUserPickHelper(Security.CurUser.UserNum);
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Outstanding Insurance Claims");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				if(isPreauth) {
					text="Including Preauthorization";
				}
				else {
					text="Not Including Preauthorization";
				}
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				if(isAllProv) {
					text="For All Providers";
				}
				else {
					text="For Providers: ";
					for(int i=0;i<provNumList.Count;i++) {
						text+=Providers.GetFormalName(provNumList[i]);
					}
				}
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
				text="Total: $"+total.ToString("F");
				g.DrawString(text,subHeadingFont,Brushes.Black,center+gridMain.Width/2-g.MeasureString(text,subHeadingFont).Width-10,yPos);
			}
			g.Dispose();
		}

		private void butExport_Click(object sender,System.EventArgs e) {
			SaveFileDialog saveFileDialog=new SaveFileDialog();
			saveFileDialog.AddExtension=true;
			saveFileDialog.FileName="Outstanding Insurance Claims";
			if(!Directory.Exists(PrefC.GetString(PrefName.ExportPath))) {
				try {
					Directory.CreateDirectory(PrefC.GetString(PrefName.ExportPath));
					saveFileDialog.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
				}
				catch {
					//initialDirectory will be blank
				}
			}
			else {
				saveFileDialog.InitialDirectory=PrefC.GetString(PrefName.ExportPath);
			}
			saveFileDialog.Filter="Text files(*.txt)|*.txt|Excel Files(*.xls)|*.xls|All files(*.*)|*.*";
			saveFileDialog.FilterIndex=0;
			if(saveFileDialog.ShowDialog()!=DialogResult.OK) {
				return;
			}
			try {
				using(StreamWriter sw=new StreamWriter(saveFileDialog.FileName,false))
				//new FileStream(,FileMode.Create,FileAccess.Write,FileShare.Read)))
				{
					String line="";
					for(int i=0;i<gridMain.Columns.Count;i++) {
						line+=gridMain.Columns[i].Heading+"\t";
					}
					sw.WriteLine(line);
					for(int i=0;i<gridMain.Rows.Count;i++) {
						line="";
						for(int j=0;j<gridMain.Columns.Count;j++) {
							line+=gridMain.Rows[i].Cells[j].Text;
							if(j<gridMain.Columns.Count-1) {
								line+="\t";
							}
						}
						sw.WriteLine(line);
					}
				}
			}
			catch {
				MessageBox.Show(Lan.g(this,"File in use by another program.  Close and try again."));
				return;
			}
			MessageBox.Show(Lan.g(this,"File created successfully"));
		}


		///<summary>Only used in this form to keep track of both the ClaimNum and PatNum within the grid.</summary>
		private class OutstandingInsClaim {
			public long ClaimNum;
			public long PatNum;
			public long ClaimTrackingNum;
		}
	}
}