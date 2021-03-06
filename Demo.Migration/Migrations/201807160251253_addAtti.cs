namespace Demo.Migration.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addAtti : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: null, newValue: "GETDATE()")
                    },
                }));
            AlterColumn("dbo.user", "UpdateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: null, newValue: "GETDATE()")
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
                        new AnnotationValues(oldValue: "GETDATE()", newValue: null)
                    },
                }));
            AlterColumn("dbo.user", "CreateTime", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UseDbTime",
                        new AnnotationValues(oldValue: "GETDATE()", newValue: null)
                    },
                }));
        }
    }
}
