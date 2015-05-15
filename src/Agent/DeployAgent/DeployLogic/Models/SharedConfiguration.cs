using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class SharedConfiguration
    {
        public bool IsDefault { get; set; }

        public IDictionary<string, string> SharedParameters { get; set; }

        public SharedConfiguration()
        {
            SharedParameters = new Dictionary<string, string>();
        }
    }
}