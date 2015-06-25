using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts
{
    public interface IInstallationsRepository
    {
        IQueryable<Installation> GetWaitingInstallations();

        void MarkInstallationPending(int installationID);

        void MarkInstallationSuccessfull(int installationID, Guid agentDeploymentID, DateTime deployed);

        void MarkInstallationFailed(int installationID, Guid? agentDeploymentID, DateTime deployed);

        void AddInstallationLog(int installationID, InstallationLog log);
    }
}