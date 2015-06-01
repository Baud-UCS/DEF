using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baud.Deployment.Web.Framework.Security
{
    public static class Permissions
    {
        public static class Security
        {
            public const string ModulePrefix = "Security.";

            public const string UserRead = ModulePrefix + "User.Read";
            public const string UserSave = ModulePrefix + "User.Save";
            public const string UserManagePositions = ModulePrefix + "User.ManagePositions";

        }
    }
}