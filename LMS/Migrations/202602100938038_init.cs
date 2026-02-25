namespace LMS.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeJobs", "JobTitle_Id", "dbo.JobTitles");
            DropIndex("dbo.EmployeeJobs", new[] { "JobTitle_Id" });
            DropColumn("dbo.EmployeeJobs", "JobTitleId");
            RenameColumn(table: "dbo.EmployeeJobs", name: "JobTitle_Id", newName: "JobTitleId");
            DropPrimaryKey("dbo.JobTitles");
            CreateTable(
                "dbo.EmployeeJobSchedules",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EmployeeJobId = c.String(),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    EmployeeJob_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeJobs", t => t.EmployeeJob_Id)
                .Index(t => t.EmployeeJob_Id);

            AddColumn("dbo.EmployeeJobs", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployeeJobs", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.EmployeeJobs", "ArchivedAt", c => c.DateTime());
            AlterColumn("dbo.EmployeeJobs", "JobTitleId", c => c.Int(nullable: false));
            AlterColumn("dbo.JobTitles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.JobTitles", "Id");
            CreateIndex("dbo.EmployeeJobs", "JobTitleId");
            AddForeignKey("dbo.EmployeeJobs", "JobTitleId", "dbo.JobTitles", "Id", cascadeDelete: true);
            DropColumn("dbo.EmployeeJobs", "AssignedOn");
        }

        public override void Down()
        {
            AddColumn("dbo.EmployeeJobs", "AssignedOn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.EmployeeJobs", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.EmployeeJobSchedules", "EmployeeJob_Id", "dbo.EmployeeJobs");
            DropIndex("dbo.EmployeeJobSchedules", new[] { "EmployeeJob_Id" });
            DropIndex("dbo.EmployeeJobs", new[] { "JobTitleId" });
            DropPrimaryKey("dbo.JobTitles");
            AlterColumn("dbo.JobTitles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EmployeeJobs", "JobTitleId", c => c.String(maxLength: 128));
            DropColumn("dbo.EmployeeJobs", "ArchivedAt");
            DropColumn("dbo.EmployeeJobs", "UpdatedAt");
            DropColumn("dbo.EmployeeJobs", "CreatedAt");
            DropTable("dbo.EmployeeJobSchedules");
            AddPrimaryKey("dbo.JobTitles", "Id");
            RenameColumn(table: "dbo.EmployeeJobs", name: "JobTitleId", newName: "JobTitle_Id");
            AddColumn("dbo.EmployeeJobs", "JobTitleId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeJobs", "JobTitle_Id");
            AddForeignKey("dbo.EmployeeJobs", "JobTitle_Id", "dbo.JobTitles", "Id");
        }
    }
}
