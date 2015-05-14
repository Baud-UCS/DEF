using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace Baud.Deployment.DeployLogic.NuGetHelpers
{
    public class NullPackageRepository : IPackageRepository
    {
        public PackageSaveModes PackageSaveMode
        {
            get { return PackageSaveModes.None; }
            set { throw new NotSupportedException(); }
        }
        public string Source
        {
            get { return null; }
        }

        public bool SupportsPrereleasePackages
        {
            get { return false; }
        }

        public void AddPackage(IPackage package)
        {
        }

        public IQueryable<IPackage> GetPackages()
        {
            return Enumerable.Empty<IPackage>().AsQueryable();
        }

        public void RemovePackage(IPackage package)
        {
        }
    }
}