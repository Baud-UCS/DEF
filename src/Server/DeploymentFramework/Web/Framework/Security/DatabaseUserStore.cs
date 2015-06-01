using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baud.Deployment.Web.Models;
using Microsoft.AspNet.Identity;

namespace Baud.Deployment.Web.Framework.Security
{
    public class DatabaseUserStore : IUserStore<ApplicationUser>
    {
        public System.Threading.Tasks.Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ApplicationUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ApplicationUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}