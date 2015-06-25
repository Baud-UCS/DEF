using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.Database;
using Baud.Deployment.Database.Deployment;

namespace Baud.Deployment.Controller.Jobs
{
    public class ProcessWaitingInstallationsJob
    {
        private readonly Func<IDeploymentUow> _uow;
        private readonly IDeployService _deployService;

        public ProcessWaitingInstallationsJob(Func<IDeploymentUow> uowFactory, IDeployService deployService)
        {
            _uow = uowFactory;
            _deployService = deployService;
        }

        public async Task<int> Start()
        {
            var processedInstallations = 0;

            using (var uow = _uow())
            {
                var installation = uow.Installations.GetWaitingInstallations().FirstOrDefault();
                while (installation != null)
                {
                    installation = uow.Installations.GetWaitingInstallations().FirstOrDefault();

                    await _deployService.ProcessInstallationAsync(installation);

                    processedInstallations++;
                }
            }

            return processedInstallations;
        }
    }
}