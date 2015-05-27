using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.DeployLogic.Contracts;
using Microsoft.Owin.Builder;
using NSubstitute;

namespace Baud.Deployment.DeployAgent.Tests.Helpers
{
    public class ApiTestContext
    {
        public ISharedSettingsService SettingsService { get; private set; }
        public ISitesService SitesService { get; private set; }

        public ApiTestContext()
        {
            SettingsService = Substitute.For<ISharedSettingsService>();
            SitesService = Substitute.For<ISitesService>();
        }

        public HttpClient GetHttpClient()
        {
            var builder = new AppBuilder();
            new OwinStartup().Configuration(builder, config =>
            {
                config.DependencyResolver = new MockDependencyResolver(SettingsService, SitesService);
            });
            var app = builder.Build();
            var handler = new OwinHttpMessageHandler(app);

            return new HttpClient(handler);
        }

        private class MockDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
        {
            private readonly ISharedSettingsService _settingsService;
            private readonly ISitesService _sitesService;

            public MockDependencyResolver(ISharedSettingsService settingsService, ISitesService sitesService)
            {
                _settingsService = settingsService;
                _sitesService = sitesService;
            }

            public System.Web.Http.Dependencies.IDependencyScope BeginScope()
            {
                return new MockDependencyResolver(_settingsService, _sitesService);
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(Baud.Deployment.DeployAgent.Api.SharedParametersController))
                {
                    return new Baud.Deployment.DeployAgent.Api.SharedParametersController(_settingsService);
                }
                if (serviceType == typeof(Baud.Deployment.DeployAgent.Api.SiteParametersController))
                {
                    return new Baud.Deployment.DeployAgent.Api.SiteParametersController(_sitesService);
                }

                return null;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return Enumerable.Empty<object>();
            }

            public void Dispose()
            {
            }
        }
    }
}