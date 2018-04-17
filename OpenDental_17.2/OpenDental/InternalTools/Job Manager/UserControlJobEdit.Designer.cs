namespace OpenDental.InternalTools.Job_Manager {
	partial class UserControlJobEdit {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.groupLinks = new System.Windows.Forms.GroupBox();
			this.gridFiles = new OpenDental.UI.ODGrid();
			this.gridBugs = new OpenDental.UI.ODGrid();
			this.gridFeatureReq = new OpenDental.UI.ODGrid();
			this.gridTasks = new OpenDental.UI.ODGrid();
			this.gridCustomerQuotes = new OpenDental.UI.ODGrid();
			this.gridWatchers = new OpenDental.UI.ODGrid();
			this.label10 = new System.Windows.Forms.Label();
			this.textVersion = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textDateEntry = new System.Windows.Forms.TextBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.textJobNum = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textApprove = new System.Windows.Forms.TextBox();
			this.timerTitle = new System.Windows.Forms.Timer(this.components);
			this.timerVersion = new System.Windows.Forms.Timer(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainerNoFlicker1 = new OpenDental.SplitContainerNoFlicker();
			this.gridRoles = new OpenDental.UI.ODGrid();
			this.labelRelatedJobs = new System.Windows.Forms.Label();
			this.treeRelatedJobs = new System.Windows.Forms.TreeView();
			this.butActions = new OpenDental.UI.Button();
			this.textEditorMain = new OpenDental.OdtextEditor();
			this.butParentPick = new OpenDental.UI.Button();
			this.textParent = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textActualHours = new OpenDental.ValidNumber();
			this.label8 = new System.Windows.Forms.Label();
			this.butParentRemove = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textEstHours = new OpenDental.ValidNumber();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.gridNotes = new OpenDental.UI.ODGrid();
			this.tabReviews = new System.Windows.Forms.TabPage();
			this.gridReview = new OpenDental.UI.ODGrid();
			this.tabDocumentation = new System.Windows.Forms.TabPage();
			this.textEditorDocumentation = new OpenDental.OdtextEditor();
			this.tabHistory = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.gridHistory = new OpenDental.UI.ODGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkShowHistoryText = new System.Windows.Forms.CheckBox();
			this.butVersionPrompt = new OpenDental.UI.Button();
			this.groupLinks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerNoFlicker1)).BeginInit();
			this.splitContainerNoFlicker1.Panel1.SuspendLayout();
			this.splitContainerNoFlicker1.Panel2.SuspendLayout();
			this.splitContainerNoFlicker1.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabReviews.SuspendLayout();
			this.tabDocumentation.SuspendLayout();
			this.tabHistory.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupLinks
			// 
			this.groupLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupLinks.Controls.Add(this.gridFiles);
			this.groupLinks.Controls.Add(this.gridBugs);
			this.groupLinks.Controls.Add(this.gridFeatureReq);
			this.groupLinks.Controls.Add(this.gridTasks);
			this.groupLinks.Controls.Add(this.gridCustomerQuotes);
			this.groupLinks.Controls.Add(this.gridWatchers);
			this.groupLinks.Location = new System.Drawing.Point(777, 47);
			this.groupLinks.Name = "groupLinks";
			this.groupLinks.Size = new System.Drawing.Size(233, 676);
			this.groupLinks.TabIndex = 296;
			this.groupLinks.TabStop = false;
			this.groupLinks.Text = "Links";
			this.groupLinks.Resize += new System.EventHandler(this.groupLinks_Resize);
			// 
			// gridFiles
			// 
			this.gridFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridFiles.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridFiles.HasAddButton = true;
			this.gridFiles.HasDropDowns = false;
			this.gridFiles.HasMultilineHeaders = false;
			this.gridFiles.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridFiles.HeaderHeight = 15;
			this.gridFiles.HScrollVisible = false;
			this.gridFiles.Location = new System.Drawing.Point(5, 503);
			this.gridFiles.Name = "gridFiles";
			this.gridFiles.ScrollValue = 0;
			this.gridFiles.Size = new System.Drawing.Size(223, 91);
			this.gridFiles.TabIndex = 260;
			this.gridFiles.Title = "Files";
			this.gridFiles.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridFiles.TitleHeight = 18;
			this.gridFiles.TranslationName = "";
			this.gridFiles.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFiles_CellDoubleClick);
			this.gridFiles.TitleAddClick += new System.EventHandler(this.gridFiles_TitleAddClick);
			// 
			// gridBugs
			// 
			this.gridBugs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridBugs.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridBugs.HasAddButton = true;
			this.gridBugs.HasDropDowns = false;
			this.gridBugs.HasMultilineHeaders = false;
			this.gridBugs.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridBugs.HeaderHeight = 15;
			this.gridBugs.HScrollVisible = false;
			this.gridBugs.Location = new System.Drawing.Point(5, 406);
			this.gridBugs.Name = "gridBugs";
			this.gridBugs.ScrollValue = 0;
			this.gridBugs.Size = new System.Drawing.Size(223, 91);
			this.gridBugs.TabIndex = 259;
			this.gridBugs.Title = "Bugs/Enhancements";
			this.gridBugs.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridBugs.TitleHeight = 18;
			this.gridBugs.TranslationName = "FormTaskEdit";
			this.gridBugs.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBugs_CellDoubleClick);
			this.gridBugs.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBugs_CellClick);
			this.gridBugs.TitleAddClick += new System.EventHandler(this.gridBugs_TitleAddClick);
			// 
			// gridFeatureReq
			// 
			this.gridFeatureReq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridFeatureReq.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridFeatureReq.HasAddButton = true;
			this.gridFeatureReq.HasDropDowns = false;
			this.gridFeatureReq.HasMultilineHeaders = false;
			this.gridFeatureReq.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridFeatureReq.HeaderHeight = 15;
			this.gridFeatureReq.HScrollVisible = false;
			this.gridFeatureReq.Location = new System.Drawing.Point(5, 309);
			this.gridFeatureReq.Name = "gridFeatureReq";
			this.gridFeatureReq.ScrollValue = 0;
			this.gridFeatureReq.Size = new System.Drawing.Size(223, 91);
			this.gridFeatureReq.TabIndex = 228;
			this.gridFeatureReq.Title = "Feature Requests";
			this.gridFeatureReq.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridFeatureReq.TitleHeight = 18;
			this.gridFeatureReq.TranslationName = "FormTaskEdit";
			this.gridFeatureReq.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFeatureReq_CellDoubleClick);
			this.gridFeatureReq.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFeatureReq_CellClick);
			this.gridFeatureReq.TitleAddClick += new System.EventHandler(this.gridFeatureReq_TitleAddClick);
			// 
			// gridTasks
			// 
			this.gridTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridTasks.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridTasks.HasAddButton = true;
			this.gridTasks.HasDropDowns = false;
			this.gridTasks.HasMultilineHeaders = false;
			this.gridTasks.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridTasks.HeaderHeight = 15;
			this.gridTasks.HScrollVisible = false;
			this.gridTasks.Location = new System.Drawing.Point(5, 212);
			this.gridTasks.Name = "gridTasks";
			this.gridTasks.ScrollValue = 0;
			this.gridTasks.Size = new System.Drawing.Size(223, 91);
			this.gridTasks.TabIndex = 227;
			this.gridTasks.Title = "Tasks";
			this.gridTasks.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridTasks.TitleHeight = 18;
			this.gridTasks.TranslationName = "FormTaskEdit";
			this.gridTasks.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridTasks_CellDoubleClick);
			this.gridTasks.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridTasks_CellClick);
			this.gridTasks.TitleAddClick += new System.EventHandler(this.gridTasks_TitleAddClick);
			// 
			// gridCustomerQuotes
			// 
			this.gridCustomerQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridCustomerQuotes.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridCustomerQuotes.HasAddButton = true;
			this.gridCustomerQuotes.HasDropDowns = false;
			this.gridCustomerQuotes.HasMultilineHeaders = false;
			this.gridCustomerQuotes.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridCustomerQuotes.HeaderHeight = 15;
			this.gridCustomerQuotes.HScrollVisible = false;
			this.gridCustomerQuotes.Location = new System.Drawing.Point(5, 115);
			this.gridCustomerQuotes.Name = "gridCustomerQuotes";
			this.gridCustomerQuotes.ScrollValue = 0;
			this.gridCustomerQuotes.Size = new System.Drawing.Size(223, 91);
			this.gridCustomerQuotes.TabIndex = 226;
			this.gridCustomerQuotes.Title = "Customers and Quotes";
			this.gridCustomerQuotes.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridCustomerQuotes.TitleHeight = 18;
			this.gridCustomerQuotes.TranslationName = "FormTaskEdit";
			this.gridCustomerQuotes.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridCustomerQuotes_CellDoubleClick);
			this.gridCustomerQuotes.TitleAddClick += new System.EventHandler(this.gridCustomerQuotes_TitleAddClick);
			// 
			// gridWatchers
			// 
			this.gridWatchers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridWatchers.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridWatchers.HasAddButton = true;
			this.gridWatchers.HasDropDowns = false;
			this.gridWatchers.HasMultilineHeaders = false;
			this.gridWatchers.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridWatchers.HeaderHeight = 15;
			this.gridWatchers.HScrollVisible = false;
			this.gridWatchers.Location = new System.Drawing.Point(5, 19);
			this.gridWatchers.Name = "gridWatchers";
			this.gridWatchers.ScrollValue = 0;
			this.gridWatchers.Size = new System.Drawing.Size(223, 91);
			this.gridWatchers.TabIndex = 225;
			this.gridWatchers.Title = "Watchers";
			this.gridWatchers.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridWatchers.TitleHeight = 18;
			this.gridWatchers.TranslationName = "FormTaskEdit";
			this.gridWatchers.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridWatchers_CellClick);
			this.gridWatchers.TitleAddClick += new System.EventHandler(this.gridWatchers_TitleAddClick);
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(733, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(63, 20);
			this.label10.TabIndex = 291;
			this.label10.Text = "Date Entry";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textVersion
			// 
			this.textVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textVersion.Location = new System.Drawing.Point(840, 21);
			this.textVersion.MaxLength = 100;
			this.textVersion.Name = "textVersion";
			this.textVersion.Size = new System.Drawing.Size(146, 20);
			this.textVersion.TabIndex = 294;
			this.textVersion.TextChanged += new System.EventHandler(this.textVersion_TextChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(837, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 20);
			this.label6.TabIndex = 292;
			this.label6.Text = "Version";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDateEntry
			// 
			this.textDateEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textDateEntry.Location = new System.Drawing.Point(736, 21);
			this.textDateEntry.MaxLength = 100;
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(98, 20);
			this.textDateEntry.TabIndex = 293;
			this.textDateEntry.TabStop = false;
			// 
			// comboStatus
			// 
			this.comboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Enabled = false;
			this.comboStatus.FormattingEnabled = true;
			this.comboStatus.Location = new System.Drawing.Point(410, 21);
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(117, 21);
			this.comboStatus.TabIndex = 290;
			this.comboStatus.SelectionChangeCommitted += new System.EventHandler(this.comboStatus_SelectionChangeCommitted);
			// 
			// comboCategory
			// 
			this.comboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCategory.FormattingEnabled = true;
			this.comboCategory.Location = new System.Drawing.Point(613, 21);
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(117, 21);
			this.comboCategory.TabIndex = 287;
			this.comboCategory.SelectionChangeCommitted += new System.EventHandler(this.comboCategory_SelectionChangeCommitted);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(3, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(65, 20);
			this.label12.TabIndex = 289;
			this.label12.Text = "Title";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboPriority
			// 
			this.comboPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.FormattingEnabled = true;
			this.comboPriority.Location = new System.Drawing.Point(287, 21);
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(117, 21);
			this.comboPriority.TabIndex = 286;
			this.comboPriority.SelectionChangeCommitted += new System.EventHandler(this.comboPriority_SelectionChangeCommitted);
			// 
			// textTitle
			// 
			this.textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textTitle.Location = new System.Drawing.Point(3, 21);
			this.textTitle.MaxLength = 255;
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(187, 20);
			this.textTitle.TabIndex = 288;
			this.textTitle.TextChanged += new System.EventHandler(this.textTitle_TextChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(610, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 20);
			this.label4.TabIndex = 282;
			this.label4.Text = "Category";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(407, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 20);
			this.label5.TabIndex = 283;
			this.label5.Text = "Phase";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(284, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 20);
			this.label3.TabIndex = 281;
			this.label3.Text = "Priority";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label19
			// 
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label19.Location = new System.Drawing.Point(196, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(61, 20);
			this.label19.TabIndex = 284;
			this.label19.Text = "JobNum";
			this.label19.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textJobNum
			// 
			this.textJobNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textJobNum.Location = new System.Drawing.Point(196, 21);
			this.textJobNum.MaxLength = 100;
			this.textJobNum.Name = "textJobNum";
			this.textJobNum.ReadOnly = true;
			this.textJobNum.Size = new System.Drawing.Size(85, 20);
			this.textJobNum.TabIndex = 285;
			this.textJobNum.TabStop = false;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(533, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 20);
			this.label2.TabIndex = 302;
			this.label2.Text = "Approved";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textApprove
			// 
			this.textApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textApprove.Location = new System.Drawing.Point(533, 21);
			this.textApprove.MaxLength = 100;
			this.textApprove.Name = "textApprove";
			this.textApprove.ReadOnly = true;
			this.textApprove.Size = new System.Drawing.Size(74, 20);
			this.textApprove.TabIndex = 303;
			this.textApprove.TabStop = false;
			this.textApprove.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textApprove_MouseDoubleClick);
			// 
			// timerTitle
			// 
			this.timerTitle.Interval = 3000;
			this.timerTitle.Tick += new System.EventHandler(this.timerTitle_Tick);
			// 
			// timerVersion
			// 
			this.timerVersion.Interval = 3000;
			this.timerVersion.Tick += new System.EventHandler(this.timerVersion_Tick);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitContainer2.Location = new System.Drawing.Point(3, 48);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer2.Panel1.Controls.Add(this.splitContainerNoFlicker1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer2.Panel2.Controls.Add(this.tabControlMain);
			this.splitContainer2.Panel2MinSize = 250;
			this.splitContainer2.Size = new System.Drawing.Size(768, 675);
			this.splitContainer2.SplitterDistance = 383;
			this.splitContainer2.TabIndex = 301;
			// 
			// splitContainerNoFlicker1
			// 
			this.splitContainerNoFlicker1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerNoFlicker1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerNoFlicker1.Name = "splitContainerNoFlicker1";
			// 
			// splitContainerNoFlicker1.Panel1
			// 
			this.splitContainerNoFlicker1.Panel1.Controls.Add(this.gridRoles);
			this.splitContainerNoFlicker1.Panel1.Controls.Add(this.labelRelatedJobs);
			this.splitContainerNoFlicker1.Panel1.Controls.Add(this.treeRelatedJobs);
			this.splitContainerNoFlicker1.Panel1.Controls.Add(this.butActions);
			// 
			// splitContainerNoFlicker1.Panel2
			// 
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.textEditorMain);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.butParentPick);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.textParent);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.label1);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.textActualHours);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.label8);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.butParentRemove);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.label7);
			this.splitContainerNoFlicker1.Panel2.Controls.Add(this.textEstHours);
			this.splitContainerNoFlicker1.Size = new System.Drawing.Size(768, 383);
			this.splitContainerNoFlicker1.SplitterDistance = 186;
			this.splitContainerNoFlicker1.TabIndex = 310;
			// 
			// gridRoles
			// 
			this.gridRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridRoles.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridRoles.HasAddButton = false;
			this.gridRoles.HasDropDowns = false;
			this.gridRoles.HasMultilineHeaders = false;
			this.gridRoles.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridRoles.HeaderHeight = 15;
			this.gridRoles.HScrollVisible = false;
			this.gridRoles.Location = new System.Drawing.Point(0, 31);
			this.gridRoles.Name = "gridRoles";
			this.gridRoles.ScrollValue = 0;
			this.gridRoles.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridRoles.Size = new System.Drawing.Size(184, 244);
			this.gridRoles.TabIndex = 304;
			this.gridRoles.Title = "JobRoles";
			this.gridRoles.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridRoles.TitleHeight = 18;
			this.gridRoles.TranslationName = "FormTaskEdit";
			// 
			// labelRelatedJobs
			// 
			this.labelRelatedJobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRelatedJobs.Location = new System.Drawing.Point(0, 278);
			this.labelRelatedJobs.Name = "labelRelatedJobs";
			this.labelRelatedJobs.Size = new System.Drawing.Size(184, 20);
			this.labelRelatedJobs.TabIndex = 309;
			this.labelRelatedJobs.Text = "Related Jobs";
			this.labelRelatedJobs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// treeRelatedJobs
			// 
			this.treeRelatedJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeRelatedJobs.Indent = 9;
			this.treeRelatedJobs.Location = new System.Drawing.Point(0, 301);
			this.treeRelatedJobs.Name = "treeRelatedJobs";
			this.treeRelatedJobs.Size = new System.Drawing.Size(184, 74);
			this.treeRelatedJobs.TabIndex = 308;
			this.treeRelatedJobs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRelatedJobs_AfterSelect);
			this.treeRelatedJobs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeRelatedJobs_NodeMouseClick);
			// 
			// butActions
			// 
			this.butActions.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butActions.Autosize = true;
			this.butActions.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butActions.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butActions.CornerRadius = 4F;
			this.butActions.Image = global::OpenDental.Properties.Resources.arrowDownTriangle;
			this.butActions.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butActions.Location = new System.Drawing.Point(0, 1);
			this.butActions.Name = "butActions";
			this.butActions.Size = new System.Drawing.Size(95, 24);
			this.butActions.TabIndex = 303;
			this.butActions.Text = "Job Actions";
			this.butActions.Click += new System.EventHandler(this.butActions_Click);
			// 
			// textEditorMain
			// 
			this.textEditorMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditorMain.HasEditorOptions = true;
			this.textEditorMain.HasSaveButton = true;
			this.textEditorMain.Location = new System.Drawing.Point(2, 0);
			this.textEditorMain.MainRtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}\r\n";
			this.textEditorMain.MainText = "";
			this.textEditorMain.MinimumSize = new System.Drawing.Size(450, 120);
			this.textEditorMain.Name = "textEditorMain";
			this.textEditorMain.ReadOnly = false;
			this.textEditorMain.Size = new System.Drawing.Size(576, 353);
			this.textEditorMain.TabIndex = 260;
			this.textEditorMain.SaveClick += new OpenDental.ODtextEditorSaveEventHandler(this.textEditor_SaveClick);
			this.textEditorMain.OnTextEdited += new OpenDental.OdtextEditor.textChangedEventHandler(this.textEditor_OnTextEdited);
			// 
			// butParentPick
			// 
			this.butParentPick.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butParentPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butParentPick.Autosize = true;
			this.butParentPick.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butParentPick.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butParentPick.CornerRadius = 4F;
			this.butParentPick.Location = new System.Drawing.Point(529, 358);
			this.butParentPick.Name = "butParentPick";
			this.butParentPick.Size = new System.Drawing.Size(23, 20);
			this.butParentPick.TabIndex = 307;
			this.butParentPick.Text = "...";
			this.butParentPick.Click += new System.EventHandler(this.butParentPick_Click);
			// 
			// textParent
			// 
			this.textParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textParent.Location = new System.Drawing.Point(314, 358);
			this.textParent.MaxLength = 100;
			this.textParent.Name = "textParent";
			this.textParent.ReadOnly = true;
			this.textParent.Size = new System.Drawing.Size(215, 20);
			this.textParent.TabIndex = 304;
			this.textParent.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(249, 358);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 20);
			this.label1.TabIndex = 305;
			this.label1.Text = "Parent Job";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textActualHours
			// 
			this.textActualHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textActualHours.Location = new System.Drawing.Point(197, 359);
			this.textActualHours.MaxVal = 255;
			this.textActualHours.MinVal = 0;
			this.textActualHours.Name = "textActualHours";
			this.textActualHours.Size = new System.Drawing.Size(46, 20);
			this.textActualHours.TabIndex = 270;
			this.textActualHours.TextChanged += new System.EventHandler(this.textActualHours_TextChanged);
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.Location = new System.Drawing.Point(130, 358);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 20);
			this.label8.TabIndex = 265;
			this.label8.Text = "Hrs. Act.";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butParentRemove
			// 
			this.butParentRemove.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butParentRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butParentRemove.Autosize = true;
			this.butParentRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butParentRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butParentRemove.CornerRadius = 4F;
			this.butParentRemove.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butParentRemove.Location = new System.Drawing.Point(552, 358);
			this.butParentRemove.Name = "butParentRemove";
			this.butParentRemove.Size = new System.Drawing.Size(23, 20);
			this.butParentRemove.TabIndex = 306;
			this.butParentRemove.Click += new System.EventHandler(this.butParentRemove_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(7, 358);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 20);
			this.label7.TabIndex = 264;
			this.label7.Text = "Hrs. Est.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textEstHours
			// 
			this.textEstHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textEstHours.Location = new System.Drawing.Point(74, 359);
			this.textEstHours.MaxVal = 255;
			this.textEstHours.MinVal = 0;
			this.textEstHours.Name = "textEstHours";
			this.textEstHours.Size = new System.Drawing.Size(46, 20);
			this.textEstHours.TabIndex = 269;
			this.textEstHours.TextChanged += new System.EventHandler(this.textEstHours_TextChanged);
			// 
			// tabControlMain
			// 
			this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlMain.Controls.Add(this.tabMain);
			this.tabControlMain.Controls.Add(this.tabReviews);
			this.tabControlMain.Controls.Add(this.tabDocumentation);
			this.tabControlMain.Controls.Add(this.tabHistory);
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(768, 285);
			this.tabControlMain.TabIndex = 261;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.gridNotes);
			this.tabMain.Location = new System.Drawing.Point(4, 22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabMain.Size = new System.Drawing.Size(760, 259);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Discussion";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// gridNotes
			// 
			this.gridNotes.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridNotes.HasAddButton = true;
			this.gridNotes.HasDropDowns = false;
			this.gridNotes.HasMultilineHeaders = false;
			this.gridNotes.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridNotes.HeaderHeight = 15;
			this.gridNotes.HScrollVisible = false;
			this.gridNotes.Location = new System.Drawing.Point(3, 3);
			this.gridNotes.Name = "gridNotes";
			this.gridNotes.ScrollValue = 0;
			this.gridNotes.Size = new System.Drawing.Size(754, 253);
			this.gridNotes.TabIndex = 194;
			this.gridNotes.Title = "Discussion";
			this.gridNotes.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridNotes.TitleHeight = 18;
			this.gridNotes.TranslationName = "FormTaskEdit";
			this.gridNotes.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridNotes_CellDoubleClick);
			this.gridNotes.TitleAddClick += new System.EventHandler(this.gridNotes_TitleAddClick);
			// 
			// tabReviews
			// 
			this.tabReviews.BackColor = System.Drawing.SystemColors.Control;
			this.tabReviews.Controls.Add(this.gridReview);
			this.tabReviews.Location = new System.Drawing.Point(4, 22);
			this.tabReviews.Name = "tabReviews";
			this.tabReviews.Padding = new System.Windows.Forms.Padding(3);
			this.tabReviews.Size = new System.Drawing.Size(760, 259);
			this.tabReviews.TabIndex = 2;
			this.tabReviews.Text = "Reviews";
			// 
			// gridReview
			// 
			this.gridReview.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridReview.HasAddButton = true;
			this.gridReview.HasDropDowns = false;
			this.gridReview.HasMultilineHeaders = false;
			this.gridReview.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridReview.HeaderHeight = 15;
			this.gridReview.HScrollVisible = false;
			this.gridReview.Location = new System.Drawing.Point(3, 3);
			this.gridReview.Name = "gridReview";
			this.gridReview.ScrollValue = 0;
			this.gridReview.Size = new System.Drawing.Size(754, 253);
			this.gridReview.TabIndex = 21;
			this.gridReview.TabStop = false;
			this.gridReview.Title = "Reviews";
			this.gridReview.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridReview.TitleHeight = 18;
			this.gridReview.TranslationName = null;
			this.gridReview.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridReview_CellDoubleClick);
			this.gridReview.TitleAddClick += new System.EventHandler(this.gridReview_TitleAddClick);
			// 
			// tabDocumentation
			// 
			this.tabDocumentation.BackColor = System.Drawing.SystemColors.Control;
			this.tabDocumentation.Controls.Add(this.textEditorDocumentation);
			this.tabDocumentation.Location = new System.Drawing.Point(4, 22);
			this.tabDocumentation.Name = "tabDocumentation";
			this.tabDocumentation.Padding = new System.Windows.Forms.Padding(3);
			this.tabDocumentation.Size = new System.Drawing.Size(760, 259);
			this.tabDocumentation.TabIndex = 4;
			this.tabDocumentation.Text = "Documentation";
			// 
			// textEditorDocumentation
			// 
			this.textEditorDocumentation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditorDocumentation.HasEditorOptions = true;
			this.textEditorDocumentation.HasSaveButton = true;
			this.textEditorDocumentation.Location = new System.Drawing.Point(3, 3);
			this.textEditorDocumentation.MainRtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}\r\n";
			this.textEditorDocumentation.MainText = "";
			this.textEditorDocumentation.MinimumSize = new System.Drawing.Size(450, 120);
			this.textEditorDocumentation.Name = "textEditorDocumentation";
			this.textEditorDocumentation.ReadOnly = false;
			this.textEditorDocumentation.Size = new System.Drawing.Size(754, 253);
			this.textEditorDocumentation.TabIndex = 261;
			this.textEditorDocumentation.SaveClick += new OpenDental.ODtextEditorSaveEventHandler(this.textEditor_SaveClick);
			this.textEditorDocumentation.OnTextEdited += new OpenDental.OdtextEditor.textChangedEventHandler(this.textEditor_OnTextEdited);
			// 
			// tabHistory
			// 
			this.tabHistory.BackColor = System.Drawing.SystemColors.Control;
			this.tabHistory.Controls.Add(this.panel2);
			this.tabHistory.Controls.Add(this.panel1);
			this.tabHistory.Location = new System.Drawing.Point(4, 22);
			this.tabHistory.Name = "tabHistory";
			this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabHistory.Size = new System.Drawing.Size(760, 259);
			this.tabHistory.TabIndex = 3;
			this.tabHistory.Text = "History";
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.gridHistory);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 20);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(754, 236);
			this.panel2.TabIndex = 247;
			// 
			// gridHistory
			// 
			this.gridHistory.AutoSize = true;
			this.gridHistory.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridHistory.HasAddButton = false;
			this.gridHistory.HasDropDowns = false;
			this.gridHistory.HasMultilineHeaders = false;
			this.gridHistory.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridHistory.HeaderHeight = 15;
			this.gridHistory.HScrollVisible = false;
			this.gridHistory.Location = new System.Drawing.Point(0, 0);
			this.gridHistory.Name = "gridHistory";
			this.gridHistory.ScrollValue = 0;
			this.gridHistory.Size = new System.Drawing.Size(754, 236);
			this.gridHistory.TabIndex = 19;
			this.gridHistory.TabStop = false;
			this.gridHistory.Title = "History Events";
			this.gridHistory.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridHistory.TitleHeight = 18;
			this.gridHistory.TranslationName = null;
			this.gridHistory.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridHistory_CellDoubleClick);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.checkShowHistoryText);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(754, 17);
			this.panel1.TabIndex = 246;
			// 
			// checkShowHistoryText
			// 
			this.checkShowHistoryText.AutoSize = true;
			this.checkShowHistoryText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkShowHistoryText.Location = new System.Drawing.Point(0, 0);
			this.checkShowHistoryText.Name = "checkShowHistoryText";
			this.checkShowHistoryText.Size = new System.Drawing.Size(754, 17);
			this.checkShowHistoryText.TabIndex = 245;
			this.checkShowHistoryText.Text = "Show Full Job Descriptions";
			this.checkShowHistoryText.UseVisualStyleBackColor = true;
			this.checkShowHistoryText.CheckedChanged += new System.EventHandler(this.checkShowHistoryText_CheckedChanged);
			// 
			// butVersionPrompt
			// 
			this.butVersionPrompt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butVersionPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butVersionPrompt.Autosize = true;
			this.butVersionPrompt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butVersionPrompt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butVersionPrompt.CornerRadius = 4F;
			this.butVersionPrompt.Location = new System.Drawing.Point(986, 21);
			this.butVersionPrompt.Name = "butVersionPrompt";
			this.butVersionPrompt.Size = new System.Drawing.Size(23, 20);
			this.butVersionPrompt.TabIndex = 308;
			this.butVersionPrompt.Text = "...";
			this.butVersionPrompt.Click += new System.EventHandler(this.butVersionPrompt_Click);
			// 
			// UserControlJobEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.butVersionPrompt);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textApprove);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.groupLinks);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textVersion);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.comboCategory);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.comboPriority);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.textJobNum);
			this.Name = "UserControlJobEdit";
			this.Size = new System.Drawing.Size(1013, 726);
			this.groupLinks.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainerNoFlicker1.Panel1.ResumeLayout(false);
			this.splitContainerNoFlicker1.Panel2.ResumeLayout(false);
			this.splitContainerNoFlicker1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerNoFlicker1)).EndInit();
			this.splitContainerNoFlicker1.ResumeLayout(false);
			this.tabControlMain.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabReviews.ResumeLayout(false);
			this.tabDocumentation.ResumeLayout(false);
			this.tabHistory.ResumeLayout(false);
			this.tabHistory.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer2;
		private OdtextEditor textEditorMain;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabMain;
		private UI.ODGrid gridNotes;
		private System.Windows.Forms.TabPage tabReviews;
		private UI.ODGrid gridReview;
		private System.Windows.Forms.TabPage tabDocumentation;
		private System.Windows.Forms.TabPage tabHistory;
		private UI.ODGrid gridHistory;
		private System.Windows.Forms.GroupBox groupLinks;
		private UI.ODGrid gridBugs;
		private UI.ODGrid gridFeatureReq;
		private UI.ODGrid gridTasks;
		private UI.ODGrid gridCustomerQuotes;
		private UI.ODGrid gridWatchers;
		private ValidNumber textEstHours;
		private ValidNumber textActualHours;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textVersion;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDateEntry;
		private System.Windows.Forms.ComboBox comboStatus;
		private System.Windows.Forms.ComboBox comboCategory;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboPriority;
		private System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textJobNum;
		private UI.Button butActions;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textParent;
		private UI.Button butParentRemove;
		private UI.Button butParentPick;
		private UI.ODGrid gridRoles;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textApprove;
		private OdtextEditor textEditorDocumentation;
		private System.Windows.Forms.Timer timerTitle;
		private System.Windows.Forms.Timer timerVersion;
		private System.Windows.Forms.Label labelRelatedJobs;
		private System.Windows.Forms.TreeView treeRelatedJobs;
		private UI.ODGrid gridFiles;
		private System.Windows.Forms.CheckBox checkShowHistoryText;
		private SplitContainerNoFlicker splitContainerNoFlicker1;
		private UI.Button butVersionPrompt;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
	}
}
