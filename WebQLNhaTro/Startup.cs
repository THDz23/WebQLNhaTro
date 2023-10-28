using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebQLNhaTro.Startup))]
namespace WebQLNhaTro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
