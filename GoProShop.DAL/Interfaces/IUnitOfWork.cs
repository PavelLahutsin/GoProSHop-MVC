using System;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Identity;
using Microsoft.Owin.Security;

namespace GoProShop.DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Customer> Customers { get; }

        IBaseRepository<Order> Orders { get; }

        IBaseRepository<Product> Products { get; }

        IBaseRepository<ProductGroup> ProductGroups { get; }

        IBaseRepository<OrderedProduct> ProductsOrdered { get; }

        IBaseRepository<StoredProduct> ProductsStored { get; }

        IBaseRepository<Store> Stores { get; }

        ApplicationUserManager UserManager { get; }

        ApplicationRoleManager RoleManager { get; }

        IAuthenticationManager AuthenticationManager { get; }

        Task Commit();

        void Rollback();
    }
}
