using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class ServerParameter : EntityBase
    {
        public int ID { get; set; }
        public int ServerID { get; set; }
        public int? ServerSiteID { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Value { get; set; }

        public Server Server { get; set; }
        public ServerSite Site { get; set; }
    }
}