using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using System.Data.Entity;

namespace GoProShop.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBaseRepository<Customer> _customerRepository;
        private IBaseRepository<Order> _orderRepository;
        private IBaseRepository<Product> _productRepository;
        private IBaseRepository<ProductSubGroup> _productSubGroupRepository;
        private IBaseRepository<OrderedProduct> _productOrderedRepository;
        private IBaseRepository<StoredProduct> _productStoredRepository;
        private IBaseRepository<Store> _storeRepository;
        private IBaseRepository<ProductGroup> _productGroupRepository;
        private IBaseRepository<Feedback> _feedbackRepository;
        private bool disposed;
        private readonly GoProShopContext _context;

        public UnitOfWork()
        {
            _context = new GoProShopContext();          
        }

        public IBaseRepository<Customer> Customers => _customerRepository ?? 
            (_customerRepository = new BaseRepository<Customer>(_context));

        public IBaseRepository<Order> Orders => _orderRepository ??
            (_orderRepository = new BaseRepository<Order>(_context));

        public IBaseRepository<Product> Products => _productRepository ?? 
            (_productRepository = new BaseRepository<Product>(_context));

        public IBaseRepository<ProductSubGroup> ProductSubGroups => _productSubGroupRepository ?? 
            (_productSubGroupRepository = new BaseRepository<ProductSubGroup>(_context));

        public IBaseRepository<OrderedProduct> OrderedProducts => _productOrderedRepository ??
            (_productOrderedRepository = new BaseRepository<OrderedProduct>(_context));

        public IBaseRepository<StoredProduct> ProductsStored => _productStoredRepository ?? 
            (_productStoredRepository = new BaseRepository<StoredProduct>(_context));

        public IBaseRepository<Store> Stores => _storeRepository ??
            (_storeRepository = new BaseRepository<Store>(_context));

        public IBaseRepository<ProductGroup> ProductGroups => _productGroupRepository ??
           (_productGroupRepository = new BaseRepository<ProductGroup>(_context));

       public IBaseRepository<Feedback> Feedbacks => _feedbackRepository ??
           (_feedbackRepository = new BaseRepository<Feedback>(_context));

        public Database Database => _context.Database;

        public async Task Commit() => await _context.SaveChangesAsync();

        public GoProShopContext Context => _context;

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
