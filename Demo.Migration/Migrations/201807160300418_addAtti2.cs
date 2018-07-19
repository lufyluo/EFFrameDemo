namespace Demo.Migration.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addAtti2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "aaa", newValue: "1991.04.15")
                    },
                }));
            AlterColumn("dbo.user", "UpdateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "GETDATE()", newValue: "1991.05.16")
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.user", "UpdateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "1991.05.16", newValue: "GETDATE()")
                    },
                }));
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "1991.04.15", newValue: "aaa")
                    },
                }));
        }
    }
}
