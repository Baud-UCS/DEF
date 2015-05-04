using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baud.Deployment.Web.App_Start
{
    public static class ContainerConfig
    {
        //public static Func<Type, IEnumerable<InjectionMember>> WithImportedProperties(Func<PropertyInfo, bool> propertyPredicate)
        //{
        //    return (Type type) =>
        //    {
        //        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(p => p.CanWrite);
        //        return properties.Where(propertyPredicate).Select(x => new InjectionProperty(x.Name));
        //    };
        //}
    }
}