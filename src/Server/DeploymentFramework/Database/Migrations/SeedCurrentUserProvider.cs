using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;

namespace Baud.Deployment.Database.Migrations
{
    public class SeedCurrentUserProvider : ICurrentUserProvider
    {
        public int GetCurrentUserID()
        {
            return -1;
        }
    }
}