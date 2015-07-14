using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.Resources;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class Server : EntityBase
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Server", ResourceType=typeof(StringResources))]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "AgentUrl", ResourceType = typeof(StringResources))]
        public string AgentUrl { get; set; }

        public List<ServerSite> Sites { get; set; }
        public List<ServerParameter> Parameters { get; set; }
    }
}