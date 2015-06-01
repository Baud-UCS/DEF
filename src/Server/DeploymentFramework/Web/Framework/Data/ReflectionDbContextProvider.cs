using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Baud.Deployment.Database;

namespace Baud.Deployment.Web.Framework.Data
{
    public class ReflectionDbContextProvider<TDbContext> : NonTrackingDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        protected override TDbContext CreateContextInner()
        {
            var context = Activator.CreateInstance<TDbContext>();
            return context;
        }
    }
}