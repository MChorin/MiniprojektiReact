using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniprojektiReact.Startup))]
namespace MiniprojektiReact
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
