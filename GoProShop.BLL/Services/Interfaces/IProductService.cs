using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAdminProductsAsync(int id);
        IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id);
        IEnumerable<ProductDTO> GetAll();
        Task<ProductDTO> GetAsync(int id);
        Task UpdateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        Task CreateAsync(ProductDTO product, HttpPostedFileBase uploadedFile);
        Task DeleteAsync(ProductDTO product);
        IEnumerable<ProductDTO> GetProductsOfDay();
    }
}
