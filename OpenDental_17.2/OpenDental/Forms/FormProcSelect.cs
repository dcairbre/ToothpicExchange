using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Linq;

namespace OpenDental{
	/// <summary></summary>
	public class FormProcSelect : ODForm {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private long _patNumCur;
		private ODGrid gridMain;
		///<summary>If form closes with OK, this contains selected proc num.</summary>
		public List<Procedure> ListSelectedProcs;
    ///<summary>A list of completed procedures that are associated to this patient or their payment plans.</summary>
    private List<Procedure> _listProcedures;
    private List<PaySplit> _listPaySplits;
    private List<Adjustment> _listAdjustments;
    private List<PayPlanCharge> _listPayPlanCharges;
    private List<ClaimProc> _listInsPayAsTotal;
    private List<ClaimProc> _listClaimProcs;
    private List<AccountEntry> _listAccountCharges;
    ///<summary>List of paysplits for the current payment.</summary>
    public List<PaySplit> ListSplitsCur= new List<PaySplit>();
		///<summary>Set to true to enable multiple procedure selection mode.</summary>
		public bool IsMultiSelect;
		private GroupBox groupCreditLogic;
		private RadioButton radioShowAll;
		private RadioButton radioExplicit;
		private RadioButton radioFIFO;

		///<summary>Does not perform FIFO logic.</summary>
		private bool _isSimpleView;

