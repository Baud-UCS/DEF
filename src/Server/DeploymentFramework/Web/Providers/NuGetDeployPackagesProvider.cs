using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Models;
using NuGet;

namespace Baud.Deployment.Web.Providers
{
    public class NuGetDeployPackagesProvider : IDeployPackagesProvider
    {
        private readonly string _repositoryFullPath;

        public NuGetDeployPackagesProvider(string repositoryFullPath)
        {
            _repositoryFullPath = repositoryFullPath;
        }

        public IQueryable<KeyValuePair<string, string>> GetPackages()
        {
            var repository = GetRepository();

            var query = from p in repository.GetPackages()
                        select new KeyValuePair<string, string>(p.Id, p.Title);

            return query.Distinct();
        }

        public IQueryable<DeployPackageInfo> GetAvailablePackagesInfo()
        {
            var repository = GetRepository();

            var query = from p in repository.GetPackages()
                        group p by p.Id into g
                        let package = g.First()
                        select new DeployPackageInfo
                        {
                            Id = g.Key,
                            Title = package.Title,
                            Description = package.Description,
                            Versions = g.OrderByDescending(x => x.Version).Select(x => x.Version.ToString()).ToList()
                        };

            return query;
        }

        public IEnumerable<string> GetPackageVersions(string packageId)
        {
            var repository = GetRepository();

            var query = from p in repository.FindPackagesById(packageId)
                        orderby p.Version descending
                        select p.Version.ToString();

            return query;
        }

        public string GetPackageFileFullPath(string packageId, string version)
        {
            var packageVersion = new SemanticVersion(version);

            var pathResolver = new DefaultPackagePathResolver(_repositoryFullPath);
            var expectedFileName = pathResolver.GetPackageFileName(packageId, packageVersion);

            return Directory.EnumerateFiles(_repositoryFullPath, expectedFileName, SearchOption.AllDirectories).FirstOrDefault();
        }

        public byte[] GetPackageBytes(string packageFullPath)
        {
            return File.ReadAllBytes(packageFullPath);
        }

        //// TODO get package file name

        //// TODO list available updates

        private IPackageRepository GetRepository()
        {
            return new NuGet.LocalPackageRepository(_repositoryFullPath);
        }
    }
}