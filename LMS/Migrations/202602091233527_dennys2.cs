namespace LMS.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class dennys2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "UpdatedAt", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.Employees", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
