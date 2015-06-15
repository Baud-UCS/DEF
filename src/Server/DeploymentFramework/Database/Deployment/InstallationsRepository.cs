using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Queries;

namespace Baud.Deployment.Database.Deployment
{
    public class InstallationsRepository : RepositoryBase<DeploymentDbContext>, IInstallationsRepository
    {
        public InstallationsRepository(DeploymentDbContext context)
            : base(context)
        {
        }

        public IQueryable<Installation> GetWaitingInstallations()
        {
            var now = DateTime.Now;

            return Context.Installations
                .Include(x => x.DeployTarget.Application)
                .Include(x => x.DeployTarget.Server)
                .OnlyWaiting()
                .OnlyPlannedBefore(now);
        }
    }
}