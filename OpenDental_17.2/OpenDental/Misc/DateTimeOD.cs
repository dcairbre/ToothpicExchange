using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDental {
	public class DateTimeOD {
		private int _yearsDiff;
		private int _monthsDiff;
		private int _daysDiff;

		public int YearsDiff {
			get {
				return _yearsDiff;
			}
		}

		public int MonthsDiff{
			get {
				return _monthsDiff;
			}
		}

		public int DaysDiff{
			get {
				return _daysDiff;
			}
		}

		///<summary>We are switching to using this method instead of DateTime.Today.  You can track actual Year/Month/Date differences by creating an 
		///instance of this class and passing in the two dates to compare.  The values will be stored in YearsDiff, MonthsDiff, and DaysDiff.</summary> 
		public static DateTime Today {
			//The problem is with dotNet serilazation to the middle tier.  It will tack on zulu change for UTC.  Like this:
			//2013-04-29T04:00:00-7:00
			//DateTime objects created with DateTimeKind.Local in one timezone and sent over the middle tier to another time zone will be different at their 
			//destination, because .NET will automatically try to adjust for the timezone change.
			//DateTime.Today uses DateTimeKind.Local and we want DateTimeKind.Unspecified.
			//DateTime DateTimeToday=DateTime.Today;//DateTimeKind.Local, so serialization seems attempt to convert it to z.
			//DateTime DateTimeSpecific=new DateTime(2013,4,29);//DateTimeKind.Unspecified.
			//DateTime DateTimeParsed=DateTime.Parse("4/29/2013");//DateTimeKind.Unspecified.
			get {
				return new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,0,0,0,DateTimeKind.Unspecified);//Today at midnight with no timezone information.
			}
		}

		///<summary>Pass in the two dates that you want to compare. Results will be stored in YearsDiff, MonthsDiff, and DaysDiff.
		///Always subtracts the smaller date from the larger date to return a positive (or 0) value.</summary>
		public DateTimeOD(DateTime date1,DateTime date2) {
			DateTime beforeDate;
			DateTime afterDate;
			if(date1 <= date2) {
				beforeDate = date1;
				afterDate = date2;
			}
			else {
				beforeDate = date2;
				afterDate = date1;
			}
			GetYears(beforeDate,afterDate); // Get the Number of Years Difference between two dates
			GetMonths(beforeDate.AddYears(YearsDiff),afterDate); // Getting the Number of Months Difference but using the Years difference earlier
			GetDays(beforeDate.AddYears(YearsDiff).AddMonths(MonthsDiff),afterDate); // Getting the Number of Days Difference but using Years and Months difference earlier
		}

		///<summary>Gets the number of years between the two passed-in dates.</summary>
		private void GetYears(DateTime startDate,DateTime endDate) {
			int years=0;
			while(endDate >= startDate.AddYears(years)) { //calculate the number of years between the two dates.
				years++;
			}
			_yearsDiff=years-1; //subtract 1 to always round down to the nearest year, since partial years are covered by months and days.

		}

		private void GetMonths(DateTime startDate,DateTime endDate) {
			int months=0;
			while(endDate >= startDate.AddMonths(months)) { //calculate the number of months between the two dates.
				months++;
			}
			_monthsDiff=months-1; //subtract 1 to always round down to the nearest month, since partial months are covered by days.

		}

		private void GetDays(DateTime startDate,DateTime endDate) {
			int days=0;
			while(endDate > startDate.AddDays(days)) { //calculate the number of days between the two dates.
				days++;
			}
			_daysDiff=days; //do not subtract 1 (always round up) since days are the smallest increment of time used.

		}

		///<summary>Returns the most recent valid date possible based on the year and month passed in.
		///E.g. y:2017,m:4,d:31 is passed in (an invalid date) which will return a date of "04/30/2017" which is the most recent 'valid' date.
		///Throws an exception if the year is not between 1 and 9999, and if the month is not between 1 and 12.</summary>
		public static DateTime GetMostRecentValidDate(int year,int month,int day) {
			int maxDay=DateTime.DaysInMonth(year,month);
			return new DateTime(year,month,Math.Min(day,maxDay));
		}


	}
}
