using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.Database.Security
{
    public class UsersRepository : RepositoryBase<SecurityDbContext>, IUsersRepository
    {
        public UsersRepository(SecurityDbContext context)
            : base(context)
        {
        }

        public IQueryable<User> GetUsers()
        {
            return Context.Users;
        }
    }
}