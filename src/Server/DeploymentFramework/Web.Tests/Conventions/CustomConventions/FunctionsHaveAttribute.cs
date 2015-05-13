using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Baud.Deployment.Web.Tests.Conventions.CustomConventions
{
    public class FunctionsHaveAttribute : FunctionsConfirm
    {
        public Type RequiredAttributeType { get; private set; }

        public FunctionsHaveAttribute(Func<IEnumerable<MethodInfo>, IEnumerable<MethodInfo>> methodsFilter, Type requiredAttributeType)
            : base(methodsFilter, x => x.GetCustomAttribute(requiredAttributeType) != null, string.Format("Defined functions must be marked with {0} attribute", requiredAttributeType))
        {
            RequiredAttributeType = requiredAttributeType;
        }
    }
}