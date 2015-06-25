using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Models
{
    public class DeployPackageInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public IEnumerable<string> Versions { get; set; }
    }
}