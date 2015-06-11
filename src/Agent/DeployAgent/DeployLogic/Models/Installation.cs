using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class Installation
    {
        public Guid ID { get; set; }
        public string SiteID { get; set; }
        public string PackageID { get; set; }
        public DateTime Date { get; set; }

        public InstallationState State { get; set; }

        public IList<InstallationLog> Logs { get; set; }

        public Installation()
        {
            Logs = new List<InstallationLog>();
        }
    }
}