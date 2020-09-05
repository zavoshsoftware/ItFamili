using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminPresentation.Startup))]
namespace AdminPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
