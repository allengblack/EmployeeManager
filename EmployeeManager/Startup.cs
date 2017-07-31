using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeManager.Startup))]
namespace EmployeeManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
