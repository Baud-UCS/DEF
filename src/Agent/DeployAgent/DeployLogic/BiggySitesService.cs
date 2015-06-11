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
    // because of possible concurrency, the service should be used as singleton
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

        public void SetSiteParameter(string siteID, string key, string value)
        {
            var site = GetOrCreateSite(siteID);
            site.Parameters[key] = value;

            _sites.Update(site);
        }

        public void RemoveSiteParameter(string siteID, string key)
        {
            var site = GetOrCreateSite(siteID);
            site.Parameters.Remove(key);

            _sites.Update(site);
        }

        public Models.Installation CreateInstallation(string siteID, Models.PackageInfo packageInfo, Guid installationID)
        {
            CheckUniqueInstallationID(installationID);
            var site = GetOrCreateSite(siteID);
            var package = GetOrCreatePackage(site, packageInfo);

            var installation = new Models.Installation
            {
                ID = installationID,
                SiteID = site.ID,
                PackageID = package.ID,
                Date = DateTime.Now,
                State = Models.InstallationState.Pending
            };
            package.Installations.Add(installation);
            _sites.Update(site);

            return installation;
        }

        public void LogInstallationProgress(string siteID, string packageID, Guid installationID, Models.InstallationState state, int logLevel, Models.LogSeverity severity, string text)
        {
            var site = _sites.First(x => x.ID == siteID);
            var package = site.InstalledPackages.First(x => x.ID == packageID);
            var installation = package.Installations.First(x => x.ID == installationID);

            installation.State = state;

            var log = new Models.InstallationLog
            {
                Level = logLevel,
                Timestamp = DateTime.Now,
                Severity = severity,
                Text = text
            };
            installation.Logs.Add(log);

            _sites.Update(site);
        }

        private void CheckUniqueInstallationID(Guid installationID)
        {
            var existinginstallations = from s in _sites
                                        from p in s.InstalledPackages
                                        from d in p.Installations
                                        select d.ID;
            if (existinginstallations.Contains(installationID))
            {
                throw new InvalidOperationException(string.Format("There is already an installation with ID '{0}'", installationID));
            }
        }

        private Models.Site GetOrCreateSite(string siteID)
        {
            var site = _sites.FirstOrDefault(x => x.ID == siteID);

            if (site == null)
            {
                // TODO lock
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
                // TODO lock
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