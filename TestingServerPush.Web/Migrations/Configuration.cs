using TestingServerPush.Web.Models;
using System.Data.Entity.Migrations;

namespace TestingServerPush.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ServerPushDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ServerPushDbContext context)
        {
            context.Statuses.AddOrUpdate(
                p => p.Name,
                new Status { Name = "New" },
                new Status { Name = "In progress" },
                new Status { Name = "Done" }
            );
        }
    }
}
