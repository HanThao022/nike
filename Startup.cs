using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nike.Startup))]
namespace Nike
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
