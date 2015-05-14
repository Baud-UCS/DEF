using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface IConfigurationProvider
    {
        string ApiUrl { get; }

        string PackagesRootPath { get; }

        string PowershellPath { get; }
    }
}