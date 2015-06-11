using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class InstallationLog : EntityBase
    {
        public int ID { get; set; }
        public int InstallationID { get; set; }

        public DateTime Timestamp { get; set; }
        public int Level { get; set; }
        public Enums.LogSeverity Severity { get; set; }

        [Required]
        public string Text { get; set; }

        public Installation Installation { get; set; }
    }
}