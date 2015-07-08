using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Queries;
using Baud.Deployment.Resources;

namespace Baud.Deployment.Web.Areas.Security.Models.Roles
{
    public class IndexFilter
    {
        [Display(Name = "Name",
            ResourceType = typeof(StringResources))]
        public string Name { get; set; }

        [Display(Name = "IsActive",
            ResourceType = typeof(StringResources))]
        public bool? IsActive { get; set; }

        public IQueryable<Role> Apply(IQueryable<Role> source)
        {
            IQueryable<Role> query = source.OrderBy(x => x.Name);

            query = query.Filter(Name, x => x.Name.Contains(Name));

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