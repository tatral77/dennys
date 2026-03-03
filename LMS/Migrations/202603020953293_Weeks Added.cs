namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeeksAdded : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestaurantWeeks");
        }
    }
}
