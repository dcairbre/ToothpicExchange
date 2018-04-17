using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Text.RegularExpressions;
using OpenDental.Forms;
using System.Linq;

namespace OpenDental {
	public partial class FormWikiListHeaders : ODForm {
		public string WikiListCurName;
		///<summary>Widths must always be specified.  Not optional.</summary>
		private List<WikiListHeaderWidth> _listTableHeaders;

		public FormWikiListHeaders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListHeaders_Load(object sender,EventArgs e) {
			_listTableHeaders=WikiListHeaderWidths.GetForList(WikiListCurName);
			FillGrid();
		}

		///<summary>Each row of data becomes a column in the grid. This pattern is only used in a few locations.</summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			//empty list to initialize a column of comboboxes, these will be set whilst adding the row
			List<string> listInitializer=new List<string>();
			ODGridColumn col=new ODGridColumn(Lan.g("TableWikiListHeaders","Column Name"),gridMain.Width-100,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableWikiListHeaders","Width"),100,true);
			gridMain.Columns.Add(col);
			col=new ODGridColumn();
			gridMain.Rows.Clear();
			for(int i = 0;i<_listTableHeaders.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(_listTableHeaders[i].ColName);
				row.Cells.Add(_listTableHeaders[i].ColWidth.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Set primary key to correct name-----------------------------------------------------------------------
			gridMain.Rows[0].Cells[0].Text=WikiListCurName+"Num";//prevents exceptions from occuring when user tries to rename PK.
			//Validate column names---------------------------------------------------------------------------------
			for(int i = 0;i<gridMain.Rows.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(Regex.IsMatch(gridMain.Rows[i].Cells[0].Text,@"^\d")) {
					MsgBox.Show(this,"Column cannot start with numbers.");
					return;
				}
				if(Regex.IsMatch(gridMain.Rows[i].Cells[0].Text,@"\s")) {
					MsgBox.Show(this,"Column names cannot contain spaces.");
					return;
				}
				if(Regex.IsMatch(gridMain.Rows[i].Cells[0].Text,@"\W")) {//W=non-word chars
					MsgBox.Show(this,"Column names cannot contain special characters.");
					return;
				}
			}
			//Check for reserved words--------------------------------------------------------------------------------
			for(int i = 0;i<gridMain.Rows.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(DbHelper.isMySQLReservedWord(gridMain.Rows[i].Cells[0].Text)) {
					MessageBox.Show(Lan.g(this,"Column name is a reserved word in MySQL")+":"+gridMain.Rows[i].Cells[0].Text);
					return;
				}
				//primary key is caught by duplicate column name logic.
			}
			//Check for duplicates-----------------------------------------------------------------------------------
			List<string> listColNamesCheck=new List<string>();
			for(int i = 0;i<gridMain.Rows.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(listColNamesCheck.Contains(gridMain.Rows[i].Cells[0].Text)) {
					MessageBox.Show(Lan.g(this,"Duplicate column name detected")+":"+gridMain.Rows[0].Cells[i].Text);
					return;
				}
				listColNamesCheck.Add(gridMain.Rows[i].Cells[0].Text);
			}
			//Validate column widths---------------------------------------------------------------------------------
			for(int i = 0;i<gridMain.Rows.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(Regex.IsMatch(gridMain.Rows[i].Cells[1].Text,@"\D")) {// "\D" matches any non-decimal character
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
				if(gridMain.Rows[i].Cells[1].Text.Contains("-")
					|| gridMain.Rows[i].Cells[1].Text.Contains(".")
					|| gridMain.Rows[i].Cells[1].Text.Contains(",")) //inlcude the comma for international support. For instance Pi = 3.1415 or 3,1415 depending on your region
				{
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
			}
			//save values to List<WikiListHeaderWidth> TableHeaders
			for(int i = 0;i<_listTableHeaders.Count;i++) {
				_listTableHeaders[i].ColName=PIn.String(gridMain.Rows[i].Cells[0].Text);
				_listTableHeaders[i].ColWidth=PIn.Int(gridMain.Rows[i].Cells[1].Text);
			}
			//Save data to database-----------------------------------------------------------------------------------
			try {
				WikiListHeaderWidths.UpdateNamesAndWidths(WikiListCurName,_listTableHeaders);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);//will throw exception if table schema has changed since the window was opened.
				DialogResult=DialogResult.Cancel;
			}
			DataValid.SetInvalid(InvalidType.Wiki);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}