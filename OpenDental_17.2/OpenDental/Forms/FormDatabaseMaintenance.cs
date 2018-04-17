using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeBase;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary>
	/// Summary description for FormCheckDatabase.
	/// </summary>
	public class FormDatabaseMaintenance:ODForm {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.UI.Button butCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Drawing.Printing.PrintDocument pd2;
		private CheckBox checkShow;
		private UI.Button butFix;
		private Label label6;
		private UI.Button butInnoDB;
		private Label label5;
		private Label labelApptProcs;
		private Label label3;
		private Label label2;
		private UI.Button butSpecChar;
		private UI.Button butApptProcs;
		private UI.Button butOptimize;
		private UI.Button butInsPayFix;
		private Label label7;
		private UI.Button butTokens;
		private OpenDental.UI.Button butPrint;
		private Label label8;
		private UI.Button butRemoveNulls;
		private ODGrid gridMain;
		private UI.Button butNone;
		///<summary>Holds any text from the log that still needs to be printed when the log spans multiple pages.</summary>
		private string LogTextPrint;
		///<summary>An array of every single method in DatabaseMaintenance.cs.  It's an array because that's what reflection uses.</summary>
		private MethodInfo[] _arrayDbmMethodsAll;
		private TextBox textBox2;
		///<summary>This is a filtered list of methods from DatabaseMaintenance.cs that have the DbmMethod attribute.  This is used to populate gridMain.</summary>
		private List<MethodInfo> _listDbmMethodsGrid;
		private Label label1;
		private UI.Button butEtrans;
		///<summary>Holds the date and time of the last time a Check or Fix was run.  Only used for printing.</summary>
		private DateTime _dateTimeLastRun;
		private Label label4;
		private UI.Button butActiveTPs;
		private TabControl tabControlDBM;
		private TabPage tabChecks;
		private TabPage tabTools;
		private Label label9;
		private UI.Button butRawEmails;
		private TextBox textBox3;
		private Label labelSkipCheckTable;
		private Label label10;
		private UI.Button butRecalcEst;
		private GroupBox groupBoxUpdateInProg;
		private Label labelUpdateInProgress;
		private TextBox textBoxUpdateInProg;
		private UI.Button butClearUpdateInProgress;

		///<summary>This bool keeps track of whether we need to invalidate cache for all users.</summary>
		private bool _isCacheInvalid; 

		///<summary></summary>
		public FormDatabaseMaintenance() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this,new System.Windows.Forms.Control[]{
				this.textBox1,
				//this.textBox2
			});
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseMaintenance));
			this.butClose = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butCheck = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.checkShow = new System.Windows.Forms.CheckBox();
			this.butPrint = new OpenDental.UI.Button();
			this.butFix = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.butActiveTPs = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butEtrans = new OpenDental.UI.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.butRemoveNulls = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.butTokens = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.butInnoDB = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.labelApptProcs = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butSpecChar = new OpenDental.UI.Button();
			this.butApptProcs = new OpenDental.UI.Button();
			this.butOptimize = new OpenDental.UI.Button();
			this.butInsPayFix = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butNone = new OpenDental.UI.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.tabControlDBM = new System.Windows.Forms.TabControl();
			this.tabChecks = new System.Windows.Forms.TabPage();
			this.tabTools = new System.Windows.Forms.TabPage();
			this.label10 = new System.Windows.Forms.Label();
			this.butRecalcEst = new OpenDental.UI.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butRawEmails = new OpenDental.UI.Button();
			this.labelSkipCheckTable = new System.Windows.Forms.Label();
			this.groupBoxUpdateInProg = new System.Windows.Forms.GroupBox();
			this.labelUpdateInProgress = new System.Windows.Forms.Label();
			this.textBoxUpdateInProg = new System.Windows.Forms.TextBox();
			this.butClearUpdateInProgress = new OpenDental.UI.Button();
			this.tabControlDBM.SuspendLayout();
			this.tabChecks.SuspendLayout();
			this.tabTools.SuspendLayout();
			this.groupBoxUpdateInProg.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(737, 567);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(6, 6);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(779, 40);
			this.textBox1.TabIndex = 1;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "This tool will check the entire database for any improper settings, inconsistenci" +
    "es, or corruption.\r\nA log is automatically saved in RepairLog.txt if user has pe" +
    "rmission.";
			// 
			// butCheck
			// 
			this.butCheck.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.butCheck.Autosize = true;
			this.butCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheck.CornerRadius = 4F;
			this.butCheck.Location = new System.Drawing.Point(301, 482);
			this.butCheck.Name = "butCheck";
			this.butCheck.Size = new System.Drawing.Size(75, 26);
			this.butCheck.TabIndex = 4;
			this.butCheck.Text = "C&heck";
			this.butCheck.Click += new System.EventHandler(this.butCheck_Click);
			// 
			// checkShow
			// 
			this.checkShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShow.Location = new System.Drawing.Point(6, 427);
			this.checkShow.Name = "checkShow";
			this.checkShow.Size = new System.Drawing.Size(447, 20);
			this.checkShow.TabIndex = 1;
			this.checkShow.Text = "Show me everything in the log  (only for advanced users)";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(6, 482);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87, 26);
			this.butPrint.TabIndex = 3;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butFix
			// 
			this.butFix.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFix.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.butFix.Autosize = true;
			this.butFix.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFix.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFix.CornerRadius = 4F;
			this.butFix.Location = new System.Drawing.Point(426, 482);
			this.butFix.Name = "butFix";
			this.butFix.Size = new System.Drawing.Size(75, 26);
			this.butFix.TabIndex = 5;
			this.butFix.Text = "&Fix";
			this.butFix.Click += new System.EventHandler(this.butFix_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(150, 409);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(631, 20);
			this.label4.TabIndex = 48;
			this.label4.Text = "Creates an active treatment plan for all pats with treatment planned procs.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butActiveTPs
			// 
			this.butActiveTPs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butActiveTPs.Autosize = true;
			this.butActiveTPs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butActiveTPs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butActiveTPs.CornerRadius = 4F;
			this.butActiveTPs.Location = new System.Drawing.Point(30, 405);
			this.butActiveTPs.Name = "butActiveTPs";
			this.butActiveTPs.Size = new System.Drawing.Size(114, 26);
			this.butActiveTPs.TabIndex = 8;
			this.butActiveTPs.Text = "Active TPs";
			this.butActiveTPs.Click += new System.EventHandler(this.butActiveTPs_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(150, 377);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(631, 20);
			this.label1.TabIndex = 46;
			this.label1.Text = "Clear out etrans entries older than a year old.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butEtrans
			// 
			this.butEtrans.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEtrans.Autosize = true;
			this.butEtrans.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEtrans.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEtrans.CornerRadius = 4F;
			this.butEtrans.Enabled = false;
			this.butEtrans.Location = new System.Drawing.Point(30, 373);
			this.butEtrans.Name = "butEtrans";
			this.butEtrans.Size = new System.Drawing.Size(114, 26);
			this.butEtrans.TabIndex = 7;
			this.butEtrans.Text = "Etrans";
			this.butEtrans.Click += new System.EventHandler(this.butEtrans_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(150, 345);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(631, 20);
			this.label8.TabIndex = 44;
			this.label8.Text = "Replace all null strings with empty strings.";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butRemoveNulls
			// 
			this.butRemoveNulls.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRemoveNulls.Autosize = true;
			this.butRemoveNulls.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemoveNulls.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemoveNulls.CornerRadius = 4F;
			this.butRemoveNulls.Location = new System.Drawing.Point(30, 341);
			this.butRemoveNulls.Name = "butRemoveNulls";
			this.butRemoveNulls.Size = new System.Drawing.Size(114, 26);
			this.butRemoveNulls.TabIndex = 6;
			this.butRemoveNulls.Text = "Remove Nulls";
			this.butRemoveNulls.Click += new System.EventHandler(this.butRemoveNulls_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(150, 313);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(631, 20);
			this.label7.TabIndex = 42;
			this.label7.Text = "Validates tokens on file with the X-Charge server.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butTokens
			// 
			this.butTokens.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTokens.Autosize = true;
			this.butTokens.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTokens.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTokens.CornerRadius = 4F;
			this.butTokens.Location = new System.Drawing.Point(30, 309);
			this.butTokens.Name = "butTokens";
			this.butTokens.Size = new System.Drawing.Size(114, 26);
			this.butTokens.TabIndex = 5;
			this.butTokens.Text = "Tokens";
			this.butTokens.Click += new System.EventHandler(this.butTokens_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(150, 280);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(631, 20);
			this.label6.TabIndex = 40;
			this.label6.Text = "Converts database storage engine to/from InnoDb.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butInnoDB
			// 
			this.butInnoDB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInnoDB.Autosize = true;
			this.butInnoDB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInnoDB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInnoDB.CornerRadius = 4F;
			this.butInnoDB.Location = new System.Drawing.Point(30, 277);
			this.butInnoDB.Name = "butInnoDB";
			this.butInnoDB.Size = new System.Drawing.Size(114, 26);
			this.butInnoDB.TabIndex = 4;
			this.butInnoDB.Text = "InnoDb";
			this.butInnoDB.Click += new System.EventHandler(this.butInnoDB_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(150, 248);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(631, 20);
			this.label5.TabIndex = 38;
			this.label5.Text = "Removes special characters from appt notes and appt proc descriptions.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelApptProcs
			// 
			this.labelApptProcs.Location = new System.Drawing.Point(150, 216);
			this.labelApptProcs.Name = "labelApptProcs";
			this.labelApptProcs.Size = new System.Drawing.Size(631, 20);
			this.labelApptProcs.TabIndex = 37;
			this.labelApptProcs.Text = "Fixes procs in the Appt module that aren\'t correctly showing tooth nums.";
			this.labelApptProcs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(150, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(631, 20);
			this.label3.TabIndex = 36;
			this.label3.Text = "Back up, optimize, and repair tables.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(150, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(631, 20);
			this.label2.TabIndex = 35;
			this.label2.Text = "Creates checks for insurance payments that are not attached to a check.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butSpecChar
			// 
			this.butSpecChar.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSpecChar.Autosize = true;
			this.butSpecChar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSpecChar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSpecChar.CornerRadius = 4F;
			this.butSpecChar.Location = new System.Drawing.Point(30, 245);
			this.butSpecChar.Name = "butSpecChar";
			this.butSpecChar.Size = new System.Drawing.Size(114, 26);
			this.butSpecChar.TabIndex = 3;
			this.butSpecChar.Text = "Spec Char";
			this.butSpecChar.Click += new System.EventHandler(this.butSpecChar_Click);
			// 
			// butApptProcs
			// 
			this.butApptProcs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butApptProcs.Autosize = true;
			this.butApptProcs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butApptProcs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butApptProcs.CornerRadius = 4F;
			this.butApptProcs.Location = new System.Drawing.Point(30, 213);
			this.butApptProcs.Name = "butApptProcs";
			this.butApptProcs.Size = new System.Drawing.Size(114, 26);
			this.butApptProcs.TabIndex = 2;
			this.butApptProcs.Text = "Appt Procs";
			this.butApptProcs.Click += new System.EventHandler(this.butApptProcs_Click);
			// 
			// butOptimize
			// 
			this.butOptimize.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOptimize.Autosize = true;
			this.butOptimize.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOptimize.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOptimize.CornerRadius = 4F;
			this.butOptimize.Location = new System.Drawing.Point(30, 181);
			this.butOptimize.Name = "butOptimize";
			this.butOptimize.Size = new System.Drawing.Size(114, 26);
			this.butOptimize.TabIndex = 1;
			this.butOptimize.Text = "Optimize";
			this.butOptimize.Click += new System.EventHandler(this.butOptimize_Click);
			// 
			// butInsPayFix
			// 
			this.butInsPayFix.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInsPayFix.Autosize = true;
			this.butInsPayFix.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsPayFix.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsPayFix.CornerRadius = 4F;
			this.butInsPayFix.Location = new System.Drawing.Point(30, 149);
			this.butInsPayFix.Name = "butInsPayFix";
			this.butInsPayFix.Size = new System.Drawing.Size(114, 26);
			this.butInsPayFix.TabIndex = 0;
			this.butInsPayFix.Text = "Ins Pay Fix";
			this.butInsPayFix.Click += new System.EventHandler(this.butInsPayFix_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridMain.HasAddButton = false;
			this.gridMain.HasDropDowns = false;
			this.gridMain.HasMultilineHeaders = true;
			this.gridMain.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridMain.HeaderHeight = 15;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(6, 52);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(790, 369);
			this.gridMain.TabIndex = 0;
			this.gridMain.Title = "Database Checks";
			this.gridMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridMain.TitleHeight = 18;
			this.gridMain.TranslationName = "TableClaimPaySplits";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(721, 427);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75, 26);
			this.butNone.TabIndex = 2;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(350, 427);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(365, 26);
			this.textBox2.TabIndex = 99;
			this.textBox2.TabStop = false;
			this.textBox2.Text = "No selections will cause all database checks to run.\r\nOtherwise only selected che" +
    "cks will run.\r\n";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tabControlDBM
			// 
			this.tabControlDBM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlDBM.Controls.Add(this.tabChecks);
			this.tabControlDBM.Controls.Add(this.tabTools);
			this.tabControlDBM.Location = new System.Drawing.Point(12, 12);
			this.tabControlDBM.Name = "tabControlDBM";
			this.tabControlDBM.SelectedIndex = 0;
			this.tabControlDBM.Size = new System.Drawing.Size(810, 540);
			this.tabControlDBM.TabIndex = 0;
			// 
			// tabChecks
			// 
			this.tabChecks.BackColor = System.Drawing.SystemColors.Control;
			this.tabChecks.Controls.Add(this.textBox1);
			this.tabChecks.Controls.Add(this.butFix);
			this.tabChecks.Controls.Add(this.butPrint);
			this.tabChecks.Controls.Add(this.textBox2);
			this.tabChecks.Controls.Add(this.butCheck);
			this.tabChecks.Controls.Add(this.checkShow);
			this.tabChecks.Controls.Add(this.butNone);
			this.tabChecks.Controls.Add(this.gridMain);
			this.tabChecks.Location = new System.Drawing.Point(4, 22);
			this.tabChecks.Name = "tabChecks";
			this.tabChecks.Padding = new System.Windows.Forms.Padding(3);
			this.tabChecks.Size = new System.Drawing.Size(802, 514);
			this.tabChecks.TabIndex = 0;
			this.tabChecks.Text = "Checks";
			// 
			// tabTools
			// 
			this.tabTools.BackColor = System.Drawing.SystemColors.Control;
			this.tabTools.Controls.Add(this.groupBoxUpdateInProg);
			this.tabTools.Controls.Add(this.label10);
			this.tabTools.Controls.Add(this.butRecalcEst);
			this.tabTools.Controls.Add(this.textBox3);
			this.tabTools.Controls.Add(this.label9);
			this.tabTools.Controls.Add(this.butRawEmails);
			this.tabTools.Controls.Add(this.label4);
			this.tabTools.Controls.Add(this.butActiveTPs);
			this.tabTools.Controls.Add(this.butInsPayFix);
			this.tabTools.Controls.Add(this.label1);
			this.tabTools.Controls.Add(this.butOptimize);
			this.tabTools.Controls.Add(this.butEtrans);
			this.tabTools.Controls.Add(this.butApptProcs);
			this.tabTools.Controls.Add(this.label8);
			this.tabTools.Controls.Add(this.butSpecChar);
			this.tabTools.Controls.Add(this.butRemoveNulls);
			this.tabTools.Controls.Add(this.label2);
			this.tabTools.Controls.Add(this.label7);
			this.tabTools.Controls.Add(this.label3);
			this.tabTools.Controls.Add(this.butTokens);
			this.tabTools.Controls.Add(this.labelApptProcs);
			this.tabTools.Controls.Add(this.label6);
			this.tabTools.Controls.Add(this.label5);
			this.tabTools.Controls.Add(this.butInnoDB);
			this.tabTools.Location = new System.Drawing.Point(4, 22);
			this.tabTools.Name = "tabTools";
			this.tabTools.Padding = new System.Windows.Forms.Padding(3);
			this.tabTools.Size = new System.Drawing.Size(802, 514);
			this.tabTools.TabIndex = 1;
			this.tabTools.Text = "Tools";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(150, 472);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(631, 20);
			this.label10.TabIndex = 53;
			this.label10.Text = "Recalc estimates that are associated to non active coverage for the patient.";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butRecalcEst
			// 
			this.butRecalcEst.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRecalcEst.Autosize = true;
			this.butRecalcEst.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecalcEst.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecalcEst.CornerRadius = 4F;
			this.butRecalcEst.Location = new System.Drawing.Point(30, 469);
			this.butRecalcEst.Name = "butRecalcEst";
			this.butRecalcEst.Size = new System.Drawing.Size(114, 26);
			this.butRecalcEst.TabIndex = 52;
			this.butRecalcEst.Text = "Recalc Estimates";
			this.butRecalcEst.Click += new System.EventHandler(this.butRecalcEst_Click);
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(6, 92);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(790, 54);
			this.textBox3.TabIndex = 51;
			this.textBox3.TabStop = false;
			this.textBox3.Text = resources.GetString("textBox3.Text");
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(150, 441);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(631, 20);
			this.label9.TabIndex = 50;
			this.label9.Text = "Fixes emails which are encoded in the Chart progress notes.  Also clears unused a" +
    "ttachments.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butRawEmails
			// 
			this.butRawEmails.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRawEmails.Autosize = true;
			this.butRawEmails.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRawEmails.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRawEmails.CornerRadius = 4F;
			this.butRawEmails.Location = new System.Drawing.Point(30, 437);
			this.butRawEmails.Name = "butRawEmails";
			this.butRawEmails.Size = new System.Drawing.Size(114, 26);
			this.butRawEmails.TabIndex = 9;
			this.butRawEmails.Text = "Raw Emails";
			this.butRawEmails.Click += new System.EventHandler(this.butRawEmails_Click);
			// 
			// labelSkipCheckTable
			// 
			this.labelSkipCheckTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSkipCheckTable.ForeColor = System.Drawing.Color.Red;
			this.labelSkipCheckTable.Location = new System.Drawing.Point(587, 574);
			this.labelSkipCheckTable.Name = "labelSkipCheckTable";
			this.labelSkipCheckTable.Size = new System.Drawing.Size(144, 16);
			this.labelSkipCheckTable.TabIndex = 2;
			this.labelSkipCheckTable.Text = "Table check is disabled";
			this.labelSkipCheckTable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelSkipCheckTable.Visible = false;
			// 
			// groupBoxUpdateInProg
			// 
			this.groupBoxUpdateInProg.Controls.Add(this.labelUpdateInProgress);
			this.groupBoxUpdateInProg.Controls.Add(this.textBoxUpdateInProg);
			this.groupBoxUpdateInProg.Controls.Add(this.butClearUpdateInProgress);
			this.groupBoxUpdateInProg.Location = new System.Drawing.Point(6, 8);
			this.groupBoxUpdateInProg.Name = "groupBoxUpdateInProg";
			this.groupBoxUpdateInProg.Size = new System.Drawing.Size(605, 78);
			this.groupBoxUpdateInProg.TabIndex = 59;
			this.groupBoxUpdateInProg.TabStop = false;
			this.groupBoxUpdateInProg.Text = "Update in progress on computer: ";
			// 
			// labelUpdateInProgress
			// 
			this.labelUpdateInProgress.Location = new System.Drawing.Point(21, 17);
			this.labelUpdateInProgress.Name = "labelUpdateInProgress";
			this.labelUpdateInProgress.Size = new System.Drawing.Size(578, 26);
			this.labelUpdateInProgress.TabIndex = 58;
			this.labelUpdateInProgress.Text = "Clear this value only if you are unable to start the program on other workstation" +
    "s and you are sure an update is not currently in progress.";
			// 
			// textBoxUpdateInProg
			// 
			this.textBoxUpdateInProg.Location = new System.Drawing.Point(24, 47);
			this.textBoxUpdateInProg.Name = "textBoxUpdateInProg";
			this.textBoxUpdateInProg.ReadOnly = true;
			this.textBoxUpdateInProg.Size = new System.Drawing.Size(149, 20);
			this.textBoxUpdateInProg.TabIndex = 55;
			// 
			// butClearUpdateInProgress
			// 
			this.butClearUpdateInProgress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearUpdateInProgress.Autosize = true;
			this.butClearUpdateInProgress.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearUpdateInProgress.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearUpdateInProgress.CornerRadius = 4F;
			this.butClearUpdateInProgress.Location = new System.Drawing.Point(179, 45);
			this.butClearUpdateInProgress.Name = "butClearUpdateInProgress";
			this.butClearUpdateInProgress.Size = new System.Drawing.Size(78, 23);
			this.butClearUpdateInProgress.TabIndex = 54;
			this.butClearUpdateInProgress.Text = "Clear";
			this.butClearUpdateInProgress.UseVisualStyleBackColor = true;
			this.butClearUpdateInProgress.Click += new System.EventHandler(this.butClearUpdateInProgress_Click);
			// 
			// FormDatabaseMaintenance
			// 
			this.AcceptButton = this.butCheck;
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(834, 605);
			this.Controls.Add(this.labelSkipCheckTable);
			this.Controls.Add(this.tabControlDBM);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(850, 458);
			this.Name = "FormDatabaseMaintenance";
			this.ShowInTaskbar = false;
			this.Text = "Database Maintenance";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDatabaseMaintenance_FormClosing);
			this.Load += new System.EventHandler(this.FormDatabaseMaintenance_Load);
			this.tabControlDBM.ResumeLayout(false);
			this.tabChecks.ResumeLayout(false);
			this.tabChecks.PerformLayout();
			this.tabTools.ResumeLayout(false);
			this.tabTools.PerformLayout();
			this.groupBoxUpdateInProg.ResumeLayout(false);
			this.groupBoxUpdateInProg.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDatabaseMaintenance_Load(object sender,System.EventArgs e) {
			_listDbmMethodsGrid=DatabaseMaintenance.GetMethodsForDisplay(Clinics.ClinicNum);
			//Users get stopped from launching FormDatabaseMaintenance when they do not have the Setup permission.
			//Jordan wants some tools to only be accessible to users with the SecurityAdmin permission.
			if(Security.IsAuthorized(Permissions.SecurityAdmin,true)){
				butEtrans.Enabled=true;
			}
			if(Clinics.IsMedicalPracticeOrClinic(Clinics.ClinicNum)) {
				butApptProcs.Visible=false;
				labelApptProcs.Visible=false;
			}
			if(PrefC.GetBool(PrefName.DatabaseMaintenanceDisableOptimize)) {
				butOptimize.Enabled=false;
			}
			if(PrefC.GetBool(PrefName.DatabaseMaintenanceSkipCheckTable)) {
				labelSkipCheckTable.Visible=true;
			}
			FillGrid();
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				butRemoveNulls.Visible=false;
			}
			textBoxUpdateInProg.Text=PrefC.GetString(PrefName.UpdateInProgressOnComputerName);
			if(string.IsNullOrWhiteSpace(textBoxUpdateInProg.Text)) {
				butClearUpdateInProgress.Enabled=false;
			}
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			gridMain.Columns.Add(new ODGridColumn(Lan.g(this,"Name"),300));
			gridMain.Columns.Add(new ODGridColumn(Lan.g(this,"Break\r\nDown"),40,HorizontalAlignment.Center));
			gridMain.Columns.Add(new ODGridColumn(Lan.g(this,"Results"),0));
			gridMain.Rows.Clear();
			ODGridRow row;
			//_listDbmMethodsGrid has already been filled on load with the correct methods to display in the grid.
			for(int i=0;i<_listDbmMethodsGrid.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_listDbmMethodsGrid[i].Name);
				row.Cells.Add(DatabaseMaintenance.MethodHasBreakDown(_listDbmMethodsGrid[i]) ? "X" : "");
				row.Cells.Add("");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(!DatabaseMaintenance.MethodHasBreakDown(_listDbmMethodsGrid[e.Row])) {
				return;
			}
			//We know that this method supports giving the user a break down and shall call the method's fix section where the break down results should be.
			//TODO: Make sure that DBM methods with break downs ALWAYS have the break down in the fix section.
			long userNum=0;
			long patNum=0;
			DbmMethodAttr methodAttributes=(DbmMethodAttr)Attribute.GetCustomAttribute(_listDbmMethodsGrid[e.Row],typeof(DbmMethodAttr));
			//We always send verbose and modeCur into all DBM methods.
			List<object> parameters=new List<object>() { checkShow.Checked,DbmMode.Breakdown };
			//There are optional paramaters available to some methods and adding them in the following order is very important.
			if(methodAttributes.HasUserNum) {
				parameters.Add(userNum);
			}
			if(methodAttributes.HasPatNum) {
				parameters.Add(patNum);
			}
			Cursor=Cursors.WaitCursor;
			string result=(string)_listDbmMethodsGrid[e.Row].Invoke(null,parameters.ToArray());
			if(result=="") {//Only possible if running a check / fix in non-verbose mode and nothing happened or needs to happen.
				result=Lan.g("FormDatabaseMaintenance","Done.  No maintenance needed.");
			}
			//Show the result of the dbm method in a simple copy paste msg box.
			MsgBoxCopyPaste msgBoxCP=new MsgBoxCopyPaste(result);
			Cursor=Cursors.Default;
			msgBoxCP.Show();//Let this window be non-modal so that they can keep it open while they fix their problems.
		}

		private void butNone_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
		}

		#region Database Tools
		
		private void butClearUpdateInProgress_Click(object sender,EventArgs e) {
			Prefs.UpdateString(PrefName.UpdateInProgressOnComputerName,"");
			DataValid.SetInvalid(InvalidType.Prefs);
			textBoxUpdateInProg.Text="";
		}

		private void butInsPayFix_Click(object sender,EventArgs e) {
			FormInsPayFix formIns=new FormInsPayFix();
			formIns.ShowDialog();
		}

		private void butOptimize_Click(object sender,EventArgs e) {
			if(MessageBox.Show(Lan.g("FormDatabaseMaintenance","This tool will backup, optimize, and repair all tables.")+"\r\n"+Lan.g("FormDatabaseMaintenance","Continue?")
				,Lan.g("FormDatabaseMaintenance","Backup Optimize Repair")
				,MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			string result="";
			if(Shared.BackupRepairAndOptimize(true,BackupLocation.OptimizeTool)) {
				result=DateTime.Now.ToString()+"\r\n"+Lan.g("FormDatabaseMaintenance","Repair and Optimization Complete");
			}
			else {
				result=DateTime.Now.ToString()+"\r\n";
				result+=Lan.g("FormDatabaseMaintenance","Backup, repair, or optimize has failed.  Your database has not been altered.")+"\r\n";
				result+=Lan.g("FormDatabaseMaintenance","Please call support for help, a manual backup of your data must be made before trying to fix your database.")+"\r\n";
			}
			Cursor=Cursors.Default;
			MsgBoxCopyPaste msgBoxCP=new MsgBoxCopyPaste(result);
			msgBoxCP.Show();//Let this window be non-modal so that they can keep it open while they fix their problems.
			try {
				DatabaseMaintenance.SaveLogToFile(result);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butApptProcs_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will fix procedure descriptions in the Appt module that aren't correctly showing tooth numbers.\r\nContinue?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			Appointments.UpdateProcDescriptForAppts(Appointments.GetForPeriod(DateTime.Now.Date.AddMonths(-6),DateTime.MaxValue.AddDays(-10)).ToList());
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done. Please refresh Appt module to see the changes.");
		}

		private void butSpecChar_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This is only used if your mobile synch or middle tier is failing.  This cannot be undone.  Do you wish to continue?")) {
				return;
			}
			InputBox box=new InputBox("In our online manual, on the database maintenance page, look for the password and enter it below.");
			if(box.ShowDialog()!=DialogResult.OK) {
				return;
			}
			if(box.textResult.Text!="fix") {
				MessageBox.Show("Wrong password.");
				return;
			}
			DatabaseMaintenance.FixSpecialCharacters();
			MsgBox.Show(this,"Done.");
			_isCacheInvalid=true;//Definitions are cached and could have been changed from above DBM.
		}

		private void butInnoDB_Click(object sender,EventArgs e) {
			FormInnoDb form=new FormInnoDb();
			form.ShowDialog();
		}

		private void butTokens_Click(object sender,EventArgs e) {
			FormXchargeTokenTool FormXCT=new FormXchargeTokenTool();
			FormXCT.ShowDialog();
		}

		private void butRemoveNulls_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will replace ALL null strings in your database with empty strings.  This cannot be undone.  Do you wish to continue?")) {
				return;
			}
			MessageBox.Show(Lan.g(this,"Number of null strings replaced with empty strings")+": "+DatabaseMaintenance.MySqlRemoveNullStrings());
			_isCacheInvalid=true;//The above DBM could have potentially changed cached tables. 
		}

		private void butEtrans_Click(object sender,EventArgs e) {
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				MsgBox.Show(this,"Tool does not currently support Oracle.  Please call support to see if you need this fix.");
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will clear out etrans message text entries over a year old.  An automatic backup of the database will be created before deleting any entries.  This process may take a while to run depending on the size of your database.  Continue?")) {
				return;
			}
#if !DEBUG
			if(!Shared.MakeABackup(BackupLocation.DatabaseMaintenanceTool)) {
				MsgBox.Show(this,"Etrans message text entries were not altered.  Please try again.");
				return;
			}
#endif
			DatabaseMaintenance.ClearOldEtransMessageText();
			MsgBox.Show(this,"Etrans message text entries over a year old removed");
		}

		private void butActiveTPs_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			List<Procedure> listTpTpiProcs=DatabaseMaintenance.GetProcsNoActiveTp();
			Cursor=Cursors.Default;
			if(listTpTpiProcs.Count==0) {
				MsgBox.Show(this,"Done");
				return;
			}
			int numTPs=listTpTpiProcs.Select(x => x.PatNum).Distinct().ToList().Count;
			int numTPAs=listTpTpiProcs.Count;
			TimeSpan estRuntime=TimeSpan.FromSeconds((numTPs+numTPAs)*0.001d);
			//the factor 0.001 was determined by running tests on a large db
			//212631 TPAs and 30000 TPs were inserted in 225 seconds
			//225/(212631+30000)=0.0009273341 seconds per inserted row (rounded up to 0.001 seconds)
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"From your database size we estimate that this could take "+(estRuntime.Minutes+1)+" minutes to create "
				+numTPs+" treatment plans for "+numTPAs+" procedures if running form the server.\r\nDo you wish to continue?"))
			{
				return;
			}
			Cursor=Cursors.WaitCursor;
			string msg=DatabaseMaintenance.CreateMissingActiveTPs(listTpTpiProcs);
			Cursor=Cursors.Default;
			if(string.IsNullOrEmpty(msg)) {
				msg="Done";
			}
			MsgBox.Show(this,msg);
		}

		private void butRawEmails_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo
				,"This tool is only necessary to run if utilizing the email inbox feature.\r\n"
				+"Run this tool if email messages are encoded in the Chart progress notes, \r\n"
				+"or if the emailmessage table has grown to a large size.\r\n"
				+"This will decode any encoded clear text emails and will remove unused attachment content.\r\n\r\n"
				+"This tool could take a long time to finish, do you wish to continue?"))
			{
				return;
			}
			//Create a new thread that will show a progress window (takes a while even if no clean up needed) so the user know work is being done.
			Action actionCloseEmailProgress=ODProgress.ShowProgressStatus("RawEmailCleanUp");
			string results="";
			try {
				results=DatabaseMaintenance.CleanUpRawEmails();
			}
			catch(Exception ex) {
				results=Lan.g(this,"There was an error cleaning up email bloat:")+"\r\n"+ex.Message;
			}
			finally {
				actionCloseEmailProgress();
			}
			MessageBox.Show(results);
		}

		private void butRecalcEst_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo
				,"This tool will mimic what happens when you click OK in the procedure edit window.  "
				+"The tool will identify patients with at least one estimate which belongs to a dropped insurance plan.  "
				+"For each such patient, estimates will be recalculated for current plans, and  "
				+"for plans which have been dropped, estimates associated to the dropped plans will be deleted.\r\n"
				+"This tool could take a long time to finish, do you wish to continue?"))
			{
				return;
			}
			//Create a new thread that will show a progress window (takes a while even if no clean up needed) so the user know work is being done.
			Action actionCloseInvalidEstProgress=ODProgress.ShowProgressStatus("Recalc Estiamtes");
			DatabaseMaintenance.RecalcEstimates(Procedures.GetProcsWithOldEstimates());
			actionCloseInvalidEstProgress();
		}

		#endregion

		private void butTemp_Click(object sender,EventArgs e) {
			FormDatabaseMaintTemp form=new FormDatabaseMaintTemp();
			form.ShowDialog();
		}

		private void Run(DbmMode modeCur) {
			Cursor=Cursors.WaitCursor;
			//Clear out the result column for all rows before every "run"
			for(int i=0;i<gridMain.Rows.Count;i++) {
				gridMain.Rows[i].Cells[2].Text="";//Don't use UpdateResultTextForRow here because users will see the rows clearing out one by one.
			}
			bool verbose=checkShow.Checked;
			StringBuilder logText=new StringBuilder();
			//Create a thread that will show a window and then stay open until the closing phrase is thrown from this form.
			Action actionCloseCheckTableProgress=ODProgress.ShowProgressStatus("CheckTableProgress");
			string result=DatabaseMaintenance.MySQLTables(verbose,modeCur);
			actionCloseCheckTableProgress();
			logText.Append(result);//No database maintenance checks should be run unless this passes.
			if(!DatabaseMaintenance.GetSuccess()) {
				Cursor=Cursors.Default;
				MsgBoxCopyPaste msgBoxCP=new MsgBoxCopyPaste(result);//Result is already translated.
				msgBoxCP.Show();//Let this window be non-modal so that they can keep it open while they fix their problems.
				return;
			}
			if(gridMain.SelectedIndices.Length < 1) {
				//No rows are selected so the user wants to run all checks.
				gridMain.SetSelected(true);
			}
			int[] selectedIndices=gridMain.SelectedIndices;
			for(int i=0;i<selectedIndices.Length;i++) {
				long userNum=0;
				long patNum=0;
				DbmMethodAttr methodAttributes=(DbmMethodAttr)Attribute.GetCustomAttribute(_listDbmMethodsGrid[selectedIndices[i]],typeof(DbmMethodAttr));
				//We always send verbose and modeCur into all DBM methods.
				List<object> parameters=new List<object>() { verbose,modeCur };
				//There are optional paramaters available to some methods and adding them in the following order is very important.
				if(methodAttributes.HasUserNum) {
					parameters.Add(userNum);
				}
				if(methodAttributes.HasPatNum) {
					parameters.Add(patNum);
				}
				gridMain.ScrollToIndexBottom(selectedIndices[i]);
				UpdateResultTextForRow(selectedIndices[i],Lan.g("FormDatabaseMaintenance","Running")+"...");
				try {
					result=(string)_listDbmMethodsGrid[selectedIndices[i]].Invoke(null,parameters.ToArray());
				}
				catch(Exception ex) {
					if(ex.InnerException!=null) {
						ExceptionDispatchInfo.Capture(ex.InnerException).Throw();//This preserves the stack trace of the InnerException.
					}
					throw;
				}
				string status="";
				if(result=="") {//Only possible if running a check / fix in non-verbose mode and nothing happened or needs to happen.
					status=Lan.g("FormDatabaseMaintenance","Done.  No maintenance needed.");
				}
				UpdateResultTextForRow(selectedIndices[i],result+status);
				logText.Append(result);
			}
			gridMain.SetSelected(selectedIndices,true);//Reselect all rows that were originally selected.
			try {
				DatabaseMaintenance.SaveLogToFile(logText.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
		}

		/// <summary>Updates the result column for the specified row in gridMain with the text passed in.</summary>
		private void UpdateResultTextForRow(int index,string text) {
			gridMain.BeginUpdate();
			//Checks to see if it has a breakdown, and if it needs any maintenenece to decide whether or not to apply the "X"
			if(!DatabaseMaintenance.MethodHasBreakDown(_listDbmMethodsGrid[index]) || text == "Done.  No maintenance needed.") {
				gridMain.Rows[index].Cells[1].Text="";
			}
			else {
				gridMain.Rows[index].Cells[1].Text="X";
			}
			gridMain.Rows[index].Cells[2].Text=text;
			gridMain.EndUpdate();
			Application.DoEvents();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(_dateTimeLastRun==DateTime.MinValue) {
				_dateTimeLastRun=DateTime.Now;
			}
			StringBuilder strB=new StringBuilder();
			strB.Append(_dateTimeLastRun.ToString());
			strB.Append('-',65);
			strB.AppendLine();//New line.
			if(gridMain.SelectedIndices.Length < 1) {
				//No rows are selected so the user wants to run all checks.
				gridMain.SetSelected(true);
			}
			int[] selectedIndices=gridMain.SelectedIndices;
			for(int i=0;i<selectedIndices.Length;i++) {
				string resultText=gridMain.Rows[selectedIndices[i]].Cells[2].Text;
				if(!String.IsNullOrEmpty(resultText) && resultText!="Done.  No maintenance needed.") {
					strB.Append(gridMain.Rows[selectedIndices[i]].Cells[0].Text+"\r\n");
					strB.Append("---"+gridMain.Rows[selectedIndices[i]].Cells[2].Text+"\r\n");
					strB.AppendLine();
				}
			}
			strB.AppendLine(Lan.g("FormDatabaseMaintenance","Done"));
			LogTextPrint=strB.ToString();
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(40,50,50,60);
			try {
#if DEBUG
				FormPrintPreview printPreview=new FormPrintPreview(PrintSituation.Default,pd2,0,0,"Database Maintenance log printed");
				printPreview.ShowDialog();
#else
				pd2.Print();
#endif
			}
			catch {
				MessageBox.Show("Printer not available");
			}
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			int charsOnPage=0;
			int linesPerPage=0;
			Font font=new Font("Courier New",10);
			ev.Graphics.MeasureString(LogTextPrint,font,ev.MarginBounds.Size,StringFormat.GenericTypographic,out charsOnPage,out linesPerPage);
			ev.Graphics.DrawString(LogTextPrint,font,Brushes.Black,ev.MarginBounds,StringFormat.GenericTypographic);
			LogTextPrint=LogTextPrint.Substring(charsOnPage);
			ev.HasMorePages=(LogTextPrint.Length > 0);
		}

		private void butCheck_Click(object sender,System.EventArgs e) {
			Run(DbmMode.Check);
		}

		private void butFix_Click(object sender,EventArgs e) {
			List<string> runningComps=Computers.GetRunningComputers(); 
			if(runningComps.Count>50) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"WARNING!\r\nMore than 50 workstations are connected to this database. "
					+"Running DBM may cause severe network slowness. "
					+"We recommend running this tool when fewer users are connected (possibly after working hours). \r\n\r\n"
					+"Continue?")) 
				{
					return;
				}
			}
			Run(DbmMode.Fix);
			_isCacheInvalid=true;//Flag cache to be invalidated on closing.  Some DBM fixes alter cached tables.
		}

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

		private void FormDatabaseMaintenance_FormClosing(object sender,FormClosingEventArgs e) {
			if(_isCacheInvalid) {
				Action actionCloseDBM=ODProgress.ShowProgressStatus("DatabaseMaintEvent",Lan.g(this,"Refreshing all caches, this can take a while..."));
				//Invalidate all cached tables.  DBM could have touched anything so blast them all.  
				//Failure to invalidate cache can cause UEs in the main program.
				DataValid.SetInvalid(Cache.GetAllCachedInvalidTypes().ToArray());
				actionCloseDBM?.Invoke();
			}
		}
	}


}
