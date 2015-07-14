using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts
{
    public interface IServersRepository
    {
        IQueryable<Server> GetServers();

        Server GetServerDetail(int id);

        void UpdateServer(int id, Server server);

        Server AddServer(Server server);
    }
}
