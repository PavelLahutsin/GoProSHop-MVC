using GoProShop.BLL.DTO;
using System.Collections.Generic;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetGroupProducts(int id);
    }
}
