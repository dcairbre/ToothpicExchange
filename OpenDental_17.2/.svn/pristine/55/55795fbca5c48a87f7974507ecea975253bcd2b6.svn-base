using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAutoNotePromptText:ODForm {
		///<summary>Set this value externally.</summary>
		public string PromptText;
		///<summary>What the user entered.  This can be set externally to the default value.</summary>
		public string ResultText;
		///<summary>If user has the option to go back</summary>
		public bool IsGoBack;
		///<summary>The string value of previous user response</summary>
		public string CurPromptResponse;

		public FormAutoNotePromptText() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNotePromptText_Load(object sender,EventArgs e) {
			CurPromptResponse=!string.IsNullOrEmpty(CurPromptResponse) ? CurPromptResponse : "";
			Location=new Point(Left,Top+150);
			butBack.Visible=IsGoBack;
			labelPrompt.Text=PromptText;
			if(CurPromptResponse!="") {//display previous user response
				textMain.Text=CurPromptResponse;
			}
			else {
				textMain.Text=ResultText;
			}			
		}

		private void butOK_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butSkip_Click(object sender,EventArgs e) {
			ResultText="";
			DialogResult=DialogResult.OK;
		}

		private void butPreview_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			FormAutoNotePromptPreview FormP=new FormAutoNotePromptPreview();
			FormP.ResultText=ResultText;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				ResultText=FormP.ResultText;
				DialogResult=DialogResult.OK;
			}
		}

		private void butBack_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Retry;
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Abort autonote entry?")) {
				return;
			}
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}