using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.ReportingComplex;
using OpenDentBusiness;
using System.Linq;
using CodeBase;

namespace OpenDental {
	public partial class FormRpPresentedTreatmentProduction:ODForm {
		private List<Userod> _listUsers;
		private List<Clinic> _listClinics;
		public FormRpPresentedTreatmentProduction() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRpTreatPlanPresenter_Load(object sender,EventArgs e) {
			date1.SelectionStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddMonths(-1);
			date2.SelectionStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddDays(-1);
			_listUsers=UserodC.ShortList;
			listUser.Items.AddRange(_listUsers.Select(x => x.UserName).ToArray());
			checkAllUsers.Checked=true;
			if(PrefC.HasClinicsEnabled) {
				if(!Security.CurUser.ClinicIsRestricted) {
					listClin.Items.Add(Lan.g(this,"Unassigned"));
				}
				_listClinics=Clinics.GetForUserod(Security.CurUser);
				listClin.Items.AddRange(_listClinics.Select(x => x.Abbr).ToArray());
				checkAllClinics.Checked=true;
			}
			else {
				listClin.Visible=false;
				checkAllClinics.Visible=false;
				labelClin.Visible=false;
				groupType.Location=new Point(185,225);
				groupOrder.Location=new Point(185,295);
				groupUser.Location=new Point(185,365);
				listUser.Width+=30;
			}
		}

