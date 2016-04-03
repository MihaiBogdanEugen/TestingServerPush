using System;
using Hangfire;
using Hangfire.SqlServer;
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
            app.MapSignalR();

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection", new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(5)
            });
            app.UseHangfireDashboard("/hangfire");
            app.UseHangfireServer();
        }
    }
}
