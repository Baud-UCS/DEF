using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface IDeployService
    {
        void DeployPackage(string siteID, System.IO.Stream packageStream);
    }
}