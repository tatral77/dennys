namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeJobSchedules", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployeeJobSchedules", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeJobSchedules", "EndDate");
            DropColumn("dbo.EmployeeJobSchedules", "StartDate");
        }
    }
}
