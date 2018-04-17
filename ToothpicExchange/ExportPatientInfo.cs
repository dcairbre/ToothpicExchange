using OpenDentBusiness;
using OpenDentBusiness.HL7;


namespace ToothpicExchange
{

    /// <summary>
    /// This Enum is to allow ExportPatientInfo report back with the degree of success it had.
    /// This is beacause ExportPatientInfo does not have a UI, it reports back to the homepage with these enums.
    /// </summary>
    public enum ExportResult
    {
        /// <summary>
        /// Successful export.
        /// </summary>
        Success,
        /// <summary>
        /// Generic failure.
        /// </summary>
        Failure,
        /// <summary>
        /// If a duplicate user already exists in Toothpic's API
        /// </summary>
        DuplicateUser,
        /// <summary>
        /// Null object.
        /// </summary>
        Null,
        /// <summary>
        /// Authentication failure.
        /// </summary>
        AuthenticationFailure
    }

    /// <summary>
    /// This class is responsible for exporting the patient in OpenDental to a user in Toothpic's API.
    /// </summary>
    /// <remarks>
    /// It takes a Patient object, converts into a ToothpicUser object, and advises whether the user was POSTed successfully or not
    /// to Toothpic's API.
    /// An API object is required to make a POST request, otherwise it will fail.
    /// </remarks>
    public partial class ExportPatientInfo
    {
        /// <summary>
        /// The current selected patient to be exported.
        /// </summary>
        private Patient pat;

        /// <summary>
        /// The authenticated API call object.
        /// </summary>
        private ToothpicAPI call;


        /// <summary>
        /// This method initialises the class with the current selected patient.
        /// !important: This constructor is generally used for debug only, it is advised ExportPatientInfo(pat, call) is used instead.
        /// </summary>
        /// <param name="pat">Usually the current patient within OpenDental</param>
        public ExportPatientInfo(Patient pat)
        {
            this.pat = pat;
        }

        /// <summary>
        /// This method initialises the class with the current selected patient and an authenticated API call ready to go.
        /// </summary>
        /// <param name="pat">Usually the current patient within OpenDental.</param>
        /// <param name="call">An authenticated API object.</param>
        public ExportPatientInfo(Patient pat, ToothpicAPI call)
        {
            this.pat = pat;
            this.call = call;
        }

        /// <summary>
        /// Implements ToothpicAPI to POST a ToothpicUser.
        /// </summary>
        /// <returns>An enum ExportResult explaining whether the POST was successful or not.</returns>
        public ExportResult PostToToothpic()
        {
            // This converts an OpenDental PT to Toothpic User
            var user = new ToothpicUser(pat);

            // Checks if call is null.
            if (call == null)
                return ExportResult.Null;

            // Checks if the API object is authenticated.
            if (!call.IsAuthenticated())
                return ExportResult.AuthenticationFailure;

            // Checks if a duplicate user already exists on the system.
            if (call.CheckDuplicateUser(user))
                return ExportResult.DuplicateUser;

            // POSTs the user.
            if (call.PostToothpicUser(user))
                return ExportResult.Success;
            else
                return ExportResult.Failure;
        }

        /// <summary>
        /// This method is used to construct a HL7 v2 ADT message to file. It is included for reference.
        /// </summary>
        /// <returns>A boolean whether the file was successfully written or not</returns>
        public bool WriteHL7ADT()
        {
            if(pat != null)
            {
                // Path for export
                var fileWriteDir = @"\";
                var outputFile = string.Format("{0}{1}_{2}.txt", fileWriteDir, pat.LName, pat.FName);
                var outputContents = MessageConstructor.GenerateADT(pat, null, EventTypeHL7.A04).ToString();

                System.IO.File.WriteAllText(outputFile, outputContents);
                return true;
            }
            return false;
        }
    }
}