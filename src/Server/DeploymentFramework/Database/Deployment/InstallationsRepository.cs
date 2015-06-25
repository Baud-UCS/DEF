using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Agents.Models;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Enums;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Queries;

namespace Baud.Deployment.Database.Deployment
{
    public class InstallationsRepository : RepositoryBase<DeploymentDbContext>, IInstallationsRepository
    {
        public InstallationsRepository(DeploymentDbContext context)
            : base(context)
        {
        }

        public IQueryable<Installation> GetWaitingInstallations()
        {
            var now = DateTime.Now;

            return Context.Installations
                .Include(x => x.DeployTarget.Site.Server)
                .OnlyWaiting()
                .OnlyPlannedBefore(now);
        }

        public void MarkInstallationPending(int installationID)
        {
            var now = DateTime.Now;

            var installation = Context.Installations
                .FilterByID(installationID)
                .First();

            installation.Started = now;
            installation.State = BusinessLogic.Domain.Deployment.Enums.InstallationState.Pending;

            Context.AttachAsModified(
                installation,
                x => x.Started,
                x => x.State);
        }

        public void MarkInstallationSuccessfull(int installationID, Guid agentDeploymentID, DateTime deployed)
        {
            var now = DateTime.Now;

            var installation = Context.Installations
                .FilterByID(installationID)
                .First();

            installation.AgentDeploymentId = agentDeploymentID;
            installation.State = InstallationState.Success;
            installation.Deployed = deployed;

            Context.AttachAsModified(
                installation,
                x => x.AgentDeploymentId,
                x => x.State,
                x => x.Deployed);
        }

        public void MarkInstallationFailed(int installationID, Guid? agentDeploymentID, DateTime deployed)
        {
            var now = DateTime.Now;

            var installation = Context.Installations
                .FilterByID(installationID)
                .First();

            installation.AgentDeploymentId = agentDeploymentID;
            installation.State = InstallationState.Failure;
            installation.Deployed = deployed;

            Context.AttachAsModified(
                installation,
                x => x.AgentDeploymentId,
                x => x.State,
                x => x.Deployed);
        }

        public void AddInstallationLog(int installationID, InstallationLog log)
        {
            log.Installation = null;
            log.InstallationID = installationID;

            Context.InstallationLogs.Add(log);
        }
    }
}