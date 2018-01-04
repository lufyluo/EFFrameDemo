using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFRepository.DependencyManagement;

namespace EFRepository
{
    public interface IRepository<T>:IDependency
    {
        IWorkContext WorkContext { get; set; }
        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Delete(int id);
        T Get(int id);
        void Delete(T entity);
    }
}
