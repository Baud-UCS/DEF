using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Agents;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.BusinessLogic.Tests.Agents
{
    [TestClass]
    public class WebApiAgentAdapterDebugTests
    {
        ////[TestMethod]
        public async Task _Debug_DeplyPackageAsync()
        {
            var adapter = CreateAdapter();

            var package = File.ReadAllBytes(@"C:\Temp\DEF\Baud.Deploy.HOS-RS-3.3.0.15118.4.nupkg");
            var result = await adapter.DeplyPackageAsync("ControllerSite", package);

            result.Should().NotBeNull();
        }

        #region Shared parameters

        ////[TestMethod]
        public async Task _Debug_GetSharedParameters()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSharedParameters();

            result.Should().NotBeEmpty();
        }

        ////[TestMethod]
        public async Task _Debug_GetSharedParameter_Existing()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSharedParameter("KeyOne");

            result.Should().NotBeNull();
        }

        ////[TestMethod]
        public async Task _Debug_GetSharedParameter_NotExisting()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSharedParameter("missingKey");

            result.Should().BeNull();
        }

        ////[TestMethod]
        public async Task _Debug_SetSharedParameters()
        {
            var adapter = CreateAdapter();

            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "First value" },
                { "KeyTwo", "Second value" }
            };
            await adapter.SetSharedParameters(parameters);
        }

        ////[TestMethod]
        public async Task _Debug_SetSharedParameter()
        {
            var adapter = CreateAdapter();

            await adapter.SetSharedParameter("KeyOne", "One value");
        }

        ////[TestMethod]
        public async Task _Debug_DeleteSharedParameter()
        {
            var adapter = CreateAdapter();

            await adapter.DeleteSharedParameter("KeyOne");
        }

        #endregion Shared parameters

        #region Site parameters

        ////[TestMethod]
        public async Task _Debug_GetSiteParameters()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSiteParameters("TestSite");

            result.Should().NotBeEmpty();
        }

        ////[TestMethod]
        public async Task _Debug_GetSiteParameter_Existing()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSiteParameter("TestSite", "KeyOne");

            result.Should().NotBeNull();
        }

        ////[TestMethod]
        public async Task _Debug_GetSiteParameter_NotExisting()
        {
            var adapter = CreateAdapter();

            var result = await adapter.GetSiteParameter("TestSite", "missingKey");

            result.Should().BeNull();
        }

        ////[TestMethod]
        public async Task _Debug_SetSiteParameters()
        {
            var adapter = CreateAdapter();

            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "First value" },
                { "KeyTwo", "Second value" }
            };
            await adapter.SetSiteParameters("TestSite", parameters);
        }

        ////[TestMethod]
        public async Task _Debug_SetSiteParameter()
        {
            var adapter = CreateAdapter();

            await adapter.SetSiteParameter("TestSite", "KeyOne", "One value");
        }

        ////[TestMethod]
        public async Task _Debug_DeleteSiteParameter()
        {
            var adapter = CreateAdapter();

            await adapter.DeleteSiteParameter("TestSite", "KeyOne");
        }

        #endregion Site parameters

        private WebApiAgentAdapter CreateAdapter()
        {
            return new WebApiAgentAdapter("http://localhost:9000/");
        }
    }
}