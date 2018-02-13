using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using EFRepository;
using EFRepository.Extensions;

namespace MyEFDemo.Domain.Entity.Repo
{
    public class UserRepository:Repository<User>, IUserRepository
    {
        public void Update(Expression<Func<User, bool>> condition)
        {
            var entity = from p in Where(condition) select p;
            WorkContext.Commit();
        }
    }
}
