using System;
using System.Collections.Generic;
using RestSharp;

namespace ToothpicExchange
{
    /// <summary>
    /// ToothpicAPI is the class responsible for handling Toothpic's REST API. All of its attributed are private. It holds the API's URL and the UserAuth object.
    /// </summary>
    public class ToothpicAPI
    {
        /// <summary>
        /// This is the API's BaseURL. It is changed in the APISettings.settings config file before build.
        /// </summary>
        private string BaseUrl = APISettings.Default.ToothpicAPIBaseURI;

        /// <summary>
        /// This is the API's authentication object.
        /// </summary>
        private UserAuth Auth;

        /// <summary>
        /// The API key for this client (i.e Open Dental).
        /// </summary>
        private string apiKey = "1234";

        /// <summary>
        /// The API endpoint for authentication.
        /// </summary>
        private string authEndpoint = "/auth/";

        /// <summary>
        /// The API endpoint for users.
        /// </summary>
        private string userEndpoint = "/users/";

        /// <summary>
        /// Contains the BaseUrl, Authentication object and an API key.
        /// </summary>
        public ToothpicAPI()
        {
            BaseUrl = APISettings.Default.ToothpicAPIBaseURI;
            Auth = null;
            apiKey = "";
        }

        /// <summary>
        /// This method executes a REST request.
        /// It is from the RestSharp class http://restsharp.org
        /// See RestSharp documentation for more info.
        /// It makes a request and adds the necessary headers for authentication etc.
        /// </summary>
        /// <typeparam name="T">Can be any type, normally ToothpicUser</typeparam>
        /// <param name="request">Any RestSharp request object</param>
        /// <returns>Returns the object specified, normally ToothpicUser.</returns>
        public T Execute<T>(RestRequest request) where T : new()
        {
            // Sets up a new client.
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);

            // Adds authentication headers to the request.
            request.AddHeader("x-signature", Auth.AuthToken);
            request.AddHeader("x-auth-token", Auth.AuthToken);
            request.AddHeader("x-api-key", apiKey);

            // Makes the request.
            var response = client.Execute<T>(request);

            // Throws an exception if there are any errors.
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving API response.";
                ApplicationException toothpicAPIException = new ApplicationException(message, response.ErrorException);
                throw toothpicAPIException;
            }

            // Returns the relevant object.
            return response.Data;
        }

        /// <summary>
        /// Calls the API to authenticate the user. Returns a UserAuth object if successful, otherwise returns null.
        /// </summary>
        /// <param name="username">The user's username (or email)</param>
        /// <param name="password">The user's password</param>
        /// <returns>Returns itself if authentication was successful, otherwise null.</returns>
        public ToothpicAPI AuthenticateUser(string username, string password)
        {
            // The auth_type corresponding to Toothpic API
            var authType = "practice_user";

            // Sets up a new POST request
            var request = new RestRequest(Method.POST);

            // This is the API endpoint, can easily be changed
            request.Resource = authEndpoint;

            // Specifies content-type: application/json
            request.RequestFormat = DataFormat.Json;

            // The JSON body
            request.AddBody(new
            {
                email = username,
                auth_type = authType,
                password = password
            });

            // BEGIN DEBUG

            Auth = new UserAuth  
            {
                AuthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzY2hlbWEiOiJ0b290aHBpYyIsInJvbGVzIjpbInJlZ3VsYXIiXSwidHlwIjoiand0IiwiaXNzIjoiR3JpYW5naHJhZkRlRmlhY2FpbCIsImlhdCI6MTUxOTkzNDIyOCwicmVzb3VyY2UiOiJVc2VyIn0.q5nEBhAYLB5QpaGYerbJBIuEQYeuTHs1mXt13Jm-DJg",
                UserId = "1111",
                MemberTrackingId = "aa0fc553181fe5ee66c0973c5868cc16c3035955"
            };
            
            if (username == "hello")
                return this;
            else
                return null;

            // END DEBUG
            
            // Attempts to POST and returns object as necessary
            try
            {
                Auth = Execute<UserAuth>(request);
                return this;
            }
            catch (ApplicationException)
            {
                return null;
            }

        }

        /// <summary>
        /// A boolean indicating whether the API object is authenticated or not.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return (Auth != null);
        }

        /// <summary>
        /// Fetches the current authorised user (by their user ID). Primarily used in LaunchToothpicExchange.cs.
        /// </summary>
        /// <returns>The currently authenticated user as a ToothpicUser object, otherwise null.</returns>
        public ToothpicUser GetAuthenticatedUser()
        {
            if (Auth != null)
                return GetToothpicUser(Auth.UserId);
            else
                return null;
        }

        /// <summary>
        /// This method fetches a user from the Toothpic API by a specified User ID.
        /// </summary>
        /// <param name="userId">A Toothpic User ID.</param>
        /// <returns>The specified ToothpicUser.</returns>
        public ToothpicUser GetToothpicUser(string userId)
        {
            // Sets up a new GET request
            var request = new RestRequest();

            // this is the API endpoint, can easily be changed
            request.Resource = userEndpoint + userId;

            // Attempts to GET and returns null if necessary
            try
            {
                return Execute<ToothpicUser>(request);
            }
            catch (ApplicationException)
            {
                return null;
            }
        }

        /// <summary>
        /// This method fetches a list of Toothpic users from the API.
        /// </summary>
        /// <returns>A List of Toothpic Users, null otherwise.</returns>
        public List<ToothpicUser> GetToothpicUsers()
        {
            // Sets up a new GET request
            var request = new RestRequest();

            // this is the API endpoint, can easily be changed
            request.Resource = userEndpoint;

            // if necessary, request.RootElement maybe required, see RestSharp documentation for more info

            // Attempts to GET and returns null if necessary
            try
            {
                return Execute<List<ToothpicUser>>(request);
            }
            catch (ApplicationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Fetches a list of users and checks for duplictes.
        /// </summary>
        /// <param name="searchUser">The user to be checked.</param>
        /// <returns></returns>
        public bool CheckDuplicateUser(ToothpicUser searchUser)
        {
            var users = GetToothpicUsers();

            if(users != null)
            {
                foreach (var user in users)
                {
                    if (searchUser.Equals(user))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method POSTs a Toothpic User to the API.
        /// The JSON formatting is defined within the method.
        /// </summary>
        /// <param name="user">Any ToothpicUser.</param>
        /// <returns>A boolean whether it POSTed successfully or not</returns>
        public bool PostToothpicUser(ToothpicUser user)
        {
            // Sets up a new POST request
            var request = new RestRequest(Method.POST);

            // This is the API endpoint, can easily be changed
            request.Resource = userEndpoint;

            // Specifies content-type: application/json
            request.RequestFormat = DataFormat.Json;

            // The JSON body
            request.AddBody(new
            {
                dob = user.DateOfBirth.ToString("yyyy-MM-dd"),
                email = user.Email,
                first_name = user.FirstName,
                middle_name = user.MiddleName,
                last_name = user.LastName,
                gender = user.Gender,
                zip_code = user.ZipCode
            });

            // Attempts to POST and returns a boolean as necessary
            try
            {
                var response = Execute<ToothpicUser>(request);
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }
    }
}
