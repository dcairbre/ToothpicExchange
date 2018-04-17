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
	///<summary>Form manages all eServices setup.  Also includes monitoring for the Listener Service that is required for HQ hosted eServices.</summary>
	public partial class FormEServicesSetup:ODForm {
		///<summary>Output from HQ initialized in FillForm().</summary>
		WebServiceMainHQProxy.EServiceSetup.SignupOut _singupOut=new WebServiceMainHQProxy.EServiceSetup.SignupOut();
		private List<T> GetSignups<T>(eServiceCode eService) where T : WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService {
			return _singupOut.EServices
				.FindAll(x => x.EService==eService)
				.Cast<T>().ToList();
		}

		///<summary>Indicates if HQ says this account is registered for the given eService.</summary>
		private bool IsEServiceActive(eServiceCode eService) {			
			return GetSignups<WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService>(eService)
				.Any(x => x.IsEnabled);
		}

		///<summary>Launches the eServices Setup window defaulted to the tab of the eService passed in.</summary>
		public FormEServicesSetup(EService setTab=EService.SignupPortal) {
			InitializeComponent();
			Lan.F(this);
			Lan.C(this,menuWebSchedNewPatApptHostedURLsRightClick);			
			switch(setTab) {
				case EService.ListenerService:
					tabControl.SelectTab(tabEConnector);
					break;
				case EService.MobileOld:
					tabControl.SelectTab(tabMobileSynch);
					break;
				case EService.MobileNew:
					tabControl.SelectTab(tabMobileWeb);
					break;
				case EService.WebSched:
					tabControl.SelectTab(tabWebSched);
					break;
				case EService.SmsService:
					tabControl.SelectTab(tabTexting);
					break;
				case EService.eConfirmRemind:
					tabControl.SelectTab(tabECR);
					break;
				case EService.eMisc:
					tabControl.SelectTab(tabMisc);
					break;
				case EService.PatientPortal:
					tabControl.SelectTab(tabPatientPortal);
					break;
				case EService.SignupPortal:
				default:
					tabControl.SelectTab(tabSignup);
					break;
			}
		}

		private void FormEServicesSetup_Load(object sender,EventArgs e) {
			FillForm();
		}

		///<summary>Makes a web call to WebServiceMainHQ to get the corresponding EServiceSetupFull information and then attempts to fill each tab.
		///If anything goes wrong within this method a message box will show to the user and then the window will auto close via Abort.</summary>
		private void FillForm() {
			Action actionCloseProgress=ODProgress.ShowProgressStatus("EServicesSetupProgress","Validating eServices...");
			try {
				if(MiscUtils.TryUpdateIeEmulation()) {
					throw new Exception("Browser emulation version updated.\r\nYou must restart this application before accessing the Signup Portal.");
				}
				//Send light version of clinics to HQ to be used by signup portal below. Get back all args needed from HQ in order to perform the operations of this window.
				SignupPortalPermission perm=SignupPortalPermission.ReadOnly;
				if(Security.IsAuthorized(Permissions.SecurityAdmin,true)) {
					perm=SignupPortalPermission.FullPermission;
				}
				else if(Security.IsAuthorized(Permissions.EServicesSetup,true)) {
					perm=SignupPortalPermission.ReadOnly;
				}
				else {
					perm=SignupPortalPermission.Denied;
				}
				//Clinics will be stored in this order at HQ to allow signup portal to display them in proper order.
				List<Clinic> clinics=Clinics.ListLong.OrderBy(x => x.ItemOrder).ToList();
				if(PrefC.GetBool(PrefName.ClinicListIsAlphabetical)) {
					clinics.OrderBy(x => x.Abbr);
				}
				_singupOut=WebServiceMainHQProxy.GetEServiceSetupFull(
					perm,
					0, //ClinicNum is not currently used.
					PrefC.GetString(PrefName.ProgramVersion),
					clinics
						.Select(x => new WebServiceMainHQProxy.EServiceSetup.SignupIn.ClinicLiteIn() {
							ClinicNum=x.ClinicNum,
							ClinicTitle=x.Abbr,
							IsHidden=x.IsHidden,
						}).ToList());
				#region Fill
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Signup")));
				FillTabSignup();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - eConnector Service")));
				FillTabEConnector();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Mobile Synch (old-style)")));
				FillTabMobileSynch();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Mobile Web")));
				FillTabMobileWeb();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Patient Portal")));
				FillTabPatientPortal();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Web Sched Recall")));
				FillTabWebSchedRecall();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Web Sched New Pat Appt")));
				FillTabWebSchedNewPat();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Texting Services")));
				FillTabTexting();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - eReminders & eConfirmations")));
				FillTabECR();
				EServicesEvent.Fire(new ODEventArgs("EServicesSetupProgress",Lan.g(this,"Loading tab - Miscellaneous")));
				FillTabMisc();
				#endregion
				#region Authorize editing
				//Disable certain buttons but let them continue to view.
				bool allowEdit=Security.IsAuthorized(Permissions.EServicesSetup,true);
				AuthorizeTabSignup(allowEdit);
				AuthorizeTabEConnector(allowEdit);
				AuthorizeTabMobileSynch(allowEdit);
				AuthorizeTabPatientPortal(allowEdit);
				AuthorizeTabWebSchedRecall(allowEdit);
				AuthorizeTabWebSchedNewPat(allowEdit);
				AuthorizeTabTexting(allowEdit);
				AuthorizeTabECR(allowEdit);
				AuthorizeTabMisc(allowEdit);
				((Control)tabMobileSynch).Enabled=allowEdit;
				#endregion
			}
			catch(WebException we) {
				actionCloseProgress?.Invoke();
				MessageBox.Show(Lan.g(this,"Could not reach HQ.  Please make sure you have an internet connection and try again or call support."
					+"\r\n\r\nError Message")+":\r\n"+we.Message);
				//Set the dialog result to Abort so that FormClosing knows to not try and save any changes.
				DialogResult=DialogResult.Abort;
				Close();
			}
			catch(Exception e) {
				actionCloseProgress?.Invoke();
				MessageBox.Show(Lan.g(this,"There was a problem loading the eServices Setup window.  Please try again or call support."
					+"\r\n\r\nError Message")+":\r\n"+e.Message);
				//Set the dialog result to Abort so that FormClosing knows to not try and save any changes.
				DialogResult=DialogResult.Abort;
				Close();
			}
			actionCloseProgress?.Invoke();
			//These next three lines are to prevent this window from opening up behind all other windows. I (ChrisM) believe this is happening because
			//FormProgressStatus is not being run on the main UI thread.
			TopMost=true;
			Application.DoEvents();
			TopMost=false;
		}

		///<summary>Validate form inputs and display any required messages to user. Returns true if all info valid.</summary>
		private bool IsFormValid() {
			if(
				!IsTabValidSignup() ||
				!IsTabValidEConnector()||
				!IsTabValidMobileSynch()||
				!IsTabValidMobileWeb()||
				!IsTabValidPatientPortal()||
				!IsTabValidWebSchedRecall()||
				!IsTabValidWebSchedNewPat()||
				!IsTabValidTexting()||
				!IsTabValidECR()||
				!IsTabValidMisc()) 
			{
				return false;
			}
			return true;
		}

		private bool SaveForm() {
			if(!IsFormValid()) {
				return false;
			}
			SaveTabSignup();
			SaveTabEConnector();
			SaveTabMobileSynch();
			SaveTabMobileWeb();
			SaveTabPatientPortal();
			SaveTabWebSchedRecall();
			SaveTabWebSchedNewPat();
			SaveTabTexting();
			SaveTabECR();
			SaveTabMisc();
			DataValid.SetInvalid(InvalidType.Prefs);
			DataValid.SetInvalid(InvalidType.Providers);//Providers includes clinics.
			return true;
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormEServicesSetup_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.Abort || !Security.IsAuthorized(Permissions.EServicesSetup,true)) {
				return;
			}
			//Anything they could have modified should have been disabled on load anyways.
			if(!SaveForm()) { //Something failed. Ask the user if they are ok with exiting and not saving.
				if(!MsgBox.Show(this,true,"Validation failed and no changes were saved. Would you like to close without saving?")) {
					//User wants to keep the form open.
					e.Cancel=true;
				}
			}
		}

		private void tabControl_Deselecting(object sender,TabControlCancelEventArgs e) {
			bool isTabValid=false;
			bool doFillForm=false;
			if(e.TabPage==tabSignup) {
				isTabValid=IsTabValidSignup();
				if(isTabValid) { //Signup portal may have some changes so refill the entire form. This is chatty but it has to be this way.
					doFillForm=true;
				}
			}
			else if(e.TabPage==tabEConnector) {
				isTabValid=IsTabValidEConnector();
			}
			else if(e.TabPage==tabMobileSynch) {
				isTabValid=IsTabValidMobileSynch();
			}
			else if(e.TabPage==tabMobileWeb) {
				isTabValid=IsTabValidMobileWeb();
			}
			else if(e.TabPage==tabPatientPortal) {
				isTabValid=IsTabValidPatientPortal();
			}
			else if(e.TabPage==tabWebSched) {
				isTabValid=IsTabValidWebSchedRecall()&&IsTabValidWebSchedNewPat();
			}
			else if(e.TabPage==tabTexting) {
				isTabValid=IsTabValidTexting();
			}
			else if(e.TabPage==tabECR) {
				isTabValid=IsTabValidECR();
			}
			else if(e.TabPage==tabMisc) {
				isTabValid=IsTabValidMisc();
			}
			else {
				throw new Exception("New eService TabPage validation has not been implemented for: "+e.TabPage.Text);
			}
			e.Cancel=!isTabValid;
			if(doFillForm) {
				FillForm();
			}
		}
		
		///<summary>Typically used in ctor determine which tab should be activated be default.</summary>
		public enum EService {
			PatientPortal,
			MobileOld,
			MobileNew,
			WebSched,
			ListenerService,
			SmsService,
			eConfirmRemind,
			eMisc,
			SignupPortal,
		}
	}
}