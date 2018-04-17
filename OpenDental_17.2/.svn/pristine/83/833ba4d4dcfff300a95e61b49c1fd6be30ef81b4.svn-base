using OpenDental;
using OpenDentBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CentralManager {
	public partial class FormCentralUserEdit:Form {
		public Userod _userCur;
		private List<AlertSub> _listAlertSubsOld;

		public FormCentralUserEdit(Userod user) {
			InitializeComponent();
			_userCur=user.Copy();
		}		

		private void FormCentralUserEdit_Load(object sender,EventArgs e) {
			checkIsHidden.Checked=_userCur.IsHidden;
			textUserName.Text=_userCur.UserName;
			for(int i=0;i<UserGroups.List.Length;i++){
				listUserGroup.Items.Add(UserGroups.List[i].Description);
				if(_userCur.UserGroupNum==UserGroups.List[i].UserGroupNum){
					listUserGroup.SelectedIndex=i;
				}
			}
			if(listUserGroup.SelectedIndex==-1){//never allowed to delete last group, so this won't fail
				listUserGroup.SelectedIndex=0;
			}
			if(_userCur.Password==""){
				butPassword.Text="Create Password";
			}
			_listAlertSubsOld=AlertSubs.GetAllForUser(Security.CurUser.UserNum);
			listAlertSubMulti.Items.Clear();
			string[] arrayAlertTypes=Enum.GetNames(typeof(AlertType));
			for(int i=0;i<arrayAlertTypes.Length;i++){
				listAlertSubMulti.Items.Add(arrayAlertTypes[i]);
				listAlertSubMulti.SetSelected(i,_listAlertSubsOld.Exists(x => x.Type==(AlertType)i));
			}
			if(_userCur.IsNew) {
				butUnlock.Visible=false;
			}
		}

		private void butPassword_Click(object sender,EventArgs e) {
			bool isCreate=false;
			if(_userCur.Password==null) {
				isCreate=true;
			}
			FormCentralUserPasswordEdit FormCPE=new FormCentralUserPasswordEdit(isCreate,_userCur.UserName);
			FormCPE.ShowDialog();
			if(FormCPE.DialogResult==DialogResult.Cancel){
				return;
			}
			_userCur.Password=FormCPE.HashedResult;
			if(_userCur.Password==""){
				butPassword.Text="Create Password";
			}
			else{
				butPassword.Text="Change Password";
			}
		}

		private void butUnlock_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Users can become locked when invalid credentials have been entered several times in a row.\r\n"
				+"Unlock this user so that more log in attempts can be made?")) 
			{
				return;
			}
			_userCur.DateTFail=DateTime.MinValue;
			_userCur.FailedAttempts=0;
			try {
				Userods.Update(_userCur);//This will also commit other things about the user if they've changed.  Oh well.
				MsgBox.Show(this,"User has been unlocked.");
			}
			catch(Exception) {
				MsgBox.Show(this,"There was a problem unlocking this user.  Please call support or wait the allotted lock time.");
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textUserName.Text==""){
				MessageBox.Show(this,"Please enter a username.");
				return;
			}
			List<AlertSub> listAlertSubsCur=new List<AlertSub>();
			foreach(int index in listAlertSubMulti.SelectedIndices) {
				AlertSub alertSub=new AlertSub();
				alertSub.ClinicNum=0;
				alertSub.UserNum=Security.CurUser.UserNum;
				alertSub.Type=(AlertType)index;
				listAlertSubsCur.Add(alertSub);
			}
			AlertSubs.Sync(listAlertSubsCur,_listAlertSubsOld);
			_userCur.IsHidden=checkIsHidden.Checked;
			_userCur.UserName=textUserName.Text;
			if(_userCur.UserNum==Security.CurUser.UserNum) {
				Security.CurUser.UserName=textUserName.Text;
				//They changed their logged in user's information.  Update for when they sync then attempt to connect to remote DB.
			}
			_userCur.UserGroupNum=UserGroups.List[listUserGroup.SelectedIndex].UserGroupNum;
			_userCur.EmployeeNum=0;
			_userCur.ProvNum=0;
			_userCur.ClinicNum=0;
			_userCur.ClinicIsRestricted=false;
			try{
				if(_userCur.IsNew){
					long userNum=Userods.Insert(_userCur);
					_userCur.UserNumCEMT=userNum;
					Userods.Update(_userCur);//Doing this instead of making a new version of insert...
				}
				else{
					Userods.Update(_userCur);
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
