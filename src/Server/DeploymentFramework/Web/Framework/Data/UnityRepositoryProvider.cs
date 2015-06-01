using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baud.Deployment.Database;
using Microsoft.Practices.Unity;

namespace Baud.Deployment.Web.Framework.Data
{
    public class UnityRepositoryProvider<TContext> : RepositoryProviderBase<TContext>
    {
        private readonly IUnityContainer _container;

        public UnityRepositoryProvider(IUnityContainer container)
        {
            _container = container;
        }

        protected override T CreateRepository<T>(TContext context)
        {
            var repository = _container.Resolve<T>(new ParameterOverride("context", context));
            return repository;
        }
    }
}