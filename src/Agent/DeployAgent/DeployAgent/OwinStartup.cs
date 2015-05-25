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
            Configuration(appBuilder, null);
        }

        internal void Configuration(IAppBuilder appBuilder, Action<HttpConfiguration> additionalConfiguration)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            if (additionalConfiguration != null)
            {
                additionalConfiguration(config);
            }

            appBuilder.UseWebApi(config);
        }
    }
}