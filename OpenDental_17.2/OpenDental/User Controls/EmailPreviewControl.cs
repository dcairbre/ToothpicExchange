﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using CodeBase;
using OpenDentBusiness;
using System.Linq;

namespace OpenDental {
	public partial class EmailPreviewControl:UserControl {

		///<summary>TODO: Replace this flag with a new flag on the email address object.</summary>
		private bool _isSigningEnabled=true;
		private bool _hasMessageChanged=false;
		private bool _isLoading=false;
		private bool _isComposing=false;
		private EmailMessage _emailMessage=null;		
		private X509Certificate2 _certSig=null;
		private List<EmailAttach> _listEmailAttachDisplayed=null;
		///<summary>Used when sending to get Clinic.</summary>
		private Patient _patCur=null;
		///<summary>If the message is an html email with images, then this list contains the raw image mime parts.  The user must give permission before converting these to images, for security purposes.  Gmail also does this with images, for example.</summary>
		private List<Health.Direct.Common.Mime.MimeEntity> _listImageParts=null;
    ///<summary>The list of email addresses in the From combobox.</summary>
		private List<EmailAddress> _listEmailAddresses;
		///<summary>Must be set externally before showing this control to the user.</summary>
		public EmailAddress EmailAddressPreview=null;

		public bool HasMessageChanged { get { return _hasMessageChanged; } }
		public bool IsComposing { get { return _isComposing; } }
		public string Subject { get { return textSubject.Text; } set { textSubject.Text=value; } }
		public string BodyText { get { return textBodyText.Text; } set { textBodyText.Text=value; } }
		public string FromAddress { get { return textFromAddress.Text; } }
		public string ToAddress { get { return textToAddress.Text; } set { textToAddress.Text=value; } }
		public string CcAddress { get { return textCcAddress.Text; } set { textCcAddress.Text=value; } }
		public string BccAddress { get { return textBccAddress.Text; } set { textBccAddress.Text=value; } }
		public bool IsSigned { get { return (_isSigningEnabled && _certSig!=null); } }
		public bool HasAttachments { get { return _emailMessage.Attachments.Count>0; } }
		
		public long PatNum {
			get { 
				if(_patCur!=null) {
					return _patCur.PatNum;
				} 
				return 0;
			}
		}
		public long ClinicNum {
			get { 
				if(_patCur!=null) {
					return _patCur.ClinicNum;
				} 
				return 0;
			}
		}

		public X509Certificate2 Signature {
			get {
				if(IsSigned) {
					return _certSig;
				}
				return null;
			}
		}

    ///<summary>Passes back the email address selected in the combobox.  Only used to get the email address to send From in outgoing emails.</summary>
    public EmailAddress GetOutgoingEmailAddress() {
      //the _listEmailAddresses should always be 1:1 with the combobox.
      if(comboEmailFrom.SelectedIndex==-1) {
        return new EmailAddress();
      }
      return _listEmailAddresses[comboEmailFrom.SelectedIndex];
    }

		public EmailPreviewControl() {
			InitializeComponent();
			gridAttachments.ContextMenu=contextMenuAttachments;
		}

