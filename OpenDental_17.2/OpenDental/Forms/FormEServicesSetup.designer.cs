namespace OpenDental{
	partial class FormEServicesSetup {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEServicesSetup));
			this.label37 = new System.Windows.Forms.Label();
			this.menuWebSchedNewPatApptHostedURLsRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemCopyURL = new System.Windows.Forms.MenuItem();
			this.menuItemNavigateToURL = new System.Windows.Forms.MenuItem();
			this.labelAutoSave = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabSignup = new System.Windows.Forms.TabPage();
			this.webBrowserSignup = new System.Windows.Forms.WebBrowser();
			this.tabEConnector = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textEConnectorListeningType = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.checkAllowEConnectorComm = new System.Windows.Forms.CheckBox();
			this.butSaveListenerPort = new OpenDental.UI.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butInstallEConnector = new OpenDental.UI.Button();
			this.labelListenerServiceAck = new System.Windows.Forms.Label();
			this.butListenerServiceAck = new OpenDental.UI.Button();
			this.label27 = new System.Windows.Forms.Label();
			this.butListenerServiceHistoryRefresh = new OpenDental.UI.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.gridListenerServiceStatusHistory = new OpenDental.UI.ODGrid();
			this.butStartListenerService = new OpenDental.UI.Button();
			this.label24 = new System.Windows.Forms.Label();
			this.labelListenerStatus = new System.Windows.Forms.Label();
			this.butListenerAlertsOff = new OpenDental.UI.Button();
			this.textListenerServiceStatus = new System.Windows.Forms.TextBox();
			this.tabMobileSynch = new System.Windows.Forms.TabPage();
			this.checkTroubleshooting = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.textDateTimeLastRun = new System.Windows.Forms.Label();
			this.groupPreferences = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textMobileUserName = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.butCurrentWorkstation = new OpenDental.UI.Button();
			this.textMobilePassword = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textMobileSynchWorkStation = new System.Windows.Forms.TextBox();
			this.textSynchMinutes = new OpenDental.ValidNumber();
			this.label18 = new System.Windows.Forms.Label();
			this.butSaveMobileSynch = new OpenDental.UI.Button();
			this.textDateBefore = new OpenDental.ValidDate();
			this.labelMobileSynchURL = new System.Windows.Forms.Label();
			this.textMobileSyncServerURL = new System.Windows.Forms.TextBox();
			this.labelMinutesBetweenSynch = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.butFullSync = new OpenDental.UI.Button();
			this.butSync = new OpenDental.UI.Button();
			this.tabMobileWeb = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.butSetupMobileWebUsers = new OpenDental.UI.Button();
			this.label29 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textHostedUrlMobileWeb = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPatientPortal = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBoxNotification = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxNotificationSubject = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxNotificationBody = new System.Windows.Forms.TextBox();
			this.textPatientFacingUrlPortal = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textHostedUrlPortal = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tabWebSched = new System.Windows.Forms.TabPage();
			this.linkLabelAboutWebSched = new System.Windows.Forms.LinkLabel();
			this.labelWebSchedDesc = new System.Windows.Forms.Label();
			this.tabControlWebSched = new System.Windows.Forms.TabControl();
			this.tabWebSchedRecalls = new System.Windows.Forms.TabPage();
			this.groupWebSchedText = new System.Windows.Forms.GroupBox();
			this.labelWebSchedPerBatch = new System.Windows.Forms.Label();
			this.textWebSchedPerBatch = new OpenDental.ValidNumber();
			this.radioDoNotSendText = new System.Windows.Forms.RadioButton();
			this.radioSendText = new System.Windows.Forms.RadioButton();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.listboxWebSchedRecallIgnoreBlockoutTypes = new System.Windows.Forms.ListBox();
			this.label32 = new System.Windows.Forms.Label();
			this.butWebSchedRecallBlockouts = new OpenDental.UI.Button();
			this.groupBoxWebSchedAutomation = new System.Windows.Forms.GroupBox();
			this.radioSendToEmailNoPreferred = new System.Windows.Forms.RadioButton();
			this.radioDoNotSend = new System.Windows.Forms.RadioButton();
			this.radioSendToEmailOnlyPreferred = new System.Windows.Forms.RadioButton();
			this.radioSendToEmail = new System.Windows.Forms.RadioButton();
			this.label21 = new System.Windows.Forms.Label();
			this.groupWebSchedPreview = new System.Windows.Forms.GroupBox();
			this.butWebSchedPickClinic = new OpenDental.UI.Button();
			this.butWebSchedPickProv = new OpenDental.UI.Button();
			this.label22 = new System.Windows.Forms.Label();
			this.comboWebSchedProviders = new System.Windows.Forms.ComboBox();
			this.butWebSchedToday = new OpenDental.UI.Button();
			this.gridWebSchedTimeSlots = new OpenDental.UI.ODGrid();
			this.textWebSchedDateStart = new OpenDental.ValidDate();
			this.labelWebSchedClinic = new System.Windows.Forms.Label();
			this.labelWebSchedRecallTypes = new System.Windows.Forms.Label();
			this.comboWebSchedClinic = new System.Windows.Forms.ComboBox();
			this.comboWebSchedRecallTypes = new System.Windows.Forms.ComboBox();
			this.gridWebSchedOperatories = new OpenDental.UI.ODGrid();
			this.label35 = new System.Windows.Forms.Label();
			this.listBoxWebSchedProviderPref = new System.Windows.Forms.ListBox();
			this.butRecallSchedSetup = new OpenDental.UI.Button();
			this.label31 = new System.Windows.Forms.Label();
			this.gridWebSchedRecallTypes = new OpenDental.UI.ODGrid();
			this.label20 = new System.Windows.Forms.Label();
			this.tabWebSchedNewPatAppts = new System.Windows.Forms.TabPage();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this.textWebSchedNewPatApptMessage = new System.Windows.Forms.TextBox();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.listboxWebSchedNewPatIgnoreBlockoutTypes = new System.Windows.Forms.ListBox();
			this.label33 = new System.Windows.Forms.Label();
			this.butWebSchedNewPatBlockouts = new OpenDental.UI.Button();
			this.gridWebSchedNewPatApptURLs = new OpenDental.UI.ODGrid();
			this.label44 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.textWebSchedNewPatApptLength = new System.Windows.Forms.TextBox();
			this.butWebSchedNewPatApptsRemove = new OpenDental.UI.Button();
			this.butWebSchedNewPatApptsAdd = new OpenDental.UI.Button();
			this.gridWebSchedNewPatApptOps = new OpenDental.UI.ODGrid();
			this.label42 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.butWebSchedNewPatApptsToday = new OpenDental.UI.Button();
			this.gridWebSchedNewPatApptTimeSlots = new OpenDental.UI.ODGrid();
			this.textWebSchedNewPatApptsDateStart = new OpenDental.ValidDate();
			this.gridWebSchedNewPatApptProcs = new OpenDental.UI.ODGrid();
			this.label41 = new System.Windows.Forms.Label();
			this.textWebSchedNewPatApptSearchDays = new OpenDental.ValidNumber();
			this.label40 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.tabTexting = new System.Windows.Forms.TabPage();
			this.butDefaultClinicClear = new OpenDental.UI.Button();
			this.butDefaultClinic = new OpenDental.UI.Button();
			this.butBackMonth = new OpenDental.UI.Button();
			this.dateTimePickerSms = new System.Windows.Forms.DateTimePicker();
			this.gridSmsSummary = new OpenDental.UI.ODGrid();
			this.butFwdMonth = new OpenDental.UI.Button();
			this.butThisMonth = new OpenDental.UI.Button();
			this.tabECR = new System.Windows.Forms.TabPage();
			this.label11 = new System.Windows.Forms.Label();
			this.gridConfStatuses = new OpenDental.UI.ODGrid();
			this.groupAutomationStatuses = new System.Windows.Forms.GroupBox();
			this.comboStatusEFailed = new System.Windows.Forms.ComboBox();
			this.label50 = new System.Windows.Forms.Label();
			this.checkEnableNoClinic = new System.Windows.Forms.CheckBox();
			this.comboStatusEDeclined = new System.Windows.Forms.ComboBox();
			this.comboStatusESent = new System.Windows.Forms.ComboBox();
			this.comboStatusEAccepted = new System.Windows.Forms.ComboBox();
			this.label51 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.textStatusReminders = new System.Windows.Forms.TextBox();
			this.butActivateReminder = new OpenDental.UI.Button();
			this.textStatusConfirmations = new System.Windows.Forms.TextBox();
			this.butActivateConfirm = new OpenDental.UI.Button();
			this.butAddReminderFutureDay = new OpenDental.UI.Button();
			this.checkUseDefaultsEC = new System.Windows.Forms.CheckBox();
			this.checkIsConfirmEnabled = new System.Windows.Forms.CheckBox();
			this.comboClinicEConfirm = new System.Windows.Forms.ComboBox();
			this.label54 = new System.Windows.Forms.Label();
			this.butAddConfirmation = new OpenDental.UI.Button();
			this.butAddReminderSameDay = new OpenDental.UI.Button();
			this.gridRemindersMain = new OpenDental.UI.ODGrid();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.groupNotUsed = new System.Windows.Forms.GroupBox();
			this.butShowOldMobileSych = new OpenDental.UI.Button();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.dateRunEnd = new System.Windows.Forms.DateTimePicker();
			this.label46 = new System.Windows.Forms.Label();
			this.dateRunStart = new System.Windows.Forms.DateTimePicker();
			this.label47 = new System.Windows.Forms.Label();
			this.label48 = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabSignup.SuspendLayout();
			this.tabEConnector.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabMobileSynch.SuspendLayout();
			this.groupPreferences.SuspendLayout();
			this.tabMobileWeb.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPatientPortal.SuspendLayout();
			this.groupBoxNotification.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabWebSched.SuspendLayout();
			this.tabControlWebSched.SuspendLayout();
			this.tabWebSchedRecalls.SuspendLayout();
			this.groupWebSchedText.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBoxWebSchedAutomation.SuspendLayout();
			this.groupWebSchedPreview.SuspendLayout();
			this.tabWebSchedNewPatAppts.SuspendLayout();
			this.groupBox13.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.tabTexting.SuspendLayout();
			this.tabECR.SuspendLayout();
			this.groupAutomationStatuses.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.groupNotUsed.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.SuspendLayout();
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(0, 0);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(100, 23);
			this.label37.TabIndex = 0;
			// 
			// menuWebSchedNewPatApptHostedURLsRightClick
			// 
			this.menuWebSchedNewPatApptHostedURLsRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemCopyURL,
            this.menuItemNavigateToURL});
			// 
			// menuItemCopyURL
			// 
			this.menuItemCopyURL.Index = 0;
			this.menuItemCopyURL.Text = "Copy URL to clipboard";
			this.menuItemCopyURL.Click += new System.EventHandler(this.menuItemCopyURL_Click);
			// 
			// menuItemNavigateToURL
			// 
			this.menuItemNavigateToURL.Index = 1;
			this.menuItemNavigateToURL.Text = "Navigate to the URL on the web browser";
			this.menuItemNavigateToURL.Click += new System.EventHandler(this.menuItemNavigateToURL_Click);
			// 
			// labelAutoSave
			// 
			this.labelAutoSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelAutoSave.Location = new System.Drawing.Point(337, 663);
			this.labelAutoSave.Name = "labelAutoSave";
			this.labelAutoSave.Size = new System.Drawing.Size(754, 20);
			this.labelAutoSave.TabIndex = 501;
			this.labelAutoSave.Text = "(With the exception of the Signup tab, eService settings save on close)";
			this.labelAutoSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label23
			// 
			this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label23.Location = new System.Drawing.Point(13, 9);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(1159, 28);
			this.label23.TabIndex = 244;
			this.label23.Text = "eServices refer to Open Dental features that can be delivered electronically via " +
    "the internet.  All eServices hosted by Open Dental use the eConnector Service.";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(1097, 660);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 23);
			this.butClose.TabIndex = 500;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabSignup);
			this.tabControl.Controls.Add(this.tabEConnector);
			this.tabControl.Controls.Add(this.tabMobileSynch);
			this.tabControl.Controls.Add(this.tabMobileWeb);
			this.tabControl.Controls.Add(this.tabPatientPortal);
			this.tabControl.Controls.Add(this.tabWebSched);
			this.tabControl.Controls.Add(this.tabTexting);
			this.tabControl.Controls.Add(this.tabECR);
			this.tabControl.Controls.Add(this.tabMisc);
			this.tabControl.Location = new System.Drawing.Point(12, 40);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1162, 614);
			this.tabControl.TabIndex = 53;
			this.tabControl.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Deselecting);
			// 
			// tabSignup
			// 
			this.tabSignup.BackColor = System.Drawing.SystemColors.Control;
			this.tabSignup.Controls.Add(this.webBrowserSignup);
			this.tabSignup.Location = new System.Drawing.Point(4, 22);
			this.tabSignup.Name = "tabSignup";
			this.tabSignup.Padding = new System.Windows.Forms.Padding(3);
			this.tabSignup.Size = new System.Drawing.Size(1154, 588);
			this.tabSignup.TabIndex = 9;
			this.tabSignup.Text = "Signup";
			// 
			// webBrowserSignup
			// 
			this.webBrowserSignup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowserSignup.Location = new System.Drawing.Point(3, 3);
			this.webBrowserSignup.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowserSignup.Name = "webBrowserSignup";
			this.webBrowserSignup.Size = new System.Drawing.Size(1148, 582);
			this.webBrowserSignup.TabIndex = 0;
			// 
			// tabEConnector
			// 
			this.tabEConnector.BackColor = System.Drawing.SystemColors.Control;
			this.tabEConnector.Controls.Add(this.groupBox4);
			this.tabEConnector.Controls.Add(this.label25);
			this.tabEConnector.Controls.Add(this.groupBox3);
			this.tabEConnector.Location = new System.Drawing.Point(4, 22);
			this.tabEConnector.Name = "tabEConnector";
			this.tabEConnector.Padding = new System.Windows.Forms.Padding(3);
			this.tabEConnector.Size = new System.Drawing.Size(1154, 588);
			this.tabEConnector.TabIndex = 4;
			this.tabEConnector.Text = "eConnector Service";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox4.Controls.Add(this.textEConnectorListeningType);
			this.groupBox4.Controls.Add(this.label38);
			this.groupBox4.Controls.Add(this.checkAllowEConnectorComm);
			this.groupBox4.Controls.Add(this.butSaveListenerPort);
			this.groupBox4.Location = new System.Drawing.Point(222, 424);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(711, 158);
			this.groupBox4.TabIndex = 252;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "eConnector Service Settings";
			// 
			// textEConnectorListeningType
			// 
			this.textEConnectorListeningType.Location = new System.Drawing.Point(282, 78);
			this.textEConnectorListeningType.Name = "textEConnectorListeningType";
			this.textEConnectorListeningType.ReadOnly = true;
			this.textEConnectorListeningType.Size = new System.Drawing.Size(131, 20);
			this.textEConnectorListeningType.TabIndex = 247;
			// 
			// label38
			// 
			this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label38.Location = new System.Drawing.Point(91, 79);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(185, 17);
			this.label38.TabIndex = 246;
			this.label38.Text = "eConnector Listening Type";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAllowEConnectorComm
			// 
			this.checkAllowEConnectorComm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowEConnectorComm.Location = new System.Drawing.Point(39, 105);
			this.checkAllowEConnectorComm.Name = "checkAllowEConnectorComm";
			this.checkAllowEConnectorComm.Size = new System.Drawing.Size(372, 17);
			this.checkAllowEConnectorComm.TabIndex = 244;
			this.checkAllowEConnectorComm.Text = "Allow eConnector to communicate for eServices";
			this.checkAllowEConnectorComm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkAllowEConnectorComm.UseVisualStyleBackColor = true;
			// 
			// butSaveListenerPort
			// 
			this.butSaveListenerPort.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSaveListenerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSaveListenerPort.Autosize = true;
			this.butSaveListenerPort.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveListenerPort.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveListenerPort.CornerRadius = 4F;
			this.butSaveListenerPort.Location = new System.Drawing.Point(352, 128);
			this.butSaveListenerPort.Name = "butSaveListenerPort";
			this.butSaveListenerPort.Size = new System.Drawing.Size(61, 24);
			this.butSaveListenerPort.TabIndex = 243;
			this.butSaveListenerPort.Text = "Save";
			this.butSaveListenerPort.Click += new System.EventHandler(this.butSaveListenerPort_Click);
			// 
			// label25
			// 
			this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label25.Location = new System.Drawing.Point(111, 8);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(932, 68);
			this.label25.TabIndex = 251;
			this.label25.Text = resources.GetString("label25.Text");
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox3.Controls.Add(this.butInstallEConnector);
			this.groupBox3.Controls.Add(this.labelListenerServiceAck);
			this.groupBox3.Controls.Add(this.butListenerServiceAck);
			this.groupBox3.Controls.Add(this.label27);
			this.groupBox3.Controls.Add(this.butListenerServiceHistoryRefresh);
			this.groupBox3.Controls.Add(this.label26);
			this.groupBox3.Controls.Add(this.gridListenerServiceStatusHistory);
			this.groupBox3.Controls.Add(this.butStartListenerService);
			this.groupBox3.Controls.Add(this.label24);
			this.groupBox3.Controls.Add(this.labelListenerStatus);
			this.groupBox3.Controls.Add(this.butListenerAlertsOff);
			this.groupBox3.Controls.Add(this.textListenerServiceStatus);
			this.groupBox3.Location = new System.Drawing.Point(114, 79);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(929, 339);
			this.groupBox3.TabIndex = 249;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "eConnector Service Monitor";
			// 
			// butInstallEConnector
			// 
			this.butInstallEConnector.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInstallEConnector.Autosize = true;
			this.butInstallEConnector.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInstallEConnector.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInstallEConnector.CornerRadius = 4F;
			this.butInstallEConnector.Location = new System.Drawing.Point(594, 45);
			this.butInstallEConnector.Name = "butInstallEConnector";
			this.butInstallEConnector.Size = new System.Drawing.Size(61, 24);
			this.butInstallEConnector.TabIndex = 255;
			this.butInstallEConnector.Text = "Install";
			this.butInstallEConnector.Click += new System.EventHandler(this.butInstallEConnector_Click);
			// 
			// labelListenerServiceAck
			// 
			this.labelListenerServiceAck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelListenerServiceAck.Location = new System.Drawing.Point(278, 263);
			this.labelListenerServiceAck.Name = "labelListenerServiceAck";
			this.labelListenerServiceAck.Size = new System.Drawing.Size(578, 13);
			this.labelListenerServiceAck.TabIndex = 254;
			this.labelListenerServiceAck.Text = "Acknowledge all errors.  This will stop the eServices menu from showing yellow.";
			this.labelListenerServiceAck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butListenerServiceAck
			// 
			this.butListenerServiceAck.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butListenerServiceAck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butListenerServiceAck.Autosize = true;
			this.butListenerServiceAck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butListenerServiceAck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butListenerServiceAck.CornerRadius = 4F;
			this.butListenerServiceAck.Location = new System.Drawing.Point(862, 257);
			this.butListenerServiceAck.Name = "butListenerServiceAck";
			this.butListenerServiceAck.Size = new System.Drawing.Size(61, 24);
			this.butListenerServiceAck.TabIndex = 253;
			this.butListenerServiceAck.Text = "Ack";
			this.butListenerServiceAck.Click += new System.EventHandler(this.butListenerServiceAck_Click);
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(7, 18);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(916, 19);
			this.label27.TabIndex = 252;
			this.label27.Text = "Open Dental monitors the status of the eConnector Service and alerts all workstat" +
    "ions when status is critical.";
			// 
			// butListenerServiceHistoryRefresh
			// 
			this.butListenerServiceHistoryRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butListenerServiceHistoryRefresh.Autosize = true;
			this.butListenerServiceHistoryRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butListenerServiceHistoryRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butListenerServiceHistoryRefresh.CornerRadius = 4F;
			this.butListenerServiceHistoryRefresh.Location = new System.Drawing.Point(862, 87);
			this.butListenerServiceHistoryRefresh.Name = "butListenerServiceHistoryRefresh";
			this.butListenerServiceHistoryRefresh.Size = new System.Drawing.Size(61, 24);
			this.butListenerServiceHistoryRefresh.TabIndex = 251;
			this.butListenerServiceHistoryRefresh.Text = "Refresh";
			this.butListenerServiceHistoryRefresh.Click += new System.EventHandler(this.butListenerServiceHistoryRefresh_Click);
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(3, 70);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(853, 37);
			this.label26.TabIndex = 250;
			this.label26.Text = resources.GetString("label26.Text");
			this.label26.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridListenerServiceStatusHistory
			// 
			this.gridListenerServiceStatusHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridListenerServiceStatusHistory.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridListenerServiceStatusHistory.HasAddButton = false;
			this.gridListenerServiceStatusHistory.HasDropDowns = false;
			this.gridListenerServiceStatusHistory.HasMultilineHeaders = false;
			this.gridListenerServiceStatusHistory.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridListenerServiceStatusHistory.HeaderHeight = 15;
			this.gridListenerServiceStatusHistory.HScrollVisible = false;
			this.gridListenerServiceStatusHistory.Location = new System.Drawing.Point(6, 117);
			this.gridListenerServiceStatusHistory.Name = "gridListenerServiceStatusHistory";
			this.gridListenerServiceStatusHistory.ScrollValue = 0;
			this.gridListenerServiceStatusHistory.Size = new System.Drawing.Size(917, 138);
			this.gridListenerServiceStatusHistory.TabIndex = 249;
			this.gridListenerServiceStatusHistory.Title = "eConnector History";
			this.gridListenerServiceStatusHistory.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridListenerServiceStatusHistory.TitleHeight = 18;
			this.gridListenerServiceStatusHistory.TranslationName = "FormEServicesSetup";
			this.gridListenerServiceStatusHistory.WrapText = false;
			// 
			// butStartListenerService
			// 
			this.butStartListenerService.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butStartListenerService.Autosize = true;
			this.butStartListenerService.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butStartListenerService.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butStartListenerService.CornerRadius = 4F;
			this.butStartListenerService.Enabled = false;
			this.butStartListenerService.Location = new System.Drawing.Point(527, 45);
			this.butStartListenerService.Name = "butStartListenerService";
			this.butStartListenerService.Size = new System.Drawing.Size(61, 24);
			this.butStartListenerService.TabIndex = 245;
			this.butStartListenerService.Text = "Start";
			this.butStartListenerService.Click += new System.EventHandler(this.butStartListenerService_Click);
			// 
			// label24
			// 
			this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label24.Location = new System.Drawing.Point(115, 306);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(578, 29);
			this.label24.TabIndex = 248;
			this.label24.Text = "Before you stop monitoring, first uninstall the eConnector Service.\r\nMonitoring w" +
    "ill automatically resume when an active eConnector Service has been detected.";
			// 
			// labelListenerStatus
			// 
			this.labelListenerStatus.Location = new System.Drawing.Point(177, 48);
			this.labelListenerStatus.Name = "labelListenerStatus";
			this.labelListenerStatus.Size = new System.Drawing.Size(238, 17);
			this.labelListenerStatus.TabIndex = 244;
			this.labelListenerStatus.Text = "Current eConnector Service Status";
			this.labelListenerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butListenerAlertsOff
			// 
			this.butListenerAlertsOff.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butListenerAlertsOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butListenerAlertsOff.Autosize = true;
			this.butListenerAlertsOff.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butListenerAlertsOff.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butListenerAlertsOff.CornerRadius = 4F;
			this.butListenerAlertsOff.Location = new System.Drawing.Point(9, 307);
			this.butListenerAlertsOff.Name = "butListenerAlertsOff";
			this.butListenerAlertsOff.Size = new System.Drawing.Size(100, 24);
			this.butListenerAlertsOff.TabIndex = 247;
			this.butListenerAlertsOff.Text = "Stop Monitoring";
			this.butListenerAlertsOff.Click += new System.EventHandler(this.butListenerAlertsOff_Click);
			// 
			// textListenerServiceStatus
			// 
			this.textListenerServiceStatus.Location = new System.Drawing.Point(421, 47);
			this.textListenerServiceStatus.Name = "textListenerServiceStatus";
			this.textListenerServiceStatus.ReadOnly = true;
			this.textListenerServiceStatus.Size = new System.Drawing.Size(100, 20);
			this.textListenerServiceStatus.TabIndex = 246;
			// 
			// tabMobileSynch
			// 
			this.tabMobileSynch.BackColor = System.Drawing.SystemColors.Control;
			this.tabMobileSynch.Controls.Add(this.checkTroubleshooting);
			this.tabMobileSynch.Controls.Add(this.butDelete);
			this.tabMobileSynch.Controls.Add(this.textDateTimeLastRun);
			this.tabMobileSynch.Controls.Add(this.groupPreferences);
			this.tabMobileSynch.Controls.Add(this.label19);
			this.tabMobileSynch.Controls.Add(this.butFullSync);
			this.tabMobileSynch.Controls.Add(this.butSync);
			this.tabMobileSynch.Location = new System.Drawing.Point(4, 22);
			this.tabMobileSynch.Name = "tabMobileSynch";
			this.tabMobileSynch.Size = new System.Drawing.Size(1154, 588);
			this.tabMobileSynch.TabIndex = 2;
			this.tabMobileSynch.Text = "Mobile Synch (old-style)";
			// 
			// checkTroubleshooting
			// 
			this.checkTroubleshooting.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.checkTroubleshooting.Location = new System.Drawing.Point(636, 230);
			this.checkTroubleshooting.Name = "checkTroubleshooting";
			this.checkTroubleshooting.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkTroubleshooting.Size = new System.Drawing.Size(184, 24);
			this.checkTroubleshooting.TabIndex = 254;
			this.checkTroubleshooting.Text = "Synch Troubleshooting Mode";
			this.checkTroubleshooting.UseVisualStyleBackColor = true;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(504, 279);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(68, 24);
			this.butDelete.TabIndex = 253;
			this.butDelete.Text = "Delete All";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textDateTimeLastRun
			// 
			this.textDateTimeLastRun.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textDateTimeLastRun.Location = new System.Drawing.Point(505, 230);
			this.textDateTimeLastRun.Name = "textDateTimeLastRun";
			this.textDateTimeLastRun.Size = new System.Drawing.Size(207, 18);
			this.textDateTimeLastRun.TabIndex = 252;
			this.textDateTimeLastRun.Text = "3/4/2011 4:15 PM";
			this.textDateTimeLastRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupPreferences
			// 
			this.groupPreferences.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupPreferences.Controls.Add(this.label13);
			this.groupPreferences.Controls.Add(this.label14);
			this.groupPreferences.Controls.Add(this.textMobileUserName);
			this.groupPreferences.Controls.Add(this.label15);
			this.groupPreferences.Controls.Add(this.butCurrentWorkstation);
			this.groupPreferences.Controls.Add(this.textMobilePassword);
			this.groupPreferences.Controls.Add(this.label16);
			this.groupPreferences.Controls.Add(this.label17);
			this.groupPreferences.Controls.Add(this.textMobileSynchWorkStation);
			this.groupPreferences.Controls.Add(this.textSynchMinutes);
			this.groupPreferences.Controls.Add(this.label18);
			this.groupPreferences.Controls.Add(this.butSaveMobileSynch);
			this.groupPreferences.Controls.Add(this.textDateBefore);
			this.groupPreferences.Controls.Add(this.labelMobileSynchURL);
			this.groupPreferences.Controls.Add(this.textMobileSyncServerURL);
			this.groupPreferences.Controls.Add(this.labelMinutesBetweenSynch);
			this.groupPreferences.Location = new System.Drawing.Point(236, 7);
			this.groupPreferences.Name = "groupPreferences";
			this.groupPreferences.Size = new System.Drawing.Size(682, 212);
			this.groupPreferences.TabIndex = 251;
			this.groupPreferences.TabStop = false;
			this.groupPreferences.Text = "Preferences";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 183);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(575, 19);
			this.label13.TabIndex = 246;
			this.label13.Text = "To change your password, enter a new one in the box and Save.  To keep the old pa" +
    "ssword, leave the box empty.";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(222, 48);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(343, 18);
			this.label14.TabIndex = 244;
			this.label14.Text = "Set to 0 to stop automatic Synchronization";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textMobileUserName
			// 
			this.textMobileUserName.Location = new System.Drawing.Point(177, 131);
			this.textMobileUserName.Name = "textMobileUserName";
			this.textMobileUserName.Size = new System.Drawing.Size(247, 20);
			this.textMobileUserName.TabIndex = 242;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(5, 132);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(169, 19);
			this.label15.TabIndex = 243;
			this.label15.Text = "User Name";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCurrentWorkstation
			// 
			this.butCurrentWorkstation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCurrentWorkstation.Autosize = true;
			this.butCurrentWorkstation.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCurrentWorkstation.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCurrentWorkstation.CornerRadius = 4F;
			this.butCurrentWorkstation.Location = new System.Drawing.Point(430, 101);
			this.butCurrentWorkstation.Name = "butCurrentWorkstation";
			this.butCurrentWorkstation.Size = new System.Drawing.Size(115, 24);
			this.butCurrentWorkstation.TabIndex = 247;
			this.butCurrentWorkstation.Text = "Current Workstation";
			this.butCurrentWorkstation.Click += new System.EventHandler(this.butCurrentWorkstation_Click);
			// 
			// textMobilePassword
			// 
			this.textMobilePassword.Location = new System.Drawing.Point(177, 159);
			this.textMobilePassword.Name = "textMobilePassword";
			this.textMobilePassword.PasswordChar = '*';
			this.textMobilePassword.Size = new System.Drawing.Size(247, 20);
			this.textMobilePassword.TabIndex = 243;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(4, 105);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(170, 18);
			this.label16.TabIndex = 246;
			this.label16.Text = "Workstation for Synching";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(5, 160);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(169, 19);
			this.label17.TabIndex = 244;
			this.label17.Text = "Password";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMobileSynchWorkStation
			// 
			this.textMobileSynchWorkStation.Location = new System.Drawing.Point(177, 103);
			this.textMobileSynchWorkStation.Name = "textMobileSynchWorkStation";
			this.textMobileSynchWorkStation.Size = new System.Drawing.Size(247, 20);
			this.textMobileSynchWorkStation.TabIndex = 245;
			// 
			// textSynchMinutes
			// 
			this.textSynchMinutes.Location = new System.Drawing.Point(177, 47);
			this.textSynchMinutes.MaxVal = 255;
			this.textSynchMinutes.MinVal = 0;
			this.textSynchMinutes.Name = "textSynchMinutes";
			this.textSynchMinutes.Size = new System.Drawing.Size(39, 20);
			this.textSynchMinutes.TabIndex = 241;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(5, 76);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(170, 18);
			this.label18.TabIndex = 85;
			this.label18.Text = "Exclude Appointments Before";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSaveMobileSynch
			// 
			this.butSaveMobileSynch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSaveMobileSynch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSaveMobileSynch.Autosize = true;
			this.butSaveMobileSynch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveMobileSynch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveMobileSynch.CornerRadius = 4F;
			this.butSaveMobileSynch.Location = new System.Drawing.Point(615, 182);
			this.butSaveMobileSynch.Name = "butSaveMobileSynch";
			this.butSaveMobileSynch.Size = new System.Drawing.Size(61, 24);
			this.butSaveMobileSynch.TabIndex = 240;
			this.butSaveMobileSynch.Text = "Save";
			this.butSaveMobileSynch.Click += new System.EventHandler(this.butSaveMobileSynch_Click);
			// 
			// textDateBefore
			// 
			this.textDateBefore.Location = new System.Drawing.Point(177, 75);
			this.textDateBefore.Name = "textDateBefore";
			this.textDateBefore.Size = new System.Drawing.Size(100, 20);
			this.textDateBefore.TabIndex = 84;
			// 
			// labelMobileSynchURL
			// 
			this.labelMobileSynchURL.Location = new System.Drawing.Point(6, 20);
			this.labelMobileSynchURL.Name = "labelMobileSynchURL";
			this.labelMobileSynchURL.Size = new System.Drawing.Size(169, 19);
			this.labelMobileSynchURL.TabIndex = 76;
			this.labelMobileSynchURL.Text = "Host Server Address";
			this.labelMobileSynchURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMobileSyncServerURL
			// 
			this.textMobileSyncServerURL.Location = new System.Drawing.Point(177, 19);
			this.textMobileSyncServerURL.Name = "textMobileSyncServerURL";
			this.textMobileSyncServerURL.Size = new System.Drawing.Size(445, 20);
			this.textMobileSyncServerURL.TabIndex = 75;
			// 
			// labelMinutesBetweenSynch
			// 
			this.labelMinutesBetweenSynch.Location = new System.Drawing.Point(6, 48);
			this.labelMinutesBetweenSynch.Name = "labelMinutesBetweenSynch";
			this.labelMinutesBetweenSynch.Size = new System.Drawing.Size(169, 19);
			this.labelMinutesBetweenSynch.TabIndex = 79;
			this.labelMinutesBetweenSynch.Text = "Minutes Between Synch";
			this.labelMinutesBetweenSynch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label19.Location = new System.Drawing.Point(335, 230);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(167, 18);
			this.label19.TabIndex = 250;
			this.label19.Text = "Date/time of last sync";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butFullSync
			// 
			this.butFullSync.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFullSync.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butFullSync.Autosize = true;
			this.butFullSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFullSync.CornerRadius = 4F;
			this.butFullSync.Location = new System.Drawing.Point(578, 279);
			this.butFullSync.Name = "butFullSync";
			this.butFullSync.Size = new System.Drawing.Size(68, 24);
			this.butFullSync.TabIndex = 249;
			this.butFullSync.Text = "Full Synch";
			this.butFullSync.Click += new System.EventHandler(this.butFullSync_Click);
			// 
			// butSync
			// 
			this.butSync.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSync.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butSync.Autosize = true;
			this.butSync.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSync.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSync.CornerRadius = 4F;
			this.butSync.Location = new System.Drawing.Point(652, 279);
			this.butSync.Name = "butSync";
			this.butSync.Size = new System.Drawing.Size(68, 24);
			this.butSync.TabIndex = 248;
			this.butSync.Text = "Synch";
			this.butSync.Click += new System.EventHandler(this.butSync_Click);
			// 
			// tabMobileWeb
			// 
			this.tabMobileWeb.BackColor = System.Drawing.SystemColors.Control;
			this.tabMobileWeb.Controls.Add(this.groupBox5);
			this.tabMobileWeb.Controls.Add(this.groupBox2);
			this.tabMobileWeb.Location = new System.Drawing.Point(4, 22);
			this.tabMobileWeb.Name = "tabMobileWeb";
			this.tabMobileWeb.Padding = new System.Windows.Forms.Padding(3);
			this.tabMobileWeb.Size = new System.Drawing.Size(1154, 588);
			this.tabMobileWeb.TabIndex = 10;
			this.tabMobileWeb.Text = "Mobile Web";
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox5.Controls.Add(this.butSetupMobileWebUsers);
			this.groupBox5.Controls.Add(this.label29);
			this.groupBox5.Location = new System.Drawing.Point(153, 101);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(848, 97);
			this.groupBox5.TabIndex = 79;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Setup Users";
			// 
			// butSetupMobileWebUsers
			// 
			this.butSetupMobileWebUsers.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetupMobileWebUsers.Autosize = true;
			this.butSetupMobileWebUsers.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetupMobileWebUsers.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetupMobileWebUsers.CornerRadius = 4F;
			this.butSetupMobileWebUsers.Location = new System.Drawing.Point(9, 67);
			this.butSetupMobileWebUsers.Name = "butSetupMobileWebUsers";
			this.butSetupMobileWebUsers.Size = new System.Drawing.Size(220, 24);
			this.butSetupMobileWebUsers.TabIndex = 250;
			this.butSetupMobileWebUsers.Text = "Setup Mobile Web Users";
			this.butSetupMobileWebUsers.Click += new System.EventHandler(this.butSetupMobileWebUsers_Click);
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(6, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(664, 42);
			this.label29.TabIndex = 72;
			this.label29.Text = resources.GetString("label29.Text");
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox2.Controls.Add(this.textHostedUrlMobileWeb);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Location = new System.Drawing.Point(153, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(848, 89);
			this.groupBox2.TabIndex = 78;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Mobile Web";
			// 
			// textHostedUrlMobileWeb
			// 
			this.textHostedUrlMobileWeb.Location = new System.Drawing.Point(144, 61);
			this.textHostedUrlMobileWeb.Name = "textHostedUrlMobileWeb";
			this.textHostedUrlMobileWeb.ReadOnly = true;
			this.textHostedUrlMobileWeb.Size = new System.Drawing.Size(349, 20);
			this.textHostedUrlMobileWeb.TabIndex = 74;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(12, 63);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(126, 17);
			this.label12.TabIndex = 73;
			this.label12.Text = "Hosted URL";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(664, 42);
			this.label5.TabIndex = 72;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// tabPatientPortal
			// 
			this.tabPatientPortal.BackColor = System.Drawing.SystemColors.Control;
			this.tabPatientPortal.Controls.Add(this.label1);
			this.tabPatientPortal.Controls.Add(this.label8);
			this.tabPatientPortal.Controls.Add(this.groupBoxNotification);
			this.tabPatientPortal.Controls.Add(this.textPatientFacingUrlPortal);
			this.tabPatientPortal.Controls.Add(this.groupBox1);
			this.tabPatientPortal.Location = new System.Drawing.Point(4, 22);
			this.tabPatientPortal.Name = "tabPatientPortal";
			this.tabPatientPortal.Padding = new System.Windows.Forms.Padding(3);
			this.tabPatientPortal.Size = new System.Drawing.Size(1154, 588);
			this.tabPatientPortal.TabIndex = 1;
			this.tabPatientPortal.Text = "Patient Portal";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.Location = new System.Drawing.Point(193, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(808, 184);
			this.label1.TabIndex = 51;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label8.Location = new System.Drawing.Point(163, 101);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(129, 17);
			this.label8.TabIndex = 52;
			this.label8.Text = "Patient Facing URL";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBoxNotification
			// 
			this.groupBoxNotification.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBoxNotification.Controls.Add(this.label9);
			this.groupBoxNotification.Controls.Add(this.label7);
			this.groupBoxNotification.Controls.Add(this.textBoxNotificationSubject);
			this.groupBoxNotification.Controls.Add(this.label6);
			this.groupBoxNotification.Controls.Add(this.label4);
			this.groupBoxNotification.Controls.Add(this.textBoxNotificationBody);
			this.groupBoxNotification.Location = new System.Drawing.Point(154, 309);
			this.groupBoxNotification.Name = "groupBoxNotification";
			this.groupBoxNotification.Size = new System.Drawing.Size(847, 240);
			this.groupBoxNotification.TabIndex = 48;
			this.groupBoxNotification.TabStop = false;
			this.groupBoxNotification.Text = "Notification Email";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.Location = new System.Drawing.Point(39, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(791, 53);
			this.label9.TabIndex = 52;
			this.label9.Text = resources.GetString("label9.Text");
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(90, 95);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(573, 17);
			this.label7.TabIndex = 48;
			this.label7.Text = "[URL] will be replaced with the value of \'Patient Facing URL\' as entered above.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxNotificationSubject
			// 
			this.textBoxNotificationSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNotificationSubject.Location = new System.Drawing.Point(93, 72);
			this.textBoxNotificationSubject.Name = "textBoxNotificationSubject";
			this.textBoxNotificationSubject.Size = new System.Drawing.Size(737, 20);
			this.textBoxNotificationSubject.TabIndex = 45;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9, 115);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 17);
			this.label6.TabIndex = 47;
			this.label6.Text = "Body";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 73);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 17);
			this.label4.TabIndex = 44;
			this.label4.Text = "Subject";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxNotificationBody
			// 
			this.textBoxNotificationBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNotificationBody.Location = new System.Drawing.Point(93, 117);
			this.textBoxNotificationBody.Multiline = true;
			this.textBoxNotificationBody.Name = "textBoxNotificationBody";
			this.textBoxNotificationBody.Size = new System.Drawing.Size(737, 112);
			this.textBoxNotificationBody.TabIndex = 46;
			// 
			// textPatientFacingUrlPortal
			// 
			this.textPatientFacingUrlPortal.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textPatientFacingUrlPortal.Location = new System.Drawing.Point(298, 99);
			this.textPatientFacingUrlPortal.Name = "textPatientFacingUrlPortal";
			this.textPatientFacingUrlPortal.Size = new System.Drawing.Size(686, 20);
			this.textPatientFacingUrlPortal.TabIndex = 50;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox1.Controls.Add(this.textHostedUrlPortal);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(154, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(847, 84);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Open Dental Hosted";
			// 
			// textHostedUrlPortal
			// 
			this.textHostedUrlPortal.Location = new System.Drawing.Point(144, 49);
			this.textHostedUrlPortal.Name = "textHostedUrlPortal";
			this.textHostedUrlPortal.ReadOnly = true;
			this.textHostedUrlPortal.Size = new System.Drawing.Size(349, 20);
			this.textHostedUrlPortal.TabIndex = 43;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 17);
			this.label2.TabIndex = 40;
			this.label2.Text = "Hosted URL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(39, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(802, 26);
			this.label3.TabIndex = 42;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// tabWebSched
			// 
			this.tabWebSched.BackColor = System.Drawing.SystemColors.Control;
			this.tabWebSched.Controls.Add(this.linkLabelAboutWebSched);
			this.tabWebSched.Controls.Add(this.labelWebSchedDesc);
			this.tabWebSched.Controls.Add(this.tabControlWebSched);
			this.tabWebSched.Location = new System.Drawing.Point(4, 22);
			this.tabWebSched.Name = "tabWebSched";
			this.tabWebSched.Size = new System.Drawing.Size(1154, 588);
			this.tabWebSched.TabIndex = 3;
			this.tabWebSched.Text = "Web Sched";
			// 
			// linkLabelAboutWebSched
			// 
			this.linkLabelAboutWebSched.Location = new System.Drawing.Point(773, 3);
			this.linkLabelAboutWebSched.Name = "linkLabelAboutWebSched";
			this.linkLabelAboutWebSched.Size = new System.Drawing.Size(31, 28);
			this.linkLabelAboutWebSched.TabIndex = 303;
			this.linkLabelAboutWebSched.TabStop = true;
			this.linkLabelAboutWebSched.Text = "help";
			this.linkLabelAboutWebSched.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabelAboutWebSched.Click += new System.EventHandler(this.linkLabelAboutWebSched_Click);
			// 
			// labelWebSchedDesc
			// 
			this.labelWebSchedDesc.Location = new System.Drawing.Point(171, 3);
			this.labelWebSchedDesc.Name = "labelWebSchedDesc";
			this.labelWebSchedDesc.Size = new System.Drawing.Size(602, 28);
			this.labelWebSchedDesc.TabIndex = 52;
			this.labelWebSchedDesc.Text = "Web Sched is a separate service that gives your patients an easy way to schedule " +
    "appointments via the web within seconds.";
			this.labelWebSchedDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabControlWebSched
			// 
			this.tabControlWebSched.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlWebSched.Controls.Add(this.tabWebSchedRecalls);
			this.tabControlWebSched.Controls.Add(this.tabWebSchedNewPatAppts);
			this.tabControlWebSched.Location = new System.Drawing.Point(3, 24);
			this.tabControlWebSched.Name = "tabControlWebSched";
			this.tabControlWebSched.SelectedIndex = 0;
			this.tabControlWebSched.Size = new System.Drawing.Size(1148, 561);
			this.tabControlWebSched.TabIndex = 302;
			// 
			// tabWebSchedRecalls
			// 
			this.tabWebSchedRecalls.Controls.Add(this.groupWebSchedText);
			this.tabWebSchedRecalls.Controls.Add(this.groupBox6);
			this.tabWebSchedRecalls.Controls.Add(this.groupBoxWebSchedAutomation);
			this.tabWebSchedRecalls.Controls.Add(this.label21);
			this.tabWebSchedRecalls.Controls.Add(this.groupWebSchedPreview);
			this.tabWebSchedRecalls.Controls.Add(this.gridWebSchedOperatories);
			this.tabWebSchedRecalls.Controls.Add(this.label35);
			this.tabWebSchedRecalls.Controls.Add(this.listBoxWebSchedProviderPref);
			this.tabWebSchedRecalls.Controls.Add(this.butRecallSchedSetup);
			this.tabWebSchedRecalls.Controls.Add(this.label31);
			this.tabWebSchedRecalls.Controls.Add(this.gridWebSchedRecallTypes);
			this.tabWebSchedRecalls.Controls.Add(this.label20);
			this.tabWebSchedRecalls.Location = new System.Drawing.Point(4, 22);
			this.tabWebSchedRecalls.Name = "tabWebSchedRecalls";
			this.tabWebSchedRecalls.Padding = new System.Windows.Forms.Padding(3);
			this.tabWebSchedRecalls.Size = new System.Drawing.Size(1140, 535);
			this.tabWebSchedRecalls.TabIndex = 0;
			this.tabWebSchedRecalls.Text = "Recalls";
			this.tabWebSchedRecalls.UseVisualStyleBackColor = true;
			// 
			// groupWebSchedText
			// 
			this.groupWebSchedText.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupWebSchedText.Controls.Add(this.labelWebSchedPerBatch);
			this.groupWebSchedText.Controls.Add(this.textWebSchedPerBatch);
			this.groupWebSchedText.Controls.Add(this.radioDoNotSendText);
			this.groupWebSchedText.Controls.Add(this.radioSendText);
			this.groupWebSchedText.Location = new System.Drawing.Point(576, 472);
			this.groupWebSchedText.Name = "groupWebSchedText";
			this.groupWebSchedText.Size = new System.Drawing.Size(484, 55);
			this.groupWebSchedText.TabIndex = 313;
			this.groupWebSchedText.TabStop = false;
			this.groupWebSchedText.Text = "Send Text Messages Automatically To";
			// 
			// labelWebSchedPerBatch
			// 
			this.labelWebSchedPerBatch.Location = new System.Drawing.Point(276, 16);
			this.labelWebSchedPerBatch.Name = "labelWebSchedPerBatch";
			this.labelWebSchedPerBatch.Size = new System.Drawing.Size(147, 32);
			this.labelWebSchedPerBatch.TabIndex = 316;
			this.labelWebSchedPerBatch.Text = "Max number of texts sent every 10 minutes per clinic";
			this.labelWebSchedPerBatch.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textWebSchedPerBatch
			// 
			this.textWebSchedPerBatch.Location = new System.Drawing.Point(426, 28);
			this.textWebSchedPerBatch.MaxVal = 100000000;
			this.textWebSchedPerBatch.MinVal = 1;
			this.textWebSchedPerBatch.Name = "textWebSchedPerBatch";
			this.textWebSchedPerBatch.Size = new System.Drawing.Size(39, 20);
			this.textWebSchedPerBatch.TabIndex = 315;
			// 
			// radioDoNotSendText
			// 
			this.radioDoNotSendText.Location = new System.Drawing.Point(7, 16);
			this.radioDoNotSendText.Name = "radioDoNotSendText";
			this.radioDoNotSendText.Size = new System.Drawing.Size(438, 16);
			this.radioDoNotSendText.TabIndex = 77;
			this.radioDoNotSendText.Text = "Do Not Send";
			this.radioDoNotSendText.UseVisualStyleBackColor = true;
			this.radioDoNotSendText.CheckedChanged += new System.EventHandler(this.WebSchedRecallAutoSendRadioButtons_CheckedChanged);
			// 
			// radioSendText
			// 
			this.radioSendText.Location = new System.Drawing.Point(7, 32);
			this.radioSendText.Name = "radioSendText";
			this.radioSendText.Size = new System.Drawing.Size(438, 18);
			this.radioSendText.TabIndex = 0;
			this.radioSendText.Text = "Patients with wireless phone and texting OK";
			this.radioSendText.UseVisualStyleBackColor = true;
			this.radioSendText.CheckedChanged += new System.EventHandler(this.WebSchedRecallAutoSendRadioButtons_CheckedChanged);
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox6.Controls.Add(this.listboxWebSchedRecallIgnoreBlockoutTypes);
			this.groupBox6.Controls.Add(this.label32);
			this.groupBox6.Controls.Add(this.butWebSchedRecallBlockouts);
			this.groupBox6.Location = new System.Drawing.Point(669, 277);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(339, 103);
			this.groupBox6.TabIndex = 312;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Ignore Blockout Types";
			// 
			// listboxWebSchedRecallIgnoreBlockoutTypes
			// 
			this.listboxWebSchedRecallIgnoreBlockoutTypes.FormattingEnabled = true;
			this.listboxWebSchedRecallIgnoreBlockoutTypes.Location = new System.Drawing.Point(213, 13);
			this.listboxWebSchedRecallIgnoreBlockoutTypes.Name = "listboxWebSchedRecallIgnoreBlockoutTypes";
			this.listboxWebSchedRecallIgnoreBlockoutTypes.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listboxWebSchedRecallIgnoreBlockoutTypes.Size = new System.Drawing.Size(120, 82);
			this.listboxWebSchedRecallIgnoreBlockoutTypes.TabIndex = 197;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(6, 14);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(206, 20);
			this.label32.TabIndex = 223;
			this.label32.Text = "Currently Ignored Blockout Types";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butWebSchedRecallBlockouts
			// 
			this.butWebSchedRecallBlockouts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedRecallBlockouts.Autosize = true;
			this.butWebSchedRecallBlockouts.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedRecallBlockouts.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedRecallBlockouts.CornerRadius = 4F;
			this.butWebSchedRecallBlockouts.Location = new System.Drawing.Point(144, 72);
			this.butWebSchedRecallBlockouts.Name = "butWebSchedRecallBlockouts";
			this.butWebSchedRecallBlockouts.Size = new System.Drawing.Size(68, 23);
			this.butWebSchedRecallBlockouts.TabIndex = 197;
			this.butWebSchedRecallBlockouts.Text = "Edit";
			this.butWebSchedRecallBlockouts.Click += new System.EventHandler(this.butWebSchedRecallBlockouts_Click);
			// 
			// groupBoxWebSchedAutomation
			// 
			this.groupBoxWebSchedAutomation.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBoxWebSchedAutomation.Controls.Add(this.radioSendToEmailNoPreferred);
			this.groupBoxWebSchedAutomation.Controls.Add(this.radioDoNotSend);
			this.groupBoxWebSchedAutomation.Controls.Add(this.radioSendToEmailOnlyPreferred);
			this.groupBoxWebSchedAutomation.Controls.Add(this.radioSendToEmail);
			this.groupBoxWebSchedAutomation.Location = new System.Drawing.Point(576, 384);
			this.groupBoxWebSchedAutomation.Name = "groupBoxWebSchedAutomation";
			this.groupBoxWebSchedAutomation.Size = new System.Drawing.Size(484, 84);
			this.groupBoxWebSchedAutomation.TabIndex = 73;
			this.groupBoxWebSchedAutomation.TabStop = false;
			this.groupBoxWebSchedAutomation.Text = "Send Email Messages Automatically To";
			// 
			// radioSendToEmailNoPreferred
			// 
			this.radioSendToEmailNoPreferred.Location = new System.Drawing.Point(7, 47);
			this.radioSendToEmailNoPreferred.Name = "radioSendToEmailNoPreferred";
			this.radioSendToEmailNoPreferred.Size = new System.Drawing.Size(438, 18);
			this.radioSendToEmailNoPreferred.TabIndex = 1;
			this.radioSendToEmailNoPreferred.Text = "Patients with email address and no other preferred recall method is selected.";
			this.radioSendToEmailNoPreferred.UseVisualStyleBackColor = true;
			this.radioSendToEmailNoPreferred.CheckedChanged += new System.EventHandler(this.WebSchedRecallAutoSendRadioButtons_CheckedChanged);
			// 
			// radioDoNotSend
			// 
			this.radioDoNotSend.Location = new System.Drawing.Point(7, 16);
			this.radioDoNotSend.Name = "radioDoNotSend";
			this.radioDoNotSend.Size = new System.Drawing.Size(438, 16);
			this.radioDoNotSend.TabIndex = 77;
			this.radioDoNotSend.Text = "Do Not Send";
			this.radioDoNotSend.UseVisualStyleBackColor = true;
			// 
			// radioSendToEmailOnlyPreferred
			// 
			this.radioSendToEmailOnlyPreferred.Location = new System.Drawing.Point(7, 63);
			this.radioSendToEmailOnlyPreferred.Name = "radioSendToEmailOnlyPreferred";
			this.radioSendToEmailOnlyPreferred.Size = new System.Drawing.Size(438, 18);
			this.radioSendToEmailOnlyPreferred.TabIndex = 74;
			this.radioSendToEmailOnlyPreferred.Text = "Patients with email address and email is selected as their preferred recall metho" +
    "d.";
			this.radioSendToEmailOnlyPreferred.UseVisualStyleBackColor = true;
			this.radioSendToEmailOnlyPreferred.CheckedChanged += new System.EventHandler(this.WebSchedRecallAutoSendRadioButtons_CheckedChanged);
			// 
			// radioSendToEmail
			// 
			this.radioSendToEmail.Location = new System.Drawing.Point(7, 32);
			this.radioSendToEmail.Name = "radioSendToEmail";
			this.radioSendToEmail.Size = new System.Drawing.Size(438, 16);
			this.radioSendToEmail.TabIndex = 0;
			this.radioSendToEmail.Text = "Patients with email address";
			this.radioSendToEmail.UseVisualStyleBackColor = true;
			this.radioSendToEmail.CheckedChanged += new System.EventHandler(this.WebSchedRecallAutoSendRadioButtons_CheckedChanged);
			// 
			// label21
			// 
			this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label21.Location = new System.Drawing.Point(114, 448);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(313, 56);
			this.label21.TabIndex = 310;
			this.label21.Text = resources.GetString("label21.Text");
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupWebSchedPreview
			// 
			this.groupWebSchedPreview.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupWebSchedPreview.Controls.Add(this.butWebSchedPickClinic);
			this.groupWebSchedPreview.Controls.Add(this.butWebSchedPickProv);
			this.groupWebSchedPreview.Controls.Add(this.label22);
			this.groupWebSchedPreview.Controls.Add(this.comboWebSchedProviders);
			this.groupWebSchedPreview.Controls.Add(this.butWebSchedToday);
			this.groupWebSchedPreview.Controls.Add(this.gridWebSchedTimeSlots);
			this.groupWebSchedPreview.Controls.Add(this.textWebSchedDateStart);
			this.groupWebSchedPreview.Controls.Add(this.labelWebSchedClinic);
			this.groupWebSchedPreview.Controls.Add(this.labelWebSchedRecallTypes);
			this.groupWebSchedPreview.Controls.Add(this.comboWebSchedClinic);
			this.groupWebSchedPreview.Controls.Add(this.comboWebSchedRecallTypes);
			this.groupWebSchedPreview.Location = new System.Drawing.Point(114, 241);
			this.groupWebSchedPreview.Name = "groupWebSchedPreview";
			this.groupWebSchedPreview.Size = new System.Drawing.Size(439, 201);
			this.groupWebSchedPreview.TabIndex = 252;
			this.groupWebSchedPreview.TabStop = false;
			this.groupWebSchedPreview.Text = "Available Times For Patients";
			// 
			// butWebSchedPickClinic
			// 
			this.butWebSchedPickClinic.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedPickClinic.Autosize = false;
			this.butWebSchedPickClinic.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedPickClinic.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedPickClinic.CornerRadius = 2F;
			this.butWebSchedPickClinic.Location = new System.Drawing.Point(414, 159);
			this.butWebSchedPickClinic.Name = "butWebSchedPickClinic";
			this.butWebSchedPickClinic.Size = new System.Drawing.Size(18, 21);
			this.butWebSchedPickClinic.TabIndex = 313;
			this.butWebSchedPickClinic.Text = "...";
			this.butWebSchedPickClinic.Click += new System.EventHandler(this.butWebSchedPickClinic_Click);
			// 
			// butWebSchedPickProv
			// 
			this.butWebSchedPickProv.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedPickProv.Autosize = false;
			this.butWebSchedPickProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedPickProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedPickProv.CornerRadius = 2F;
			this.butWebSchedPickProv.Location = new System.Drawing.Point(414, 118);
			this.butWebSchedPickProv.Name = "butWebSchedPickProv";
			this.butWebSchedPickProv.Size = new System.Drawing.Size(18, 21);
			this.butWebSchedPickProv.TabIndex = 312;
			this.butWebSchedPickProv.Text = "...";
			this.butWebSchedPickProv.Click += new System.EventHandler(this.butWebSchedPickProv_Click);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(200, 101);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(182, 14);
			this.label22.TabIndex = 310;
			this.label22.Text = "Provider";
			this.label22.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboWebSchedProviders
			// 
			this.comboWebSchedProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboWebSchedProviders.Location = new System.Drawing.Point(200, 118);
			this.comboWebSchedProviders.MaxDropDownItems = 30;
			this.comboWebSchedProviders.Name = "comboWebSchedProviders";
			this.comboWebSchedProviders.Size = new System.Drawing.Size(209, 21);
			this.comboWebSchedProviders.TabIndex = 311;
			this.comboWebSchedProviders.SelectionChangeCommitted += new System.EventHandler(this.comboWebSchedProviders_SelectionChangeCommitted);
			// 
			// butWebSchedToday
			// 
			this.butWebSchedToday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedToday.Autosize = true;
			this.butWebSchedToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedToday.CornerRadius = 4F;
			this.butWebSchedToday.Location = new System.Drawing.Point(334, 36);
			this.butWebSchedToday.Name = "butWebSchedToday";
			this.butWebSchedToday.Size = new System.Drawing.Size(75, 21);
			this.butWebSchedToday.TabIndex = 309;
			this.butWebSchedToday.Text = "Today";
			this.butWebSchedToday.Click += new System.EventHandler(this.butWebSchedToday_Click);
			// 
			// gridWebSchedTimeSlots
			// 
			this.gridWebSchedTimeSlots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridWebSchedTimeSlots.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedTimeSlots.HasAddButton = false;
			this.gridWebSchedTimeSlots.HasDropDowns = false;
			this.gridWebSchedTimeSlots.HasMultilineHeaders = false;
			this.gridWebSchedTimeSlots.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedTimeSlots.HeaderHeight = 15;
			this.gridWebSchedTimeSlots.HScrollVisible = false;
			this.gridWebSchedTimeSlots.Location = new System.Drawing.Point(18, 19);
			this.gridWebSchedTimeSlots.Name = "gridWebSchedTimeSlots";
			this.gridWebSchedTimeSlots.ScrollValue = 0;
			this.gridWebSchedTimeSlots.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridWebSchedTimeSlots.Size = new System.Drawing.Size(174, 176);
			this.gridWebSchedTimeSlots.TabIndex = 302;
			this.gridWebSchedTimeSlots.Title = "Time Slots";
			this.gridWebSchedTimeSlots.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedTimeSlots.TitleHeight = 18;
			this.gridWebSchedTimeSlots.TranslationName = "FormEServicesSetup";
			this.gridWebSchedTimeSlots.WrapText = false;
			// 
			// textWebSchedDateStart
			// 
			this.textWebSchedDateStart.Location = new System.Drawing.Point(203, 36);
			this.textWebSchedDateStart.Name = "textWebSchedDateStart";
			this.textWebSchedDateStart.Size = new System.Drawing.Size(90, 20);
			this.textWebSchedDateStart.TabIndex = 303;
			this.textWebSchedDateStart.Text = "07/08/2015";
			this.textWebSchedDateStart.TextChanged += new System.EventHandler(this.textWebSchedDateStart_TextChanged);
			// 
			// labelWebSchedClinic
			// 
			this.labelWebSchedClinic.Location = new System.Drawing.Point(200, 142);
			this.labelWebSchedClinic.Name = "labelWebSchedClinic";
			this.labelWebSchedClinic.Size = new System.Drawing.Size(182, 14);
			this.labelWebSchedClinic.TabIndex = 264;
			this.labelWebSchedClinic.Text = "Clinic";
			this.labelWebSchedClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelWebSchedRecallTypes
			// 
			this.labelWebSchedRecallTypes.Location = new System.Drawing.Point(200, 60);
			this.labelWebSchedRecallTypes.Name = "labelWebSchedRecallTypes";
			this.labelWebSchedRecallTypes.Size = new System.Drawing.Size(182, 14);
			this.labelWebSchedRecallTypes.TabIndex = 254;
			this.labelWebSchedRecallTypes.Text = "Recall Type";
			this.labelWebSchedRecallTypes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboWebSchedClinic
			// 
			this.comboWebSchedClinic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboWebSchedClinic.Location = new System.Drawing.Point(200, 159);
			this.comboWebSchedClinic.MaxDropDownItems = 30;
			this.comboWebSchedClinic.Name = "comboWebSchedClinic";
			this.comboWebSchedClinic.Size = new System.Drawing.Size(209, 21);
			this.comboWebSchedClinic.TabIndex = 305;
			this.comboWebSchedClinic.SelectionChangeCommitted += new System.EventHandler(this.comboWebSchedClinic_SelectionChangeCommitted);
			// 
			// comboWebSchedRecallTypes
			// 
			this.comboWebSchedRecallTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboWebSchedRecallTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboWebSchedRecallTypes.Location = new System.Drawing.Point(200, 77);
			this.comboWebSchedRecallTypes.MaxDropDownItems = 30;
			this.comboWebSchedRecallTypes.Name = "comboWebSchedRecallTypes";
			this.comboWebSchedRecallTypes.Size = new System.Drawing.Size(209, 21);
			this.comboWebSchedRecallTypes.TabIndex = 304;
			this.comboWebSchedRecallTypes.SelectionChangeCommitted += new System.EventHandler(this.comboWebSchedRecallTypes_SelectionChangeCommitted);
			// 
			// gridWebSchedOperatories
			// 
			this.gridWebSchedOperatories.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gridWebSchedOperatories.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedOperatories.HasAddButton = false;
			this.gridWebSchedOperatories.HasDropDowns = false;
			this.gridWebSchedOperatories.HasMultilineHeaders = false;
			this.gridWebSchedOperatories.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedOperatories.HeaderHeight = 15;
			this.gridWebSchedOperatories.HScrollVisible = false;
			this.gridWebSchedOperatories.Location = new System.Drawing.Point(114, 21);
			this.gridWebSchedOperatories.Name = "gridWebSchedOperatories";
			this.gridWebSchedOperatories.ScrollValue = 0;
			this.gridWebSchedOperatories.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridWebSchedOperatories.Size = new System.Drawing.Size(532, 214);
			this.gridWebSchedOperatories.TabIndex = 307;
			this.gridWebSchedOperatories.Title = "Operatories Considered";
			this.gridWebSchedOperatories.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedOperatories.TitleHeight = 18;
			this.gridWebSchedOperatories.TranslationName = "FormEServicesSetup";
			this.gridWebSchedOperatories.WrapText = false;
			this.gridWebSchedOperatories.DoubleClick += new System.EventHandler(this.gridWebSchedOperatories_DoubleClick);
			// 
			// label35
			// 
			this.label35.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label35.Location = new System.Drawing.Point(666, 3);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(345, 15);
			this.label35.TabIndex = 254;
			this.label35.Text = "Double click to edit.";
			this.label35.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// listBoxWebSchedProviderPref
			// 
			this.listBoxWebSchedProviderPref.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.listBoxWebSchedProviderPref.FormattingEnabled = true;
			this.listBoxWebSchedProviderPref.Items.AddRange(new object[] {
            "First Available",
            "Primary Provider",
            "Secondary Provider",
            "Last Seen Hygienist"});
			this.listBoxWebSchedProviderPref.Location = new System.Drawing.Point(433, 448);
			this.listBoxWebSchedProviderPref.Name = "listBoxWebSchedProviderPref";
			this.listBoxWebSchedProviderPref.Size = new System.Drawing.Size(120, 56);
			this.listBoxWebSchedProviderPref.TabIndex = 309;
			this.listBoxWebSchedProviderPref.SelectedIndexChanged += new System.EventHandler(this.listBoxWebSchedProviderPref_SelectedIndexChanged);
			// 
			// butRecallSchedSetup
			// 
			this.butRecallSchedSetup.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRecallSchedSetup.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butRecallSchedSetup.Autosize = true;
			this.butRecallSchedSetup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecallSchedSetup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecallSchedSetup.CornerRadius = 4F;
			this.butRecallSchedSetup.Location = new System.Drawing.Point(905, 241);
			this.butRecallSchedSetup.Name = "butRecallSchedSetup";
			this.butRecallSchedSetup.Size = new System.Drawing.Size(103, 24);
			this.butRecallSchedSetup.TabIndex = 308;
			this.butRecallSchedSetup.Text = "Recall Setup";
			this.butRecallSchedSetup.Click += new System.EventHandler(this.butWebSchedSetup_Click);
			// 
			// label31
			// 
			this.label31.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label31.Location = new System.Drawing.Point(389, 3);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(257, 15);
			this.label31.TabIndex = 254;
			this.label31.Text = "Double click to edit.";
			this.label31.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// gridWebSchedRecallTypes
			// 
			this.gridWebSchedRecallTypes.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gridWebSchedRecallTypes.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedRecallTypes.HasAddButton = false;
			this.gridWebSchedRecallTypes.HasDropDowns = false;
			this.gridWebSchedRecallTypes.HasMultilineHeaders = false;
			this.gridWebSchedRecallTypes.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedRecallTypes.HeaderHeight = 15;
			this.gridWebSchedRecallTypes.HScrollVisible = false;
			this.gridWebSchedRecallTypes.Location = new System.Drawing.Point(669, 21);
			this.gridWebSchedRecallTypes.Name = "gridWebSchedRecallTypes";
			this.gridWebSchedRecallTypes.ScrollValue = 0;
			this.gridWebSchedRecallTypes.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridWebSchedRecallTypes.Size = new System.Drawing.Size(342, 214);
			this.gridWebSchedRecallTypes.TabIndex = 307;
			this.gridWebSchedRecallTypes.Title = "Recall Types";
			this.gridWebSchedRecallTypes.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedRecallTypes.TitleHeight = 18;
			this.gridWebSchedRecallTypes.TranslationName = "FormEServicesSetup";
			this.gridWebSchedRecallTypes.WrapText = false;
			this.gridWebSchedRecallTypes.DoubleClick += new System.EventHandler(this.gridWebSchedRecallTypes_DoubleClick);
			// 
			// label20
			// 
			this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label20.Location = new System.Drawing.Point(666, 239);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(233, 28);
			this.label20.TabIndex = 247;
			this.label20.Text = "Customize the notification message that will be sent to the patient.";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabWebSchedNewPatAppts
			// 
			this.tabWebSchedNewPatAppts.Controls.Add(this.groupBox13);
			this.tabWebSchedNewPatAppts.Controls.Add(this.groupBox11);
			this.tabWebSchedNewPatAppts.Controls.Add(this.gridWebSchedNewPatApptURLs);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label44);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label43);
			this.tabWebSchedNewPatAppts.Controls.Add(this.textWebSchedNewPatApptLength);
			this.tabWebSchedNewPatAppts.Controls.Add(this.butWebSchedNewPatApptsRemove);
			this.tabWebSchedNewPatAppts.Controls.Add(this.butWebSchedNewPatApptsAdd);
			this.tabWebSchedNewPatAppts.Controls.Add(this.gridWebSchedNewPatApptOps);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label42);
			this.tabWebSchedNewPatAppts.Controls.Add(this.groupBox7);
			this.tabWebSchedNewPatAppts.Controls.Add(this.gridWebSchedNewPatApptProcs);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label41);
			this.tabWebSchedNewPatAppts.Controls.Add(this.textWebSchedNewPatApptSearchDays);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label40);
			this.tabWebSchedNewPatAppts.Controls.Add(this.label39);
			this.tabWebSchedNewPatAppts.Location = new System.Drawing.Point(4, 22);
			this.tabWebSchedNewPatAppts.Name = "tabWebSchedNewPatAppts";
			this.tabWebSchedNewPatAppts.Padding = new System.Windows.Forms.Padding(3);
			this.tabWebSchedNewPatAppts.Size = new System.Drawing.Size(1140, 535);
			this.tabWebSchedNewPatAppts.TabIndex = 1;
			this.tabWebSchedNewPatAppts.Text = "New Patient Appts";
			this.tabWebSchedNewPatAppts.UseVisualStyleBackColor = true;
			// 
			// groupBox13
			// 
			this.groupBox13.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox13.Controls.Add(this.textWebSchedNewPatApptMessage);
			this.groupBox13.Location = new System.Drawing.Point(434, 416);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new System.Drawing.Size(242, 103);
			this.groupBox13.TabIndex = 324;
			this.groupBox13.TabStop = false;
			this.groupBox13.Text = "Appointment Message";
			// 
			// textWebSchedNewPatApptMessage
			// 
			this.textWebSchedNewPatApptMessage.Location = new System.Drawing.Point(6, 17);
			this.textWebSchedNewPatApptMessage.Multiline = true;
			this.textWebSchedNewPatApptMessage.Name = "textWebSchedNewPatApptMessage";
			this.textWebSchedNewPatApptMessage.Size = new System.Drawing.Size(230, 80);
			this.textWebSchedNewPatApptMessage.TabIndex = 313;
			// 
			// groupBox11
			// 
			this.groupBox11.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox11.Controls.Add(this.listboxWebSchedNewPatIgnoreBlockoutTypes);
			this.groupBox11.Controls.Add(this.label33);
			this.groupBox11.Controls.Add(this.butWebSchedNewPatBlockouts);
			this.groupBox11.Location = new System.Drawing.Point(678, 416);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(339, 103);
			this.groupBox11.TabIndex = 323;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "Ignore Blockout Types";
			// 
			// listboxWebSchedNewPatIgnoreBlockoutTypes
			// 
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.FormattingEnabled = true;
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.Location = new System.Drawing.Point(213, 13);
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.Name = "listboxWebSchedNewPatIgnoreBlockoutTypes";
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.Size = new System.Drawing.Size(120, 82);
			this.listboxWebSchedNewPatIgnoreBlockoutTypes.TabIndex = 197;
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(6, 14);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(206, 20);
			this.label33.TabIndex = 223;
			this.label33.Text = "Currently Ignored Blockout Types";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butWebSchedNewPatBlockouts
			// 
			this.butWebSchedNewPatBlockouts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedNewPatBlockouts.Autosize = true;
			this.butWebSchedNewPatBlockouts.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedNewPatBlockouts.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedNewPatBlockouts.CornerRadius = 4F;
			this.butWebSchedNewPatBlockouts.Location = new System.Drawing.Point(144, 72);
			this.butWebSchedNewPatBlockouts.Name = "butWebSchedNewPatBlockouts";
			this.butWebSchedNewPatBlockouts.Size = new System.Drawing.Size(68, 23);
			this.butWebSchedNewPatBlockouts.TabIndex = 197;
			this.butWebSchedNewPatBlockouts.Text = "Edit";
			this.butWebSchedNewPatBlockouts.Click += new System.EventHandler(this.butWebSchedNewPatBlockouts_Click);
			// 
			// gridWebSchedNewPatApptURLs
			// 
			this.gridWebSchedNewPatApptURLs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gridWebSchedNewPatApptURLs.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedNewPatApptURLs.HasAddButton = false;
			this.gridWebSchedNewPatApptURLs.HasAutoWrappedHeaders = true;
			this.gridWebSchedNewPatApptURLs.HasDropDowns = false;
			this.gridWebSchedNewPatApptURLs.HasLinkDetect = false;
			this.gridWebSchedNewPatApptURLs.HasMultilineHeaders = true;
			this.gridWebSchedNewPatApptURLs.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptURLs.HeaderHeight = 15;
			this.gridWebSchedNewPatApptURLs.HScrollVisible = true;
			this.gridWebSchedNewPatApptURLs.Location = new System.Drawing.Point(434, 247);
			this.gridWebSchedNewPatApptURLs.Name = "gridWebSchedNewPatApptURLs";
			this.gridWebSchedNewPatApptURLs.ScrollValue = 0;
			this.gridWebSchedNewPatApptURLs.Size = new System.Drawing.Size(583, 165);
			this.gridWebSchedNewPatApptURLs.TabIndex = 317;
			this.gridWebSchedNewPatApptURLs.Title = "Hosted URLs";
			this.gridWebSchedNewPatApptURLs.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptURLs.TitleHeight = 18;
			this.gridWebSchedNewPatApptURLs.TranslationName = "FormEServicesSetup";
			this.gridWebSchedNewPatApptURLs.WrapText = false;
			this.gridWebSchedNewPatApptURLs.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridWebSchedNewPatApptURLs_CellDoubleClick);
			this.gridWebSchedNewPatApptURLs.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridWebSchedNewPatApptURLs_CellClick);
			// 
			// label44
			// 
			this.label44.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label44.Location = new System.Drawing.Point(434, 16);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(136, 19);
			this.label44.TabIndex = 314;
			this.label44.Text = "(only /\'s and X\'s)";
			// 
			// label43
			// 
			this.label43.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label43.Location = new System.Drawing.Point(138, 14);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(174, 17);
			this.label43.TabIndex = 313;
			this.label43.Text = "Appointment length";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textWebSchedNewPatApptLength
			// 
			this.textWebSchedNewPatApptLength.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textWebSchedNewPatApptLength.Location = new System.Drawing.Point(318, 13);
			this.textWebSchedNewPatApptLength.Multiline = true;
			this.textWebSchedNewPatApptLength.Name = "textWebSchedNewPatApptLength";
			this.textWebSchedNewPatApptLength.Size = new System.Drawing.Size(110, 20);
			this.textWebSchedNewPatApptLength.TabIndex = 312;
			this.textWebSchedNewPatApptLength.TextChanged += new System.EventHandler(this.textWebSchedNewPatApptLength_TextChanged);
			this.textWebSchedNewPatApptLength.Leave += new System.EventHandler(this.textWebSchedNewPatApptLength_Leave);
			// 
			// butWebSchedNewPatApptsRemove
			// 
			this.butWebSchedNewPatApptsRemove.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedNewPatApptsRemove.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butWebSchedNewPatApptsRemove.Autosize = true;
			this.butWebSchedNewPatApptsRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedNewPatApptsRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedNewPatApptsRemove.CornerRadius = 4F;
			this.butWebSchedNewPatApptsRemove.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butWebSchedNewPatApptsRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butWebSchedNewPatApptsRemove.Location = new System.Drawing.Point(318, 206);
			this.butWebSchedNewPatApptsRemove.Name = "butWebSchedNewPatApptsRemove";
			this.butWebSchedNewPatApptsRemove.Size = new System.Drawing.Size(78, 25);
			this.butWebSchedNewPatApptsRemove.TabIndex = 311;
			this.butWebSchedNewPatApptsRemove.Text = "Remove";
			this.butWebSchedNewPatApptsRemove.Click += new System.EventHandler(this.butWebSchedNewPatApptsRemove_Click);
			// 
			// butWebSchedNewPatApptsAdd
			// 
			this.butWebSchedNewPatApptsAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedNewPatApptsAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butWebSchedNewPatApptsAdd.Autosize = true;
			this.butWebSchedNewPatApptsAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedNewPatApptsAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedNewPatApptsAdd.CornerRadius = 4F;
			this.butWebSchedNewPatApptsAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butWebSchedNewPatApptsAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butWebSchedNewPatApptsAdd.Location = new System.Drawing.Point(317, 76);
			this.butWebSchedNewPatApptsAdd.Name = "butWebSchedNewPatApptsAdd";
			this.butWebSchedNewPatApptsAdd.Size = new System.Drawing.Size(79, 25);
			this.butWebSchedNewPatApptsAdd.TabIndex = 310;
			this.butWebSchedNewPatApptsAdd.Text = "&Add";
			this.butWebSchedNewPatApptsAdd.Click += new System.EventHandler(this.butWebSchedNewPatApptsAdd_Click);
			// 
			// gridWebSchedNewPatApptOps
			// 
			this.gridWebSchedNewPatApptOps.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gridWebSchedNewPatApptOps.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedNewPatApptOps.HasAddButton = false;
			this.gridWebSchedNewPatApptOps.HasDropDowns = false;
			this.gridWebSchedNewPatApptOps.HasMultilineHeaders = false;
			this.gridWebSchedNewPatApptOps.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptOps.HeaderHeight = 15;
			this.gridWebSchedNewPatApptOps.HScrollVisible = false;
			this.gridWebSchedNewPatApptOps.Location = new System.Drawing.Point(434, 76);
			this.gridWebSchedNewPatApptOps.Name = "gridWebSchedNewPatApptOps";
			this.gridWebSchedNewPatApptOps.ScrollValue = 0;
			this.gridWebSchedNewPatApptOps.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridWebSchedNewPatApptOps.Size = new System.Drawing.Size(583, 142);
			this.gridWebSchedNewPatApptOps.TabIndex = 309;
			this.gridWebSchedNewPatApptOps.Title = "Operatories Considered";
			this.gridWebSchedNewPatApptOps.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptOps.TitleHeight = 18;
			this.gridWebSchedNewPatApptOps.TranslationName = "FormEServicesSetup";
			this.gridWebSchedNewPatApptOps.WrapText = false;
			this.gridWebSchedNewPatApptOps.DoubleClick += new System.EventHandler(this.gridWebSchedNewPatApptOps_DoubleClick);
			// 
			// label42
			// 
			this.label42.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label42.Location = new System.Drawing.Point(795, 58);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(222, 15);
			this.label42.TabIndex = 308;
			this.label42.Text = "Double click to edit.";
			this.label42.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// groupBox7
			// 
			this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox7.Controls.Add(this.label10);
			this.groupBox7.Controls.Add(this.butWebSchedNewPatApptsToday);
			this.groupBox7.Controls.Add(this.gridWebSchedNewPatApptTimeSlots);
			this.groupBox7.Controls.Add(this.textWebSchedNewPatApptsDateStart);
			this.groupBox7.Location = new System.Drawing.Point(123, 237);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(285, 228);
			this.groupBox7.TabIndex = 304;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Available Times For Patients";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(12, 196);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(261, 26);
			this.label10.TabIndex = 318;
			this.label10.Text = "Select a Hosted URL to view its available time slots.";
			// 
			// butWebSchedNewPatApptsToday
			// 
			this.butWebSchedNewPatApptsToday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butWebSchedNewPatApptsToday.Autosize = true;
			this.butWebSchedNewPatApptsToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butWebSchedNewPatApptsToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butWebSchedNewPatApptsToday.CornerRadius = 4F;
			this.butWebSchedNewPatApptsToday.Location = new System.Drawing.Point(195, 46);
			this.butWebSchedNewPatApptsToday.Name = "butWebSchedNewPatApptsToday";
			this.butWebSchedNewPatApptsToday.Size = new System.Drawing.Size(79, 21);
			this.butWebSchedNewPatApptsToday.TabIndex = 309;
			this.butWebSchedNewPatApptsToday.Text = "Today";
			this.butWebSchedNewPatApptsToday.Click += new System.EventHandler(this.butWebSchedNewPatApptsToday_Click);
			// 
			// gridWebSchedNewPatApptTimeSlots
			// 
			this.gridWebSchedNewPatApptTimeSlots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridWebSchedNewPatApptTimeSlots.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedNewPatApptTimeSlots.HasAddButton = false;
			this.gridWebSchedNewPatApptTimeSlots.HasDropDowns = false;
			this.gridWebSchedNewPatApptTimeSlots.HasMultilineHeaders = false;
			this.gridWebSchedNewPatApptTimeSlots.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptTimeSlots.HeaderHeight = 15;
			this.gridWebSchedNewPatApptTimeSlots.HScrollVisible = false;
			this.gridWebSchedNewPatApptTimeSlots.Location = new System.Drawing.Point(15, 20);
			this.gridWebSchedNewPatApptTimeSlots.Name = "gridWebSchedNewPatApptTimeSlots";
			this.gridWebSchedNewPatApptTimeSlots.ScrollValue = 0;
			this.gridWebSchedNewPatApptTimeSlots.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridWebSchedNewPatApptTimeSlots.Size = new System.Drawing.Size(174, 169);
			this.gridWebSchedNewPatApptTimeSlots.TabIndex = 302;
			this.gridWebSchedNewPatApptTimeSlots.Title = "Time Slots";
			this.gridWebSchedNewPatApptTimeSlots.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptTimeSlots.TitleHeight = 18;
			this.gridWebSchedNewPatApptTimeSlots.TranslationName = "FormEServicesSetup";
			this.gridWebSchedNewPatApptTimeSlots.WrapText = false;
			// 
			// textWebSchedNewPatApptsDateStart
			// 
			this.textWebSchedNewPatApptsDateStart.Location = new System.Drawing.Point(195, 20);
			this.textWebSchedNewPatApptsDateStart.Name = "textWebSchedNewPatApptsDateStart";
			this.textWebSchedNewPatApptsDateStart.Size = new System.Drawing.Size(79, 20);
			this.textWebSchedNewPatApptsDateStart.TabIndex = 303;
			this.textWebSchedNewPatApptsDateStart.Text = "07/08/2015";
			this.textWebSchedNewPatApptsDateStart.TextChanged += new System.EventHandler(this.textWebSchedNewPatApptsDateStart_TextChanged);
			// 
			// gridWebSchedNewPatApptProcs
			// 
			this.gridWebSchedNewPatApptProcs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gridWebSchedNewPatApptProcs.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWebSchedNewPatApptProcs.HasAddButton = false;
			this.gridWebSchedNewPatApptProcs.HasDropDowns = false;
			this.gridWebSchedNewPatApptProcs.HasMultilineHeaders = false;
			this.gridWebSchedNewPatApptProcs.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptProcs.HeaderHeight = 15;
			this.gridWebSchedNewPatApptProcs.HScrollVisible = false;
			this.gridWebSchedNewPatApptProcs.Location = new System.Drawing.Point(138, 76);
			this.gridWebSchedNewPatApptProcs.Name = "gridWebSchedNewPatApptProcs";
			this.gridWebSchedNewPatApptProcs.ScrollValue = 0;
			this.gridWebSchedNewPatApptProcs.Size = new System.Drawing.Size(174, 155);
			this.gridWebSchedNewPatApptProcs.TabIndex = 303;
			this.gridWebSchedNewPatApptProcs.Title = "Procedures";
			this.gridWebSchedNewPatApptProcs.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWebSchedNewPatApptProcs.TitleHeight = 18;
			this.gridWebSchedNewPatApptProcs.TranslationName = "FormEServicesSetup";
			this.gridWebSchedNewPatApptProcs.WrapText = false;
			// 
			// label41
			// 
			this.label41.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label41.Location = new System.Drawing.Point(359, 40);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(304, 17);
			this.label41.TabIndex = 244;
			this.label41.Text = "days.  Empty includes all possible openings.";
			this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textWebSchedNewPatApptSearchDays
			// 
			this.textWebSchedNewPatApptSearchDays.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textWebSchedNewPatApptSearchDays.Location = new System.Drawing.Point(318, 39);
			this.textWebSchedNewPatApptSearchDays.MaxVal = 365;
			this.textWebSchedNewPatApptSearchDays.MinVal = 0;
			this.textWebSchedNewPatApptSearchDays.Name = "textWebSchedNewPatApptSearchDays";
			this.textWebSchedNewPatApptSearchDays.Size = new System.Drawing.Size(38, 20);
			this.textWebSchedNewPatApptSearchDays.TabIndex = 243;
			this.textWebSchedNewPatApptSearchDays.Leave += new System.EventHandler(this.textWebSchedNewPatApptSearchDays_Leave);
			this.textWebSchedNewPatApptSearchDays.Validated += new System.EventHandler(this.textWebSchedNewPatApptSearchDays_Validated);
			// 
			// label40
			// 
			this.label40.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label40.Location = new System.Drawing.Point(138, 40);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(174, 17);
			this.label40.TabIndex = 242;
			this.label40.Text = "Search for openings after";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label39
			// 
			this.label39.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label39.Location = new System.Drawing.Point(434, 220);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(617, 26);
			this.label39.TabIndex = 59;
			this.label39.Text = resources.GetString("label39.Text");
			// 
			// tabTexting
			// 
			this.tabTexting.BackColor = System.Drawing.SystemColors.Control;
			this.tabTexting.Controls.Add(this.butDefaultClinicClear);
			this.tabTexting.Controls.Add(this.butDefaultClinic);
			this.tabTexting.Controls.Add(this.butBackMonth);
			this.tabTexting.Controls.Add(this.dateTimePickerSms);
			this.tabTexting.Controls.Add(this.gridSmsSummary);
			this.tabTexting.Controls.Add(this.butFwdMonth);
			this.tabTexting.Controls.Add(this.butThisMonth);
			this.tabTexting.Location = new System.Drawing.Point(4, 22);
			this.tabTexting.Name = "tabTexting";
			this.tabTexting.Padding = new System.Windows.Forms.Padding(3);
			this.tabTexting.Size = new System.Drawing.Size(1154, 588);
			this.tabTexting.TabIndex = 6;
			this.tabTexting.Text = "Texting Services";
			// 
			// butDefaultClinicClear
			// 
			this.butDefaultClinicClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDefaultClinicClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDefaultClinicClear.Autosize = true;
			this.butDefaultClinicClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaultClinicClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaultClinicClear.CornerRadius = 4F;
			this.butDefaultClinicClear.Location = new System.Drawing.Point(980, 532);
			this.butDefaultClinicClear.Name = "butDefaultClinicClear";
			this.butDefaultClinicClear.Size = new System.Drawing.Size(81, 23);
			this.butDefaultClinicClear.TabIndex = 269;
			this.butDefaultClinicClear.Text = "Clear Default";
			this.butDefaultClinicClear.UseVisualStyleBackColor = true;
			this.butDefaultClinicClear.Click += new System.EventHandler(this.butDefaultClinicClear_Click);
			// 
			// butDefaultClinic
			// 
			this.butDefaultClinic.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDefaultClinic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDefaultClinic.Autosize = true;
			this.butDefaultClinic.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaultClinic.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaultClinic.CornerRadius = 4F;
			this.butDefaultClinic.Location = new System.Drawing.Point(1067, 532);
			this.butDefaultClinic.Name = "butDefaultClinic";
			this.butDefaultClinic.Size = new System.Drawing.Size(81, 23);
			this.butDefaultClinic.TabIndex = 262;
			this.butDefaultClinic.Text = "Set Default";
			this.butDefaultClinic.UseVisualStyleBackColor = true;
			this.butDefaultClinic.Click += new System.EventHandler(this.butDefaultClinic_Click);
			// 
			// butBackMonth
			// 
			this.butBackMonth.AdjustImageLocation = new System.Drawing.Point(-3, -1);
			this.butBackMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butBackMonth.Autosize = true;
			this.butBackMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackMonth.CornerRadius = 4F;
			this.butBackMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butBackMonth.Image = ((System.Drawing.Image)(resources.GetObject("butBackMonth.Image")));
			this.butBackMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBackMonth.Location = new System.Drawing.Point(490, 534);
			this.butBackMonth.Name = "butBackMonth";
			this.butBackMonth.Size = new System.Drawing.Size(32, 22);
			this.butBackMonth.TabIndex = 268;
			this.butBackMonth.Text = "M";
			this.butBackMonth.Click += new System.EventHandler(this.butBackMonth_Click);
			// 
			// dateTimePickerSms
			// 
			this.dateTimePickerSms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePickerSms.CustomFormat = "MMM yyyy";
			this.dateTimePickerSms.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerSms.Location = new System.Drawing.Point(522, 535);
			this.dateTimePickerSms.Name = "dateTimePickerSms";
			this.dateTimePickerSms.Size = new System.Drawing.Size(113, 20);
			this.dateTimePickerSms.TabIndex = 258;
			this.dateTimePickerSms.ValueChanged += new System.EventHandler(this.dateTimePickerSms_ValueChanged);
			// 
			// gridSmsSummary
			// 
			this.gridSmsSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSmsSummary.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridSmsSummary.HasAddButton = false;
			this.gridSmsSummary.HasDropDowns = false;
			this.gridSmsSummary.HasMultilineHeaders = true;
			this.gridSmsSummary.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridSmsSummary.HeaderHeight = 15;
			this.gridSmsSummary.HScrollVisible = false;
			this.gridSmsSummary.Location = new System.Drawing.Point(0, 6);
			this.gridSmsSummary.Name = "gridSmsSummary";
			this.gridSmsSummary.ScrollValue = 0;
			this.gridSmsSummary.Size = new System.Drawing.Size(1150, 522);
			this.gridSmsSummary.TabIndex = 252;
			this.gridSmsSummary.Title = "Text Messaging Phone Number and Usage Summary";
			this.gridSmsSummary.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridSmsSummary.TitleHeight = 18;
			this.gridSmsSummary.TranslationName = "FormEServicesSetup";
			this.gridSmsSummary.WrapText = false;
			// 
			// butFwdMonth
			// 
			this.butFwdMonth.AdjustImageLocation = new System.Drawing.Point(5, -1);
			this.butFwdMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFwdMonth.Autosize = false;
			this.butFwdMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwdMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwdMonth.CornerRadius = 4F;
			this.butFwdMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butFwdMonth.Image = ((System.Drawing.Image)(resources.GetObject("butFwdMonth.Image")));
			this.butFwdMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butFwdMonth.Location = new System.Drawing.Point(635, 534);
			this.butFwdMonth.Name = "butFwdMonth";
			this.butFwdMonth.Size = new System.Drawing.Size(29, 22);
			this.butFwdMonth.TabIndex = 267;
			this.butFwdMonth.Text = "M";
			this.butFwdMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butFwdMonth.Click += new System.EventHandler(this.butFwdMonth_Click);
			// 
			// butThisMonth
			// 
			this.butThisMonth.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butThisMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butThisMonth.Autosize = false;
			this.butThisMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butThisMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butThisMonth.CornerRadius = 4F;
			this.butThisMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butThisMonth.Location = new System.Drawing.Point(541, 558);
			this.butThisMonth.Name = "butThisMonth";
			this.butThisMonth.Size = new System.Drawing.Size(75, 22);
			this.butThisMonth.TabIndex = 262;
			this.butThisMonth.Text = "This Month";
			this.butThisMonth.Click += new System.EventHandler(this.butThisMonth_Click);
			// 
			// tabECR
			// 
			this.tabECR.Controls.Add(this.label11);
			this.tabECR.Controls.Add(this.gridConfStatuses);
			this.tabECR.Controls.Add(this.groupAutomationStatuses);
			this.tabECR.Controls.Add(this.textStatusReminders);
			this.tabECR.Controls.Add(this.butActivateReminder);
			this.tabECR.Controls.Add(this.textStatusConfirmations);
			this.tabECR.Controls.Add(this.butActivateConfirm);
			this.tabECR.Controls.Add(this.butAddReminderFutureDay);
			this.tabECR.Controls.Add(this.checkUseDefaultsEC);
			this.tabECR.Controls.Add(this.checkIsConfirmEnabled);
			this.tabECR.Controls.Add(this.comboClinicEConfirm);
			this.tabECR.Controls.Add(this.label54);
			this.tabECR.Controls.Add(this.butAddConfirmation);
			this.tabECR.Controls.Add(this.butAddReminderSameDay);
			this.tabECR.Controls.Add(this.gridRemindersMain);
			this.tabECR.Location = new System.Drawing.Point(4, 22);
			this.tabECR.Name = "tabECR";
			this.tabECR.Padding = new System.Windows.Forms.Padding(3);
			this.tabECR.Size = new System.Drawing.Size(1154, 588);
			this.tabECR.TabIndex = 8;
			this.tabECR.Text = "Automated eReminders & eConfirmations";
			this.tabECR.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label11.Location = new System.Drawing.Point(18, 508);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(272, 72);
			this.label11.TabIndex = 271;
			this.label11.Text = "Don\'t Send: If an appointment has this status, do not send a confirmation.\r\nDon\'t" +
    " Change: If an appointment has this status, do not change status when a response" +
    " is received.";
			// 
			// gridConfStatuses
			// 
			this.gridConfStatuses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridConfStatuses.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridConfStatuses.HasAddButton = false;
			this.gridConfStatuses.HasDropDowns = false;
			this.gridConfStatuses.HasMultilineHeaders = true;
			this.gridConfStatuses.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridConfStatuses.HeaderHeight = 15;
			this.gridConfStatuses.HScrollVisible = false;
			this.gridConfStatuses.Location = new System.Drawing.Point(18, 292);
			this.gridConfStatuses.Name = "gridConfStatuses";
			this.gridConfStatuses.ScrollValue = 0;
			this.gridConfStatuses.Size = new System.Drawing.Size(272, 213);
			this.gridConfStatuses.TabIndex = 270;
			this.gridConfStatuses.Title = "Confirmation Statuses";
			this.gridConfStatuses.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridConfStatuses.TitleHeight = 18;
			this.gridConfStatuses.TranslationName = null;
			this.gridConfStatuses.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridConfStatuses_CellDoubleClick);
			// 
			// groupAutomationStatuses
			// 
			this.groupAutomationStatuses.Controls.Add(this.comboStatusEFailed);
			this.groupAutomationStatuses.Controls.Add(this.label50);
			this.groupAutomationStatuses.Controls.Add(this.checkEnableNoClinic);
			this.groupAutomationStatuses.Controls.Add(this.comboStatusEDeclined);
			this.groupAutomationStatuses.Controls.Add(this.comboStatusESent);
			this.groupAutomationStatuses.Controls.Add(this.comboStatusEAccepted);
			this.groupAutomationStatuses.Controls.Add(this.label51);
			this.groupAutomationStatuses.Controls.Add(this.label52);
			this.groupAutomationStatuses.Controls.Add(this.label53);
			this.groupAutomationStatuses.Location = new System.Drawing.Point(18, 110);
			this.groupAutomationStatuses.Name = "groupAutomationStatuses";
			this.groupAutomationStatuses.Size = new System.Drawing.Size(272, 163);
			this.groupAutomationStatuses.TabIndex = 269;
			this.groupAutomationStatuses.TabStop = false;
			this.groupAutomationStatuses.Text = "Global eConfirmation Settings";
			// 
			// comboStatusEFailed
			// 
			this.comboStatusEFailed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboStatusEFailed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEFailed.Location = new System.Drawing.Point(97, 100);
			this.comboStatusEFailed.MaxDropDownItems = 30;
			this.comboStatusEFailed.Name = "comboStatusEFailed";
			this.comboStatusEFailed.Size = new System.Drawing.Size(151, 21);
			this.comboStatusEFailed.TabIndex = 173;
			// 
			// label50
			// 
			this.label50.Location = new System.Drawing.Point(6, 101);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(89, 16);
			this.label50.TabIndex = 174;
			this.label50.Text = "Failed";
			this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEnableNoClinic
			// 
			this.checkEnableNoClinic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEnableNoClinic.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEnableNoClinic.Location = new System.Drawing.Point(9, 127);
			this.checkEnableNoClinic.Name = "checkEnableNoClinic";
			this.checkEnableNoClinic.Size = new System.Drawing.Size(239, 31);
			this.checkEnableNoClinic.TabIndex = 172;
			this.checkEnableNoClinic.Text = "Allow eMessages from Appts w/o Clinic";
			this.checkEnableNoClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatusEDeclined
			// 
			this.comboStatusEDeclined.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboStatusEDeclined.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEDeclined.Location = new System.Drawing.Point(97, 73);
			this.comboStatusEDeclined.MaxDropDownItems = 30;
			this.comboStatusEDeclined.Name = "comboStatusEDeclined";
			this.comboStatusEDeclined.Size = new System.Drawing.Size(151, 21);
			this.comboStatusEDeclined.TabIndex = 170;
			// 
			// comboStatusESent
			// 
			this.comboStatusESent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboStatusESent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusESent.Location = new System.Drawing.Point(97, 19);
			this.comboStatusESent.MaxDropDownItems = 30;
			this.comboStatusESent.Name = "comboStatusESent";
			this.comboStatusESent.Size = new System.Drawing.Size(151, 21);
			this.comboStatusESent.TabIndex = 166;
			// 
			// comboStatusEAccepted
			// 
			this.comboStatusEAccepted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboStatusEAccepted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEAccepted.Location = new System.Drawing.Point(97, 46);
			this.comboStatusEAccepted.MaxDropDownItems = 30;
			this.comboStatusEAccepted.Name = "comboStatusEAccepted";
			this.comboStatusEAccepted.Size = new System.Drawing.Size(151, 21);
			this.comboStatusEAccepted.TabIndex = 168;
			// 
			// label51
			// 
			this.label51.Location = new System.Drawing.Point(6, 74);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(89, 16);
			this.label51.TabIndex = 171;
			this.label51.Text = "Not Accepted";
			this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label52
			// 
			this.label52.Location = new System.Drawing.Point(6, 20);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(89, 16);
			this.label52.TabIndex = 167;
			this.label52.Text = "Sent";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label53
			// 
			this.label53.Location = new System.Drawing.Point(6, 47);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(89, 16);
			this.label53.TabIndex = 169;
			this.label53.Text = "Accepted";
			this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStatusReminders
			// 
			this.textStatusReminders.Location = new System.Drawing.Point(6, 37);
			this.textStatusReminders.Name = "textStatusReminders";
			this.textStatusReminders.ReadOnly = true;
			this.textStatusReminders.Size = new System.Drawing.Size(131, 20);
			this.textStatusReminders.TabIndex = 268;
			this.textStatusReminders.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// butActivateReminder
			// 
			this.butActivateReminder.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butActivateReminder.Autosize = true;
			this.butActivateReminder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butActivateReminder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butActivateReminder.CornerRadius = 4F;
			this.butActivateReminder.Location = new System.Drawing.Point(143, 35);
			this.butActivateReminder.Name = "butActivateReminder";
			this.butActivateReminder.Size = new System.Drawing.Size(147, 23);
			this.butActivateReminder.TabIndex = 267;
			this.butActivateReminder.Text = "Activate eReminders";
			this.butActivateReminder.UseVisualStyleBackColor = true;
			this.butActivateReminder.Click+=new System.EventHandler(this.butActivateReminder_Click);
			// 
			// textStatusConfirmations
			// 
			this.textStatusConfirmations.Location = new System.Drawing.Point(6, 66);
			this.textStatusConfirmations.Name = "textStatusConfirmations";
			this.textStatusConfirmations.ReadOnly = true;
			this.textStatusConfirmations.Size = new System.Drawing.Size(131, 20);
			this.textStatusConfirmations.TabIndex = 266;
			this.textStatusConfirmations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// butActivateConfirm
			// 
			this.butActivateConfirm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butActivateConfirm.Autosize = true;
			this.butActivateConfirm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butActivateConfirm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butActivateConfirm.CornerRadius = 4F;
			this.butActivateConfirm.Location = new System.Drawing.Point(143, 64);
			this.butActivateConfirm.Name = "butActivateConfirm";
			this.butActivateConfirm.Size = new System.Drawing.Size(147, 23);
			this.butActivateConfirm.TabIndex = 265;
			this.butActivateConfirm.Text = "Activate eConfirmations";
			this.butActivateConfirm.UseVisualStyleBackColor = true;
			this.butActivateConfirm.Click+=new System.EventHandler(this.butActivateConfirm_Click);
			// 
			// butAddReminderFutureDay
			// 
			this.butAddReminderFutureDay.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddReminderFutureDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddReminderFutureDay.Autosize = true;
			this.butAddReminderFutureDay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddReminderFutureDay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddReminderFutureDay.CornerRadius = 4F;
			this.butAddReminderFutureDay.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddReminderFutureDay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddReminderFutureDay.Location = new System.Drawing.Point(587, 558);
			this.butAddReminderFutureDay.Name = "butAddReminderFutureDay";
			this.butAddReminderFutureDay.Size = new System.Drawing.Size(162, 24);
			this.butAddReminderFutureDay.TabIndex = 264;
			this.butAddReminderFutureDay.Text = "Add Future Day Reminder";
			this.butAddReminderFutureDay.UseVisualStyleBackColor = true;
			this.butAddReminderFutureDay.Click += new System.EventHandler(this.butAddReminderFutureDay_Click);
			// 
			// checkUseDefaultsEC
			// 
			this.checkUseDefaultsEC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkUseDefaultsEC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUseDefaultsEC.Location = new System.Drawing.Point(755, 561);
			this.checkUseDefaultsEC.Name = "checkUseDefaultsEC";
			this.checkUseDefaultsEC.Size = new System.Drawing.Size(105, 19);
			this.checkUseDefaultsEC.TabIndex = 263;
			this.checkUseDefaultsEC.Text = "Use Defaults";
			this.checkUseDefaultsEC.Click += new System.EventHandler(this.checkUseDefaultsEC_CheckedChanged);
			// 
			// checkIsConfirmEnabled
			// 
			this.checkIsConfirmEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsConfirmEnabled.Location = new System.Drawing.Point(630, 9);
			this.checkIsConfirmEnabled.Name = "checkIsConfirmEnabled";
			this.checkIsConfirmEnabled.Size = new System.Drawing.Size(216, 19);
			this.checkIsConfirmEnabled.TabIndex = 167;
			this.checkIsConfirmEnabled.Text = "Enable Automation for Clinic";
			this.checkIsConfirmEnabled.CheckedChanged += new System.EventHandler(this.checkIsConfirmEnabled_CheckedChanged);
			// 
			// comboClinicEConfirm
			// 
			this.comboClinicEConfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinicEConfirm.Location = new System.Drawing.Point(427, 8);
			this.comboClinicEConfirm.MaxDropDownItems = 30;
			this.comboClinicEConfirm.Name = "comboClinicEConfirm";
			this.comboClinicEConfirm.Size = new System.Drawing.Size(194, 21);
			this.comboClinicEConfirm.TabIndex = 164;
			this.comboClinicEConfirm.SelectedIndexChanged += new System.EventHandler(this.comboClinicEConfirm_SelectedIndexChanged);
			// 
			// label54
			// 
			this.label54.Location = new System.Drawing.Point(364, 9);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(57, 16);
			this.label54.TabIndex = 165;
			this.label54.Text = "Clinic";
			this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butAddConfirmation
			// 
			this.butAddConfirmation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddConfirmation.Autosize = true;
			this.butAddConfirmation.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddConfirmation.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddConfirmation.CornerRadius = 4F;
			this.butAddConfirmation.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddConfirmation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddConfirmation.Location = new System.Drawing.Point(296, 558);
			this.butAddConfirmation.Name = "butAddConfirmation";
			this.butAddConfirmation.Size = new System.Drawing.Size(119, 24);
			this.butAddConfirmation.TabIndex = 93;
			this.butAddConfirmation.Text = "Add Confirmation";
			this.butAddConfirmation.UseVisualStyleBackColor = true;
			this.butAddConfirmation.Click += new System.EventHandler(this.butAddConfirmation_Click);
			// 
			// butAddReminderSameDay
			// 
			this.butAddReminderSameDay.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddReminderSameDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddReminderSameDay.Autosize = true;
			this.butAddReminderSameDay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddReminderSameDay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddReminderSameDay.CornerRadius = 4F;
			this.butAddReminderSameDay.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddReminderSameDay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddReminderSameDay.Location = new System.Drawing.Point(421, 558);
			this.butAddReminderSameDay.Name = "butAddReminderSameDay";
			this.butAddReminderSameDay.Size = new System.Drawing.Size(160, 24);
			this.butAddReminderSameDay.TabIndex = 92;
			this.butAddReminderSameDay.Text = "Add Same-Day Reminder";
			this.butAddReminderSameDay.UseVisualStyleBackColor = true;
			this.butAddReminderSameDay.Click += new System.EventHandler(this.butAddReminderSameDay_Click);
			// 
			// gridRemindersMain
			// 
			this.gridRemindersMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridRemindersMain.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridRemindersMain.HasAddButton = false;
			this.gridRemindersMain.HasDropDowns = false;
			this.gridRemindersMain.HasMultilineHeaders = true;
			this.gridRemindersMain.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridRemindersMain.HeaderHeight = 15;
			this.gridRemindersMain.HScrollVisible = false;
			this.gridRemindersMain.Location = new System.Drawing.Point(296, 35);
			this.gridRemindersMain.Name = "gridRemindersMain";
			this.gridRemindersMain.ScrollValue = 0;
			this.gridRemindersMain.Size = new System.Drawing.Size(852, 517);
			this.gridRemindersMain.TabIndex = 68;
			this.gridRemindersMain.Title = "eReminder and eConfirmation Rules";
			this.gridRemindersMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridRemindersMain.TitleHeight = 18;
			this.gridRemindersMain.TranslationName = null;
			this.gridRemindersMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridRemindersMain_CellDoubleClick);
			// 
			// tabMisc
			// 
			this.tabMisc.BackColor = System.Drawing.SystemColors.Control;
			this.tabMisc.Controls.Add(this.groupNotUsed);
			this.tabMisc.Controls.Add(this.groupBox8);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(1154, 588);
			this.tabMisc.TabIndex = 7;
			this.tabMisc.Text = "Miscellaneous";
			// 
			// groupNotUsed
			// 
			this.groupNotUsed.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupNotUsed.Controls.Add(this.butShowOldMobileSych);
			this.groupNotUsed.Location = new System.Drawing.Point(152, 94);
			this.groupNotUsed.Name = "groupNotUsed";
			this.groupNotUsed.Size = new System.Drawing.Size(851, 58);
			this.groupNotUsed.TabIndex = 250;
			this.groupNotUsed.TabStop = false;
			this.groupNotUsed.Text = "No Longer Used";
			this.groupNotUsed.Visible = false;
			// 
			// butShowOldMobileSych
			// 
			this.butShowOldMobileSych.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowOldMobileSych.Autosize = true;
			this.butShowOldMobileSych.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowOldMobileSych.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowOldMobileSych.CornerRadius = 4F;
			this.butShowOldMobileSych.Location = new System.Drawing.Point(9, 21);
			this.butShowOldMobileSych.Name = "butShowOldMobileSych";
			this.butShowOldMobileSych.Size = new System.Drawing.Size(225, 24);
			this.butShowOldMobileSych.TabIndex = 249;
			this.butShowOldMobileSych.Text = "Show Mobile Synch (old-style)";
			this.butShowOldMobileSych.Visible = false;
			this.butShowOldMobileSych.Click += new System.EventHandler(this.butShowOldMobileSych_Click);
			// 
			// groupBox8
			// 
			this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox8.Controls.Add(this.dateRunEnd);
			this.groupBox8.Controls.Add(this.label46);
			this.groupBox8.Controls.Add(this.dateRunStart);
			this.groupBox8.Controls.Add(this.label47);
			this.groupBox8.Controls.Add(this.label48);
			this.groupBox8.Location = new System.Drawing.Point(152, 7);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(851, 81);
			this.groupBox8.TabIndex = 76;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Automated eServices Schedule";
			// 
			// dateRunEnd
			// 
			this.dateRunEnd.CustomFormat = " ";
			this.dateRunEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateRunEnd.Location = new System.Drawing.Point(144, 56);
			this.dateRunEnd.Name = "dateRunEnd";
			this.dateRunEnd.ShowUpDown = true;
			this.dateRunEnd.Size = new System.Drawing.Size(90, 20);
			this.dateRunEnd.TabIndex = 7;
			this.dateRunEnd.Value = new System.DateTime(2015, 11, 3, 22, 0, 0, 0);
			// 
			// label46
			// 
			this.label46.Location = new System.Drawing.Point(6, 16);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(838, 13);
			this.label46.TabIndex = 72;
			this.label46.Text = "This applies to eConfirmations, eReminders, and WebSched recall notifications. It" +
    " dictates the time interval that the service will automatically notify patients." +
    "";
			// 
			// dateRunStart
			// 
			this.dateRunStart.CustomFormat = " ";
			this.dateRunStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateRunStart.Location = new System.Drawing.Point(144, 36);
			this.dateRunStart.Name = "dateRunStart";
			this.dateRunStart.ShowUpDown = true;
			this.dateRunStart.Size = new System.Drawing.Size(90, 20);
			this.dateRunStart.TabIndex = 6;
			this.dateRunStart.Value = new System.DateTime(2015, 11, 3, 7, 0, 0, 0);
			// 
			// label47
			// 
			this.label47.Location = new System.Drawing.Point(12, 58);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(126, 17);
			this.label47.TabIndex = 5;
			this.label47.Text = "End Time";
			this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label48
			// 
			this.label48.Location = new System.Drawing.Point(12, 38);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(126, 17);
			this.label48.TabIndex = 4;
			this.label48.Text = "Start Time";
			this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormEServicesSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1184, 692);
			this.Controls.Add(this.labelAutoSave);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(990, 731);
			this.Name = "FormEServicesSetup";
			this.Text = "eServices Setup";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEServicesSetup_FormClosing);
			this.Load += new System.EventHandler(this.FormEServicesSetup_Load);
			this.tabControl.ResumeLayout(false);
			this.tabSignup.ResumeLayout(false);
			this.tabEConnector.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabMobileSynch.ResumeLayout(false);
			this.groupPreferences.ResumeLayout(false);
			this.groupPreferences.PerformLayout();
			this.tabMobileWeb.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabPatientPortal.ResumeLayout(false);
			this.tabPatientPortal.PerformLayout();
			this.groupBoxNotification.ResumeLayout(false);
			this.groupBoxNotification.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabWebSched.ResumeLayout(false);
			this.tabControlWebSched.ResumeLayout(false);
			this.tabWebSchedRecalls.ResumeLayout(false);
			this.groupWebSchedText.ResumeLayout(false);
			this.groupWebSchedText.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBoxWebSchedAutomation.ResumeLayout(false);
			this.groupWebSchedPreview.ResumeLayout(false);
			this.groupWebSchedPreview.PerformLayout();
			this.tabWebSchedNewPatAppts.ResumeLayout(false);
			this.tabWebSchedNewPatAppts.PerformLayout();
			this.groupBox13.ResumeLayout(false);
			this.groupBox13.PerformLayout();
			this.groupBox11.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.tabTexting.ResumeLayout(false);
			this.tabECR.ResumeLayout(false);
			this.tabECR.PerformLayout();
			this.groupAutomationStatuses.ResumeLayout(false);
			this.tabMisc.ResumeLayout(false);
			this.groupNotUsed.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textHostedUrlPortal;
		private System.Windows.Forms.TextBox textBoxNotificationSubject;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxNotificationBody;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBoxNotification;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPatientFacingUrlPortal;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPatientPortal;
		private UI.Button butClose;
		private System.Windows.Forms.TabPage tabMobileSynch;
		private UI.Button butSaveListenerPort;
		private System.Windows.Forms.CheckBox checkTroubleshooting;
		private UI.Button butDelete;
		private System.Windows.Forms.Label textDateTimeLastRun;
		private System.Windows.Forms.GroupBox groupPreferences;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textMobileUserName;
		private System.Windows.Forms.Label label15;
		private UI.Button butCurrentWorkstation;
		private System.Windows.Forms.TextBox textMobilePassword;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox textMobileSynchWorkStation;
		private ValidNumber textSynchMinutes;
		private System.Windows.Forms.Label label18;
		private UI.Button butSaveMobileSynch;
		private ValidDate textDateBefore;
		private System.Windows.Forms.Label labelMobileSynchURL;
		private System.Windows.Forms.TextBox textMobileSyncServerURL;
		private System.Windows.Forms.Label labelMinutesBetweenSynch;
		private System.Windows.Forms.Label label19;
		private UI.Button butFullSync;
		private UI.Button butSync;
		private System.Windows.Forms.TabPage tabWebSched;
		private System.Windows.Forms.Label labelWebSchedDesc;
		private UI.Button butRecallSchedSetup;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TabPage tabEConnector;
		private System.Windows.Forms.GroupBox groupBox3;
		private UI.Button butStartListenerService;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label labelListenerStatus;
		private UI.Button butListenerAlertsOff;
		private System.Windows.Forms.TextBox textListenerServiceStatus;
		private System.Windows.Forms.Label label23;
		private UI.ODGrid gridListenerServiceStatusHistory;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private UI.Button butListenerServiceHistoryRefresh;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label labelListenerServiceAck;
		private UI.Button butListenerServiceAck;
		private System.Windows.Forms.TabPage tabTexting;
		private UI.ODGrid gridSmsSummary;
		private System.Windows.Forms.DateTimePicker dateTimePickerSms;
		private UI.Button butBackMonth;
		private UI.Button butFwdMonth;
		private UI.Button butThisMonth;
		private UI.ODGrid gridWebSchedRecallTypes;
		private System.Windows.Forms.Label label35;
		private UI.ODGrid gridWebSchedTimeSlots;
		private System.Windows.Forms.GroupBox groupWebSchedPreview;
		private System.Windows.Forms.Label labelWebSchedClinic;
		private System.Windows.Forms.Label labelWebSchedRecallTypes;
		private System.Windows.Forms.ComboBox comboWebSchedClinic;
		private System.Windows.Forms.ComboBox comboWebSchedRecallTypes;
		private ValidDate textWebSchedDateStart;
		private UI.Button butWebSchedToday;
		private UI.ODGrid gridWebSchedOperatories;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ListBox listBoxWebSchedProviderPref;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox comboWebSchedProviders;
		private UI.Button butWebSchedPickClinic;
		private UI.Button butWebSchedPickProv;
		private System.Windows.Forms.Label label37;
		private UI.Button butInstallEConnector;
		private System.Windows.Forms.CheckBox checkAllowEConnectorComm;
		private System.Windows.Forms.TextBox textEConnectorListeningType;
		private System.Windows.Forms.Label label38;
		private UI.Button butDefaultClinic;
		private UI.Button butDefaultClinicClear;
		private System.Windows.Forms.TabControl tabControlWebSched;
		private System.Windows.Forms.TabPage tabWebSchedRecalls;
		private System.Windows.Forms.TabPage tabWebSchedNewPatAppts;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label41;
		private ValidNumber textWebSchedNewPatApptSearchDays;
		private System.Windows.Forms.Label label40;
		private UI.ODGrid gridWebSchedNewPatApptOps;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.GroupBox groupBox7;
		private UI.Button butWebSchedNewPatApptsToday;
		private UI.ODGrid gridWebSchedNewPatApptTimeSlots;
		private ValidDate textWebSchedNewPatApptsDateStart;
		private UI.ODGrid gridWebSchedNewPatApptProcs;
		private UI.Button butWebSchedNewPatApptsAdd;
		private UI.Button butWebSchedNewPatApptsRemove;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.TextBox textWebSchedNewPatApptLength;
		private System.Windows.Forms.Label label44;
		private UI.ODGrid gridWebSchedNewPatApptURLs;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ContextMenu menuWebSchedNewPatApptHostedURLsRightClick;
		private System.Windows.Forms.MenuItem menuItemCopyURL;
		private System.Windows.Forms.MenuItem menuItemNavigateToURL;
		private System.Windows.Forms.LinkLabel linkLabelAboutWebSched;
		private System.Windows.Forms.GroupBox groupBoxWebSchedAutomation;
		private System.Windows.Forms.RadioButton radioDoNotSend;
		private System.Windows.Forms.RadioButton radioSendToEmailOnlyPreferred;
		private System.Windows.Forms.RadioButton radioSendToEmailNoPreferred;
		private System.Windows.Forms.RadioButton radioSendToEmail;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.DateTimePicker dateRunEnd;
		private System.Windows.Forms.DateTimePicker dateRunStart;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.TabPage tabECR;
		private System.Windows.Forms.CheckBox checkIsConfirmEnabled;
		private System.Windows.Forms.ComboBox comboClinicEConfirm;
		private System.Windows.Forms.Label label54;
		private UI.Button butAddConfirmation;
		private UI.Button butAddReminderSameDay;
		private UI.ODGrid gridRemindersMain;
		private System.Windows.Forms.CheckBox checkUseDefaultsEC;
		private UI.Button butAddReminderFutureDay;
		private System.Windows.Forms.TabPage tabSignup;
		private System.Windows.Forms.WebBrowser webBrowserSignup;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.ListBox listboxWebSchedRecallIgnoreBlockoutTypes;
		private System.Windows.Forms.Label label32;
		private UI.Button butWebSchedRecallBlockouts;
		private System.Windows.Forms.GroupBox groupBox13;
		private System.Windows.Forms.TextBox textWebSchedNewPatApptMessage;
		private System.Windows.Forms.GroupBox groupBox11;
		private System.Windows.Forms.ListBox listboxWebSchedNewPatIgnoreBlockoutTypes;
		private System.Windows.Forms.Label label33;
		private UI.Button butWebSchedNewPatBlockouts;
		private System.Windows.Forms.TabPage tabMobileWeb;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textHostedUrlMobileWeb;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupNotUsed;
		private UI.Button butShowOldMobileSych;
		private System.Windows.Forms.GroupBox groupBox5;
		private UI.Button butSetupMobileWebUsers;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.GroupBox groupWebSchedText;
		private System.Windows.Forms.RadioButton radioDoNotSendText;
		private System.Windows.Forms.RadioButton radioSendText;
		private System.Windows.Forms.Label labelAutoSave;
		private System.Windows.Forms.Label labelWebSchedPerBatch;
		private ValidNumber textWebSchedPerBatch;
		private UI.ODGrid gridConfStatuses;
		private System.Windows.Forms.GroupBox groupAutomationStatuses;
		private System.Windows.Forms.ComboBox comboStatusEFailed;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.CheckBox checkEnableNoClinic;
		private System.Windows.Forms.ComboBox comboStatusEDeclined;
		private System.Windows.Forms.ComboBox comboStatusESent;
		private System.Windows.Forms.ComboBox comboStatusEAccepted;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.TextBox textStatusReminders;
		private UI.Button butActivateReminder;
		private System.Windows.Forms.TextBox textStatusConfirmations;
		private UI.Button butActivateConfirm;
		private System.Windows.Forms.Label label11;
	}
}