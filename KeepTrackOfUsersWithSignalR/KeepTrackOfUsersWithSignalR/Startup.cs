using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KeepTrackOfUsersWithSignalR.Startup))]
namespace KeepTrackOfUsersWithSignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}