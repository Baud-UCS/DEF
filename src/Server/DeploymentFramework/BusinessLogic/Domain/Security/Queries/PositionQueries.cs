using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Queries
{
    public static class PositionQueries
    {
        public static IQueryable<Position> FilterByID(this IQueryable<Position> query, int positionID)
        {
            return query.Where(x => x.ID == positionID);
        }

        public static IQueryable<Position> OnlyActive(this IQueryable<Position> query)
        {
            return query.Where(x => x.IsActive);
        }

        public static IQueryable<Position> OnlyInactive(this IQueryable<Position> query)
        {
            return query.Where(x => !x.IsActive);
        }
    }
}
