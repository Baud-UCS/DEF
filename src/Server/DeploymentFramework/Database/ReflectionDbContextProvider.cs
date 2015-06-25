using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.Database
{
    public class ReflectionDbContextProvider<TDbContext> : NoChangeTrackingDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        protected override TDbContext CreateContextInner()
        {
            var context = Activator.CreateInstance<TDbContext>();
            return context;
        }
    }
}