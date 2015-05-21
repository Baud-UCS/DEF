using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Contracts;

namespace Baud.Deployment.BusinessLogic.DataAccess
{
    public class BusinessUow : UowBase<IBusinessContext>, IBusinessUow
    {
        public IProjectsRepository Projects
        {
            get 
            {
                return GetRepository<IProjectsRepository>();
            }
        }

        public BusinessUow(IDbContextProvider<IBusinessContext> contextProvider, IRepositoryProvider<IBusinessContext> repositoryProvider, ICurrentUserProvider currentUserProvider, IDateTimeProvider dateTimeProvider)
            : base(contextProvider, repositoryProvider, currentUserProvider, dateTimeProvider)
        {
        }
    }
}