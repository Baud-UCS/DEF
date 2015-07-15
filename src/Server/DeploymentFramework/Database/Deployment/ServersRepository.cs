using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Queries;

namespace Baud.Deployment.Database.Deployment
{
    public class ServersRepository : RepositoryBase<DeploymentDbContext>, IServersRepository
    {
        public ServersRepository(DeploymentDbContext context)
            : base(context)
        {
        }

        public IQueryable<Server> GetServers()
        {
            return Context.Servers;
        }

        public Server GetServerDetail(int id)
        {
            return Context.Servers
                .FilterByID(id)
                .Include(x => x.Sites)
                .Include(x => x.Parameters)
                .FirstOrDefault();
        }

        public void UpdateServer(int id, Server server)
        {
            server.ID = id;

            Context.AttachAsModified(server,
                x => x.Name,
                x => x.AgentUrl);
        }

        public Server AddServer(Server server)
        {
            return Context.Servers.Add(server);
        }
    }
}
