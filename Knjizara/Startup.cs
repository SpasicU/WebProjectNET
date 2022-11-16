using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Knjizara.Startup))]
namespace Knjizara
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
