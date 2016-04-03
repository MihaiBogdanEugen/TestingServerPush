using Microsoft.Owin;
using Owin;
using TestingServerPush.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace TestingServerPush.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
