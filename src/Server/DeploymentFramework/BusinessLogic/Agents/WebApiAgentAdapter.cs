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

        public void DeplyPackage(string siteID, byte[] package)
        {
            var request = new RestRequest(Constants.Deploy, Method.POST);
            request.AddUrlSegment(Constants.SiteSegment, siteID);
            request.AddFile(Constants.PackageParameter, package, "package.nupkg");

            var client = CreateClient();
            var result = client.Execute(request);
        }

        private IRestClient CreateClient()
        {
            return new RestClient(_baseUrl);
        }

        private static class Constants
        {
            public const string SiteSegment = "site";
            public const string PackageParameter = "package";

            public const string Deploy = "Deploy/{site}";
        }
    }
}