    public void LoadEmailMessage(EmailMessage emailMessage) {
			Cursor=Cursors.WaitCursor;
			_emailMessage=emailMessage;
			_patCur=Patients.GetPat(_emailMessage.PatNum);//we could just as easily pass this in.
			if(_emailMessage.SentOrReceived==EmailSentOrReceived.Neither) {//Composing a message
				_isComposing=true;
				if(_isSigningEnabled) {
					SetSig(EmailMessages.GetCertFromPrivateStore(_emailMessage.FromAddress));
				}
			}
			else {//sent or received (not composing)
				//For all email received or sent types, we disable most of the controls and put the window into a mostly read-only state.
				//There is no reason a user should ever edit a received message.
				//The user can copy the content and send a new email if needed (to mimic forwarding until we add the forwarding feature).
				_isComposing=false;
				textMsgDateTime.Text=_emailMessage.MsgDateTime.ToString();
				textMsgDateTime.ForeColor=Color.Black;
				butAttach.Enabled=false;
				textFromAddress.ReadOnly=true;
				textToAddress.ReadOnly=true;
				textCcAddress.ReadOnly=true;
				textBccAddress.ReadOnly=true;
				textSubject.ReadOnly=true;
				textSubject.SpellCheckIsEnabled=false;//Prevents slowness resizing the window, because spell checker runs each time resize event is fired.
				textBodyText.ReadOnly=true;
				textBodyText.SpellCheckIsEnabled=false;//Prevents slowness resizing the window, because spell checker runs each time resize event is fired.
				comboEmailFrom.Visible=false;
				textFromAddress.Width=textCcAddress.Width;//Match the size of Cc Address.
				textFromAddress.Anchor=((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));//Change the anchors to accommodate.
			}
			textSentOrReceived.Text=_emailMessage.SentOrReceived.ToString();
			textFromAddress.Text=_emailMessage.FromAddress;
			textToAddress.Text=_emailMessage.ToAddress;
			textCcAddress.Text=_emailMessage.CcAddress;
			textBccAddress.Text=_emailMessage.BccAddress; //if you send an email to yourself, you'll be able to see everyone in the bcc field.
			textSubject.Text=_emailMessage.Subject;
			textBodyText.Visible=true;
			webBrowser.Visible=false;
			if(EmailMessages.IsReceived(_emailMessage.SentOrReceived)) {
				List<List<Health.Direct.Common.Mime.MimeEntity>> listMimeParts=
					EmailMessages.GetMimePartsForMimeTypes(_emailMessage.RawEmailIn,EmailAddressPreview,"text/html","text/plain","image/");
				List<Health.Direct.Common.Mime.MimeEntity> listHtmlParts=listMimeParts[0];//If RawEmailIn is blank, then this list will also be blank (ex Secure Web Mail messages).
				List<Health.Direct.Common.Mime.MimeEntity> listTextParts=listMimeParts[1];//If RawEmailIn is blank, then this list will also be blank (ex Secure Web Mail messages).
				_listImageParts=listMimeParts[2];//If RawEmailIn is blank, then this list will also be blank (ex Secure Web Mail messages).
				if(listHtmlParts.Count>0) {//Html body found.
					textBodyText.Visible=false;
					_isLoading=true;
					try {
						webBrowser.DocumentText=EmailMessages.ProcessMimeTextPart(listHtmlParts[0]);
					}
					catch(ApplicationException ex) {
						webBrowser.DocumentText="Improperly formatted email. Error displaying email: "+ex.Message;
					}
					webBrowser.Location=textBodyText.Location;
					webBrowser.Size=textBodyText.Size;
					webBrowser.Anchor=textBodyText.Anchor;
					webBrowser.Visible=true;
					if(_listImageParts.Count>0) {
						butShowImages.Visible=true;
					}
				}
				else if(listTextParts.Count>0) {//No html body found, however one specific mime part is for viewing in text only.					
					textBodyText.Text=EmailMessages.ProcessMimeTextPart(listTextParts[0]);
				}
				else {//No html body found and no text body found.  Last resort.  Show all mime parts which are not attachments (ugly).
					textBodyText.Text=_emailMessage.BodyText;//This version of the body text includes all non-attachment mime parts.
				}
			}
			else {//Sent or Unsent/Saved.
				textBodyText.Text=_emailMessage.BodyText;//Show the body text exactly as typed by the user.
			}
			FillAttachments();
			if(IsComposing) {
				FillComboEmail();
			}
			textBodyText.Select();
			Cursor=Cursors.Default;
		}

		private void FillComboEmail() {
      //emails to include: 
      //1. Default Practice/Clinic
      //2. Me
      //3. All other email addresses not tied to a user
      _listEmailAddresses=new List<EmailAddress>();
      EmailAddress emailAddressDefault=EmailAddresses.GetByClinic(ClinicNum);
      EmailAddress emailAddressMe=EmailAddresses.GetForUser(Security.CurUser.UserNum);
      if(emailAddressDefault!=null) {
        _listEmailAddresses.Add(emailAddressDefault);
      }
      if(emailAddressMe!=null) {
        _listEmailAddresses.Add(emailAddressMe);
      }
      foreach (EmailAddress emailCur in EmailAddresses.Listt) {
        if((emailAddressDefault!=null && emailCur.EmailUsername==emailAddressDefault.EmailUsername)
          || (emailAddressMe!=null && emailCur.EmailUsername==emailAddressMe.EmailUsername)) {
          continue;
        }
        _listEmailAddresses.Add(emailCur);
      }
      _listEmailAddresses.ForEach(x => {
        if (emailAddressDefault!=null && x.EmailUsername==emailAddressDefault.EmailUsername) {
          comboEmailFrom.Items.Add(Lan.g(this, "Practice/Clinic") + " <" + x.EmailUsername+">");
        }
        else if (emailAddressMe != null && x.EmailUsername == emailAddressMe.EmailUsername) {
          comboEmailFrom.Items.Add(Lan.g(this, "Me") +" <"+ x.EmailUsername+">");
        }
        else {
          comboEmailFrom.Items.Add(x.EmailUsername);
        }
      });
			//not perfect. Tries to guess what the selected combobox item should be based on the current text in FromAddress.
			for(int i = 0;i < _listEmailAddresses.Count;i++) {
				string senderAddress=_listEmailAddresses[i].SenderAddress.Trim().ToLower();
				string emailUserName=_listEmailAddresses[i].EmailUsername.Trim().ToLower();
				string fromAddress=_emailMessage.FromAddress.Trim().ToLower();
				if((senderAddress!="" && fromAddress.Contains(senderAddress))
					|| (emailUserName!="" && fromAddress.Contains(emailUserName))
					|| (fromAddress!="" && (emailUserName.Contains(fromAddress) || senderAddress.Contains(fromAddress)))) {
					comboEmailFrom.SelectedIndex=i;
				}
			}
			if(!_isComposing || !_isSigningEnabled) {
				return;
			}
      if(emailAddressDefault!=null) {
        SetSig(EmailMessages.GetCertFromPrivateStore(GetOutgoingEmailAddress().EmailUsername));
      }
		}

