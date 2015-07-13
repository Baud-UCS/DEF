using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Queries;
using Baud.Deployment.Resources;

namespace Baud.Deployment.Web.Areas.Deployment.Models.Servers
{
    public class IndexFilter
    {
        [Display(Name = "Name", ResourceType = typeof(StringResources))]
        public string Name { get; set; }

        [Display(Name = "AgentURL", ResourceType = typeof(StringResources))]
        public string AgentUrl { get; set; }

        public IQueryable<Server> Apply(IQueryable<Server> source)
        {
            IQueryable<Server> query = source.OrderBy(x => x.Name);

            query = query.Filter(Name, x => x.Name.Contains(Name));
            query = query.Filter(AgentUrl, x => x.AgentUrl.Contains(AgentUrl));
            return query;
        }
    }
}