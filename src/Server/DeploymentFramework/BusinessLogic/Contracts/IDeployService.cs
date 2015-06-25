using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Enums;

namespace Baud.Deployment.BusinessLogic.Contracts
{
    public interface IDeployService
    {
        Task<InstallationState> ProcessInstallationAsync(Installation installation);
    }
}