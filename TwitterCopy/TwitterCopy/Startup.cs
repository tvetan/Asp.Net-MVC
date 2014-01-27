using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterCopy.Startup))]
namespace TwitterCopy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}