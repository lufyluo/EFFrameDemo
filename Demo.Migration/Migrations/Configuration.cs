using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Demo.Migration.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo.Migration.MigrationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(Demo.Migration.MigrationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        //AddColumnOperation  在添加列触发
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }
        //AddColumnOperation  在添加表触发
        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedColumns(createTableOperation.Columns);
            base.Generate(createTableOperation);
        }

        private static void SetCreatedColumns(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedColumn(columnModel);
            }
        }

        private static void SetCreatedColumn(PropertyModel column)
        {
            if (column.Name == "CreateTime")
            {
                column.DefaultValueSql = "GETDATE()";

            }
        }
    }
}
