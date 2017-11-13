using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Linq;

namespace GoProShop.DAL.Interfaces
{
    public interface IBaseRepository<T> 
        where T : IdProvider
    {
        DbSet<T> Entities { get; }

        T Create(T entity);

        T Update(T entity);

        void Delete(T entity);

        T GetById(int id);

        IQueryable<T> GetAll();
    }
}
