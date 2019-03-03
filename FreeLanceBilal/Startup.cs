using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreeLanceBilal.Startup))]
namespace FreeLanceBilal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
