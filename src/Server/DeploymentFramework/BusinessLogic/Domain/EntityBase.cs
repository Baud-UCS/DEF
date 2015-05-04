using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain
{
    public abstract class EntityBase
    {
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public int? ModifiedBy { get; set; }
    }
}