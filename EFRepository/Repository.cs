using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFRepository.Extensions;
using EntityFramework;
using EntityFramework.Batch;
using EntityFramework.Extensions;
using EntityFramework.Mapping;
using EntityFramework.Utilities;

namespace EFRepository
{
    public class Repository<T>:IRepository<T> where T:class 
    {
        public IWorkContext WorkContext { get; set; }

        private IDbSet<T> _objectset;
        private IDbSet<T> ObjectSet
        {
            get
            {
                if (_objectset == null)
                {
                    _objectset = WorkContext.Context.Set<T>();
                }
                return _objectset;
            }
        }

        public virtual IQueryable<T> All()
        {
            return ObjectSet.AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.Where(expression);
        }

        public void Add(T entity)
        {
            ObjectSet.Add(entity);
        }

        public void Add(IList<T> entities)
        {
            EFBatchOperation.For(WorkContext.Context, ObjectSet).InsertAll(entities);
            //ObjectSet.BulkAdd(entities, WorkContext.Context);
        }

        public void Update(Expression<Func<T, bool>> condition, Expression<Func<T, T>> expression)
        {
            EFBatchOperation.For(WorkContext.Context, ObjectSet).Where(condition).Update(n=>n,expression);
            ObjectSet.Where(condition).BatchUpdate(expression);
        }

        public void Update(T entities)
        {
            
        }
        public T Get(int id)
        {
            var idProp = ObjectSet.ElementType.GetProperty(string.Format("{0}Id", ObjectSet.ElementType.Name)) ??
                         ObjectSet.ElementType.GetProperty("Id");
            if (idProp != null)
            {
                ParameterExpression idParam = Expression.Parameter(typeof(T));
                ConstantExpression idValue = Expression.Constant(id, typeof(int));
                MemberExpression idMbr = Expression.Property(idParam, idProp);
                BinaryExpression idExpression = Expression.Equal(idMbr, idValue);

                Expression<Func<T, bool>> lambda1 =
                    Expression.Lambda<Func<T, bool>>(
                        idExpression,
                        new ParameterExpression[] { idParam });

                var entity = ObjectSet.FirstOrDefault(lambda1);

                return entity;
            }

            return null;
        }

        public void Delete(int id)
        {
            var idProp = ObjectSet.ElementType.GetProperty(string.Format("{0}Id", ObjectSet.ElementType.Name)) ??
                         ObjectSet.ElementType.GetProperty("Id");
            if (idProp != null)
            {
                ParameterExpression idParam = Expression.Parameter(typeof(T));
                ConstantExpression idValue = Expression.Constant(id, typeof(int));
                MemberExpression idMbr = Expression.Property(idParam, idProp);
                BinaryExpression idExpression = Expression.Equal(idMbr, idValue);

                Expression<Func<T, bool>> lambda1 =
                    Expression.Lambda<Func<T, bool>>(
                        idExpression,
                        new ParameterExpression[] { idParam });

                var entity = ObjectSet.FirstOrDefault(lambda1);

                if (entity != null)
                {
                    ObjectSet.Remove(entity);
                }
            }
        }

        public void Delete(T entity)
        {
            ObjectSet.Remove(entity);
        }

        private T[] GetItems(Expression<Func<T, T>> exp)
        {
            return ObjectSet.Where(AddGlobalFilters(exp).Compile()).ToArray();
        }

        private Expression<Func<T, bool>> AddGlobalFilters(Expression<Func<T, T>> exp)
        {
            ParameterExpression idParam = Expression.Parameter(typeof(T));
            ConstantExpression idValue = Expression.Constant(25, typeof(int));
            MemberExpression idMbr = Expression.Property(idParam, typeof(T).GetProperty("Age"));
            BinaryExpression idExpression = Expression.Equal(idMbr, idValue);
            Expression<Func<T, bool>> lambda1 =
                Expression.Lambda<Func<T, bool>>(
                    idExpression,
                    new ParameterExpression[] { idParam });
            return lambda1;

            // get the global filter
            //Expression<Func<T, bool>> newExp = c => c.TStatusId != (int)TStatus.Finished;

            //// get the visitor
            //var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            //// replace the parameter in the expression just created
            //newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            //// now you can and together the two expressions
            //var binExp = Expression.And(exp.Body, newExp.Body);
            //// and return a new lambda, that will do what you want. NOTE that the binExp has reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            //return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
        }


        /// <summary>
        /// updates the parameter in the expression
        /// </summary>
        class ParameterUpdateVisitor : ExpressionVisitor
        {
            private ParameterExpression _oldParameter;
            private ParameterExpression _newParameter;

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (object.ReferenceEquals(node, _oldParameter))
                    return _newParameter;

                return base.VisitParameter(node);
            }
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
