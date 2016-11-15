using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCexp.Startup))]
namespace MVCexp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
