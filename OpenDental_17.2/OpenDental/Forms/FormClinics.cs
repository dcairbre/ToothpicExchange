using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClinics:ODForm {
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private UI.ODGrid gridMain;
		public bool IsSelectionMode;
		public long SelectedClinicNum;
		private UI.Button butOK;
		private UI.Button butDown;
		private UI.Button butUp;
		private CheckBox checkOrderAlphabetical;
		///<summary>Set this list prior to loading this window to use a custom list of clinics.  Otherwise, uses the cache.</summary>
		public List<Clinic> ListClinics;
		///<summary>This list will be a copy of _listclinics and is used for syncing on window closed.</summary>
		public List<Clinic> ListClinicsOld;
		private CheckBox checkShowHidden;
		///<summary>A deep copy of all Clinic Specialty defs when this window loaded.  Used for display purposes.</summary>
		private List<Def> _listClinicSpecialtyDefs=new List<Def>();

		///<summary>Set to true prior to loading to include a 'Headquarters' option.</summary>
		public bool IncludeHQInList;

		///<summary></summary>
		public FormClinics()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClinics));
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.checkOrderAlphabetical = new System.Windows.Forms.CheckBox();
			this.checkShowHidden = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(304, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "This is usually only used if you have multiple locations";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.gridMain.Location = new System.Drawing.Point(12, 37);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(470, 561);
			this.gridMain.TabIndex = 12;
			this.gridMain.Title = "Clinics";
			this.gridMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridMain.TitleHeight = 18;
			this.gridMain.TranslationName = "TableClinics";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
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
			this.butAdd.Location = new System.Drawing.Point(487, 37);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(487, 572);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(487, 540);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 13;
			this.butOK.Text = "&OK";
			this.butOK.Visible = false;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(487, 234);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(75, 26);
			this.butDown.TabIndex = 14;
			this.butDown.Text = "Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(487, 202);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(75, 26);
			this.butUp.TabIndex = 15;
			this.butUp.Text = "Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// checkOrderAlphabetical
			// 
			this.checkOrderAlphabetical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkOrderAlphabetical.Location = new System.Drawing.Point(487, 159);
			this.checkOrderAlphabetical.Name = "checkOrderAlphabetical";
			this.checkOrderAlphabetical.Size = new System.Drawing.Size(87, 37);
			this.checkOrderAlphabetical.TabIndex = 16;
			this.checkOrderAlphabetical.Text = "Order Alphabetical";
			this.checkOrderAlphabetical.UseVisualStyleBackColor = true;
			this.checkOrderAlphabetical.Click += new System.EventHandler(this.checkOrderAlphabetical_Click);
			// 
			// checkShowHidden
			// 
			this.checkShowHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkShowHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowHidden.Checked = true;
			this.checkShowHidden.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowHidden.Location = new System.Drawing.Point(358, 12);
			this.checkShowHidden.Name = "checkShowHidden";
			this.checkShowHidden.Size = new System.Drawing.Size(124, 18);
			this.checkShowHidden.TabIndex = 17;
			this.checkShowHidden.Text = "Show Hidden";
			this.checkShowHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowHidden.UseVisualStyleBackColor = true;
			this.checkShowHidden.CheckedChanged += new System.EventHandler(this.checkShowHidden_CheckedChanged);
			// 
			// FormClinics
			// 
			this.ClientSize = new System.Drawing.Size(574, 610);
			this.Controls.Add(this.checkShowHidden);
			this.Controls.Add(this.checkOrderAlphabetical);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(587, 376);
			this.Name = "FormClinics";
			this.ShowInTaskbar = false;
			this.Text = "Clinics";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClinics_Closing);
			this.Load += new System.EventHandler(this.FormClinics_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClinics_Load(object sender, System.EventArgs e) {
			if(ListClinics==null) {
				ListClinics=Clinics.GetAllForUserod(Security.CurUser);
			}
			ListClinicsOld=ListClinics.Select(x => x.Copy()).ToList();
			_listClinicSpecialtyDefs=DefC.GetListLong(DefCat.ClinicSpecialty);
			checkOrderAlphabetical.Checked=PrefC.GetBool(PrefName.ClinicListIsAlphabetical);
			if(IsSelectionMode) {
				butAdd.Visible=false;
				butOK.Visible=true;
				butUp.Visible=false;
				butDown.Visible=false;
				checkOrderAlphabetical.Visible=false;
			}
			else {
				if(checkOrderAlphabetical.Checked) {
					butUp.Enabled=false;
					butDown.Enabled=false;
				}
				else {
					butUp.Enabled=true;
					butDown.Enabled=true;
				}
			}			
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableClinics","Abbr"),120));
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableClinics","Description"),200));
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableClinics","Specialty"),80));
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableClinics","Hidden"),0,HorizontalAlignment.Center));
			gridMain.Rows.Clear();
			ODGridRow row;
			if(IncludeHQInList) {
				row=new ODGridRow();
				row.Tag=new Clinic();//With a ClinicNum of 0, this will act as Headquarters.
				row.Cells.Add("");
				row.Cells.Add(Lan.g("TableClinics","Headquarters"));
				row.Cells.Add("");
				row.Cells.Add("");
				gridMain.Rows.Add(row);
			}
			for(int i=0;i<ListClinics.Count;i++) {
				if(!checkShowHidden.Checked && ListClinics[i].IsHidden) {
					continue;
				}
				row=new ODGridRow();
				row.Tag=ListClinics[i];
				row.Cells.Add(ListClinics[i].Abbr);
				row.Cells.Add(ListClinics[i].Description);
				row.Cells.Add(DefC.GetName(ListClinics[i].Specialty,_listClinicSpecialtyDefs));
				row.Cells.Add(ListClinics[i].IsHidden ? "X" : "");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Clinic ClinicCur=new Clinic();
			if(PrefC.GetBool(PrefName.PracticeIsMedicalOnly)) {
				ClinicCur.IsMedicalOnly=true;
			}
			ClinicCur.ItemOrder=gridMain.Rows.Count;//Set it last in the last position.
			FormClinicEdit FormCE=new FormClinicEdit(ClinicCur);
			FormCE.IsNew=true;
			if(FormCE.ShowDialog()==DialogResult.OK) {
				ListClinics.Add(ClinicCur);
			}
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.Rows.Count==0){
				return;
			}
			if(IsSelectionMode) {
				SelectedClinicNum=((Clinic)gridMain.Rows[e.Row].Tag).ClinicNum;
				DialogResult=DialogResult.OK;
				return;
			}
			FormClinicEdit FormCE=new FormClinicEdit(((Clinic)gridMain.Rows[e.Row].Tag).Copy());
			if(FormCE.ShowDialog()==DialogResult.OK) {
				if(FormCE.ClinicCur==null) {//Clinic was deleted
					//Fix ItemOrders
					for(int i=0;i<ListClinics.Count;i++) {
						if(ListClinics[i].ItemOrder>((Clinic)gridMain.Rows[e.Row].Tag).ItemOrder) {
							ListClinics[i].ItemOrder--;//Fix item orders
						}
					}
					ListClinics.Remove((Clinic)gridMain.Rows[e.Row].Tag);
				}
				else {//Changes could have been made to this clinic within the edit window.  Update our list of clinics that we are going to sync later.
					ListClinics[ListClinics.IndexOf((Clinic)gridMain.Rows[e.Row].Tag)]=FormCE.ClinicCur;
				}
			}
			FillGrid();			
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a clinic first.");
				return;
			}
			//Already at the top of the list
			if(gridMain.GetSelectedIndex()==0) {
				return;
			}
			int selectedIdx=gridMain.GetSelectedIndex();
			//Swap clinic ItemOrders
			Clinic sourceClin = ((Clinic)gridMain.Rows[selectedIdx].Tag);
			Clinic destClin = ((Clinic)gridMain.Rows[selectedIdx-1].Tag);
			int sourceOrder=sourceClin.ItemOrder;
			sourceClin.ItemOrder = destClin.ItemOrder;
			destClin.ItemOrder = sourceOrder;
			//Move selected clinic up
			ListClinics.Sort(ClinicSort);
			FillGrid();
			//Reselect the clinic that was moved
			gridMain.SetSelected(selectedIdx-1,true);
			gridMain.SetSelected(selectedIdx,false);
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a clinic first.");
				return;
			}
			//Already at the bottom of the list
			if(gridMain.GetSelectedIndex()==gridMain.Rows.Count-1) {
				return;
			}
			int selectedIdx=gridMain.GetSelectedIndex();
			//Swap clinic ItemOrders
			Clinic sourceClin = ((Clinic)gridMain.Rows[selectedIdx].Tag);
			Clinic destClin = ((Clinic)gridMain.Rows[selectedIdx+1].Tag);
			int sourceOrder=sourceClin.ItemOrder;
			sourceClin.ItemOrder = destClin.ItemOrder;
			destClin.ItemOrder = sourceOrder;
			//Move selected clinic down
			ListClinics.Sort(ClinicSort);
			FillGrid();
			//Reselect the clinic that was moved
			gridMain.SetSelected(selectedIdx+1,true);
			gridMain.SetSelected(selectedIdx,false);
		}

		private void checkOrderAlphabetical_Click(object sender,EventArgs e) {
			if(checkOrderAlphabetical.Checked) {
				butUp.Enabled=false;
				butDown.Enabled=false;
			}
			else {
				butUp.Enabled=true;
				butDown.Enabled=true;
			}
			ListClinics.Sort(ClinicSort);//Sorts based on the status of the checkbox.
			FillGrid();
		}

		private int ClinicSort(Clinic x,Clinic y) {
			if(checkOrderAlphabetical.Checked) {
				return x.Abbr.CompareTo(y.Abbr);
			}
			return x.ItemOrder.CompareTo(y.ItemOrder);
		}

		private void checkShowHidden_CheckedChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(IsSelectionMode && gridMain.SelectedIndices.Length>0) {
				SelectedClinicNum=((Clinic)gridMain.Rows[gridMain.GetSelectedIndex()].Tag).ClinicNum;
				DialogResult=DialogResult.OK;
			}
			Close();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			SelectedClinicNum=0;
			Close();
		}

		private void FormClinics_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(IsSelectionMode) {
				return;
			}
			if(Clinics.Sync(ListClinics,ListClinicsOld)) {
				DataValid.SetInvalid(InvalidType.Providers);
			}
			if(Prefs.UpdateBool(PrefName.ClinicListIsAlphabetical,checkOrderAlphabetical.Checked)) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			//Joe - Now that we have called sync on ListClinics we want to make sure that each clinic has program properties for PayConnect and XCharge
			//We are doing this because of a previous bug that caused some customers to have over 3.4 million duplicate rows in their programproperty table
			long payConnectProgNum=Programs.GetProgramNum(ProgramName.PayConnect);
			long xChargeProgNum=Programs.GetProgramNum(ProgramName.Xcharge);
			bool hasChanges=ProgramProperties.InsertForClinic(payConnectProgNum,
				ListClinics.Select(x => x.ClinicNum)
					.Where(x => ProgramProperties.GetListForProgramAndClinic(payConnectProgNum,x).Count==0).ToList());
			hasChanges=ProgramProperties.InsertForClinic(xChargeProgNum,
				ListClinics.Select(x => x.ClinicNum)
					.Where(x => ProgramProperties.GetListForProgramAndClinic(xChargeProgNum,x).Count==0).ToList()) || hasChanges;//prevent short curcuit
			if(hasChanges) {
				DataValid.SetInvalid(InvalidType.Programs);
			}
		}
	}
}





















