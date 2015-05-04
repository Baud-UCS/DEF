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
        public const string ContextRightClaimName = "http://schemas.baud.cz/2014/08/claims/contextright";

        public static bool HasContextRight(this ClaimsPrincipal principal, string contextRight)
        {
            return principal.HasClaim(ContextRightClaimName, contextRight);
        }

        public static bool HasContextRight(this IPrincipal userPrincipal, string contextRight)
        {
            var principal = userPrincipal as System.Security.Claims.ClaimsPrincipal;

            return principal != null && principal.HasContextRight(contextRight);
        }
    }
}