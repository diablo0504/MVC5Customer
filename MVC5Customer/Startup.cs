using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Customer.Startup))]
namespace MVC5Customer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
