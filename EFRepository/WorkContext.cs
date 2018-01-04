using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    public class WorkContext:DbContext,IWorkContext
    {
        public DbContext Context => this;
        public WorkContext():base("EFDemoContext")
        {
            Database.SetInitializer<WorkContext>(null);
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public bool LazyLoadingEnabled
        {
            get => Context.Configuration.LazyLoadingEnabled;
            set => Context.Configuration.LazyLoadingEnabled = value;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                dbModelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(dbModelBuilder);
        }

        private void RegistEntity(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
