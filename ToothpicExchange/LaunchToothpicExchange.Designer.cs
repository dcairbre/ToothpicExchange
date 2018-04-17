namespace ToothpicExchange
{
    partial class LaunchToothpicExchange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchToothpicExchange));
            this.Heading = new System.Windows.Forms.Label();
            this.ExportButton = new System.Windows.Forms.Button();
            this.LabelForExportButton = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ImportButton = new System.Windows.Forms.Button();
            this.logout = new System.Windows.Forms.Button();
            this.loggedIn = new System.Windows.Forms.Label();
            this.infoText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Heading.Location = new System.Drawing.Point(38, 110);
            this.Heading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(382, 29);
            this.Heading.TabIndex = 0;
            this.Heading.Text = "Welcome to ToothpicExchange.";
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(43, 347);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(218, 69);
            this.ExportButton.TabIndex = 1;
            this.ExportButton.Text = "Export Patient Info";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // LabelForExportButton
            // 
            this.LabelForExportButton.AutoSize = true;
            this.LabelForExportButton.Location = new System.Drawing.Point(305, 371);
            this.LabelForExportButton.Name = "LabelForExportButton";
            this.LabelForExportButton.Size = new System.Drawing.Size(48, 20);
            this.LabelForExportButton.TabIndex = 2;
            this.LabelForExportButton.Text = "Label";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Setup a new patient in OpenDental";
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(43, 240);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(218, 69);
            this.ImportButton.TabIndex = 3;
            this.ImportButton.Text = "Import Patient Info";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // logout
            // 
            this.logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Location = new System.Drawing.Point(583, 27);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(74, 33);
            this.logout.TabIndex = 5;
            this.logout.Text = "Logout";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // loggedIn
            // 
            this.loggedIn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.loggedIn.Location = new System.Drawing.Point(43, 33);
            this.loggedIn.Name = "loggedIn";
            this.loggedIn.Size = new System.Drawing.Size(531, 20);
            this.loggedIn.TabIndex = 6;
            this.loggedIn.Text = "Currently logged in: Dr James Smith";
            this.loggedIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // infoText
            // 
            this.infoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoText.Location = new System.Drawing.Point(40, 148);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(458, 65);
            this.infoText.TabIndex = 7;
            this.infoText.Text = resources.GetString("infoText.Text");
            // 
            // LaunchToothpicExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 458);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.loggedIn);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.LabelForExportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.Heading);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LaunchToothpicExchange";
            this.Text = "ToothpicExchange";
            this.Load += new System.EventHandler(this.LaunchToothpicExchange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Label LabelForExportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Label loggedIn;
        private System.Windows.Forms.Label infoText;
    }
}

