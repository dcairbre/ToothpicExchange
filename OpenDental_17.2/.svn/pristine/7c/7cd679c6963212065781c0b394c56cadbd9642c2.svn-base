using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormClaimCustomTrackingUpdate:ODForm {
		/// <summary>List of claims selected from outstanding claim report</summary>
		private List<Claim> _listClaims;

		public FormClaimCustomTrackingUpdate(List<Claim> listClaims) {
			InitializeComponent();
			Lan.F(this);
			_listClaims=listClaims;
		}

		public FormClaimCustomTrackingUpdate(Claim claimCur) {
			InitializeComponent();
			Lan.F(this);
			_listClaims=new List<Claim>() {claimCur};
		}

		private void FormClaimCustomTrackingUpdate_Load(object sender,EventArgs e) {
			comboCustomTracking.Items.Clear();
			comboCustomTracking.Items.Add(Lan.g(this,"None"));
			comboCustomTracking.SelectedIndex=0;
			Def[] arrayCustomTrackingDefs=DefC.GetList(DefCat.ClaimCustomTracking);
			foreach(Def definition in arrayCustomTrackingDefs) { 
				comboCustomTracking.Items.Add(definition.ItemName);
			}
			FillComboErrorCode();
		}

		private void FillComboErrorCode() {
			Def[] arrayErrorCodeDefs = DefC.GetList(DefCat.ClaimErrorCode);
			comboErrorCode.Items.Clear();
			//Add "none" option.
			ODBoxItem<Def> comboBoxItem = new ODBoxItem<Def>(Lan.g(this,"None"),null);
			comboErrorCode.Items.Add(comboBoxItem);
			comboErrorCode.SelectedIndex=0;
			if(arrayErrorCodeDefs.Length==0) {
				//if the list is empty, then disable the comboBox.
				comboErrorCode.Enabled=false;
				return;
			}
			//Fill comboErrorCode.
			foreach(Def definition in arrayErrorCodeDefs) {
				//hooray for using new ODBoxItems!
				comboBoxItem = new ODBoxItem<Def>(definition.ItemName,definition);
				comboErrorCode.Items.Add(comboBoxItem);
			}
		}

		private void comboErrorCode_SelectionChangeCommitted(object sender,EventArgs e) {
			if((!comboErrorCode.Enabled) || ((ODBoxItem<Def>)comboErrorCode.SelectedItem).Tag==null) {
				textErrorDesc.Text="";
			}
			else {
				textErrorDesc.Text=((ODBoxItem<Def>)comboErrorCode.SelectedItem).Tag.ItemValue.ToString();
			}	
		}

		private void butUpdate_Click(object sender,EventArgs e) {
			long customTrackingDefNum;
			long errorCodeDefNum;
			if(PrefC.GetBool(PrefName.ClaimTrackingRequiresError) 
				&& ((ODBoxItem<Def>)comboErrorCode.SelectedItem).Tag == null 
				&& comboErrorCode.Enabled )
			{
				MsgBox.Show(this,"You must specify an error code."); //Do they have to specify an error code even if they set the status to None?
				return;
			}
			if(comboCustomTracking.SelectedIndex<1) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Setting the status to none will disable filtering in the Outstanding Claims Report."
						+"  Do you wish to set the status of this claim to none?")) {
					return;
				}
				customTrackingDefNum=0;
			}
			else {
				customTrackingDefNum=DefC.Long[(int)DefCat.ClaimCustomTracking][comboCustomTracking.SelectedIndex-1].DefNum;
			}
			Def errorCodeDef=((ODBoxItem<Def>)comboErrorCode.SelectedItem).Tag;
			errorCodeDefNum=errorCodeDef==null ? 0 : errorCodeDef.DefNum;
			for(int i=0;i<_listClaims.Count;i++) {
				_listClaims[i].CustomTracking=customTrackingDefNum;
				Claims.Update(_listClaims[i]);
				ClaimTracking statusEntry=new ClaimTracking();
				statusEntry.ClaimNum=_listClaims[i].ClaimNum;
				statusEntry.Note=textNotes.Text;
				statusEntry.TrackingDefNum=customTrackingDefNum;
				statusEntry.TrackingErrorDefNum=errorCodeDefNum;
				statusEntry.UserNum=Security.CurUser.UserNum;
				ClaimTrackings.Insert(statusEntry);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}