﻿/* Receivables Breakdown Report:
 * Author: Bill MacWilliams
 * Company: Kapricorn Systems Inc.
 * 
 * This report will give you the currect calculated receivables upto the date specified.
 * This report does NOT show anything past the date specified.
 * This will allow you to do a breakdown for any month / year with totals for the month
 * at the bottom of the report and a running receivables at the far right. It also gives
 * your a breakdown by day of your receivables.  Currently this report is for the entire
 * practice.
*/
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Collections.Generic;

namespace OpenDental {

	///<summary></summary>
	public class FormRpReceivablesBreakdown:ODForm {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private MonthCalendar date1;
		private Label label1;
		private System.Windows.Forms.ListBox listProv;
		private Label labelProvider;
		private ListBox listClinic;
		private Label labClinic;
		private GroupBox groupInsBox;
		private RadioButton radioWriteoffPay;
		private RadioButton radioWriteoffProc;
		private Label label2;
		private FormQuery FormQuery2;
		private List<Provider> _listProvs;

		///<summary></summary>
		public FormRpReceivablesBreakdown() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpReceivablesBreakdown));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.labelProvider = new System.Windows.Forms.Label();
			this.listClinic = new System.Windows.Forms.ListBox();
			this.labClinic = new System.Windows.Forms.Label();
			this.groupInsBox = new System.Windows.Forms.GroupBox();
			this.radioWriteoffProc = new System.Windows.Forms.RadioButton();
			this.radioWriteoffPay = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.groupInsBox.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(468, 286);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(378, 286);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(317, 56);
			this.date1.Name = "date1";
			this.date1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(314, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(230, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Up to the following date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(27, 58);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(126, 147);
			this.listProv.TabIndex = 30;
			// 
			// labelProvider
			// 
			this.labelProvider.Location = new System.Drawing.Point(24, 39);
			this.labelProvider.Name = "labelProvider";
			this.labelProvider.Size = new System.Drawing.Size(103, 16);
			this.labelProvider.TabIndex = 7;
			this.labelProvider.Text = "Providers";
			this.labelProvider.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listClinic
			// 
			this.listClinic.FormattingEnabled = true;
			this.listClinic.Location = new System.Drawing.Point(171, 58);
			this.listClinic.Name = "listClinic";
			this.listClinic.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listClinic.Size = new System.Drawing.Size(126, 147);
			this.listClinic.TabIndex = 31;
			// 
			// labClinic
			// 
			this.labClinic.Location = new System.Drawing.Point(171, 39);
			this.labClinic.Name = "labClinic";
			this.labClinic.Size = new System.Drawing.Size(103, 16);
			this.labClinic.TabIndex = 32;
			this.labClinic.Text = "Clinic";
			this.labClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupInsBox
			// 
			this.groupInsBox.Controls.Add(this.radioWriteoffProc);
			this.groupInsBox.Controls.Add(this.radioWriteoffPay);
			this.groupInsBox.Location = new System.Drawing.Point(27, 217);
			this.groupInsBox.Name = "groupInsBox";
			this.groupInsBox.Size = new System.Drawing.Size(270, 66);
			this.groupInsBox.TabIndex = 33;
			this.groupInsBox.TabStop = false;
			this.groupInsBox.Text = "Show Insurance Writeoffs";
			// 
			// radioWriteoffProc
			// 
			this.radioWriteoffProc.Location = new System.Drawing.Point(7, 38);
			this.radioWriteoffProc.Name = "radioWriteoffProc";
			this.radioWriteoffProc.Size = new System.Drawing.Size(199, 17);
			this.radioWriteoffProc.TabIndex = 1;
			this.radioWriteoffProc.TabStop = true;
			this.radioWriteoffProc.Text = "Using procedure date.";
			this.radioWriteoffProc.UseVisualStyleBackColor = true;
			// 
			// radioWriteoffPay
			// 
			this.radioWriteoffPay.Location = new System.Drawing.Point(7, 20);
			this.radioWriteoffPay.Name = "radioWriteoffPay";
			this.radioWriteoffPay.Size = new System.Drawing.Size(240, 17);
			this.radioWriteoffPay.TabIndex = 0;
			this.radioWriteoffPay.TabStop = true;
			this.radioWriteoffPay.Text = "Using insurance payment date.";
			this.radioWriteoffPay.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(519, 20);
			this.label2.TabIndex = 34;
			this.label2.Text = "This report does not take payment plans into account if you are not using Line-It" +
    "em Payment Plans.";
			// 
			// FormRpReceivablesBreakdown
			// 
			this.AcceptButton = this.butOK;
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(567, 324);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupInsBox);
			this.Controls.Add(this.labClinic);
			this.Controls.Add(this.listClinic);
			this.Controls.Add(this.labelProvider);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpReceivablesBreakdown";
			this.ShowInTaskbar = false;
			this.Text = "Receivables Breakdown";
			this.Load += new System.EventHandler(this.FormRpReceivablesBreakdown_Load);
			this.groupInsBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormRpReceivablesBreakdown_Load(object sender,EventArgs e) {
			_listProvs=ProviderC.GetListReports();
			radioWriteoffPay.Checked = true;
			listProv.Items.Add(Lan.g(this,"Practice"));
			for(int i = 0;i < _listProvs.Count;i++) {
				listProv.Items.Add(_listProvs[i].GetLongDesc());
			}
			listProv.SetSelected(0,true);
			//if(PrefC.GetBool(PrefName.EasyNoClinics")){
			listClinic.Visible = false;
			labClinic.Visible = false;
			/*}
			else{
					listClinic.Items.Add(Lan.g(this,"Unassigned"));
					listClinic.SetSelected(0,true);
					for(int i=0;i<Clinics.List.Length;i++) {
							listClinic.Items.Add(Clinics.List[i].Description);
							listClinic.SetSelected(i+1,true);
					}
			}*/
		}


		private void butOK_Click(object sender,System.EventArgs e) {
			int payPlanVersion=PrefC.GetInt(PrefName.PayPlansVersion);
			string bDate;
			string eDate;
			decimal rcvStart = 0;
			decimal rcvProd = 0;
			decimal rcvPayPlanCharges = 0;
			decimal rcvAdj = 0;
			decimal rcvWriteoff = 0;
			decimal rcvPayment = 0;
			decimal rcvInsPayment = 0;
			decimal runningRcv = 0;
			decimal rcvDaily = 0;
			decimal[] colTotals=new decimal[9];
			string wMonth;
			string wYear;
			string wDay = "01";
			string wDate;
			// Get the year / month and instert the 1st of the month for stop point for calculated running balance
			wYear = date1.SelectionStart.Year.ToString();
			wMonth = date1.SelectionStart.Month.ToString();
			if(wMonth.Length<2) {
				wMonth = "0" + wMonth;
			}
			wDate = wYear +"-"+ wMonth +"-"+ wDay;
			ReportSimpleGrid report = new ReportSimpleGrid();
			
			//
			// Create temperary tables for sorting data
			//
			DataTable TableCharge = new DataTable();  //charges
			DataTable TablePayPlanCharge = new DataTable();  //PayPlanCharges
			DataTable TableCapWriteoff = new DataTable();  //capComplete writeoffs
			DataTable TableInsWriteoff = new DataTable();  //ins writeoffs
			DataTable TablePay = new DataTable();  //payments - Patient
			DataTable TableIns = new DataTable();  //payments - Ins, added SPK 
			DataTable TableAdj = new DataTable();  //adjustments
			//
			// Main Loop:  This will loop twice 1st loop gets running balance to start of month selected
			//             2nd will break the numbers down by day and calculate the running balances
			//
			for(int j = 0;j <= 1;j++) {
				if(j == 0) {
					bDate = "0001-01-01";
					eDate = wDate;
				}
				else {
					bDate = wDate;
					eDate = POut.Date(date1.SelectionStart.AddDays(1)).Substring(1,10);// Needed because all Queries are < end date to get correct Starting AR
				}
				string whereProv;//used as the provider portion of the where clauses.
				//each whereProv needs to be set up separately for each query
				string whereProvx;  //Extended for more than 4 names
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "ProvNum = "
              + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				report.Query = "SELECT TranDate, SUM(Amt) Amt "
								+ "FROM ( "
									+ "SELECT procedurelog.ProcDate TranDate, "
									+ "SUM(procedurelog.ProcFee*(procedurelog.UnitQty+procedurelog.BaseUnits)) Amt "
									+ "FROM procedurelog "
									+ "WHERE procedurelog.ProcDate >= '" + bDate + "' "
									+ "AND procedurelog.ProcDate < '" + eDate + "' "
									+ "AND procedurelog.ProcStatus = "+POut.Int((int)ProcStat.C)+" "
									+ whereProv
									+ "GROUP BY procedurelog.ProcDate ";
				if(payPlanVersion==2) {
					report.Query += "UNION ALL "
										+ "SELECT payplancharge.ChargeDate TranDate, "
										+ "-SUM(payplancharge.Principal) Amt "
										+ "FROM payplancharge "
										+ "WHERE payplancharge.ChargeDate >= '" + bDate + "' "
										+ "AND payplancharge.ChargeDate < '" + eDate + "' "
										+ "AND payplancharge.ChargeType = "+POut.Int((int)PayPlanChargeType.Credit)+" "
										+ whereProv
										+ "GROUP BY payplancharge.ChargeDate ";
				}
				report.Query += ")tran "
								+ "GROUP BY TranDate "
								+	"ORDER BY TranDate";
				TableCharge = report.GetTempTable();
				if(payPlanVersion==2) {
					report.Query="SELECT payplancharge.ChargeDate, SUM(payplancharge.Principal + payplancharge.Interest) Amt "
					+ "FROM payplancharge "
					+ "WHERE payplancharge.ChargeType = " +POut.Int((int)PayPlanChargeType.Debit) +" "
					+ "AND payplancharge.ChargeDate BETWEEN '" +bDate+"' AND '"+eDate+"' "
					+ whereProv
					+ "GROUP BY payplancharge.ChargeDate "
					+ "ORDER BY payplancharge.ChargeDate ";
					TablePayPlanCharge=report.GetTempTable();
				}
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "claimproc.ProvNum = "
                            + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				if(radioWriteoffPay.Checked) {
					report.Query = "SELECT DateCP, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "DateCP >= '" + bDate + "' "
                    + "AND DateCP < '" + eDate + "' "
                    + "AND Status = '7' "//CapComplete
                    + whereProv
                    + " GROUP BY DateCP "
                    + "ORDER BY DateCP";
				}
				else {
					report.Query = "SELECT ProcDate, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "ProcDate >= '" + bDate + "' "
                    + "AND ProcDate < '" + eDate + "' "
                    + "AND Status = '7' "//CapComplete
                    + whereProv
                    + " GROUP BY ProcDate "
                    + "ORDER BY ProcDate";
				}
				TableCapWriteoff = report.GetTempTable();
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "ProvNum = "
                            + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				if(radioWriteoffPay.Checked) {
					report.Query = "SELECT DateCP, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "DateCP >= '" + bDate + "' "
                    + "AND DateCP < '" + eDate + "' "
                    + "AND Status IN (1,4,5) "//Received, supplemental, capclaim. Otherwise, it's only an estimate. 7-CapCompl handled above.
                    + whereProv
                    + " GROUP BY DateCP "
                    + "ORDER BY DateCP";
				}
				else {
					report.Query = "SELECT ProcDate, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "ProcDate >= '" + bDate + "' "
                    + "AND ProcDate < '" + eDate + "' "
                    + "AND Status IN (0,1,4,5) " //Notreceived, received, supplemental, capclaim. 7-CapCompl handled above.
                    + whereProv
                    + " GROUP BY ProcDate "
                    + "ORDER BY ProcDate";
				}
				TableInsWriteoff = report.GetTempTable();
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "paysplit.ProvNum = "
                            + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				report.Query = "SELECT paysplit.DatePay,SUM(paysplit.splitamt) FROM paysplit "
                + "WHERE paysplit.PayPlanNum=0 "
				+ "AND paysplit.DatePay >= '" + bDate + "' "
                + "AND paysplit.DatePay < '" + eDate + "' "
                + whereProv
                + " GROUP BY paysplit.DatePay ORDER BY DatePay";
				TablePay = report.GetTempTable();
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "claimproc.ProvNum = "
                            + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				report.Query = "SELECT DateCP,SUM(InsPayamt) "
                + "FROM claimproc WHERE "
                + "Status IN (1,4,5,7) "//Received, supplemental, capclaim, capcomplete.
                + "AND DateCP >= '" + bDate + "' "
                + "AND DateCP < '" + eDate + "' "
								+ "AND PayPlanNum=0 "
                + whereProv
                + " GROUP BY DateCP ORDER BY DateCP";
				TableIns = report.GetTempTable();
				whereProv = "";
				if(listProv.SelectedIndices[0] != 0) {
					for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
						if(i == 0) {
							whereProv += " AND (";
						}
						else {
							whereProv += "OR ";
						}
						whereProv += "ProvNum = "
                            + POut.Long(_listProvs[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
					}
					whereProv += ") ";
				}
				report.Query = "SELECT adjdate, SUM(adjamt) FROM adjustment WHERE "
                + "adjdate >= '" + bDate + "' "
                + "AND adjdate < '" + eDate + "' "
                + whereProv
                + " GROUP BY adjdate ORDER BY adjdate";
				TableAdj = report.GetTempTable();
				//Sum up all the transactions grouped by date.
				Dictionary<DateTime,decimal> dictPayPlanCharges=TablePayPlanCharge.Select().GroupBy(x => PIn.Date(x["ChargeDate"].ToString()))
								.ToDictionary(y => y.Key,y => y.ToList().Sum(z => PIn.Decimal(z["Amt"].ToString())));
				//1st Loop Calculate running Accounts Receivable upto the 1st of the Month Selected
				//2nd Loop Calculate the Daily Accounts Receivable upto the Date Selected
				//Finaly Generate Report showing the breakdown upto the date specified with totals for what is on the report
				if(j == 0) {
					for(int k = 0;k < TableCharge.Rows.Count;k++) {
						rcvProd += PIn.Decimal(TableCharge.Rows[k][1].ToString());
					}
					rcvPayPlanCharges+=dictPayPlanCharges.Sum(x => x.Value);
					for(int k = 0;k < TableCapWriteoff.Rows.Count;k++) {
						rcvWriteoff += PIn.Decimal(TableCapWriteoff.Rows[k][1].ToString());
					}
					for(int k = 0;k < TableInsWriteoff.Rows.Count;k++) {
						rcvWriteoff += PIn.Decimal(TableInsWriteoff.Rows[k][1].ToString());
					}
					for(int k = 0;k < TablePay.Rows.Count;k++) {
						rcvPayment += PIn.Decimal(TablePay.Rows[k][1].ToString());
					}
					for(int k = 0;k < TableIns.Rows.Count;k++) {
						rcvInsPayment += PIn.Decimal(TableIns.Rows[k][1].ToString());
					}
					for(int k = 0;k < TableAdj.Rows.Count;k++) {
						rcvAdj += PIn.Decimal(TableAdj.Rows[k][1].ToString());
					}
					TableCharge.Clear();
					TablePayPlanCharge.Clear();
					TableCapWriteoff.Clear();
					TableInsWriteoff.Clear();
					TablePay.Clear();
					TableIns.Clear();
					TableAdj.Clear();
					rcvStart = (rcvProd + rcvPayPlanCharges + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
				}
				else {
					rcvAdj = 0;
					rcvInsPayment = 0;
					rcvPayment = 0;
					rcvProd = 0;
					rcvPayPlanCharges = 0;
					rcvWriteoff = 0;
					rcvDaily = 0;
					runningRcv = rcvStart;
					report.TableQ=new DataTable();
					report.TableQ.Columns.Add("Date");
					report.TableQ.Columns.Add("Charges");
					report.TableQ.Columns.Add("PayPlanCharges");
					report.TableQ.Columns.Add("Adjustments");
					report.TableQ.Columns.Add("Writeoffs");
					report.TableQ.Columns.Add("Payment");
					report.TableQ.Columns.Add("InsPayment");
					report.TableQ.Columns.Add("Daily");
					report.TableQ.Columns.Add("Running");
					eDate = POut.Date(date1.SelectionStart).Substring(1,10);// Reset EndDate to Selected Date
					DateTime[] dates = new DateTime[(PIn.Date(eDate) - PIn.Date(bDate)).Days + 1];
					for(int i = 0;i < dates.Length;i++) {//usually 31 days in loop
						dates[i] = PIn.Date(bDate).AddDays(i);
						//create new row called 'row' based on structure of TableQ
						DataRow row = report.TableQ.NewRow();
						row[0] = dates[i].ToShortDateString();
						for(int k = 0;k < TableCharge.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TableCharge.Rows[k][0].ToString()))) {
								rcvProd += PIn.Decimal(TableCharge.Rows[k][1].ToString());
							}
						}
						decimal rcvPayPlanChargesForDay=0;
						if(dictPayPlanCharges.TryGetValue(dates[i],out rcvPayPlanChargesForDay)) {
							rcvPayPlanCharges += rcvPayPlanChargesForDay;
						}
						for(int k = 0;k < TableCapWriteoff.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TableCapWriteoff.Rows[k][0].ToString()))) {
								rcvWriteoff += PIn.Decimal(TableCapWriteoff.Rows[k][1].ToString());
							}
						}
						for(int k = 0;k < TableAdj.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TableAdj.Rows[k][0].ToString()))) {
								rcvAdj += PIn.Decimal(TableAdj.Rows[k][1].ToString());
							}
						}
						for(int k = 0;k < TableInsWriteoff.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TableInsWriteoff.Rows[k][0].ToString()))) {
								rcvWriteoff += PIn.Decimal(TableInsWriteoff.Rows[k][1].ToString());
							}
						}
						for(int k = 0;k < TablePay.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TablePay.Rows[k][0].ToString()))) {
								rcvPayment += PIn.Decimal(TablePay.Rows[k][1].ToString());
							}
						}
						for(int k = 0;k < TableIns.Rows.Count;k++) {
							if(dates[i] == (PIn.Date(TableIns.Rows[k][0].ToString()))) {
								rcvInsPayment += PIn.Decimal(TableIns.Rows[k][1].ToString());
							}
						}
						//rcvPayPlanCharges will be 0 if not on version 2.
						rcvDaily = (rcvProd + rcvPayPlanCharges + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
						runningRcv += (rcvProd + rcvPayPlanCharges + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
						row["Charges"] = rcvProd.ToString("n");
						row["PayPlanCharges"] = rcvPayPlanCharges.ToString("n");
						row["Adjustments"] = rcvAdj.ToString("n");
						row["Writeoffs"] = rcvWriteoff.ToString("n");
						row["Payment"] = rcvPayment.ToString("n");
						row["InsPayment"] = rcvInsPayment.ToString("n");
						row["Daily"] = rcvDaily.ToString("n");
						row["Running"] = runningRcv.ToString("n");
						colTotals[1] += rcvProd;
						colTotals[2] += rcvPayPlanCharges;
						colTotals[3] += rcvAdj;
						colTotals[4] += rcvWriteoff;
						colTotals[5] += rcvPayment;
						colTotals[6] += rcvInsPayment;
						colTotals[7] += rcvDaily;
						colTotals[8] = runningRcv;
						report.TableQ.Rows.Add(row);  //adds row to table Q
						rcvAdj = 0;
						rcvInsPayment = 0;
						rcvPayment = 0;
						rcvProd = 0;
						rcvPayPlanCharges = 0;
						rcvWriteoff = 0;
					}
					//Drop the PayPlanCharges column if not version 2
					if(payPlanVersion!=2) {
						report.TableQ.Columns.RemoveAt(2);
					}
					report.InitializeColumns();
					//columnCount will get incremented on the second ColTotal call so we can use it in this way
					if(payPlanVersion==2) {
						report.ColTotal[1]=PIn.Decimal(colTotals[1].ToString("n")); //prod
						report.ColTotal[2]=PIn.Decimal(colTotals[2].ToString("n")); //payplancharges
						report.ColTotal[3]=PIn.Decimal(colTotals[3].ToString("n")); //adjustment
						report.ColTotal[4]=PIn.Decimal(colTotals[4].ToString("n")); //writeoffs
						report.ColTotal[5]=PIn.Decimal(colTotals[5].ToString("n")); //payment
						report.ColTotal[6]=PIn.Decimal(colTotals[6].ToString("n")); //inspayment
						report.ColTotal[7]=PIn.Decimal(colTotals[7].ToString("n")); //daily
						report.ColTotal[8]=PIn.Decimal(colTotals[8].ToString("n")); //running
					}
					else {
						report.ColTotal[1]=PIn.Decimal(colTotals[1].ToString("n")); //prod
						report.ColTotal[2]=PIn.Decimal(colTotals[3].ToString("n")); //adjustment
						report.ColTotal[3]=PIn.Decimal(colTotals[4].ToString("n")); //writeoffs
						report.ColTotal[4]=PIn.Decimal(colTotals[5].ToString("n")); //payment
						report.ColTotal[5]=PIn.Decimal(colTotals[6].ToString("n")); //inspayment
						report.ColTotal[6]=PIn.Decimal(colTotals[7].ToString("n")); //daily
						report.ColTotal[7]=PIn.Decimal(colTotals[8].ToString("n")); //running
					}
					FormQuery2 = new FormQuery(report);
					FormQuery2.IsReport = true;
					FormQuery2.ResetGrid();
					report.Title = "Receivables Breakdown Report";
					report.SubTitle.Add(PrefC.GetString(PrefName.PracticeTitle));
					whereProv = "Report for: Practice";
					whereProvx = "";
					if(listProv.SelectedIndices[0] != 0) {
						int nameCount = 0;
						whereProv = "Report Includes:  ";
						for(int i = 0;i < listProv.SelectedIndices.Count;i++) {
							if(nameCount < 3) {
								whereProv+=" "+_listProvs[listProv.SelectedIndices[i]-1].GetAbbr()+"-"+_listProvs[listProv.SelectedIndices[i]-1].GetFormalName()+" /";
							}
							else {
								whereProvx+=" "+_listProvs[listProv.SelectedIndices[i]-1].GetAbbr()+"-"+_listProvs[listProv.SelectedIndices[i]-1].GetFormalName()+" /";
							}
							nameCount += 1;
						}
						whereProv = whereProv.Substring(0,whereProv.Length-1);
						if(whereProvx.Length > 0) {
							whereProvx = whereProvx.Substring(0,whereProvx.Length-1);
						}
					}
					report.SubTitle.Add(whereProv);
					report.SubTitle.Add(whereProvx);
					if(payPlanVersion==2) {
						report.SetColumn(this,0,"Day",70);
						report.SetColumn(this,1,"Production",80,HorizontalAlignment.Right);
						report.SetColumn(this,2,"PayPlan",80,HorizontalAlignment.Right);
						report.SetColumn(this,3,"Adjustment",80,HorizontalAlignment.Right);
						report.SetColumn(this,4,"Writeoff",80,HorizontalAlignment.Right);
						report.SetColumn(this,5,"Payment",80,HorizontalAlignment.Right);
						report.SetColumn(this,6,"InsPayment",80,HorizontalAlignment.Right);
						report.SetColumn(this,7,"Daily A/R",100,HorizontalAlignment.Right);
						report.SetColumn(this,8,"Ending A/R",100,HorizontalAlignment.Right);
						report.Summary.Add(
							Lan.g(this,"Receivables Calculation: (Production + PayPlanCharges + Adjustments - Writeoffs) - (Payments + Insurance Payments)"));
					}
					else {
						report.SetColumn(this,0,"Day",80);
						report.SetColumn(this,1,"Production",80,HorizontalAlignment.Right);
						report.SetColumn(this,2,"Adjustment",100,HorizontalAlignment.Right);
						report.SetColumn(this,3,"Writeoff",100,HorizontalAlignment.Right);
						report.SetColumn(this,4,"Payment",100,HorizontalAlignment.Right);
						report.SetColumn(this,5,"InsPayment",100,HorizontalAlignment.Right);
						report.SetColumn(this,6,"Daily A/R",110,HorizontalAlignment.Right);
						report.SetColumn(this,7,"Ending A/R",110,HorizontalAlignment.Right);
						report.Summary.Add(
							Lan.g(this,"Receivables Calculation: (Production + Adjustments - Writeoffs) - (Payments + Insurance Payments)"));
					}
					FormQuery2.ShowDialog();
					DialogResult = DialogResult.OK;
				}//END If 
			}// END For Loop
		}//END OK button Clicked
	}
}