using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
using Baud.Deployment.BusinessLogic.Domain.Security.Queries;

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
            return Context.Users.OnlyNonSystem();
        }

        public User GetUserDetail(short id)
        {
            return Context.Users
                .FilterByID(id)
                .Include(x => x.PositionLinks)
                .Include("PositionLinks.Position")
                .FirstOrDefault();
        }

        public void UpdateUser(short id, User user)
        {
            user.ID = id;

            Context.AttachAsModified(user,
                x => x.FirstName,
                x => x.LastName,
                x => x.Email,
                x => x.Note,
                x => x.ActiveFrom,
                x => x.ActiveTo);
        }

        public void AddUser(User user)
        {
            Context.Users.Add(user);
        }
    }
}