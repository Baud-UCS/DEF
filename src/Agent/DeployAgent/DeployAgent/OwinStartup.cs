using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace Baud.Deployment.DeployAgent
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{site}/{id}",
                defaults: new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(config);
        }
    }
}