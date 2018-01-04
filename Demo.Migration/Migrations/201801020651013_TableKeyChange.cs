namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableKeyChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.User");
            AddColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.User", "Id");
            DropColumn("dbo.User", "Guid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Guid", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.User");
            DropColumn("dbo.User", "Id");
            AddPrimaryKey("dbo.User", "Guid");
        }
    }
}
