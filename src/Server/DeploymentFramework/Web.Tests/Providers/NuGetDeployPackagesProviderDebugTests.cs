using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.Web.Providers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.Web.Tests.Providers
{
    [TestClass]
    public class NuGetDeployPackagesProviderDebugTests
    {
        ////[TestMethod]
        public void _Debug_ListAvailablePackages()
        {
            var provider = new NuGetDeployPackagesProvider(@"C:\Temp\DEF\Packages");
            var packages = provider.GetAvailablePackagesInfo().ToList();

            packages.Should().NotBeEmpty();
        }

        ////[TestMethod]
        public void _Debug_GetPackageVersions()
        {
            var provider = new NuGetDeployPackagesProvider(@"C:\Temp\DEF\Packages");

            var package = provider.GetPackages().First();
            var versions = provider.GetPackageVersions(package.Key);

            versions.Should().NotBeEmpty();
        }

        ////[TestMethod]
        public void _Debug_GetPackageFileFullPath()
        {
            var provider = new NuGetDeployPackagesProvider(@"C:\Temp\DEF\Packages");

            var packagePath = provider.GetPackageFileFullPath("Baud.Deploy.FEP-RS", "1.5.15009.2");
            packagePath.Should().Be(@"C:\Temp\DEF\Packages\Baud.Deploy.FEP-RS.1.5.15009.2\Baud.Deploy.FEP-RS.1.5.15009.2.nupkg");
        }
    }
}