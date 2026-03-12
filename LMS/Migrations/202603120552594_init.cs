namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobSchedules", "LocationId", "dbo.Locations");
            DropIndex("dbo.JobSchedules", new[] { "LocationId" });
            CreateTable(
                "dbo.RestaurantWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeekNumber = c.Int(nullable: false),
                        WeekDecription = c.String(),
                        Year = c.Int(nullable: false),
                        WeekStartDate = c.DateTime(nullable: false),
                        WeekEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.JobSchedules", "LocationId", c => c.Int(nullable: true));
            CreateIndex("dbo.JobSchedules", "LocationId");
            AddForeignKey("dbo.JobSchedules", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSchedules", "LocationId", "dbo.Locations");
            DropIndex("dbo.JobSchedules", new[] { "LocationId" });
            AlterColumn("dbo.JobSchedules", "LocationId", c => c.Int());
            DropTable("dbo.RestaurantWeeks");
            CreateIndex("dbo.JobSchedules", "LocationId");
            AddForeignKey("dbo.JobSchedules", "LocationId", "dbo.Locations", "Id");
        }
    }
}
