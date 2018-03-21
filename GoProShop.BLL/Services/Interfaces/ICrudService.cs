using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GoProShop.BLL.DTO;
using IdProvider = GoProShop.DAL.Entities.IdProvider;
using IdProviderDto = GoProShop.BLL.DTO.IdProvider;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface ICrudService<TEntity, TModel>
        where TEntity : IdProvider
        where TModel : IdProviderDto
    {
        Task CreateAsync(TModel model);
        Task<ResponseDTO> UpdateAsync(TModel model, string itemName);
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TModel>> GetAllAsync();
        IEnumerable<TModel> GetAll(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TModel> GetAll();
        Task<TModel> GetAsync(int id);
        Task<ResponseDTO> DeleteAsync(int id, string itemName);

    }
}
