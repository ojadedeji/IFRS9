using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fintrak.Host.ServicePortal.Startup))]
namespace Fintrak.Host.ServicePortal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
