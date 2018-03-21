using System;
using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id);
        Task<IEnumerable<ProductDTO>> GetAllAsync(Expression<Func<Product, bool>> filter);
        Task<ProductDTO> GetAsync(int id);
        Task UpdateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        Task CreateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        Task<ResponseDTO> DeleteAsync(int id, string itemName);
        IEnumerable<ProductDTO> GetProductsOfDay();
    }
}
