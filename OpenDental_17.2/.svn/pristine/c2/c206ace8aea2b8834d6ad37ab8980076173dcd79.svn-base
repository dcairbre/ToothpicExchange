/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	///<summary></summary>
	public class FormCommItem : ODForm{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private IContainer components;

		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDateTime;
		private System.Windows.Forms.ListBox listMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listSentOrReceived;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.ListBox listType;
		private TextBox textPatientName;
		private Label label5;
		private TextBox textUser;
		private Label label16;
		private Label labelCommlogNum;
		private TextBox textCommlogNum;
		private OpenDental.UI.SignatureBoxWrapper signatureBoxWrapper;
		private Commlog CommlogCur;
		private Commlog _commLogOld;
		private bool IsStartingUp;
		private UI.Button butNow;
		private UI.Button butNowEnd;
		private TextBox textDateTimeEnd;
		private Label labelDateTimeEnd;
		private Timer timerAutoSave;
		private bool SigChanged;
		private Label labelSavedManually;
		private Timer timerManualSave;
		private UI.Button butAutoNote;
		private UI.Button butUserPrefs;
		///<summary>Set to true if this commlog window should always stay open.  Changes lots of functionality throughout the entire window.</summary>
		public bool IsPersistent;
		///<summary>The user pref that indicates if this user wants the Note text box to clear after a commlog is saved in persistent mode.
		///Can be null and will be treated as turned on (true) if null.</summary>
		private UserOdPref _userOdPrefClearNote;
		///<summary>The user pref that indicates if this user wants the End text box to clear after a commlog is saved in persistent mode.
		///Can be null and will be treated as turned on (true) if null</summary>
		private UserOdPref _userOdPrefEndDate;
		///<summary>The user pref that indicates if this user wants the Date / Time text box to clear after a commlog is saved in persistent mode.
		///Can be null and will be treated as turned on (true) if null</summary>
		private UserOdPref _userOdPrefUpdateDateTimeNewPat;

		///<summary></summary>
		public FormCommItem(Commlog commlogCur){
			InitializeComponent();
			Lan.F(this);
			CommlogCur=commlogCur;
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommItem));
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.textDateTime = new System.Windows.Forms.TextBox();
			this.listMode = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listSentOrReceived = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.textPatientName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.labelCommlogNum = new System.Windows.Forms.Label();
			this.textCommlogNum = new System.Windows.Forms.TextBox();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.butNow = new OpenDental.UI.Button();
			this.butNowEnd = new OpenDental.UI.Button();
			this.textDateTimeEnd = new System.Windows.Forms.TextBox();
			this.labelDateTimeEnd = new System.Windows.Forms.Label();
			this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
			this.labelSavedManually = new System.Windows.Forms.Label();
			this.timerManualSave = new System.Windows.Forms.Timer(this.components);
			this.butAutoNote = new OpenDental.UI.Button();
			this.butUserPrefs = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date / Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(80, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Type";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(494, 557);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butCancel.Location = new System.Drawing.Point(575, 557);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 557);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 25);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(81, 199);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(82, 98);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120, 95);
			this.listType.TabIndex = 20;
			this.listType.SelectedIndexChanged += new System.EventHandler(this.listType_SelectedIndexChanged);
			// 
			// textDateTime
			// 
			this.textDateTime.Location = new System.Drawing.Point(82, 31);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.Size = new System.Drawing.Size(205, 20);
			this.textDateTime.TabIndex = 21;
			this.textDateTime.TextChanged += new System.EventHandler(this.textDateTime_TextChanged);
			// 
			// listMode
			// 
			this.listMode.Location = new System.Drawing.Point(215, 98);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(73, 95);
			this.listMode.TabIndex = 23;
			this.listMode.SelectedIndexChanged += new System.EventHandler(this.listMode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(214, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 16);
			this.label3.TabIndex = 22;
			this.label3.Text = "Mode";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSentOrReceived
			// 
			this.listSentOrReceived.Location = new System.Drawing.Point(303, 98);
			this.listSentOrReceived.Name = "listSentOrReceived";
			this.listSentOrReceived.Size = new System.Drawing.Size(87, 43);
			this.listSentOrReceived.TabIndex = 25;
			this.listSentOrReceived.SelectedValueChanged += new System.EventHandler(this.listSentOrReceived_SelectedValueChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(302, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142, 16);
			this.label4.TabIndex = 24;
			this.label4.Text = "Sent or Received";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsTab = true;
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.DetectLinksEnabled = false;
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(82, 217);
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.CommLog;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(568, 209);
			this.textNote.TabIndex = 27;
			this.textNote.Text = "";
			this.textNote.TextChanged += new System.EventHandler(this.textNote_TextChanged);
			// 
			// textPatientName
			// 
			this.textPatientName.Location = new System.Drawing.Point(82, 7);
			this.textPatientName.Name = "textPatientName";
			this.textPatientName.ReadOnly = true;
			this.textPatientName.Size = new System.Drawing.Size(205, 20);
			this.textPatientName.TabIndex = 30;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(78, 18);
			this.label5.TabIndex = 29;
			this.label5.Text = "Patient";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUser
			// 
			this.textUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textUser.Location = new System.Drawing.Point(450, 7);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119, 20);
			this.textUser.TabIndex = 103;
			// 
			// label16
			// 
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label16.Location = new System.Drawing.Point(388, 9);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60, 16);
			this.label16.TabIndex = 102;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCommlogNum
			// 
			this.labelCommlogNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelCommlogNum.Location = new System.Drawing.Point(365, 533);
			this.labelCommlogNum.Name = "labelCommlogNum";
			this.labelCommlogNum.Size = new System.Drawing.Size(96, 16);
			this.labelCommlogNum.TabIndex = 104;
			this.labelCommlogNum.Text = "CommlogNum";
			this.labelCommlogNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCommlogNum
			// 
			this.textCommlogNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textCommlogNum.Location = new System.Drawing.Point(462, 531);
			this.textCommlogNum.Name = "textCommlogNum";
			this.textCommlogNum.ReadOnly = true;
			this.textCommlogNum.Size = new System.Drawing.Size(188, 20);
			this.textCommlogNum.TabIndex = 105;
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.Location = new System.Drawing.Point(82, 432);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.SignatureMode = OpenDental.UI.SignatureBoxWrapper.SigMode.Default;
			this.signatureBoxWrapper.Size = new System.Drawing.Size(364, 81);
			this.signatureBoxWrapper.TabIndex = 106;
			this.signatureBoxWrapper.UserSig = null;
			this.signatureBoxWrapper.SignatureChanged += new System.EventHandler(this.signatureBoxWrapper_SignatureChanged);
			// 
			// butNow
			// 
			this.butNow.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNow.Autosize = true;
			this.butNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow.CornerRadius = 4F;
			this.butNow.Location = new System.Drawing.Point(293, 31);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(48, 21);
			this.butNow.TabIndex = 107;
			this.butNow.Text = "Now";
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
			// 
			// butNowEnd
			// 
			this.butNowEnd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNowEnd.Autosize = true;
			this.butNowEnd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNowEnd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNowEnd.CornerRadius = 4F;
			this.butNowEnd.Location = new System.Drawing.Point(293, 55);
			this.butNowEnd.Name = "butNowEnd";
			this.butNowEnd.Size = new System.Drawing.Size(48, 21);
			this.butNowEnd.TabIndex = 110;
			this.butNowEnd.Text = "Now";
			this.butNowEnd.Click += new System.EventHandler(this.butNowEnd_Click);
			// 
			// textDateTimeEnd
			// 
			this.textDateTimeEnd.Location = new System.Drawing.Point(82, 55);
			this.textDateTimeEnd.Name = "textDateTimeEnd";
			this.textDateTimeEnd.Size = new System.Drawing.Size(205, 20);
			this.textDateTimeEnd.TabIndex = 109;
			// 
			// labelDateTimeEnd
			// 
			this.labelDateTimeEnd.Location = new System.Drawing.Point(1, 56);
			this.labelDateTimeEnd.Name = "labelDateTimeEnd";
			this.labelDateTimeEnd.Size = new System.Drawing.Size(81, 18);
			this.labelDateTimeEnd.TabIndex = 108;
			this.labelDateTimeEnd.Text = "End";
			this.labelDateTimeEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timerAutoSave
			// 
			this.timerAutoSave.Interval = 10000;
			this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
			// 
			// labelSavedManually
			// 
			this.labelSavedManually.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSavedManually.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.labelSavedManually.Location = new System.Drawing.Point(382, 561);
			this.labelSavedManually.Name = "labelSavedManually";
			this.labelSavedManually.Size = new System.Drawing.Size(106, 16);
			this.labelSavedManually.TabIndex = 111;
			this.labelSavedManually.Text = "Saved";
			this.labelSavedManually.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelSavedManually.Visible = false;
			// 
			// timerManualSave
			// 
			this.timerManualSave.Interval = 1500;
			this.timerManualSave.Tick += new System.EventHandler(this.timerManualSave_Tick);
			// 
			// butAutoNote
			// 
			this.butAutoNote.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAutoNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAutoNote.Autosize = true;
			this.butAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAutoNote.CornerRadius = 4F;
			this.butAutoNote.Location = new System.Drawing.Point(575, 192);
			this.butAutoNote.Name = "butAutoNote";
			this.butAutoNote.Size = new System.Drawing.Size(75, 21);
			this.butAutoNote.TabIndex = 112;
			this.butAutoNote.Text = "Auto Note";
			this.butAutoNote.Click += new System.EventHandler(this.butAutoNote_Click);
			// 
			// butUserPrefs
			// 
			this.butUserPrefs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUserPrefs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butUserPrefs.Autosize = true;
			this.butUserPrefs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUserPrefs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUserPrefs.CornerRadius = 4F;
			this.butUserPrefs.Location = new System.Drawing.Point(575, 7);
			this.butUserPrefs.Name = "butUserPrefs";
			this.butUserPrefs.Size = new System.Drawing.Size(75, 21);
			this.butUserPrefs.TabIndex = 113;
			this.butUserPrefs.Text = "User Prefs";
			this.butUserPrefs.Visible = false;
			this.butUserPrefs.Click += new System.EventHandler(this.butUserPrefs_Click);
			// 
			// FormCommItem
			// 
			this.ClientSize = new System.Drawing.Size(662, 594);
			this.Controls.Add(this.butUserPrefs);
			this.Controls.Add(this.butAutoNote);
			this.Controls.Add(this.labelSavedManually);
			this.Controls.Add(this.butNowEnd);
			this.Controls.Add(this.textDateTimeEnd);
			this.Controls.Add(this.labelDateTimeEnd);
			this.Controls.Add(this.butNow);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.textCommlogNum);
			this.Controls.Add(this.labelCommlogNum);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textPatientName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.listSentOrReceived);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listMode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTime);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(580, 445);
			this.Name = "FormCommItem";
			this.Text = "Communication Item";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCommItem_FormClosing);
			this.Load += new System.EventHandler(this.FormCommItem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCommItem_Load(object sender, System.EventArgs e) {
			IsStartingUp=true;
			textPatientName.Text=Patients.GetLim(CommlogCur.PatNum).GetNameFL();
			textUser.Text=Userods.GetName(CommlogCur.UserNum);//might be blank. 
			textDateTime.Text=CommlogCur.CommDateTime.ToShortDateString()+"  "+CommlogCur.CommDateTime.ToShortTimeString();
			if(CommlogCur.DateTimeEnd.Year>1880) {
				textDateTimeEnd.Text=CommlogCur.DateTimeEnd.ToShortDateString()+"  "+CommlogCur.DateTimeEnd.ToShortTimeString();
			}
			//there will usually be a commtype set before this dialog is opened
			for(int i=0;i<DefC.Short[(int)DefCat.CommLogTypes].Length;i++){
				listType.Items.Add(DefC.Short[(int)DefCat.CommLogTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.CommLogTypes][i].DefNum==CommlogCur.CommType){
					listType.SelectedIndex=i;
				}
			}
			for(int i=0;i<Enum.GetNames(typeof(CommItemMode)).Length;i++){
				listMode.Items.Add(Lan.g("enumCommItemMode",Enum.GetNames(typeof(CommItemMode))[i]));
			}
			listMode.SelectedIndex=(int)CommlogCur.Mode_;
			for(int i=0;i<Enum.GetNames(typeof(CommSentOrReceived)).Length;i++){
				listSentOrReceived.Items.Add
					(Lan.g("enumCommSentOrReceived",Enum.GetNames(typeof(CommSentOrReceived))[i]));
			}
			try{
				listSentOrReceived.SelectedIndex=(int)CommlogCur.SentOrReceived;
			}
			catch{
				MessageBox.Show(((int)CommlogCur.SentOrReceived).ToString());
			}
			//checkIsStatementSent.Checked=CommlogCur.IsStatementSent;
			textNote.Text=CommlogCur.Note;
			textNote.SelectionStart=textNote.Text.Length;
			#if !DEBUG
				labelCommlogNum.Visible=false;
				textCommlogNum.Visible=false;
			#endif
			textCommlogNum.Text=CommlogCur.CommlogNum.ToString();
			if(!PrefC.IsODHQ) {
				labelDateTimeEnd.Visible=false;
				textDateTimeEnd.Visible=false;
				butNow.Visible=false;
				butNowEnd.Visible=false;
			}
			textNote.Select();
			string keyData=GetSignatureKey();
			signatureBoxWrapper.FillSignature(CommlogCur.SigIsTopaz,keyData,CommlogCur.Signature);
			signatureBoxWrapper.BringToFront();
			IsStartingUp=false;
			if(!Security.IsAuthorized(Permissions.CommlogEdit,CommlogCur.CommDateTime)){
				//The user does not have permissions to create or edit commlogs.
				if(IsNew || IsPersistent) {
					DialogResult=DialogResult.Cancel;
					Close();
					return;
				}
				butDelete.Enabled=false;
				butOK.Enabled=false;
			}
			if(IsPersistent) {
				RefreshUserOdPrefs();
				labelCommlogNum.Visible=false;
				textCommlogNum.Visible=false;
				butUserPrefs.Visible=true;
				butOK.Text=Lan.g(this,"Create");
				butCancel.Text=Lan.g(this,"Close");
				butDelete.Visible=false;
				PatientChangedEvent.Fired+=PatientChangedEvent_Fired;
			}
			if(IsNew && PrefC.GetBool(PrefName.CommLogAutoSave)) {
				_commLogOld=CommlogCur.Copy();
				_commLogOld.Note="";
				_commLogOld.CommDateTime=PIn.DateT(textDateTime.Text);
				timerAutoSave.Start();
			}
			CommItemSaveEvent.Fired+=CommItemSaveEvent_Fired;
		}

		///<summary>Updates the class wide UserOdPrefs with their corresponding values from the database.</summary>
		private void RefreshUserOdPrefs() {
			if(Security.CurUser==null || Security.CurUser.UserNum < 1) {
				return;
			}
			_userOdPrefClearNote=UserOdPrefs.GetByUserAndFkeyType(Security.CurUser.UserNum,UserOdFkeyType.CommlogPersistClearNote).FirstOrDefault();
			_userOdPrefEndDate=UserOdPrefs.GetByUserAndFkeyType(Security.CurUser.UserNum,UserOdFkeyType.CommlogPersistClearEndDate).FirstOrDefault();
			_userOdPrefUpdateDateTimeNewPat=UserOdPrefs.GetByUserAndFkeyType(Security.CurUser.UserNum,UserOdFkeyType.CommlogPersistUpdateDateTimeWithNewPatient).FirstOrDefault();
		}

		private void PatientChangedEvent_Fired(ODEventArgs e) {
			if(e.Name!="FormOpenDental" || e.Tag.GetType()!=typeof(long) || this.IsDisposed) {
				return;
			}
			//The tag for this event is the newly selected PatNum
			CommlogCur.PatNum=(long)e.Tag;
			textPatientName.Text=Patients.GetLim(CommlogCur.PatNum).GetNameFL();
			if(IsPersistent && (_userOdPrefUpdateDateTimeNewPat==null || PIn.Bool(_userOdPrefUpdateDateTimeNewPat.ValueString))) {
				UpdateButNow();
			}
		}

		private void CommItemSaveEvent_Fired(CodeBase.ODEventArgs e) {
			if(e.Name!="CommItemSave") {
				return;
			}
			//save comm item
			SaveCommItem(false);
		}

		///<summary>Returns true if the commlog was able to save to the database.  Otherwise returns false.
		///Set showMsg to true to show a meaningful message when the commlog cannot be saved.</summary>
		private bool SaveCommItem(bool showMsg) {
			if(textDateTime.Text==""){
				if(showMsg) {
					MsgBox.Show(this,"Please enter a date first.");
				}
				return false;
			}
			try{
				DateTime.Parse(textDateTime.Text);
			}
			catch{
				if(showMsg) {
					MsgBox.Show(this,"Date / Time invalid.");
				}
				return false;
			}
			if(textDateTimeEnd.Text!="") {
				try {
					DateTime.Parse(textDateTimeEnd.Text);
				}
				catch {
					if(showMsg) {
						MsgBox.Show(this,"End date and time invalid.");
					}
					return false;
				}
				CommlogCur.DateTimeEnd=PIn.DateT(textDateTimeEnd.Text);
			}
			CommlogCur.CommDateTime=PIn.DateT(textDateTime.Text);
			//there may not be a commtype selected.
			if(listType.SelectedIndex==-1){
				CommlogCur.CommType=0;
			}
			else{
				CommlogCur.CommType=DefC.Short[(int)DefCat.CommLogTypes][listType.SelectedIndex].DefNum;
			}
			CommlogCur.Mode_=(CommItemMode)listMode.SelectedIndex;
			CommlogCur.SentOrReceived=(CommSentOrReceived)listSentOrReceived.SelectedIndex;
			CommlogCur.Note=textNote.Text;
			try {
				SaveSignature();
			}
			catch(Exception ex){
				if(showMsg) {
					MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
				}
				return false;
			}
			if(IsNew || IsPersistent) {
				Commlogs.Insert(CommlogCur);
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Insert");
				//Post insert persistent user preferences.
				if(IsPersistent) {
					if(_userOdPrefClearNote==null || PIn.Bool(_userOdPrefClearNote.ValueString)) {
						textNote.Clear();
					}
					if(_userOdPrefEndDate==null || PIn.Bool(_userOdPrefEndDate.ValueString)) {
						textDateTimeEnd.Clear();
						CommlogCur.DateTimeEnd=DateTime.MinValue;//Specifically set the variable to MinValue because DateTimeEnd is not always reconsidered.
					}
					ODException.SwallowAnyException(() => {
						FormOpenDental.S_RefreshCurrentModule();
					});
				}
			}
			else{
				Commlogs.Update(CommlogCur);
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"");
			}
			return true;
		}

		private void signatureBoxWrapper_SignatureChanged(object sender,EventArgs e) {
			CommlogCur.UserNum=Security.CurUser.UserNum;
			textUser.Text=Userods.GetName(CommlogCur.UserNum);
			SigChanged=true;
		}

		private void ClearSignature(){
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				//SigChanged=true;//happens automatically through the event.
				signatureBoxWrapper.ClearSignature();
			}
		}

		private string GetSignatureKey(){
			string keyData=CommlogCur.UserNum.ToString();
			keyData+=CommlogCur.CommDateTime.ToString();
			keyData+=CommlogCur.Mode_.ToString();
			keyData+=CommlogCur.SentOrReceived.ToString();
			if(CommlogCur.Note!=null){
				keyData+=CommlogCur.Note.ToString();
			}
			return keyData;
		}

		private void SaveSignature(){
			if(SigChanged){
				string keyData=GetSignatureKey();
				CommlogCur.Signature=signatureBoxWrapper.GetSignature(keyData);
				CommlogCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
			}
		}

		private void textDateTime_TextChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listType_SelectedIndexChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listMode_SelectedIndexChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void listSentOrReceived_SelectedValueChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void textNote_TextChanged(object sender,EventArgs e) {
			ClearSignature();
		}

		private void timerAutoSave_Tick(object sender,EventArgs e) {
			if(IsPersistent) {
				//Just in case the auto save timer got turned on in persistent mode.
				timerAutoSave.Stop();
				return;
			}
			if(textDateTime.Text=="") {
				return;
			}
			else {
				try { 
					CommlogCur.CommDateTime=DateTime.Parse(textDateTime.Text);
				}
				catch {
					return;
				}
			}
			if(textDateTimeEnd.Text=="") {
				CommlogCur.DateTimeEnd=DateTime.MinValue;//Not sure this is the proper action. Perhaps DateTime.Now?
			}
			else {
				try { 
					CommlogCur.DateTimeEnd=DateTime.Parse(textDateTimeEnd.Text);
				}
				catch {
					return;
				}
			}
			//there may not be a commtype selected.
			if(listType.SelectedIndex==-1) {
				CommlogCur.CommType=0;
			}
			else {
				CommlogCur.CommType=DefC.Short[(int)DefCat.CommLogTypes][listType.SelectedIndex].DefNum;
			}
			CommlogCur.Mode_=(CommItemMode)listMode.SelectedIndex;
			CommlogCur.SentOrReceived=(CommSentOrReceived)listSentOrReceived.SelectedIndex;
			CommlogCur.Note=textNote.Text;
			if(_commLogOld.Compare(CommlogCur)) {//They're equal, don't bother saving
				return;
			}
			if(IsNew) {
				//Insert
				Commlogs.Insert(CommlogCur);
				_commLogOld=CommlogCur.Copy();
				textCommlogNum.Text=CommlogCur.CommlogNum.ToString();
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Autosave Insert");
				IsNew=false;
				butCancel.Enabled=false;
				this.Text="Communication Item - Saved: "+DateTime.Now;
			}
			else {
				//Update
				Commlogs.Update(CommlogCur);
				_commLogOld=CommlogCur.Copy();
				this.Text="Communication Item - Saved: "+DateTime.Now;
			}
		}

		private void timerManualSave_Tick(object sender,EventArgs e) {
			labelSavedManually.Visible=false;
			timerManualSave.Stop();
		}

		private void butUserPrefs_Click(object sender,EventArgs e) {
			FormCommItemUserPrefs FormCIUP=new FormCommItemUserPrefs();
			FormCIUP.ShowDialog();
			if(FormCIUP.DialogResult==DialogResult.OK) {
				RefreshUserOdPrefs();
			}
		}

		private void butNow_Click(object sender,EventArgs e) {
			UpdateButNow();
		}

		///<summary>Helper method to update textDateTime with DateTime.Now</summary>
		private void UpdateButNow() {
			textDateTime.Text=DateTime.Now.ToShortDateString()+"  "+DateTime.Now.ToShortTimeString();
		}

		private void butNowEnd_Click(object sender,EventArgs e) {
			textDateTimeEnd.Text=DateTime.Now.ToShortDateString()+"  "+DateTime.Now.ToShortTimeString();
		}

		private void butAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textNote.AppendText(FormA.CompletedNote);
			}
		}

		private void butDelete_Click(object sender,System.EventArgs e) {
			//button not enabled if no permission and is invisible for persistent mode.
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			try {
				Commlogs.Delete(CommlogCur);
				SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Delete");
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(this,ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//button not enabled if no permission
			if(!SaveCommItem(true)) {
				return;
			}
			if(IsPersistent) {
				//Show the user an indicator that the commlog has been saved but do not close the window.
				labelSavedManually.Visible=true;
				timerManualSave.Start();
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void FormCommItem_FormClosing(object sender,FormClosingEventArgs e) {
			CommItemSaveEvent.Fired-=CommItemSaveEvent_Fired;
			if(IsPersistent) {
				PatientChangedEvent.Fired-=PatientChangedEvent_Fired;
				return;
			}
			if(DialogResult==DialogResult.Cancel && timerAutoSave.Enabled && !IsNew) {
				try {
					Commlogs.Delete(CommlogCur);
					SecurityLogs.MakeLogEntry(Permissions.CommlogEdit,CommlogCur.PatNum,"Autosaved Commlog Deleted");
				}
				catch(Exception ex) {
					MessageBox.Show(this,ex.Message);
				}
			}
			timerAutoSave.Stop();
			timerAutoSave.Enabled=false;
		}
	}

}
