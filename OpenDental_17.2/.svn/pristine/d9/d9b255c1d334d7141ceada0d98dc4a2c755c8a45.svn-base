using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Linq;

namespace OpenDental {
	public partial class FormDiscountPlanEdit:ODForm {
		public DiscountPlan DiscountPlanCur;
		///<summary>FeeSched for the current DiscountPlan.  May be null if the DiscountPlan is new.</summary>
		private FeeSched _feeSchedCur;
		private List<Def> _listAdjTypeDefs;
		///<summary>IsSelectionMode is true if this window is opened with the intent of selecting a plan for a user</summary>
		public bool IsSelectionMode;

		public FormDiscountPlanEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDiscountPlanEdit_Load(object sender,EventArgs e) {
			textDescript.Text=DiscountPlanCur.Description;
			_feeSchedCur=FeeScheds.GetOne(DiscountPlanCur.FeeSchedNum,FeeSchedC.GetListShort());
			textFeeSched.Text=_feeSchedCur!=null ? _feeSchedCur.Description : "";
			_listAdjTypeDefs=DefC.GetDiscountPlanAdjTypes().ToList();
			for(int i=0;i<_listAdjTypeDefs.Count;i++) {
				comboBoxAdjType.Items.Add(_listAdjTypeDefs[i].ItemName);
				if(_listAdjTypeDefs[i].DefNum==DiscountPlanCur.DefNum) {
					comboBoxAdjType.SelectedIndex=i;
				}
			}
			if(!Security.IsAuthorized(Permissions.InsPlanEdit,true)) {//User may be able to get here if FormDiscountPlans is not in selection mode.
				textDescript.ReadOnly=true;
				comboBoxAdjType.Enabled=false;
				butFeeSched.Enabled=false;
				butOK.Enabled=false;
			}
			if(IsSelectionMode) {
				butDrop.Visible=true;
			}
		}

		private void butFeeSched_Click(object sender,EventArgs e) {
			FormFeeScheds FormFS=new FormFeeScheds();
			FormFS.SelectedFeeSchedNum=_feeSchedCur==null ? 0 : _feeSchedCur.FeeSchedNum;
			FormFS.ListFeeScheds=FeeSchedC.GetListShort();
			FormFS.IsSelectionMode=true;
			if(FormFS.ShowDialog()==DialogResult.OK) {
				_feeSchedCur=FeeScheds.GetOne(FormFS.SelectedFeeSchedNum,FormFS.ListFeeScheds);
				textFeeSched.Text=_feeSchedCur.Description;
			}
		}

		private void butDrop_Click(object sender,EventArgs e) {
			DiscountPlans.DropForPatient(FormOpenDental.CurPatNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDescript.Text.Trim()=="") {
				MsgBox.Show(this,"Please enter a description.");
				return;
			}
			if(_feeSchedCur==null) {
				MsgBox.Show(this,"Please select a fee schedule.");
				return;
			}
			if(comboBoxAdjType.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an adjustment type.\r\nYou may need to create discount plan adjustment types within definition setup.");
				return;
			}
			DiscountPlanCur.Description=textDescript.Text;
			DiscountPlanCur.FeeSchedNum=_feeSchedCur.FeeSchedNum;
			DiscountPlanCur.DefNum=_listAdjTypeDefs[comboBoxAdjType.SelectedIndex].DefNum;
			if(DiscountPlanCur.IsNew) {
				DiscountPlans.Insert(DiscountPlanCur);
			}
			else {
				DiscountPlans.Update(DiscountPlanCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}