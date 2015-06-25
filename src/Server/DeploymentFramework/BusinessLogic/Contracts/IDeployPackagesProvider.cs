using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Models;

namespace Baud.Deployment.BusinessLogic.Contracts
{
    public interface IDeployPackagesProvider
    {
        IQueryable<DeployPackageInfo> GetAvailablePackagesInfo();

        IQueryable<KeyValuePair<string, string>> GetPackages();

        IEnumerable<string> GetPackageVersions(string packageId);

        string GetPackageFileFullPath(string packageId, string version);

        byte[] GetPackageBytes(string packageFullPath);
    }
}