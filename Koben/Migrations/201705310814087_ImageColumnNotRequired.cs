namespace Koben.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageColumnNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staff", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staff", "Image", c => c.String(nullable: false));
        }
    }
}
