using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;

namespace Baud.Deployment.BusinessLogic.Providers
{
    public class SimpleCurrentUserProvider : ICurrentUserProvider
    {
        private readonly int _userID;

        public SimpleCurrentUserProvider(int userID)
        {
            _userID = userID;
        }

        public int GetCurrentUserID()
        {
            return _userID;
        }
    }
}