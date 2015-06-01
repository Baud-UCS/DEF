using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Framework.Security
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public string RequiredPermission { get; private set; }

        public string[] PossiblePermissions { get; private set; }

        public RequirePermissionAttribute(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }

        public RequirePermissionAttribute(params string[] possiblePermissions)
        {
            PossiblePermissions = possiblePermissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var principal = httpContext.User as System.Security.Claims.ClaimsPrincipal;

            if (principal == null)
            {
                return false;
            }

            if (RequiredPermission != null)
            {
                return principal.HasPermission(RequiredPermission);
            }
            else
            {
                return PossiblePermissions.Any(r => principal.HasPermission(r));
            }
        }
    }
}