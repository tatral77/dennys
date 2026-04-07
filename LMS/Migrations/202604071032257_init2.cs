namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        JobTitleId = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        OverTimeRate = c.Double(nullable: false),
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
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 25),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 25),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Phone, unique: true);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 300),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocationWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleWeekId = c.Int(nullable: false),
                        Description = c.String(),
                        LocationId = c.Int(nullable: false),
                        ForcastedSale = c.Double(nullable: false),
                        Percentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.ScheduleWeeks", t => t.ScheduleWeekId, cascadeDelete: true)
                .Index(t => t.ScheduleWeekId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.EmployeeJobSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationWeekId = c.Int(nullable: false),
                        EmployeeJobId = c.Int(nullable: false),
                        StartWeekDayId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndWeekDayId = c.Int(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EndTime = c.Time(nullable: false, precision: 7),
                        IsActive = c.Boolean(nullable: false),
                        SalaryType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeJobs", t => t.EmployeeJobId, cascadeDelete: true)
                .ForeignKey("dbo.LocationWeeks", t => t.LocationWeekId, cascadeDelete: false)
                .ForeignKey("dbo.SalaryTypes", t => t.SalaryType_Id)
                .Index(t => t.LocationWeekId)
                .Index(t => t.EmployeeJobId)
                .Index(t => t.SalaryType_Id);
            
            CreateTable(
                "dbo.ScheduleWeeks",
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
            
            CreateTable(
                "dbo.JobTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SalaryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployeeJobSchedules", "SalaryType_Id", "dbo.SalaryTypes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmployeeWeeklyJobs", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.EmployeeWeeklyJobs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeJobs", "JobTitleId", "dbo.JobTitles");
            DropForeignKey("dbo.LocationWeeks", "ScheduleWeekId", "dbo.ScheduleWeeks");
            DropForeignKey("dbo.LocationWeeks", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.EmployeeJobSchedules", "LocationWeekId", "dbo.LocationWeeks");
            DropForeignKey("dbo.EmployeeJobSchedules", "EmployeeJobId", "dbo.EmployeeJobs");
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.EmployeeJobs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EmployeeWeeklyJobs", new[] { "JobTitleId" });
            DropIndex("dbo.EmployeeWeeklyJobs", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeJobSchedules", new[] { "SalaryType_Id" });
            DropIndex("dbo.EmployeeJobSchedules", new[] { "EmployeeJobId" });
            DropIndex("dbo.EmployeeJobSchedules", new[] { "LocationWeekId" });
            DropIndex("dbo.LocationWeeks", new[] { "LocationId" });
            DropIndex("dbo.LocationWeeks", new[] { "ScheduleWeekId" });
            DropIndex("dbo.Employees", new[] { "Phone" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropIndex("dbo.Employees", new[] { "LocationId" });
            DropIndex("dbo.EmployeeJobs", new[] { "JobTitleId" });
            DropIndex("dbo.EmployeeJobs", new[] { "EmployeeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SalaryTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmployeeWeeklyJobs");
            DropTable("dbo.JobTitles");
            DropTable("dbo.ScheduleWeeks");
            DropTable("dbo.EmployeeJobSchedules");
            DropTable("dbo.LocationWeeks");
            DropTable("dbo.Locations");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeJobs");
        }
    }
}
