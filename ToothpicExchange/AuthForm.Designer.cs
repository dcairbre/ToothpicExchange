namespace ToothpicExchange
{
    partial class AuthForm
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
            this.welcome1 = new System.Windows.Forms.Label();
            this.welcome2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.labelForUsername = new System.Windows.Forms.Label();
            this.labelForPassword = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // welcome1
            // 
            this.welcome1.AutoSize = true;
            this.welcome1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome1.Location = new System.Drawing.Point(110, 49);
            this.welcome1.Name = "welcome1";
            this.welcome1.Size = new System.Drawing.Size(229, 20);
            this.welcome1.TabIndex = 0;
            this.welcome1.Text = "Welcome to ToothpicExchange";
            this.welcome1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // welcome2
            // 
            this.welcome2.AutoSize = true;
            this.welcome2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome2.Location = new System.Drawing.Point(166, 79);
            this.welcome2.Name = "welcome2";
            this.welcome2.Size = new System.Drawing.Size(125, 16);
            this.welcome2.TabIndex = 0;
            this.welcome2.Text = "Please login below.";
            this.welcome2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Location = new System.Drawing.Point(139, 136);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(253, 22);
            this.username.TabIndex = 1;
            // 
            // labelForUsername
            // 
            this.labelForUsername.AutoSize = true;
            this.labelForUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForUsername.Location = new System.Drawing.Point(49, 141);
            this.labelForUsername.Name = "labelForUsername";
            this.labelForUsername.Size = new System.Drawing.Size(74, 16);
            this.labelForUsername.TabIndex = 6;
            this.labelForUsername.Text = "Username:";
            // 
            // labelForPassword
            // 
            this.labelForPassword.AutoSize = true;
            this.labelForPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForPassword.Location = new System.Drawing.Point(52, 171);
            this.labelForPassword.Name = "labelForPassword";
            this.labelForPassword.Size = new System.Drawing.Size(71, 16);
            this.labelForPassword.TabIndex = 5;
            this.labelForPassword.Text = "Password:";
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(139, 168);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(253, 22);
            this.password.TabIndex = 2;
            // 
            // Login
            // 
            this.Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.Location = new System.Drawing.Point(170, 226);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(107, 30);
            this.Login.TabIndex = 3;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel.Location = new System.Drawing.Point(283, 226);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(107, 30);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AuthForm
            // 
            this.AcceptButton = this.Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 292);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.labelForPassword);
            this.Controls.Add(this.password);
            this.Controls.Add(this.labelForUsername);
            this.Controls.Add(this.username);
            this.Controls.Add(this.welcome2);
            this.Controls.Add(this.welcome1);
            this.Name = "AuthForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcome1;
        private System.Windows.Forms.Label welcome2;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label labelForUsername;
        private System.Windows.Forms.Label labelForPassword;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Cancel;
    }
}