namespace ToothpicExchange
{
    partial class ImportPatientInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.importButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.labelForSearch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(44, 70);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(701, 444);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(487, 524);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(258, 57);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Add User to OpenDental";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(280, 33);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(465, 26);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // labelForSearch
            // 
            this.labelForSearch.AutoSize = true;
            this.labelForSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForSearch.Location = new System.Drawing.Point(41, 39);
            this.labelForSearch.Name = "labelForSearch";
            this.labelForSearch.Size = new System.Drawing.Size(233, 16);
            this.labelForSearch.TabIndex = 3;
            this.labelForSearch.Text = "Filter by first name, last name or email:";
            // 
            // ImportPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 593);
            this.Controls.Add(this.labelForSearch);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ImportPatientInfo";
            this.Text = "Import Patient Info";
            this.Load += new System.EventHandler(this.ImportPatientInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label labelForSearch;
    }
}