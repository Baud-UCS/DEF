using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using RestSharp;
using RestSharp.Contrib;

namespace Baud.Deployment.BusinessLogic.Agents
{
    public class WebApiAgentAdapter : IAgentAdapter
    {
        private readonly string _baseUrl;

        public WebApiAgentAdapter(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<DeployLogic.Models.Deployment> DeplyPackageAsync(string siteID, byte[] package)
        {
            var request = new RestRequest(Urls.Deploy, Method.POST);
            request.AddUrlSegment(UrlSegments.Site, siteID);
            request.AddFile(Parameters.Package, package, "package.nupkg");

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync<DeployLogic.Models.Deployment>(request);

            return response.Data;
        }

        #region Shared parameters

        public async Task<IDictionary<string, string>> GetSharedParameters()
        {
            var request = new RestRequest(Urls.SharedParameters, Method.GET);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync<Dictionary<string, string>>(request);

            return response.Data;
        }

        public async Task<string> GetSharedParameter(string parameterName)
        {
            var request = new RestRequest(Urls.SharedParameter, Method.GET);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var result = UnwrapString(response.Content);
            return result;
        }

        public async Task SetSharedParameters(IDictionary<string, string> parameters)
        {
            var request = new RestRequest(Urls.SharedParameters, Method.POST);
            request.AddJsonBody(parameters);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        public async Task SetSharedParameter(string parameterName, string stringValue)
        {
            var request = new RestRequest(Urls.SharedParameter, Method.PUT);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);
            request.AddJsonBody(stringValue);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        public async Task DeleteSharedParameter(string parameterName)
        {
            var request = new RestRequest(Urls.SharedParameter, Method.DELETE);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        #endregion Shared parameters

        #region Site parameters

        public async Task<IDictionary<string, string>> GetSiteParameters(string siteID)
        {
            var request = new RestRequest(Urls.SiteParameters, Method.GET);
            request.AddUrlSegment(UrlSegments.Site, siteID);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync<Dictionary<string, string>>(request);

            return response.Data;
        }

        public async Task<string> GetSiteParameter(string siteID, string parameterName)
        {
            var request = new RestRequest(Urls.SiteParameter, Method.GET);
            request.AddUrlSegment(UrlSegments.Site, siteID);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var result = UnwrapString(response.Content);
            return result;
        }

        public async Task SetSiteParameters(string siteID, IDictionary<string, string> parameters)
        {
            var request = new RestRequest(Urls.SiteParameters, Method.POST);
            request.AddUrlSegment(UrlSegments.Site, siteID);
            request.AddJsonBody(parameters);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        public async Task SetSiteParameter(string siteID, string parameterName, string stringValue)
        {
            var request = new RestRequest(Urls.SiteParameter, Method.PUT);
            request.AddUrlSegment(UrlSegments.Site, siteID);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);
            request.AddJsonBody(stringValue);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        public async Task DeleteSiteParameter(string siteID, string parameterName)
        {
            var request = new RestRequest(Urls.SiteParameter, Method.DELETE);
            request.AddUrlSegment(UrlSegments.Site, siteID);
            request.AddUrlSegment(UrlSegments.Parameter, parameterName);

            var client = CreateClient();
            var response = await client.ExecuteTaskAsync(request);
        }

        #endregion Site parameters

        private IRestClient CreateClient()
        {
            return new RestClient(_baseUrl);
        }

        private string UnwrapString(string input)
        {
            return input.Trim('"');
        }

        private static class Urls
        {
            public const string Deploy = "api/deploy/{site}";
            public const string SharedParameters = "api/parameters";
            public const string SharedParameter = "api/parameters/{parameter}";

            public const string SiteParameters = "api/sites/{site}/parameters";
            public const string SiteParameter = "api/sites/{site}/parameters/{parameter}";
        }

        private static class UrlSegments
        {
            public const string Site = "site";
            public const string Parameter = "parameter";
        }

        private static class Parameters
        {
            public const string Package = "package";
        }
    }
}