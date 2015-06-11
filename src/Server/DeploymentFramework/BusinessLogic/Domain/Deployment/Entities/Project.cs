using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class Project : EntityBase
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Priority { get; set; }

        public List<Environment> Environments { get; set; }
        public List<Application> Applications { get; set; }
    }
}