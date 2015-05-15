using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class Site
    {
        public string ID { get; set; }

        public IList<Package> InstalledPackages { get; set; }

        public IDictionary<string, string> Parameters { get; set; }

        public Site()
        {
            InstalledPackages = new List<Package>();
            Parameters = new Dictionary<string, string>();
        }
    }
}