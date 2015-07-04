using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Contracts
{
    public interface IRolesRepository
    {
        IQueryable<Role> GetRoles();

        Role GetRoleDetail(short id);

        void UpdateRole(short id, Role role);
    }
}
