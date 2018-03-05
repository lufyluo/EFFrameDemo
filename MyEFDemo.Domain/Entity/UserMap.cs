using System.ComponentModel.DataAnnotations.Schema;

namespace MyEFDemo.Domain.Entity
{
    public class UserMap: DemoEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            Property(p => p.Name).IsRequired();
            Property(p => p.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
