using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.Database.Contracts;

namespace Baud.Deployment.Database.Deployment
{
    public class DeploymentUow : UowBase<DeploymentDbContext>, IDeploymentUow
    {
        public IProjectsRepository Projects
        {
            get { return GetRepository<IProjectsRepository>(); }
        }

        public IInstallationsRepository Installations
        {
            get { return GetRepository<IInstallationsRepository>(); }
        }

        public IServersRepository Servers
        {
            get { return GetRepository<IServersRepository>(); }
        }

        public DeploymentUow(IDbContextProvider<DeploymentDbContext> contextProvider, IRepositoryProvider<DeploymentDbContext> repositoryProvider, ICurrentUserProvider currentUserProvider, IDateTimeProvider dateTimeProvider)
            : base(contextProvider, repositoryProvider, currentUserProvider, dateTimeProvider)
        {
        }
    }
}