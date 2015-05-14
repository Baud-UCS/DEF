using System;
using System.IO;
using Baud.Deployment.BusinessLogic.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.BusinessLogic.Tests.Agents
{
    [TestClass]
    public class WebApiAgentAdapterTests
    {
        //[TestMethod]
        public void _Debug_DeployPackage()
        {
            var adapter = new WebApiAgentAdapter("http://localhost:9000/api/");

            var package = File.ReadAllBytes(@"C:\Temp\DEF\Baud.Deploy.HOS-RS-3.3.0.15118.4.nupkg");
            adapter.DeplyPackage("ControllerSite", package);
        }
    }
}