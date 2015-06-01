using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain;
using Baud.Deployment.Database.Contracts;

namespace Baud.Deployment.Database
{
    public abstract class UowBase<TDbContext> : IUow where TDbContext : IDbContext
    {
        protected TDbContext Context { get; private set; }

        protected IRepositoryProvider<TDbContext> RepositoryProvider { get; private set; }
        protected ICurrentUserProvider CurrentUserProvider { get; private set; }
        protected IDateTimeProvider DateTimeProvider { get; private set; }

        public UowBase(IDbContextProvider<TDbContext> contextProvider, IRepositoryProvider<TDbContext> repositoryProvider, ICurrentUserProvider currentUserProvider, IDateTimeProvider dateTimeProvider)
        {
            CurrentUserProvider = currentUserProvider;
            DateTimeProvider = dateTimeProvider;

            Context = contextProvider.CreateContext();

            repositoryProvider.DbContext = Context;
            RepositoryProvider = repositoryProvider;
        }

        protected virtual T GetRepository<T>()
        {
            return RepositoryProvider.GetRepository<T>();
        }

        public virtual void Commit()
        {
            Context.SaveChanges();
        }

        public virtual Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        #endregion IDisposable
    }
}