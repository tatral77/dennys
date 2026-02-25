namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12022026 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeJobSchedules", "EmployeeJob_Id", "dbo.EmployeeJobs");
            DropIndex("dbo.EmployeeJobSchedules", new[] { "EmployeeJob_Id" });
            DropColumn("dbo.EmployeeJobSchedules", "EmployeeJobId");
            RenameColumn(table: "dbo.EmployeeJobSchedules", name: "EmployeeJob_Id", newName: "EmployeeJobId");
            CreateTable(
                "dbo.JobSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EmployeeJobSchedules", "JobScheduleId", c => c.Int(nullable: false));
            AddColumn("dbo.EmployeeJobSchedules", "StartWeekDayId", c => c.Int(nullable: false));
            AddColumn("dbo.EmployeeJobSchedules", "EndWeekDayId", c => c.Int(nullable: false));
            AlterColumn("dbo.EmployeeJobSchedules", "EmployeeJobId", c => c.Int(nullable: false));
            AlterColumn("dbo.EmployeeJobSchedules", "EmployeeJobId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeJobSchedules", "JobScheduleId");
            CreateIndex("dbo.EmployeeJobSchedules", "EmployeeJobId");
            AddForeignKey("dbo.EmployeeJobSchedules", "JobScheduleId", "dbo.JobSchedules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeJobSchedules", "EmployeeJobId", "dbo.EmployeeJobs", "Id", cascadeDelete: true);
            DropColumn("dbo.EmployeeJobSchedules", "WeekDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeJobSchedules", "WeekDay", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmployeeJobSchedules", "EmployeeJobId", "dbo.EmployeeJobs");
            DropForeignKey("dbo.EmployeeJobSchedules", "JobScheduleId", "dbo.JobSchedules");
            DropIndex("dbo.EmployeeJobSchedules", new[] { "EmployeeJobId" });
            DropIndex("dbo.EmployeeJobSchedules", new[] { "JobScheduleId" });
            AlterColumn("dbo.EmployeeJobSchedules", "EmployeeJobId", c => c.Int());
            AlterColumn("dbo.EmployeeJobSchedules", "EmployeeJobId", c => c.String());
            DropColumn("dbo.EmployeeJobSchedules", "EndWeekDayId");
            DropColumn("dbo.EmployeeJobSchedules", "StartWeekDayId");
            DropColumn("dbo.EmployeeJobSchedules", "JobScheduleId");
            DropTable("dbo.JobSchedules");
            RenameColumn(table: "dbo.EmployeeJobSchedules", name: "EmployeeJobId", newName: "EmployeeJob_Id");
            AddColumn("dbo.EmployeeJobSchedules", "EmployeeJobId", c => c.String());
            CreateIndex("dbo.EmployeeJobSchedules", "EmployeeJob_Id");
            AddForeignKey("dbo.EmployeeJobSchedules", "EmployeeJob_Id", "dbo.EmployeeJobs", "Id");
        }
    }
}
