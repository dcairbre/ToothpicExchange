/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
///<summary></summary>
	public class FormDocSign:ODForm {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;//required by designer
		//<summary></summary>
		//public bool IsNew;
		//private Patient PatCur;
		private TextBox textNote;
		private Label label8;
		private Label label15;
		private Document DocCur;
		///<summary>This keeps the noteChanged event from erasing the signature when first loading.</summary>
		private bool IsStartingUp;
		private bool SigChanged;
		private Patient PatCur;
		private UI.SignatureBoxWrapper signatureBoxWrapper;
		private string PatFolder;
		
		///<summary></summary>
		public FormDocSign(Document docCur,Patient pat) {
			InitializeComponent();
			DocCur=docCur;
			PatCur=pat;
			PatFolder=ImageStore.GetPatientFolder(pat,ImageStore.GetPreferredAtoZpath());
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
			this.textNote = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.SuspendLayout();
			// 
			// textNote
			// 
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textNote.Location = new System.Drawing.Point(39, 0);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(302, 85);
			this.textNote.TabIndex = 17;
			this.textNote.TextChanged += new System.EventHandler(this.textNote_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(0, 2);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33, 45);
			this.label8.TabIndex = 16;
			this.label8.Text = "Note";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(370, 2);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(85, 25);
			this.label15.TabIndex = 88;
			this.label15.Text = "Signature";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(837, 50);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(837, 19);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.Location = new System.Drawing.Point(457, 0);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.Size = new System.Drawing.Size(362, 79);
			this.signatureBoxWrapper.TabIndex = 182;
			this.signatureBoxWrapper.SignatureChanged += new System.EventHandler(this.signatureBoxWrapper_SignatureChanged);
			// 
			// FormDocSign
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(913, 84);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label8);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDocSign";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Signature";
			this.Load += new System.EventHandler(this.FormDocSign_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary></summary>
		public void FormDocSign_Load(object sender, System.EventArgs e){
			IsStartingUp=true;
			textNote.Text=DocCur.Note;
			signatureBoxWrapper.SignatureMode=UI.SignatureBoxWrapper.SigMode.Document;
			string keyData=ImageStore.GetHashString(DocCur,PatFolder);
			signatureBoxWrapper.FillSignature(DocCur.SigIsTopaz,keyData,DocCur.Signature);
			IsStartingUp=false;
		}

		private void textNote_TextChanged(object sender,EventArgs e) {
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				signatureBoxWrapper.ClearSignature();
				//this will call OnSignatureChanged to set UserNum, textUser, and SigChanged
			}
		}

		private void SaveSignature() {
			if(SigChanged) {
				string keyData=ImageStore.GetHashString(DocCur,PatFolder);
				DocCur.Signature=signatureBoxWrapper.GetSignature(keyData);
				DocCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
			}
		}

		private void signatureBoxWrapper_SignatureChanged(object sender,EventArgs e) {
			SigChanged=true;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			DocCur.Note=textNote.Text;
			SaveSignature();
			Documents.Update(DocCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}