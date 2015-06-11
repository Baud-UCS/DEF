using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.DeployLogic.Tests
{
    [TestClass]
    public class BiggySitesServiceTests
    {
        private const string ConflictingGuid = "{00000000-0000-0000-0000-000000000000}";

        [TestMethod]
        public void GetSiteParameters_Throws_WhenSiteNotPresent()
        {
            var service = CreateCleanService();

            Action action = () => service.GetSiteParameters("UnknownSite");

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void CreateInstallation_CreatesSite_IfNotExist()
        {
            var service = CreateCleanService();
            var serviceHelper = (IBiggySitesServiceTesting)service;

            var sitesBeforeInstallation = serviceHelper.ListSites().ToList();
            Assert.AreEqual(sitesBeforeInstallation.Count, 0);

            service.CreateInstallation("TestSite", new Models.PackageInfo(), new Guid());

            var sitesAfterInstallation = serviceHelper.ListSites().ToList();
            sitesAfterInstallation.Should().ContainSingle("TestSite");
        }

        [TestMethod]
        public void CreateInstallation_Throws_IfInstallationIDAlreadyExistis_InAnotherSite()
        {
            var service = CreateCleanService();
            var serviceHelper = (IBiggySitesServiceTesting)service;

            var existingSite = new Models.Site
            {
                ID = "AnotherSite",
                InstalledPackages = new List<Models.Package>
                {
                    new Models.Package
                    {
                        ID = "AnotherPackage",
                        Version = "1.0",
                        Installations = new List<Models.Installation>
                        {
                            new Models.Installation
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateInstallation("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void CreateInstallation_Throws_IfInstallationIDAlreadyExistis_InAnotherPackage()
        {
            var service = CreateCleanService();
            var serviceHelper = (IBiggySitesServiceTesting)service;

            var existingSite = new Models.Site
            {
                ID = "TestSite",
                InstalledPackages = new List<Models.Package>
                {
                    new Models.Package
                    {
                        ID = "AnotherPackage",
                        Version = "1.0",
                        Installations = new List<Models.Installation>
                        {
                            new Models.Installation
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateInstallation("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void CreateInstallation_Throws_IfInstallationIDAlreadyExistis_InTheSamePackage()
        {
            var service = CreateCleanService();
            var serviceHelper = (IBiggySitesServiceTesting)service;

            var existingSite = new Models.Site
            {
                ID = "TestSite",
                InstalledPackages = new List<Models.Package>
                {
                    new Models.Package
                    {
                        ID = "TestPackage",
                        Version = "1.0",
                        Installations = new List<Models.Installation>
                        {
                            new Models.Installation
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateInstallation("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

            action.ShouldThrow<InvalidOperationException>();
        }

        public TestContext TestContext { get; set; }

        private BiggySitesService CreateCleanService()
        {
            var rootFolder = TestContext.DeploymentDirectory;
            var subFolder = "SitesDatabase";
            var fullPath = System.IO.Path.Combine(rootFolder, subFolder);

            if (System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.Delete(fullPath, true);
            }

            return new BiggySitesService(TestContext.DeploymentDirectory, subFolder);
        }
    }
}