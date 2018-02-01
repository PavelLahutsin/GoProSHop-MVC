using System.Collections;
using System.Collections.Generic;
using GoProShop.BLL.DTO;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface ISearchService
    {
        SearchResultDTO<ProductDTO> SearchProducts(string searchString);

        IEnumerable<ProductDTO> GetProducts(string searchString);
    }
}
