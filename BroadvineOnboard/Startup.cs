using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BroadvineOnboard.Startup))]
namespace BroadvineOnboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
