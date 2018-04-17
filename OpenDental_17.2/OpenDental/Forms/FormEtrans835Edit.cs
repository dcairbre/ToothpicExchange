using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Linq;

namespace OpenDental {
	public partial class FormEtrans835Edit:ODForm {

		public string TranSetId835;
		///<summary>Must be set before the form is shown.</summary>
		public Etrans EtransCur;
		///<summary>Must be set before the form is shown.  The message text for EtransCur.</summary>
		public string MessageText835;
		private X835 _x835;
		private decimal _claimInsPaidSum;
		private decimal _provAdjAmtSum;
		private static FormEtrans835Edit _form835=null;
		private ContextMenu gridClaimDetailsMenu=new ContextMenu();

		public FormEtrans835Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans835Edit_Load(object sender,EventArgs e) {
			if(_form835!=null && !_form835.IsDisposed) {
				if(!MsgBox.Show(this,true,"Opening another ERA will close the current ERA you already have open.  Continue?")) {
					//Form already exists and user wants to keep current instance.
					TranSetId835=_form835.TranSetId835;
					EtransCur=_form835.EtransCur;
					MessageText835=_form835.MessageText835;
				}
				_form835.Close();//Always close old form and open new form, so the new copy will come to front, since BringToFront() does not always work.
			}
			_form835=this;//Set the static variable to this form because we're always going to show this form even if they're viewing old information.
			List <Etrans835Attach> listAttached=Etrans835Attaches.GetForEtrans(EtransCur.EtransNum);
			_x835=new X835(MessageText835,TranSetId835,listAttached);
			FillAll();
			Menu.MenuItemCollection menuItemCollection=new Menu.MenuItemCollection(gridClaimDetailsMenu);
			menuItemCollection.AddRange(new MenuItem[] { new MenuItem(Lan.g(this,"Go to Account"),new EventHandler(gridClaimDetails_RightClickHelper)) });
			gridClaimDetailsMenu.Popup+=new EventHandler((o,ea) => {
				int index=gridClaimDetails.GetSelectedIndex();
				bool isGoToAccountEnabled=false;
				if(index!=-1 && gridClaimDetails.SelectedIndices.Count()==1) {
					Hx835_Claim claimPaid=(Hx835_Claim)gridClaimDetails.Rows[index].Tag;
					if(claimPaid.IsAttachedToClaim && claimPaid.ClaimNum!=0) {
						isGoToAccountEnabled=true;
					}
				}
				gridClaimDetailsMenu.MenuItems[0].Enabled=isGoToAccountEnabled;
			});
			gridClaimDetails.ContextMenu=gridClaimDetailsMenu;
			gridClaimDetails.AllowSortingByColumn=true;
		}

