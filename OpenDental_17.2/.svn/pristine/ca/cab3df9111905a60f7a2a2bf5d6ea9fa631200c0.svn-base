using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormDefinitions : ODForm {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textGuide;
		private System.Windows.Forms.GroupBox groupEdit;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butHide;
		private UI.ODGrid gridDefs;
		private DefCat _initialCat;
		private bool _isDefChanged;
		private List<Def> _listDefsAll;

		///<summary>Gets the currently selected DefCat along with its options.</summary>
		private DefCatOptions _selectedDefCatOpt {
			get { return ((ODBoxItem<DefCatOptions>)listCategory.SelectedItem).Tag; }
		}

		///<summary>All definitions for the current category, hidden and non-hidden.</summary>
		private List<Def> _listDefsCur {
			get { return _listDefsAll.Where(x => x.Category == _selectedDefCatOpt.DefCat).OrderBy(x => x.ItemOrder).ToList(); }
		}

		///<summary>Must check security before allowing this window to open.</summary>
		public FormDefinitions(DefCat initialCat){
			InitializeComponent();// Required for Windows Form Designer support
			_initialCat=initialCat;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDefinitions));
			this.butClose = new OpenDental.UI.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textGuide = new System.Windows.Forms.TextBox();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.butHide = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.gridDefs = new OpenDental.UI.ODGrid();
			this.groupEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(700, 638);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(78, 602);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 18);
			this.label14.TabIndex = 22;
			this.label14.Text = "Guidelines";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGuide
			// 
			this.textGuide.Location = new System.Drawing.Point(184, 602);
			this.textGuide.Multiline = true;
			this.textGuide.Name = "textGuide";
			this.textGuide.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textGuide.Size = new System.Drawing.Size(488, 63);
			this.textGuide.TabIndex = 2;
			// 
			// groupEdit
			// 
			this.groupEdit.Controls.Add(this.butHide);
			this.groupEdit.Controls.Add(this.butDown);
			this.groupEdit.Controls.Add(this.butUp);
			this.groupEdit.Controls.Add(this.butAdd);
			this.groupEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEdit.Location = new System.Drawing.Point(184, 547);
			this.groupEdit.Name = "groupEdit";
			this.groupEdit.Size = new System.Drawing.Size(488, 51);
			this.groupEdit.TabIndex = 1;
			this.groupEdit.TabStop = false;
			this.groupEdit.Text = "Edit Items";
			// 
			// butHide
			// 
			this.butHide.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHide.Autosize = true;
			this.butHide.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHide.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHide.CornerRadius = 4F;
			this.butHide.Location = new System.Drawing.Point(152, 19);
			this.butHide.Name = "butHide";
			this.butHide.Size = new System.Drawing.Size(79, 24);
			this.butHide.TabIndex = 10;
			this.butHide.Text = "&Hide";
			this.butHide.Click += new System.EventHandler(this.butHide_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(360, 19);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 24);
			this.butDown.TabIndex = 9;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(256, 19);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 24);
			this.butUp.TabIndex = 8;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(48, 19);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 24);
			this.butAdd.TabIndex = 6;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(11, 30);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(167, 511);
			this.listCategory.TabIndex = 0;
			this.listCategory.SelectedIndexChanged += new System.EventHandler(this.listCategory_SelectedIndexChanged);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(11, 12);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(162, 17);
			this.label13.TabIndex = 17;
			this.label13.Text = "Select Category:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridDefs
			// 
			this.gridDefs.CellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
			this.gridDefs.HasAddButton = false;
			this.gridDefs.HasDropDowns = false;
			this.gridDefs.HasMultilineHeaders = false;
			this.gridDefs.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
			this.gridDefs.HeaderHeight = 15;
			this.gridDefs.HScrollVisible = false;
			this.gridDefs.Location = new System.Drawing.Point(184, 30);
			this.gridDefs.Name = "gridDefs";
			this.gridDefs.ScrollValue = 0;
			this.gridDefs.Size = new System.Drawing.Size(488, 511);
			this.gridDefs.TabIndex = 23;
			this.gridDefs.Title = "Definitions";
			this.gridDefs.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.gridDefs.TitleHeight = 18;
			this.gridDefs.TranslationName = null;
			this.gridDefs.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDefs_CellDoubleClick);
			// 
			// FormDefinitions
			// 
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(789, 675);
			this.Controls.Add(this.gridDefs);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textGuide);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label13);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefinitions";
			this.ShowInTaskbar = false;
			this.Text = "Definitions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDefinitions_Closing);
			this.Load += new System.EventHandler(this.FormDefinitions_Load);
			this.groupEdit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDefinitions_Load(object sender, System.EventArgs e) {
			/*if(PermissionsOld.AuthorizationRequired("Definitions")){
				user=Users.Authenticate("Definitions");
				if(!UserPermissions.IsAuthorized("Definitions",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			LoadListDefCats();
		}

		private void LoadListDefCats() {
			List<DefCatOptions> listDefCatsOrdered = new List<DefCatOptions>();
			DefCatOptions defCOption;
			foreach(DefCat defCatCur in Enum.GetValues(typeof(DefCat))) {
				if(defCatCur.GetDescription() == "NotUsed") {
					continue;
				}
				defCOption=new DefCatOptions(defCatCur);
				switch(defCatCur) {
					case DefCat.AccountColors:
						defCOption.CanEditName=false;
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"Changes the color of text for different types of entries in Account Module");
						break;
					case DefCat.AccountQuickCharge:
						defCOption.CanDelete=true;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"Procedure Codes");
						defCOption.HelpText=Lan.g(this,"Account Proc Quick Add items.  Each entry can be a series of procedure codes separated by commas (e.g. D0180,D1101,D8220).  Used in the account module to quickly charge patients for items.");
						break;
					case DefCat.AdjTypes:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"+, -, or dp");
						defCOption.HelpText=Lan.g(this,"Plus increases the patient balance.  Minus decreases it.  Dp means discount plan.  Not allowed to change value after creating new type since changes affect all patient accounts.");
						break;
					case DefCat.AppointmentColors:
						defCOption.CanEditName=false;
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"Changes colors of background in Appointments Module, and colors for completed appointments.");
						break;
					case DefCat.ApptConfirmed:
						defCOption.EnableColor=true;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"Abbrev");
						defCOption.HelpText=Lan.g(this,"Color shows on each appointment if Appointment View is set to show ConfirmedColor.");
						break;
					case DefCat.ApptProcsQuickAdd:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"ADA Code(s)");
						if(Clinics.IsMedicalPracticeOrClinic(Clinics.ClinicNum)) {
							defCOption.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
						}
						else {
							defCOption.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  They must not require a tooth number. Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
						}
						break;
					case DefCat.AutoNoteCats:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.EnableValue=true;
						defCOption.IsValueDefNum=true;
						defCOption.ValueText=Lan.g(this,"Parent Category");
						defCOption.HelpText=Lan.g(this,"Leave the Parent Category blank for categories at the root level. Assign a Parent Category to move a category within another. The order set here will only affect the order within the assigned Parent Category in the Auto Note list. For example, a category may be moved above its parent in this list, but it will still be within its Parent Category in the Auto Note list.");
						break;
					case DefCat.BillingTypes:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"E=Email bill");
						defCOption.HelpText=Lan.g(this,"It is recommended to use as few billing types as possible.  They can be useful when running reports to separate delinquent accounts, but can cause 'forgotten accounts' if used without good office procedures. Changes affect all patients.");
						break;
					case DefCat.BlockoutTypes:
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"Blockout types are used in the appointments module.");
						break;
					case DefCat.ChartGraphicColors:
						defCOption.CanEditName=false;
						defCOption.EnableColor=true;
						if(Clinics.IsMedicalPracticeOrClinic(Clinics.ClinicNum)) {
							defCOption.HelpText=Lan.g(this,"These colors will be used to graphically display treatments.");
						}
						else {
							defCOption.HelpText=Lan.g(this,"These colors will be used on the graphical tooth chart to draw restorations.");
						}
						break;
					case DefCat.ClaimCustomTracking:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"Days Suppressed");
						defCOption.HelpText=Lan.g(this,"Some offices may set up claim tracking statuses such as 'review', 'hold', 'riskmanage', etc.")+"\r\n"
							+Lan.g(this,"Set the value of 'Days Suppressed' to the number of days the claim will be suppressed from the Outstanding Claims Report "
							+"when the status is changed to the selected status.");
						break;
					case DefCat.ClaimErrorCode:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"Description");
						defCOption.HelpText=Lan.g(this,"Used to track error codes when entering claim custom statuses.");
						break;
					case DefCat.ClaimPaymentTracking:
						defCOption.ValueText=Lan.g(this,"Value");
						defCOption.HelpText=Lan.g(this,"EOB adjudication method codes to be used for insurance payments.  Last entry cannot be hidden.");
						break;
					case DefCat.ClaimPaymentGroups:
						defCOption.ValueText=Lan.g(this,"Value");
						defCOption.HelpText=Lan.g(this,"Used to group claim payments in the daily payments report.");
						break;
					case DefCat.ClinicSpecialty:
						defCOption.CanHide=true;
						defCOption.CanDelete=false;
						defCOption.HelpText=Lan.g(this,"You can add as many specialties as you want.  Changes affect all current records.");
						break;
					case DefCat.CommLogTypes:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"APPT,FIN,RECALL,MISC,TEXT");
						defCOption.HelpText=Lan.g(this,"Changes affect all current commlog entries.  In the second column, you can optionally specify APPT,FIN,RECALL,MISC,or TEXT. Only one of each. This helps automate new entries.");
						break;
					case DefCat.ContactCategories:
						defCOption.HelpText=Lan.g(this,"You can add as many categories as you want.  Changes affect all current contact records.");
						break;
					case DefCat.Diagnosis:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"1 or 2 letter abbreviation");
						defCOption.HelpText=Lan.g(this,"The diagnosis list is shown when entering a procedure.  Ones that are less used should go lower on the list.  The abbreviation is shown in the progress notes.  BE VERY CAREFUL.  Changes affect all patients.");
						break;
					case DefCat.FeeColors:
						defCOption.CanEditName=false;
						defCOption.CanHide=false;
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"These are the colors associated to fee types.");
						break;
					case DefCat.ImageCats:
						defCOption.ValueText=Lan.g(this,"Usage");
						defCOption.HelpText=Lan.g(this,"These are the categories that will be available in the image and chart modules.  If you hide a category, images in that category will be hidden, so only hide a category if you are certain it has never been used.  Multiple categories can be set to show in the Chart module, but only one category should be set for patient pictures, statements, and tooth charts. Selecting multiple categories for treatment plans will save the treatment plan in each category. Affects all patient records.");
						break;
					case DefCat.InsurancePaymentType:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"N=Not selected for deposit");
						defCOption.HelpText=Lan.g(this,"These are claim payment types for insurance payments attached to claims.");
						break;
					case DefCat.InsuranceVerificationStatus:
						defCOption.HelpText=Lan.g(this,"These are statuses for the insurance verification list.");
						break;
					case DefCat.LetterMergeCats:
						defCOption.HelpText=Lan.g(this,"Categories for Letter Merge.  You can safely make any changes you want.");
						break;
					case DefCat.MiscColors:
						defCOption.CanEditName=false;
						defCOption.EnableColor=true;
						defCOption.HelpText="";
						break;
					case DefCat.PaymentTypes:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"N=Not selected for deposit");
						defCOption.HelpText=Lan.g(this,"Types of payments that patients might make. Any changes will affect all patients.");
						break;
					case DefCat.PaySplitUnearnedType:
						defCOption.HelpText=Lan.g(this,"Usually only used by offices that use accrual basis accounting instead of cash basis accounting. Any changes will affect all patients.");
						break;
					case DefCat.ProcButtonCats:
						defCOption.HelpText=Lan.g(this,"These are similar to the procedure code categories, but are only used for organizing and grouping the procedure buttons in the Chart module.");
						break;
					case DefCat.ProcCodeCats:
						defCOption.HelpText=Lan.g(this,"These are the categories for organizing procedure codes. They do not have to follow ADA categories.  There is no relationship to insurance categories which are setup in the Ins Categories section.  Does not affect any patient records.");
						break;
					case DefCat.ProgNoteColors:
						defCOption.CanEditName=false;
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"Changes color of text for different types of entries in the Chart Module Progress Notes.");
						break;
					case DefCat.Prognosis:
						//Nothing special. Might add HelpText later.
						break;
					case DefCat.ProviderSpecialties:
						defCOption.HelpText=Lan.g(this,"Provider specialties cannot be deleted.  Changes to provider specialties could affect e-claims.");
						break;
					case DefCat.RecallUnschedStatus:
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"Abbreviation");
						defCOption.HelpText=Lan.g(this,"Recall/Unsched Status.  Abbreviation must be 7 characters or less.  Changes affect all patients.");
						break;
					case DefCat.Regions:
						defCOption.CanHide=false;
						defCOption.HelpText=Lan.g(this,"The region identifying the clinic it is assigned to.");
						break;
					case DefCat.SupplyCats:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.HelpText=Lan.g(this,"The categories for inventory supplies.");
						break;
					case DefCat.TaskPriorities:
						defCOption.EnableColor=true;
						defCOption.EnableValue=true;
						defCOption.ValueText=Lan.g(this,"D = Default, R = Reminder");
						defCOption.HelpText=Lan.g(this,"Priorities available for selection within the task edit window.  Task lists are sorted using the order of these priorities.  They can have any description and color.  At least one priority should be Default (D).  If more than one priority is flagged as the default, the last default in the list will be used.  If no default is set, the last priority will be used.  Use (R) to indicate the initial reminder task priority to use when creating reminder tasks.  Changes affect all tasks where the definition is used.");
						break;
					case DefCat.TxPriorities:
						defCOption.EnableColor=true;
						defCOption.HelpText=Lan.g(this,"Priorities available for selection in the Treatment Plan module.  They can be simple numbers or descriptive abbreviations 7 letters or less.  Changes affect all procedures where the definition is used.");
						break;
					case DefCat.WebSchedNewPatApptTypes:
						defCOption.CanDelete=true;
						defCOption.CanHide=false;
						defCOption.HelpText=Lan.g(this,"Appointment types to be displayed in the Web Sched New Pat Appt web application.  These are selectable for the new patients and will be saved to the appointment note.");
						break;
				}
				listDefCatsOrdered.Add(defCOption);
			}
			listDefCatsOrdered = listDefCatsOrdered.OrderBy(x => x.DefCat.GetDescription()).ToList(); //orders alphabetically.
			ODBoxItem<DefCatOptions> defCatItem;
			foreach(DefCatOptions defCOpt in listDefCatsOrdered) {
				defCatItem=new ODBoxItem<DefCatOptions>(Lan.g(this,defCOpt.DefCat.GetDescription()),defCOpt);
				listCategory.Items.Add(defCatItem);
				if(_initialCat == defCOpt.DefCat) {
					listCategory.SelectedItem=defCatItem;
				}
			}
		}

		private void listCategory_SelectedIndexChanged(object sender,System.EventArgs e) {
			FillGridDefs();
		}

		private void RefreshDefs() {
			Defs.RefreshCache();
			_listDefsAll=DefC.GetListAll();
		}

		private void FillGridDefs(){
			Def selectedDef=null;
			if(gridDefs.GetSelectedIndex() > -1) {
				selectedDef=(Def)gridDefs.Rows[gridDefs.GetSelectedIndex()].Tag;
			}
			int scroll=gridDefs.ScrollValue;
			gridDefs.BeginUpdate();
			gridDefs.Columns.Clear();
			ODGridColumn col;
			col = new ODGridColumn(Lan.g("TableDefs","Name"),190);
			gridDefs.Columns.Add(col);
			col = new ODGridColumn(_selectedDefCatOpt.ValueText,190);
			gridDefs.Columns.Add(col);
			col = new ODGridColumn(_selectedDefCatOpt.EnableColor ? Lan.g("TableDefs","Color") : "",40);
			gridDefs.Columns.Add(col);
			col = new ODGridColumn(_selectedDefCatOpt.CanHide ? Lan.g("TableDefs","Hide") : "",30,HorizontalAlignment.Center);
			gridDefs.Columns.Add(col);
			gridDefs.Rows.Clear();
			ODGridRow row;
			if(_listDefsAll == null || _listDefsAll.Count == 0) {
				RefreshDefs();
			}
			foreach(Def defCur in _listDefsCur) {
				if(DefC.IsDefDeprecated(defCur)) {
					defCur.IsHidden=true;
				}
				row=new ODGridRow();
				if(_selectedDefCatOpt.CanEditName) {
					row.Cells.Add(defCur.ItemName);
				}
				else {//Users cannot edit the item name so let them translate them.
					row.Cells.Add(Lan.g("FormDefinitions",defCur.ItemName));//Doesn't use 'this' so that renaming the form doesn't change the translation
				}
				if(_selectedDefCatOpt.DefCat==DefCat.ImageCats) {
					row.Cells.Add(GetItemDescForImages(defCur.ItemValue));
				}
				else if(_selectedDefCatOpt.DefCat==DefCat.AutoNoteCats) {
					Dictionary<string,string> dictAutoNoteDefs = new Dictionary<string,string>();
					dictAutoNoteDefs=_listDefsCur.ToDictionary(x => x.DefNum.ToString(),x => x.ItemName);
					string nameCur;
					row.Cells.Add(dictAutoNoteDefs.TryGetValue(defCur.ItemValue,out nameCur) ? nameCur : defCur.ItemValue);
				}
				else {
					row.Cells.Add(defCur.ItemValue);
				}
				row.Cells.Add("");
				if(_selectedDefCatOpt.EnableColor) {
					row.Cells[row.Cells.Count-1].CellColor=defCur.ItemColor;
				}
				if(defCur.IsHidden) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				row.Tag=defCur;
				gridDefs.Rows.Add(row);
			}
			gridDefs.EndUpdate();
			//the following do not require a refresh of the table:
			if(_selectedDefCatOpt.CanHide) {
				butHide.Visible=true;
			}
			else {
				butHide.Visible=false;
			}
			if(_selectedDefCatOpt.CanEditName) {
				groupEdit.Enabled=true;
				groupEdit.Text="Edit Items";
			}
			else {
				groupEdit.Enabled=false;
				groupEdit.Text="Not allowed";
			}
			textGuide.Text=_selectedDefCatOpt.HelpText;
			if(selectedDef!=null) {
				for(int i = 0;i < gridDefs.Rows.Count;i++) {
					if(((Def)gridDefs.Rows[i].Tag).DefNum == selectedDef.DefNum) {
						gridDefs.SetSelected(i,true);
						break;
					}
				}
			}
			gridDefs.ScrollValue=scroll;
		}

		private string GetItemDescForImages(string itemValue) {
			List<string> listVals=new List<string>();
			if(itemValue.Contains("X")) {
				listVals.Add(Lan.g(this,"ChartModule"));
			}
			if(itemValue.Contains("F")) {
				listVals.Add(Lan.g(this,"PatientForm"));
			}
			if(itemValue.Contains("P")){
				listVals.Add(Lan.g(this,"PatientPic"));
			}
			if(itemValue.Contains("S")){
				listVals.Add(Lan.g(this,"Statement"));
			}
			if(itemValue.Contains("T")){
				listVals.Add(Lan.g(this,"ToothChart"));
			}
			if(itemValue.Contains("R")) {
				listVals.Add(Lan.g(this,"TreatPlans"));
			}
			if(itemValue.Contains("L")) {
				listVals.Add(Lan.g(this,"PatientPortal"));
			}
			if(itemValue.Contains("A")) {
				listVals.Add(Lan.g(this,"PayPlans"));
			}
			return string.Join(", ",listVals);
		}

		private void gridDefs_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Def selectedDef = (Def)gridDefs.Rows[e.Row].Tag;
			if(_selectedDefCatOpt.DefCat==DefCat.ImageCats) {
				FormDefEditImages FormDEI = new FormDefEditImages(selectedDef);
				FormDEI.IsNew=false;
				FormDEI.ShowDialog();
				if(FormDEI.DialogResult==DialogResult.OK) {
					_isDefChanged=true;
					RefreshDefs();
					FillGridDefs();
				}
			}
			else {
				FormDefEdit FormDefEdit2 = new FormDefEdit(selectedDef,_listDefsCur,_selectedDefCatOpt);
				FormDefEdit2.IsNew=false;
				FormDefEdit2.ShowDialog();
				if(FormDefEdit2.DialogResult==DialogResult.OK) {
					if(_selectedDefCatOpt.DefCat==DefCat.AutoNoteCats) {
						RefreshDefs();
					}
					if(FormDefEdit2.IsDeleted) {
						_listDefsAll.Remove(selectedDef);
					}
					_isDefChanged=true;
					FillGridDefs();
				}
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Def DefCur=new Def();
			int itemOrder=0;
			if(DefC.GetListLong(_selectedDefCatOpt.DefCat).Count>0) {
				itemOrder=DefC.GetListLong(_selectedDefCatOpt.DefCat).Max(x => x.ItemOrder) + 1;
			}
			DefCur.ItemOrder=itemOrder;
			DefCur.Category=_selectedDefCatOpt.DefCat;
			DefCur.ItemName="";
			DefCur.ItemValue="";//necessary
			if(_selectedDefCatOpt.DefCat==DefCat.InsurancePaymentType) {
				DefCur.ItemValue="N";
			}
			if(_selectedDefCatOpt.DefCat==DefCat.ImageCats) {
				FormDefEditImages FormDEI=new FormDefEditImages(DefCur);
				FormDEI.IsNew=true;
				FormDEI.ShowDialog();
				if(FormDEI.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else {
				List<Def> listCurrentDefs = new List<Def>();
				foreach(ODGridRow rowCur in gridDefs.Rows) {
					listCurrentDefs.Add((Def)rowCur.Tag);
				}
				FormDefEdit FormDE=new FormDefEdit(DefCur,listCurrentDefs,_selectedDefCatOpt);
				FormDE.IsNew=true;
				FormDE.ShowDialog();
				if(FormDE.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			_isDefChanged=true;
			RefreshDefs();
			FillGridDefs();
		}

		private void butHide_Click(object sender, System.EventArgs e) {
			if(gridDefs.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select item first,");
				return;
			}
			Def selectedDef = (Def)gridDefs.Rows[gridDefs.GetSelectedIndex()].Tag;
			//Warn the user if they are about to hide a billing type currently in use.
			if(_selectedDefCatOpt.DefCat==DefCat.BillingTypes && Patients.IsBillingTypeInUse(selectedDef.DefNum)) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Warning: Billing type is currently in use by patients or insurance plans.")) {
					return;
				}
			}
			if(selectedDef.Category==DefCat.ProviderSpecialties
				&& (Providers.IsSpecialtyInUse(selectedDef.DefNum)
				|| Referrals.IsSpecialtyInUse(selectedDef.DefNum)))
			{
				MsgBox.Show(this,"You cannot hide a specialty if it is in use by a provider or a referral source.");
				return;
			}
			if(Defs.IsDefinitionInUse(selectedDef)) {
				if(selectedDef.DefNum==PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.TreatPlanDiscountAdjustmentType)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.BillingChargeAdjustmentType)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.PracticeDefaultBillType)
					|| selectedDef.DefNum==PrefC.GetLong(PrefName.FinanceChargeAdjustmentType)) 
				{
					MsgBox.Show(this,"You cannot hide a definition if it is in use within Module Preferences.");
					return;
				}
				else {
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Warning: This definition is currently in use within the program.")) {
						return;
					}
				}
			}
			//Stop users from hiding the last definition in categories that must have at least one def in them.
			if(Defs.IsHidable(selectedDef.Category))	{
				List<Def> listDefsCurNotHidden = DefC.GetList(_selectedDefCatOpt.DefCat).ToList();
				if(listDefsCurNotHidden.Count ==1) {
					MsgBox.Show(this,"You cannot hide the last definition in this category.");
					return;
				}
			}
			Defs.HideDef(selectedDef);
			selectedDef.IsHidden=true;
			_isDefChanged=true;
			FillGridDefs();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(gridDefs.GetSelectedIndex()==-1){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(gridDefs.GetSelectedIndex()==0) {
				return;
			}
			Def defSelected = _listDefsCur[gridDefs.GetSelectedIndex()];
			Def defAbove = _listDefsCur[gridDefs.GetSelectedIndex()-1];
			defSelected.ItemOrder--;
			defAbove.ItemOrder++;
			Defs.Update(defSelected);
			Defs.Update(defAbove);
			_isDefChanged=true;
			FillGridDefs();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(gridDefs.GetSelectedIndex()==-1){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(gridDefs.GetSelectedIndex()==gridDefs.Rows.Count-1) {
				return;
			}
			Def defSelected = _listDefsCur[gridDefs.GetSelectedIndex()];
			Def deBelow = _listDefsCur[gridDefs.GetSelectedIndex()+1];
			defSelected.ItemOrder++;
			deBelow.ItemOrder--;
			Defs.Update(defSelected);
			Defs.Update(deBelow);
			_isDefChanged=true;
			FillGridDefs();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormDefinitions_Closing(object sender,System.ComponentModel.CancelEventArgs e) {
			//Correct the item orders of all definition categories.
			List<Def> listDefUpdates = new List<Def>();
			Def[][] arrayAllDefs=DefC.GetArrayLong();//Don't use DefsList because it will only be an array of defs for the selected category.
			for(int i=0;i<arrayAllDefs.Length;i++) {
				for(int j=0;j<arrayAllDefs[i].Length;j++) {
					if(arrayAllDefs[i][j].ItemOrder!=j) {
						arrayAllDefs[i][j].ItemOrder=j;
						listDefUpdates.Add(arrayAllDefs[i][j]);
					}
				}
			}
			listDefUpdates.ForEach(x => Defs.Update(x));
			if(_isDefChanged || listDefUpdates.Count>0) {
				DataValid.SetInvalid(InvalidType.Defs);
			}
		}
	}
}
