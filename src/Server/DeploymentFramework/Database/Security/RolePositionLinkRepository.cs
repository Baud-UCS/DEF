using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;

namespace Baud.Deployment.Database.Security
{
    public class RolePositionLinkRepository : RepositoryBase<SecurityDbContext>, IRolePositionLinkRepository
    {
        public RolePositionLinkRepository(SecurityDbContext context)
            : base(context)
        {
        }
    }
}
