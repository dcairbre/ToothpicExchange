using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeBase;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	///<summary>Internet browser window for NewCrop.  This is essentially a Microsoft Internet Explorer control embedded into our form.
	///The browser.ScriptErrorsSuppressed is true in order to prevent javascript error popups from annoying the user.</summary>
	public class FormErx:FormWebBrowser {
		///<summary>The PatNum of the patient eRx was opened for.  The patient is tied to the window so that when the window is closed the Chart
		///knows which patient to refresh.  If the patient is different than the patient modified in the eRx window then the Chart does not need to 
		///refresh.</summary>
		public Patient PatCur=null;
		///<summary>This XML contains the patient information, provider information, employee information, practice information, etc...</summary>
		public string ClickThroughXml="";

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			ComposeNewRx();
		}

		///<summary>Sends the ClickThroughXml to eRx and loads the result within the browser control.
		///Loads the compose tab in NewCrop's web interface.  Can be called externally to send provider information to eRx
		///without allowing the user to write any prescriptions.</summary>
		public void ComposeNewRx() {
			string xmlBase64=System.Web.HttpUtility.HtmlEncode(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(ClickThroughXml)));
			xmlBase64=xmlBase64.Replace("+","%2B");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("/","%2F");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("=","%3D");//Base 64 strings usually end in '=', but parameters also use '=' so we must escape.
			String postdata="RxInput=base64:"+xmlBase64;
			byte[] arrayPostDataBytes=System.Text.Encoding.UTF8.GetBytes(postdata);
			string additionalHeaders="Content-Type: application/x-www-form-urlencoded\r\n";
#if DEBUG
			string newCropUrl="http://preproduction.newcropaccounts.com/interfaceV7/rxentry.aspx";
#else //Debug
			string newCropUrl="https://secure.newcropaccounts.com/interfacev7/rxentry.aspx";
#endif
			browser.Navigate(newCropUrl,"",arrayPostDataBytes,additionalHeaders);
		}

		protected override void SetTitle() {
			Text=Lan.g(this,"eRx");
			if(browser.DocumentTitle.Trim()!="") {
				Text+=" - "+browser.DocumentTitle;
			}
			if(PatCur!=null) {//Can only be null when a subwindow is opened by clicking on a link from inside another FormErx instance.
				Text+=" - "+PatCur.GetNameFL();
			}
		}

		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);
			ODEvent.Fire(new ODEventArgs("ErxBrowserClosed",PatCur));
		}

	}
}