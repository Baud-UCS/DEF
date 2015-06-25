using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Contracts
{
    public interface IAgentAdapter
    {
        Task<Agents.Models.Deployment> DeployPackageAsync(string siteID, byte[] package);

        Task<IDictionary<string, string>> GetSharedParameters();

        Task<string> GetSharedParameter(string parameterName);

        Task SetSharedParameters(IDictionary<string, string> parameters);

        Task SetSharedParameter(string parameterName, string stringValue);

        Task DeleteSharedParameter(string parameterName);

        Task<IDictionary<string, string>> GetSiteParameters(string siteID);

        Task<string> GetSiteParameter(string siteID, string parameterName);

        Task SetSiteParameters(string siteID, IDictionary<string, string> parameters);

        Task SetSiteParameter(string siteID, string parameterName, string stringValue);

        Task DeleteSiteParameter(string siteID, string parameterName);
    }
}