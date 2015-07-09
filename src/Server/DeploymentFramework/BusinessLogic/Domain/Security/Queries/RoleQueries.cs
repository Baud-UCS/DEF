using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Queries
{
    public static class RoleQueries
    {
        public static IQueryable<Role> FilterByID(this IQueryable<Role> query, int roleID)
        {
            return query.Where(x => x.ID == roleID);
        }

        public static IQueryable<Role> OnlyActive(this IQueryable<Role> query)
        {
            return query.Where(x => x.IsActive);
        }

        public static IQueryable<Role> OnlyInactive(this IQueryable<Role> query)
        {
            return query.Where(x => !x.IsActive);
        }
    }
}
