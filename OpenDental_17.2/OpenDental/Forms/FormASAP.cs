using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Linq;

namespace OpenDental{
///<summary></summary>
	public class FormASAP:ODForm {
		private IContainer components;
		private OpenDental.UI.Button butClose;
		///<summary></summary>
		public bool PinClicked=false;		
		///<summary></summary>
		public static string procsForCur;
		private OpenDental.UI.ODGrid grid;
		private OpenDental.UI.Button butPrint;
		private List<Appointment> ListASAP;
		private PrintDocument pd;
		private bool headingPrinted;
		private int pagesPrinted;
		///<summary>Only used if PinClicked=true</summary>
		public long AptSelected;
		private ComboBox comboProv;
		private Label label4;
		private OpenDental.UI.Button butRefresh;
		private ComboBox comboSite;
		private Label labelSite;
		///<summary>When this form closes, this will be the patNum of the last patient viewed.  The calling form should then make use of this to refresh to that patient.  If 0, then calling form should not refresh.</summary>
		public long SelectedPatNum;
		private ComboBox comboClinic;
		private Label labelClinic;
		private Dictionary<long,string> _dictPatientNames;
		private List<Clinic> _listUserClinics;
		private ToolStripMenuItem toolStripMenuItemSelectPatient;
		private ToolStripMenuItem toolStripMenuItemSeeChart;
		private ToolStripMenuItem toolStripMenuItemSendToPinboard;
		private ToolStripMenuItem toolStripMenuItemRemoveFromASAP;
		private ContextMenuStrip menuRightClick;

