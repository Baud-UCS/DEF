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

        public void Enable(short id)
        {
            Role role = GetRoleDetail(id);
            role.IsActive = true;

            Context.AttachAsModified(role,
                x => x.IsActive);
        }

        public void Disable(short id)
        {
            Role role = GetRoleDetail(id);
            role.IsActive = false;

            Context.AttachAsModified(role,
                x => x.IsActive);
        }

        public void UpdateName(short id, string name)
        {
            Role role = GetRoleDetail(id);
            role.Name = name;

            Context.AttachAsModified(role,
                x => x.Name);
        }
    }
}