		private void comboEmailFrom_SelectionChangeCommitted(object sender,EventArgs e) {
      EmailAddress emailAddressSelected=_listEmailAddresses[comboEmailFrom.SelectedIndex];
			textFromAddress.Text=(emailAddressSelected.SenderAddress=="")?emailAddressSelected.EmailUsername:emailAddressSelected.SenderAddress;
			if(!_isComposing || !_isSigningEnabled) {
				return;
			}
			SetSig(EmailMessages.GetCertFromPrivateStore(emailAddressSelected.EmailUsername));
		}

		#region Attachments

		public void FillAttachments() {
			_listEmailAttachDisplayed=new List<EmailAttach>();
			if(!_isComposing) {
				SetSig(null);
			}
			gridAttachments.BeginUpdate();
			gridAttachments.Rows.Clear();
			gridAttachments.Columns.Clear();
			gridAttachments.Columns.Add(new OpenDental.UI.ODGridColumn("",0));//No name column, since there is only one column.
			for(int i=0;i<_emailMessage.Attachments.Count;i++) {
				if(_emailMessage.Attachments[i].DisplayedFileName.ToLower()=="smime.p7s") {
					if(!_isComposing) {
						string smimeP7sFilePath=FileAtoZ.CombinePaths(EmailAttaches.GetAttachPath(),_emailMessage.Attachments[i].ActualFileName);
						string localFile=PrefC.GetRandomTempFile(".p7s");
						FileAtoZ.Copy(smimeP7sFilePath,localFile,FileAtoZSourceDestination.AtoZToLocal);
						SetSig(EmailMessages.GetEmailSignatureFromSmimeP7sFile(localFile));
					}
					//Do not display email signatures in the attachment list, because "smime.p7s" has no meaning to a user
					//Also, Windows will install the smime.p7s into an useless place in the Windows certificate store.
					continue;
				}
				OpenDental.UI.ODGridRow row=new UI.ODGridRow();
				row.Cells.Add(_emailMessage.Attachments[i].DisplayedFileName);
				gridAttachments.Rows.Add(row);
				_listEmailAttachDisplayed.Add(_emailMessage.Attachments[i]);
			}
			gridAttachments.EndUpdate();
			if(gridAttachments.Rows.Count>0) {
				gridAttachments.SetSelected(0,true);
			}
		}

		private void contextMenuAttachments_Popup(object sender,EventArgs e) {
			menuItemOpen.Enabled=false;
			menuItemRename.Enabled=false;
			menuItemRemove.Enabled=false;
			if(gridAttachments.SelectedIndices.Length>0) {
				menuItemOpen.Enabled=true;
			}
			if(gridAttachments.SelectedIndices.Length>0 && _isComposing) {
				menuItemRename.Enabled=true;
				menuItemRemove.Enabled=true;
			}
		}

		private void menuItemOpen_Click(object sender,EventArgs e) {
			OpenFile();
		}

		private void menuItemRename_Click(object sender,EventArgs e) {
			InputBox input=new InputBox(Lan.g(this,"Filename"));
			EmailAttach emailAttach=_listEmailAttachDisplayed[gridAttachments.SelectedIndices[0]];
			input.textResult.Text=emailAttach.DisplayedFileName;
			input.ShowDialog();
			if(input.DialogResult!=DialogResult.OK) {
				return;
			}
			emailAttach.DisplayedFileName=input.textResult.Text;
			FillAttachments();
		}

