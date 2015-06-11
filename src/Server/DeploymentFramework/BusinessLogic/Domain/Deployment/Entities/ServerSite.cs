using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class ServerSite : EntityBase
    {
        public int ID { get; set; }
        public int ServerID { get; set; }

        [Required]
        [MaxLength(40)]
        public string Key { get; set; }

        public string Description { get; set; }

        public Server Server { get; set; }

        public List<ServerParameter> Parameters { get; set; }
    }
}