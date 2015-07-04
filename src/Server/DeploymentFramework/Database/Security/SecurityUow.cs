using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.Database.Contracts;

namespace Baud.Deployment.Database.Security
{
    public class SecurityUow : UowBase<SecurityDbContext>, ISecurityUow
    {
        public IUsersRepository Users
        {
            get { return GetRepository<IUsersRepository>(); }
        }

        public IRolesRepository Roles
        {
            get { return GetRepository<IRolesRepository>(); }
        }

        public IPositionsRepository Positions
        {
            get { return GetRepository<IPositionsRepository>(); }
        }

        public SecurityUow(IDbContextProvider<SecurityDbContext> contextProvider, IRepositoryProvider<SecurityDbContext> repositoryProvider, ICurrentUserProvider currentUserProvider, IDateTimeProvider dateTimeProvider)
            : base(contextProvider, repositoryProvider, currentUserProvider, dateTimeProvider)
        {
        }
    }
}