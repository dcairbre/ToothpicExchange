using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormDiscountPlans:ODForm {
		public bool IsSelectionMode;
		public DiscountPlan SelectedPlan;

		public FormDiscountPlans() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDiscountPlans_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			List<DiscountPlan> listDiscountPlans=DiscountPlans.GetAll();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableDiscountPlans","Description"),200));
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableDiscountPlans","Fee Schedule"),250));
			gridMain.Columns.Add(new ODGridColumn(Lan.g("TableDiscountPlans","Adjustment Type"),0));
			gridMain.Rows.Clear();
			ODGridRow row;
			int selectedIdx=-1;
			Def[][] arrayDefs=DefC.GetArrayLong();
			List<FeeSched> listFeeScheds=FeeSchedC.GetListLong();
			for(int i=0;i<listDiscountPlans.Count;i++) {
				Def adjType=DefC.GetDef(DefCat.AdjTypes,listDiscountPlans[i].DefNum,arrayDefs);
				row=new ODGridRow();
				row.Cells.Add(listDiscountPlans[i].Description);
				row.Cells.Add(FeeScheds.GetDescription(listDiscountPlans[i].FeeSchedNum,listFeeScheds));
				row.Cells.Add((adjType==null) ? "" : adjType.ItemName);
				row.Tag=listDiscountPlans[i];
				gridMain.Rows.Add(row);
				if(SelectedPlan!=null && listDiscountPlans[i].DiscountPlanNum==SelectedPlan.DiscountPlanNum) {
					selectedIdx=i;
				}
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(selectedIdx,true);
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			SelectedPlan=(DiscountPlan)gridMain.Rows[gridMain.GetSelectedIndex()].Tag;
			if(IsSelectionMode) {	
				DialogResult=DialogResult.OK;
				return;
			}
			FormDiscountPlanEdit FormDPE=new FormDiscountPlanEdit();
			FormDPE.DiscountPlanCur=SelectedPlan.Copy();
			if(FormDPE.ShowDialog()==DialogResult.OK) {
				FillGrid();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.InsPlanEdit)) {
				return;
			}
			DiscountPlan discountPlan=new DiscountPlan();
			discountPlan.IsNew=true;
			FormDiscountPlanEdit FormDPE=new FormDiscountPlanEdit();
			FormDPE.DiscountPlanCur=discountPlan;
			if(FormDPE.ShowDialog()==DialogResult.OK) {
				SelectedPlan=discountPlan;
				FillGrid();
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(IsSelectionMode) {
				if(gridMain.GetSelectedIndex()==-1) {
					MsgBox.Show(this,"Please select an entry.");
					return;
				}
				SelectedPlan=(DiscountPlan)gridMain.Rows[gridMain.GetSelectedIndex()].Tag;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}