namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCreateTimeValueTo1992 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false));
        }
    }
}
