using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Entities;

namespace Baud.Deployment.BusinessLogic.DataAccess.Contracts
{
    public interface IBusinessContext : IDbContext
    {
        IDbSet<Project> Projects { get; }
    }
}