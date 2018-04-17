using System;
using System.Windows.Forms;
using OpenDentBusiness;

namespace ToothpicExchange
{
    /// <summary>
    /// This class is the entry point for ToothpicExchange.
    /// When a user clicks the Toothpic button from the menu bar, this class is initialised from Plugin.cs
    /// Both import and export functions are launched from within this form.
    /// </summary>
    public partial class LaunchToothpicExchange : Form
    {
        /// <summary>
        /// The current patient Number taken from OpenDental's Patients.GetPat(PatNum)
        /// </summary>
        public long PatNum;

        /// <summary>
        /// The current patient.
        /// </summary>
        private Patient pat;

        /// <summary>
        /// The API call object used for API calls.
        /// </summary>
        private ToothpicAPI call = null;

        /// <summary>
        /// The user currently authenticated, used mostly for aesthetic purposes.
        /// </summary>
        private ToothpicUser authenticatedUser = null;

        /// <summary>
        /// This method initialises the form.
        /// </summary>
        public LaunchToothpicExchange()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handling method to be executed when form loads. It checks if the user is authenticated, if not launches an AuthForm object.
        /// Otherwise, it loads the plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchToothpicExchange_Load(object sender, EventArgs e)
        {
            // If the user has not already been authenticated.
            if(call == null)
            {
                // Launch a new AuthForm
                var form = new AuthForm();
                form.ShowDialog();

                // If the AuthForm reports all OK then store the authenticated ToothpicAPI object to be reused for further API calls
                if (form.DialogResult == DialogResult.OK)
                {
                    // Stores the authenticated ToothpicAPI object for reuse.
                    call = form.Call;

                    // Gets the currentl authenticated user's information (mostly for aesthetic reasons, i.e. "Welcome Dr Smith").
                    authenticatedUser = call.GetAuthenticatedUser();

                    // Updates the top right hand corner with the authorised user's name.
                    loggedIn.Text = string.Format("Currently logged in: {0} {1}" , authenticatedUser.FirstName, authenticatedUser.LastName);
                }
                else
                {
                    call = null;
                    this.Close();
                }
                   
            }

            // Get the current patient number from OpenDental.
            pat = Patients.GetPat(PatNum);

            // This block disables the ExportButton if no PT is currently selected in OpenDental
            if (pat != null)
            {
                ExportButton.Enabled = true;
                LabelForExportButton.Text = string.Format("{0} ({1}) is the selected patient.", pat.GetNameFL(), pat.Age);
            }
            else
            {
                ExportButton.Enabled = false;
                LabelForExportButton.Text = "Please select a patient in order to export.";
            }
        }

        /// <summary>
        /// Event handling method to be executed when ExportButton is clicked. Launches an ExportPatientInfo instance. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (pat != null)
            {
                // Opens a new instance of ExportPatientInfo based on the selected PT and passes on the authenticated API object.
                var export = new ExportPatientInfo(pat, call);

                // Attempts to POST the user info to Toothpic API
                ExportResult result = export.PostToToothpic();

                switch (result)
                {
                    case ExportResult.Success:
                        MessageBox.Show("Patient successfully exported to Toothpic API.");
                        break;
                    case ExportResult.AuthenticationFailure:
                        MessageBox.Show("Error exporting. The request was not authenticated.");
                        break;
                    case ExportResult.DuplicateUser:
                        MessageBox.Show("Error exporting. A duplicate user already exists.");
                        break;
                    case ExportResult.Null:
                        MessageBox.Show("Error exporting. A patient was not selected");
                        break;
                    default:
                        MessageBox.Show("Error exporting patient to Toothpic API.");
                        break;
                }
            }
            else
                MessageBox.Show("No patient selected. Please select a patient to export.");
        }

        /// <summary>
        /// Method to be executed when ImportButton is clicked. Launches a new ImportPatientInfo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_Click(object sender, EventArgs e)
        {
            // Opens a new ImportPatientInfo form dialog box and passes on the authenticated API object.
            var form = new ImportPatientInfo(call);
            form.ShowDialog();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            call = null;
            this.Close();
        }
    }
}
