using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Entities
{
    /// <summary>
    /// A helper class used in Server Parameters editor.
    /// </summary>
    public class ServerServerParameter : EntityBase
    {
        public Server Server { get; set; }
        public ServerParameter ServerParameter { get; set; }
    }
}