		private void FormEtrans835Edit_Resize(object sender,EventArgs e) {
			//This funciton is called before FormEtrans835Edit_Load() when using ShowDialog(). Therefore, x835 is null the first time FormEtrans835Edit_Resize() is called.
			if(_x835==null) {
				return;
			}
			gridProviderAdjustments.Width=butOK.Right-gridProviderAdjustments.Left;
			FillProviderAdjustmentDetails();//Because the grid columns change size depending on the form size.
			gridClaimDetails.Width=gridProviderAdjustments.Width;
			gridClaimDetails.Height=labelPaymentAmount.Top-5-gridClaimDetails.Top;
			FillClaimDetails();//Because the grid columns change size depending on the form size.
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information in this form.</summary>
		private void FillAll() {
			//*835 has 3 parts: Table 1 (header), Table 2 (claim level details, one CLP segment for each claim), and Table 3 (PLB: provider/check level details).
			FillHeader();//Table 1
			FillClaimDetails();//Table 2
			FillProviderAdjustmentDetails();//Table 3
			FillFooter();
			//The following concepts should each be addressed as development progresses.
			//*837 CLM01 -> 835 CLP01 (even for split claims)
			//*Advance payments (pg. 23): in PLB segment with adjustment reason code PI.  Can be yearly or monthly.  We need to find a way to pull provider level adjustments into a deposit.
			//*Bundled procs (pg. 27): have the original proc listed in SV06. Use Line Item Control Number to identify the original proc line.
			//*Predetermination (pg. 28): Identified by claim status code 25 in CLP02. Claim adjustment reason code is 101.
			//*Claim reversals (pg. 30): Identified by code 22 in CLP02. The original claim adjustment codes can be found in CAS01 to negate the original claim.
			//Use CLP07 to identify the original claim, or if different, get the original ref num from REF02 of 2040REF*F8.
			//*Interest and Prompt Payment Discounts (pg. 31): Located in AMT segments with qualifiers I (interest) and D8 (discount). Found at claim and provider/check level.
			//Not part of AR, but part of deposit. Handle this situation by using claimprocs with 2 new status, one for interest and one for discount? Would allow reports, deposits, and claim checks to work as is.
			//*Capitation and related payments or adjustments (pg. 34 & 52): Not many of our customers use capitation, so this will probably be our last concern.
			//*Claim splits (pg. 36): MIA or MOA segments will exist to indicate the claim was split.
			//*Service Line Splits (pg. 42): LQ segment with LQ01=HE and LQ02=N123 indicate the procedure was split.
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 1 (Header).</summary>
		private void FillHeader() {
			//Payer information
			textPayerName.Text=_x835.PayerName;
			textPayerID.Text=_x835.PayerId;
			textPayerAddress1.Text=_x835.PayerAddress;
			textPayerCity.Text=_x835.PayerCity;
			textPayerState.Text=_x835.PayerState;
			textPayerZip.Text=_x835.PayerZip;
			textPayerContactInfo.Text=_x835.PayerContactInfo;
			//Payee information
			textPayeeName.Text=_x835.PayeeName;
			labelPayeeIdType.Text=Lan.g(this,"Payee")+" "+_x835.PayeeIdType;
			textPayeeID.Text=_x835.PayeeId;
			//Payment information
			textTransHandlingDesc.Text=_x835.TransactionHandlingDescript;
			textPaymentMethod.Text=_x835.PayMethodDescript;
			if(_x835.IsCredit) {
				textPaymentAmount.Text=_x835.InsPaid.ToString("f2");
			}
			else {
				textPaymentAmount.Text="-"+_x835.InsPaid.ToString("f2");
			}
			textAcctNumEndingIn.Text=_x835.AccountNumReceiving;
			if(_x835.DateEffective.Year>1880) {
				textDateEffective.Text=_x835.DateEffective.ToShortDateString();
			}
			textCheckNumOrRefNum.Text=_x835.TransRefNum;
			textNote.Text=EtransCur.Note;
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 2 (Detail).</summary>
		private void FillClaimDetails() {
			const int colWidthRecd=32;
			const int colWidthName=250;
			const int colWidthDateService=80;
			const int colWidthClaimId=86;
			const int colWidthPayorControlNum=108;
			const int colWidthClaimAmt=80;
			const int colWidthPaidAmt=80;
			const int colWidthPatAmt=80;
			int colWidthVariable=gridClaimDetails.Width-colWidthRecd-colWidthName-colWidthDateService-colWidthClaimId-colWidthPayorControlNum-colWidthClaimAmt-colWidthPaidAmt-colWidthPatAmt;
			gridClaimDetails.BeginUpdate();
			gridClaimDetails.Columns.Clear();
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"Recd"),colWidthRecd,HorizontalAlignment.Center));
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"Patient"),colWidthName,HorizontalAlignment.Left));
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"DateService"),colWidthDateService,HorizontalAlignment.Center));
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"ClaimIdentifier"),colWidthClaimId,HorizontalAlignment.Left));
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"PayorControlNum"),colWidthPayorControlNum,HorizontalAlignment.Center));//Payer Claim Control Number (CLP07)
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"Status"),colWidthVariable,HorizontalAlignment.Left));//Claim Status Code Description (CLP02)
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"ClaimFee"),colWidthClaimAmt,HorizontalAlignment.Right));//Total Claim Charge Amount (CLP03)
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"InsPaid"),colWidthPaidAmt,HorizontalAlignment.Right));//Claim Payment Amount (CLP04)
			gridClaimDetails.Columns.Add(new ODGridColumn(Lan.g(this,"PatResp"),colWidthPatAmt,HorizontalAlignment.Right));//Patient Responsibility Amount (CLP05)
			gridClaimDetails.Rows.Clear();
			_claimInsPaidSum=0;
			List<Hx835_Claim> listClaimsPaid=_x835.ListClaimsPaid;
			for(int i=0;i<listClaimsPaid.Count;i++) {
				Hx835_Claim claimPaid=listClaimsPaid[i];
				ODGridRow row=new ODGridRow();
				SetClaimDetailRow(row,claimPaid);
				_claimInsPaidSum+=claimPaid.InsPaid;
				gridClaimDetails.Rows.Add(row);
			}
			gridClaimDetails.EndUpdate();
		}

		private void SetClaimDetailRow(ODGridRow row,Hx835_Claim claimPaid) {
			row.Tag=claimPaid;
			row.Cells.Clear();
			string claimStatus="";
			if(claimPaid.IsAttachedToClaim && claimPaid.ClaimNum!=0) {
				Claim claim=Claims.GetClaim(claimPaid.ClaimNum);
				if(claim!=null && claim.ClaimStatus=="R") {
					claimStatus="X";
				}
			}
			else if(claimPaid.IsAttachedToClaim && claimPaid.ClaimNum==0) {
				claimStatus="N/A";//User detached claim manually.
			}
			row.Cells.Add(claimStatus);
			row.Cells.Add(new UI.ODGridCell(claimPaid.PatientName.ToString()));//Patient
			string strDateService=claimPaid.DateServiceStart.ToShortDateString();
			if(claimPaid.DateServiceEnd>claimPaid.DateServiceStart) {
				strDateService+=" to "+claimPaid.DateServiceEnd.ToShortDateString();
			}
			row.Cells.Add(new UI.ODGridCell(strDateService));//DateService
			row.Cells.Add(new UI.ODGridCell(claimPaid.ClaimTrackingNumber));//Claim Identfier
			row.Cells.Add(new UI.ODGridCell(claimPaid.PayerControlNumber));//PayorControlNum
			row.Cells.Add(new UI.ODGridCell(claimPaid.StatusCodeDescript));//Status
			row.Cells.Add(new UI.ODGridCell(claimPaid.ClaimFee.ToString("f2")));//ClaimFee
			row.Cells.Add(new UI.ODGridCell(claimPaid.InsPaid.ToString("f2")));//InsPaid
			row.Cells.Add(new UI.ODGridCell(claimPaid.PatientRespAmt.ToString("f2")));//PatResp
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 3 (Summary).</summary>
		private void FillProviderAdjustmentDetails() {
			if(_x835.ListProvAdjustments.Count==0) {
				gridProviderAdjustments.Title="Provider Adjustments (None Reported)";
			}
			else {
				gridProviderAdjustments.Title="Provider Adjustments";
			}
			const int colWidthNPI=88;
			const int colWidthFiscalPeriod=80;
			const int colWidthReasonCode=90;
			const int colWidthRefIdent=80;
			const int colWidthAmount=80;
			int colWidthVariable=gridProviderAdjustments.Width-colWidthNPI-colWidthFiscalPeriod-colWidthReasonCode-colWidthRefIdent-colWidthAmount;
			gridProviderAdjustments.BeginUpdate();
			gridProviderAdjustments.Columns.Clear();
			gridProviderAdjustments.Columns.Add(new ODGridColumn("NPI",colWidthNPI,HorizontalAlignment.Center));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("FiscalPeriod",colWidthFiscalPeriod,HorizontalAlignment.Center));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("Reason",colWidthVariable,HorizontalAlignment.Left));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("ReasonCode",colWidthReasonCode,HorizontalAlignment.Center));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("RefIdent",colWidthRefIdent,HorizontalAlignment.Center));			
			gridProviderAdjustments.Columns.Add(new ODGridColumn("AdjAmt",colWidthAmount,HorizontalAlignment.Right));
			gridProviderAdjustments.EndUpdate();
			gridProviderAdjustments.BeginUpdate();
			gridProviderAdjustments.Rows.Clear();
			_provAdjAmtSum=0;
			for(int i=0;i<_x835.ListProvAdjustments.Count;i++) {
				Hx835_ProvAdj provAdj=_x835.ListProvAdjustments[i];
				ODGridRow row=new ODGridRow();
				row.Tag=provAdj;
				row.Cells.Add(new ODGridCell(provAdj.Npi));//NPI
				row.Cells.Add(new ODGridCell(provAdj.DateFiscalPeriod.ToShortDateString()));//FiscalPeriod
				row.Cells.Add(new ODGridCell(provAdj.ReasonCodeDescript));//Reason
				row.Cells.Add(new ODGridCell(provAdj.ReasonCode));//ReasonCode
				row.Cells.Add(new ODGridCell(provAdj.RefIdentification));//RefIdent
				row.Cells.Add(new ODGridCell(provAdj.AdjAmt.ToString("f2")));//AdjAmt
				_provAdjAmtSum+=provAdj.AdjAmt;
				gridProviderAdjustments.Rows.Add(row);
			}
			gridProviderAdjustments.EndUpdate();
		}

		private void FillFooter() {
			textClaimInsPaidSum.Text=_claimInsPaidSum.ToString("f2");
			textProjAdjAmtSum.Text=_provAdjAmtSum.ToString("f2");
			textPayAmountCalc.Text=(_claimInsPaidSum-_provAdjAmtSum).ToString("f2");
		}

		private void butRawMessage_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageText835);
			msgbox.Show(this);//This window is just used to display information.
		}

		private void gridProviderAdjustments_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Hx835_ProvAdj provAdj=(Hx835_ProvAdj)gridProviderAdjustments.Rows[e.Row].Tag;
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(
				provAdj.Npi+"\r\n"
				+provAdj.DateFiscalPeriod.ToShortDateString()+"\r\n"
				+provAdj.ReasonCode+" "+provAdj.ReasonCodeDescript+"\r\n"
				+provAdj.RefIdentification+"\r\n"
				+provAdj.AdjAmt.ToString("f2"));
			msgbox.Show(this);//This window is just used to display information.
		}

		private void gridClaimDetails_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Hx835_Claim claimPaid=(Hx835_Claim)gridClaimDetails.Rows[e.Row].Tag;
			Claim claim=claimPaid.GetClaimFromDb();
			if(claimPaid.IsSplitClaim && claim==null) {
				//TODO: Instead of showing this popup message, we could automatically split the claim for the user, which
				//would allow us to import ERAs more silently and thus support full automation better.
				if(MessageBox.Show(Lan.g(this,"The insurance carrier has split the claim")+". "
						+Lan.g(this,"You must manually locate and split the claim before entering the payment information")+". "
						+Lan.g(this,"Continue entering payment")+"?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
					return;
				}
			}
			bool isReadOnly=true;
			bool isAttachNeeded=(!claimPaid.IsAttachedToClaim);
			if(claim==null) {//Original claim not found.
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Original claim not found.  You can now attempt to locate the claim manually.")) {
					return;
				}
				//Create partial patient object to pre-fill search text boxes in FormPatientSelect
				Patient patCur=new Patient();
				patCur.LName=claimPaid.PatientName.Lname;
				patCur.FName=claimPaid.PatientName.Fname;
				FormPatientSelect formP=new FormPatientSelect(patCur);
				formP.PreFillSearchBoxes(patCur);
				formP.ShowDialog();
				if(formP.DialogResult!=DialogResult.OK) {
					return;
				}
				FormEtrans835ClaimSelect eTransClaimSelect=new FormEtrans835ClaimSelect(formP.SelectedPatNum,claimPaid);
				eTransClaimSelect.ShowDialog();
				if(eTransClaimSelect.DialogResult!=DialogResult.OK) {
					return;
				}
				claim=eTransClaimSelect.ClaimSelected; //Set claim so below we can act if a claim was already linked.
				if(!String.IsNullOrEmpty(claimPaid.ClaimTrackingNumber) && claimPaid.ClaimTrackingNumber!="0") {//Claim was not printed, it is an eclaim.
					claim.ClaimIdentifier=claimPaid.ClaimTrackingNumber;//Already checked DOS and ClaimFee, update claim identifier to link claims.
					Claims.UpdateClaimIdentifier(claim.ClaimNum,claim.ClaimIdentifier);//Update DB
				}
				isAttachNeeded=true;
			}
			if(isAttachNeeded) {
				//Create a hard link between the selected claim and the claim info on the 835.
				Etrans835Attaches.Delete(EtransCur.EtransNum,claimPaid.ClpSegmentIndex);//Detach existing if any.
				Etrans835Attach attach=new Etrans835Attach();
				attach.EtransNum=EtransCur.EtransNum;
				attach.ClaimNum=claim.ClaimNum;
				attach.ClpSegmentIndex=claimPaid.ClpSegmentIndex;
				Etrans835Attaches.Insert(attach);
				claimPaid.ClaimNum=claim.ClaimNum;
				claimPaid.IsAttachedToClaim=true;
			}
			//TODO: Supplemental payments are currently blocked because the first payment marks the claim received.
			//We need to somehow determine if the payment is supplemental (flag in the 835?), then create new Supplemental claimprocs for the claim
			//so we can call EnterPayment() to enter the payment on the new supplemental procs.
			if(claim!=null && claim.ClaimStatus=="R") {//Claim found and is already received.
				//If the claim is already received, then we do not allow the user to enter payments.
				//The user can edit the claim to change the status from received if they wish to enter the payments again.
				Patient pat=Patients.GetPat(claim.PatNum);
				Family fam=Patients.GetFamily(claim.PatNum);
				FormClaimEdit formCE=new FormClaimEdit(claim,pat,fam);
				formCE.ShowDialog();//Modal, because the user could edit information in this window.
				isReadOnly=false;
			}
			else if(Security.IsAuthorized(Permissions.InsPayCreate)) {//Claim found and is not received.  Date not checked here, but it will be checked when actually creating the check.
				EnterPayment(claimPaid,claim,false);
				isReadOnly=false;
			}
			if(isReadOnly) {
				FormEtrans835ClaimEdit formC=new FormEtrans835ClaimEdit(claimPaid);
				formC.Show(this);//This window is just used to display information.
			}
			if(!gridClaimDetails.IsDisposed) {//Not sure how the grid is sometimes disposed, perhaps because of it being non-modal.
				//Refresh the claim detail row in case something changed above.
				gridClaimDetails.BeginUpdate();
				SetClaimDetailRow(gridClaimDetails.Rows[e.Row],claimPaid);
				gridClaimDetails.EndUpdate();
			}
		}

		///<summary>Click method used by all gridClaimDetails right click options.</summary>
		private void gridClaimDetails_RightClickHelper(object sender,EventArgs e) {
			int index=gridClaimDetails.GetSelectedIndex();
			if(index==-1) {//Should not happen, menu item is only enabled when exactly 1 row is selected.
				return;
			}
			Hx835_Claim claimPaid=(Hx835_Claim)gridClaimDetails.Rows[index].Tag;
			GotoModule.GotoAccount(Claims.GetClaim(claimPaid.ClaimNum).PatNum);
		}

		///<summary>Enter either by total and/or by procedure, depending on whether or not procedure detail was provided in the 835 for this claim.
		///This function creates the payment claimprocs and displays the payment entry window.</summary>
		public static void EnterPayment(Hx835_Claim claimPaid,Claim claim,bool isAutomatic) {
			Patient pat=Patients.GetPat(claim.PatNum);
			Family fam=Patients.GetFamily(claim.PatNum);
			List<InsSub> listInsSubs=InsSubs.RefreshForFam(fam);
			List<InsPlan> listInsPlans=InsPlans.RefreshForSubList(listInsSubs);
			List<PatPlan> listPatPlans=PatPlans.Refresh(claim.PatNum);
			List<ClaimProc> listClaimProcsForClaim=ClaimProcs.RefreshForClaim(claim.ClaimNum);
			List<Procedure> listProcs=Procedures.GetProcsFromClaimProcs(listClaimProcsForClaim);
			ClaimProc cpByTotal=new ClaimProc();
			cpByTotal.FeeBilled=0;//All attached claimprocs will show in the grid and be used for the total sum.
			cpByTotal.DedApplied=(double)claimPaid.PatientDeductAmt;
			cpByTotal.AllowedOverride=(double)claimPaid.AllowedAmt;
			cpByTotal.InsPayAmt=(double)claimPaid.InsPaid;
			//Calculate the total claim writeoff by calculating the claim UCR total fee and subtracting the total allowed amount.
			//Note that claim.ClaimFee is the total billed fee (sum of claimproc.FeeBilled), not the UCR total fee, so we need to sum up the proc fees here.
			//Notice that this calculation does not rely on procedure matching, which makes the calculation more accurate.
			double claimUcrFee=0;
			for(int i=0;i<listClaimProcsForClaim.Count;i++) {
				ClaimProc claimProc=listClaimProcsForClaim[i];
				Procedure proc=Procedures.GetProcFromList(listProcs,claimProc.ProcNum);
				claimUcrFee+=proc.ProcFee;
			}
			//Writeoff could be negative if the UCR fee schedule was incorrectly entered by the user.  If negative, then is fixed below.
			cpByTotal.WriteOff=0;
			if(claimPaid.AllowedAmt>0) {
				cpByTotal.WriteOff=claimUcrFee-(double)claimPaid.AllowedAmt;
			}
			List<ClaimProc> listClaimProcsToEdit=new List<ClaimProc>();
			//Automatically set PayPlanNum if there is a payplan with matching PatNum, PlanNum, and InsSubNum that has not been paid in full.
			long insPayPlanNum=0;
			if(claim.ClaimType!="PreAuth" && claim.ClaimType!="Cap") {//By definition, capitation insurance pays in one lump-sum, not over an extended period of time.
				//By sending in ClaimNum, we ensure that we only get the payplan a claimproc from this claim was already attached to or payplans with no claimprocs attached.
				List<PayPlan> listPayPlans=PayPlans.GetValidInsPayPlans(claim.PatNum,claim.PlanNum,claim.InsSubNum,claim.ClaimNum);
				if(listPayPlans.Count==1) {
					insPayPlanNum=listPayPlans[0].PayPlanNum;
				}
				else if(listPayPlans.Count>1 && !isAutomatic) {
					//More than one valid PayPlan.  Cannot show this prompt when entering automatically, because it would disrupt workflow.
					FormPayPlanSelect FormPPS=new FormPayPlanSelect(listPayPlans);
					FormPPS.ShowDialog();//Modal because this form allows editing of information.
					if(FormPPS.DialogResult==DialogResult.OK) {
						insPayPlanNum=FormPPS.SelectedPayPlanNum;
					}
				}
			}
			//Choose the claimprocs which are not received.
			for(int i=0;i<listClaimProcsForClaim.Count;i++) {
				if(listClaimProcsForClaim[i].ProcNum==0) {//Exclude any "by total" claimprocs.  Choose claimprocs for procedures only.
					continue;
				}
				if(listClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived) {//Ignore procedures already received.
					continue;
				}
				listClaimProcsToEdit.Add(listClaimProcsForClaim[i]);//Procedures not yet received.
			}
			//If all claimprocs are received, then choose claimprocs if not paid on.
			if(listClaimProcsToEdit.Count==0) {
				for(int i=0;i<listClaimProcsForClaim.Count;i++) {
					if(listClaimProcsForClaim[i].ProcNum==0) {//Exclude any "by total" claimprocs.  Choose claimprocs for procedures only.
						continue;
					}
					if(listClaimProcsForClaim[i].ClaimPaymentNum!=0) {//Exclude claimprocs already paid.
						continue;
					}
					listClaimProcsToEdit.Add(listClaimProcsForClaim[i]);//Procedures not paid yet.
				}
			}
			//For each NotReceived/unpaid procedure on the claim where the procedure information can be successfully located on the EOB, enter the payment information.
			List <List <Hx835_Proc>> listProcsForClaimProcs=claimPaid.GetPaymentsForClaimProcs(listClaimProcsToEdit);
			for(int i=0;i<listClaimProcsToEdit.Count;i++) {
				ClaimProc claimProc=listClaimProcsToEdit[i];
				List<Hx835_Proc> listProcsForProcNum=listProcsForClaimProcs[i];
				//If listProcsForProcNum.Count==0, then procedure payment details were not not found for this one specific procedure.
				//This can happen with procedures from older 837s, when we did not send out the procedure identifiers, in which case ProcNum would be 0.
				//Since we cannot place detail on the service line, we will leave the amounts for the procedure on the total payment line.
				//If listProcsForPorcNum.Count==1, then we know that the procedure was adjudicated as is or it might have been bundled, but we treat both situations the same way.
				//The 835 is required to include one line for each bundled procedure, which gives is a direct manner in which to associate each line to its original procedure.
				//If listProcForProcNum.Count > 1, then the procedure was either split or unbundled when it was adjudicated by the payer.
				//We will not bother to modify the procedure codes on the claim, because the user can see how the procedure was split or unbunbled by viewing the 835 details.
				//Instead, we will simply add up all of the partial payment lines for the procedure, and report the full payment amount on the original procedure.
				claimProc.DedApplied=0;
				claimProc.AllowedOverride=0;
				claimProc.InsPayAmt=0;
				claimProc.WriteOff=0;
				StringBuilder sb=new StringBuilder();
				for(int j=0;j<listProcsForProcNum.Count;j++) {
					Hx835_Proc procPaidPartial=listProcsForProcNum[j];
					claimProc.DedApplied+=(double)procPaidPartial.DeductibleAmt;
					claimProc.AllowedOverride+=(double)procPaidPartial.AllowedAmt;
					claimProc.InsPayAmt+=(double)procPaidPartial.InsPaid;
					if(sb.Length>0) {
						sb.Append("\r\n");
					}
					sb.Append(procPaidPartial.GetRemarks());
				}
				//Procedure writeoff is calculated with procedure UCR fee instead of fee billed, to avoid inflating the writeoff.
				//Can only be done when a match was found, otherwise the the entire procedure fee would be written off due to allowed amount being unknown.
				if(claimPaid.AllowedAmt>0 && listProcsForProcNum.Count>0) {
					Procedure proc=Procedures.GetProcFromList(listProcs,claimProc.ProcNum);
					claimProc.WriteOff=proc.ProcFee-claimProc.AllowedOverride;//Might be negative if UCR fee schedule was entered incorrectly.
					if(claimProc.WriteOff<0) {
						claimProc.WriteOff=0;
					}
				}
				claimProc.Remarks=sb.ToString();
				if(claim.ClaimType=="PreAuth") {
					claimProc.Status=ClaimProcStatus.Preauth;
				}
				else if(claim.ClaimType=="Cap") {
					//Do nothing.  The claimprocstatus will remain Capitation.
				}
				else {
					claimProc.Status=ClaimProcStatus.Received;
					claimProc.DateEntry=DateTime.Now;//Date is was set rec'd
					claimProc.PayPlanNum=insPayPlanNum;//Payment plans do not exist for PreAuths or Capitation claims, by definition.
				}
				claimProc.DateCP=DateTimeOD.Today;
			}
			//Displace the procedure totals from the "by total" payment, since they have now been accounted for on the individual procedure lines.  Totals will not be affected if no procedure details could be located.
			//If a total payment was previously entered manually, this will subtract the existing total payment from the new total payment, causing the new total payment to be discarded below where zero amounts are checked.
			for(int i=0;i<listClaimProcsForClaim.Count;i++) {
				ClaimProc claimProc=listClaimProcsForClaim[i];
				cpByTotal.DedApplied-=claimProc.DedApplied;
				cpByTotal.AllowedOverride-=claimProc.AllowedOverride;
				cpByTotal.InsPayAmt-=claimProc.InsPayAmt;
				cpByTotal.WriteOff-=claimProc.WriteOff;//May cause cpByTotal.Writeoff to go negative if the user typed in the value for claimProc.Writeoff.
			}
			//The writeoff may be negative if the user manually entered some payment amounts before loading this window or if UCR fee schedule incorrect.
			if(cpByTotal.WriteOff<0) {
				cpByTotal.WriteOff=0;
			}
			bool isByTotalIncluded=true;
			//Do not create a total payment if the payment contains all zero amounts, because it would not be useful.  Written to account for potential rounding errors in the amounts.
			if(Math.Round(cpByTotal.DedApplied,2,MidpointRounding.AwayFromZero)==0
				&& Math.Round(cpByTotal.AllowedOverride,2,MidpointRounding.AwayFromZero)==0
				&& Math.Round(cpByTotal.InsPayAmt,2,MidpointRounding.AwayFromZero)==0
				&& Math.Round(cpByTotal.WriteOff,2,MidpointRounding.AwayFromZero)==0)
			{
				isByTotalIncluded=false;
			}
			if(claim.ClaimType=="PreAuth") {
				//In the claim edit window we currently block users from entering PreAuth payments by total, presumably because total payments affect the patient balance.
				isByTotalIncluded=false;
			}
			else if(claim.ClaimType=="Cap") {
				//In the edit claim window, we currently warn and discourage users from entering Capitation payments by total, because total payments affect the patient balance.
				isByTotalIncluded=false;
			}
			if(isByTotalIncluded) {
				cpByTotal.Status=ClaimProcStatus.Received;
				cpByTotal.ClaimNum=claim.ClaimNum;
				cpByTotal.PatNum=claim.PatNum;
				cpByTotal.ProvNum=claim.ProvTreat;
				cpByTotal.PlanNum=claim.PlanNum;
				cpByTotal.InsSubNum=claim.InsSubNum;
				cpByTotal.DateCP=DateTimeOD.Today;
				cpByTotal.ProcDate=claim.DateService;
				cpByTotal.DateEntry=DateTime.Now;
				cpByTotal.ClinicNum=claim.ClinicNum;
				cpByTotal.Remarks=claimPaid.GetRemarks();
				cpByTotal.PayPlanNum=insPayPlanNum;
				//Add the total payment to the beginning of the list, so that the ins paid amount for the total payment will be highlighted when FormEtrans835ClaimPay loads.
				listClaimProcsForClaim.Insert(0,cpByTotal);
			}
			FormEtrans835ClaimPay FormP=new FormEtrans835ClaimPay(claimPaid,claim,pat,fam,listInsPlans,listPatPlans,listInsSubs);
			FormP.ListClaimProcsForClaim=listClaimProcsForClaim;
			if(isAutomatic) {
				FormP.ReceivePayment();
			}
			else if(FormP.ShowDialog()!=DialogResult.OK) {//Modal because this window can edit information
				if(cpByTotal.ClaimProcNum!=0) {
					ClaimProcs.Delete(cpByTotal);
				}
			}
		}

		///<summary>etrans must be the entire object due to butOK_Click(...) calling Etranss.Update(...).
		///Anywhere we pull etrans from Etranss.RefreshHistory(...) will need to pull full object using an additional query.
		///Eventually we should enhance Etranss.RefreshHistory(...) to return full objects.</summary>
		public static void ShowEra(Etrans etrans){
			string messageText835=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum,false);
			X12object x835=new X12object(messageText835);
			List<string> listTranSetIds=x835.GetTranSetIds();
			if(listTranSetIds.Count>=2 && etrans.TranSetId835=="") {//More than one EOB in the 835 and we do not know which one to pick.
				FormEtrans835PickEob formPickEob=new FormEtrans835PickEob(listTranSetIds,messageText835,etrans);
				formPickEob.ShowDialog();
			}
			else {
				FormEtrans835Edit Form835=new FormEtrans835Edit();
				Form835.EtransCur=etrans;
				Form835.MessageText835=messageText835;
				Form835.TranSetId835=etrans.TranSetId835;//Empty or null string will cause the first EOB in the 835 to display.
				Form835.Show();//Non-modal
			}
		}

		private void butDetachClaim_Click(object sender,EventArgs e) {
			if(gridClaimDetails.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a claim from the claims paid grid and try again.");
				return;
			}
			if(gridClaimDetails.SelectedIndices.Length > 1) {
				if(!MsgBox.Show(this,true,
					"All selected claims will be immediately detached from this ERA even if you click Cancel when you leave the ERA window.  "
					+"Click OK to continue, or click Cancel to leave claims attached."))
				{
					return;
				}
			}
			DetachClaimHelper(gridClaimDetails.SelectedIndices.ToList());
		}

		private void DetachClaimHelper(List<int> listGridIndices) {
			gridClaimDetails.BeginUpdate();
			for(int i=0;i<listGridIndices.Count;i++) {
				ODGridRow row=gridClaimDetails.Rows[listGridIndices[i]];
				Hx835_Claim claimPaid=(Hx835_Claim)row.Tag;
				Etrans835Attaches.Delete(EtransCur.EtransNum,claimPaid.ClpSegmentIndex);
				Etrans835Attach attach=new Etrans835Attach();
				attach.EtransNum=EtransCur.EtransNum;
				attach.ClaimNum=0;
				attach.ClpSegmentIndex=claimPaid.ClpSegmentIndex;
				Etrans835Attaches.Insert(attach);
				claimPaid.IsAttachedToClaim=true;
				claimPaid.ClaimNum=0;
				SetClaimDetailRow(row,claimPaid);
			}
			gridClaimDetails.EndUpdate();
		}

		private void butClaimDetails_Click(object sender,EventArgs e) {
			if(gridClaimDetails.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Choose a claim paid before viewing details.");
				return;
			}
			Hx835_Claim claimPaid=(Hx835_Claim)gridClaimDetails.Rows[gridClaimDetails.SelectedIndices[0]].Tag;
			FormEtrans835ClaimEdit formE=new FormEtrans835ClaimEdit(claimPaid);
			formE.Show(this);//This window is just used to display information.
		}

		private void butPrint_Click(object sender,EventArgs e) {
			FormEtrans835Print form=new FormEtrans835Print(_x835);
			form.Show(this);//This window is just used to display information.
		}
		
		///<summary>Since ERAs are only used in the United States, we do not need to translate any text shown to the user.</summary>
		private void butBatch_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.InsPayCreate)) {//date not checked here, but it will be checked when saving the check to prevent backdating
				return;
			}
			if(gridClaimDetails.Rows.Count==0) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			//List of claims from _x835.ListClaimsPaid[X].GetClaimFromDB(), can be null.
			List<Claim> listClaimsFor835=Claims.GetClaimsFromClaimNums(_x835.ListClaimsPaid.Where(x=> x.ClaimNum!=0).Select(x => x.ClaimNum).ToList());
			List<Claim> listClaims=new List<Claim>();
			#region Populate listClaims
			List<Hx835_Claim> listSkippedPreauths=_x835.ListClaimsPaid.FindAll(x => x.IsPreauth && !x.IsAttachedToClaim);
			if(listSkippedPreauths.Count>0
				&& !MsgBox.Show(this,MsgBoxButtons.YesNo,
				"There are preauths that have not been attached to an internal claim or have not been detached from the ERA.  "
				+"Would you like to automatically detach and ignore these preauths?") )
			{
				Cursor=Cursors.Default;
				return;
			}
			List<int> listGridIndices=listSkippedPreauths.Select(x => _x835.ListClaimsPaid.FindIndex(y => y==x)).ToList();
			DetachClaimHelper(listGridIndices);
			foreach(Hx835_Claim claim in _x835.ListClaimsPaid) {
				if((claim.IsAttachedToClaim && claim.ClaimNum==0) //User manually detached claim.
					|| claim.IsPreauth) 
				{
					continue;
				}
				listClaims.Add(listClaimsFor835.FirstOrDefault(x => x.ClaimNum==claim.ClaimNum));//Can add nulls
				int index=listClaims.Count-1;
				Claim claimCur=listClaims[index];
				if(claimCur==null) {//Claim wasn't found in DB.
					listClaims[index]=new Claim();//ClaimNum will be 0, indicating that this is not a real claim.
					continue;
				}
			}
			#endregion
			if(listClaims.Count==0) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"All claims have been detached from this ERA or are preauths (there is no payment).  Click OK to close the ERA instead.");
				return;
			}
			if(listClaims.Exists(x => x.ClaimNum==0 || x.ClaimStatus!="R")) {
				#region Column width: PatNum
				int patNumColumnLength=6;//Minimum of 6 because that is the width of the column name "PatNum"
				if(listClaims.Exists(x => x.ClaimNum!=0)) {//There are claims that were found in DB, need to consider claim.PatNum.ToString() lengths.
					patNumColumnLength=Math.Min(8,Math.Max(patNumColumnLength,listClaims.Max(x => x.PatNum.ToString().Length)));
				}
				#endregion
				#region Column width: Patient
				Dictionary<long,string> dictPatNames=new Dictionary<long,string>();
				foreach(Claim claim in listClaims) {
					if(dictPatNames.ContainsKey(claim.PatNum)) {//Can be 0.  We want to add 0 to dictionary so blank name will show below.
						continue;
					}
					Patient patCur=Patients.GetPat(claim.PatNum);
					if(patCur==null) {
						dictPatNames.Add(0,"");
					}
					else {
						dictPatNames.Add(claim.PatNum,Patients.GetNameLF(patCur));
					}
				}
				int maxNamesLength=Math.Max(7,dictPatNames.Values.Max(x => x.Length));//Minimum of 7 to account for column title width "Patient".
				int maxX835NameLength=0;
				if(listClaims.Exists(x => x.ClaimNum==0)) {//There is a claim that could not be found in the DB. Must consider Hx835_Claims.PatientName lengths.
					maxX835NameLength=_x835.ListClaimsPaid
						//Only consider claims that could not be found in DB.  Both list are 1:1 and ClaimNum==0 represents that the claim could not be found.
						.FindAll(x => listClaims[_x835.ListClaimsPaid.IndexOf(x)].ClaimNum==0)
						.Max(x => x.PatientName.ToString().Length);
				}
				int maxColumnLength=Math.Max(maxNamesLength,maxX835NameLength);
				#endregion
				#region Construct msg
				string msg="One or more claims are not recieved.\r\n"
					+"You must receive all of the following claims before finializing payment:\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				msg+="PatNum".PadRight(patNumColumnLength)+"\t"+"Patient".PadRight(maxColumnLength)+"\tDOS       \tTotal Fee\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				for(int i=0;i<listClaims.Count;i++) {
					if(listClaims[i].ClaimNum==0) {//Current claim was not found in DB, so we will use the Hx835_Claim object.
						Hx835_Claim xClaimCur=_x835.ListClaimsPaid[i];
						msg+="".PadRight(patNumColumnLength)+"\t"//Blank PatNum because unknown.
							+xClaimCur.PatientName.ToString().PadRight(maxColumnLength)+"\t"
							+xClaimCur.DateServiceStart.ToShortDateString()+"\t"
							+POut.Decimal(xClaimCur.ClaimFee)+"\r\n";
						continue;
					}
					//Current claim was found in DB, so we will use Claim object
					Claim claim=listClaims[i];
					if(claim.ClaimStatus=="R") {
						continue;
					}
					msg+=claim.PatNum.ToString().PadRight(patNumColumnLength).Substring(0,patNumColumnLength)+"\t"
						+dictPatNames[claim.PatNum].PadRight(maxColumnLength).Substring(0,maxColumnLength)+"\t"
						+claim.DateService.ToShortDateString()+"\t"
						+POut.Double(claim.ClaimFee)+"\r\n";
				}
				#endregion
				new MsgBoxCopyPaste(msg).ShowDialog();
				Cursor=Cursors.Default;
				return;
			}
			List<ClaimProc> listClaimProcsAll=ClaimProcs.RefreshForClaims(listClaims.Where(x => x.ClaimNum!=0).Select(x=>x.ClaimNum).ToList());
			//Dictionary such that the key is a claimNum and the value is a list of associated claimProcs.
			Dictionary<long,List<ClaimProc>> dictClaimProcs=listClaimProcsAll.GroupBy(x => x.ClaimNum)
				.ToDictionary(
					x => x.Key,//ClaimNum
					x=>listClaimProcsAll.FindAll(y => y.ClaimNum==x.Key)//List of claimprocs associated to current claimNum
			);
			if(listClaimProcsAll.Exists(x => !x.Status.In(ClaimProcStatus.Received,ClaimProcStatus.Supplemental,ClaimProcStatus.CapClaim))) {
				int patNumColumnLength=Math.Max(6,listClaimProcsAll.Max(x => x.PatNum.ToString().Length));//PatNum column length
				Dictionary<long,string> dictPatNames=GetAllUniquePatNamesForClaims(listClaims);
				int maxNamesLength=dictPatNames.Values.Max(x => x.Length);
				#region Construct msg
				string msg="One or more claim procedures are set to the wrong status and are not ready to be finalized.\r\n"
					+"The acceptable claim procedure statuses are Received, Supplemental and CapClaim.\r\n"
					+"The following claims have claim procedures which need to be modified before finalizing:\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				msg+="PatNum".PadRight(patNumColumnLength)+"\t"+"Patient".PadRight(maxNamesLength)+"\tDOS       \tTotal Fee\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				foreach(Claim claim in listClaims) {
					List <ClaimProc> listClaimProcs=dictClaimProcs[claim.ClaimNum];
					if(listClaimProcs.All(x => x.Status.In(ClaimProcStatus.Received,ClaimProcStatus.Supplemental,ClaimProcStatus.CapClaim))) {
						continue;
					}
					msg+=claim.PatNum.ToString().PadRight(patNumColumnLength).Substring(0,patNumColumnLength)+"\t"
						+dictPatNames[claim.PatNum].PadRight(maxNamesLength).Substring(0,maxNamesLength)+"\t"
						+claim.DateService.ToShortDateString()+"\t"
						+POut.Double(claim.ClaimFee)+"\r\n";
				}
				#endregion
				new MsgBoxCopyPaste(msg).ShowDialog();
				Cursor=Cursors.Default;
				return;
			}
			if(listClaimProcsAll.Exists(x => x.ClaimPaymentNum!=0)) {
				int patNumColumnLength=Math.Max(6,listClaimProcsAll.Max(x => x.PatNum.ToString().Length));//PatNum column length
				Dictionary<long,string> dictPatNames=GetAllUniquePatNamesForClaims(listClaims);
				int maxNamesLength=dictPatNames.Values.Max(x => x.Length);
				#region Construct msg
				string msg="One or more claim procedures are already associated to a claim payment.\r\n"
					+"Either the wrong claim is attached to the ERA or you must detach the other claim payment from the following claims before continuing:\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				msg+="PatNum".PadRight(patNumColumnLength)+"\t"+"Patient".PadRight(maxNamesLength)+"\tDOS       \tTotal Fee\r\n";
				msg+="-------------------------------------------------------------------\r\n";
				foreach(Claim claim in listClaims) {
				List <ClaimProc> listClaimProcs=dictClaimProcs[claim.ClaimNum];
					if(listClaimProcs.All(x => x.ClaimPaymentNum==0)) {
						continue;
					}
					msg+=claim.PatNum.ToString().PadRight(patNumColumnLength).Substring(0,patNumColumnLength)+"\t"
						+dictPatNames[claim.PatNum].PadRight(maxNamesLength).Substring(0,maxNamesLength)+"\t"
						+claim.DateService.ToShortDateString()+"\t"
						+POut.Double(claim.ClaimFee)+"\r\n";
				}
				#endregion
				new MsgBoxCopyPaste(msg).ShowDialog();
				Cursor=Cursors.Default;
				return;
			}
			#region ClaimPayment creation
			ClaimPayment claimPay=new ClaimPayment();
			//Mimics FormClaimEdit.butBatch_Click(...)
			claimPay.CheckDate=MiscData.GetNowDateTime().Date;//Today's date for easier tracking by the office and to avoid backdating before accounting lock dates.
			claimPay.IsPartial=false;
			Patient pat=Patients.GetPat(listClaims[0].PatNum);
			claimPay.ClinicNum=pat.ClinicNum;
			claimPay.CarrierName=_x835.PayerName;
			claimPay.CheckAmt=listClaims.Select(x => x.InsPayAmt).Sum();
			claimPay.CheckNum=_x835.TransRefNum;
			long defNum=0;
			if(_x835._paymentMethodCode=="CHK") {//Physical check
				defNum=DefC.GetByExactName(DefCat.InsurancePaymentType,"Check");
			}
			else if(_x835._paymentMethodCode=="ACH") {//Electronic check
				defNum=DefC.GetByExactName(DefCat.InsurancePaymentType,"EFT");
			}
			else if(_x835._paymentMethodCode=="FWT") {//Wire transfer
				defNum=DefC.GetByExactName(DefCat.InsurancePaymentType,"Wired");
			}
			claimPay.PayType=defNum;		
			ClaimPayments.Insert(claimPay);
			#endregion
			#region Update Claim and ClaimProcs
			foreach(Claim claim in listClaims) {//All Claims in list were found in DB.
				Hx835_Claim claim835=_x835.ListClaimsPaid.First(x => x.ClaimNum==claim.ClaimNum);//Safe
				claim.InsPayAmt=(double)claim835.InsPaid;
				dictClaimProcs[claim.ClaimNum].ForEach(x => { 
					x.ClaimPaymentNum=claimPay.ClaimPaymentNum;
					ClaimProcs.Update(x); 
				});
				Claims.Update(claim);
			}
			#endregion
			FormClaimEdit.FormFinalizePaymentHelper(claimPay,listClaims[0],pat,Patients.GetFamily(pat.PatNum));
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
			Close();
		}

		private Dictionary<long,string> GetAllUniquePatNamesForClaims(List<Claim> listClaims) {
			Dictionary<long,string> dictPatNames=new Dictionary<long, string>();
			foreach(Claim claim in listClaims) {
				if(dictPatNames.ContainsKey(claim.PatNum)) {//Can be 0.  We want to add 0 to dictionary so blank name will show below.
					continue;
				}
				Patient patCur=Patients.GetPat(claim.PatNum);
				if(patCur==null) {
					dictPatNames.Add(0,"");
				}
				else {
					dictPatNames.Add(claim.PatNum,Patients.GetNameLF(patCur));
				}
			}
			return dictPatNames;
		}

		private void butOK_Click(object sender,EventArgs e) {
			EtransCur.Note=textNote.Text;
			bool isReceived=true;
			for(int i=0;i<gridClaimDetails.Rows.Count;i++) {
				if(gridClaimDetails.Rows[i].Cells[0].Text=="") {
					isReceived=false;
					break;
				}
			}
			if(isReceived) {
				EtransCur.AckCode="Recd";
			}
			else {
				EtransCur.AckCode="";
			}
			Etranss.Update(EtransCur);
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void FormEtrans835Edit_FormClosing(object sender,FormClosingEventArgs e) {
			_form835=null;
		}
		
	}
}