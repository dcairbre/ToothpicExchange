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
		private bool IsTabValidSignup() {
			return true;
		}

		private void FillTabSignup() {
			ODException.SwallowAnyException(() => webBrowserSignup.Navigate(_singupOut.SignupPortalUrl));
		}

		private void SaveTabSignup() {

		}

		private void AuthorizeTabSignup(bool allowEdit) {

		}
	}
}
