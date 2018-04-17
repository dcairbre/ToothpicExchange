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
		///<summary>Keeps track of the last selected index for the Web Sched New Pat Appt URL grid.</summary>
		private int _indexLastNewPatURL=-1;
		private ODThread _threadFillGridWebSchedNewPatApptTimeSlots=null;
		///<summary>Set to true whenever the Web Sched new pat appt thread is already running while another thing wants it to refresh yet again.
		///E.g. The window loads which initially starts a fill thread and then the user quickly starts changing filters.</summary>
		private bool _isWebSchedNewPatApptTimeSlotsOutdated=false;
		///<summary>List of ClinicPrefs that holds information on what preferences have been changed.</summary>
		private List<ClinicPref> _listClinicPrefsWebSchedNewPats;
		private WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService _newPatApptUrlSelected {
			get {
				int sel=gridWebSchedNewPatApptURLs.GetSelectedIndex();
				if(sel<0 && gridWebSchedNewPatApptURLs.Rows.Count>=0) { //Default to first row if none selected.
					sel=0;
				}
				if(sel<0) {
					return new WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService();
				}
				return (WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService)gridWebSchedNewPatApptURLs.Rows[gridWebSchedNewPatApptURLs.GetSelectedIndex()].Tag;
			}
		}

		private bool IsTabValidWebSchedNewPat() {
			return true;
		}

		private void FillTabWebSchedNewPat() {
			int newPatApptDays=PrefC.GetInt(PrefName.WebSchedNewPatApptSearchAfterDays);
			textWebSchedNewPatApptMessage.Text=PrefC.GetString(PrefName.WebSchedNewPatApptMessage);
			textWebSchedNewPatApptSearchDays.Text=newPatApptDays>0 ? newPatApptDays.ToString() : "";
			textWebSchedNewPatApptLength.Text=PrefC.GetString(PrefName.WebSchedNewPatApptTimePattern);
			DateTime dateWebSchedNewPatSearch=DateTime.Now;
			dateWebSchedNewPatSearch=dateWebSchedNewPatSearch.AddDays(newPatApptDays);
			textWebSchedNewPatApptsDateStart.Text=dateWebSchedNewPatSearch.ToShortDateString();
			gridWebSchedNewPatApptURLs.ContextMenu=menuWebSchedNewPatApptHostedURLsRightClick;
			FillListBoxWebSchedBlockoutTypes(PrefC.GetString(PrefName.WebSchedNewPatApptIgnoreBlockoutTypes).Split(new char[] { ',' }),listboxWebSchedNewPatIgnoreBlockoutTypes);
			FillGridWebSchedNewPatApptHostedURLs();
			FillGridWebSchedNewPatApptTimeSlotsThreaded();
			FillGridWebSchedNewPatApptProcs();
			FillGridWebSchedNewPatApptOps();
		}

		private void SaveTabWebSchedNewPat() {
			Prefs.UpdateString(PrefName.WebSchedNewPatApptMessage,textWebSchedNewPatApptMessage.Text);
			List<ClinicPref> listClinicPrefs=new List<ClinicPref>();
			foreach(ODGridRow row in gridWebSchedNewPatApptURLs.Rows) {
				var clinic=(WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService)row.Tag;
				string allowChildren=(row.Cells[2].Text=="X" ? "1" : "0");
				if(clinic.ClinicNum==0) {
					Prefs.UpdateString(PrefName.WebSchedNewPatAllowChildren,allowChildren);
					continue;
				}
				ClinicPref clinicPref=_listClinicPrefsWebSchedNewPats
					.FirstOrDefault(x => x.ClinicNum==clinic.ClinicNum && x.PrefName==PrefName.WebSchedNewPatAllowChildren)?.Clone();
				if(clinicPref==null) {
					clinicPref=new ClinicPref(clinic.ClinicNum,PrefName.WebSchedNewPatAllowChildren,allowChildren);
				}
				else {
					clinicPref.ValueString=allowChildren;
				}
				listClinicPrefs.Add(clinicPref);
			}
			if(ClinicPrefs.Sync(listClinicPrefs,_listClinicPrefsWebSchedNewPats)) {
				DataValid.SetInvalid(InvalidType.ClinicPrefs);
			}
		}

		private void AuthorizeTabWebSchedNewPat(bool allowEdit) {
			butWebSchedNewPatBlockouts.Enabled=allowEdit;
			butWebSchedNewPatApptsAdd.Enabled=allowEdit;
			butWebSchedNewPatApptsRemove.Enabled=allowEdit;
			textWebSchedNewPatApptSearchDays.Enabled=allowEdit;
			textWebSchedNewPatApptLength.Enabled=allowEdit;
		}

		private void textWebSchedNewPatApptLength_Leave(object sender,EventArgs e) {
			//Only refresh if the value of this preference changed.  _indexLastNewPatURL will be set to -1 if a refresh is needed.
			if(_indexLastNewPatURL==-1) {
				FillGridWebSchedNewPatApptTimeSlotsThreaded();
			}
		}

		private void textWebSchedNewPatApptSearchDays_Leave(object sender,EventArgs e) {
			//Only refresh if the value of this preference changed.  _indexLastNewPatURL will be set to -1 if a refresh is needed.
			if(_indexLastNewPatURL==-1) {
				FillGridWebSchedNewPatApptTimeSlotsThreaded();
			}
		}

		private void FillGridWebSchedNewPatApptProcs() {
			List<string> listProcCodes=PrefC.GetString(PrefName.WebSchedNewPatApptProcs)
				.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)
				.ToList();
			gridWebSchedNewPatApptProcs.BeginUpdate();
			gridWebSchedNewPatApptProcs.Columns.Clear();
			gridWebSchedNewPatApptProcs.Columns.Add(new ODGridColumn(Lan.g(this,"Proc Codes"),0,HorizontalAlignment.Center));
			gridWebSchedNewPatApptProcs.Rows.Clear();
			ODGridRow row;
			foreach(string procCode in listProcCodes) {
				row=new ODGridRow();
				row.Cells.Add(procCode);
				row.Tag=procCode;
				gridWebSchedNewPatApptProcs.Rows.Add(row);
			}
			gridWebSchedNewPatApptProcs.EndUpdate();
		}

		private void gridWebSchedNewPatApptURLs_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Row!=_indexLastNewPatURL) {
				FillGridWebSchedNewPatApptTimeSlotsThreaded();
			}
			//Cell coordinates are [e.Row][e.Col]
			if(e.Col==2) { //Allow Children Column
				//Set the text of the clicked cell
				string cellTextCur=gridWebSchedNewPatApptURLs.Rows[e.Row].Cells[e.Col].Text;
				string cellTextNew=(cellTextCur=="X" ? "" : "X");
				gridWebSchedNewPatApptURLs.Rows[e.Row].Cells[e.Col].Text=cellTextNew;
				//refresh the grid to immediately show changes
				gridWebSchedNewPatApptURLs.Refresh();
			}
		}

		private void gridWebSchedNewPatApptURLs_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//Open the URL that the user double clicked on in case they are curious to see how the Web Sched app works.
			NavigateToURL(_newPatApptUrlSelected.HostedUrl);
		}

		private void FillGridWebSchedNewPatApptHostedURLs() {			
			List<Clinic> clinicsAll=Clinics.ListLong;
			var eServiceData=GetSignups<WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService>(eServiceCode.WebSchedNewPatAppt)
				.Select(x => new {
					Signup=x,
					ClinicName=x.ClinicNum==0 ? Lan.g(this,"Headquarters") : (clinicsAll.FirstOrDefault(y => y.ClinicNum==x.ClinicNum)??new Clinic() { Abbr="N\\A" }).Abbr
				})
				//HQ to the top.
				.OrderBy(x => x.Signup.ClinicNum!=0)
				//Everything else is alpha sorted.
				.ThenBy(x => x.ClinicName);				
			int selectedIndex=gridWebSchedNewPatApptURLs.GetSelectedIndex();
			long selectedClinicNum=-1;
			if(selectedIndex>-1) {
				selectedClinicNum=_newPatApptUrlSelected.ClinicNum;
			}
			_listClinicPrefsWebSchedNewPats=new List<ClinicPref>();
			gridWebSchedNewPatApptURLs.BeginUpdate();
			//Always update the grids title because the user could have had an error on load and double clicked to retry.
			gridWebSchedNewPatApptURLs.Title=Lan.g(this,"Hosted URLs");
			gridWebSchedNewPatApptURLs.Columns.Clear();
			gridWebSchedNewPatApptURLs.Columns.Add(new ODGridColumn(Lan.g(this,"Location"),100));
			gridWebSchedNewPatApptURLs.Columns.Add(new ODGridColumn(Lan.g(this,"Excluded"),60,HorizontalAlignment.Center));
			gridWebSchedNewPatApptURLs.Columns.Add(new ODGridColumn(Lan.g(this,"Allow Children"),60,HorizontalAlignment.Center));
			gridWebSchedNewPatApptURLs.Columns.Add(new ODGridColumn(Lan.g(this,"URL"),400));
			gridWebSchedNewPatApptURLs.Rows.Clear();
			foreach(var clinic in eServiceData) {
				ODGridRow row=new ODGridRow() {Tag=clinic.Signup};
				row.Cells.Add(clinic.ClinicName);
				row.Cells.Add(clinic.Signup.IsEnabled ? "" : "X");
				//Display preferences to allow children
				if(clinic.Signup.ClinicNum==0) {
					row.Cells.Add(PrefC.GetBool(PrefName.WebSchedNewPatAllowChildren) ? "X" : "");
				}
				else {					
					ClinicPref clinicPref=ClinicPrefs.GetPref(PrefName.WebSchedNewPatAllowChildren,clinic.Signup.ClinicNum);
					if(clinicPref==null) {
						row.Cells.Add(PrefC.GetBool(PrefName.WebSchedNewPatAllowChildren) ? "X" : "");
					}
					else {
						row.Cells.Add(clinicPref.ValueString=="1" ? "X" : "");
						_listClinicPrefsWebSchedNewPats.Add(clinicPref);
					}
				}
				row.Cells.Add(clinic.Signup.HostedUrl);
				gridWebSchedNewPatApptURLs.Rows.Add(row);
			}
			gridWebSchedNewPatApptURLs.EndUpdate();
			if(gridWebSchedNewPatApptURLs.Rows.Count<1) {//This should never happen.
				return;//No row to select / preserve so just return and do nothing.
			}
			//Now to select headquarters OR keep whatever clinic they had selected when this fill was called.
			gridWebSchedNewPatApptURLs.SetSelected(false);
			int indexDesired=0;
			if(selectedClinicNum>-1) {
				indexDesired=gridWebSchedNewPatApptURLs.Rows.Cast<ODGridRow>()
					.Select(x => x.Tag)
					.Cast<WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService>().ToList()
					.FindIndex(x => x.ClinicNum==selectedClinicNum);
				if(indexDesired==-1) {
					indexDesired=0;//There should always be at least one row for HQ.
				}
			}
			gridWebSchedNewPatApptURLs.SetSelected(indexDesired,true);
		}

		private void menuItemCopyURL_Click(object sender,EventArgs e) {
			if(gridWebSchedNewPatApptURLs.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Select a URL to copy first.");
				return;
			}
			try {
				Clipboard.SetText(_newPatApptUrlSelected.HostedUrl);
			}
			catch(Exception) {
				MsgBox.Show(this,"Could not copy the URL to the clipboard.");
			}
		}

		private void menuItemNavigateToURL_Click(object sender,EventArgs e) {
			if(gridWebSchedNewPatApptURLs.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Select a URL to navigate to first.");
				return;
			}
			NavigateToURL(_newPatApptUrlSelected.HostedUrl);
		}

		private void NavigateToURL(string URL) {
			try {
				Process.Start(URL);
			}
			catch(Exception) {
				MsgBox.Show(this,"There was a problem launching the URL with a web browser.  Make sure a default browser has been set.");
			}
		}

		private void FillGridWebSchedNewPatApptOps() {
			int opNameWidth=200;
			int clinicWidth=80;
			if(!PrefC.HasClinicsEnabled) {
				opNameWidth+=clinicWidth;
			}
			gridWebSchedNewPatApptOps.BeginUpdate();
			gridWebSchedNewPatApptOps.Columns.Clear();
			gridWebSchedNewPatApptOps.Columns.Add(new ODGridColumn(Lan.g("FormEServicesSetup","Op Name"),opNameWidth));
			gridWebSchedNewPatApptOps.Columns.Add(new ODGridColumn(Lan.g("FormEServicesSetup","Abbrev"),90));
			if(PrefC.HasClinicsEnabled) {
				gridWebSchedNewPatApptOps.Columns.Add(new ODGridColumn(Lan.g("FormEServicesSetup","Clinic"),clinicWidth));
			}
			gridWebSchedNewPatApptOps.Columns.Add(new ODGridColumn(Lan.g("FormEServicesSetup","Provider"),90));
			gridWebSchedNewPatApptOps.Columns.Add(new ODGridColumn(Lan.g("FormEServicesSetup","Hygienist"),90));
			gridWebSchedNewPatApptOps.Rows.Clear();
			//A list of all operatories that have IsWebSched set to true.
			List<Operatory> listWebSchedNewPatApptOps=Operatories.GetOpsForWebSchedNewPatAppts();
			ODGridRow row;
			foreach(Operatory op in listWebSchedNewPatApptOps) {
				row=new ODGridRow();
				row.Cells.Add(op.OpName);
				row.Cells.Add(op.Abbrev);
				if(PrefC.HasClinicsEnabled) {
					row.Cells.Add(Clinics.GetAbbr(op.ClinicNum));
				}
				row.Cells.Add(Providers.GetAbbr(op.ProvDentist));
				row.Cells.Add(Providers.GetAbbr(op.ProvHygienist));
				row.Tag=op;
				gridWebSchedNewPatApptOps.Rows.Add(row);
			}
			gridWebSchedNewPatApptOps.EndUpdate();
		}

		private void FillGridWebSchedNewPatApptTimeSlotsThreaded() {
			if(this.InvokeRequired) {
				this.BeginInvoke((Action)delegate () {
					FillGridWebSchedNewPatApptTimeSlotsThreaded();
				});
				return;
			}
			//Clear the current grid rows before starting the thread below. This allows that thread to exit at any time without leaving old rows in the grid.
			gridWebSchedNewPatApptTimeSlots.BeginUpdate();
			gridWebSchedNewPatApptTimeSlots.Rows.Clear();
			gridWebSchedNewPatApptTimeSlots.EndUpdate();
			//Validate time slot settings.
			if(textWebSchedNewPatApptsDateStart.errorProvider1.GetError(textWebSchedNewPatApptsDateStart)!="") {
				//Don't bother warning the user.  It will just be annoying.  The red indicator should be sufficient.
				return;
			}
			if(gridWebSchedNewPatApptURLs.GetSelectedIndex()<0) {
				return;//Nothing to do.
			}
			//Protect against re-entry
			if(_threadFillGridWebSchedNewPatApptTimeSlots!=null) {
				//A thread is already refreshing the time slots grid so we simply need to queue up another refresh once the one thread has finished.
				_isWebSchedNewPatApptTimeSlotsOutdated=true;
				return;
			}
			_isWebSchedNewPatApptTimeSlotsOutdated=false;
			_indexLastNewPatURL=gridWebSchedNewPatApptURLs.GetSelectedIndex();
			DateTime dateStart=PIn.DateT(textWebSchedNewPatApptsDateStart.Text);
			DateTime dateEnd=dateStart.AddDays(30);
			if(!_newPatApptUrlSelected.IsEnabled) {
				return;//Do nothing, this clinic is excluded from New Pat Appts.
			}
			//Only get time slots for headquarters or clinics that are NOT excluded (aka included).
			var args=new {
				ClinicNum=_newPatApptUrlSelected.ClinicNum,
				DateStart=dateStart,
				DateEnd=dateStart.AddDays(30)
			};
			_threadFillGridWebSchedNewPatApptTimeSlots=new ODThread(new ODThread.WorkerDelegate((th) => {
				//The user might not have Web Sched ops set up correctly.  Don't warn them here because it is just annoying.  They'll figure it out.
				ODException.SwallowAnyException(() => {
					//Get the next 30 days of open time schedules with the current settings
					List<TimeSlot> listTimeSlots=TimeSlots.GetAvailableNewPatApptTimeSlots(args.DateStart,args.DateEnd,args.ClinicNum);
					FillGridWebSchedNewPatApptTimeSlots(listTimeSlots);					
				});				
			})) { Name="ThreadWebSchedNewPatApptTimeSlots" };			
			_threadFillGridWebSchedNewPatApptTimeSlots.AddExitHandler(new ODThread.WorkerDelegate((th) => {
				_threadFillGridWebSchedNewPatApptTimeSlots=null;
				//If something else wanted to refresh the grid while we were busy filling it then we need to refresh again.  A filter could have changed.
				if(_isWebSchedNewPatApptTimeSlotsOutdated) {
					FillGridWebSchedNewPatApptTimeSlotsThreaded();
				}
			}));
			_threadFillGridWebSchedNewPatApptTimeSlots.Start(true);
		}
		
		private void FillGridWebSchedNewPatApptTimeSlots(List<TimeSlot> listTimeSlots) {
			if(this.InvokeRequired) {
				this.Invoke((Action)delegate () { FillGridWebSchedNewPatApptTimeSlots(listTimeSlots); });
				return;
			}
			gridWebSchedNewPatApptTimeSlots.BeginUpdate();
			gridWebSchedNewPatApptTimeSlots.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",0);
			col.TextAlign=HorizontalAlignment.Center;
			gridWebSchedNewPatApptTimeSlots.Columns.Add(col);
			gridWebSchedNewPatApptTimeSlots.Rows.Clear();
			ODGridRow row;
			DateTime dateTimeSlotLast=DateTime.MinValue;
			foreach(TimeSlot timeSlot in listTimeSlots) {
				//Make a new row for every unique day.
				if(dateTimeSlotLast.Date!=timeSlot.DateTimeStart.Date) {
					dateTimeSlotLast=timeSlot.DateTimeStart;
					row=new ODGridRow();
					row.ColorBackG=Color.LightBlue;
					row.Cells.Add(timeSlot.DateTimeStart.ToShortDateString());
					gridWebSchedNewPatApptTimeSlots.Rows.Add(row);
				}
				row=new ODGridRow();
				row.Cells.Add(timeSlot.DateTimeStart.ToShortTimeString()+" - "+timeSlot.DateTimeStop.ToShortTimeString());
				gridWebSchedNewPatApptTimeSlots.Rows.Add(row);
			}
			gridWebSchedNewPatApptTimeSlots.EndUpdate();
		}

		private void gridWebSchedNewPatApptOps_DoubleClick(object sender,EventArgs e) {
			ShowOperatoryEditAndRefreshGrids();
		}

		private void butWebSchedNewPatApptsAdd_Click(object sender,EventArgs e) {
			FormProcCodes FormPC=new FormProcCodes();
			FormPC.IsSelectionMode=true;
			FormPC.ShowDialog();
			if(FormPC.DialogResult!=DialogResult.OK) {
				return;
			}
			string procCode=ProcedureCodes.GetStringProcCode(FormPC.SelectedCodeNum);
			string prefProcs=PrefC.GetString(PrefName.WebSchedNewPatApptProcs);
			if(!string.IsNullOrEmpty(prefProcs)) {
				prefProcs+=",";
			}
			prefProcs+=procCode;
			Prefs.UpdateString(PrefName.WebSchedNewPatApptProcs,prefProcs);
			FillGridWebSchedNewPatApptProcs();
		}

		private void butWebSchedNewPatApptsRemove_Click(object sender,EventArgs e) {
			int selectedIndex=gridWebSchedNewPatApptProcs.GetSelectedIndex();
			if(selectedIndex==-1) {
				MsgBox.Show(this,"Select a procedure to remove.");
				return;
			}
			string procCode=(string)gridWebSchedNewPatApptProcs.Rows[selectedIndex].Tag;
			List<string> listProcCodes=PrefC.GetString(PrefName.WebSchedNewPatApptProcs).Split(',').ToList();
			listProcCodes.Remove(procCode);
			Prefs.UpdateString(PrefName.WebSchedNewPatApptProcs,string.Join(",",listProcCodes));
			FillGridWebSchedNewPatApptProcs();
		}

		private void butWebSchedNewPatApptsToday_Click(object sender,EventArgs e) {
			textWebSchedNewPatApptsDateStart.Text=DateTime.Today.ToShortDateString();
		}

		private void textWebSchedNewPatApptSearchDays_Validated(object sender,EventArgs e) {
			if(textWebSchedNewPatApptSearchDays.errorProvider1.GetError(textWebSchedNewPatApptSearchDays)!="") {
				return;
			}
			int newPatApptDays=PIn.Int(textWebSchedNewPatApptSearchDays.Text);
			if(Prefs.UpdateInt(PrefName.WebSchedNewPatApptSearchAfterDays,newPatApptDays>0 ? newPatApptDays : 0)) {
				_indexLastNewPatURL=-1;//Force refresh of the grid in because this setting changed.
			}
		}

		private void textWebSchedNewPatApptLength_TextChanged(object sender,EventArgs e) {
			int selectionStart=textWebSchedNewPatApptLength.SelectionStart;
			char[] arrayChars=textWebSchedNewPatApptLength.Text.ToCharArray();
			string newPatApptLength=new string(Array.FindAll<char>(arrayChars,(x => (x=='x' || x=='X' || x=='/')))).ToUpper();
			textWebSchedNewPatApptLength.Text=newPatApptLength;
			//If no text was removed, put the cursor back to where it was
			if(arrayChars.Length==newPatApptLength.Length) {
				textWebSchedNewPatApptLength.Select(selectionStart,0);
			}
			else if(selectionStart>0) {//The character typed in was removed and there is still text in the box.
				textWebSchedNewPatApptLength.Select(selectionStart-1,0);
			}
			if(Prefs.UpdateString(PrefName.WebSchedNewPatApptTimePattern,newPatApptLength)) {
				_indexLastNewPatURL=-1;//Force refresh of the grid in because this setting changed.
			}
		}

		private void textWebSchedNewPatApptsDateStart_TextChanged(object sender,EventArgs e) {
			//Only refresh the grid if the user has typed in a valid date.
			if(textWebSchedNewPatApptsDateStart.errorProvider1.GetError(textWebSchedNewPatApptsDateStart)=="") {
				FillGridWebSchedNewPatApptTimeSlotsThreaded();
			}
		}

		private void butWebSchedNewPatBlockouts_Click(object sender,EventArgs e) {
			string[] arrayDefNums=PrefC.GetString(PrefName.WebSchedNewPatApptIgnoreBlockoutTypes).Split(new char[] {','}); //comma-delimited list.
			List<long> listBlockoutTypes=new List<long>();
			foreach(string strDefNum in arrayDefNums) {
				listBlockoutTypes.Add(PIn.Long(strDefNum));
			}
			List<Def> listBlockoutTypeDefs=DefC.GetDefs(DefCat.BlockoutTypes,listBlockoutTypes);
			FormDefinitionPicker FormDP=new FormDefinitionPicker(DefCat.BlockoutTypes,listBlockoutTypeDefs);
			FormDP.HasShowHiddenOption=true;
			FormDP.IsMultiSelectionMode=true;
			FormDP.ShowDialog();
			if(FormDP.DialogResult==DialogResult.OK) {
				listboxWebSchedNewPatIgnoreBlockoutTypes.Items.Clear();
				foreach(Def defCur in FormDP.ListSelectedDefs) {
					listboxWebSchedNewPatIgnoreBlockoutTypes.Items.Add(defCur.ItemName);
				}
				string strListWebSchedNewPatIgnoreBlockoutTypes=String.Join(",",FormDP.ListSelectedDefs.Select(x => x.DefNum));
				Prefs.UpdateString(PrefName.WebSchedNewPatApptIgnoreBlockoutTypes,strListWebSchedNewPatIgnoreBlockoutTypes);
			}
		}
	}
}
