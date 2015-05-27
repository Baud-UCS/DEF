using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Baud.Deployment.DeployLogic;
using Baud.Deployment.DeployLogic.Contracts;

namespace Baud.Deployment.DeployAgent.Api
{
    [RoutePrefix("api/sites")]
    public class SitesController : ApiController
    {
        private readonly ISitesService _sites;

        // TODO inject through dependecy injection
        public SitesController()
        {
            _sites = new BiggySitesService();
        }

        public SitesController(ISitesService sites)
        {
            _sites = sites;
        }

        [Route("")]
        public IEnumerable<string> Get(string site)
        {
            // return sites list
            throw new NotImplementedException();
        }
    }
}