using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.Contracts;
using Microsoft.AspNet.Identity;

namespace Baud.Deployment.Web.Framework.Security
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private const int NotAuthenticatedUserID = 0;

        public int GetCurrentUserID()
        {
            var identity = HttpContext.Current.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return NotAuthenticatedUserID;
            }

            int userID = identity.GetUserId<int>();
            return userID;
        }
    }
}