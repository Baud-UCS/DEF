using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Baud.Deployment.DeployLogic;
using Baud.Deployment.DeployLogic.Contracts;

namespace Baud.Deployment.DeployAgent.Api
{
    public class DeployController : ApiController
    {
        private readonly IDeployService _deployService;

        public DeployController()
        {
            // TODO inject through dependecy injection
            var configuration = new Configuration.MockConfigurationProvider();
            _deployService = new NuGetDeployService(configuration, null, new PowershellScriptService(configuration));
        }

        public async Task Post(string site)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            using (var packageStream = new MemoryStream())
            {
                var streamProvider = new MemoryDataStreamProvider(packageStream);
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                _deployService.DeployPackage(site, packageStream);
            }
        }
    }
}