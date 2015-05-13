using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployAgent.Configuration
{
    public class MockConfigurationProvider : IConfigurationProvider
    {
        public string ApiUrl
        {
            get { return "http://localhost:9000/"; }
        }
    }
}