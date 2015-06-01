using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Baud.Deployment.Web.Framework.Security
{
    public class StaticPermissionsProvider : IPermissionProvider
    {
        public IEnumerable<string> GetAllPermissions()
        {
            var types = typeof(Permissions).GetNestedTypes(BindingFlags.Public);

            return types.SelectMany(x => x.GetFields(BindingFlags.Public | BindingFlags.Static))
                .Where(x => x.FieldType == typeof(string))
                .Select(x => (string)x.GetValue(null))
                .Where(x => !x.EndsWith("."));
        }
    }
}