using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;

namespace Baud.Deployment.DeployAgent.Configuration
{
    public class MockConfigurationProvider : IConfigurationProvider
    {
        public string ApiUrl
        {
            get { return "http://localhost:9000/"; }
        }

        public string PackagesRootPath
        {
            get { return @"C:\Temp\DEF\Agent"; }
        }

        public string PowershellPath
        {
            get { return "powershell.exe"; }
        }
    }
}