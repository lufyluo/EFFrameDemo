using System;
using System.Data.Entity;
using EFRepository.DependencyManagement;

namespace EFRepository
{
    public interface IWorkContext:IDisposable,IDependency
    {
        DbContext Context { get;}
        void Commit();
        bool LazyLoadingEnabled { get; set; }
    }
}
