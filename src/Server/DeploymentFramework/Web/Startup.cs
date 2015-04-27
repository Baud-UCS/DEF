using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Baud.Deployment.Web.Startup))]

namespace Baud.Deployment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}