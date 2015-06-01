using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.DataAccess;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.Database
{
    public class BusinessContext : ContextBase, IBusinessContext
    {
        public IDbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .ToTable("Project", "Configuration");
        }
    }
}