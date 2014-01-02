using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoringImages.Startup))]
namespace StoringImages
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
