using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormApptBreak:ODForm {
		
		public ApptBreakSelection FormApptBreakSelection;
		public ProcedureCode SelectedProcCode;

		public FormApptBreak() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptBreak_Load(object sender,EventArgs e) {
			Dictionary<string,Pref> dictPrefs=PrefC.GetDict();
			BrokenApptProcedure brokenApptProcs=(BrokenApptProcedure)PrefC.GetInt(PrefName.BrokenApptProcedure,dictPrefs);
			radioMissed.Enabled=brokenApptProcs.In(BrokenApptProcedure.Missed,BrokenApptProcedure.Both);
			radioCancelled.Enabled=brokenApptProcs.In(BrokenApptProcedure.Cancelled,BrokenApptProcedure.Both);
			if(radioMissed.Enabled && !radioCancelled.Enabled) {
				radioMissed.Checked=true;
			}
			else if(!radioMissed.Enabled && radioCancelled.Enabled) {
				radioMissed.Checked=true;
			}
		}

		private bool ValidateSelection() {
			if(!radioMissed.Checked && !radioCancelled.Checked) {
				MsgBox.Show(this,"Please select a broken procedure type.");
				return false;
			}
			return true;
		}
		
		private void butUnsched_Click(object sender,EventArgs e) {;
			if(!ValidateSelection()) {
				return;
			}
			FormApptBreakSelection=ApptBreakSelection.Unsched;
			DialogResult=DialogResult.OK;
		}

		private void butPinboard_Click(object sender,EventArgs e) {
			if(!ValidateSelection()) {
				return;
			}
			FormApptBreakSelection=ApptBreakSelection.Pinboard;
			DialogResult=DialogResult.OK;
		}

		private void butApptBook_Click(object sender,EventArgs e) {
			if(!ValidateSelection()) {
				return;
			}
			FormApptBreakSelection=ApptBreakSelection.ApptBook;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			FormApptBreakSelection=ApptBreakSelection.None;
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptBreak_FormClosing(object sender,FormClosingEventArgs e) {
			if(this.DialogResult!=DialogResult.OK) {
				return;
			}
			SelectedProcCode=radioMissed.Checked?ProcedureCodes.GetProcCode("D9986"):ProcedureCodes.GetProcCode("D9987");
		}
	}

	public enum ApptBreakSelection {
		///<summary>0 - Default.</summary>
		None,
		///<summary>1</summary>
		Unsched,
		///<summary>2</summary>
		Pinboard,
		///<summary>3</summary>
		ApptBook
	}

}