using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Entities
{
    public class User : EntityBase
    {
        public short ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public bool IsSystemUser { get; set; }

        public List<UserPositionLink> PositionLinks { get; set; }
    }
}