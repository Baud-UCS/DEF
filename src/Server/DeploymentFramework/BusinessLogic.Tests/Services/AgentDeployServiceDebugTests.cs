using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Providers;
using Baud.Deployment.BusinessLogic.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Baud.Deployment.BusinessLogic.Tests.Services
{
    [TestClass]
    public class AgentDeployServiceDebugTests
    {
        ////[TestMethod]
        public async Task _Debug_ProcessInstallationAsync_Success()
        {
            var uow = Substitute.For<IDeploymentUow>();
            var packagesProvider = Substitute.For<IDeployPackagesProvider>();
            packagesProvider.GetPackageBytes(null).ReturnsForAnyArgs(x => File.ReadAllBytes(@"C:\Temp\DEF\Baud.Deploy.HOS-RS-3.3.0.15118.4.nupkg"));

            var service = new AgentDeployService(() => uow, new WebApiAgentAdapterProvider(), packagesProvider);

            var installation = new Installation
            {
                DeployTarget = new DeployTarget
                {
                    Site = new ServerSite
                    {
                        Key = "Test",
                        Server = new Server
                        {
                            AgentUrl = "http://localhost:9000/"
                        }
                    }
                }
            };
            var result = await service.ProcessInstallationAsync(installation);

            result.Should().Be(Baud.Deployment.BusinessLogic.Domain.Deployment.Enums.InstallationState.Success);
        }
    }
}