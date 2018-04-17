using OpenDentBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormBugEdit:Form {
		public Bug BugCur;
		public bool IsNew;

		public FormBugEdit() {
			InitializeComponent();
		}

		private void FormBugEdit_Load(object sender,EventArgs e) {
			textBugId.Text=BugCur.BugId.ToString();
			textCreationDate.Text=BugCur.CreationDate.ToString();
			comboStatus.Text=BugCur.Status_.ToString();
			comboPriority.Text=BugCur.PriorityLevel.ToString();
			textVersionsFound.Text=BugCur.VersionsFound;
			textVersionsFixed.Text=BugCur.VersionsFixed;
			textDescription.Text=BugCur.Description;
			textLongDesc.Text=BugCur.LongDesc;
			textPrivateDesc.Text=BugCur.PrivateDesc;
			textDiscussion.Text=BugCur.Discussion;
			textSubmitter.Text=Bugs.GetSubmitterName(BugCur.Submitter);
		}

		private void butCopyDown_Click(object sender,EventArgs e) {
			textVersionsFixed.Text=textVersionsFound.Text;
		}

		private void butLast1found_Click(object sender,EventArgs e) {
			textVersionsFound.Text=VersionReleases.GetLastReleases(1);
		}

		private void butLast2found_Click(object sender,EventArgs e) {
			textVersionsFound.Text=VersionReleases.GetLastReleases(2);
		}

		private void butLast3found_Click(object sender,EventArgs e) {
			textVersionsFound.Text=VersionReleases.GetLastReleases(3);
		}

		private void butLast1_Click(object sender,EventArgs e) {
			textVersionsFixed.Text=VersionReleases.GetLastReleases(1);
		}

		private void butLast2_Click(object sender,EventArgs e) {
			textVersionsFixed.Text=VersionReleases.GetLastReleases(2);
		}

		private void butLast3_Click(object sender,EventArgs e) {
			textVersionsFixed.Text=VersionReleases.GetLastReleases(3);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			Bugs.Delete(BugCur.BugId);
			BugCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butLeaveStatus_Click(object sender,EventArgs e) {
			BugCur.Status_=(BugStatus)Enum.Parse(typeof(BugStatus),comboStatus.Text);
			SaveToDb();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(BugCur.Submitter==0) {
				MessageBox.Show("A valid submitter wasn't picked.  Make sure the computer being used is associated to a buguser.");
				return;
			}
			if(textVersionsFixed.Text!=""){
				BugCur.Status_=BugStatus.Fixed;
			}
			else if(comboStatus.SelectedIndex==0) {//none
				BugCur.Status_=BugStatus.Accepted;
			}
			else{
				BugCur.Status_=(BugStatus)Enum.Parse(typeof(BugStatus),comboStatus.Text);
			}
			SaveToDb();
			DialogResult=DialogResult.OK;
		}

		private void SaveToDb(){
			//BugId
			//CreationDate
			BugCur.Type_=BugType.Bug;//user can't change
			BugCur.PriorityLevel=PIn.Int(comboPriority.Text);
			BugCur.VersionsFound=textVersionsFound.Text;
			BugCur.VersionsFixed=textVersionsFixed.Text;
			BugCur.Description=textDescription.Text;
			BugCur.LongDesc=textLongDesc.Text;
			BugCur.PrivateDesc=textPrivateDesc.Text;
			BugCur.Discussion=textDiscussion.Text;
			//Submitter
			if(IsNew){
				Bugs.Insert(BugCur);
			}
			else{
				Bugs.Update(BugCur);
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

	}
}