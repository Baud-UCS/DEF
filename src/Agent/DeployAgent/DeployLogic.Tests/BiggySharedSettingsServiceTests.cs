using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Baud.Deployment.DeployLogic.Tests
{
    [TestClass]
    public class BiggySharedSettingsServiceTests
    {
        [TestMethod]
        public void GetSharedParameters_ReturnsEmptyDictionary_WhenNoParametersHaveBeenSet()
        {
            var service = CreateCleanService();

            var result = service.GetSharedParameters();

            result.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void SetSharedParameter_SetsNewValue_WhenParameterDoesntExist()
        {
            var service = CreateCleanService();

            service.SetSharedParameter("TestKey", "TestValue");

            var result = service.GetSharedParameters();
            result.Should().NotBeNull()
                .And.HaveCount(1)
                .And.Contain(new KeyValuePair<string, string>("TestKey", "TestValue"));
        }

        [TestMethod]
        public void SetSharedParameter_UpdatesValue_WhenParameterDoesntExist()
        {
            var service = CreateCleanService();

            service.SetSharedParameter("TestKey", "TestValue");
            service.SetSharedParameter("TestKey", "NewValue");

            var result = service.GetSharedParameters();
            result.Should().NotBeNull()
                .And.HaveCount(1)
                .And.Contain(new KeyValuePair<string, string>("TestKey", "NewValue"));
        }

        [TestMethod]
        public void RemoveSharedParameter_RemovesParameter()
        {
            var service = CreateCleanService();

            service.RemoveSharedParameter("TestKey");

            var result = service.GetSharedParameters();
            result.Should().NotBeNull()
                .And.BeEmpty();
        }

        public TestContext TestContext { get; set; }

        private BiggySharedSettingsService CreateCleanService()
        {
            var rootFolder = TestContext.DeploymentDirectory;
            var subFolder = "SharedSettingsDatabase";
            var fullPath = System.IO.Path.Combine(rootFolder, subFolder);

            if (System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.Delete(fullPath, true);
            }

            return new BiggySharedSettingsService(TestContext.DeploymentDirectory, subFolder);
        }
    }
}