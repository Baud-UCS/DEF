using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.NuGetHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.DeployLogic.Tests.NuGetHelpers
{
    [TestClass]
    public class SiteProjectSystemTests
    {
        [TestMethod]
        public void GetPropertyValue_ReturnsSharedParameter()
        {
            var siteSpecificParameters = new Dictionary<string, string>();
            var sharedParameters = new Dictionary<string, string> { { "TestKey", "SharedValue" } };

            var fileSystem = NSubstitute.Substitute.For<NuGet.IFileSystem>();
            var projectSystem = new SiteProjectSystem(fileSystem, siteSpecificParameters, sharedParameters);

            object result = projectSystem.GetPropertyValue("TestKey");
            result.Should().Be("SharedValue");
        }

        [TestMethod]
        public void GetPropertyValue_ReturnsSiteSpecificParameter()
        {
            var siteSpecificParameters = new Dictionary<string, string> { { "TestKey", "SpecificValue" } };
            var sharedParameters = new Dictionary<string, string>();

            var fileSystem = NSubstitute.Substitute.For<NuGet.IFileSystem>();
            var projectSystem = new SiteProjectSystem(fileSystem, siteSpecificParameters, sharedParameters);

            object result = projectSystem.GetPropertyValue("TestKey");
            result.Should().Be("SpecificValue");
        }

        [TestMethod]
        public void GetPropertyValue_ReturnsSiteSpecificParameterOverSharedParameter()
        {
            var siteSpecificParameters = new Dictionary<string, string> { { "TestKey", "SpecificValue" } };
            var sharedParameters = new Dictionary<string, string> { { "TestKey", "SharedValue" } };

            var fileSystem = NSubstitute.Substitute.For<NuGet.IFileSystem>();
            var projectSystem = new SiteProjectSystem(fileSystem, siteSpecificParameters, sharedParameters);

            object result = projectSystem.GetPropertyValue("TestKey");
            result.Should().Be("SpecificValue");
        }
    }
}