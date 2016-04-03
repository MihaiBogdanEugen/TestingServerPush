using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingServerPush.Web.Models
{
    public class ServerPushDbContext : DbContext
    {
        public ServerPushDbContext() : base("DefaultConnection") { }

        public ServerPushDbContext(string connectionName) : base(connectionName) { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAction> JobActions { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobPosition>().Property(x => x.Longitude).HasPrecision(18, 15);
            modelBuilder.Entity<JobPosition>().Property(x => x.Latitude).HasPrecision(18, 15);
        }
    }
}
