using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.DataAccess;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Contracts;

namespace Baud.Deployment.BusinessLogic.Domain.Configuration
{
    public class ProjectsRepository : RepositoryBase<IBusinessContext>, IProjectsRepository
    {
        public ProjectsRepository(IBusinessContext context)
            : base(context)
        {
        }
    }
}