		private void RunTotals(List<long> listUserNums,List<long> listClinicsNums) {
			ReportComplex report=new ReportComplex(true,false);
			report.AddTitle("Title",Lan.g(this,"Presented Treatment Production"));
			report.AddSubTitle("SubTitle","Totals Report");
			report.AddSubTitle("PracTitle",PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle("Date",date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			if(checkAllUsers.Checked) {
				report.AddSubTitle("Users",Lan.g(this,"All Users"));
			}
			else {
				string strUsers="";
				for(int i = 0;i < listUser.SelectedIndices.Count;i++) {
					if(i == 0) {
						strUsers=_listUsers[listUser.SelectedIndices[i]].UserName;
					}
					else {
						strUsers+=", "+_listUsers[listUser.SelectedIndices[i]].UserName;
					}
				}
				report.AddSubTitle("Users",strUsers);
			}
			if(PrefC.HasClinicsEnabled) {
				if(checkAllClinics.Checked) {
					report.AddSubTitle("Clinics",Lan.g(this,"All Clinics"));
				}
				else {
					string clinNames="";
					for(int i = 0;i<listClin.SelectedIndices.Count;i++) {
						if(i>0) {
							clinNames+=", ";
						}
						if(Security.CurUser.ClinicIsRestricted) {
							clinNames+=_listClinics[listClin.SelectedIndices[i]].Abbr;
						}
						else {
							if(listClin.SelectedIndices[i]==0) {
								clinNames+=Lan.g(this,"Unassigned");
							}
							else {
								clinNames+=_listClinics[listClin.SelectedIndices[i]-1].Abbr;//Minus 1 from the selected index
							}
						}
					}
					report.AddSubTitle("Clinics",clinNames);
				}
			}
			List<TreatPlanPresenterEntry> listTreatPlanPresenterEntries=GetListTreatPlanPresenterEntries(listClinicsNums);
			listTreatPlanPresenterEntries=listTreatPlanPresenterEntries
				.Where(x => _listUsers.Select(y => y.UserNum).Contains(x.UserNumPresenter))
				.OrderBy(x => x.Presenter).ThenBy(x => x.DatePresented).ToList();
			DataTable table = new DataTable();
			table.Columns.Add("Presenter");
			table.Columns.Add("# of Procs");
			table.Columns.Add("GrossProd");
			table.Columns.Add("WriteOffs");
			table.Columns.Add("Adjustments");
			table.Columns.Add("NetProduction");
			listTreatPlanPresenterEntries.GroupBy(x => x.Presenter).ToList().ForEach(x => {
				DataRow row = table.NewRow();
				row["Presenter"] = x.Select(y => y.Presenter).First() == "" ? "None" : x.Select(y => y.Presenter).First();
				row["# of Procs"] = x.Count();
				row["GrossProd"] = x.Sum(y => y.GrossProd);
				row["WriteOffs"] = x.Sum(y => y.WriteOffs);
				row["Adjustments"] = x.Sum(y => y.Adjustments);
				row["NetProduction"] = x.Sum(y => y.NetProd);
				table.Rows.Add(row);
			});
			QueryObject query=report.AddQuery(table,"","",SplitByKind.None,1,true);
			query.AddColumn(Lan.g(this,"Presenter"),100,FieldValueType.String);
			query.AddColumn(Lan.g(this,"# of Procs"),70,FieldValueType.Integer);
			query.AddColumn(Lan.g(this,"GrossProd"),100,FieldValueType.Number);
			query.AddColumn(Lan.g(this,"WriteOffs"),100,FieldValueType.Number);
			query.AddColumn(Lan.g(this,"Adjustments"),100,FieldValueType.Number);
			query.AddColumn(Lan.g(this,"NetProduction"),100,FieldValueType.Number);
			if(!report.SubmitQueries()) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportComplex FormR=new FormReportComplex(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void RunDetailed(List<long> listUserNums,List<long> listClinicsNums) {
			ReportComplex report=new ReportComplex(true,false);
			report.AddTitle("Title",Lan.g(this,"Presented Treatment Production"));
			report.AddSubTitle("SubTitle", "Detailed Report");
			report.AddSubTitle("PracTitle",PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle("Date",date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			if(checkAllUsers.Checked) {
				report.AddSubTitle("Users",Lan.g(this,"All Users"));
			}
			else {
				string strUsers="";
				for(int i = 0;i < listUser.SelectedIndices.Count;i++) {
					if(i == 0) {
						strUsers=_listUsers[listUser.SelectedIndices[i]].UserName;
					}
					else {
						strUsers+=", "+_listUsers[listUser.SelectedIndices[i]].UserName;
					}
				}
				report.AddSubTitle("Users",strUsers);
			}
			if(PrefC.HasClinicsEnabled) {
				if(checkAllClinics.Checked) {
					report.AddSubTitle("Clinics",Lan.g(this,"All Clinics"));
				}
				else {
					string clinNames="";
					for(int i = 0;i<listClin.SelectedIndices.Count;i++) {
						if(i>0) {
							clinNames+=", ";
						}
						if(Security.CurUser.ClinicIsRestricted) {
							clinNames+=_listClinics[listClin.SelectedIndices[i]].Abbr;
						}
						else {
							if(listClin.SelectedIndices[i]==0) {
								clinNames+=Lan.g(this,"Unassigned");
							}
							else {
								clinNames+=_listClinics[listClin.SelectedIndices[i]-1].Abbr;//Minus 1 from the selected index
							}
						}
					}
					report.AddSubTitle("Clinics",clinNames);
				}
			}
			List<TreatPlanPresenterEntry> listTreatPlanPresenterEntries=GetListTreatPlanPresenterEntries(listClinicsNums);
			listTreatPlanPresenterEntries=listTreatPlanPresenterEntries
				.Where(x => listUserNums.Contains(x.UserNumPresenter))
				.OrderBy(x => x.Presenter).ThenBy(x => x.DatePresented).ToList();
			DataTable tableReport = new DataTable();
			tableReport.Columns.Add("Presenter");
			tableReport.Columns.Add("DatePresented");
			tableReport.Columns.Add("DateCompleted");
			tableReport.Columns.Add("Descript");
			tableReport.Columns.Add("GrossProd");
			tableReport.Columns.Add("WriteOffs");
			tableReport.Columns.Add("Adjustments");
			tableReport.Columns.Add("NetProduction");
			foreach(TreatPlanPresenterEntry presenterEntry in listTreatPlanPresenterEntries) {
				DataRow row = tableReport.NewRow();
				row["Presenter"] = presenterEntry.Presenter==""?"None":presenterEntry.Presenter;
				row["DatePresented"] = presenterEntry.DatePresented;
				row["DateCompleted"] = presenterEntry.DateCompleted;
				row["Descript"] = presenterEntry.ProcDescript;
				row["GrossProd"] = presenterEntry.GrossProd;
				row["WriteOffs"] = presenterEntry.WriteOffs;
				row["Adjustments"] = presenterEntry.Adjustments;
				row["NetProduction"] = presenterEntry.NetProd;
				tableReport.Rows.Add(row);
			}
			QueryObject query=report.AddQuery(tableReport,"","",SplitByKind.None,1,true);
			query.AddColumn("\r\n"+Lan.g(this,"Presenter"),90,FieldValueType.String);
			query.AddColumn(Lan.g(this,"Date")+"\r\n"+Lan.g(this,"Presented"),75,FieldValueType.Date);
			query.AddColumn(Lan.g(this,"Date")+"\r\n"+Lan.g(this,"Completed"),75,FieldValueType.Date);
			query.AddColumn("\r\n"+Lan.g(this,"Descript"),200,FieldValueType.String);
			query.AddColumn("\r\n"+Lan.g(this,"GrossProd"),90,FieldValueType.Number);
			query.AddColumn("\r\n"+Lan.g(this,"WriteOffs"),90,FieldValueType.Number);
			query.AddColumn("\r\n"+Lan.g(this,"Adjustments"),90,FieldValueType.Number);
			query.AddColumn("\r\n"+Lan.g(this,"NetProduction"),90,FieldValueType.Number);
			if(!report.SubmitQueries()) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportComplex FormR=new FormReportComplex(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private List<TreatPlanPresenterEntry> GetListTreatPlanPresenterEntries(List<long> listClinicNums) {
			List<Procedure> listProcsComplete = Procedures.GetCompletedForDateRangeLimited(date1.SelectionStart,date2.SelectionStart,listClinicNums);
			List<ProcTP> listProcTPs = ProcTPs.GetForProcs(listProcsComplete.Select(x => x.ProcNum).ToList());
			List<Procedure> listTreatPlanProcs = listProcsComplete.Where(x => listProcTPs.Select(y => y.ProcNumOrig).Contains(x.ProcNum)).ToList();
			List<TreatPlan> listSavedTreatPlans = TreatPlans.GetFromProcTPs(listProcTPs); // attached proctps to treatment plans.
			List<ClaimProc> listClaimProcs = ClaimProcs.GetForProcsLimited(listTreatPlanProcs.Select(x => x.ProcNum).ToList(),
				ClaimProcStatus.Received,ClaimProcStatus.Supplemental,ClaimProcStatus.CapComplete,ClaimProcStatus.NotReceived);
			List<Adjustment> listAdjustments = Adjustments.GetForProcs(listTreatPlanProcs.Select(x => x.ProcNum).ToList());
			List<Userod> listUserods = Userods.GetAll();
			List<TreatPlanPresenterEntry> listTreatPlanPresenterEntries = new List<TreatPlanPresenterEntry>();
			List<ProcedureCode> listProcCodes = ProcedureCodes.GetCodesForCodeNums(listTreatPlanProcs.Select(x => x.CodeNum).ToList());
			foreach(Procedure procCur in listTreatPlanProcs) {
				double grossProd = procCur.ProcFee * (procCur.UnitQty + procCur.BaseUnits);
				double writeOffs = listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum)
						.Where(x => x.Status == ClaimProcStatus.CapComplete)
						.Sum(x => x.WriteOff);
				grossProd-=writeOffs;
				writeOffs = listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum)
					.Where(x => x.Status == ClaimProcStatus.NotReceived
						|| x.Status == ClaimProcStatus.Received
						|| x.Status == ClaimProcStatus.Supplemental)
					.Sum(x => x.WriteOff);
				double adjustments = listAdjustments.Where(x => x.ProcNum == procCur.ProcNum).Sum(x => x.AdjAmt);
				double netProd = grossProd - writeOffs + adjustments;
				TreatPlan treatPlanCur;
				if(radioFirstPresented.Checked) {
					treatPlanCur = listSavedTreatPlans.Where(x => x.ListProcTPs.Any(y => y.ProcNumOrig==procCur.ProcNum)).OrderBy(x => x.DateTP).First();
				}
				else { //radioLastPresented
					treatPlanCur = listSavedTreatPlans.Where(x => x.ListProcTPs.Any(y => y.ProcNumOrig==procCur.ProcNum)).OrderByDescending(x => x.DateTP).First();
				}
				Userod userPresenter;
				if(radioPresenter.Checked) {
					userPresenter = listUserods.FirstOrDefault(x => x.UserNum == treatPlanCur.UserNumPresenter);
				}
				else { //radioEntryUser
					userPresenter = listUserods.FirstOrDefault(x => x.UserNum == treatPlanCur.SecUserNumEntry);
				}
				ProcedureCode procCode = listProcCodes.First(x => x.CodeNum == procCur.CodeNum);
				listTreatPlanPresenterEntries.Add(new TreatPlanPresenterEntry() {
					Presenter = userPresenter==null?"":userPresenter.UserName,
					DatePresented = treatPlanCur.DateTP,
					DateCompleted = procCur.ProcDate,
					ProcDescript = procCode.Descript,
					GrossProd = grossProd,
					Adjustments = adjustments,
					WriteOffs = writeOffs,
					NetProd = netProd,
					UserNumPresenter = userPresenter==null ? 0 : userPresenter.UserNum,
					PresentedClinic = procCur.ClinicNum
				});
			}
			return listTreatPlanPresenterEntries;
		}

		private void checkAllUsers_Click(object sender,EventArgs e) {
			if(checkAllUsers.Checked) {
				listUser.SelectedIndices.Clear();
			}
		}

		private void listUser_Click(object sender,EventArgs e) {
			if(listUser.SelectedIndices.Count>0) {
				checkAllUsers.Checked=false;
			}
		}

		private void checkAllClinics_Click(object sender,EventArgs e) {
			if(checkAllClinics.Checked) {
				listClin.SelectedIndices.Clear();
			}
		}

		private void listClin_Click(object sender,EventArgs e) {
			if(listClin.SelectedIndices.Count>0) {
				checkAllClinics.Checked=false;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(date2.SelectionStart<date1.SelectionStart) {
				MsgBox.Show(this,"End date cannot be before start date.");
				return;
			}
			if(!checkAllUsers.Checked && listUser.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select at least one user.");
				return;
			}
			if(PrefC.HasClinicsEnabled && !checkAllClinics.Checked && listClin.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select at least one clinic.");
				return;
			}
			List<long> listUserNums=new List<long>();
			List<long> listClinicNums=new List<long>();
			if(checkAllUsers.Checked) {
				listUserNums=_listUsers.Select(x => x.UserNum).ToList();
			}
			else {
				listUserNums=listUser.SelectedIndices.OfType<int>().ToList().Select(x => _listUsers[x].UserNum).ToList();
			}
			if(PrefC.HasClinicsEnabled) {
				if(checkAllClinics.Checked) {
					listClinicNums=_listClinics.Select(x => x.ClinicNum).ToList();
				}
				else {
					for(int i = 0;i<listClin.SelectedIndices.Count;i++) {
						if(Security.CurUser.ClinicIsRestricted) {
							listClinicNums.Add(_listClinics[listClin.SelectedIndices[i]].ClinicNum);
						}
						else if(listClin.SelectedIndices[i]!=0) {
							listClinicNums.Add(_listClinics[listClin.SelectedIndices[i]-1].ClinicNum);
						}
					}
				}
				if(!Security.CurUser.ClinicIsRestricted && (listClin.GetSelected(0) || checkAllClinics.Checked)) {
					listClinicNums.Add(0);
				}
			}
			if(radioDetailed.Checked) {
				RunDetailed(listUserNums,listClinicNums);
			}
			else {
				RunTotals(listUserNums,listClinicNums);
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		///<summary>Just a handy little container class to keep treat plan presenter entries.</summary>
		private class TreatPlanPresenterEntry {
			public string Presenter;
			public DateTime DatePresented;
			public DateTime DateCompleted;
			public string ProcDescript;
			public double GrossProd;
			public double WriteOffs;
			public double Adjustments;
			public double NetProd;
			public long UserNumPresenter;
			public long PresentedClinic;
		}

	}

}