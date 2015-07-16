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

        ServerParameter GetParameterByID(int id);

        void UpdateServer(int id, Server server);

        void AddServer(Server server);

        void UpdateParameters(ServerParameter parameter);
    }
}
