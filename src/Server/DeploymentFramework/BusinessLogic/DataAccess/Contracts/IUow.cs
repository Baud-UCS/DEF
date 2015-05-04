﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.DataAccess.Contracts
{
    public interface IUow : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}