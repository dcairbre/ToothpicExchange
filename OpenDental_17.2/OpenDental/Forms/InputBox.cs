using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace OpenDental{
	/// <summary>A quick entry form for various purposes.  Pull the result from textResult.Text.</summary>
	public class InputBox : ODForm {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		public Label labelPrompt;
		///<summary></summary>
		public TextBox textResult;
		public ComboBox comboSelection;
		public bool IsMultiline;
		public bool IsMultiSelect;
		private UI.ComboBoxMulti comboSelectMulti;

		public int SelectedIndex {
			get {
				if(SelectedIndices.Count<1) {
					return -1;
				}
				return SelectedIndices[0];
			}
		}

		public List<int> SelectedIndices {
			get {
				if(IsMultiSelect) {
					return comboSelectMulti.SelectedIndices.Cast<int>().ToList();
				}
				else {
					return new List<int> { comboSelection.SelectedIndex };
				}
			}
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public InputBox(string prompt)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			labelPrompt.Text=prompt;
			Lan.F(this);
		}

		///<summary>This constructor allows a list of strings to be sent in and fill a comboBox for users to select from.  The comboBox is in place of the textResult text box.</summary>
		public InputBox(string prompt,List<string> listSelections) {
			InitializeComponent();
			labelPrompt.Text=prompt;
			comboSelection.Location=textResult.Location;
			textResult.Visible=false;
			comboSelection.Visible=true;
			for(int i=0;i<listSelections.Count;i++) {
				comboSelection.Items.Add(listSelections[i]);
				comboSelectMulti.Items.Add(listSelections[i]);
			}
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
			this.labelPrompt = new System.Windows.Forms.Label();
			this.textResult = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.comboSelection = new System.Windows.Forms.ComboBox();
			this.comboSelectMulti = new OpenDental.UI.ComboBoxMulti();
			this.SuspendLayout();
			// 
			// labelPrompt
			// 
			this.labelPrompt.Location = new System.Drawing.Point(31, 10);
			this.labelPrompt.Name = "labelPrompt";
			this.labelPrompt.Size = new System.Drawing.Size(387, 36);
			this.labelPrompt.TabIndex = 2;
			this.labelPrompt.Text = "labelPrompt";
			this.labelPrompt.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textResult
			// 
			this.textResult.Location = new System.Drawing.Point(32, 51);
			this.textResult.Name = "textResult";
			this.textResult.Size = new System.Drawing.Size(385, 20);
			this.textResult.TabIndex = 0;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(262, 136);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(343, 136);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// comboSelection
			// 
			this.comboSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSelection.DropDownWidth = 395;
			this.comboSelection.Location = new System.Drawing.Point(32, 77);
			this.comboSelection.MaxDropDownItems = 30;
			this.comboSelection.Name = "comboSelection";
			this.comboSelection.Size = new System.Drawing.Size(386, 21);
			this.comboSelection.TabIndex = 3;
			this.comboSelection.Visible = false;
			// 
			// comboSelectMulti
			// 
			this.comboSelectMulti.ArraySelectedIndices = new int[0];
			this.comboSelectMulti.BackColor = System.Drawing.SystemColors.Window;
			this.comboSelectMulti.Items = ((System.Collections.ArrayList)(resources.GetObject("comboSelectMulti.Items")));
			this.comboSelectMulti.Location = new System.Drawing.Point(32, 104);
			this.comboSelectMulti.Name = "comboSelectMulti";
			this.comboSelectMulti.SelectedIndices = ((System.Collections.ArrayList)(resources.GetObject("comboSelectMulti.SelectedIndices")));
			this.comboSelectMulti.Size = new System.Drawing.Size(386, 21);
			this.comboSelectMulti.TabIndex = 4;
			this.comboSelectMulti.Visible = false;
			// 
			// InputBox
			// 
			this.ClientSize = new System.Drawing.Size(453, 174);
			this.Controls.Add(this.comboSelectMulti);
			this.Controls.Add(this.comboSelection);
			this.Controls.Add(this.textResult);
			this.Controls.Add(this.labelPrompt);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputBox";
			this.ShowInTaskbar = false;
			this.Text = "Input";
			this.Load += new System.EventHandler(this.InputBox_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		
		private void InputBox_Load(object sender,EventArgs e) {
			if(IsMultiline) {
				comboSelection.Visible=false;
				this.Size=new Size(this.Size.Width,270);
				textResult.Multiline=true;
				textResult.ScrollBars=ScrollBars.Vertical;
				textResult.Size=new Size(textResult.Size.Width,100);
			}
			else {
				Size=new Size(this.Size.Width,170);
			}
			if(IsMultiSelect) {
				comboSelection.Visible=false;
				comboSelectMulti.Location=textResult.Location;
				comboSelectMulti.Visible=true;
			}
		}

		public void setTitle(string title) {
			this.Text=title;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}





















