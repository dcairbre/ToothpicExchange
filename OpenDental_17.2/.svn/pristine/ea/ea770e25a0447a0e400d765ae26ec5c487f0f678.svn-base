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
	public partial class FormRpTreatPlanPresentationStatistics:ODForm {
		private List<Userod> _listUsers;
		private List<Clinic> _listClinics;
		public FormRpTreatPlanPresentationStatistics() {
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
				groupGrossNet.Location=new Point(185,225);
				groupOrder.Location=new Point(185,295);
				groupUser.Location=new Point(185,365);
				listUser.Width+=30;
			}
		}

		private void RunReport(List<long> listUserNums,List<long> listClinicsNums) {
			ReportComplex report = new ReportComplex(true,false);
			report.AddTitle("Title",Lan.g(this,"Presented Procedure Totals"));
			report.AddSubTitle("PracTitle",PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle("Date",date1.SelectionStart.ToShortDateString()+" - "+date2.SelectionStart.ToShortDateString());
			List<Userod> listSelectedUsers = new List<Userod>();
			if(checkAllUsers.Checked) {
				report.AddSubTitle("Users",Lan.g(this,"All Users"));
				listSelectedUsers.AddRange(_listUsers); //add all users
			}
			else {
				for(int i = 0;i < listUser.SelectedIndices.Count;i++) {
					listSelectedUsers.Add(_listUsers[listUser.SelectedIndices[i]]); //add selected users
				}
				report.AddSubTitle("Users",string.Join(",",listSelectedUsers.Select(x => x.UserName)));
			}
			List<Clinic> listSelectedClinics = new List<Clinic>();
			if(PrefC.HasClinicsEnabled) {
				if(checkAllClinics.Checked) {
					report.AddSubTitle("Clinics",Lan.g(this,"All Clinics"));
					listSelectedClinics.Add(new Clinic() {
						ClinicNum = 0,
						Description = "Unassigned"
					});
					listSelectedClinics.AddRange(_listClinics); //add all clinics and the unassigned clinic.
				}
				else {
					for(int i = 0;i<listClin.SelectedIndices.Count;i++) {
						if(Security.CurUser.ClinicIsRestricted) {
							listSelectedClinics.Add(_listClinics[listClin.SelectedIndices[i]]);
						}
						else {
							if(listClin.SelectedIndices[i]==0) {
								listSelectedClinics.Add(new Clinic() {
									ClinicNum = 0,
									Description = "Unassigned"
								});
							}
							else {
								listSelectedClinics.Add(_listClinics[listClin.SelectedIndices[i]-1]);//Minus 1 from the selected index
							}
						}
					}
					report.AddSubTitle("Clinics",string.Join(",",listSelectedClinics.Select(x => x.Description)));
				}
			}
			DataTable table=GetTreatPlanPresentationStatistics(listSelectedClinics.Select(y => y.ClinicNum).ToList(),listSelectedUsers.Select(y => y.UserNum).ToList());			
			QueryObject query=report.AddQuery(table,"","",SplitByKind.None,1,true);
			query.AddColumn(Lan.g(this,"Presenter"),100,FieldValueType.String);
			query.AddColumn(Lan.g(this,"# of Plans"),85,FieldValueType.Integer);
			query.AddColumn(Lan.g(this,"# of Procs"),85,FieldValueType.Integer);
			query.AddColumn(Lan.g(this,"# of ProcsSched"),100,FieldValueType.Integer);
			query.AddColumn(Lan.g(this,"# of ProcsComp"),100,FieldValueType.Integer);
			if(radioGross.Checked) {
				query.AddColumn(Lan.g(this,"GrossTPAmt"),95,FieldValueType.Number);
				query.AddColumn(Lan.g(this,"GrossSchedAmt"),95,FieldValueType.Number);
				query.AddColumn(Lan.g(this,"GrossCompAmt"),95,FieldValueType.Number);
			}
			else {
				query.AddColumn(Lan.g(this,"NetTPAmt"),95,FieldValueType.Number);
				query.AddColumn(Lan.g(this,"NetSchedAmt"),95,FieldValueType.Number);
				query.AddColumn(Lan.g(this,"NetCompAmt"),95,FieldValueType.Number);
			}
			if(!report.SubmitQueries()) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			FormReportComplex FormR=new FormReportComplex(report);
			FormR.ShowDialog();
			//DialogResult=DialogResult.OK;
		}

		///<summary>Gets the main data table used in this report.</summary
		private DataTable GetTreatPlanPresentationStatistics(List<long> listClinicNums,List<long> listUserNums) {
			List<ProcTP> listProcTPsAll = ProcTPs.GetAllLim();
			List<TreatPlan> listSavedTreatPlans = TreatPlans.GetAllSavedLim();
			List<ProcTpTreatPlan> listProcTPTreatPlans = new List<ProcTpTreatPlan>();
			listProcTPsAll.ForEach(x => {
				listProcTPTreatPlans.Add(new ProcTpTreatPlan() {
					TreatPlanCur = listSavedTreatPlans.First(y => y.TreatPlanNum == x.TreatPlanNum),
					ProcTPCur = x
				});
			});
			//get one entry per procedure with their first/last date of presentation based on radio buttons.
			if(radioFirstPresented.Checked) {
				listProcTPTreatPlans = listProcTPTreatPlans
					.OrderBy(x => x.ProcTPCur.ProcNumOrig)
					.ThenBy(x => x.TreatPlanCur.DateTP)
					.ThenBy(x => x.TreatPlanCur.TreatPlanNum)
					.GroupBy(x => x.ProcTPCur.ProcNumOrig)
					.Select(x => x.First())
					.ToList();
			}
			else {
				listProcTPTreatPlans = listProcTPTreatPlans
					.OrderBy(x => x.ProcTPCur.ProcNumOrig)
					.ThenByDescending(x => x.TreatPlanCur.DateTP)
					.ThenBy(x => x.TreatPlanCur.TreatPlanNum)
					.GroupBy(x => x.ProcTPCur.ProcNumOrig)
					.Select(x => x.First())
					.ToList();
			}
			//get rid of any entries that are outside the range selected.
			listProcTPTreatPlans=listProcTPTreatPlans.Where(x => x.TreatPlanCur.DateTP.Date >= date1.SelectionStart
				&& x.TreatPlanCur.DateTP.Date <= date2.SelectionStart).ToList();
			//Get the associated procedures, claimprocs, adjustments, users, appointments.
			List<Procedure> listProcsForTreatPlans = Procedures.GetForProcTPs(listProcTPTreatPlans.Select(x => x.ProcTPCur).ToList(),ProcStat.C,ProcStat.TP);
			if(PrefC.HasClinicsEnabled && !checkAllClinics.Checked) {
				listProcsForTreatPlans=
					listProcsForTreatPlans.FindAll(x => listClinicNums.Contains(x.ClinicNum));
			}
			List<ClaimProc> listClaimProcs = ClaimProcs.GetForProcsLimited(listProcsForTreatPlans.Select(x => x.ProcNum).ToList(),
				ClaimProcStatus.CapComplete,ClaimProcStatus.NotReceived,ClaimProcStatus.Received,ClaimProcStatus.Supplemental,ClaimProcStatus.Estimate);
			List<Adjustment> listAdjustments = Adjustments.GetForProcs(listProcsForTreatPlans.Select(x => x.ProcNum).ToList());
			List<Userod> listUserods = Userods.GetAll();
			List<TreatPlanPresenterEntry> listTreatPlanPresenterEntries = new List<TreatPlanPresenterEntry>();
			List<ProcedureCode> listProcCodes = ProcedureCodes.GetCodesForCodeNums(listProcsForTreatPlans.Select(x => x.CodeNum).ToList());
			List<Appointment> listApts = Appointments.GetMultApts(listProcsForTreatPlans.Select(x => x.AptNum).ToList());
			double amt = listProcsForTreatPlans.Sum(x => x.ProcFee);
			foreach(Procedure procCur in listProcsForTreatPlans) {
				double grossProd = procCur.ProcFee * (procCur.UnitQty + procCur.BaseUnits);
				double writeOffs = listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum)
						.Where(x => x.Status == ClaimProcStatus.CapComplete)
						.Sum(x => x.WriteOff);
				grossProd-=writeOffs;
				if(procCur.ProcStatus == ProcStat.C) {
					writeOffs += listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum)
						.Where(x => x.Status.In(ClaimProcStatus.NotReceived,ClaimProcStatus.Received,ClaimProcStatus.Supplemental))
						.Sum(x => x.WriteOff);
				}
				else {
					foreach(ClaimProc claimProcCur in listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum).Where(x => x.Status == ClaimProcStatus.Estimate)) {
						if(claimProcCur.WriteOffEstOverride == -1) {
							if(claimProcCur.WriteOffEst!=-1) {
								writeOffs+=claimProcCur.WriteOffEst;
							}
						}
						else {
							writeOffs+=claimProcCur.WriteOffEstOverride;
						}
					}
					//writeOffs += listClaimProcs.Where(x => x.ProcNum == procCur.ProcNum)
					//	.Where(x => x.Status == ClaimProcStatus.Estimate)
					//	.Sum(x => x.WriteOffEstOverride == -1 ? (x.WriteOffEst == -1 ? 0 : x.WriteOffEst) : x.WriteOffEstOverride); //Allen won't let me commit this nested ternary :(
				}
				double adjustments = listAdjustments.Where(x => x.ProcNum == procCur.ProcNum).Sum(x => x.AdjAmt);
				double netProd = grossProd - writeOffs + adjustments;
				TreatPlan treatPlanCur = listProcTPTreatPlans.Where(x => x.ProcTPCur.ProcNumOrig == procCur.ProcNum).First().TreatPlanCur;
				Userod userPresenter;
				if(radioPresenter.Checked) {
					userPresenter = listUserods.FirstOrDefault(x => x.UserNum == treatPlanCur.UserNumPresenter);
				}
				else { //radioEntryUser
					userPresenter = listUserods.FirstOrDefault(x => x.UserNum == treatPlanCur.SecUserNumEntry);
				}
				ProcedureCode procCode = listProcCodes.First(x => x.CodeNum == procCur.CodeNum);
				Appointment aptCur = listApts.FirstOrDefault(x => x.AptNum == procCur.AptNum);
				listTreatPlanPresenterEntries.Add(new TreatPlanPresenterEntry() {
					Presenter = userPresenter==null ? "" : userPresenter.UserName,
					DatePresented = treatPlanCur.DateTP,
					DateCompleted = procCur.ProcDate,
					ProcDescript = procCode.Descript,
					GrossProd = grossProd,
					Adjustments = adjustments,
					WriteOffs = writeOffs,
					NetProd = netProd,
					UserNumPresenter = userPresenter==null ? 0 : userPresenter.UserNum,
					PresentedClinic = procCur.ClinicNum,
					ProcStatus = procCur.ProcStatus,
					TreatPlanNum = treatPlanCur.TreatPlanNum,
					AptNum = procCur.AptNum,
					AptStatus = aptCur==null ? ApptStatus.None : aptCur.AptStatus
				});
			}
			DataTable table = new DataTable();
			table.Columns.Add("Presenter");
			table.Columns.Add("# of Plans");
			table.Columns.Add("# of Procs");
			table.Columns.Add("# of ProcsSched");
			table.Columns.Add("# of ProcsComp");
			if(radioGross.Checked) {
				table.Columns.Add("GrossTPAmt");
				table.Columns.Add("GrossSchedAmt");
				table.Columns.Add("GrossCompAmt");
			}
			else {
				table.Columns.Add("NetTpAmt");
				table.Columns.Add("NetSchedAmt");
				table.Columns.Add("NetCompAmt");
			}
			if(!checkAllUsers.Checked) {
				listTreatPlanPresenterEntries=listTreatPlanPresenterEntries.Where(x => listUserNums.Contains(x.UserNumPresenter)).ToList();
			}
			listTreatPlanPresenterEntries=listTreatPlanPresenterEntries.OrderBy(x => x.Presenter).ToList();
			listTreatPlanPresenterEntries
				.GroupBy(x => x.Presenter).ToList().ForEach(x => {
					DataRow row = table.NewRow();
					row["Presenter"] = x.First().Presenter=="" ? "None" : x.First().Presenter;
					row["# of Plans"] = x.GroupBy(y => y.TreatPlanNum).Count();
					row["# of Procs"] = x.Count();
					row["# of ProcsSched"] = x.Count(y => y.ProcStatus == ProcStat.TP && y.AptNum != 0 && y.AptStatus.In(ApptStatus.Scheduled,ApptStatus.ASAP));
					row["# of ProcsComp"] = x.Count(y => y.ProcStatus == ProcStat.C);
					if(radioGross.Checked) {
						row["GrossTpAmt"] = x.Sum(y => y.GrossProd);
						row["GrossSchedAmt"] = x.Where(y => y.ProcStatus == ProcStat.TP && y.AptNum != 0 && y.AptStatus.In(ApptStatus.Scheduled,ApptStatus.ASAP)).Sum(y => y.GrossProd);
						row["GrossCompAmt"] = x.Where(y => y.ProcStatus == ProcStat.C).Sum(y => y.GrossProd);
					}
					else {
						row["NetTpAmt"] = x.Sum(y => y.NetProd);
						row["NetSchedAmt"] = x.Where(y => y.ProcStatus == ProcStat.TP && y.AptNum != 0 && y.AptStatus.In(ApptStatus.Scheduled,ApptStatus.ASAP)).Sum(y => y.NetProd);
						row["NetCompAmt"] = x.Where(y => y.ProcStatus == ProcStat.C).Sum(y => y.NetProd);
					}
					table.Rows.Add(row);
				});
			return table;
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
			RunReport(listUserNums,listClinicNums);
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		///<summary>Combines proctps and treatplans into one handy object.</summary>
		private class ProcTpTreatPlan {
			public ProcTP ProcTPCur;
			public TreatPlan TreatPlanCur;
			//public Procedure ProcCur;
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
			public ProcStat ProcStatus;
			public ApptStatus AptStatus;
			public long UserNumPresenter;
			public long PresentedClinic;
			public long TreatPlanNum;
			public long AptNum;
		}

	}
}