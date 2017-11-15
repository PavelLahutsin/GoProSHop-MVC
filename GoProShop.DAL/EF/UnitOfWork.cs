using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace GoProShop.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBaseRepository<Customer> _customerRepository;
        private IBaseRepository<Order> _orderRepository;
        private IBaseRepository<Product> _productRepository;
        private IBaseRepository<ProductGroup> _productGroupRepository;
        private IBaseRepository<OrderedProduct> _productOrderedRepository;
        private IBaseRepository<StoredProduct> _productStoredRepository;
        private IBaseRepository<Store> _storeRepository;                                                                                                                                                                                                                                              
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private bool disposed = false;
        private readonly GoProShopContext _context;
        private readonly IAuthenticationManager _authenticationManager;

        public UnitOfWork(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
            _context = new GoProShopContext();          
        }

        public IBaseRepository<Customer> Customers => _customerRepository ?? 
            (_customerRepository = new BaseRepository<Customer>(_context));

        public IBaseRepository<Order> Orders => _orderRepository ??
            (_orderRepository = new BaseRepository<Order>(_context));

        public IBaseRepository<Product> Products => _productRepository ?? 
            (_productRepository = new BaseRepository<Product>(_context));

        public IBaseRepository<ProductGroup> ProductGroups => _productGroupRepository ?? 
            (_productGroupRepository = new BaseRepository<ProductGroup>(_context));

        public IBaseRepository<OrderedProduct> ProductsOrdered => _productOrderedRepository ??
            (_productOrderedRepository = new BaseRepository<OrderedProduct>(_context));

        public IBaseRepository<StoredProduct> ProductsStored => _productStoredRepository ?? 
            (_productStoredRepository = new BaseRepository<StoredProduct>(_context));

        public IBaseRepository<Store> Stores => _storeRepository ??
            (_storeRepository = new BaseRepository<Store>(_context));

        public ApplicationUserManager UserManager => _userManager ??
            (_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context)));

        public ApplicationRoleManager RoleManager => _roleManager ?? 
            (_roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context)));

        public IAuthenticationManager AuthenticationManager =>
            _authenticationManager;

        public async Task Commit()
            => await _context.SaveChangesAsync();

        public void Rollback()
            => _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
