using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Environment = Baud.Deployment.BusinessLogic.Domain.Deployment.Entities.Environment;

namespace Baud.Deployment.Database.Migrations
{
    internal sealed class DeploymentConfiguration : DbMigrationsConfiguration<Baud.Deployment.Database.Deployment.DeploymentDbContext>
    {
        public DeploymentConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Baud.Deployment.Database.Deployment.DeploymentDbContext context)
        {
            var now = DateTime.Now;

            context.Servers.AddOrUpdate(
                new Server { ID = 1, Name = "Shared web development server", AgentUrl = "webdev.local/api", Created = now },
                new Server { ID = 2, Name = "Intranet test server", AgentUrl = "intranet.test.local/api", Created = now },
                new Server { ID = 3, Name = "Intranet live web server", AgentUrl = "web.intranet.live/api", Created = now },
                new Server { ID = 4, Name = "Intranet live db server", AgentUrl = "db.intranet.live/api", Created = now },
                new Server { ID = 5, Name = "Intranet live report server", AgentUrl = "ssrs.intranet.live/api", Created = now });

            context.ServerSites.AddOrUpdate(
                new ServerSite { ID = 1, ServerID = 1, Key = "default", Description = "Default site", Created = now },
                new ServerSite { ID = 2, ServerID = 1, Key = "Intranet", Description = "Company Intranet site", Created = now },
                new ServerSite { ID = 3, ServerID = 2, Key = "default", Description = "Default site", Created = now },
                new ServerSite { ID = 4, ServerID = 3, Key = "default", Description = "Default site", Created = now },
                new ServerSite { ID = 5, ServerID = 4, Key = "default", Description = "Default site", Created = now },
                new ServerSite { ID = 6, ServerID = 5, Key = "default", Description = "Default site", Created = now });

            context.Projects.AddOrUpdate(
                new Project { ID = 1, Name = "Company Intranet", Description = "Company process controlling software", Created = now });

            context.Environments.AddOrUpdate(
                new Environment { ID = 1, ProjectID = 1, Name = "Dev", Description = "Developer nightly builds", Priority = 0, Created = now },
                new Environment { ID = 2, ProjectID = 1, Name = "Test", Description = "Testing", Priority = 10, Created = now },
                new Environment { ID = 3, ProjectID = 1, Name = "Live", Description = "Live servers", Priority = 20, Created = now });

            context.Applications.AddOrUpdate(
                new Application { ID = 1, ProjectID = 1, PackageId = "Baud.DEF.Demo.Web", Name = "Website", Description = "Web application", Priority = 100, Created = now },
                new Application { ID = 2, ProjectID = 1, PackageId = "Baud.DEF.Demo.Database", Name = "Database", Description = "Application database", Priority = 50, Created = now },
                new Application { ID = 3, ProjectID = 1, PackageId = "Baud.DEF.Demo.Reports", Name = "Reports", Description = "SSRS reports", Priority = 30, Created = now });

            context.DeployTargets.AddOrUpdate(
                new DeployTarget { ID = 1, ApplicationID = 1, EnvironmentID = 1, SiteID = 2, Url = "http://intranet.dev", Created = now },
                new DeployTarget { ID = 2, ApplicationID = 1, EnvironmentID = 2, SiteID = 3, Url = "http://intranet.test", Created = now },
                new DeployTarget { ID = 3, ApplicationID = 1, EnvironmentID = 3, SiteID = 4, Url = "http://intranet", Created = now },
                new DeployTarget { ID = 4, ApplicationID = 2, EnvironmentID = 1, SiteID = 2, Created = now },
                new DeployTarget { ID = 5, ApplicationID = 2, EnvironmentID = 2, SiteID = 3, Created = now },
                new DeployTarget { ID = 6, ApplicationID = 2, EnvironmentID = 3, SiteID = 5, Created = now },
                new DeployTarget { ID = 7, ApplicationID = 3, EnvironmentID = 2, SiteID = 3, Created = now },
                new DeployTarget { ID = 8, ApplicationID = 3, EnvironmentID = 3, SiteID = 6, Created = now });
        }
    }
}