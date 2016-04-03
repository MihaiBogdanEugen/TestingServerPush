using System.Data.Entity.Migrations;

namespace TestingServerPush.Web.Migrations
{    
    public partial class AllModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedAt = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedAt = c.DateTime(nullable: false),
                        Resolution = c.String(maxLength: 255),
                        StatusId = c.Int(nullable: false),
                        IsInProgress = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedAt = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 15),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 15),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "StatusId", "dbo.Status");
            DropForeignKey("dbo.JobPositions", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobActions", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobPositions", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "StatusId" });
            DropIndex("dbo.JobActions", new[] { "JobId" });
            DropTable("dbo.Status");
            DropTable("dbo.JobPositions");
            DropTable("dbo.Jobs");
            DropTable("dbo.JobActions");
        }
    }
}