		private void menuItemRemove_Click(object sender,EventArgs e) {
			EmailAttach emailAttach=_listEmailAttachDisplayed[gridAttachments.SelectedIndices[0]];
			_emailMessage.Attachments.Remove(emailAttach);
			FillAttachments();
		}

		private void gridAttachments_MouseDown(object sender,MouseEventArgs e) {
			//A right click also needs to select an items so that the context menu will work properly.
			if(e.Button==MouseButtons.Right) {
				int clickedIndex=gridAttachments.PointToRow(e.Y);
				if(clickedIndex!=-1) {
					gridAttachments.SetSelected(clickedIndex,true);
				}
			}
		}

		private void gridAttachments_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			OpenFile();
		}

		private void OpenFile() {
			EmailAttach emailAttach=_listEmailAttachDisplayed[gridAttachments.SelectedIndices[0]];
			string strFilePathAttach=FileAtoZ.CombinePaths(EmailAttaches.GetAttachPath(),emailAttach.ActualFileName);
			try {
				if(EhrCCD.IsCcdEmailAttachment(emailAttach)) {
					string strTextXml=FileAtoZ.ReadAllText(strFilePathAttach);
					if(EhrCCD.IsCCD(strTextXml)) {
						Patient patEmail=null;//Will be null for most email messages.
						if(_emailMessage.SentOrReceived==EmailSentOrReceived.ReadDirect || _emailMessage.SentOrReceived==EmailSentOrReceived.ReceivedDirect) {
							patEmail=_patCur;//Only allow reconcile if received via Direct.
						}
						string strAlterateFilPathXslCCD="";
						//Try to find a corresponding stylesheet. This will only be used in the event that the default stylesheet cannot be loaded from the EHR dll.
						for(int i=0;i<_listEmailAttachDisplayed.Count;i++) {
							if(Path.GetExtension(_listEmailAttachDisplayed[i].ActualFileName).ToLower()==".xsl") {
								strAlterateFilPathXslCCD=FileAtoZ.CombinePaths(EmailAttaches.GetAttachPath(),_listEmailAttachDisplayed[i].ActualFileName);
								break;
							}
						}
						FormEhrSummaryOfCare.DisplayCCD(strTextXml,patEmail,strAlterateFilPathXslCCD);
						return;
					}
				}
				else if(IsORU_R01message(strFilePathAttach)) {
					if(DataConnection.DBtype==DatabaseType.Oracle) {
						MsgBox.Show(this,"Labs not supported with Oracle.  Opening raw file instead.");
					}
					else {
						FormEhrLabOrderImport FormELOI =new FormEhrLabOrderImport();
						FormELOI.Hl7LabMessage=FileAtoZ.ReadAllText(strFilePathAttach);
						FormELOI.ShowDialog();
						return;
					}
				}
				FileAtoZ.OpenFile(FileAtoZ.CombinePaths(EmailAttaches.GetAttachPath(),emailAttach.ActualFileName),emailAttach.DisplayedFileName);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butAttach_Click(object sender,EventArgs e) {
			_emailMessage.Attachments.AddRange(EmailAttachL.PickAttachments(_patCur));
			FillAttachments();
		}

		///<summary>Attempts to parse message and detects if it is an ORU_R01 HL7 message.  Returns false if it fails, or does not detect message type.</summary>
		private bool IsORU_R01message(string strFilePathAttach) {
			if(Path.GetExtension(strFilePathAttach) != "txt") {
				return false;
			}
			try {
				string[] ArrayMSHFields=FileAtoZ.ReadAllText(strFilePathAttach).Split(new string[] { "\r\n" },
					StringSplitOptions.RemoveEmptyEntries)[0].Split('|');
				if(ArrayMSHFields[8]!="ORU^R01^ORU_R01") {
					return false;
				}
			}
			catch(Exception ex) {
				ex.DoNothing();
				return false;
			}
			return true;
		}

		#endregion Attachments

		#region Signature

		private void butSig_Click(object sender,EventArgs e) {
			FormEmailDigitalSignature form=new FormEmailDigitalSignature(_certSig);
			if(form.ShowDialog()==DialogResult.OK) {
				//If the user just added trust, then refresh to pull the newly added certificate into the memory cache.
				EmailMessages.RefreshCertStoreExternal(EmailAddressPreview);
			}
		}

		private void SetSig(X509Certificate2 certSig) {
			_certSig=certSig;
			labelSignedBy.Visible=false;
			textSignedBy.Visible=false;
			textSignedBy.Text="";
			butSig.Visible=false;
			textFromAddress.ReadOnly=false;
			if(certSig!=null) {
				labelSignedBy.Visible=true;
				textSignedBy.Visible=true;
				textSignedBy.Text=EmailNameResolver.GetCertSubjectName(certSig);
				//Show the user that, if the message is signed, then the sender will always look like the address on the certificate,
				//even if they have a Sender Address setup.  Otherwise we would be misrepresenting how the Sender Address feature works.
				textFromAddress.Text=textSignedBy.Text;
				textFromAddress.ReadOnly=true;
				butSig.Visible=true;
			}
		}

		#endregion Signature

		#region Body

		public void LoadTemplate(string subject,string bodyText,List<EmailAttach> attachments) {
			List<Appointment> listApts=Appointments.GetFutureSchedApts(PatNum);
			Appointment aptNext=null;
			if(listApts.Count > 0){
				aptNext=listApts[0]; //next sched appt. If none, null.
			}
			Clinic clinic=Clinics.GetClinic(ClinicNum);
			Subject=subject;
			//patient information
			Subject=FormMessageReplacements.ReplacePatient(Subject,_patCur);
			//Next Scheduled Appointment Information
			Subject=FormMessageReplacements.ReplaceAppointment(Subject,aptNext); //handles null nextApts.
			//Currently Logged in User Information
			Subject=FormMessageReplacements.ReplaceUser(Subject,Security.CurUser);
			//Clinic Information
			Subject=FormMessageReplacements.ReplaceOffice(Subject,clinic);
			//Misc Information
			Subject=FormMessageReplacements.ReplaceMisc(Subject);
			BodyText=bodyText;
			//patient information
			BodyText=FormMessageReplacements.ReplacePatient(BodyText,_patCur);
			//Next Scheduled Appointment Information
			BodyText=FormMessageReplacements.ReplaceAppointment(BodyText,aptNext); //handles null nextApts.
			//Currently Logged in User Information
			BodyText=FormMessageReplacements.ReplaceUser(BodyText,Security.CurUser);
			//Clinic Information
			BodyText=FormMessageReplacements.ReplaceOffice(BodyText,clinic);
			//Misc Information
			BodyText=FormMessageReplacements.ReplaceMisc(BodyText);
			_emailMessage.Attachments.AddRange(attachments);
			FillAttachments();
			_hasMessageChanged=false;
		}

		private void butShowImages_Click(object sender,EventArgs e) {
			try {
				//We need a folder in order to place the images beside the html file in order for the relative image paths to work correctly.
				string htmlFolderPath=ODFileUtils.CreateRandomFolder(PrefC.GetTempFolderPath());//Throws exceptions.
				string filePathHtml=ODFileUtils.CreateRandomFile(htmlFolderPath,".html");
				string html=webBrowser.DocumentText;
				for(int i=0;i<_listImageParts.Count;i++) {
					string contentId=EmailMessages.GetMimeImageContentId(_listImageParts[i]);
					string fileName=EmailMessages.GetMimeImageFileName(_listImageParts[i]);
					html=html.Replace("cid:"+contentId,fileName);
					EmailMessages.SaveMimeImageToFile(_listImageParts[i],htmlFolderPath);
				}
				File.WriteAllText(filePathHtml,html);
				_isLoading=true;
				webBrowser.Navigate(filePathHtml);
				butShowImages.Visible=false;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.ToString());
			}
		}

		private void textBodyText_TextChanged(object sender,EventArgs e) {
			_hasMessageChanged=true;
		}

		private void webBrowser_Navigating(object sender,WebBrowserNavigatingEventArgs e) {
			if(_isLoading) {
				return;
			}
			e.Cancel=true;//Cancel browser navigation (for links clicked within the email message).
			Process.Start(e.Url.ToString());//Instead launch the URL into a new default browser window.
		}

		private void webBrowser_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			_isLoading=false;
		}

		#endregion Body

		///<summary>Saves the UI input values into the emailMessage.  Allowed to save message with invalid fields, so no validation here.</summary>
		public void SaveMsg(EmailMessage emailMessage) {
			emailMessage.FromAddress=textFromAddress.Text;
			emailMessage.ToAddress=textToAddress.Text;
			emailMessage.CcAddress=textCcAddress.Text;
			emailMessage.BccAddress=textBccAddress.Text;
			emailMessage.Subject=textSubject.Text;
			emailMessage.BodyText=textBodyText.Text;
			emailMessage.MsgDateTime=DateTime.Now;
			emailMessage.SentOrReceived=_emailMessage.SentOrReceived;//Status does not ever change.
		}

	}
}
