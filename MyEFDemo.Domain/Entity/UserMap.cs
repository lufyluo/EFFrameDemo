using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using EFRepository;

namespace MyEFDemo.Domain.Entity
{
    public class UserMap: DemoEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("user");
            Property(p => p.Name).IsRequired();
            Property(p => p.CreateTime).HasColumnAnnotation("UseDbTime",true);
            //Property(p => p.CreateTime).HasColumnType(nameof(PrimitiveTypeKind.DateTime)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
