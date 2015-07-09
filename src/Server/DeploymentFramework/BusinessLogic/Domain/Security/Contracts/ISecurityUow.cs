using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Contracts
{
    public interface ISecurityUow : IUow
    {
        IUsersRepository Users { get; }

        IRolesRepository Roles { get; }

        IPositionsRepository Positions { get; }
    }
}