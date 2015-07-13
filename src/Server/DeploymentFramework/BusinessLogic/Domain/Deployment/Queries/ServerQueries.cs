using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Queries
{
    public static class ServerQueries
    {
        public static IQueryable<Server> FilterByID(this IQueryable<Server> query, int serverID)
        {
            return query.Where(x => x.ID == serverID);
        }
    }
}
