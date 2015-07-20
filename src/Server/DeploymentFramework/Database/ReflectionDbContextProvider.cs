using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;

namespace Baud.Deployment.Database
{
    public class ReflectionDbContextProvider<TDbContext> : NoChangeTrackingDbContextProvider<TDbContext> where TDbContext : ContextBase
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        public ReflectionDbContextProvider(ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
        }

        protected override TDbContext CreateContextInner()
        {
            var context = Activator.CreateInstance<TDbContext>();
            context.CurrentUserProvider = _currentUserProvider;
            return context;
        }
    }
}