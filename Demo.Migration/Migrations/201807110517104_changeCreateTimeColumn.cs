namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCreateTimeColumn : DbMigration
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
