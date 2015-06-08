using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Contracts
{
    public interface IUsersRepository
    {
        IQueryable<User> GetUsers();

        User GetUserDetail(short id);

        void UpdateUser(short id, User user);
    }
}