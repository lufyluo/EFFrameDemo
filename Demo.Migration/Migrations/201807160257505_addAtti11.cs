namespace Demo.Migration.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addAtti11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "GETDATE()", newValue: "aaa")
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "aaa", newValue: "GETDATE()")
                    },
                }));
        }
    }
}
