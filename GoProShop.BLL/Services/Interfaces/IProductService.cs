using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService : ICrudService<Product, ProductDTO>
    {
        IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id);
        Task UpdateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        Task CreateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        IEnumerable<ProductDTO> GetProductsOfDay();
    }
}
