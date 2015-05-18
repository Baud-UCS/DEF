using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface IScriptService
    {
        int Run(string workingDirectory, string scriptFilePath, string parameter, out string standardOutput, out string standardError);
    }
}