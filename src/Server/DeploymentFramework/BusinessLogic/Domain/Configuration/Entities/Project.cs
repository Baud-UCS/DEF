using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Configuration.Entities
{
    public class Project : EntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Priority { get; set; }
    }
}