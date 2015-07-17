using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.Resources;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class ServerParameter : EntityBase
    {
        public int ID { get; set; }
        public int ServerID { get; set; }
        public int? ServerSiteID { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Name", ResourceType = typeof(StringResources))]
        public string Name { get; set; }

        [Display(Name = "Value", ResourceType = typeof(StringResources))]
        public string Value { get; set; }

        public Server Server { get; set; }
        public ServerSite Site { get; set; }
    }
}