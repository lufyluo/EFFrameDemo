namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "CreateTime");
        }
    }
}
