namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeWeeklyJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        JobTitleId = c.Int(nullable: false),
                        WeeklySalary = c.Double(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.JobTitles", t => t.JobTitleId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.JobTitleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeWeeklyJobs", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.EmployeeWeeklyJobs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeWeeklyJobs", new[] { "JobTitleId" });
            DropIndex("dbo.EmployeeWeeklyJobs", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeWeeklyJobs");
        }
    }
}
