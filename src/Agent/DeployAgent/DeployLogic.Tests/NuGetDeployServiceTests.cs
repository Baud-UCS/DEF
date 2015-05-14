using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic;
using Baud.Deployment.DeployLogic.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Baud.Deployment.DeployLogic.Tests
{
    [TestClass]
    public class NuGetDeployServiceTests
    {
        //[TestMethod]
        public void _Debug_DeployPackage()
        {
            var configuration = Substitute.For<IConfigurationProvider>();
            configuration.PackagesRootPath.Returns(@"C:\Temp\DEF\Agent");

            var sites = Substitute.For<ISitesService>();
            sites.GetSiteParameters("TestSite").Returns(new Dictionary<string, string>());
            sites.GetSharedParameters().Returns(new Dictionary<string, string>());

            var script = Substitute.For<IScriptService>();

            var deployService = new NuGetDeployService(configuration, sites, script);

            using (var packageStream = File.OpenRead(@"C:\Temp\DEF\Baud.Deploy.HOS-RS-3.3.0.15118.4.nupkg"))
            {
                deployService.DeployPackage("TestSite", packageStream);
            }
        }

        //[TestMethod]
        public void _Debug_InstallDeployScripts()
        {
            var configuration = Substitute.For<IConfigurationProvider>();
            configuration.PackagesRootPath.Returns(@"C:\Temp\DEF\Agent");

            var sites = Substitute.For<ISitesService>();
            sites.GetSiteParameters("Shared").Returns(new Dictionary<string, string>());
            sites.GetSharedParameters().Returns(new Dictionary<string, string>());

            var script = Substitute.For<IScriptService>();

            var deployService = new NuGetDeployService(configuration, sites, script);

            using (var packageStream = File.OpenRead(@"C:\Temp\DEF\DEF.DeployScripts.0.1.0.nupkg"))
            {
                deployService.DeployPackage("Shared", packageStream);
            }
        }
    }
}