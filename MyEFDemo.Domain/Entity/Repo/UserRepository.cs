using EFRepository;

namespace MyEFDemo.Domain.Entity.Repo
{
    public class UserRepository:Repository<User>, IUserRepository
    {
    }
}
