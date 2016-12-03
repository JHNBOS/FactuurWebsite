using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Factuur.Startup))]
namespace Factuur
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            
        }
    }
}
