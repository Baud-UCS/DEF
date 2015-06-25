using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.Database.Contracts;

namespace Baud.Deployment.Database
{
    public abstract class NoChangeTrackingDbContextProvider<TDbContext> : IDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        public TDbContext CreateContext()
        {
            var context = CreateContextInner();
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

#if DEBUG
            context.Database.Log = log => System.Diagnostics.Debug.WriteLine(log);
#endif

            return context;
        }

        protected abstract TDbContext CreateContextInner();
    }
}