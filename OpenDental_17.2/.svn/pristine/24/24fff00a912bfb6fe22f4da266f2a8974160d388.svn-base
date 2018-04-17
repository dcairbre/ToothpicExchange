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
		private bool IsTabValidPatientPortal() {
#if !DEBUG
			if(!textPatientFacingUrlPortal.Text.ToUpper().StartsWith("HTTPS")) {
				MsgBox.Show(this,"Patient Facing URL must start with HTTPS.");
				return false;
			}
#endif
			if(textBoxNotificationSubject.Text=="") {
				MsgBox.Show(this,"Notification Subject is empty");
				textBoxNotificationSubject.Focus();
				return false;
			}
			if(textBoxNotificationBody.Text=="") {
				MsgBox.Show(this,"Notification Body is empty");
				textBoxNotificationBody.Focus();
				return false;
			}
			if(!textBoxNotificationBody.Text.Contains("[URL]")) { //prompt user that they omitted the URL field but don't prevent them from continuing
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"[URL] not included in notification body. Continue without setting the [URL] field?")) {
					textBoxNotificationBody.Focus();
					return false;
				}
			}
			return true;
		}

		private void FillTabPatientPortal() {
			//Office may have set a customer URL
			textPatientFacingUrlPortal.Text=PrefC.GetString(PrefName.PatientPortalURL);
			//HQ provides this URL for this customer.
			string urlFromHQ=(
				GetSignups<WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService>(eServiceCode.PatientPortal).FirstOrDefault()??
				new WebServiceMainHQProxy.EServiceSetup.SignupOut.SignupOutEService() { HostedUrl="" }
			).HostedUrl;
			textHostedUrlPortal.Text=urlFromHQ;
			if(textPatientFacingUrlPortal.Text=="") { //Customer has not set their own URL so use the URL provided by OD.
				textPatientFacingUrlPortal.Text=urlFromHQ;
			}
			textBoxNotificationSubject.Text=PrefC.GetString(PrefName.PatientPortalNotifySubject);
			textBoxNotificationBody.Text=PrefC.GetString(PrefName.PatientPortalNotifyBody);
		}

		private void SaveTabPatientPortal() {
			Prefs.UpdateString(PrefName.PatientPortalURL,textPatientFacingUrlPortal.Text);
			Prefs.UpdateString(PrefName.PatientPortalNotifySubject,textBoxNotificationSubject.Text);
			Prefs.UpdateString(PrefName.PatientPortalNotifyBody,textBoxNotificationBody.Text);
		}

		private void AuthorizeTabPatientPortal(bool allowEdit) {
			groupBoxNotification.Enabled=allowEdit;
			textPatientFacingUrlPortal.Enabled=allowEdit;
		}
	}
}
