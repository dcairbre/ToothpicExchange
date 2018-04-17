using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using CodeBase;
using OpenDental.UI;
using OpenDental.WebSheets;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary>
	/// This Form is primarily used by the dental office to upload sheetDefs
	/// </summary>
	public partial class FormWebFormSetup:ODForm {

		private string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
		private string SheetDefAddress="";
		WebSheets.Sheets wh=new WebSheets.Sheets();
		OpenDental.WebSheets.webforms_sheetdef[] sheetDefList;
		long DentalOfficeID=0;
		private List<long> _listSelectedNextFormIds=new List<long>();
		private List<Clinic> _listClinics;

		public FormWebFormSetup() {
			InitializeComponent();
			gridMain.ContextMenu=menuWebFormSetupRight;
			Lan.F(this);
		}

		private void FormWebFormSetup_Load(object sender,EventArgs e) {
			if(!PrefC.HasClinicsEnabled) {
				comboClinic.Visible=false;
				labelClinic.Visible=false;
				butPickClinic.Visible=false;
			}
			else {
				_listClinics=Clinics.GetForUserod(Security.CurUser);
				comboClinic.Items.Add(Lan.g(this,"None"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<_listClinics.Count;i++) {
					comboClinic.Items.Add(_listClinics[i].Abbr);
					if(_listClinics[i].ClinicNum==Clinics.ClinicNum) {
						comboClinic.SelectedIndex=i+1;//Plus 1 for 'None'
					}
				}
			}
		}

		private void FormWebFormSetup_Shown(object sender,EventArgs e) {
			FetchValuesFromWebServer();
		}

		private void FetchValuesFromWebServer() {
			try {
				String WebHostSynchServerURL=PrefC.GetString(PrefName.WebHostSynchServerURL);
				textboxWebHostAddress.Text=WebHostSynchServerURL;
				butSave.Enabled=false;
				if((WebHostSynchServerURL==WebFormL.SynchUrlStaging) || (WebHostSynchServerURL==WebFormL.SynchUrlDev)) {
					WebFormL.IgnoreCertificateErrors();
				}
				Cursor=Cursors.WaitCursor;
				if(!TestWebServiceExists()) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
					return;
				}
				DentalOfficeID=wh.GetDentalOfficeID(RegistrationKey);
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				OpenDental.WebSheets.webforms_preference PrefObj=wh.GetPreferences(RegistrationKey);
				if(PrefObj==null) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"There has been an error retrieving values from the server");
				}
				butWebformBorderColor.BackColor=Color.FromArgb(PrefObj.ColorBorder);
				SheetDefAddress=wh.GetSheetDefAddress(RegistrationKey);
				//dennis: the below if statement is for backward compatibility only April 14 2011 and can be removed later.
				if(String.IsNullOrEmpty(PrefObj.CultureName)){
					PrefObj.CultureName=System.Globalization.CultureInfo.CurrentCulture.Name;
					wh.SetPreferencesV2(RegistrationKey,PrefObj);
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			FillGrid();//Also gets sheet def list from server
			Cursor=Cursors.Default;
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. This was made with the intention of testing the correctness of the webservice URL. If an incorrect webservice URL is used in a background thread the exception cannot be handled easily to a point where even a correct URL cannot be keyed in by the user. Because an exception in a background thread closes the Form which spawned it.
		/// </summary>
		/// <returns></returns>
		private bool TestWebServiceExists() {
			try {
				wh.Url=textboxWebHostAddress.Text;
				//if(textboxWebHostAddress.Text.Contains("192.168.0.196") || textboxWebHostAddress.Text.Contains("localhost")) {
				if(textboxWebHostAddress.Text.Contains("10.10.1.196") || textboxWebHostAddress.Text.Contains("localhost")) {
					WebFormL.IgnoreCertificateErrors();// done so that TestWebServiceExists() does not thow an error.
				}
				if(wh.ServiceExists()){
					return true;
				}
			}
			catch{//(Exception ex) {
				return false;
			}
			return true;
		}

		///<summary>This now also gets a new list of sheet defs from the server.  But it's only called after testing that the web service exists.</summary>
		private void FillGrid() {
			try{
				wh.Url=textboxWebHostAddress.Text;
				sheetDefList=wh.DownloadSheetDefs(RegistrationKey)??new OpenDental.WebSheets.webforms_sheetdef[0];
				gridMain.Columns.Clear();
				ODGridColumn col=new ODGridColumn(Lan.g(this,"Description"),200);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Browser Address For Patients"),510);
				gridMain.Columns.Add(col);
				gridMain.Rows.Clear();
				for(int i=0;i<sheetDefList.Length;i++) {
					ODGridRow row=new ODGridRow();
					row.Tag=sheetDefList[i];
					row.Cells.Add(sheetDefList[i].Description);
					row.Cells.Add(SheetDefBaseURL(sheetDefList[i]));
					gridMain.Rows.Add(row);
				}
				gridMain.EndUpdate();
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}

		private string SheetDefBaseURL(webforms_sheetdef sheetDef) {
			return SheetDefAddress+"?DOID="+DentalOfficeID+"&WSDID="+sheetDef.WebSheetDefID;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			OpenBrowser();
		}

		private void gridMain_MouseUp(object sender,MouseEventArgs e) {
			ConstructURLs();
		}

		private void menuItemNavigateURL_Click(object sender,EventArgs e) {
			OpenBrowser();
		}

		private void menuItemCopyURL_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			OpenDental.WebSheets.webforms_sheetdef WebSheetDef=(OpenDental.WebSheets.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			try {
				Clipboard.SetText(SheetDefBaseURL(WebSheetDef));
			}
			catch(Exception ex) {
				MsgBox.Show(this,"Could not copy contents to the clipboard.  Please try again.");
				ex.DoNothing();
			}
		}

		private void OpenBrowser() {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			OpenDental.WebSheets.webforms_sheetdef WebSheetDef=(OpenDental.WebSheets.webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			System.Diagnostics.Process.Start(SheetDefBaseURL(WebSheetDef));
		}

		private void textboxWebHostAddress_TextChanged(object sender,EventArgs e) {
			butSave.Enabled=true;
		}

		private void butSave_Click(object sender,EventArgs e) {
			//disabled unless user changed url
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			try {
				Prefs.UpdateString(PrefName.WebHostSynchServerURL,textboxWebHostAddress.Text.Trim());
				butSave.Enabled=false;
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			FetchValuesFromWebServer();
			Cursor=Cursors.Default;
		}

		private void butWebformBorderColor_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void butChange_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void ShowColorDialog(){
			colorDialog1.Color=butWebformBorderColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			butWebformBorderColor.BackColor=colorDialog1.Color;
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect.");
				return;
			}
			try {
				if(wh.GetDentalOfficeID(RegistrationKey)==0) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key incorrect.");
					return;
				}
				OpenDental.WebSheets.webforms_preference PrefObj=new OpenDental.WebSheets.webforms_preference();
				PrefObj.ColorBorder=butWebformBorderColor.BackColor.ToArgb();
				PrefObj.CultureName=System.Globalization.CultureInfo.CurrentCulture.Name;
				bool IsPrefSet=wh.SetPreferencesV2(RegistrationKey,PrefObj);
				Cursor=Cursors.Default;
				if(!IsPrefSet) {
					MsgBox.Show(this,"Error, color could not be saved to server.");
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}

		private void textRedirectURL_TextChanged(object sender,EventArgs e) {
			ConstructURLs();
		}

		private void butNextForms_Click(object sender,EventArgs e) {
			InputBox input=new InputBox("Select next forms",sheetDefList.Select(x => x.Description).ToList());
			input.Text=Lan.g(this,"Select Sheet Defs");
			input.IsMultiSelect=true;
			if(input.ShowDialog()==DialogResult.OK) {
				_listSelectedNextFormIds=input.SelectedIndices.Select(x => sheetDefList[x].WebSheetDefID).ToList();
				textNextForms.Text=string.Join(", ",input.SelectedIndices.Select(x => sheetDefList[x].Description));
				ConstructURLs();
			}
		}

		private void butPickClinic_Click(object sender,EventArgs e) {
			FormClinics FormC=new FormClinics();
			FormC.IsSelectionMode=true;
			if(FormC.ShowDialog()==DialogResult.OK) {
				for(int i=0;i<_listClinics.Count;i++) {
					if(_listClinics[i].ClinicNum!=FormC.SelectedClinicNum) {
						continue;
					}
					comboClinic.SelectedIndex=i+1;//Plus 1 for 'None'
				}
			}
		}

		private void comboClinic_SelectedIndexChanged(object sender,EventArgs e) {
			ConstructURLs();
		}

		private void ConstructURLs() {
			textURLs.Clear();
			List<string> listURLs=new List<string>();
			foreach(webforms_sheetdef sheetDef in gridMain.SelectedIndices.Select(x => gridMain.Rows[x].Tag).Cast<webforms_sheetdef>()) {
				string url=SheetDefBaseURL(sheetDef);
				if(_listSelectedNextFormIds.Count>0) {
					url+="&NFID="+string.Join("&NFID=",_listSelectedNextFormIds);
				}
				if(comboClinic.SelectedIndex > 0) {//'None' is not selected
					url+="&CID="+_listClinics[comboClinic.SelectedIndex-1].ClinicNum;//Minus 1 for 'None'
				}
				if(textRedirectURL.Text != "") {
					url+="&ReturnURL="+HttpUtility.UrlEncode(textRedirectURL.Text);
				}
				listURLs.Add(url);
			}
			textURLs.AppendText(string.Join("\r\n",listURLs));
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormSheetPicker FormS=new FormSheetPicker();
			FormS.SheetType=SheetTypeEnum.PatientForm;
			FormS.HideKioskButton=true;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			//Make sure each selected sheet contains FName, LName, and Birthdate.
			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {//There will always only be one
				if(!WebFormL.VerifyRequiredFieldsPresent(FormS.SelectedSheetDefs[i])) {
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			for(int i=0;i<FormS.SelectedSheetDefs.Count;i++) {//There will always only be one
				WebFormL.LoadImagesToSheetDef(FormS.SelectedSheetDefs[i]);
				wh.Timeout=300000; //for slow connections more timeout is provided. The  default is 100 seconds i.e 100000
				wh.UpLoadSheetDef(RegistrationKey,FormS.SelectedSheetDefs[i]);
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butUpdate_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length < 1) {
				MsgBox.Show(this,"Please select an item from the grid first.");
				return;
			}
			if(gridMain.SelectedIndices.Length > 1) {
				MsgBox.Show(this,"Please select one web form at a time.");
				return;
			}
			webforms_sheetdef wf_sheetDef=(webforms_sheetdef)gridMain.Rows[gridMain.SelectedIndices[0]].Tag;
			SheetDef sheetDef=null;
			if(wf_sheetDef.SheetDefNum != 0 
				&& SheetDefC.Listt.Any(x => x.SheetDefNum==wf_sheetDef.SheetDefNum))
			{
				sheetDef=SheetDefC.Listt.FirstOrDefault(x => x.SheetDefNum==wf_sheetDef.SheetDefNum);
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			if(sheetDef==null) {//This web form has never had a SheetDefNum assigned or the sheet has been deleted.
				MsgBox.Show(this,"This Web Form is not linked to a valid Sheet.  Please select the correct Sheet that this Web Form should be linked to.");
				FormSheetPicker FormS=new FormSheetPicker();
				FormS.SheetType=SheetTypeEnum.PatientForm;
				FormS.HideKioskButton=true;
				FormS.ShowDialog();
				if(FormS.DialogResult != DialogResult.OK || FormS.SelectedSheetDefs.Count==0) {
					return;
				}
				sheetDef=FormS.SelectedSheetDefs.FirstOrDefault();
			}
			if(!WebFormL.VerifyRequiredFieldsPresent(sheetDef)) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			WebFormL.LoadImagesToSheetDef(sheetDef);
			wh.UpdateSheetDef(RegistrationKey,wf_sheetDef.WebSheetDefID,sheetDef);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an item from the grid first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			if(!TestWebServiceExists()) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Either the web service is not available or the WebHostSynch URL is incorrect");
				return;
			}
			foreach(webforms_sheetdef wf_sheetDef in gridMain.SelectedIndices.Select(x => gridMain.Rows[x].Tag).Cast<webforms_sheetdef>()) {
				wh.DeleteSheetDef(RegistrationKey,wf_sheetDef.WebSheetDefID);
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}