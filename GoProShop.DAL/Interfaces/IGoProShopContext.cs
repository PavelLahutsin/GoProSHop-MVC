using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GoProShop.DAL.Interfaces
{
    public interface IGoProShopContext
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderedProduct> OrderedProducts { get; set; }

        DbSet<Store> Stores { get; set; }

        DbSet<StoredProduct> StoredProducts { get; set; }

        DbSet<ProductSubGroup> ProductSubGroups { get; set; }

        DbSet<ProductGroup> ProductGroups{ get; set; }

        DbSet<Feedback> Feedbacks { get; set; }

        DbChangeTracker ChangeTracker { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry Entry(object entity);

        DbSet<T> Set<T>() where T : class;
    }
}
