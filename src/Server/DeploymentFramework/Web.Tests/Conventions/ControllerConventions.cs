using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Baud.Deployment.Web.Tests.Conventions.CustomConventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;
using TestStack.ConventionTests.Conventions;

namespace Baud.Deployment.Web.Tests.Conventions
{
    [TestClass]
    public class ControllerConventions
    {
        private readonly Types _mvcControllers;

        public ControllerConventions()
        {
            _mvcControllers = Types.InAssemblyOf<Baud.Deployment.Web.Startup>(
                "MVC Controllers",
                types => from t in types
                         where typeof(IController).IsAssignableFrom(t)
                         where !t.Name.EndsWith("ControllerBase")
                         where t.Namespace == null || !t.Namespace.StartsWith("T4MVC")
                         select t);
        }

        [TestMethod]
        public void ControllersHaveControllerSuffix()
        {
            Convention.Is(new MvcControllerNameAndBaseClassConvention(), _mvcControllers);
        }

        [TestMethod]
        public void PostMethodsHaveValidateAntiForgeryTokenAttribute()
        {
            Convention.Is(
                new FunctionsHaveAttribute(methods => methods.Where(m => m.GetCustomAttribute<HttpPostAttribute>() != null), typeof(ValidateAntiForgeryTokenAttribute)),
                _mvcControllers);
        }
    }
}