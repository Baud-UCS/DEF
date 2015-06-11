using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface ISitesService
    {
        Models.Installation CreateInstallation(string siteID, Models.PackageInfo packageInfo, Guid installationID);

        IReadOnlyDictionary<string, string> GetSiteParameters(string siteID);

        void SetSiteParameter(string siteID, string key, string value);

        void RemoveSiteParameter(string siteID, string key);

        void LogInstallationProgress(string siteID, string packageID, Guid installationID, Models.InstallationState state, int logLevel, Models.LogSeverity severity, string text);
    }
}