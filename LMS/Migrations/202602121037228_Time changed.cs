namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Timechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeJobSchedules", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.EmployeeJobSchedules", "EndTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeJobSchedules", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmployeeJobSchedules", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
