using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Enums;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Queries
{
    public static class InstallationQueries
    {
        public static IQueryable<Installation> OnlyWaiting(this IQueryable<Installation> query)
        {
            return query.Where(x => x.State == InstallationState.Waiting);
        }

        public static IQueryable<Installation> OnlyPlannedBefore(this IQueryable<Installation> query, DateTime date)
        {
            return query.Where(x => x.Planned <= date);
        }
    }
}