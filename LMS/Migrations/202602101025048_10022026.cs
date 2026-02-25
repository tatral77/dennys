namespace LMS.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class _10022026 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeJobSchedules", "WeekDay", c => c.Int(nullable: false));
            AddColumn("dbo.EmployeeJobSchedules", "IsActive", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.EmployeeJobSchedules", "IsActive");
            DropColumn("dbo.EmployeeJobSchedules", "WeekDay");
        }
    }
}
