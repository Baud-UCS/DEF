using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;

namespace Baud.Deployment.BusinessLogic.DataAccess
{
    public abstract class UowBase<TDbContext> : IUow where TDbContext : IDbContext
    {
        protected TDbContext Context { get; private set; }

        protected IRepositoryProvider<TDbContext> RepositoryProvider { get; private set; }
        protected ICurrentUserProvider CurrentUserProvider { get; private set; }
        protected IDateTimeProvider DateTimeProvider { get; private set; }

        public UowBase(IRepositoryProvider<TDbContext> repositoryProvider, ICurrentUserProvider currentUserProvider, IDateTimeProvider dateTimeProvider)
        {
            CurrentUserProvider = currentUserProvider;
            DateTimeProvider = dateTimeProvider;

            Context = CreateDbContext();

            repositoryProvider.DbContext = Context;
            RepositoryProvider = repositoryProvider;
        }

        protected abstract TDbContext CreateDbContext();

        protected virtual T GetRepository<T>()
        {
            return RepositoryProvider.GetCustomRepository<T>();
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