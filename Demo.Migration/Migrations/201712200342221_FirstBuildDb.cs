namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstBuildDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
