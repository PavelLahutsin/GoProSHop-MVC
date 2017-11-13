using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Linq;
using System.Data.Entity;

namespace GoProShop.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : IdProvider
    {
        private readonly IGoProShopContext _context;

        public BaseRepository(IGoProShopContext context)
        {
            _context = context;
        }

        public DbSet<T> Entities
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public virtual T Create(T entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            var storedEntity = Entities.FirstOrDefault(x => x.Id == entity.Id);

            if (storedEntity != null)
                Entities.Remove(storedEntity);
        }

        public virtual IQueryable<T> GetAll()
            => _context.Set<T>();

        public virtual T GetById(int id)
            => Entities.FirstOrDefault(x => x.Id == id);
        

        public virtual T Update(T entity)
        {
            var storedEntity = Entities.FirstOrDefault(x => x.Id == entity.Id);

            if (storedEntity == null)
            {
                entity.Id = 0;
                return Create(entity);
            }
            _context.Entry(storedEntity).CurrentValues.SetValues(entity);
       
            return entity;
        }
    }
}
