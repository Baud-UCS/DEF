using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Framework.Security
{
    public class RequireContextRightAttribute : AuthorizeAttribute
    {
        public string RequiredContextRight { get; private set; }

        public string[] PossibleContextRights { get; private set; }

        public RequireContextRightAttribute(string requiredContextRight)
        {
            RequiredContextRight = requiredContextRight;
        }

        public RequireContextRightAttribute(params string[] possibleContextRights)
        {
            PossibleContextRights = possibleContextRights;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var principal = httpContext.User as System.Security.Claims.ClaimsPrincipal;

            if (principal == null)
            {
                return false;
            }

            if (RequiredContextRight != null)
            {
                return principal.HasContextRight(RequiredContextRight);
            }
            else
            {
                return PossibleContextRights.Any(r => principal.HasContextRight(r));
            }
        }
    }
}