namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PrimaryAuthor = c.Boolean(nullable: false),
                        LibraryAssetId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LibraryAssets", t => t.LibraryAssetId, cascadeDelete: true)
                .Index(t => t.LibraryAssetId);
            
            CreateTable(
                "dbo.LibraryAssets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccNo = c.String(),
                        ClassNo = c.String(maxLength: 25),
                        Title = c.String(nullable: false, maxLength: 250),
                        SubTitle = c.String(maxLength: 250),
                        EditionNo = c.String(maxLength: 25),
                        VersionNo = c.String(maxLength: 25),
                        Volume = c.String(maxLength: 25),
                        PublisherId = c.Int(nullable: false),
                        DistributorId = c.Int(nullable: false),
                        AccompanyingMaterial = c.String(maxLength: 100),
                        IsbnNo = c.String(maxLength: 50),
                        IssnNo = c.String(maxLength: 50),
                        DdcClassificationNo = c.Single(nullable: false),
                        PublishingYear = c.Int(nullable: false),
                        PublishingDate = c.DateTime(nullable: false),
                        DateOfPurchase = c.DateTime(nullable: false),
                        Price = c.Single(nullable: false),
                        TotalPages = c.Int(nullable: false),
                        Barcode = c.String(maxLength: 50),
                        LocationPlaced = c.String(nullable: false, maxLength: 50),
                        RemarksOrDescription = c.String(maxLength: 250),
                        MaterialTypeId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        Cover = c.String(),
                        Pdf = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.DistributorId)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.MaterialTypes", t => t.MaterialTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.PublisherId)
                .Index(t => t.DistributorId)
                .Index(t => t.MaterialTypeId)
                .Index(t => t.LanguageId)
                .Index(t => t.CategoryId)
                .Index(t => t.LocationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Symbol = c.String(nullable: false, maxLength: 5),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 25),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 25),
                        IsActive = c.Boolean(nullable: false),
                        DepartmentId = c.Int(),
                        DesignationId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Phone, unique: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.DesignationId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.LibraryAssets", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.LibraryAssets", "MaterialTypeId", "dbo.MaterialTypes");
            DropForeignKey("dbo.LibraryAssets", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.LibraryAssets", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.LibraryAssets", "DistributorId", "dbo.Publishers");
            DropForeignKey("dbo.LibraryAssets", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.LibraryAssets", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Authors", "LibraryAssetId", "dbo.LibraryAssets");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "Phone" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropIndex("dbo.LibraryAssets", new[] { "DepartmentId" });
            DropIndex("dbo.LibraryAssets", new[] { "LocationId" });
            DropIndex("dbo.LibraryAssets", new[] { "CategoryId" });
            DropIndex("dbo.LibraryAssets", new[] { "LanguageId" });
            DropIndex("dbo.LibraryAssets", new[] { "MaterialTypeId" });
            DropIndex("dbo.LibraryAssets", new[] { "DistributorId" });
            DropIndex("dbo.LibraryAssets", new[] { "PublisherId" });
            DropIndex("dbo.Authors", new[] { "LibraryAssetId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.Designations");
            DropTable("dbo.Currencies");
            DropTable("dbo.MaterialTypes");
            DropTable("dbo.Locations");
            DropTable("dbo.Languages");
            DropTable("dbo.Publishers");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
            DropTable("dbo.LibraryAssets");
            DropTable("dbo.Authors");
        }
    }
}
