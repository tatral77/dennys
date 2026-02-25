namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dennys3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "LibraryAssetId", "dbo.LibraryAssets");
            DropForeignKey("dbo.LibraryAssets", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.LibraryAssets", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.LibraryAssets", "DistributorId", "dbo.Publishers");
            DropForeignKey("dbo.LibraryAssets", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.LibraryAssets", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.LibraryAssets", "MaterialTypeId", "dbo.MaterialTypes");
            DropForeignKey("dbo.LibraryAssets", "PublisherId", "dbo.Publishers");
            DropIndex("dbo.Authors", new[] { "LibraryAssetId" });
            DropIndex("dbo.LibraryAssets", new[] { "PublisherId" });
            DropIndex("dbo.LibraryAssets", new[] { "DistributorId" });
            DropIndex("dbo.LibraryAssets", new[] { "MaterialTypeId" });
            DropIndex("dbo.LibraryAssets", new[] { "LanguageId" });
            DropIndex("dbo.LibraryAssets", new[] { "CategoryId" });
            DropIndex("dbo.LibraryAssets", new[] { "LocationId" });
            DropIndex("dbo.LibraryAssets", new[] { "DepartmentId" });
            AddColumn("dbo.Employees", "LocationId", c => c.Int(nullable: true));
            AlterColumn("dbo.Locations", "Name", c => c.String(maxLength: 300));
            CreateIndex("dbo.Employees", "LocationId");
            AddForeignKey("dbo.Employees", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            DropTable("dbo.Authors");
            DropTable("dbo.LibraryAssets");
            DropTable("dbo.Categories");
            DropTable("dbo.Departments");
            DropTable("dbo.Publishers");
            DropTable("dbo.Languages");
            DropTable("dbo.MaterialTypes");
            DropTable("dbo.Currencies");
            DropTable("dbo.Designations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
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
                        IsActive = c.Boolean(nullable: false),
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
                        IsActive = c.Boolean(nullable: false),
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
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
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
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LibraryAssets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessionNo = c.String(),
                        ClassificationNo = c.String(maxLength: 25),
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
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PrimaryAuthor = c.Boolean(nullable: false),
                        LibraryAssetId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ArchivedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropIndex("dbo.Employees", new[] { "LocationId" });
            AlterColumn("dbo.Locations", "Name", c => c.String(maxLength: 100));
            DropColumn("dbo.Employees", "LocationId");
            CreateIndex("dbo.LibraryAssets", "DepartmentId");
            CreateIndex("dbo.LibraryAssets", "LocationId");
            CreateIndex("dbo.LibraryAssets", "CategoryId");
            CreateIndex("dbo.LibraryAssets", "LanguageId");
            CreateIndex("dbo.LibraryAssets", "MaterialTypeId");
            CreateIndex("dbo.LibraryAssets", "DistributorId");
            CreateIndex("dbo.LibraryAssets", "PublisherId");
            CreateIndex("dbo.Authors", "LibraryAssetId");
            AddForeignKey("dbo.LibraryAssets", "PublisherId", "dbo.Publishers", "Id");
            AddForeignKey("dbo.LibraryAssets", "MaterialTypeId", "dbo.MaterialTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LibraryAssets", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LibraryAssets", "LanguageId", "dbo.Languages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LibraryAssets", "DistributorId", "dbo.Publishers", "Id");
            AddForeignKey("dbo.LibraryAssets", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LibraryAssets", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Authors", "LibraryAssetId", "dbo.LibraryAssets", "Id", cascadeDelete: true);
        }
    }
}
