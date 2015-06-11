using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface IDeployService
    {
        Models.Installation DeployPackage(string siteID, Guid installationID, System.IO.Stream packageStream);
    }
}