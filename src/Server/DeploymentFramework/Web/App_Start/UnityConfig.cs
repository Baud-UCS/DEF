using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.BusinessLogic.Domain;
using Baud.Deployment.BusinessLogic.Providers;
using Baud.Deployment.Database;
using Baud.Deployment.Database.Contracts;
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
            var businessLogicAssembly = typeof(IUow).Assembly;
            var databaseAssembly = typeof(ContextBase).Assembly;
            var webAssembly = typeof(MvcApplication).Assembly;

            RegisterDataAccessTypes(container, businessLogicAssembly, databaseAssembly);
            RegisterServices(container);
            RegisterProviders(container);
        }

        private static void RegisterDataAccessTypes(UnityContainer container, params Assembly[] assemblies)
        {
            container.RegisterTypes(
                AllClasses.FromAssemblies(assemblies).Where(t => !t.IsAbstract && t.Name.EndsWith("Repository")),
                WithMappings.FromMatchingInterface);

            container.RegisterTypes(
                AllClasses.FromAssemblies(assemblies).Where(t => !t.IsAbstract && t.Name.EndsWith("Uow")),
                WithMappings.FromMatchingInterface);

            container.RegisterType(typeof(IRepositoryProvider<>), typeof(UnityRepositoryProvider<>));
            container.RegisterType(typeof(IDbContextProvider<>), typeof(ReflectionDbContextProvider<>));
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