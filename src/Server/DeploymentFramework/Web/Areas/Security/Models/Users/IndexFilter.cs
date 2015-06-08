using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Queries;

namespace Baud.Deployment.Web.Areas.Security.Models.Users
{
    public class IndexFilter
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public IQueryable<User> Apply(IQueryable<User> source)
        {
            IQueryable<User> query = source.OrderBy(x => x.Login);

            query = query.Filter(Name, x => x.FirstName.Contains(Name) || x.LastName.Contains(Name));

            DateTime now = DateTime.Now;
            if (IsActive == true)
            {
                query = query.OnlyActive();
            }
            else if (IsActive == false)
            {
                query = query.OnlyInactive();
            }

            return query;
        }
    }
}