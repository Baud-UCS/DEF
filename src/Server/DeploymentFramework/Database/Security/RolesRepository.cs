using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Queries;

namespace Baud.Deployment.Database.Security
{
    public class RolesRepository : RepositoryBase<SecurityDbContext>, IRolesRepository
    {
        public RolesRepository(SecurityDbContext context)
            : base(context)
        {
        }

        public IQueryable<Role> GetRoles()
        {
            return Context.Roles;
        }

        public Role GetRoleDetail(short id)
        {
            return Context.Roles.FilterByID(id).FirstOrDefault();
        }

        public void UpdateRole(short id, Role role)
        {
            role.ID = id;

            Context.AttachAsModified(role,
                x => x.Name,
                x => x.IsActive);
        }
    }
}
