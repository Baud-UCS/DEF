using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Queries
{
    public static class UserQueries
    {
        public static IQueryable<User> FilterByID(this IQueryable<User> query, int userID)
        {
            return query.Where(x => x.ID == userID);
        }

        public static IQueryable<User> OnlyNonSystem(this IQueryable<User> query)
        {
            return query.Where(x => !x.IsSystemUser);
        }

        public static IQueryable<User> OnlyActive(this IQueryable<User> query, DateTime? date = null)
        {
            DateTime chosenDate = date ?? DateTime.Now;
            return query.Where(x => x.ActiveFrom <= chosenDate && (x.ActiveTo == null || x.ActiveTo > chosenDate));
        }

        public static IQueryable<User> OnlyInactive(this IQueryable<User> query, DateTime? date = null)
        {
            DateTime chosenDate = date ?? DateTime.Now;
            return query.Where(x => x.ActiveFrom > chosenDate || x.ActiveTo < chosenDate);
        }
    }
}