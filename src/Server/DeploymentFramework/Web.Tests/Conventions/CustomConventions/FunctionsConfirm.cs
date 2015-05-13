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
    public class FunctionsConfirm : IConvention<Types>
    {
        public Func<IEnumerable<MethodInfo>, IEnumerable<MethodInfo>> MethodsFilter { get; private set; }
        public Func<MethodInfo, bool> MethodRequirement { get; private set; }

        public FunctionsConfirm(Func<IEnumerable<MethodInfo>, IEnumerable<MethodInfo>> methodsFilter, Func<MethodInfo, bool> methodRequirement, string conventionReason = null)
        {
            MethodsFilter = methodsFilter;
            MethodRequirement = methodRequirement;

            ConventionReason = conventionReason ?? string.Format("Defined functions must confirm with {0}", MethodRequirement);
        }

        public string ConventionReason { get; set; }

        public void Execute(Types data, IConventionResultContext result)
        {
            var allmethods = data.TypesToVerify.SelectMany(x => x.GetMethods());
            var methodsToTest = MethodsFilter(allmethods);
            var violatingMethods = methodsToTest.Where(m => !MethodRequirement(m));
            result.Is(ConventionReason, violatingMethods);
        }
    }
}