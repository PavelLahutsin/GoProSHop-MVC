using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id);
        IEnumerable<ProductDTO> GetAll();
        Task<ProductDTO> GetAsync(int id);
        Task UpdateAsync(ProductDTO product);
        Task CreateAsync(ProductDTO product);
        Task DeleteAsync(ProductDTO product);
        IEnumerable<ProductDTO> GetProductsOfDay();
        ProductDTO MapImage(ProductDTO product, HttpPostedFileBase uploadedFile);
    }
}
