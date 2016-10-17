using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalLibrary.Startup))]
namespace PersonalLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
