using GoProShop.DAL.Interfaces;
using System.Data.Entity;
using System.Security.Principal;
using GoProShop.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GoProShop.DAL.EF
{
    public class GoProShopContext : IdentityDbContext<ApplicationUser>, IGoProShopContext
    {
        public GoProShopContext()
            : base("GoProShopDb")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedProduct> OrderedProducts { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<StoredProduct> StoredProducts { get; set; }

        public DbSet<ProductSubGroup> ProductSubGroups { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }
    }
}
