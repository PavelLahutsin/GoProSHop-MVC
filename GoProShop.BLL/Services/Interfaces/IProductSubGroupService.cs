using GoProShop.BLL.DTO;
using System.Collections.Generic;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductSubGroupService
    {
        IEnumerable<ProductSubGroupDTO> GetAll();
    }
}
