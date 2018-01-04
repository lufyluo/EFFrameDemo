namespace MyEFDemo.Domain.Entity
{
    public class UserMap: DemoEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            Property(p => p.Name).IsRequired();
        }
    }
}
