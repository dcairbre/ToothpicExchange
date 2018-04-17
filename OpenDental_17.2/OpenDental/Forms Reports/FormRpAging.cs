using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CodeBase;
using OpenDental.ReportingComplex;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormRpAging : ODForm {
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.CheckBox checkIncludeNeg;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkOnlyNeg;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private ListBox listProv;
		private Label label3;
		private CheckBox checkProvAll;
		private CheckBox checkBillTypesAll;
		private CheckBox checkBadAddress;
		private CheckBox checkAllClin;
		private ListBox listClin;
		private Label labelClin;
		private System.Windows.Forms.RadioButton radioAny;
		private CheckBox checkHasDateLastPay;
		private GroupBox groupGroupBy;
		private RadioButton radioGroupByPat;
		private RadioButton radioGroupByFam;
		private CheckBox checkAgeWriteoffs;
		private List<Provider> _listProviders;
		private List<Def> _listBillingTypeDefs;

		///<summary></summary>
		public FormRpAging(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpAging));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkIncludeNeg = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkOnlyNeg = new System.Windows.Forms.CheckBox();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkProvAll = new System.Windows.Forms.CheckBox();
			this.checkBillTypesAll = new System.Windows.Forms.CheckBox();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.checkAllClin = new System.Windows.Forms.CheckBox();
			this.listClin = new System.Windows.Forms.ListBox();
			this.labelClin = new System.Windows.Forms.Label();
			this.checkHasDateLastPay = new System.Windows.Forms.CheckBox();
			this.groupGroupBy = new System.Windows.Forms.GroupBox();
			this.radioGroupByPat = new System.Windows.Forms.RadioButton();
			this.radioGroupByFam = new System.Windows.Forms.RadioButton();
			this.checkAgeWriteoffs = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupGroupBy.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(661, 392);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 17;
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
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butOK.Location = new System.Drawing.Point(580, 392);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 16;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.Location = new System.Drawing.Point(24, 114);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(196, 110);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12, 40);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(174, 18);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12, 84);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(174, 18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12, 62);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(174, 18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12, 18);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(174, 18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(143, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "As Of Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(36, 16);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(106, 20);
			this.textDate.TabIndex = 1;
			// 
			// listBillType
			// 
			this.listBillType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listBillType.Location = new System.Drawing.Point(233, 58);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(163, 186);
			this.listBillType.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(233, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Billing Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkIncludeNeg
			// 
			this.checkIncludeNeg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeNeg.Location = new System.Drawing.Point(12, 18);
			this.checkIncludeNeg.Name = "checkIncludeNeg";
			this.checkIncludeNeg.Size = new System.Drawing.Size(174, 18);
			this.checkIncludeNeg.TabIndex = 0;
			this.checkIncludeNeg.Text = "Include negative balances";
			this.checkIncludeNeg.Click += new System.EventHandler(this.checkIncludeNeg_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkOnlyNeg);
			this.groupBox2.Controls.Add(this.checkIncludeNeg);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(24, 230);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(196, 66);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Negative Balances";
			// 
			// checkOnlyNeg
			// 
			this.checkOnlyNeg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkOnlyNeg.Location = new System.Drawing.Point(12, 40);
			this.checkOnlyNeg.Name = "checkOnlyNeg";
			this.checkOnlyNeg.Size = new System.Drawing.Size(174, 18);
			this.checkOnlyNeg.TabIndex = 1;
			this.checkOnlyNeg.Text = "Only show negative balances";
			this.checkOnlyNeg.Click += new System.EventHandler(this.checkOnlyNeg_Click);
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(36, 302);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(280, 18);
			this.checkExcludeInactive.TabIndex = 5;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			// 
			// listProv
			// 
			this.listProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listProv.Location = new System.Drawing.Point(402, 58);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(163, 186);
			this.listProv.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(402, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(163, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "Providers";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkProvAll
			// 
			this.checkProvAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkProvAll.Checked = true;
			this.checkProvAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkProvAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkProvAll.Location = new System.Drawing.Point(402, 37);
			this.checkProvAll.Name = "checkProvAll";
			this.checkProvAll.Size = new System.Drawing.Size(163, 18);
			this.checkProvAll.TabIndex = 12;
			this.checkProvAll.Text = "All";
			this.checkProvAll.Click += new System.EventHandler(this.checkProvAll_Click);
			// 
			// checkBillTypesAll
			// 
			this.checkBillTypesAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBillTypesAll.Checked = true;
			this.checkBillTypesAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBillTypesAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBillTypesAll.Location = new System.Drawing.Point(233, 37);
			this.checkBillTypesAll.Name = "checkBillTypesAll";
			this.checkBillTypesAll.Size = new System.Drawing.Size(163, 18);
			this.checkBillTypesAll.TabIndex = 10;
			this.checkBillTypesAll.Text = "All";
			this.checkBillTypesAll.Click += new System.EventHandler(this.checkBillTypesAll_Click);
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(36, 324);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(280, 18);
			this.checkBadAddress.TabIndex = 6;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			// 
			// checkAllClin
			// 
			this.checkAllClin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkAllClin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAllClin.Location = new System.Drawing.Point(571, 37);
			this.checkAllClin.Name = "checkAllClin";
			this.checkAllClin.Size = new System.Drawing.Size(163, 18);
			this.checkAllClin.TabIndex = 14;
			this.checkAllClin.Text = "All";
			this.checkAllClin.Click += new System.EventHandler(this.checkAllClin_Click);
			// 
			// listClin
			// 
			this.listClin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listClin.Location = new System.Drawing.Point(571, 58);
			this.listClin.Name = "listClin";
			this.listClin.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClin.Size = new System.Drawing.Size(163, 186);
			this.listClin.TabIndex = 15;
			this.listClin.Click += new System.EventHandler(this.listClin_Click);
			// 
			// labelClin
			// 
			this.labelClin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelClin.Location = new System.Drawing.Point(571, 17);
			this.labelClin.Name = "labelClin";
			this.labelClin.Size = new System.Drawing.Size(163, 17);
			this.labelClin.TabIndex = 0;
			this.labelClin.Text = "Clinics";
			this.labelClin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkHasDateLastPay
			// 
			this.checkHasDateLastPay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHasDateLastPay.Location = new System.Drawing.Point(36, 346);
			this.checkHasDateLastPay.Name = "checkHasDateLastPay";
			this.checkHasDateLastPay.Size = new System.Drawing.Size(280, 18);
			this.checkHasDateLastPay.TabIndex = 7;
			this.checkHasDateLastPay.Text = "Include Last Payment Date (Landscape orientation)";
			// 
			// groupGroupBy
			// 
			this.groupGroupBy.Controls.Add(this.radioGroupByPat);
			this.groupGroupBy.Controls.Add(this.radioGroupByFam);
			this.groupGroupBy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupGroupBy.Location = new System.Drawing.Point(24, 42);
			this.groupGroupBy.Name = "groupGroupBy";
			this.groupGroupBy.Size = new System.Drawing.Size(196, 66);
			this.groupGroupBy.TabIndex = 2;
			this.groupGroupBy.TabStop = false;
			this.groupGroupBy.Text = "Group By";
			// 
			// radioGroupByPat
			// 
			this.radioGroupByPat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioGroupByPat.Location = new System.Drawing.Point(12, 40);
			this.radioGroupByPat.Name = "radioGroupByPat";
			this.radioGroupByPat.Size = new System.Drawing.Size(174, 18);
			this.radioGroupByPat.TabIndex = 1;
			this.radioGroupByPat.Text = "Individual";
			this.radioGroupByPat.UseVisualStyleBackColor = true;
			// 
			// radioGroupByFam
			// 
			this.radioGroupByFam.Checked = true;
			this.radioGroupByFam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioGroupByFam.Location = new System.Drawing.Point(12, 18);
			this.radioGroupByFam.Name = "radioGroupByFam";
			this.radioGroupByFam.Size = new System.Drawing.Size(174, 18);
			this.radioGroupByFam.TabIndex = 0;
			this.radioGroupByFam.TabStop = true;
			this.radioGroupByFam.Text = "Family";
			this.radioGroupByFam.UseVisualStyleBackColor = true;
			// 
			// checkAgeWriteoffs
			// 
			this.checkAgeWriteoffs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAgeWriteoffs.Location = new System.Drawing.Point(36, 368);
			this.checkAgeWriteoffs.Name = "checkAgeWriteoffs";
			this.checkAgeWriteoffs.Size = new System.Drawing.Size(280, 18);
			this.checkAgeWriteoffs.TabIndex = 8;
			this.checkAgeWriteoffs.Text = "Age writeoff estimates";
			// 
			// FormRpAging
			// 
			this.AcceptButton = this.butOK;
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(748, 430);
			this.Controls.Add(this.checkAgeWriteoffs);
			this.Controls.Add(this.groupGroupBy);
			this.Controls.Add(this.checkHasDateLastPay);
			this.Controls.Add(this.checkAllClin);
			this.Controls.Add(this.listClin);
			this.Controls.Add(this.labelClin);
			this.Controls.Add(this.checkBadAddress);
			this.Controls.Add(this.checkBillTypesAll);
			this.Controls.Add(this.checkProvAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkExcludeInactive);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpAging";
			this.ShowInTaskbar = false;
			this.Text = "Aging of Accounts Receivable Report";
			this.Load += new System.EventHandler(this.FormAging_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupGroupBy.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAging_Load(object sender, System.EventArgs e) {
			_listProviders=ProviderC.GetListReports();
			DateTime lastAgingDate=PrefC.GetDate(PrefName.DateLastAging);
			if(lastAgingDate.Year<1880) {
				textDate.Text="";
			}
			else if(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)){
				textDate.Text=lastAgingDate.ToShortDateString();
			}
			else{
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			_listBillingTypeDefs=new List<Def>(DefC.GetList(DefCat.BillingTypes));
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			if(listBillType.Items.Count>0){
				listBillType.SelectedIndex=0;
			}
			listBillType.Visible=false;
			checkBillTypesAll.Checked=true;
			for(int i=0;i<_listProviders.Count;i++){
				listProv.Items.Add(_listProviders[i].GetLongDesc());
			}
			if(listProv.Items.Count>0) {
				listProv.SelectedIndex=0;
			}
			checkProvAll.Checked=true;
			listProv.Visible=false;
			if(!PrefC.HasClinicsEnabled) {
				listClin.Visible=false;
				labelClin.Visible=false;
				checkAllClin.Visible=false;
			}
			else {
				List<Clinic> listClinics = Clinics.GetForUserod(Security.CurUser,true,"Unassigned").ToList();
				if(!listClinics.Exists(x => x.ClinicNum==Clinics.ClinicNum)) {//Could have a hidden clinic selected
					listClinics.Add(Clinics.GetClinic(Clinics.ClinicNum));
				}
				foreach(Clinic clin in listClinics) {
					ODBoxItem<Clinic> boxItemCur;
					if(clin.IsHidden) {
						boxItemCur=new ODBoxItem<Clinic>(clin.Abbr+" "+Lan.g(this,"(hidden)"),clin);
					}
					else {
						boxItemCur=new ODBoxItem<Clinic>(clin.Abbr,clin);
					}
					listClin.Items.Add(boxItemCur);
					if(clin.ClinicNum == Clinics.ClinicNum) {
						listClin.SelectedItem = boxItemCur;
					}
				}
				if(Clinics.ClinicNum==0) {
					checkAllClin.Checked=true;
					listClin.Visible=false;
				}
			}
		}

		private void checkBillTypesAll_Click(object sender,EventArgs e) {
			if(checkBillTypesAll.Checked){
				listBillType.Visible=false;
			}
			else{
				listBillType.Visible=true;
			}
		}

		private void checkProvAll_Click(object sender,EventArgs e) {
			if(checkProvAll.Checked) {
				listProv.Visible=false;
			}
			else {
				listProv.Visible=true;
			}
		}

		private void checkIncludeNeg_Click(object sender, System.EventArgs e) {
			if(checkIncludeNeg.Checked){
				checkOnlyNeg.Checked=false;
			}
		}

		private void checkOnlyNeg_Click(object sender, System.EventArgs e) {
			if(checkOnlyNeg.Checked){
				checkIncludeNeg.Checked=false;
			}
		}

		private void checkAllClin_Click(object sender,EventArgs e) {
			if(checkAllClin.Checked) {
				listClin.Visible=false;
			}
			else {
				listClin.Visible=true;
			}
		}

		private void listClin_Click(object sender,EventArgs e) {
			checkAllClin.Checked=false;//will not clear all selected indices
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!checkBillTypesAll.Checked && listBillType.SelectedIndices.Count==0){
				MsgBox.Show(this,"At least one billing type must be selected.");
				return;
			}
			if(!checkProvAll.Checked && listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(PrefC.HasClinicsEnabled && !checkAllClin.Checked && listClin.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one clinic must be selected.");
				return;
			}
			if(textDate.errorProvider1.GetError(textDate)!="") {
				MsgBox.Show(this,"Invalid date.");
				return;
			}
			DateTime asOfDate=PIn.Date(textDate.Text);
			List<long> listProvNums=new List<long>();
			List<long> listClinicNums=new List<long>();
			List<long> listBillingTypeDefNums=new List<long>();
			if(!checkProvAll.Checked) {
				listProvNums=listProv.SelectedIndices.OfType<int>().Select(x => _listProviders[x].ProvNum).ToList();
			}
			if(PrefC.HasClinicsEnabled) {
				//if "All" is selected and the user is not restricted, show ALL clinics, including the 0 clinic.
				if(checkAllClin.Checked && !Security.CurUser.ClinicIsRestricted) { 
					listClinicNums.Clear();
					listClinicNums.Add(0);
					Clinics.ListLong.ForEach(x => listClinicNums.Add(x.ClinicNum));
				}
				else {
					listClinicNums=listClin.SelectedItems.OfType<ODBoxItem<Clinic>>().Select(x => x.Tag.ClinicNum).ToList();
				}
			}
			if(!checkBillTypesAll.Checked) {
				for(int i=0;i<listBillType.SelectedIndices.Count;i++) {
					listBillingTypeDefNums.Add(_listBillingTypeDefs[listBillType.SelectedIndices[i]].DefNum);
				}
			}
			AgeOfAccount accountAge=AgeOfAccount.Any;
			if(radioAny.Checked) {
				accountAge=AgeOfAccount.Any;
			}
			else if(radio30.Checked) {
				accountAge=AgeOfAccount.Over30;
			}
			else if(radio60.Checked) {
				accountAge=AgeOfAccount.Over60;
			}
			else if(radio90.Checked) {
				accountAge=AgeOfAccount.Over90;
			}
			DataTable tableAging=new DataTable();
			tableAging=RpAging.GetAgingTable(asOfDate,checkAgeWriteoffs.Checked,checkHasDateLastPay.Checked,radioGroupByFam.Checked,checkOnlyNeg.Checked,
				accountAge,checkIncludeNeg.Checked,checkExcludeInactive.Checked,checkBadAddress.Checked,listProvNums,listClinicNums,listBillingTypeDefNums);
			ReportComplex report=new ReportComplex(true,false); 
			report.IsLandscape=checkHasDateLastPay.Checked;
			report.ReportName=Lan.g(this,"AGING OF ACCOUNTS RECEIVABLE REPORT");
			report.AddTitle("Aging Report",Lan.g(this, "AGING OF ACCOUNTS RECEIVABLE"));
			report.AddSubTitle("PracTitle",PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle("AsOf",Lan.g(this,"As of ")+textDate.Text);
			if(radioAny.Checked){
				report.AddSubTitle("Balance",Lan.g(this,"Any Balance"));
			}
			if(radio30.Checked){
				report.AddSubTitle("Over30",Lan.g(this,"Over 30 Days"));
			}
			if(radio60.Checked){
				report.AddSubTitle("Over60",Lan.g(this,"Over 60 Days"));
			}
			if(radio90.Checked){
				report.AddSubTitle("Over90",Lan.g(this,"Over 90 Days"));
			}
			if(checkBillTypesAll.Checked){
				report.AddSubTitle("AllBillingTypes",Lan.g(this,"All Billing Types"));
			}
			else{
				string subt=DefC.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[0]].ItemName;
				for(int i=1;i<listBillType.SelectedIndices.Count;i++){
					subt+=", "+DefC.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].ItemName;
				}
				report.AddSubTitle("",subt);
			}
			string subtitleProvs="";
			if(checkProvAll.Checked) {
				subtitleProvs=Lan.g(this,"All Providers");
			}
			else {
				subtitleProvs+=string.Join(", ",listProv.SelectedIndices.OfType<int>().ToList().Select(x => _listProviders[x].Abbr));
			}
			report.AddSubTitle("Providers",subtitleProvs);
			if(checkAllClin.Checked) {
				report.AddSubTitle("Clinics",Lan.g(this,"All Clinics"));
			}
			else {
				string subt = string.Join(", ",listClin.SelectedItems.OfType<ODBoxItem<Clinic>>().Select(x => x.Tag.Abbr));
				report.AddSubTitle("Clinics",subt);
			}
			//Patient Account Aging Query-----------------------------------------------
			bool isWoEstIncluded=true;
			if(checkAgeWriteoffs.Checked && tableAging.Select().All(x => Math.Abs(PIn.Double(x["InsWoEst"].ToString()))<=0.005)) {
				tableAging.Columns.Remove("InsWoEst");
				isWoEstIncluded=false;
			}
			QueryObject query=report.AddQuery(tableAging,"Date "+DateTime.Today.ToShortDateString());
			query.AddColumn((radioGroupByFam.Checked?"GUARANTOR":"PATIENT"),160,FieldValueType.String);
			query.AddColumn("0-30 DAYS",75,FieldValueType.Number);
			query.AddColumn("31-60 DAYS",75,FieldValueType.Number);
			query.AddColumn("61-90 DAYS",75,FieldValueType.Number);
			query.AddColumn("> 90 DAYS",75,FieldValueType.Number);
			query.AddColumn("TOTAL", 80, FieldValueType.Number);
			if(isWoEstIncluded) {
				query.AddColumn("-W/O EST",75,FieldValueType.Number);
			}
			query.AddColumn("-INS EST",75,FieldValueType.Number);
			query.AddColumn("=PATIENT",80,FieldValueType.Number);
			if(checkHasDateLastPay.Checked) {
				query.AddColumn("",10);//add some space between the right alligned amounts and the left alligned date
				query.AddColumn("LAST PAY DATE",100,FieldValueType.Date);
			}
			report.AddPageNum();
			report.AddGridLines();
			if(!report.SubmitQueries()) {
				return;
			}
			FormReportComplex FormR=new FormReportComplex(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

		

		

	}



}
