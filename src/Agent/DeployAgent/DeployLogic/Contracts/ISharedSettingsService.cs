using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Contracts
{
    public interface ISharedSettingsService
    {
        IReadOnlyDictionary<string, string> GetSharedParameters();

        void SetSharedParameter(string key, string value);

        void RemoveSharedParameter(string key);
    }
}