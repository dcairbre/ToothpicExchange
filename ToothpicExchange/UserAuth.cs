using System;

namespace ToothpicExchange
{
    /// <summary>
    /// This class contains the Auth object which is used for authenticating API calls.
    /// </summary>
    class UserAuth
    {
        /// <summary>
        /// The Auth token used for API calls. Returned from API once user is authenticated.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// The authenticated user's ID. Returned from API once user is authenticated
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The member_tracking_id returned from API once user is authenticated.
        /// </summary>
        public string MemberTrackingId { get; set; }

        public UserAuth()
        {
            AuthToken = "";
            UserId = "";
            MemberTrackingId = "";
        }

        public override string ToString()
        {
            return string.Format("AuthToken: {0}\nUserId: {1}\nMemberTrackingId: {2}", AuthToken, UserId, MemberTrackingId);
        }

    }
}
