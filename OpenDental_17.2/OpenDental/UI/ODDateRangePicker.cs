using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.UI {
	
	public partial class ODDateRangePicker:UserControl {
		
		private bool _enableWeekButtons=true;
		private DateTime _defaultDateTimeFrom=new DateTime(DateTime.Today.Year,1,1);
		private DateTime _defaultDateTimeTo=DateTime.Today;

		#region Properties
		///<summary>Set true to enable butWeekPrevious and butWeekNext</summary>
		[Category("Behavior"), Description("Set true to enable butWeekPrevious and butWeekNext."), DefaultValue(true)]
		public bool EnableWeekButtons {
			get {
				return _enableWeekButtons;
			}
			set {
				_enableWeekButtons=value;
			}
		}

		///<summary>Set true to enable butWeekPrevious and butWeekNext</summary>
		[Category("Behavior"), Description("Set the initial from date for load.")]
		public DateTime DefaultDateTimeFrom {
			get {
				return _defaultDateTimeFrom;
			}
			set {
				_defaultDateTimeFrom=value;
			}
		}
		
		///<summary>Set true to enable butWeekPrevious and butWeekNext</summary>
		[Category("Behavior"), Description("Set the initial to date for load.")]
		public DateTime DefaultDateTimeTo {
			get {
				return _defaultDateTimeTo;
			}
			set {
				_defaultDateTimeTo=value;
			}
		}
		#endregion
		public ODDateRangePicker() {
			InitializeComponent();
		}

		public DateTime GetDateTimeFrom() {
			try {
				return DateTime.Parse(textDateFrom.Text);
			}
			catch(Exception ex) {
				ex.DoNothing();
				return DateTime.MinValue;
			}
		}

		public DateTime GetDateTimeTo() {
			try {
				return DateTime.Parse(textDateTo.Text);
			}
			catch(Exception ex) {
				ex.DoNothing();
				return DateTime.MinValue;
			}
		}

		public void SetDateTimeFrom(DateTime dateTime) {
			textDateFrom.Text=dateTime.ToShortDateString();
		}

		public void SetDateTimeTo(DateTime dateTime) {
			textDateTo.Text=dateTime.ToShortDateString();
		}

		private void ODDateRangePicker_Load(object sender,EventArgs e) {
			if(!_enableWeekButtons) {
				panelWeek.Visible=false;
			}
			textDateFrom.Text=_defaultDateTimeFrom.ToShortDateString();
			textDateTo.Text=_defaultDateTimeTo.ToShortDateString();
			ToggleCalendars();//Close the calendars.
		}

		private void butToggleCalendar_Click(object sender,EventArgs e) {
			ToggleCalendars();
		}

		private void ToggleCalendars() {
			if(calendarFrom.Visible) {
				butDropFrom.ImageIndex=0;//Set to arrow down image.
				butDropTo.ImageIndex=0;//Set to arrow down image.
				//hide the calendars
				calendarFrom.Visible=false;
				calendarTo.Visible=false;
				panelCalendarGap.Visible=false;
				this.Height=this.MinimumSize.Height;
			}
			else {
				butDropFrom.ImageIndex=1;//Set to arrow up image.
				butDropTo.ImageIndex=1;//Set to arrow up image.
				//set the date on the calendars to match what's showing in the boxes
				if(textDateFrom.errorProvider1.GetError(textDateFrom)==""
					&& textDateTo.errorProvider1.GetError(textDateTo)=="") {//if no date errors
					if(textDateFrom.Text=="") {
						calendarFrom.SetDate(DateTime.Today);
					}
					else {
						calendarFrom.SetDate(PIn.Date(textDateFrom.Text));
					}
					if(textDateTo.Text=="") {
						calendarTo.SetDate(DateTime.Today);
					}
					else {
						calendarTo.SetDate(PIn.Date(textDateTo.Text));
					}
				}
				//show the calendars
				calendarFrom.Visible=true;
				calendarTo.Visible=true;
				panelCalendarGap.Visible=true;
				this.Height=this.MaximumSize.Height;
			}
		}
		
		private void calendarFrom_DateSelected(object sender,DateRangeEventArgs e) {
			textDateFrom.Text=calendarFrom.SelectionStart.ToShortDateString();
		}

		private void calendarTo_DateSelected(object sender,DateRangeEventArgs e) {
			textDateTo.Text=calendarTo.SelectionStart.ToShortDateString();
		}

		private void butWeekPrevious_Click(object sender,EventArgs e) {
			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);
			if(dateFrom!=DateTime.MinValue) {
				dateTo=dateFrom.AddDays(-1);
				textDateFrom.Text=dateTo.AddDays(-7).ToShortDateString();
				textDateTo.Text=dateTo.ToShortDateString();
			}
			else if(dateTo!=DateTime.MinValue) {//Invalid dateFrom but valid dateTo
				dateTo=dateTo.AddDays(-8);
				textDateFrom.Text=dateTo.AddDays(-7).ToShortDateString();
				textDateTo.Text=dateTo.ToShortDateString();
			}
			else {//Both dates invalid
				textDateFrom.Text=DateTime.Today.AddDays(-7).ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
			}
			if(calendarFrom.Visible) { //textTo and textFrom are set above, so no check is necessary.
				calendarFrom.SetDate(PIn.Date(textDateFrom.Text));
				calendarTo.SetDate(PIn.Date(textDateTo.Text));
			}
		}

		private void butWeekNext_Click(object sender,EventArgs e) {
			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);
			if(dateTo!=DateTime.MinValue) {
				dateFrom=dateTo.AddDays(1);
				textDateFrom.Text=dateFrom.ToShortDateString();
				textDateTo.Text=dateFrom.AddDays(7).ToShortDateString();
			}
			else if(dateFrom!=DateTime.MinValue) {//Invalid dateTo but valid dateFrom
				 dateFrom=dateFrom.AddDays(8);
				 textDateFrom.Text=dateFrom.ToShortDateString();
				 textDateTo.Text=dateFrom.AddDays(7).ToShortDateString();
			}
			else {//Both dates invalid
				textDateFrom.Text=DateTime.Today.ToShortDateString();
				textDateTo.Text=DateTime.Today.AddDays(7).ToShortDateString();
			}
			if(calendarFrom.Visible) { //textTo and textFrom are set above, so no check is necessary.
				calendarFrom.SetDate(PIn.Date(textDateFrom.Text));
				calendarTo.SetDate(PIn.Date(textDateTo.Text));
			}
		}

	}
}
