using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic
{
    public class DeployScriptException : Exception
    {
        public DeployScriptException(string message)
            : base("An error occured while running deploy script: " + message)
        {
        }
    }
}