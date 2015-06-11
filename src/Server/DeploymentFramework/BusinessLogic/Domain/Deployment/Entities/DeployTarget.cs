using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    public class DeployTarget : EntityBase
    {
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public int EnvironmentID { get; set; }
        public int ServerID { get; set; }
        public int SiteID { get; set; }

        public string Note { get; set; }
        public string Url { get; set; }

        public Environment Environment { get; set; }
        public Application Application { get; set; }
        public Server Server { get; set; }

        public List<Installation> Installations { get; set; }
    }
}