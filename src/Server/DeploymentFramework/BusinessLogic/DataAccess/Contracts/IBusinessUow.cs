using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Contracts;

namespace Baud.Deployment.BusinessLogic.DataAccess.Contracts
{
    public interface IBusinessUow : IUow
    {
        IProjectsRepository Projects { get; }
    }
}