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
		private bool IsTabValidMisc() {
			return true;
		}

		private void FillTabMisc() {
			//.NET has a bug in the DateTimePicker control where the text will not get updated and will instead default to showing DateTime.Now.
			//In order to get the control into a mode where it will display the correct value that we set, we need to set the property Checked to true.
			//Today's date will show even when the property is defaulted to true (via the designer), so we need to do it programmatically right here.
			//E.g. set your computer region to Assamese (India) and the DateTimePickers on the Automation Setting tab will both be set to todays date
			// if the tab is NOT set to be the first tab to display (don't ask me why it works then).
			//This is bad for our customers because setting both of the date pickers to the same date and time will cause automation to stop.
			dateRunStart.Checked=true;
			dateRunEnd.Checked=true;
			//Now that the DateTimePicker controls are ready to display the DateTime we set, go ahead and set them.
			//If loading the picker controls with the DateTime fields from the database failed, the date picker controls default to 7 AM and 10 PM.
			ODException.SwallowAnyException(() => {
				dateRunStart.Value=PrefC.GetDateT(PrefName.AutomaticCommunicationTimeStart);
				dateRunEnd.Value=PrefC.GetDateT(PrefName.AutomaticCommunicationTimeEnd);
			});
		}

		private void SaveTabMisc() {
			Prefs.UpdateDateT(PrefName.AutomaticCommunicationTimeStart,dateRunStart.Value);
			Prefs.UpdateDateT(PrefName.AutomaticCommunicationTimeEnd,dateRunEnd.Value);
		}

		private void AuthorizeTabMisc(bool allowEdit) {
			dateRunStart.Enabled=allowEdit;
			dateRunEnd.Enabled=allowEdit;
		}

		private void butShowOldMobileSych_Click(object sender,EventArgs e) {
			butShowOldMobileSych.Enabled=false;
			tabControl.TabPages.Add(tabMobileSynch);
			tabControl.SelectedTab=tabMobileSynch;
		}
	}
}
