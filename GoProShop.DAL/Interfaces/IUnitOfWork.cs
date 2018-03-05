using System;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using System.Data.Entity;
using GoProShop.DAL.EF;

namespace GoProShop.DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : IdProvider;

        GoProShopContext Context { get; }

        Database Database { get; }

        Task Commit();

        void Rollback();
    }
}
