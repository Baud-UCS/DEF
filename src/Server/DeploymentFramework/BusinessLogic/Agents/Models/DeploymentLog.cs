using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Agents.Models
{
    public class DeploymentLog
    {
        public DateTime Timestamp { get; set; }
        public int Level { get; set; }
        public LogSeverity Severity { get; set; }
        public string Text { get; set; }
    }
}