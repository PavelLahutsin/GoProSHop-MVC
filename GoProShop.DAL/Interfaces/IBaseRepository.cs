using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GoProShop.DAL.Interfaces
{
    public interface IBaseRepository<T> 
        where T : IdProvider
    {
        DbSet<T> Entities { get; }

        T Create(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> GetAsync(int id);

        IQueryable<T> GetAll();
    }
}
