using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace GoProShop.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoProShopContext _context;
        private readonly Dictionary<Type, object> _repositories;

        private bool _disposed;

        public UnitOfWork()
        {
            _repositories = new Dictionary<Type, object>();
            _context = new GoProShopContext();          
        }

        public IBaseRepository<T> Repository<T>() where T : IdProvider
        {
            if (_repositories.Keys.Contains(typeof(T)) == true)
            {
                return _repositories[typeof(T)] as IBaseRepository<T>;
            }

            IBaseRepository<T> repo = new BaseRepository<T>(_context);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public Database Database => _context.Database;

        public async Task Commit() => await _context.SaveChangesAsync();

        public GoProShopContext Context => _context;

        public void Rollback() => _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
