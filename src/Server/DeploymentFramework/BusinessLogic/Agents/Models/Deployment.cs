using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class Deployment
    {
        public Guid ID { get; set; }
        public string SiteID { get; set; }
        public string PackageID { get; set; }
        public DateTime Date { get; set; }

        public DeploymentState State { get; set; }

        public List<DeploymentLog> Logs { get; set; }

        public Deployment()
        {
            Logs = new List<DeploymentLog>();
        }
    }
}