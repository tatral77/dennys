namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _130220262 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobSchedules", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobSchedules", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
