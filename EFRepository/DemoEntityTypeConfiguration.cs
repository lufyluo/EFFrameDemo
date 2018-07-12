using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EFRepository
{
    public abstract class DemoEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        protected DemoEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {
            HasKey(n => n.Id);
            Property(n => n.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
