using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;

namespace Baud.Deployment.DeployLogic
{
    public class PowershellScriptService : Contracts.IScriptService
    {
        private readonly string _powershell;

        public PowershellScriptService(IConfigurationProvider configuration)
        {
            _powershell = configuration.PowershellPath;
        }

        public int Run(string scriptFilePath, string parameter, out string standardOutput, out string standardError)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = _powershell;
            startInfo.Arguments = string.Format(@"& '{0}' '{1}'", scriptFilePath, parameter);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;

            process.Start();

            standardOutput = process.StandardOutput.ReadToEnd();
            standardError = process.StandardError.ReadToEnd();

            return process.ExitCode;
        }
    }
}