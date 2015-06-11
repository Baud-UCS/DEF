using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class Installation : EntityBase
    {
        public int ID { get; set; }
        public int DeployTargetID { get; set; }

        [Required]
        [MaxLength(100)]
        public string PackageVersion { get; set; }

        public DateTime Planned { get; set; }
        public DateTime? Deployed { get; set; }
        public Guid? AgentDeploymentId { get; set; }

        public Enums.InstallationState State { get; set; }

        public DeployTarget DeployTarget { get; set; }

        public List<InstallationLog> InstallationLogs { get; set; }
    }
}