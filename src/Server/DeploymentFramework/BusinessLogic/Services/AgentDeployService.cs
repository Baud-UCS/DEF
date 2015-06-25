using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Enums;

namespace Baud.Deployment.BusinessLogic.Services
{
    public class AgentDeployService : IDeployService
    {
        private readonly IDeployPackagesProvider _packagesProvider;
        private readonly IAgentAdapterProvider _agentAdapterProvider;
        private readonly Func<IDeploymentUow> _uow;

        public AgentDeployService(Func<IDeploymentUow> uowFactory, IAgentAdapterProvider agentAdapterProvider, IDeployPackagesProvider packagesProvider)
        {
            _uow = uowFactory;
            _agentAdapterProvider = agentAdapterProvider;
            _packagesProvider = packagesProvider;
        }

        public async Task<InstallationState> ProcessInstallationAsync(Installation installation)
        {
            InstallationState installationResult;

            using (var uow = _uow())
            {
                uow.Installations.MarkInstallationPending(installation.ID);
                await uow.CommitAsync();

                try
                {
                    var package = _packagesProvider.GetPackageBytes(installation.PackageFilePath);

                    var agentUrl = installation.DeployTarget.Site.Server.AgentUrl;
                    var agentAdapter = _agentAdapterProvider.CreateAgentAdapter(agentUrl);

                    var result = await agentAdapter.DeployPackageAsync(installation.DeployTarget.Site.Key, package);

                    switch (result.State)
                    {
                        case Baud.Deployment.BusinessLogic.Agents.Models.DeploymentState.Success:
                            installationResult = Domain.Deployment.Enums.InstallationState.Success;
                            uow.Installations.MarkInstallationSuccessfull(installation.ID, result.ID, result.Date);
                            break;

                        case Baud.Deployment.BusinessLogic.Agents.Models.DeploymentState.Failure:
                            installationResult = Domain.Deployment.Enums.InstallationState.Failure;
                            uow.Installations.MarkInstallationFailed(installation.ID, result.ID, result.Date);
                            break;

                        default:
                            throw new InvalidOperationException("Invalid deployment state: " + result.State);
                    }

                    foreach (var log in result.Logs)
                    {
                        uow.Installations.AddInstallationLog(
                            installation.ID,
                            new InstallationLog
                            {
                                Level = log.Level,
                                Severity = ParseSeverity(log.Severity),
                                Text = log.Text,
                                Timestamp = log.Timestamp
                            });
                    }
                }
                catch (Exception ex)
                {
                    var now = DateTime.Now;

                    installationResult = Domain.Deployment.Enums.InstallationState.Failure;
                    uow.Installations.MarkInstallationFailed(installation.ID, null, now);
                    uow.Installations.AddInstallationLog(
                            installation.ID,
                            new InstallationLog
                            {
                                Level = 1,
                                Severity = LogSeverity.Error,
                                Text = ex.ToString(),
                                Timestamp = now
                            });
                }

                uow.Commit();
            }

            return installationResult;
        }

        private static LogSeverity ParseSeverity(BusinessLogic.Agents.Models.LogSeverity severity)
        {
            switch (severity)
            {
                case Baud.Deployment.BusinessLogic.Agents.Models.LogSeverity.Trace:
                    return LogSeverity.Trace;

                case Baud.Deployment.BusinessLogic.Agents.Models.LogSeverity.Debug:
                    return LogSeverity.Debug;

                case Baud.Deployment.BusinessLogic.Agents.Models.LogSeverity.Info:
                    return LogSeverity.Info;

                case Baud.Deployment.BusinessLogic.Agents.Models.LogSeverity.Warning:
                    return LogSeverity.Warning;

                case Baud.Deployment.BusinessLogic.Agents.Models.LogSeverity.Error:
                    return LogSeverity.Error;

                default:
                    throw new ArgumentOutOfRangeException("severity");
            }
        }
    }
}