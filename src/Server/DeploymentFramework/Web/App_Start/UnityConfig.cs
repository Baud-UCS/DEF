using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.DataAccess;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.BusinessLogic.Providers;
using Baud.Deployment.Web.Framework.Data;
using Baud.Deployment.Web.Framework.Security;
using Microsoft.Practices.Unity;

namespace Baud.Deployment.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterTypes(container);

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static void RegisterTypes(UnityContainer container)
        {
            var businessLogicAssembly = typeof(BusinessUow).Assembly;
            var webAssembly = typeof(MvcApplication).Assembly;

            RegisterUowTypes(container, businessLogicAssembly);
            RegisterServices(container);
            RegisterProviders(container);
        }

        private static void RegisterUowTypes(UnityContainer container, params Assembly[] assemblies)
        {
            container.RegisterTypes(
                AllClasses.FromAssemblies(assemblies).Where(t => t.Name.EndsWith("Repository")),
                WithMappings.FromMatchingInterface);

            container.RegisterType<IDbContextProvider<IBusinessContext>, EntityFrameworkContextProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IRepositoryProvider<IBusinessContext>, UnityRepositoryProvider>();
            container.RegisterType<IBusinessUow, BusinessUow>();
        }

        private static void RegisterServices(UnityContainer container)
        {
        }

        private static void RegisterProviders(UnityContainer container)
        {
            container.RegisterInstance<IDateTimeProvider>(new RealDateTimeProvider());
            container.RegisterType<ICurrentUserProvider, CurrentUserProvider>();
        }
    }
}