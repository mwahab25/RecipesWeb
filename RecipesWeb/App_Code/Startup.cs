using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipesWeb.Startup))]
namespace RecipesWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
