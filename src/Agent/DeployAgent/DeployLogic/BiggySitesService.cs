using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;
using Biggy.Core;
using Biggy.Data.Json;
using Biggy.Extensions;

namespace Baud.Deployment.DeployLogic
{
    public class BiggySitesService : ISitesService, IBiggySitesServiceTesting
    {
        private readonly BiggyList<Models.Site> _sites;

        public BiggySitesService()
            : this(new JsonDbCore())
        {
        }

        internal BiggySitesService(string dbDirectory, string dbName)
            : this(new JsonDbCore(dbDirectory, dbName))
        {
        }

        private BiggySitesService(JsonDbCore dbCore)
        {
            var store = new JsonStore<Models.Site>(dbCore);
            _sites = new BiggyList<Models.Site>(store);
        }

        public IReadOnlyDictionary<string, string> GetSiteParameters(string siteID)
        {
            var site = _sites.FirstOrDefault(x => x.ID == siteID);

            if (site == null)
            {
                throw new ArgumentOutOfRangeException("siteID", string.Format("Site ID '{0}' does not exist.", siteID));
            }

            return new ReadOnlyDictionary<string, string>(site.Parameters);
        }

        public Models.Deployment CreateDeployment(string siteID, Models.PackageInfo packageInfo, Guid deploymentID)
        {
            CheckUniqueDeploymentID(deploymentID);
            var site = GetOrCreateSite(siteID);
            var package = GetOrCreatePackage(site, packageInfo);

            var deployment = new Models.Deployment
            {
                ID = deploymentID,
                SiteID = site.ID,
                Date = DateTime.Now,
                State = Models.DeploymentState.Pending
            };
            package.Deployments.Add(deployment);
            _sites.Update(site);

            return deployment;
        }

        private void CheckUniqueDeploymentID(Guid deploymentID)
        {
            var existingDeployments = from s in _sites
                                      from p in s.InstalledPackages
                                      from d in p.Deployments
                                      select d.ID;
            if (existingDeployments.Contains(deploymentID))
            {
                throw new InvalidOperationException(string.Format("There is already a deployment with ID '{0}'", deploymentID));
            }
        }

        private Models.Site GetOrCreateSite(string siteID)
        {
            var site = _sites.FirstOrDefault(x => x.ID == siteID);

            if (site == null)
            {
                site = new Models.Site
                {
                    ID = siteID
                };
                _sites.Add(site);
            }

            return site;
        }

        private Models.Package GetOrCreatePackage(Models.Site site, Models.PackageInfo packageInfo)
        {
            var package = site.InstalledPackages.FirstOrDefault(x => x.ID == packageInfo.ID && x.Version == packageInfo.Version);

            if (package == null)
            {
                package = new Models.Package
                {
                    ID = packageInfo.ID,
                    Version = packageInfo.Version,
                    Name = packageInfo.Name
                };
                site.InstalledPackages.Add(package);
                _sites.Update(site);
            }

            return package;
        }

        #region Test helpers

        IEnumerable<string> IBiggySitesServiceTesting.ListSites()
        {
            return _sites.Select(x => x.ID);
        }

        void IBiggySitesServiceTesting.AddSite(Models.Site site)
        {
            _sites.Add(site);
        }

        #endregion Test helpers
    }

    internal interface IBiggySitesServiceTesting
    {
        IEnumerable<string> ListSites();

        void AddSite(Models.Site site);
    }
}