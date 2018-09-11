using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCDynamicPagingDemo.Startup))]
namespace MVCDynamicPagingDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
