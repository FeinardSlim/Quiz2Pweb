using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quiz2Pweb.Startup))]
namespace Quiz2Pweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
