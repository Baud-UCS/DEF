using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Models;

namespace Baud.Deployment.Controller
{
    public class FileSystemDeployPackagesProvider : IDeployPackagesProvider
    {
        public byte[] GetPackageBytes(string packageFullPath)
        {
            return File.ReadAllBytes(packageFullPath);
        }

        public IQueryable<DeployPackageInfo> GetAvailablePackagesInfo()
        {
            throw new NotSupportedException();
        }

        public IQueryable<KeyValuePair<string, string>> GetPackages()
        {
            throw new NotSupportedException();
        }

        public IEnumerable<string> GetPackageVersions(string packageId)
        {
            throw new NotSupportedException();
        }

        public string GetPackageFileFullPath(string packageId, string version)
        {
            throw new NotSupportedException();
        }
    }
}