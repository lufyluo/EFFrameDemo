namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EFBegin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
