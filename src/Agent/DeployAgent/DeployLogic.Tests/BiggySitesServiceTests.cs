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
        public void CreateDeployment_CreatesSite_IfNotExist()
        {
            var service = CreateCleanService();
            var serviceHelper = (IBiggySitesServiceTesting)service;

            var sitesBeforeDeployment = serviceHelper.ListSites().ToList();
            Assert.AreEqual(sitesBeforeDeployment.Count, 0);

            service.CreateDeployment("TestSite", new Models.PackageInfo(), new Guid());

            var sitesAfterDeployment = serviceHelper.ListSites().ToList();
            sitesAfterDeployment.Should().ContainSingle("TestSite");
        }

        [TestMethod]
        public void CreateDeployment_Throws_IfDeploymentIDAlreadyExistis_InAnotherSite()
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
                        Deployments = new List<Models.Deployment>
                        {
                            new Models.Deployment
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateDeployment("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void CreateDeployment_Throws_IfDeploymentIDAlreadyExistis_InAnotherPackage()
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
                        Deployments = new List<Models.Deployment>
                        {
                            new Models.Deployment
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateDeployment("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void CreateDeployment_Throws_IfDeploymentIDAlreadyExistis_InTheSamePackage()
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
                        Deployments = new List<Models.Deployment>
                        {
                            new Models.Deployment
                            {
                                ID = new Guid(ConflictingGuid)
                            }
                        }
                    }
                }
            };
            serviceHelper.AddSite(existingSite);

            Action action = () => service.CreateDeployment("TestSite", new Models.PackageInfo { ID = "TestPackage", Version = "1.0" }, new Guid(ConflictingGuid));

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