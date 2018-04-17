using System;
using System.Windows.Forms;

namespace ToothpicExchange
{
    /// <summary>
    /// This class contains the form for users to authenticate (login).
    /// </summary>
    /// <remarks>
    /// The user enters the username/password which is used in an API call to authenticate an API object.
    /// If the authentication is successful, then that API object is made available for future authenticated API calls.
    /// </remarks>
    public partial class AuthForm : Form
    {
        /// <summary>
        /// The API object which has been authenticated and ready to use for API calls. It is public so it can pass the object on to the main plugin.
        /// </summary>
        public ToothpicAPI Call { get; set; }

        /// <summary>
        /// This method initialises the form.
        /// </summary>
        public AuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// An event handling method once the login button has clicked. It authenticates the user with the API.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, EventArgs e)
        {
            // Simple form validation
            if (username.Text.Length < 2)
            {
                MessageBox.Show("You must enter a full username.");
            } else if (password.Text.Length < 2)
            {
                MessageBox.Show("You must enter a full password.");
            } else
            {
                // The initial call to authenticate. This instance is then used for all further API calls.
                Call = new ToothpicAPI().AuthenticateUser(username.Text, password.Text);

                if (Call != null)
                    this.DialogResult = DialogResult.OK;
                else
                    MessageBox.Show("Unsuccessful authentication. Please try again.");
            }

        }

        /// <summary>
        /// An event handling method if the cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
