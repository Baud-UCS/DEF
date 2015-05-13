using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployAgent.Configuration
{
    public interface IConfigurationProvider
    {
        string ApiUrl { get; }
    }
}