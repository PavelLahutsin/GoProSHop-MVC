using System;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Identity;
using Microsoft.Owin.Security;
using System.Data.Entity;
using GoProShop.DAL.EF;

namespace GoProShop.DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Customer> Customers { get; }

        IBaseRepository<Order> Orders { get; }

        IBaseRepository<Product> Products { get; }

        IBaseRepository<ProductSubGroup> ProductSubGroups { get; }

        IBaseRepository<OrderedProduct> OrderedProducts { get; }

        IBaseRepository<StoredProduct> ProductsStored { get; }

        IBaseRepository<Store> Stores { get; }

        IBaseRepository<ProductGroup> ProductGroups { get; }

        IBaseRepository<Feedback> Feedbacks { get; }

        GoProShopContext Context { get; }

        Database Database { get; }

        Task Commit();

        void Rollback();
    }
}
