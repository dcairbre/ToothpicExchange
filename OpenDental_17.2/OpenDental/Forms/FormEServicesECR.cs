using CodeBase;
using Microsoft.Win32;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using System.Data;
using System.Linq;
using System.IO;
using WebServiceSerializer;
using OpenDentBusiness.WebServiceMainHQ;
using OpenDentBusiness.WebTypes.WebSched.TimeSlot;

namespace OpenDental {
	public partial class FormEServicesSetup {
		//==================== eConfirm & eRemind Variables ====================
		private List<Def> _listDefsApptStatus;
		private List<Clinic> _ecListClinics;
		private Clinic _ecClinicCur;
		///<summary>When using clinics, this is the index of the clinic rules to use.</summary>
		///<summary>not acutal idx, actually just ClinicNum.</summary>
		private long _clinicRuleIdx;
		///<summary>Key = ClinicNum, 0=Practice/Defaults. Value = Rules defined for that clinic. If a clinic uses defaults, its respective list of rules will be empty.</summary>
		private Dictionary<long,List<ApptReminderRule>> _dictClinicRules;
		
		private bool IsTabValidECR() {
			if(new[] { comboStatusEAccepted.SelectedIndex,comboStatusESent.SelectedIndex,comboStatusEDeclined.SelectedIndex,comboStatusEFailed.SelectedIndex }.Where(x => x!=-1).GroupBy(x => x).Any(x => x.Count()>1)) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"All eConfirmation appointment statuses should be different. Continue anyway?")) {
					return false;
				}
			}
			return true;
		}

		private void FillTabECR() {			
			if(PrefC.GetBool(PrefName.ApptConfirmAutoEnabled) && !IsEServiceActive(eServiceCode.ConfirmationRequest)) { //HQ says confirms is disabled but local says it's enabled. Turn it off to sync with HQ.
				Prefs.UpdateBool(PrefName.ApptConfirmAutoEnabled,false);
				SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Automated appointment eConfirmations automatically deactivated by HQ.  Local pref was out of sync with HQ.");
				Signalods.SetInvalid(InvalidType.Prefs);
				MsgBox.Show(this,"Automated appointment eConfirmations has been disabled.  Please use the Signup tab to activate this service with support.");
			}
			FillECRActivationButtons();
			checkEnableNoClinic.Checked=PrefC.GetBool(PrefName.ApptConfirmEnableForClinicZero);
			if(PrefC.HasClinicsEnabled) {//CLINICS
				checkUseDefaultsEC.Visible=true;
				checkUseDefaultsEC.Enabled=false;//when loading form we will be viewing defaults.
				checkIsConfirmEnabled.Visible=true;
				groupAutomationStatuses.Text=Lan.g(this,"eConfirmation Settings")+" - "+Lan.g(this,"Affects all Clinics");
			}
			else {//NO CLINICS
				checkUseDefaultsEC.Visible=false;
				checkUseDefaultsEC.Enabled=false;
				checkUseDefaultsEC.Checked=false;
				checkIsConfirmEnabled.Visible=false;
				checkEnableNoClinic.Visible=false;
				groupAutomationStatuses.Text=Lan.g(this,"eConfirmation Settings");
			}
			setListClinicsAndDictRulesHelper();
			comboClinicEConfirm.SelectedIndex=0;
			_listDefsApptStatus=DefC.Short[(int)DefCat.ApptConfirmed].ToList();
			comboStatusESent.Items.Clear();
			comboStatusEAccepted.Items.Clear();
			comboStatusEDeclined.Items.Clear();
			comboStatusEFailed.Items.Clear();
			_listDefsApptStatus.ForEach(x => comboStatusESent.Items.Add(x.ItemName));
			_listDefsApptStatus.ForEach(x => comboStatusEAccepted.Items.Add(x.ItemName));
			_listDefsApptStatus.ForEach(x => comboStatusEDeclined.Items.Add(x.ItemName));
			_listDefsApptStatus.ForEach(x => comboStatusEFailed.Items.Add(x.ItemName));
			long prefApptEConfirmStatusSent=PrefC.GetLong(PrefName.ApptEConfirmStatusSent);
			long prefApptEConfirmStatusAccepted=PrefC.GetLong(PrefName.ApptEConfirmStatusAccepted);
			long prefApptEConfirmStatusDeclined=PrefC.GetLong(PrefName.ApptEConfirmStatusDeclined);
			long prefApptEConfirmStatusSendFailed=PrefC.GetLong(PrefName.ApptEConfirmStatusSendFailed);
			//SENT
			if(prefApptEConfirmStatusSent>0) {
				//Selects combo box option if it exists, if it doesn't it sets the text of the combo box to the hidden one.
				comboStatusESent.IndexSelectOrSetText(_listDefsApptStatus.FindIndex(x => x.DefNum==prefApptEConfirmStatusSent),() => {
					return DefC.GetName(DefCat.ApptConfirmed,prefApptEConfirmStatusSent)+" (hidden)";
				});
			}
			else {
				comboStatusESent.SelectedIndex=0;
			}
			//CONFIRMED
			if(prefApptEConfirmStatusAccepted>0) {
				//Selects combo box option if it exists, if it doesn't it sets the text of the combo box to the hidden one.
				comboStatusEAccepted.IndexSelectOrSetText(_listDefsApptStatus.FindIndex(x => x.DefNum==prefApptEConfirmStatusAccepted),() => {
					return DefC.GetName(DefCat.ApptConfirmed,prefApptEConfirmStatusAccepted)+" (hidden)";
				});
			}
			else {
				comboStatusEAccepted.SelectedIndex=0;
			}
			//NOT CONFIRMED
			if(prefApptEConfirmStatusDeclined>0) {
				//Selects combo box option if it exists, if it doesn't it sets the text of the combo box to the hidden one.
				comboStatusEDeclined.IndexSelectOrSetText(_listDefsApptStatus.FindIndex(x => x.DefNum==prefApptEConfirmStatusDeclined),() => {
					return DefC.GetName(DefCat.ApptConfirmed,prefApptEConfirmStatusDeclined)+" (hidden)";
				});
			}
			else {
				comboStatusEDeclined.SelectedIndex=0;
			}
			//Failed
			if(prefApptEConfirmStatusSendFailed>0) {
				//Selects combo box option if it exists, if it doesn't it sets the text of the combo box to the hidden one.
				comboStatusEFailed.IndexSelectOrSetText(_listDefsApptStatus.FindIndex(x => x.DefNum==prefApptEConfirmStatusSendFailed),() => {
					return DefC.GetName(DefCat.ApptConfirmed,prefApptEConfirmStatusSendFailed)+" (hidden)";
				});
			}
			else {
				comboStatusEFailed.SelectedIndex=0;
			}
			FillConfStatusesGrid();
			FillRemindConfirmData();
		}

		private void SaveTabECR() {
			if(comboStatusESent.SelectedIndex!=-1) {
				Prefs.UpdateLong(PrefName.ApptEConfirmStatusSent,_listDefsApptStatus[comboStatusESent.SelectedIndex].DefNum);
			}
			if(comboStatusEAccepted.SelectedIndex!=-1) {
				Prefs.UpdateLong(PrefName.ApptEConfirmStatusAccepted,_listDefsApptStatus[comboStatusEAccepted.SelectedIndex].DefNum);
			}
			if(comboStatusEDeclined.SelectedIndex!=-1) {
				Prefs.UpdateLong(PrefName.ApptEConfirmStatusDeclined,_listDefsApptStatus[comboStatusEDeclined.SelectedIndex].DefNum);
			}
			if(comboStatusEFailed.SelectedIndex!=-1) {
				Prefs.UpdateLong(PrefName.ApptEConfirmStatusSendFailed,_listDefsApptStatus[comboStatusEFailed.SelectedIndex].DefNum);
			}
			Prefs.UpdateBool(PrefName.ApptConfirmEnableForClinicZero,checkEnableNoClinic.Checked);
			ApptReminderRules.SyncByClinic(_dictClinicRules[_ecClinicCur.ClinicNum],_ecClinicCur.ClinicNum);
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum!=0) {
				_ecClinicCur.IsConfirmEnabled=checkIsConfirmEnabled.Checked;
				Clinics.Update(_ecClinicCur);
			}
		}

		private void AuthorizeTabECR(bool allowEdit) {
			groupAutomationStatuses.Enabled=allowEdit;
			butActivateReminder.Enabled=allowEdit;
			butActivateConfirm.Enabled=allowEdit;
			checkIsConfirmEnabled.Enabled=allowEdit;
			checkUseDefaultsEC.Enabled=allowEdit;
		}

		///<summary>Fills in memory Rules dictionary and clinics list based. This is very different from AppointmentReminderRules.GetRuleAndClinics.</summary>
		private void setListClinicsAndDictRulesHelper() {
			if(PrefC.HasClinicsEnabled) {//CLINICS
				_ecListClinics=new List<Clinic>() { new Clinic() { Description="Defaults",Abbr="Defaults" } };
				_ecListClinics.AddRange(Clinics.GetForUserod(Security.CurUser));
			}
			else {//NO CLINICS
				_ecListClinics=new List<Clinic>() { new Clinic() { Description="Practice",Abbr="Practice" } };
			}
			List<ApptReminderRule> listRulesTemp = ApptReminderRules.GetAll();
			_dictClinicRules=_ecListClinics.Select(x => x.ClinicNum).ToDictionary(x => x,x => listRulesTemp.FindAll(y => y.ClinicNum==x));
			int idx = comboClinicEConfirm.SelectedIndex>0 ? comboClinicEConfirm.SelectedIndex : 0;
			comboClinicEConfirm.BeginUpdate();
			comboClinicEConfirm.Items.Clear();
			_ecListClinics.ForEach(x => comboClinicEConfirm.Items.Add(x.Abbr));//combo clinics may not be visible.
			if(idx>-1&&idx<comboClinicEConfirm.Items.Count) {
				comboClinicEConfirm.SelectedIndex=idx;
			}
			comboClinicEConfirm.EndUpdate();
		}

		private void FillConfStatusesGrid() {
			List<long> listDontSendConf=PrefC.GetString(PrefName.ApptConfirmExcludeESend).Split(',').Select(x => PIn.Long(x)).ToList();
			List<long> listDontChange=PrefC.GetString(PrefName.ApptConfirmExcludeEConfirm).Split(',').Select(x => PIn.Long(x)).ToList();
			List<long> listDontSendRem=PrefC.GetString(PrefName.ApptConfirmExcludeERemind).Split(',').Select(x => PIn.Long(x)).ToList();
			gridConfStatuses.BeginUpdate();
			gridConfStatuses.Columns.Clear();
			gridConfStatuses.Columns.Add(new ODGridColumn(Lan.g(this,"Status"),100));
			gridConfStatuses.Columns.Add(new ODGridColumn(Lan.g(this,"Don't Send"),70,HorizontalAlignment.Center));
			gridConfStatuses.Columns.Add(new ODGridColumn(Lan.g(this,"Don't Change"),70,HorizontalAlignment.Center));
			gridConfStatuses.Rows.Clear();
			foreach(Def defConfStatus in _listDefsApptStatus) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(defConfStatus.ItemName);
				row.Cells.Add(listDontSendConf.Contains(defConfStatus.DefNum) ? "X" : "");
				row.Cells.Add(listDontChange.Contains(defConfStatus.DefNum) ? "X" : "");
				row.Tag=defConfStatus;
				gridConfStatuses.Rows.Add(row);
			}
			gridConfStatuses.EndUpdate();
		}

		private void FillRemindConfirmData() {
			#region Fill Reminders grid.
			gridRemindersMain.BeginUpdate();
			gridRemindersMain.Columns.Clear();
			gridRemindersMain.Columns.Add(new ODGridColumn(Lan.g(this,"Type"),150) { TextAlign=HorizontalAlignment.Center });
			gridRemindersMain.Columns.Add(new ODGridColumn(Lan.g(this,"Lead Time"),250));
			//gridRemindersMain.Columns.Add(new ODGridColumn("Send\r\nAll",50) { TextAlign=HorizontalAlignment.Center });
			gridRemindersMain.Columns.Add(new ODGridColumn(Lan.g(this,"Send Order"),100));
			gridRemindersMain.NoteSpanStart=1;
			gridRemindersMain.NoteSpanStop=2;
			gridRemindersMain.Rows.Clear();
			ODGridRow row;
			List<ApptReminderRule> listTemp = new List<ApptReminderRule>();
			if(_ecClinicCur==null||_ecClinicCur.IsConfirmDefault) {//Use defaults
				_clinicRuleIdx=0;
			}
			else {
				_clinicRuleIdx=_ecClinicCur.ClinicNum;
			}
			listTemp=_dictClinicRules[_clinicRuleIdx];
			foreach(ApptReminderRule apptRule in listTemp) {
				string sendOrderText = string.Join(", ",apptRule.SendOrder.Split(',').Select(x => Enum.Parse(typeof(CommType),x).ToString()));
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,apptRule.TypeCur.GetDescription())
					+(_ecClinicCur.IsConfirmDefault ? "\r\n("+Lan.g(this,"Defaults")+")" : ""));
				if(apptRule.TSPrior<=TimeSpan.Zero) {
					row.Cells.Add(Lan.g(this,"Disabled"));
				}
				else {
					row.Cells.Add(apptRule.TSPrior.ToStringDH());
				}
				row.Cells.Add(apptRule.IsSendAll ? Lan.g(this,"All") : sendOrderText);
				row.Note=Lan.g(this,"SMS Template")+":\r\n"+apptRule.TemplateSMS+"\r\n\r\n"+Lan.g(this,"Email Subject Template")+":\r\n"+apptRule.TemplateEmailSubject+"\r\n"+Lan.g(this,"Email Template")+":\r\n"+apptRule.TemplateEmail;
				row.Tag=apptRule;
				if(gridRemindersMain.Rows.Count%2==1) {
					row.ColorBackG=Color.FromArgb(240,240,240);//light gray every other row.
				}
				gridRemindersMain.Rows.Add(row);
			}
			gridRemindersMain.EndUpdate();
			#endregion
			#region Set add buttons
			bool allowEdit=Security.IsAuthorized(Permissions.EServicesSetup,true);
			if(comboClinicEConfirm.SelectedIndex>0) {//REAL CLINIC
				checkUseDefaultsEC.Visible=true;
				checkUseDefaultsEC.Enabled=allowEdit;
				checkIsConfirmEnabled.Enabled=allowEdit;//because we either cannot see it, or we are editing defaults.
				checkIsConfirmEnabled.Visible=true;
			}
			else {//CLINIC DEFAULTS/PRACTICE
				checkUseDefaultsEC.Visible=false;
				checkUseDefaultsEC.Enabled=false;
				checkIsConfirmEnabled.Enabled=false;//because we either cannot see it, or we are editing defaults.
				checkIsConfirmEnabled.Visible=false;
			}
			checkUseDefaultsEC.Checked=(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0&&_ecClinicCur.IsConfirmDefault);
			if(_dictClinicRules[_clinicRuleIdx].Count(x => x.TypeCur==ApptReminderType.ReminderSameDay)==0) {
				butAddReminderSameDay.Enabled=allowEdit;
			}
			else {
				butAddReminderSameDay.Enabled=false;
			}
			if(_dictClinicRules[_clinicRuleIdx].Count(x => x.TypeCur==ApptReminderType.ReminderFutureDay)==0) {
				butAddReminderFutureDay.Enabled=allowEdit;
			}
			else {
				butAddReminderFutureDay.Enabled=false;
			}
			if(_dictClinicRules[_clinicRuleIdx].Count(x => x.TypeCur==ApptReminderType.ConfirmationFutureDay)==0) {
				butAddConfirmation.Enabled=allowEdit;
			}
			else {
				butAddConfirmation.Enabled=false;
			}
			#endregion
		}

		private void FillECRActivationButtons() {
			//Reminder Activation Status
			if(PrefC.GetBool(PrefName.ApptRemindAutoEnabled)) {
				textStatusReminders.Text=Lan.g(this,"eReminders")+" : "+Lan.g(this,"Active");
				textStatusReminders.BackColor=Color.FromArgb(236,255,236);//light green
				textStatusReminders.ForeColor=Color.Black;//instead of disabled grey
				butActivateReminder.Text=Lan.g(this,"Deactivate eReminders");
			}
			else {
				textStatusReminders.Text=Lan.g(this,"eReminders")+" : "+Lan.g(this,"Inactive");
				textStatusReminders.BackColor=Color.FromArgb(254,235,233);//light red;
				textStatusReminders.ForeColor=Color.Black;//instead of disabled grey
				butActivateReminder.Text=Lan.g(this,"Activate eReminders");
			}
			//Confirmation Activation Status
			if(PrefC.GetBool(PrefName.ApptConfirmAutoEnabled)) {
				textStatusConfirmations.Text=Lan.g(this,"eConfirmations")+" : "+Lan.g(this,"Active");
				textStatusConfirmations.BackColor=Color.FromArgb(236,255,236);//light green
				textStatusConfirmations.ForeColor=Color.Black;//instead of disabled grey
				butActivateConfirm.Text=Lan.g(this,"Deactivate eConfirmations");
			}
			else {
				textStatusConfirmations.Text=Lan.g(this,"eConfirmations")+" : "+Lan.g(this,"Inactive");
				textStatusConfirmations.BackColor=Color.FromArgb(254,235,233);//light red;
				textStatusConfirmations.ForeColor=Color.Black;//instead of disabled grey
				butActivateConfirm.Text=Lan.g(this,"Activate eConfirmations");
			}
		}

		private void gridConfStatuses_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Def defConfirmation=(Def)gridConfStatuses.Rows[e.Row].Tag;
			FormDefEdit FormDE=new FormDefEdit(defConfirmation,_listDefsApptStatus,new DefCatOptions(DefCat.ApptConfirmed,enableColor:true,enableValue:true));
			FormDE.ShowDialog();
			if(FormDE.DialogResult==DialogResult.OK) {
				Defs.RefreshCache();
				_listDefsApptStatus=DefC.Short[(int)DefCat.ApptConfirmed].ToList();
				FillConfStatusesGrid();
			}
		}

		private void gridRemindersMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(!Security.IsAuthorized(Permissions.EServicesSetup)) {
				return;
			}
			if(e.Row<0||!(gridRemindersMain.Rows[e.Row].Tag is ApptReminderRule)) {
				return;//we did not click on a valid row.
			}
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0&&_ecClinicCur.IsConfirmDefault&&!switchFromDefaults()) {
				return;
			}
			ApptReminderRule arr = (ApptReminderRule)gridRemindersMain.Rows[e.Row].Tag;
			FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(arr);
			FormARRE.ShowDialog();
			if(FormARRE.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormARRE.ApptReminderRuleCur==null) {//Delete
				_dictClinicRules[_clinicRuleIdx].RemoveAt(e.Row);
			}
			else if(FormARRE.ApptReminderRuleCur.IsNew) {//Update
				_dictClinicRules[_clinicRuleIdx].Add(FormARRE.ApptReminderRuleCur);//should never happen from the double click event
			}
			else {//Insert
				_dictClinicRules[_clinicRuleIdx][e.Row]=FormARRE.ApptReminderRuleCur;
			}
			FillRemindConfirmData();
		}
		
		private void butAddReminderSameDay_Click(object sender,EventArgs e) {
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0&&!_ecClinicCur.IsConfirmDefault) {
				if(!switchFromDefaults()) {
					return;
				}
				if(_dictClinicRules[_ecClinicCur.ClinicNum].Count(x => x.TypeCur==ApptReminderType.ReminderSameDay&&x.TSPrior.TotalDays<1)>0) {
					return;//Switched to defaults but reminder already exist.
				}
			}
			ApptReminderRule arr = ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ReminderSameDay,_ecClinicCur.ClinicNum);
			FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(arr);
			FormARRE.ShowDialog();
			if(FormARRE.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormARRE.ApptReminderRuleCur==null||FormARRE.ApptReminderRuleCur.IsNew) {//Delete or Update
																						//Nothing to update, this was a new rule.
			}
			else {//Insert
				_dictClinicRules[_clinicRuleIdx].Add(FormARRE.ApptReminderRuleCur);
			}
			FillRemindConfirmData();
		}

		private void butAddReminderFutureDay_Click(object sender,EventArgs e) {
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0&&!_ecClinicCur.IsConfirmDefault) {
				if(!switchFromDefaults()) {
					return;
				}
				if(_dictClinicRules[_ecClinicCur.ClinicNum].Count(x => x.TypeCur==ApptReminderType.ReminderFutureDay&&x.TSPrior.TotalDays>=1)>0) {
					return;//Switched to defaults but reminder already exist.
				}
			}
			ApptReminderRule arr = ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ReminderFutureDay,_ecClinicCur.ClinicNum);
			FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(arr);
			FormARRE.ShowDialog();
			if(FormARRE.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormARRE.ApptReminderRuleCur==null||FormARRE.ApptReminderRuleCur.IsNew) {//Delete or Update
																						//Nothing to update, this was a new rule.
			}
			else {//Insert
				_dictClinicRules[_clinicRuleIdx].Add(FormARRE.ApptReminderRuleCur);
			}
			FillRemindConfirmData();
		}

		private void butAddConfirmation_Click(object sender,EventArgs e) {
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0&&_ecClinicCur.IsConfirmDefault) {
				if(!switchFromDefaults()) {
					return;
				}
				if(_dictClinicRules[_ecClinicCur.ClinicNum].Count(x => x.TypeCur==ApptReminderType.ConfirmationFutureDay)>0) {
					return;//Switched to defaults but confirmation already existed.
				}
			}
			ApptReminderRule arr = ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ConfirmationFutureDay,_ecClinicCur.ClinicNum);
			FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(arr);
			FormARRE.ShowDialog();
			if(FormARRE.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormARRE.ApptReminderRuleCur==null||FormARRE.ApptReminderRuleCur.IsNew) {
				//Delete or Update
				//Nothing to delete or update, this was a new rule.
			}
			else {//Insert
				_dictClinicRules[_clinicRuleIdx].Add(FormARRE.ApptReminderRuleCur);
			}
			FillRemindConfirmData();
		}

		private void comboClinicEConfirm_SelectedIndexChanged(object sender,EventArgs e) {
			if(_ecListClinics.Count==0||_dictClinicRules.Count==0) {
				return;//form load;
			}
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum>0) {//do not update this clinic-pref if we are editing defaults.
				_ecClinicCur.IsConfirmEnabled=checkIsConfirmEnabled.Checked;
				Clinics.Update(_ecClinicCur);
				Signalods.SetInvalid(InvalidType.Providers);
				//no need to save changes here because all Appointment reminder rules are saved to the DB from the edit window.
			}
			if(_ecClinicCur!=null) {
				ApptReminderRules.SyncByClinic(_dictClinicRules[_ecClinicCur.ClinicNum],_ecClinicCur.ClinicNum);
			}
			if(comboClinicEConfirm.SelectedIndex>-1&&comboClinicEConfirm.SelectedIndex<_ecListClinics.Count) {
				_ecClinicCur=_ecListClinics[comboClinicEConfirm.SelectedIndex];
			}
			checkUseDefaultsEC.Checked=_ecClinicCur!=null&&_ecClinicCur.IsConfirmDefault;
			checkIsConfirmEnabled.Checked=_ecClinicCur!=null&&_ecClinicCur.IsConfirmEnabled;
			FillRemindConfirmData();
		}

		///<summary>Switches the currently selected clinic over to using defaults. Also prompts user before continuing. 
		///Returns false if user cancelled or if there is no need to have switched to defaults.</summary>
		private bool switchFromDefaults() {
			if(_ecClinicCur==null||_ecClinicCur.ClinicNum==0) {
				return false;//somehow editing default clinic anyways, no need to switch.
			}
			//if(!MsgBox.Show(this,true,"Would you like to make a copy of the defaults for this clinic and continue editing the copy?")) {
			//	return false;
			//}
			_dictClinicRules[_ecClinicCur.ClinicNum]=_dictClinicRules[0].Select(x => x.Copy()).ToList();
			_dictClinicRules[_ecClinicCur.ClinicNum].ForEach(x => x.ClinicNum=_ecClinicCur.ClinicNum);
			_ecClinicCur.IsConfirmDefault=false;
			_ecListClinics[_ecListClinics.FindIndex(x => x.ClinicNum==_ecClinicCur.ClinicNum)].IsConfirmDefault=false;
			//Clinics.Update(_clinicCur);
			//Signalods.SetInvalid(InvalidType.Providers);//for clinics
			FillRemindConfirmData();
			return true;
		}

		///<summary>Switches the currently selected clinic over to using defaults. Also prompts user before continuing. 
		///Returns false if user cancelled or if there is no need to have switched to defaults.</summary>
		private bool switchToDefaults() {
			if(_ecClinicCur==null||_ecClinicCur.ClinicNum==0) {
				return false;//somehow editing default clinic anyways, no need to switch.
			}
			if(_dictClinicRules[_ecClinicCur.ClinicNum].Count>0&&!MsgBox.Show(this,true,"Delete custom rules for this clinic and switch to using defaults? This cannot be undone.")) {
				checkUseDefaultsEC.Checked=false;//undo checking of box.
				return false;
			}
			_ecClinicCur.IsConfirmDefault=true;
			_dictClinicRules[_ecClinicCur.ClinicNum]=new List<ApptReminderRule>();
			FillRemindConfirmData();
			return true;
		}

		private void checkIsConfirmEnabled_CheckedChanged(object sender,EventArgs e) {
			FillRemindConfirmData();
		}

		private void checkUseDefaultsEC_CheckedChanged(object sender,EventArgs e) {
			//TURNING DEFAULTS OFF
			if(!checkUseDefaultsEC.Checked&&_ecClinicCur.IsConfirmDefault&&_ecClinicCur.ClinicNum>0) {//Default switched off
				_ecClinicCur.IsConfirmDefault=false;
				_ecListClinics[comboClinicEConfirm.SelectedIndex].IsConfirmDefault=false;
				FillRemindConfirmData();
				return;
			}
			//TURNING DEFAULTS ON
			else if(checkUseDefaultsEC.Checked&&!_ecClinicCur.IsConfirmDefault&&_ecClinicCur.ClinicNum>0) {//Default switched on
				switchToDefaults();
				return;
			}
			//Silently do nothing because we just "changed" the checkbox to the state of the current clinic. 
			//I.e. When switching from clinic 1 to clinic 2, if 1 uses defaults and 2 does not, then this allows the new clinic to be loaded without updating the DB.
		}

		private void butActivateConfirm_Click(object sender,EventArgs e) {
			if(!IsEServiceActive(eServiceCode.ConfirmationRequest)) { //Not yet activated with HQ.
				MsgBox.Show(this,"You must first signup for eConfirmations via the Signup tab before activating eConfirmations.");
				return;
			}
			bool isApptConfirmAutoEnabled = PrefC.GetBool(PrefName.ApptConfirmAutoEnabled);
			isApptConfirmAutoEnabled=!isApptConfirmAutoEnabled;
			Prefs.UpdateBool(PrefName.ApptConfirmAutoEnabled,isApptConfirmAutoEnabled);
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Automated appointment eConfirmations "+(isApptConfirmAutoEnabled ? "activated" : "deactivated")+".");
			Prefs.RefreshCache();
			Signalods.SetInvalid(InvalidType.Prefs);
			FillECRActivationButtons();
		}

		private void butActivateReminder_Click(object sender,EventArgs e) {
			bool isApptRemindAutoEnabled = PrefC.GetBool(PrefName.ApptRemindAutoEnabled);
			isApptRemindAutoEnabled=!isApptRemindAutoEnabled;
			Prefs.UpdateBool(PrefName.ApptRemindAutoEnabled,isApptRemindAutoEnabled);
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Automated appointment eReminders "+(isApptRemindAutoEnabled ? "activated" : "deactivated")+".");
			Prefs.RefreshCache();
			Signalods.SetInvalid(InvalidType.Prefs);
			FillECRActivationButtons();
		}

		private void butWizardConfirm_Click(object sender,EventArgs e) {			
			//======================SAVE POTENTIAL CHANGES BEFORE STARTING======================
			Prefs.UpdateBool(PrefName.ApptConfirmEnableForClinicZero,checkEnableNoClinic.Checked);
			ApptReminderRules.SyncByClinic(_dictClinicRules[_ecClinicCur.ClinicNum],_ecClinicCur.ClinicNum);
			if(_ecClinicCur!=null&&_ecClinicCur.ClinicNum!=0) {
				_ecClinicCur.IsConfirmEnabled=checkIsConfirmEnabled.Checked;
				Clinics.Update(_ecClinicCur);
				Signalods.SetInvalid(InvalidType.Providers);//Includes Clinics.
			}
			//======================GATHER WIZARD DATA======================
			List<ApptReminderRule> listRulesAllTemp = ApptReminderRules.GetAll();
			List<Clinic> listClinicsAllTemp = Clinics.ListLong;
			ApptReminderRule ruleRemindDefault = listRulesAllTemp.FirstOrDefault(x => x.ClinicNum==0 && x.TypeCur==ApptReminderType.ReminderSameDay);
			ApptReminderRule ruleConfirmDefault = listRulesAllTemp.FirstOrDefault(x => x.ClinicNum==0 && x.TypeCur==ApptReminderType.ConfirmationFutureDay);
			//======================HELPER FUNCTIONS======================
			Func<string,string,bool> promptUserB = (heading,text) => {
				bool b = false;
				switch(MessageBox.Show(Lan.g(this,text),Lan.g(this,heading),MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						b=true;
						break;
					case DialogResult.Cancel:
						b=false;
						throw new ApplicationException(Lan.g(this,"Setup wizard cancelled. Run again at anytime to continue."));
					case DialogResult.No:
					default:
						b=false;
						break;
				}
				return b;
			};
			Action refreshLocalData = () => {
				Prefs.RefreshCache();
				Clinics.RefreshCache();
				listClinicsAllTemp = Clinics.ListLong;
				listRulesAllTemp = ApptReminderRules.GetAll();
				ruleRemindDefault = listRulesAllTemp.FirstOrDefault(x => x.ClinicNum==0 && x.TypeCur==ApptReminderType.ReminderSameDay);
				ruleConfirmDefault = listRulesAllTemp.FirstOrDefault(x => x.ClinicNum==0 && x.TypeCur==ApptReminderType.ConfirmationFutureDay);
			};
			Action RefreshPublicData = () => {
				refreshLocalData();
				Signalods.SetInvalid(InvalidType.Prefs,InvalidType.Providers);
				FillTabECR();
			};
			bool isReminderStarted = false;
			bool isConfirmationStarted = false;
			try {
				//======================INTEGRATED TEXTING======================
				if(!SmsPhones.IsIntegratedTextingEnabled()
					&&promptUserB("Enable Integrated Texting first?","Before we get started activating your automated messaging, we noticed that you have not enabled texting, which has fees associated with it.  Automated messaging is more effective if you include texting, but you can skip it if you prefer.\r\nWould you like to setup and enable Integrated Texting now?")) {
					tabControl.SelectedTab=tabTexting;
					MsgBox.Show(this,"Setup wizard suspended. Return to the Automated eConfirmation and eReminder tab and click Setup Wizard again to resume.");
					return;
					//FormEServicesSetup FormESS = new FormEServicesSetup(FormEServicesSetup.EService.SmsService);
					//FormESS.ShowDialog();
				}
				//======================REMINDERS======================
				bool isReminders = PrefC.GetBool(PrefName.ApptRemindAutoEnabled);
				if(!isReminders) {
					isReminders=promptUserB("eReminders Activation","Would you like to activate eReminders?");
					isReminderStarted|=isReminders;//true only if user answered yes
				}
				if(isReminders) {
					if(!PrefC.GetBool(PrefName.ApptRemindAutoEnabled)) {
						butActivateReminder_Click("Wizard",new EventArgs());
					}
					if(ruleRemindDefault==null) {
						if(!promptUserB("eReminders Activation","Would you like to use the default eReminder rule? (Can be edited later)")) {
							isReminderStarted=true;
							FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ReminderSameDay));
							FormARRE.ShowDialog();
							if(FormARRE.DialogResult==DialogResult.OK||FormARRE.ApptReminderRuleCur!=null) {
								ruleRemindDefault=FormARRE.ApptReminderRuleCur;
							}
						}
						//If user cancelled or deleted the reminder rule above, then use defaults.
						if(ruleRemindDefault==null) {
							ruleRemindDefault=ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ReminderSameDay);
						}
					}
					if(ruleRemindDefault.ApptReminderRuleNum==0) {//only if created above
						ApptReminderRules.Insert(ruleRemindDefault);
					}
					if(PrefC.HasClinicsEnabled) {
						if(!PrefC.GetBool(PrefName.ApptConfirmEnableForClinicZero)&&promptUserB("eReminders Activation","Automated eReminder and eConfirmation messages are sent based on which clinic a given appointment is assigned to. Do you want to allow automated messages to be sent for an appointment that is not associated with a clinic?")) {
							isReminderStarted=true;
							Prefs.UpdateBool(PrefName.ApptConfirmEnableForClinicZero,true);
						}
						if(listClinicsAllTemp.All(x => x.IsConfirmEnabled)) {
							//Good
						}
						else if(listClinicsAllTemp.All(x => !x.IsConfirmEnabled)||promptUserB("eReminders Activation","eReminders and eConfirmations are enabled for some Clinics but not others. Would you like to enable all remaining clinics?")) {
							listClinicsAllTemp.ForEach(x => x.IsConfirmEnabled=true);
						}
						//Some clinics found with no rules defined AND not using defaults. Set to use defaults.
						listClinicsAllTemp.FindAll(x => !x.IsConfirmDefault&&listRulesAllTemp.Count(y => y.ClinicNum==x.ClinicNum)==0).ForEach(x => x.IsConfirmDefault=true);
						listClinicsAllTemp.ForEach(x => Clinics.Update(x));
					}
					if(isReminderStarted) {
						MsgBox.Show(this,"Congratulations, eReminder activation complete!");
					}
					refreshLocalData();
				}//End IsReminders
				 //======================CONFIRMATIONS======================
				bool IsConfirmations = PrefC.GetBool(PrefName.ApptConfirmAutoEnabled);
				if(!IsConfirmations) {
					IsConfirmations=promptUserB("eConfirmations Activation","Would you like to activate eConfirmations?");
					isConfirmationStarted|=IsConfirmations;
				}
				if(IsConfirmations) {
					if(!PrefC.GetBool(PrefName.ApptConfirmAutoEnabled)) {
						butActivateConfirm_Click("Wizard",new EventArgs());
						if(!PrefC.GetBool(PrefName.ApptConfirmAutoEnabled)&&!promptUserB("eConfirmations Activation","Unable to activate eConfirmations, would you like to continue with the rest of the setup process?")) {
							RefreshPublicData();
							return;
						}
						isConfirmationStarted=true;
					}
					if(ruleConfirmDefault==null) {
						if(!promptUserB("eConfirmations Activation","Would you like to use the default eConfirmation rule? (Can be edited later)")) {
							FormApptReminderRuleEdit FormARRE = new FormApptReminderRuleEdit(ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ConfirmationFutureDay));
							FormARRE.ShowDialog();
							if(FormARRE.DialogResult==DialogResult.OK||FormARRE.ApptReminderRuleCur!=null) {
								ruleConfirmDefault=FormARRE.ApptReminderRuleCur;
								isConfirmationStarted=true;
							}
						}
						//If user cancelled or deleted the reminder rule above, then use defaults.
						if(ruleConfirmDefault==null) {
							ruleConfirmDefault=ApptReminderRules.CreateDefaultReminderRule(ApptReminderType.ConfirmationFutureDay);
							isConfirmationStarted=true;
						}
					}
					if(ruleConfirmDefault.ApptReminderRuleNum==0) {//only if created above
						ApptReminderRules.Insert(ruleConfirmDefault);
						isConfirmationStarted=true;
					}
					if(PrefC.HasClinicsEnabled) {
						if(!PrefC.GetBool(PrefName.ApptConfirmEnableForClinicZero)&&promptUserB("eConfirmations Activation","Automated eReminder and eConfirmation messages are sent based on which clinic a given appointment is assigned to. Do you want to allow automated messages to be sent for an appointment that is not associated with a clinic?")) {
							Prefs.UpdateBool(PrefName.ApptConfirmEnableForClinicZero,true);
							isConfirmationStarted=true;
						}
						if(listClinicsAllTemp.All(x => x.IsConfirmEnabled)) {
							//Good
						}
						else if(listClinicsAllTemp.All(x => !x.IsConfirmEnabled)||promptUserB("eConfirmations Activation","eReminders and eConfirmations are enabled for some Clinics but not others. Would you like to enable all remaining clinics?")) {
							listClinicsAllTemp.ForEach(x => x.IsConfirmEnabled=true);
						}
						//Some clinics found with no rules defined AND not using defaults. Set to use defaults.
						listClinicsAllTemp.FindAll(x => !x.IsConfirmDefault&&listRulesAllTemp.Count(y => y.ClinicNum==x.ClinicNum)==0).ForEach(x => x.IsConfirmDefault=true);
						listClinicsAllTemp.ForEach(x => Clinics.Update(x));
					}
					if(isConfirmationStarted) {
						MsgBox.Show(this,"Congratulations, eConfirmation setup complete!");
					}
				}//End IsConfirmations
			}
			catch(ApplicationException ae) {
				MessageBox.Show(ae.Message);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"An unexpected error has occured")+":\r\n\r\n"+ex.Message);
			}
			finally {
				RefreshPublicData();
			}
		}
	}
}
