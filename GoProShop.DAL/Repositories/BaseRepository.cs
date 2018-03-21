using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

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

        public DbSet<T> Entities => _context.Set<T>();

        public virtual T Create(T entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var storedEntity = await Entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
            Entities.Remove(storedEntity);
        }

        public virtual IQueryable<T> GetAll()
            => _context.Set<T>();

        public async Task<T> GetAsync(int id)
            => await Entities.FirstOrDefaultAsync(x => x.Id == id);


        public async Task<T> UpdateAsync(T entity)
        {
            var storedEntity = await Entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _context.Entry(storedEntity).CurrentValues.SetValues(entity);
            return storedEntity;
        }
    }
}
