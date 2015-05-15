using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class Package : PackageInfo
    {
        public IList<Deployment> Deployments { get; set; }

        public Package()
        {
            Deployments = new List<Deployment>();
        }
    }
}