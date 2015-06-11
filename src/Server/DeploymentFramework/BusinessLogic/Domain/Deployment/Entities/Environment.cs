using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class Environment : EntityBase
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public Project Project { get; set; }

        public List<DeployTarget> DeployTargets { get; set; }
    }
}