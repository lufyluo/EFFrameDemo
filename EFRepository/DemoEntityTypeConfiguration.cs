using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EFRepository.Attributes;

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
            SetDefaultTime();
        }

        protected virtual void SetDefaultTime()
        {
            var definedProp = GetAllDateTimeWithCustomAttr(typeof(DbTime));
            if (definedProp.Any(n => n.Name == "CreateTime"))
            {
                Property(p => p.CreateTime).HasColumnAnnotation("UseDbTime", "GETDATE()");
            }
            if (definedProp.Any(n => n.Name == "UpdateTime"))
            {
                Property(p => p.UpdateTime).HasColumnAnnotation("UseDbTime", "GETDATE()");
            }
        }

        protected IEnumerable<PropertyInfo> GetAllDateTimeWithCustomAttr(Type attrType)
        {
            var props = typeof(T).GetProperties();
            return props.Where(n => n.IsDefined(attrType, true) && n.PropertyType == typeof(DateTime));
        }
    }
}