		///<summary></summary>
		public FormASAP() {
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormASAP));
			this.butClose = new OpenDental.UI.Button();
			this.grid = new OpenDental.UI.ODGrid();
			this.menuRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSelectPatient = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSeeChart = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSendToPinboard = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRemoveFromASAP = new System.Windows.Forms.ToolStripMenuItem();
			this.butPrint = new OpenDental.UI.Button();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.comboSite = new System.Windows.Forms.ComboBox();
			this.labelSite = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.menuRightClick.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(761, 631);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(87, 24);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// grid
			// 
			this.grid.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.grid.ContextMenuStrip = this.menuRightClick;
			this.grid.HasAddButton = false;
			this.grid.HasDropDowns = false;
			this.grid.HasMultilineHeaders = false;
			this.grid.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.grid.HeaderHeight = 15;
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(10, 57);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.Size = new System.Drawing.Size(734, 598);
			this.grid.TabIndex = 8;
			this.grid.Title = "ASAP List";
			this.grid.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.grid.TitleHeight = 18;
			this.grid.TranslationName = "TableASAP";
			this.grid.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid_CellDoubleClick);
			// 
			// _menuRightClick
			// 
			this.menuRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSelectPatient,
            this.toolStripMenuItemSeeChart,
            this.toolStripMenuItemSendToPinboard,
            this.toolStripMenuItemRemoveFromASAP});
			this.menuRightClick.Name = "_menuRightClick";
			this.menuRightClick.Size = new System.Drawing.Size(179, 92);
			// 
			// selectPatientToolStripMenuItem
			// 
			this.toolStripMenuItemSelectPatient.Name = "selectPatientToolStripMenuItem";
			this.toolStripMenuItemSelectPatient.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemSelectPatient.Text = "Select Patient";
			this.toolStripMenuItemSelectPatient.Click += new System.EventHandler(this.menuRight_click);
			// 
			// seeChartToolStripMenuItem
			// 
			this.toolStripMenuItemSeeChart.Name = "seeChartToolStripMenuItem";
			this.toolStripMenuItemSeeChart.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemSeeChart.Text = "See Chart";
			this.toolStripMenuItemSeeChart.Click += new System.EventHandler(this.menuRight_click);
			// 
			// sendToPinboardToolStripMenuItem
			// 
			this.toolStripMenuItemSendToPinboard.Name = "sendToPinboardToolStripMenuItem";
			this.toolStripMenuItemSendToPinboard.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemSendToPinboard.Text = "Send to Pinboard";
			this.toolStripMenuItemSendToPinboard.Click += new System.EventHandler(this.menuRight_click);
			// 
			// removeFromASAPToolStripMenuItem
			// 
			this.toolStripMenuItemRemoveFromASAP.Name = "removeFromASAPToolStripMenuItem";
			this.toolStripMenuItemRemoveFromASAP.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemRemoveFromASAP.Text = "Remove from ASAP";
			this.toolStripMenuItemRemoveFromASAP.Click += new System.EventHandler(this.menuRight_click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(761, 583);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87, 24);
			this.butPrint.TabIndex = 21;
			this.butPrint.Text = "Print List";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(319, 7);
			this.comboProv.MaxDropDownItems = 40;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(181, 21);
			this.comboProv.TabIndex = 33;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(248, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 14);
			this.label4.TabIndex = 32;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(762, 6);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(86, 24);
			this.butRefresh.TabIndex = 31;
			this.butRefresh.Text = "&Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// comboSite
			// 
			this.comboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSite.Location = new System.Drawing.Point(584, 32);
			this.comboSite.MaxDropDownItems = 40;
			this.comboSite.Name = "comboSite";
			this.comboSite.Size = new System.Drawing.Size(160, 21);
			this.comboSite.TabIndex = 37;
			// 
			// labelSite
			// 
			this.labelSite.Location = new System.Drawing.Point(512, 36);
			this.labelSite.Name = "labelSite";
			this.labelSite.Size = new System.Drawing.Size(71, 14);
			this.labelSite.TabIndex = 36;
			this.labelSite.Text = "Site";
			this.labelSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(584, 7);
			this.comboClinic.MaxDropDownItems = 40;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(160, 21);
			this.comboClinic.TabIndex = 39;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(506, 11);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(77, 14);
			this.labelClinic.TabIndex = 38;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormASAP
			// 
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(858, 672);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.comboSite);
			this.Controls.Add(this.labelSite);
			this.Controls.Add(this.comboProv);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormASAP";
			this.Text = "ASAP List";
			this.Load += new System.EventHandler(this.FormASAP_Load);
			this.menuRightClick.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormASAP_Load(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			/*comboOrder.Items.Add(Lan.g(this,"Status"));
			comboOrder.Items.Add(Lan.g(this,"Alphabetical"));
			comboOrder.Items.Add(Lan.g(this,"Date"));
			comboOrder.SelectedIndex=0;*/
			comboProv.Items.Add(Lan.g(this,"All"));
			comboProv.SelectedIndex=0;
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProv.Items.Add(ProviderC.ListShort[i].GetLongDesc());
			}
			if(PrefC.GetBool(PrefName.EasyHidePublicHealth)){
				comboSite.Visible=false;
				labelSite.Visible=false;
			}
			else{
				comboSite.Items.Add(Lan.g(this,"All"));
				comboSite.SelectedIndex=0;
				for(int i=0;i<Sites.ListShallow.Count;i++) {
					comboSite.Items.Add(Sites.ListShallow[i].Description);
				}
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			else {
				if(!Security.CurUser.ClinicIsRestricted) {
					comboClinic.Items.Add(Lan.g(this,"All"));
					comboClinic.SelectedIndex=0;
				}
				_listUserClinics=Clinics.GetForUserod(Security.CurUser);
				for(int i=0;i<_listUserClinics.Count;i++) {
					comboClinic.Items.Add(_listUserClinics[i].Abbr);
					if(_listUserClinics[i].ClinicNum==Clinics.ClinicNum) {
						comboClinic.SelectedIndex=i;
						if(!Security.CurUser.ClinicIsRestricted) {
							comboClinic.SelectedIndex++;//add 1 for "All"
						}
					}
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void menuRight_click(object sender,System.EventArgs e) {
			if(grid.SelectedIndices.Length<=0) {
				return;//Preserving old functionality where the context menu would not act unless something was selected.
			}
			switch(menuRightClick.Items.IndexOf((ToolStripMenuItem)sender)) {
				case 0:
					SelectPatient_Click();
					break;
				case 1:
					SeeChart_Click();
					break;
				case 2:
					SendPinboard_Click();
					break;
				case 3:
					Remove_Click();
					break;
			}
		}

		private void SelectPatient_Click() {
			Patient pat=Patients.GetPat(ListASAP[grid.SelectedIndices[grid.SelectedIndices.Length-1]].PatNum);//If multiple selected, just take the last one to remain consistent with SendPinboard_Click.
			FormOpenDental.S_Contr_PatientSelected(pat,true);
		}

		private void FillGrid(){
			this.Cursor=Cursors.WaitCursor;
			/*string order="";
			switch(comboOrder.SelectedIndex) {
				case 0:
					order="status";
					break;
				case 1:
					order="alph";
					break;
				case 2:
					order="date";
					break;
			}*/
			long provNum=0;
			if(comboProv.SelectedIndex!=0) {
				provNum=ProviderC.ListShort[comboProv.SelectedIndex-1].ProvNum;
			}
			long siteNum=0;
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth) && comboSite.SelectedIndex!=0) {
				siteNum=Sites.ListShallow[comboSite.SelectedIndex-1].SiteNum;
			}
			long clinicNum=0;
			//if clinics are not enabled, comboClinic.SelectedIndex will be -1, so clinicNum will be 0 and list will not be filtered by clinic
			if(Security.CurUser.ClinicIsRestricted && comboClinic.SelectedIndex>-1) {
				clinicNum=_listUserClinics[comboClinic.SelectedIndex].ClinicNum;
			}
			else if(comboClinic.SelectedIndex > 0) {//if user is not restricted, clinicNum will be 0 and the query will get all clinic data
				clinicNum=_listUserClinics[comboClinic.SelectedIndex-1].ClinicNum;//if user is not restricted, comboClinic will contain "All" so minus 1
			}
			ListASAP=Appointments.RefreshASAP(provNum,siteNum,clinicNum);
			int scrollVal=grid.ScrollValue;
			grid.BeginUpdate();
			grid.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableASAP","Patient"),140);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableASAP","Date"),65);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableASAP","Status"),110);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableASAP","Prov"),50);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableASAP","Procedures"),150);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableASAP","Notes"),200);
			grid.Columns.Add(col);
			grid.Rows.Clear();
			ODGridRow row;
			_dictPatientNames=Patients.GetPatientNames(ListASAP.Select(x => x.PatNum).ToList());
			List<Provider> listProvs=ProviderC.GetListLong();
			List<Def> listDefCats=DefC.GetListLong(DefCat.RecallUnschedStatus);
			for(int i=0;i<ListASAP.Count;i++) {
				row=new ODGridRow();
				string patName=Lan.g(this,"UNKNOWN");
				_dictPatientNames.TryGetValue(ListASAP[i].PatNum,out patName);
				row.Cells.Add(patName);
				if(ListASAP[i].AptDateTime.Year < 1880){//shouldn't be possible.
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(ListASAP[i].AptDateTime.ToShortDateString());
				}
				row.Cells.Add(DefC.GetName(ListASAP[i].UnschedStatus,listDefCats));
				if(ListASAP[i].IsHygiene) {
					row.Cells.Add(Providers.GetAbbr(ListASAP[i].ProvHyg,listProvs));
				}
				else {
					row.Cells.Add(Providers.GetAbbr(ListASAP[i].ProvNum,listProvs));
				}
				row.Cells.Add(ListASAP[i].ProcDescript);
				row.Cells.Add(ListASAP[i].Note);
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
			grid.ScrollValue=scrollVal;
			Cursor=Cursors.Default;
		}

		///<summary>If multiple patients are selected in the list, it will use the last patient to show the chart for.</summary>
		private void SeeChart_Click() {
			if(grid.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			Patient pat=Patients.GetPat(ListASAP[grid.SelectedIndices[grid.SelectedIndices.Length-1]].PatNum); //If multiple selected, just use the last one.
			FormOpenDental.S_Contr_PatientSelected(pat,false); //Selects the patient in OpenDental.
			GotoModule.GotoChart(pat.PatNum);
		}

		private void Remove_Click() {
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)) {
				return;
			}
			if(grid.SelectedIndices.Length>1 && !MsgBox.Show(this,MsgBoxButtons.OKCancel,"Change status to Scheduled for all selected appointments?")) {
				return;
			}
			for(int i=0;i<grid.SelectedIndices.Length;i++) {
				Appointment apt=ListASAP[grid.SelectedIndices[i]];
				Appointments.SetAptStatus(apt.AptNum,ApptStatus.Scheduled);
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,apt.PatNum,"Appointment status set from ASAP to Scheduled from ASAP list.",apt.AptNum);
			}
			FillGrid();
		}

		private void SendPinboard_Click() {
			if(grid.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			List<long> listAptSelected=new List<long>();
			for(int i=0;i<grid.SelectedIndices.Length;i++) {
				listAptSelected.Add(ListASAP[grid.SelectedIndices[i]].AptNum);
			}
			GotoModule.PinToAppt(listAptSelected,0); //Pins all appointments to the pinboard that were in listAptSelected.
		}

		private void grid_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int currentSelection=e.Row;
			int currentScroll=grid.ScrollValue;
			SelectedPatNum=ListASAP[e.Row].PatNum;
			Patient pat=Patients.GetPat(SelectedPatNum);
			FormOpenDental.S_Contr_PatientSelected(pat,true);
			FormApptEdit FormAE=new FormApptEdit(ListASAP[e.Row].AptNum);
			FormAE.PinIsVisible=true;
			FormAE.ShowDialog();
			if(FormAE.DialogResult!=DialogResult.OK){
				return;
			}
			if(FormAE.PinClicked) {
				SendPinboard_Click(); //Whatever they double clicked on will still be selected, just fire the event to send it to the pinboard.
				DialogResult=DialogResult.OK;
			}
			else {
				FillGrid();
				grid.SetSelected(currentSelection,true);
				grid.ScrollValue=currentScroll;
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			#if DEBUG
				FormRpPrintPreview pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=pd;
				pView.ShowDialog();
			#else
				if(!PrinterL.SetPrinter(pd,PrintSituation.Default,0,"ASAP list printed")) {
					return;
				}
				try{
					pd.Print();
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			#endif			
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int y=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			int headingPrintH=0;
			if(!headingPrinted) {
				text=Lan.g(this,"ASAP List");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,y);
				//yPos+=(int)g.MeasureString(text,headingFont).Height;
				//text=textDateFrom.Text+" "+Lan.g(this,"to")+" "+textDateTo.Text;
				//g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				y+=25;
				headingPrinted=true;
				headingPrintH=y;
			}
			#endregion
			y=grid.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(y==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	}
}
