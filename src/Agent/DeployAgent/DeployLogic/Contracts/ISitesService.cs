using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface ISitesService
    {
        IDictionary<string, string> GetSharedParameters();

        IDictionary<string, string> GetSiteParameters(string siteID);
    }
}