namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13022026 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSchedules", "ForcastedSale", c => c.Double(nullable: false));
            AddColumn("dbo.JobSchedules", "Percentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobSchedules", "Percentage");
            DropColumn("dbo.JobSchedules", "ForcastedSale");
        }
    }
}
