using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(exp2.Startup))]
namespace exp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
