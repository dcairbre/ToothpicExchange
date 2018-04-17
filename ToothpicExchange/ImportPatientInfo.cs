using System;
using System.Linq;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using System.Collections.Generic;

namespace ToothpicExchange
{
    /// <summary>
    /// This class is responsible for importing users from Toothpic's API into patients in OpenDental.
    /// It takes an API object in order to make authenticated requests.
    /// </summary>
    /// <remarks>
    /// It querys the API to get a list of all ToothpicUsers and adds them to a ListView box.
    /// The user is selected and the import process begins.
    /// A new patient is created in OpenDental, then that patient is opened in a new FormPatientEdit window
    /// whereby the patient's information can be altered.
    /// If the FormPatientEdit window is cancelled then the user is not added.
    /// </remarks>
    public partial class ImportPatientInfo : Form
    {
        /// <summary>
        /// This is the selected Toothpic user from the ListView box. Initialiazed to be null.
        /// </summary>
        private ToothpicUser selectedUser = null;

        /// <summary>
        /// The authenticated API call object.
        /// </summary>
        private ToothpicAPI call;

        /// <summary>
        /// This is a list of Toothpic users fetched from the API
        /// </summary>
        private List<ToothpicUser> users;

        /// <summary>
        /// This method initialises the form.
        /// </summary>
        public ImportPatientInfo(ToothpicAPI call)
        {
            InitializeComponent();
            this.call = call;
        }

        /// <summary>
        /// Event handling method to be executed when form loads. Sets up the ListView and adds the fetched users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportPatientInfo_Load(object sender, EventArgs e)
        {
            if (call == null)
            {
                MessageBox.Show("Error authenticating with Toothpic API");
                DialogResult = DialogResult.Cancel;
            }

            // Fetches a list of Toothpic Users
            users = call.GetToothpicUsers();

            if (users == null)
            {
                MessageBox.Show("There are no users available to import.");
                this.Close();
                return;
            }

            // Formatting the ListView box
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Name", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("DOB", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Gender", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Email", 200, HorizontalAlignment.Left);

            // Adds each of the users to the ListView
            listView1.Items.AddRange(users.Select(user => CreateListViewItem(user)).ToArray());
        }

        /// <summary>
        /// Event handling method. Filters the users in the ListView by first name, last name or email. Updates as they type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            var search = searchBox.Text;

            // Removes everything from the list
            listView1.Items.Clear();

            // Only adds items which match the criteria
            listView1.Items.AddRange(users.Where(i => string.IsNullOrEmpty(search) || i.FirstName.StartsWith(search) || i.LastName.StartsWith(search) || i.Email.StartsWith(search))
            .Select(user => CreateListViewItem(user)).ToArray());
        }

        /// <summary>
        /// A helper function to create each ListViewItem.
        /// </summary>
        /// <param name="user">Any ToothpicUser object.</param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(ToothpicUser user)
        {
            var item = new ListViewItem(new[] {
                string.Format("{0} {2}{1}", user.FirstName, user.LastName, (user.MiddleName != null ? user.MiddleName + " " : "")),
                    user.DateOfBirth.ToString("dd MMM yyyy"),
                    user.Gender,
                    user.Email});
            item.Tag = user;

            return item;
        }

        /// <summary>
        /// Event handling method. When the ListView box is double clicked, this method adds the user to OpenDental as a patient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (selectedUser != null)
                AddNewPatient(selectedUser);
            else
                MessageBox.Show("Please select a user before attempting to import.");
        }

        /// <summary>
        /// Event handling method. When the selection in the ListView box is changed, it updates the selectedUser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedUser = (ToothpicUser)e.Item.Tag;
        }

        /// <summary>
        /// Event handling method. When a user is selected and this button is clicked, it will add that user to OpenDental as a patient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click(object sender, EventArgs e)
        {
            if (selectedUser != null)
                AddNewPatient(selectedUser);
            else
                MessageBox.Show("Please select a user before attempting to import.");
        }

        /// <summary>
        /// This method is modified from OpenDental's FormPatientSelect.cs
        /// It verifies all the user information and checks the user does not already exist
        /// </summary>
        /// <remarks>
        /// If all is good, then it proceeds with opening up an OpenDental FormPatientEdit.cs with the user's information pre-filled
        /// This allows additional information to be added to the Patient before being saved to the DB
        /// If the FormPatientEdit.cs modal is cancelled, the user will not be added to the OpenDental database
        /// </remarks>
        /// <param name="user">Any ToothpicUser</param>
        private static void AddNewPatient(ToothpicUser user)
        {
            // Checks if PT with this name and date of birth already exists.
            long search = Patients.GetPatNumByNameAndBirthday(user.LastName, user.FirstName, user.DateOfBirth);

            if (search != 0)
            {
                // Prompts the user if they'd like to add a new patient or not.
                DialogResult dialogResult = MessageBox.Show(
                    string.Format("A patient by the name {0}, {1} ({2}) already exists. Would you like to create a new patient regardless?",
                        user.LastName, user.FirstName, user.DateOfBirth.ToString("dd MMM yyyy")),
                    "Patient Conflict",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                    return;
            }

            // Sets the new PT as current PT.
            Patient PatCur = user.CreateNewPatient();
            Family FamCur = Patients.GetFamily(PatCur.PatNum);

            // Opens a new OpenDental.FormPatientEdit modal.
            var FormPE = new FormPatientEdit(PatCur, FamCur);
            // This boolean is very important, it ensures if the process is cancelled then the user will not be added to the DB
            FormPE.IsNew = true;
            FormPE.ShowDialog();

            // If the user clicked "ok" on the OpenDental.FormPatientEdit then this will save the new patient to OpenDental
            // Otherwise, it will notify the user that the patient was not imported.
            if (FormPE.DialogResult == DialogResult.OK)
            {
                // Modified from FormOpenDental.cs
                // Update the currently selected PT in OpenDental
                FormOpenDental.CurPatNum = PatCur.PatNum;
                FormOpenDental.S_RefreshCurrentModule();
                MessageBox.Show("Patient successfully added.");
            }
            else
            {
                MessageBox.Show("Patient was not imported.");
            }
        }
    }
}
