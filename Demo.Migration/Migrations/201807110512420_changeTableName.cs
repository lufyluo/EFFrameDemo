namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTableName : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "udt.user", newSchema: "dbo");
        }
        
        public override void Down()
        {
            MoveTable(name: "dbo.user", newSchema: "udt");
        }
    }
}
