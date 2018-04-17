using System;
using OpenDentBusiness;
using RestSharp.Deserializers;

namespace ToothpicExchange
{
    /// <summary>
    /// This class contains the user model as defined by Toothpic's API.
    /// It contains methods to convert a Toothpic User to an OpenDental Patient, and vice versa.
    /// </summary>
    public class ToothpicUser
    {
        /// <summary>
        /// DateOfBirth mapped to Toothpic API as "dob".
        /// </summary>
        [DeserializeAs(Name = "dob")]
        public DateTime DateOfBirth { get; set; } 

        /// <summary>
        /// FirstName mapped to Toothpic API as "first_name".
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// MiddleName mapped to Toothpic API as "middle_name".
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// LastName mapped to Toothpic API as "last_name".
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ZipCode mapped to Toothpic API as "zip_code".
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gender, recorded as a string from the Toothpic API ("male", "female", "other") which maps to OpenDental's enum PatientGender (male, female, unknown).
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Email address from Toothpic API.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Member Number from Toothpic API, not currently used in OpenDental.
        /// </summary>
        public string MemberNumber { get; set; }

        /// <summary>
        /// Contains all of the user attributes. ToothpicUser(Patient pat) is recommended.
        /// </summary>
        public ToothpicUser()
        {
            DateOfBirth = new DateTime(0);
            FirstName = "";
            MiddleName = "";
            LastName = "";
            ZipCode = "";
            Gender = "other";
            Email = "";
            MemberNumber = "";
        }

        /// <summary>
        /// Maps an OpenDental patient to a Toothpic user.
        /// </summary>
        /// <param name="Pat">An OpenDental patient</param>
        public ToothpicUser(Patient Pat)
        {
            DateOfBirth = Pat.Birthdate;
            FirstName = Pat.FName;
            MiddleName = Pat.MiddleI;
            LastName = Pat.LName;
            ZipCode = Pat.Zip;
            Gender = (Pat.Gender == PatientGender.Male ? "male" : (Pat.Gender == PatientGender.Female ? "female" : "other"));
            Email = Pat.Email;
            MemberNumber = null;
        }

        /// <summary>
        /// Modified from OpenDental's Patients.CreateNewPatient
        /// !important: assumes the user information has already been validated.
        /// It creates a new patient from the current instance of ToothpicUser and adds them to the database.
        /// </summary>
        /// <remarks>
        /// This is a complex method which currently conforms to OpenDental 17.2 but should be carefully monitored.
        /// This method is used in ImportPatientInfo.AddNewPatient
        /// It does the majority of the mapping from a ToothpicUser to an OpenDental patient.
        /// </remarks>
        /// <returns>A new patient object</returns>
        public Patient CreateNewPatient()
        {
            // makes a record in the log where this patient originated from
            var securityLogMsg = "Patient created from ToothpicExchange.";

            var patient = new Patient();

            // PT name must be longer than 1 character
            // These 2 blocks simply format the PT's name
            if (LastName.Length > 1)
                patient.LName = LastName.Substring(0, 1).ToUpper() + LastName.Substring(1);
            if (FirstName.Length > 1)
                patient.FName = FirstName.Substring(0, 1).ToUpper() + FirstName.Substring(1);
            if (MiddleName != null)
            {
                if (MiddleName.Length > 0)
                    patient.MiddleI = MiddleName.Substring(0, 1).ToUpper() + MiddleName.Substring(1);
            }
            
            // This is the mapping from ToothpicUser to Patient
            patient.Birthdate = DateOfBirth;
            // Assumes the user is not deceased
            patient.PatStatus = PatientStatus.Patient;
            patient.BillingType = PrefC.GetLong(PrefName.PracticeDefaultBillType);
            patient.Gender = (Gender == "male" ? PatientGender.Male : (Gender == "female" ? PatientGender.Female : PatientGender.Unknown));
            patient.Email = Email;
            patient.Zip = ZipCode;
            
            // Inserts the PT into the DB
            // "false" indicates we do not currently have a Primary Key for the PT
            Patients.Insert(patient, false);
            SecurityLogs.MakeLogEntry(Permissions.PatientCreate, patient.PatNum, securityLogMsg, LogSources.None);

            // Final clean up methods
            var custRef = new CustReference();
            custRef.PatNum = patient.PatNum;
            CustReferences.Insert(custRef);
            var PatOld = patient.Copy();
            patient.Guarantor = patient.PatNum;
            Patients.Update(patient, PatOld);
            
            // Returns the newly created PT
            return patient;

        }

        /// <summary>
        /// Simply formats ToothpicUser as a string.
        /// </summary>
        /// <returns>A string with each item separated by a line break.</returns>
        override public string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}", FirstName, MiddleName, LastName, ZipCode, Gender, Email, MemberNumber);
        }
    }
}
