using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsitePresentation.Startup))]
namespace WebsitePresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
