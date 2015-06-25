using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Agents;
using Baud.Deployment.BusinessLogic.Contracts;

namespace Baud.Deployment.BusinessLogic.Providers
{
    public class WebApiAgentAdapterProvider : IAgentAdapterProvider
    {
        public IAgentAdapter CreateAgentAdapter(string agentUrl)
        {
            return new WebApiAgentAdapter(agentUrl);
        }
    }
}