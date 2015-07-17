using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts
{
    public interface IDeploymentUow : IUow
    {
        IProjectsRepository Projects { get; }
        IInstallationsRepository Installations { get; }
        IServersRepository Servers { get; }
    }
}