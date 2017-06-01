namespace Koben.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Staff");
        }
    }
}
