using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Environment = Baud.Deployment.BusinessLogic.Domain.Deployment.Entities.Environment;

namespace Baud.Deployment.Database.Deployment
{
    public class DeploymentDbContext : ContextBase
    {
        public DeploymentDbContext()
            : base("name=Entities")
        {
        }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Environment> Environments { get; set; }
        public IDbSet<Application> Applications { get; set; }
        public IDbSet<DeployTarget> DeployTargets { get; set; }
        public IDbSet<Installation> Installations { get; set; }
        public IDbSet<InstallationLog> InstallationLogs { get; set; }

        public IDbSet<Server> Servers { get; set; }
        public IDbSet<ServerSite> ServerSites { get; set; }
        public IDbSet<ServerParameter> ServerParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project", "Deployment");
            modelBuilder.Entity<Project>().HasMany(x => x.Environments).WithRequired(x => x.Project);
            modelBuilder.Entity<Project>().HasMany(x => x.Applications).WithRequired(x => x.Project);

            modelBuilder.Entity<Environment>().ToTable("Environment", "Deployment")
                .HasMany(x => x.DeployTargets).WithRequired(x => x.Environment).WillCascadeOnDelete(false);

            modelBuilder.Entity<Application>().ToTable("Application", "Deployment")
                .HasMany(x => x.DeployTargets).WithRequired(x => x.Application);

            modelBuilder.Entity<DeployTarget>().ToTable("DeployTarget", "Deployment")
                .HasMany(x => x.Installations).WithRequired(x => x.DeployTarget);

            modelBuilder.Entity<Installation>().ToTable("Installation", "Deployment")
                .HasMany(x => x.InstallationLogs).WithRequired(x => x.Installation);

            modelBuilder.Entity<InstallationLog>().ToTable("InstallationLog", "Deployment");

            modelBuilder.Entity<Server>().ToTable("Server", "Deployment");
            modelBuilder.Entity<Server>().HasMany(x => x.Sites).WithRequired(x => x.Server);
            modelBuilder.Entity<Server>().HasMany(x => x.Parameters).WithRequired(x => x.Server);

            modelBuilder.Entity<ServerSite>().ToTable("ServerSite", "Deployment");
            modelBuilder.Entity<ServerSite>().HasMany(x => x.DeployTargets).WithRequired(x => x.Site);
            modelBuilder.Entity<ServerSite>().HasMany(x => x.Parameters).WithOptional(x => x.Site);

            modelBuilder.Entity<ServerParameter>().ToTable("ServerParameter", "Deployment");
        }
    }
}