using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFRepository;

namespace MyEFDemo.Domain.Entity.Repo
{
    public interface IUserRepository : IRepository<MyEFDemo.Domain.Entity.User>
    {
        void Update(Expression<Func<User, bool>> condition);
    }
}
