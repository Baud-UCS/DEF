﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Baud.Deployment.DeployAgent.Tests.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NSubstitute;

namespace Baud.Deployment.DeployAgent.Tests.Api
{
    [TestClass]
    public class SiteParametersControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsParametersFromSitesService()
        {
            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "First value" },
                { "KeyTwo", "Second value" }
            };

            var context = new ApiTestContext();
            context.SitesService.GetSiteParameters("TestSite").Returns(new ReadOnlyDictionary<string, string>(parameters));

            using (var client = context.GetHttpClient())
            {
                var response = await client.GetAsync("http://localhost/api/sites/TestSite/parameters");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var responseObject = await response.Content.ReadAsJson<JObject>();
                responseObject.Should().Contain("KeyOne", "First value");
                responseObject.Should().Contain("KeyTwo", "Second value");
            }
        }

        [TestMethod]
        public async Task Get_Returns404_IfSiteNotExist()
        {
            var context = new ApiTestContext();
            context.SitesService.GetSiteParameters("TestSite").Returns(x => { throw new ArgumentOutOfRangeException(); });

            using (var client = context.GetHttpClient())
            {
                var response = await client.GetAsync("http://localhost/api/sites/TestSite/parameters");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public async Task GetSingle_ReturnsValue_IfPresent()
        {
            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "First value" }
            };

            var context = new ApiTestContext();
            context.SitesService.GetSiteParameters("TestSite").Returns(new ReadOnlyDictionary<string, string>(parameters));

            using (var client = context.GetHttpClient())
            {
                var response = await client.GetAsync("http://localhost/api/sites/TestSite/parameters/KeyOne");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var responseObject = await response.Content.ReadAsJson<string>();
                responseObject.Should().Be("First value");
            }
        }

        [TestMethod]
        public async Task GetSingle_Returns404_IfNotPresent()
        {
            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "First value" }
            };

            var context = new ApiTestContext();
            context.SitesService.GetSiteParameters("TestSite").Returns(new ReadOnlyDictionary<string, string>(parameters));

            using (var client = context.GetHttpClient())
            {
                var response = await client.GetAsync("http://localhost/api/sites/TestSite/parameters/UnknownKey");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public async Task Post_SetsMultipleParameters()
        {
            var parameters = new Dictionary<string, string>
            {
                { "KeyOne", "Updated value 1" },
                { "KeyTwo", "Updated value 2" }
            };

            var context = new ApiTestContext();

            using (var client = context.GetHttpClient())
            {
                var response = await client.PostAsJsonAsync("http://localhost/api/sites/TestSite/parameters", parameters);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            context.SitesService.Received().SetSiteParameter("TestSite", "KeyOne", "Updated value 1");
            context.SitesService.Received().SetSiteParameter("TestSite", "KeyTwo", "Updated value 2");
        }

        [TestMethod]
        public async Task Put_SetsSingleParameter()
        {
            var context = new ApiTestContext();

            using (var client = context.GetHttpClient())
            {
                var response = await client.PutAsJsonAsync("http://localhost/api/sites/TestSite/parameters/TestKey", "New value");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            context.SitesService.Received().SetSiteParameter("TestSite", "TestKey", "New value");
        }

        [TestMethod]
        public async Task Delete_RemovesSingleParameter()
        {
            var context = new ApiTestContext();

            using (var client = context.GetHttpClient())
            {
                var response = await client.DeleteAsync("http://localhost/api/sites/TestSite/parameters/TestKey");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            context.SitesService.Received().RemoveSiteParameter("TestSite", "TestKey");
        }
    }
}