using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Entities
{
    public class Role : EntityBase
    {
        public short ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<RolePositionLink> PositionLinks { get; set; }
    }
}