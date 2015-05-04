using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;

namespace Baud.Deployment.BusinessLogic.DataAccess
{
    public abstract class RepositoryProviderBase<TDbContext> : IRepositoryProvider<TDbContext>
    {
        public TDbContext DbContext { get; set; }

        protected IDictionary<Type, object> RepositoriesCache { get; set; }

        public RepositoryProviderBase()
        {
            RepositoriesCache = new Dictionary<Type, object>();
        }

        public T GetCustomRepository<T>()
        {
            object cachedRepository;
            if (RepositoriesCache.TryGetValue(typeof(T), out cachedRepository))
            {
                return (T)cachedRepository;
            }

            return CreateAndCacheRepository<T>();
        }

        protected abstract T CreateRepository<T>(TDbContext context);

        private T CreateAndCacheRepository<T>()
        {
            var repository = CreateRepository<T>(DbContext);
            if (repository == null)
            {
                throw new InvalidOperationException(string.Format("No repository implementation for type {0} was registered.", typeof(T)));
            }

            RepositoriesCache.Add(typeof(T), repository);
            return repository;
        }
    }
}