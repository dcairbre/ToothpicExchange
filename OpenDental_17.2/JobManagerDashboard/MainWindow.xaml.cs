using OpenDentBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JobManagerDashboard {
	/// <summary>
	/// Interaction logic for JobManagerDashboard.xaml
	/// </summary>
	public partial class JobManagerDashboardTiles : Window {

		private List<string> _listActualEngineers=new List<string>{"Allen","Andrew","BrendanB","Cameron","Chris","David","Derek","Jason","Joe","Josh","KendraS","Lindsay","Matherin","Sam","Saul","Travis"};
		List<EngInformation> _engInfoList=new List<EngInformation>();
		List<Job> _listJobsAll=Jobs.GetAll();
		
		public JobManagerDashboardTiles() {
			InitializeComponent();
			List<Job> listWriteCodeJobs=_listJobsAll.Where(x => x.OwnerAction==JobAction.WriteCode).ToList();
			List<Job> unfinishedQuoteJobs=Jobs.GetAll().Where(x=>x.PhaseCur!=JobPhase.Complete).ToList();
			_engInfoList.Add(
				new EngInformation {
					EngName="Total Unfinished Quote Jobs: ",
					EngClockStatus="Total Unfinished Quote $: ",
					EngWorkStatus="Total Unfinished Jobs:	",
					StatField1="Total Jobs being worked on: "
				});
			foreach(Userod user in Userods.GetUsersByJobRole(JobPerm.Engineer,false)) {
				if(!_listActualEngineers.Contains(user.UserName)) {
					continue;
				}
				List<TextBlock> jobTitles=new List<TextBlock>();
				List<string> listEngJobs=listWriteCodeJobs.Where(x => x.UserNumEngineer==user.UserNum).Select(x => x.Title).ToList();
				foreach(string job in listEngJobs){
					//TODO: Template this in xaml and pass in the object for the template to autogen
					TextBlock tb=new TextBlock();
					tb.Text=job;
					tb.TextWrapping=TextWrapping.WrapWithOverflow;
					jobTitles.Add(tb);
				}
				_engInfoList.Add(new EngInformation { EngName=user.UserName,EngClockStatus="Available",EngWorkStatus="Needs Work",EngJobs=jobTitles });
			}
			EngTiles.ItemsSource=_engInfoList;
		}

		//This class represents the model needed to fill the desired information
		public class EngInformation{
			public string EngName { get; set; }
			public string EngClockStatus { get; set; }
			public string EngWorkStatus { get; set; }
			public List<TextBlock> EngJobs { get; set; }
			//only used for the main tile that appears first in the collection
			public string StatField1 { get; set; }
		}
	}
}
