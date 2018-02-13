using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework;
using EntityFramework.Batch;
using EntityFramework.Extensions;
using EntityFramework.Mapping;

namespace EFRepository.Extensions
{
    public static class BatchUpdateExtensions
    {
        public static int BatchUpdate<TEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TEntity>> updateExpression)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (updateExpression == null)
                throw new ArgumentNullException("updateExpression");

            ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            ObjectContext objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the query can not be null.", "source");

            EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            updateExpression = DbTimeFilter(entityMap, updateExpression);
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the source.", "source");

            var runner = ResolveRunner();
            return runner.Update(objectContext, entityMap, sourceQuery, updateExpression);
        }

        public static int BatchUpdate<TEntity>(this IQueryable<TEntity> source)
        {

            return 0;
        }
        private static Expression<Func<TEntity, TEntity>> DbTimeFilter<TEntity>(EntityMap entityMap, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            var hasDbTimeAttribute = entityMap.EntityType.GetProperties()
                .Any(p => p.GetCustomAttributes(true).Any(n => n.GetType().Name.Equals("DbTime")));
            if (hasDbTimeAttribute)
            {
                var p = updateExpression.Parameters;
                ParameterExpression idParam = Expression.Parameter(typeof(TEntity));
                ConstantExpression dateTimeExp = Expression.Constant(DBNull.Value);
                MemberExpression idMbr = Expression.Property(idParam, entityMap.EntityType.GetProperty("CreateTime"));
                BinaryExpression idExpression = Expression.Equal(idMbr, dateTimeExp);
                Expression<Func<TEntity, TEntity>> lambda1 =
                    Expression.Lambda<Func<TEntity, TEntity>>(
                        idExpression,
                        p);
                return lambda1;
            }
            return updateExpression;
        }

        private static IBatchRunner ResolveRunner()
        {
            var provider = Locator.Current.Resolve<IBatchRunner>();
            if (provider == null)
                throw new InvalidOperationException("Could not resolve the IBatchRunner. Make sure IBatchRunner is registered in the Locator.Current container.");

            return provider;
        }
    }
}
