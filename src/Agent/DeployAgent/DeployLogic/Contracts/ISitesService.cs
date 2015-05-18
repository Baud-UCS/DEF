using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface ISitesService
    {
        Models.Deployment CreateDeployment(string siteID, Models.PackageInfo packageInfo, Guid deploymentID);

        IReadOnlyDictionary<string, string> GetSiteParameters(string siteID);

        void LogDeploymentProgress(string siteID, string packageID, Guid deploymentID, Models.DeploymentState state, int logLevel, Models.LogSeverity severity, string text);
    }
}