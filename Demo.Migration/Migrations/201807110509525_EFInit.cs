namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EFInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "udt.user",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        NickName = c.String(),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("udt.user");
        }
    }
}
