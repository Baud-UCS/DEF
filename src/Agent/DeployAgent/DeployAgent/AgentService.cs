using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployAgent.Configuration;
using Baud.Deployment.DeployLogic.Contracts;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Baud.Deployment.DeployAgent
{
    public class AgentService : ServiceControl
    {
        private readonly IConfigurationProvider _configuration;
        private IDisposable _owinHost;

        public AgentService(IConfigurationProvider configuration)
        {
            _configuration = configuration;
        }

        public bool Start(HostControl hostControl)
        {
            string baseAddress = _configuration.ApiUrl;

            _owinHost = WebApp.Start<OwinStartup>(url: baseAddress);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _owinHost.Dispose();
            return true;
        }
    }
}