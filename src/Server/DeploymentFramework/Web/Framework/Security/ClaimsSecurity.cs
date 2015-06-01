using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Baud.Deployment.Web.Framework.Security
{
    public static class ClaimsSecurity
    {
        public const string PermissionClaimName = "http://schemas.baud.cz/2014/08/claims/permission";

        public static bool HasPermission(this ClaimsPrincipal principal, string permission)
        {
            return principal.HasClaim(PermissionClaimName, permission);
        }

        public static bool HasPermission(this IPrincipal userPrincipal, string permission)
        {
            var principal = userPrincipal as System.Security.Claims.ClaimsPrincipal;

            return principal != null && principal.HasPermission(permission);
        }
    }
}