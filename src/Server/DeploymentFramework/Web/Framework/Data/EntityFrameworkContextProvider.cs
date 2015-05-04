using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.Database;

namespace Baud.Deployment.Web.Framework.Data
{
    public class EntityFrameworkContextProvider : IDbContextProvider<IBusinessContext>
    {
        public IBusinessContext CreateContext()
        {
            var context = new BusinessContext();
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

#if DEBUG
            context.Database.Log = log => System.Diagnostics.Debug.WriteLine(log);
#endif

            return context;
        }
    }
}