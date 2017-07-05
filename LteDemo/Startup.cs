using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LteDemo.Startup))]
namespace LteDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
