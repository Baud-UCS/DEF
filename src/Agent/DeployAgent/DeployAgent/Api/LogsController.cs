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
    public class LogsController : ApiController
    {
        private readonly ISitesService _sites;

        public LogsController()
        {
            _sites = new BiggySitesService();
        }
    }
}