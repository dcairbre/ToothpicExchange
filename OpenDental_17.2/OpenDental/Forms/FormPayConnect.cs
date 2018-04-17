using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MigraDoc.DocumentObjectModel;
using OpenDentBusiness;
using DentalXChange.Dps.Pos;
using CodeBase;
using OpenDental.Bridges;

namespace OpenDental {
	public partial class FormPayConnect:ODForm {
		private Patient _patCur;
		private string _amountInit;
		private PayConnectService.transResponse _response;
		private MagstripCardParser _parser=null;
		private string _receiptStr;
		private PayConnectService.transType _trantype=PayConnectService.transType.SALE;
		private CreditCard _creditCardCur;
		private PayConnectService.creditCardRequest _request;
		private bool _isAddingCard;
		private long _clinicNum;
		private Program _progCur;
		private PosResponse _posResponse;

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public string AmountCharged {
			get { return PIn.Decimal(textAmount.Text).ToString("F"); }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public PayConnectService.creditCardRequest Request {
			get { return _request; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public string ReceiptStr {
			get { return _receiptStr; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public PayConnectService.transType TranType {
			get { return _trantype; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public string CardNumber {
			get { return textCardNumber.Text; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public PayConnectResponse Response {
			get {
				if(_response != null) {
					return new PayConnectResponse(_response,_request);
				}
				if(_posResponse != null) {
					return new PayConnectResponse(_posResponse);
				}
				return null;
			}
		}

		///<summary>Can handle CreditCard being null.</summary>
		public FormPayConnect(long clinicNum,Patient pat,string amount,CreditCard creditCard,bool isAddingCard=false) {
			InitializeComponent();
			Lan.F(this);
			_clinicNum=clinicNum;
			_patCur=pat;
			_amountInit=amount;
			_receiptStr="";
			_creditCardCur=creditCard;
			_isAddingCard=isAddingCard;
		}

		private void FormPayConnect_Load(object sender,EventArgs e) {
			_progCur=Programs.GetCur(ProgramName.PayConnect);
			if(_progCur==null) {
				MsgBox.Show(this,"PayConnect does not exist in the database.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(PIn.Bool(ProgramProperties.GetPropVal(_progCur.ProgramNum,"TerminalProcessingEnabled",_clinicNum))) {
				try {
					//If the config file for the DentalXChange credit card processing .dll doesn't exist, construct it from the included resource.
					if(!File.Exists("DpsPos.dll.config")) {
						File.WriteAllText("DpsPos.dll.config",Properties.Resources.DpsPos_dll_config);
					}
				}
				catch(Exception ex) {
					ex.DoNothing();//I'm not sure if the DpsPos.dll.config is really necessary.
				}
			}
			if(!PIn.Bool(ProgramProperties.GetPropVal(_progCur.ProgramNum,"TerminalProcessingEnabled",_clinicNum))
				|| _isAddingCard) //When adding a card, the web service must be used.
			{
				groupProcessMethod.Visible=false;
				Height-=55;//All the controls except for the Transaction Type group box should be anchored to the bottom, so they will move themselves up.
			}
			textAmount.Text=_amountInit;
			if(_patCur==null) {//Prepaid card
				radioAuthorization.Enabled=false;
				radioVoid.Enabled=false;
				radioReturn.Enabled=false;
				textZipCode.ReadOnly=true;
				textNameOnCard.ReadOnly=true;
				checkSaveToken.Enabled=false;
				sigBoxWrapper.Enabled=false;
			}
			else {//Other cards
				textZipCode.Text=_patCur.Zip;
				textNameOnCard.Text=_patCur.GetNameFL();
				checkSaveToken.Checked=PrefC.GetBool(PrefName.StoreCCtokens);
				if(PrefC.GetBool(PrefName.StoreCCnumbers)) {
					labelStoreCCNumWarning.Visible=true;
				}
				FillFieldsFromCard();
			}
			if(_isAddingCard) {//We will run a 0.01 authorization so we will not allow the user to change the transaction type or the amount.
				radioAuthorization.Checked=true;
				_trantype=PayConnectService.transType.AUTH;
				groupTransType.Enabled=false;
				labelAmount.Visible=false;
				textAmount.Visible=false;
				checkSaveToken.Checked=true;
				checkSaveToken.Enabled=false;
				checkForceDuplicate.Checked=true;
				checkForceDuplicate.Enabled=false;
			}
		}

		private void FillFieldsFromCard() {
			if(_creditCardCur!=null) {//User selected a credit card from drop down.
				if(_creditCardCur.CCNumberMasked!="") {
					textCardNumber.Text=_creditCardCur.CCNumberMasked;
				}
				if(_creditCardCur.CCExpiration!=null && _creditCardCur.CCExpiration.Year>2005) {
					textExpDate.Text=_creditCardCur.CCExpiration.ToString("MMyy");
				}
				if(_creditCardCur.Zip!="") {
					textZipCode.Text=_creditCardCur.Zip;
				}
				if(_creditCardCur.PayConnectToken!="" && _creditCardCur.PayConnectTokenExp>DateTime.MinValue) {
					checkSaveToken.Checked=true;
					checkSaveToken.Enabled=false;
					textSecurityCode.ReadOnly=true;
					textZipCode.ReadOnly=true;
					textNameOnCard.ReadOnly=true;
					textCardNumber.ReadOnly=true;
					textExpDate.ReadOnly=true;
				}
			}
		}

		private void radioSale_Click(object sender,EventArgs e) {
			radioSale.Checked=true;
			radioAuthorization.Checked=false;
			radioVoid.Checked=false;
			radioReturn.Checked=false;
			radioForce.Checked=false;
			textRefNumber.Visible=false;
			labelRefNumber.Visible=false;
			_trantype=PayConnectService.transType.SALE;
			if(radioWebService.Checked) {
				textCardNumber.Focus();//Usually transaction type is chosen before card number is entered, but textCardNumber box must be selected in order for card swipe to work.
			}
			else {
				textAmount.Focus();
			}
		}

		private void radioAuthorization_Click(object sender,EventArgs e) {
			radioSale.Checked=false;
			radioAuthorization.Checked=true;
			radioVoid.Checked=false;
			radioReturn.Checked=false;
			radioForce.Checked=false;
			textRefNumber.Visible=false;
			labelRefNumber.Visible=false;
			_trantype=PayConnectService.transType.AUTH;
			if(radioWebService.Checked) {
				textCardNumber.Focus();//Usually transaction type is chosen before card number is entered, but textCardNumber box must be selected in order for card swipe to work.
			}
			else {
				textAmount.Focus();
			}
		}

		private void radioVoid_Click(object sender,EventArgs e) {
			radioSale.Checked=false;
			radioAuthorization.Checked=false;
			radioVoid.Checked=true;
			radioReturn.Checked=false;
			radioForce.Checked=false;
			textRefNumber.Visible=true;
			labelRefNumber.Visible=true;
			labelRefNumber.Text=Lan.g(this,"Ref Number");
			_trantype=PayConnectService.transType.VOID;
			if(radioWebService.Checked) {
				textCardNumber.Focus();//Usually transaction type is chosen before card number is entered, but textCardNumber box must be selected in order for card swipe to work.
			}
			else {
				textAmount.Focus();
			}
		}

		private void radioReturn_Click(object sender,EventArgs e) {
			radioSale.Checked=false;
			radioAuthorization.Checked=false;
			radioVoid.Checked=false;
			radioReturn.Checked=true;
			radioForce.Checked=false;
			textRefNumber.Visible=true;
			labelRefNumber.Visible=true;
			labelRefNumber.Text=Lan.g(this,"Ref Number");
			_trantype=PayConnectService.transType.RETURN;
			if(radioWebService.Checked) {
				textCardNumber.Focus();//Usually transaction type is chosen before card number is entered, but textCardNumber box must be selected in order for card swipe to work.
			}
			else {
				textAmount.Focus();
			}
		}

		private void radioForce_Click(object sender,EventArgs e) {
			radioSale.Checked=false;
			radioAuthorization.Checked=false;
			radioVoid.Checked=false;
			radioReturn.Checked=false;
			radioForce.Checked=true;
			textRefNumber.Visible=true;
			labelRefNumber.Visible=true;
			labelRefNumber.Text=Lan.g(this,"Authorization Code");
			_trantype=PayConnectService.transType.FORCE;
			if(radioWebService.Checked) {
				textCardNumber.Focus();//Usually transaction type is chosen before card number is entered, but textCardNumber box must be selected in order for card swipe to work.
			}
			else {
				textAmount.Focus();
			}
		}

		private void radioWebService_CheckedChanged(object sender,EventArgs e) {
			radioTerminal.Checked=!radioWebService.Checked;
			if(!radioWebService.Checked) {
				return;
			}
			foreach(TextBox textBox in Controls.OfType<TextBox>()) {
				textBox.ReadOnly=false;
			}
			radioForce.Enabled=true;
			checkSaveToken.Enabled=true;
			checkForceDuplicate.Enabled=true;
			FillFieldsFromCard();
			textNameOnCard.Text=_patCur.GetNameFL();
		}

		private void radioTerminal_CheckedChanged(object sender,EventArgs e) {
			radioWebService.Checked=!radioTerminal.Checked;
			if(!radioTerminal.Checked) {
				return;
			}
			foreach(TextBox textBox in Controls.OfType<TextBox>()) {
				if(textBox==textRefNumber || textBox==textAmount) {
					continue;
				}
				textBox.ReadOnly=true;
				textBox.Text="";
			}
			radioForce.Enabled=false;
			checkSaveToken.Checked=false;
			checkSaveToken.Enabled=false;
			textAmount.Focus();
		}

		private void textCardNumber_KeyPress(object sender,KeyPressEventArgs e) {
			if(String.IsNullOrEmpty(textCardNumber.Text)) {
				return;
			}
			if(textCardNumber.Text.StartsWith("%") && textCardNumber.Text.EndsWith("?") && e.KeyChar == 13) {
				e.Handled=true;
				ParseSwipedCard(textCardNumber.Text);
			}
		}

		private void ParseSwipedCard(string data) {
			Clear();
			try {
				_parser=new MagstripCardParser(data);
			}
			catch(MagstripCardParseException) {
				MessageBox.Show(this,"Could not read card, please try again.","Card Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			if(_parser!=null) {
				textCardNumber.Text=_parser.AccountNumber;
				textExpDate.Text=_parser.ExpirationMonth.ToString().PadLeft(2,'0')+(_parser.ExpirationYear%100).ToString().PadLeft(2,'0');
				textNameOnCard.Text=_parser.FirstName+" "+_parser.LastName;
				GetNextControl(textNameOnCard,true).Focus();//Move forward to the next control in the tab order.
			}
		}

		private void Clear() {
			textCardNumber.Text="";
			textExpDate.Text="";
			textNameOnCard.Text="";
			textSecurityCode.Text="";
			textZipCode.Text="";
		}

		private bool VerifyData(out int expYear,out int expMonth){
			expYear=0;
			expMonth=0;
			if(!Regex.IsMatch(textAmount.Text,"^[0-9]+$") && !Regex.IsMatch(textAmount.Text,"^[0-9]*\\.[0-9]+$")) {
				MsgBox.Show(this,"Invalid amount.");
				return false;
			}
			if((_trantype==PayConnectService.transType.VOID || 
				(_trantype==PayConnectService.transType.RETURN && radioWebService.Checked))//The reference number is optional for terminal returns. 
				&& textRefNumber.Text=="") 
			{
				MsgBox.Show(this,"Ref Number required.");
				return false;
			}
			string paytype=ProgramProperties.GetPropVal(_progCur.ProgramNum,"PaymentType",_clinicNum);
			if(!DefC.Short[(int)DefCat.PaymentTypes].Any(x => x.DefNum.ToString()==paytype)) { //paytype is not a valid DefNum
				MsgBox.Show(this,"The PayConnect payment type has not been set.");
				return false;
			}
			if(radioTerminal.Checked) {
				return true;
			}
			//Processing through Web Service
			// Consider adding more advanced verification methods using PayConnect validation requests.
			if(textCardNumber.Text.Trim().Length<5){
				MsgBox.Show(this,"Invalid Card Number.");
				return false;
			}
			try {//PIn.Int will throw an exception if not a valid format
				if(Regex.IsMatch(textExpDate.Text,@"^\d\d[/\- ]\d\d$")) {//08/07 or 08-07 or 08 07
					expYear=PIn.Int("20"+textExpDate.Text.Substring(3,2));
					expMonth=PIn.Int(textExpDate.Text.Substring(0,2));
				}
				else if(Regex.IsMatch(textExpDate.Text,@"^\d{4}$")) {//0807
					expYear=PIn.Int("20"+textExpDate.Text.Substring(2,2));
					expMonth=PIn.Int(textExpDate.Text.Substring(0,2));
				}
				else {
					MsgBox.Show(this,"Expiration format invalid.");
					return false;
				}
			}
			catch(Exception) {
				MsgBox.Show(this,"Expiration format invalid.");
				return false;
			}
			if(_creditCardCur==null) {//if the user selected a new CC, verify through PayConnect
				//using a new CC and the card number entered contains something other than digits
				if(textCardNumber.Text.Any(x => !char.IsDigit(x))) {
					MsgBox.Show(this,"Invalid card number.");
					return false;
				}
				if(!Bridges.PayConnect.IsValidCardAndExp(textCardNumber.Text,expYear,expMonth)) {//if exception happens, a message box will show with the error
					MsgBox.Show(this,"Card number or expiration date failed validation with PayConnect.");
					return false;
				}
			}
			else if(_creditCardCur.PayConnectToken=="" && Regex.IsMatch(textCardNumber.Text,@"X+[0-9]{4}")) {//using a stored CC
				MsgBox.Show(this,"There is no saved PayConnect token for this credit card.  The card number and expiration must be re-entered.");
				return false;
			}
			if(textNameOnCard.Text.Trim()=="" && _patCur!=null){//Name required for patient credit cards, not prepaid cards.
				MsgBox.Show(this,"Name On Card required.");
				return false;
			}
			if(_trantype==PayConnectService.transType.FORCE && textRefNumber.Text=="") {
				MsgBox.Show(this,"Authorization Code required.");
				return false;
			}
			//verify the selected clinic has a username and password type entered
			if(ProgramProperties.GetPropVal(_progCur.ProgramNum,"Username",_clinicNum)==""
				|| ProgramProperties.GetPropVal(_progCur.ProgramNum,"Password",_clinicNum)=="") //if username or password is blank
			{
				MsgBox.Show(this,"The PayConnect username and/or password has not been set.");
				return false;
			}
			return true;
		}

		///<summary>Builds a receipt string for a web service transaction.</summary>
		private string BuildReceiptString(PayConnectService.creditCardRequest request,PayConnectService.transResponse response,
			PayConnectService.signatureResponse sigResponse) 
		{
			string result="";
			int xmin=0;
			int xleft=xmin;
			int xright=15;
			int xmax=37;
			result+=Environment.NewLine;
			result+=AddClinicToReceipt();
			//Print body
			result+="Date".PadRight(xright-xleft,'.')+DateTime.Now.ToString()+Environment.NewLine;
			result+=Environment.NewLine;
			result+="Trans Type".PadRight(xright-xleft,'.')+request.TransType.ToString()+Environment.NewLine;
			result+=Environment.NewLine;
			result+="Transaction #".PadRight(xright-xleft,'.')+response.RefNumber+Environment.NewLine;
			result+="Name".PadRight(xright-xleft,'.')+request.NameOnCard+Environment.NewLine;
			result+="Account".PadRight(xright-xleft,'.');
			for(int i=0;i<request.CardNumber.Length-4;i++) {
				result+="*";
			}
			result+=request.CardNumber.Substring(request.CardNumber.Length-4)+Environment.NewLine;//last 4 digits of card number only.
			result+="Exp Date".PadRight(xright-xleft,'.')+request.Expiration.month.ToString().PadLeft(2,'0')+(request.Expiration.year%100)+Environment.NewLine;
			result+="Card Type".PadRight(xright-xleft,'.')+PayConnectUtils.GetCardType(request.CardNumber)+Environment.NewLine;
			result+="Entry".PadRight(xright-xleft,'.')+(String.IsNullOrEmpty(request.MagData)?"Manual":"Swiped")+Environment.NewLine;
			result+="Auth Code".PadRight(xright-xleft,'.')+response.AuthCode+Environment.NewLine;
			result+="Result".PadRight(xright-xleft,'.')+response.Status.description+Environment.NewLine;
			if(response.Messages!=null) {
				string label="Message";
				foreach(string m in response.Messages) {
					result+=label.PadRight(xright-xleft,'.')+m+Environment.NewLine;
					label="";
				}
			}
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine;
			result+="Total Amt".PadRight(xright-xleft,'.')+request.Amount+Environment.NewLine;
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine;
			result+="I agree to pay the above total amount according to my card issuer/bank agreement."+Environment.NewLine;
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine+Environment.NewLine+Environment.NewLine;
			if(sigResponse==null || sigResponse.Status==null || sigResponse.Status.code!=0) {
				result+="Signature X".PadRight(xmax-xleft,'_');
			}
			else {
				result+="Electronically signed";
			}
			return result;
		}

		///<summary>Builds a receipt string for a terminal transaction.</summary>
		private string BuildReceiptString(PosRequest posRequest,PosResponse posResponse,PayConnectService.signatureResponse sigResponse) {
			string result="";
			int xleft=0;
			int xright=15;
			int xmax=37;
			result+=Environment.NewLine;
			result+=AddClinicToReceipt();
			//Print body
			result+="Date".PadRight(xright-xleft,'.')+DateTime.Now.ToString()+Environment.NewLine;
			result+=Environment.NewLine;
			result+=AddReceiptField("Trans Type",posResponse.TransactionType.ToString());
			result+=Environment.NewLine;
			result+=AddReceiptField("Transaction #",posResponse.ReferenceNumber.ToString());
			result+=AddReceiptField("Account",posResponse.CardNumber);
			result+=AddReceiptField("Card Type",posResponse.CardBrand);
			result+=AddReceiptField("Entry",posResponse.EntryMode);
			result+=AddReceiptField("Auth Code",posResponse.AuthCode);
			result+=AddReceiptField("Result",posResponse.ResponseDescription);
			result+=AddReceiptField("MerchantId",posResponse.MerchantId);
			result+=AddReceiptField("TerminalId",posResponse.TerminalId);
			result+=AddReceiptField("Mode",posResponse.Mode);
			result+=AddReceiptField("CardVerifyMthd",posResponse.CardVerificationMethod);
			if(posResponse.EMV!=null && !string.IsNullOrEmpty(posResponse.EMV.AppId)) {
				result+=AddReceiptField("EMV AppId",posResponse.EMV.AppId);
			}
			if(posResponse.EMV!=null && !string.IsNullOrEmpty(posResponse.EMV.TermVerifResults)) {
				result+=AddReceiptField("EMV TermResult",posResponse.EMV.TermVerifResults);
			}
			if(posResponse.EMV!=null && !string.IsNullOrEmpty(posResponse.EMV.IssuerAppData)) {
				result+=AddReceiptField("EMV IssuerData",posResponse.EMV.IssuerAppData);
			}
			if(posResponse.EMV!=null && !string.IsNullOrEmpty(posResponse.EMV.TransStatusInfo)) {
				result+=AddReceiptField("EMV TransInfo",posResponse.EMV.TransStatusInfo);
			}
			if(posResponse.EMV!=null && !string.IsNullOrEmpty(posResponse.EMV.AuthResponseCode)) {
				result+=AddReceiptField("EMV AuthResp",posResponse.EMV.AuthResponseCode);
			}
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine;
			result+="Total Amt".PadRight(xright-xleft,'.')+posResponse.Amount+Environment.NewLine;
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine;
			result+="I agree to pay the above total amount according to my card issuer/bank agreement."+Environment.NewLine;
			result+=Environment.NewLine+Environment.NewLine+Environment.NewLine+Environment.NewLine+Environment.NewLine;
			if(sigResponse==null || sigResponse.Status==null || sigResponse.Status.code!=0) {
				result+="Signature X".PadRight(xmax-xleft,'_');
			}
			else {
				result+="Electronically signed";
			}
			return result;
		}

		///<summary>Returns the field name and value formatted to be added to a receipt string. The fieldName should be less than 15 characters.</summary>
		private string AddReceiptField(string fieldName,string fieldValue) {
			int xleft=0;
			int xright=15;
			int xmax=37;
			string retStr="";
			fieldValue=fieldValue??"";
			retStr+=fieldName.PadRight(xright-xleft,'.');
			if(fieldValue.Length<xmax-xright) {//Short enough to fit on one line
				retStr+=fieldValue+Environment.NewLine;
			}
			else {//Put the field value on two lines
				retStr+=fieldValue.Substring(0,xmax-xright-1)+Environment.NewLine;
				retStr+="".PadRight(xright,'.')+fieldValue.Substring(xmax-xright-1)+Environment.NewLine;
			}
			return retStr;
		}

		private string AddClinicToReceipt() {
			string result="";
			Clinic clinicCur=null;
			//_clinicNum will be 0 if clinics are not enabled or if the payment.ClinicNum=0, which will happen if the patient.ClinicNum=0 and the user
			//does not change the clinic on the payment before sending to PayConnect or if the user decides to process the payment for 'Headquarters'
			//and manually changes the clinic on the payment from the patient's clinic to 'none'
			if(_clinicNum==0) {
				clinicCur=Clinics.GetPracticeAsClinicZero();
			}
			else {
				clinicCur=Clinics.GetClinic(_clinicNum);
			}
			if(clinicCur!=null) {
				if(clinicCur.Description.Length>0) {
					result+=clinicCur.Description+Environment.NewLine;
				}
				if(clinicCur.Address.Length>0) {
					result+=clinicCur.Address+Environment.NewLine;
				}
				if(clinicCur.Address2.Length>0) {
					result+=clinicCur.Address2+Environment.NewLine;
				}
				if(clinicCur.City.Length>0 || clinicCur.State.Length>0 || clinicCur.Zip.Length>0) {
					result+=clinicCur.City+", "+clinicCur.State+" "+clinicCur.Zip+Environment.NewLine;
				}
				if(clinicCur.Phone.Length==10
					&& (CultureInfo.CurrentCulture.Name=="en-US" ||
					CultureInfo.CurrentCulture.Name.EndsWith("CA"))) //Canadian. en-CA or fr-CA
				{
					result+="("+clinicCur.Phone.Substring(0,3)+")"+clinicCur.Phone.Substring(3,3)+"-"+clinicCur.Phone.Substring(6)+Environment.NewLine;
				}
				else if(clinicCur.Phone.Length>0) {
					result+=clinicCur.Phone+Environment.NewLine;
				}
			}
			result+=Environment.NewLine;
			return result;
		}
		
		private void PrintReceipt(string receiptStr) {
			string[] receiptLines=receiptStr.Split(new string[] { Environment.NewLine },StringSplitOptions.None);
			MigraDoc.DocumentObjectModel.Document doc=new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch(3.0);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch(0.181*receiptLines.Length+0.56);//enough to print receipt text plus 9/16 inch (0.56) extra space at bottom.
			doc.DefaultPageSetup.TopMargin=Unit.FromInch(0.25);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch(0.25);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch(0.25);
			MigraDoc.DocumentObjectModel.Font bodyFontx=MigraDocHelper.CreateFont(8,false);
			bodyFontx.Name=FontFamily.GenericMonospace.Name;
			MigraDoc.DocumentObjectModel.Section section=doc.AddSection();
			Paragraph par=section.AddParagraph();
			ParagraphFormat parformat=new ParagraphFormat();
			parformat.Alignment=ParagraphAlignment.Left;
			parformat.Font=bodyFontx;
			par.Format=parformat;
			par.AddFormattedText(receiptStr,bodyFontx);
			MigraDoc.Rendering.Printing.MigraDocPrintDocument printdoc=new MigraDoc.Rendering.Printing.MigraDocPrintDocument();
			MigraDoc.Rendering.DocumentRenderer renderer=new MigraDoc.Rendering.DocumentRenderer(doc);
			renderer.PrepareDocument();
			printdoc.Renderer=renderer;
#if DEBUG
			FormRpPrintPreview pView=new FormRpPrintPreview();
			pView.printPreviewControl2.Document=printdoc;
			pView.ShowDialog();
#else
			try {
				if(PrinterL.SetPrinter(pd2,PrintSituation.Receipt,_patCur.PatNum,"PayConnect receipt printed")) {
					printdoc.PrinterSettings=pd2.PrinterSettings;
					printdoc.Print();
				}
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Printer not available.")+"\r\n"+Lan.g(this,"Original error")+": "+ex.Message);
			}
#endif
		}
		
		private PayConnectService.signatureResponse SendSignature(string refNumber) {
			if(!sigBoxWrapper.GetSigChanged() || string.IsNullOrEmpty(sigBoxWrapper.GetSignature(""))) {
				return null;
			}
			PayConnectService.signatureRequest sigRequest=new PayConnectService.signatureRequest();
			sigRequest.RefNumber=refNumber;
			sigRequest.SignatureType=PayConnectService.signatureType.JPEG;
			using(Bitmap sigImage=sigBoxWrapper.GetSigImage())
			using(MemoryStream memStream=new MemoryStream()) {
				sigImage.Save(memStream,ImageFormat.Jpeg);
				byte[] imageBytes=memStream.ToArray();
				sigRequest.SignatureData=Convert.ToBase64String(imageBytes);
			}
			return PayConnect.ProcessSignature(sigRequest,_clinicNum);
		}

		///<summary>Processes a PayConnect payment via the PayConnect web service.</summary>
		private bool ProcessPaymentWebService(int expYear,int expMonth) {
			string refNumber="";
			if(_trantype==PayConnectService.transType.VOID || _trantype==PayConnectService.transType.RETURN) {
				refNumber=textRefNumber.Text;
			}
			string magData=null;
			if(_parser!=null) {
				magData=_parser.Track2;
			}
			string cardNumber=textCardNumber.Text;
			//if using a stored CC and there is an X-Charge token saved for the CC and the user enters the whole card number to get a PayConnect token
			//and the number entered doesn't have the same last 4 digits and exp date, then assume it's not the same card and clear out the X-Charge token.
			if(_creditCardCur!=null //using a saved CC
				&& !string.IsNullOrEmpty(_creditCardCur.XChargeToken) //there is an X-Charge token saved
				&& (cardNumber.Right(4)!=_creditCardCur.CCNumberMasked.Right(4) //the card number entered doesn't have the same last 4 digits
					|| expYear!=_creditCardCur.CCExpiration.Year //the card exp date entered doesn't have the same year
					|| expMonth!=_creditCardCur.CCExpiration.Month)) //the card exp date entered doesn't have the same month
			{
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"The card number or expiration date entered does not match the X-Charge card on file.  Do you wish "
					+"to replace the X-Charge card with this one?"))
				{
					_creditCardCur.XChargeToken="";
				}
				else {
					Cursor=Cursors.Default;
					return false;
				}
			}
			//if the user has chosen to store CC tokens and the stored CC has a token and the token is not expired,
			//then use it instead of the CC number and CC expiration.
			if(checkSaveToken.Checked
				&& _creditCardCur!=null //if the user selected a saved CC
				&& _creditCardCur.PayConnectToken!="" //there is a stored token for this card
				&& _creditCardCur.PayConnectTokenExp.Date>=DateTime.Today.Date) //the token is not expired
			{
				cardNumber=_creditCardCur.PayConnectToken;
				expYear=_creditCardCur.PayConnectTokenExp.Year;
				expMonth=_creditCardCur.PayConnectTokenExp.Month;
			}
			string authCode="";
			if(_trantype==PayConnectService.transType.FORCE) {
				authCode=textRefNumber.Text;
			}
			_request=Bridges.PayConnect.BuildSaleRequest(PIn.Decimal(textAmount.Text),cardNumber,expYear,
				expMonth,textNameOnCard.Text,textSecurityCode.Text,textZipCode.Text,magData,_trantype,refNumber,checkSaveToken.Checked,authCode,checkForceDuplicate.Checked);
			_response=Bridges.PayConnect.ProcessCreditCard(_request,_clinicNum);
			if(_response==null || _response.Status.code!=0) {//error in transaction
				return false;
			}
			PayConnectService.signatureResponse sigResponse=SendSignature(_response.RefNumber);
			if(_trantype==PayConnectService.transType.SALE && _response.Status.code==0) {//Only print a receipt if transaction is an approved SALE.				
				_receiptStr=BuildReceiptString(_request,_response,sigResponse);
				PrintReceipt(_receiptStr);
			}
			if(!PrefC.GetBool(PrefName.StoreCCnumbers) && !checkSaveToken.Checked) {//not storing the card number or the token
				return true;
			}
			//response must be non-null and the status code must be 0=Approved
			//also, the user must have the pref StoreCCnumbers enabled or they have the checkSaveTokens checked
			if(_creditCardCur==null) {//user selected Add new card from the payment window, save it or its token depending on settings
				_creditCardCur=new CreditCard();
				_creditCardCur.IsNew=true;
				_creditCardCur.PatNum=_patCur.PatNum;
				List<CreditCard> itemOrderCount=CreditCards.Refresh(_patCur.PatNum);
				_creditCardCur.ItemOrder=itemOrderCount.Count;
			}
			_creditCardCur.CCExpiration=new DateTime(expYear,expMonth,DateTime.DaysInMonth(expYear,expMonth));
			if(PrefC.GetBool(PrefName.StoreCCnumbers)) {
				_creditCardCur.CCNumberMasked=textCardNumber.Text;
			}
			else {
				_creditCardCur.CCNumberMasked=textCardNumber.Text.Right(4).PadLeft(textCardNumber.Text.Length,'X');
			}
			_creditCardCur.Zip=textZipCode.Text;
			_creditCardCur.PayConnectToken="";
			_creditCardCur.PayConnectTokenExp=DateTime.MinValue;
			//Store the token and the masked CC number (only last four digits).
			if(checkSaveToken.Checked && _response.PaymentToken!=null) {
				_creditCardCur.PayConnectToken=_response.PaymentToken.TokenId;
				_creditCardCur.PayConnectTokenExp=new DateTime(_response.PaymentToken.Expiration.year,_response.PaymentToken.Expiration.month,
				DateTime.DaysInMonth(_response.PaymentToken.Expiration.year,_response.PaymentToken.Expiration.month));
			}
			_creditCardCur.CCSource=CreditCardSource.PayConnect;
			if(_creditCardCur.IsNew) {
				_creditCardCur.ClinicNum=_clinicNum;
				_creditCardCur.Procedures=PrefC.GetString(PrefName.DefaultCCProcs);
				CreditCards.Insert(_creditCardCur);
			}
			else {
				if(_creditCardCur.CCSource==CreditCardSource.XServer) {//This card has also been added for XCharge.
					_creditCardCur.CCSource=CreditCardSource.XServerPayConnect;
				}
				CreditCards.Update(_creditCardCur);
			}
			return true;
		}

		///<summary>Processes a PayConnect payment via a credit card terminal.</summary>
		private bool ProcessPaymentTerminal() {
			PosRequest posRequest=null;
			try {
				if(radioSale.Checked) {
					posRequest=PosRequest.CreateSale(PIn.Decimal(textAmount.Text));
				}
				else if(radioAuthorization.Checked) {
					posRequest=PosRequest.CreateAuth(PIn.Decimal(textAmount.Text));
				}
				else if(radioVoid.Checked) {
					posRequest=PosRequest.CreateVoidByReference(textRefNumber.Text);
				}
				else if(radioReturn.Checked) {
					if(textRefNumber.Text=="") {
						posRequest=PosRequest.CreateRefund(PIn.Decimal(textAmount.Text));
					}
					else {
						posRequest=PosRequest.CreateRefund(PIn.Decimal(textAmount.Text),textRefNumber.Text);
					}
				}
				else {//Shouldn't happen
					MsgBox.Show(this,"Please select a transaction type");
					return false;
				}
				posRequest.ForceDuplicate=checkForceDuplicate.Checked;
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error creating request:")+" "+ex.Message);
				return false;
			}
			Action actionCloseProgress=null;
			try {
				actionCloseProgress=ODProgress.ShowProgressStatus("PayConnectProcessing",Lan.g(this,"Processing payment on terminal"));
				_posResponse=DpsPos.ProcessCreditCard(posRequest);

			}
			catch(Exception ex) {
				actionCloseProgress?.Invoke();
				MessageBox.Show(Lan.g(this,"Error processing card:")+" "+ex.Message);
				return false;
			}
			actionCloseProgress?.Invoke();
			if(_posResponse==null) {
				MessageBox.Show(Lan.g(this,"Error processing card"));
				return false;
			}
			if(_posResponse.ResponseCode!="0") {//"0" indicates success. May need to check the AuthCode field too to determine if this was a success.
				MessageBox.Show(Lan.g(this,"Error message from Pay Connect:")+"\r\n"+_posResponse.ResponseDescription);
				return false;
			}
			PayConnectService.signatureResponse sigResponse=null;
			try {
				Cursor=Cursors.WaitCursor;
				sigResponse=SendSignature(_posResponse.ReferenceNumber.ToString());
				Cursor=Cursors.Default;
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(Lan.g(this,"Card successfully charged. Error processing signature:")+" "+ex.Message);
			}
			textCardNumber.Text=_posResponse.CardNumber;
			textAmount.Text=_posResponse.Amount.ToString("f");
			_receiptStr=BuildReceiptString(posRequest,_posResponse,sigResponse);
			PrintReceipt(_receiptStr);
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			int expYear;
			int expMonth;
			if(!VerifyData(out expYear,out expMonth)) {
				Cursor=Cursors.Default;
				return;
			}
			bool isSuccess;
			if(radioWebService.Checked) {
				isSuccess=ProcessPaymentWebService(expYear,expMonth);
			}
			else {
				isSuccess=ProcessPaymentTerminal();
			}
			Cursor=Cursors.Default;
			if(isSuccess) {
				DialogResult=DialogResult.OK;
			}
			else if(!_isAddingCard) {//If adding the card, leave the window open so the user can try again.
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}

	internal class MagstripCardParser {

		private const char TRACK_SEPARATOR='?';
		private const char FIELD_SEPARATOR='^';
		private string _inputStripeStr;
		private string _track1Data;
		private string _track2Data;
		private string _track3Data;
		private bool _needsParsing;
		private bool _hasTrack1;
		private bool _hasTrack2;
		private bool _hasTrack3;
		private string _accountHolder;
		private string _firstName;
		private string _lastName;
		private string _accountNumber;
		private int _expMonth;
		private int _expYear;

		public MagstripCardParser(string trackString) {
			_inputStripeStr=trackString;
			_needsParsing=true;
			Parse();
		}

		#region Properties
		public bool HasTrack1 {
			get { return _hasTrack1; }
		}

		public bool HasTrack2 {
			get { return _hasTrack2; }
		}

		public bool HasTrack3 {
			get { return _hasTrack3; }
		}

		public string Track1 {
			get { return _track1Data; }
		}

		public string Track2 {
			get { return _track2Data; }
		}

		public string Track3 {
			get { return _track3Data; }
		}

		public string TrackData {
			get { return _track1Data+_track2Data+_track3Data; }
		}

		public string AccountName {
			get { return _accountHolder; }
		}

		public string FirstName {
			get { return _firstName; }
		}

		public string LastName {
			get { return _lastName; }
		}

		public string AccountNumber {
			get { return _accountNumber; }
		}

		public int ExpirationMonth {
			get { return _expMonth; }
		}

		public int ExpirationYear {
			get { return _expYear; }
		}
		#endregion

		protected void Parse() {
			if(!_needsParsing) {
				return;
			}
			try {
				//Example: Track 1 Data Only
				//%B1234123412341234^CardUser/John^030510100000019301000000877000000?
				//Key off of the presence of "^" but not "="
				//Example: Track 2 Data Only
				//;1234123412341234=0305101193010877?
				//Key off of the presence of "=" but not "^"
				//Determine the presence of special characters
				string[] tracks=_inputStripeStr.Split(new char[] { TRACK_SEPARATOR },StringSplitOptions.RemoveEmptyEntries);
				if(tracks.Length>0) {
					_hasTrack1=true;
					_track1Data=tracks[0];
				}
				if(tracks.Length>1) {
					_hasTrack2=true;
					_track2Data=tracks[1];
				}
				if(tracks.Length>2) {
					_hasTrack3=true;
					_track3Data=tracks[2];
				}
				if(_hasTrack1) {
					ParseTrack1();
				}
				if(_hasTrack2) {
					ParseTrack2();
				}
				if(_hasTrack3) {
					ParseTrack3();
				}
			}
			catch(MagstripCardParseException) {
				throw;
			}
			catch(Exception ex) {
				throw new MagstripCardParseException(ex);
			}
			_needsParsing=false;
		}

		private void ParseTrack1() {
			if(String.IsNullOrEmpty(_track1Data)) {
				throw new MagstripCardParseException("Track 1 data is empty.");
			}
			string[] parts=_track1Data.Split(new char[] { FIELD_SEPARATOR },StringSplitOptions.None);
			if(parts.Length!=3) {
				throw new MagstripCardParseException("Missing last field separator (^) in track 1 data.");
			}
			_accountNumber=PayConnectUtils.StripNonDigits(parts[0]);
			if(!String.IsNullOrEmpty(parts[1])) {
				_accountHolder=parts[1].Trim();
			}
			if(!String.IsNullOrEmpty(_accountHolder)) {
				int nameDelim=_accountHolder.IndexOf("/");
				if(nameDelim>-1) {
					_lastName=_accountHolder.Substring(0,nameDelim);
					_firstName=_accountHolder.Substring(nameDelim+1);
				}
			}
			//date format: YYMM
			string expDate=parts[2].Substring(0,4);
			_expYear=ParseExpireYear(expDate);
			_expMonth=ParseExpireMonth(expDate);
		}

		private void ParseTrack2() {
			if(String.IsNullOrEmpty(_track2Data)) {
				throw new MagstripCardParseException("Track 2 data is empty.");
			}
			if(_track2Data.StartsWith(";")) {
				_track2Data=_track2Data.Substring(1);
			}
			//may have already parsed this info out if track 1 data present
			if(String.IsNullOrEmpty(_accountNumber) || (_expMonth==0 || _expYear==0)) {
				//Track 2 only cards
				//Ex: ;1234123412341234=0305101193010877?
				int sepIndex=_track2Data.IndexOf('=');
				if(sepIndex<0) {
					throw new MagstripCardParseException("Invalid track 2 data.");
				}
				string[] parts=_track2Data.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries);
				if(parts.Length!=2) {
					throw new MagstripCardParseException("Missing field separator (=) in track 2 data.");
				}
				if(String.IsNullOrEmpty(_accountNumber)) {
					_accountNumber=PayConnectUtils.StripNonDigits(parts[0]);
				}
				if(_expMonth==0 || _expYear==0) {
					//date format: YYMM
					string expDate=parts[1].Substring(0,4);
					_expYear=ParseExpireYear(expDate);
					_expMonth=ParseExpireMonth(expDate);
				}
			}
		}

		private void ParseTrack3() {
			//not implemented
		}

		private int ParseExpireMonth(string s) {
			s=PayConnectUtils.StripNonDigits(s);
			if(!ValidateExpiration(s)) {
				return 0;
			}
			if(s.Length>4) {
				s=s.Substring(0,4);
			}
			return int.Parse(s.Substring(2,2));
		}

		private int ParseExpireYear(string s) {
			s=PayConnectUtils.StripNonDigits(s);
			if(!ValidateExpiration(s)) {
				return 0;
			}
			if(s.Length>4) {
				s=s.Substring(0,4);
			}
			int y=int.Parse(s.Substring(0,2));
			if(y>80) {
				y+=1900;
			}
			else {
				y+=2000;
			}
			return y;
		}

		private bool ValidateExpiration(string s) {
			if(String.IsNullOrEmpty(s)) {
				return false;
			}
			if(s.Length<4) {
				return false;
			}
			return true;
		}

	}

	static class PayConnectUtils {

		public static string GetCardType(string ccNum) {
			if(ccNum==null || ccNum=="") {
				return "";
			}
			ccNum=StripNonDigits(ccNum);
			if(ccNum.StartsWith("4")) {
				return "VISA";
			}
			if(ccNum.StartsWith("5")) {
				return "MASTERCARD";
			}
			if(ccNum.StartsWith("34") || ccNum.StartsWith("37")) {
				return "AMEX";
			}
			if(ccNum.StartsWith("30") || ccNum.StartsWith("36") || ccNum.StartsWith("38")) {
				return "DINERS";
			}
			if(ccNum.StartsWith("6011")) {
				return "DISCOVER";
			}
			return "";
		}

		private static bool IsValidVisaNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16 && len!=13) {
				return false;
			}
			if(number[0]!='4') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidMasterCardNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16) {
				return false;
			}
			if(number[0]!='5' || number[1]=='0' || number[1]>'5') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidAmexNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=15) {
				return false;
			}
			if(number[0]!='3' || (number[1]!='4' && number[1]!='7')) {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidDinersNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=14) {
				return false;
			}
			if(number[0]!='3' || (number[1]!='0' && number[1]!='6' && number[1]!='8') || number[1]=='0' && number[2]>'5') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidDiscoverNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16) {
				return false;
			}
			if(number[0]!='6' || number[1]!='0' || number[2]!='1' || number[3]!='1') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		///<summary>Strips non-digit characters from a string. Returns the modified string, or null if 's' is null.</summary>
		public static string StripNonDigits(string s) {
			return StripNonDigits(s,new char[] { });
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip. The allowed array must contain characters that should not be stripped. Returns the modified string, or null if 's' is null.</summary>
		public static string StripNonDigits(string s,char[] allowed) {
			if(s==null) {
				return null;
			}
			StringBuilder buff=new StringBuilder(s);
			StripNonDigits(buff,allowed);
			return buff.ToString();
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip.</summary>
		public static void StripNonDigits(StringBuilder s) {
			StripNonDigits(s,new char[] { });
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip. The allowed array must contain the characters that should not be stripped.</summary>
		public static void StripNonDigits(StringBuilder s,char[] allowed) {
			for(int i=0;i<s.Length;i++) {
				if(!Char.IsDigit(s[i]) && !ContainsCharacter(s[i],allowed)) {
					s.Remove(i,1);
					i--;
				}
			}
		}

		///<summary>Searches a character array for the presence of the given character. Variable c is the character to search for. The search array is the array to search in. Returns true if the character is present in the array.  false otherwise.</summary>
		public static bool ContainsCharacter(char c,char[] search) {
			foreach(char x in search) {
				if(c==x) {
					return true;
				}
			}
			return false;
		}

		///<summary>Performs a MOD10 check against a string. This is the check that is used to validate credit card numbers, but can be used on other numbers, as well. The algorithm is: Starting with the least significant digit, sum all odd-numbered digits; separately, sum two times each even-numbered digit (if this is greater than 10, bring it into single-digit range by subtracting 9). Then add both totals and divide by 10. If there is no remainder then the value passes the check. The variable value is the value to check, which must be all digits. Returns true iff the value passes the MOD10 check.</summary>
		public static bool CheckMOD10(string value) {
			if(value==null) {
				throw new NullReferenceException("Value is null.");
			}
			value=StripNonDigits(value);
			int sum=0;
			int count=0;
			for(int i=value.Length-1;i>=0;i--) {
				count++;
				int digit=int.Parse(value.Substring(i,1));
				if((count%2)==1) {
					sum+=digit;
				}
				else {
					int tmp=digit*2;
					if(tmp>=10) {
						tmp-=9;
					}
					sum+=tmp;
				}
			}
			return ((sum%10)==0);
		}

		///<summary>Return bool if value passed in is numeric only</summary>
		public static bool IsNumeric(string str) {
			if(str==null) {
				return false;
			}
			Regex objNotWholePattern=new Regex("[^0-9]");
			return !objNotWholePattern.IsMatch(str);
		}

	}

	internal class MagstripCardParseException:Exception {

		public MagstripCardParseException(Exception cause)
			: base(cause.Message,cause) {
		}

		public MagstripCardParseException(string msg)
			: base(msg) {
		}

		public MagstripCardParseException(string msg,Exception cause)
			: base(msg,cause) {
		}

	}
}