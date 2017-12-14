using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetGroupProducts(int id);
        Task<ProductDTO> GetAsync(int id);
        Task UpdateAsync(ProductDTO product);
        Task CreateAsync(ProductDTO product);
        Task DeleteAsync(ProductDTO product);
        IEnumerable<ProductDTO> GetProductsOfDay();
        ProductDTO MapImage(ProductDTO product, HttpPostedFileBase uploadedFile);
    }
}
