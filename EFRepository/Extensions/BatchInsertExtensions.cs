using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace EFRepository.Extensions
{
    public static class BatchInsertExtensions
    {
        private static readonly int BATCH_INSERT_SIZE = 500;

        public static void BulkAdd<T>(this IDbSet<T> dbSet, IEnumerable<T> entities, DbContext dbContext) where T : class
        {
            var context = dbSet.GetContext();
            var conn = (SqlConnection)(((EntityConnection)context.Connection).StoreConnection);
            var ownConnection = false;

            try
            {
                var t = typeof(T);

                var objectContext = context;
                var workspace = objectContext.MetadataWorkspace;
                var mappings = GetMappings(workspace, objectContext.DefaultContainerName, typeof(T).Name);

                var tableName = GetTableName(dbSet);

                if (conn.State != ConnectionState.Open)
                {
                    ownConnection = true;
                    conn.Open();
                }

                SqlTransaction tran = dbContext.Database.CurrentTransaction?.UnderlyingTransaction as SqlTransaction;

                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran) { DestinationTableName = tableName })
                {

                    bulkCopy.BatchSize = BATCH_INSERT_SIZE;
                    var properties = t.GetProperties().Where(p => p.PropertyType.IsSimpleType() && p.GetGetMethod() != null).ToArray();

                    using (var table = new DataTable())
                    {
                        foreach (var property in properties)
                        {
                            var propertyType = property.PropertyType;

                            // Nullable properties need special treatment.
                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                propertyType = Nullable.GetUnderlyingType(propertyType);
                            }

                            // Since we cannot trust the CLR type properties to be in the same order as the table columns we use the SqlBulkCopy column mappings.
                            if (propertyType != null) table.Columns.Add(new DataColumn(property.Name, propertyType));
                            var clrPropertyName = property.Name;
                            var tableColumnName = mappings[property.Name];

                            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(clrPropertyName, tableColumnName));
                        }
                        var dbTime =
                            properties.Any(n => n.GetCustomAttributes().Any(c => c.GetType().Name.Equals("DbTime")))
                                ? GetDbTime<T>(dbContext)
                                : null;
                        // Add all our entities to our data table
                        foreach (var entity in entities)
                        {
                            var e = entity;
                            table.Rows.Add(properties.Select(property => GetPropertyValue<T>(property.GetValue(e, null), property.Name, dbContext,dbTime)).ToArray());
                        }

                        // send it to the server for bulk execution
                        bulkCopy.BulkCopyTimeout = 5 * 60;

                        bulkCopy.WriteToServer(table);
                    }
                }

            }
            finally
            {
                if (ownConnection)
                {
                    conn.Close();
                }
            }
        }

        private static string GetTableName<T>(IDbSet<T> dbSet) where T : class
        {
            var sql = dbSet.ToString();
            var regex = new Regex(@"FROM (?<table>.*) AS");
            var match = regex.Match(sql);
            return match.Groups["table"].Value;
        }


        private static object GetDbTime<T>(DbContext db) where T : class
        {
            return db.Set<T>().Select(n => SqlFunctions.GetDate()).FirstOrDefault();
        }

        private static object GetPropertyValue<T>(object o, string propertyName, DbContext db, object dbTime) where T : class
        {
            if ((propertyName == "CreateTime" || propertyName == "UpdateTime") && dbTime != null)
            {
                return dbTime;
            }
            if (o == null)
            {
                return DBNull.Value;
            }
            return o;
        }

        private static Dictionary<string, string> GetMappings(MetadataWorkspace workspace, string containerName, string entityName)
        {
            var mappings = new Dictionary<string, string>();
            var storageMapping = workspace.GetItem<GlobalItem>(containerName, DataSpace.CSSpace);
            dynamic entitySetMaps = storageMapping.GetType().InvokeMember("EntitySetMaps", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, storageMapping, null);

            foreach (var entitySetMap in entitySetMaps)
            {
                var typeMappings = GetArrayList("TypeMappings", entitySetMap);
                dynamic typeMapping = typeMappings[0];
                dynamic types = GetArrayList("Types", typeMapping);

                if (types[0].Name == entityName)
                {
                    var fragments = GetArrayList("MappingFragments", typeMapping);
                    var fragment = fragments[0];
                    var properties = GetArrayList("AllProperties", fragment);
                    foreach (var property in properties)
                    {
                        var edmProperty = GetProperty("Property", property);
                        var columnProperty = GetProperty("Column", property);
                        mappings.Add(edmProperty.Name, columnProperty.Name);
                    }
                }
            }

            return mappings;
        }

        private static ArrayList GetArrayList(string property, object instance)
        {
            var type = instance.GetType();
            var objects = (IEnumerable)type.InvokeMember(property, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, instance, null);
            var list = new ArrayList();
            foreach (var o in objects)
            {
                list.Add(o);
            }
            return list;
        }

        private static dynamic GetProperty(string property, object instance)
        {
            var type = instance.GetType();
            return type.InvokeMember(property, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, instance, null);
        }
        public static bool IsSimpleType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return type != null && (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(Guid) || type == typeof(DateTime) || type == typeof(TimeSpan) || type == typeof(decimal));
        }
    }
}