		///<summary>This form only displays completed procedures to pick from.</summary>
		public FormProcSelect(long patNum, bool isSimpleView)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			_patNumCur=patNum;
			_isSimpleView=isSimpleView;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcSelect));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupCreditLogic = new System.Windows.Forms.GroupBox();
			this.radioShowAll = new System.Windows.Forms.RadioButton();
			this.radioExplicit = new System.Windows.Forms.RadioButton();
			this.radioFIFO = new System.Windows.Forms.RadioButton();
			this.groupCreditLogic.SuspendLayout();
			this.SuspendLayout();
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
			this.gridMain.Location = new System.Drawing.Point(15, 76);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(666, 475);
			this.gridMain.TabIndex = 140;
			this.gridMain.Title = "Procedures";
			this.gridMain.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridMain.TitleHeight = 18;
			this.gridMain.TranslationName = "TableProcSelect";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(525, 553);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(606, 553);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupCreditLogic
			// 
			this.groupCreditLogic.Controls.Add(this.radioShowAll);
			this.groupCreditLogic.Controls.Add(this.radioExplicit);
			this.groupCreditLogic.Controls.Add(this.radioFIFO);
			this.groupCreditLogic.Location = new System.Drawing.Point(15, 1);
			this.groupCreditLogic.Name = "groupCreditLogic";
			this.groupCreditLogic.Size = new System.Drawing.Size(331, 73);
			this.groupCreditLogic.TabIndex = 143;
			this.groupCreditLogic.TabStop = false;
			this.groupCreditLogic.Text = "Credit Filter";
			// 
			// radioShowAll
			// 
			this.radioShowAll.Location = new System.Drawing.Point(20, 51);
			this.radioShowAll.Name = "radioShowAll";
			this.radioShowAll.Size = new System.Drawing.Size(305, 18);
			this.radioShowAll.TabIndex = 2;
			this.radioShowAll.Text = "Exclude all credits";
			this.radioShowAll.UseVisualStyleBackColor = true;
			this.radioShowAll.Click += new System.EventHandler(this.radioCreditCalc_Click);
			// 
			// radioExplicit
			// 
			this.radioExplicit.Location = new System.Drawing.Point(20, 33);
			this.radioExplicit.Name = "radioExplicit";
			this.radioExplicit.Size = new System.Drawing.Size(305, 18);
			this.radioExplicit.TabIndex = 1;
			this.radioExplicit.Text = "Only include credits explicitly attached";
			this.radioExplicit.UseVisualStyleBackColor = true;
			this.radioExplicit.Click += new System.EventHandler(this.radioCreditCalc_Click);
			// 
			// radioFIFO
			// 
			this.radioFIFO.Checked = true;
			this.radioFIFO.Location = new System.Drawing.Point(20, 15);
			this.radioFIFO.Name = "radioFIFO";
			this.radioFIFO.Size = new System.Drawing.Size(305, 18);
			this.radioFIFO.TabIndex = 0;
			this.radioFIFO.TabStop = true;
			this.radioFIFO.Text = "Include all credits (FIFO)";
			this.radioFIFO.UseVisualStyleBackColor = true;
			this.radioFIFO.Click += new System.EventHandler(this.radioCreditCalc_Click);
			// 
			// FormProcSelect
			// 
			this.ClientSize = new System.Drawing.Size(693, 586);
			this.Controls.Add(this.groupCreditLogic);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcSelect";
			this.ShowInTaskbar = false;
			this.Text = "Select Procedure";
			this.Load += new System.EventHandler(this.FormProcSelect_Load);
			this.groupCreditLogic.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcSelect_Load(object sender,System.EventArgs e) {
			if(IsMultiSelect) {
				gridMain.SelectionMode=OpenDental.UI.GridSelectionMode.MultiExtended;
			}
			ListSelectedProcs=new List<Procedure>();
			_listProcedures=Procedures.GetCompleteForPats(new List<long> { _patNumCur });
			_listAdjustments=Adjustments.GetAdjustForPats(new List<long> { _patNumCur });
			_listPayPlanCharges=PayPlanCharges.GetDueForPayPlans(PayPlans.GetForPats(null,_patNumCur),_patNumCur).ToList();//Does not get charges for the future.
			_listPaySplits=PaySplits.GetForPats(new List<long> { _patNumCur }).Where(x => x.UnearnedType==0).ToList();//Might contain payplan payments. Do not include unearned.
			_listInsPayAsTotal=ClaimProcs.GetByTotForPats(new List<long> { _patNumCur });
			_listClaimProcs=ClaimProcs.GetForProcs(_listProcedures.Select(x => x.ProcNum).ToList());
			FillGrid();
		}

		private void FillGrid(){
			CreditCalcType credCalc;
			if(radioFIFO.Checked) {
				credCalc = CreditCalcType.FIFO;
			}
			else if(radioExplicit.Checked) {
				credCalc = CreditCalcType.ExplicitOnly;
			}
			else {
				credCalc= CreditCalcType.ExcludeAll;
			}
      _listAccountCharges=AccountModules.GetListUnpaidAccountCharges(_listProcedures, _listAdjustments,
				_listPaySplits, _listClaimProcs, _listPayPlanCharges, _listInsPayAsTotal, credCalc, ListSplitsCur);
      gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcSelect","Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Prov"),55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcSelect","Code"),55);
			gridMain.Columns.Add(col);
			if(Clinics.IsMedicalPracticeOrClinic(Clinics.ClinicNum)) {
				col=new ODGridColumn(Lan.g("TableProcSelect","Description"),290);
				gridMain.Columns.Add(col);
			}
			else {
				col=new ODGridColumn(Lan.g("TableProcSelect","Tooth"),40);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableProcSelect","Description"),250);
				gridMain.Columns.Add(col);
      }
      col=new ODGridColumn(Lan.g("TableProcSelect","Amt Orig"),60,HorizontalAlignment.Right);
      gridMain.Columns.Add(col);
      col=new ODGridColumn(Lan.g("TableProcSelect","Amt Start"),60,HorizontalAlignment.Right);
      gridMain.Columns.Add(col);
      col=new ODGridColumn(Lan.g("TableProcSelect","Amt End"),60,HorizontalAlignment.Right);
      gridMain.Columns.Add(col);
      gridMain.Rows.Clear();
			ODGridRow row;
      List<ProcedureCode> listProcCodes = ProcedureCodeC.Listt;
      foreach(AccountEntry entry in _listAccountCharges) {
        if(entry.GetType()!=typeof(Procedure) || entry.AmountEnd <=0) {
          continue;
        }
        Procedure procCur = (Procedure)entry.Tag;
        ProcedureCode procCodeCur = ProcedureCodes.GetProcCode(procCur.CodeNum,listProcCodes);
        row=new ODGridRow();
        row.Cells.Add(procCur.ProcDate.ToShortDateString());
        row.Cells.Add(Providers.GetAbbr(entry.ProvNum));
        row.Cells.Add(procCodeCur.ProcCode);
        if(!Clinics.IsMedicalPracticeOrClinic(Clinics.ClinicNum)) {
          row.Cells.Add(Tooth.ToInternat(procCur.ToothNum));
        }
        row.Cells.Add(procCodeCur.Descript);
        row.Cells.Add(entry.AmountOriginal.ToString("f"));
        row.Cells.Add(entry.AmountStart.ToString("f"));
        row.Cells.Add(entry.AmountEnd.ToString("f"));
				row.Tag=procCur;
        gridMain.Rows.Add(row);
      }
			gridMain.EndUpdate();
		}

    private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			ListSelectedProcs.Add((Procedure)gridMain.Rows[e.Row].Tag);
			DialogResult=DialogResult.OK;
		}

		private void radioCreditCalc_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				ListSelectedProcs.Add((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
    }
	}
}





















