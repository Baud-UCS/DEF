using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Baud.Deployment.DeployAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AgentService>();
            });
        }
    }
}