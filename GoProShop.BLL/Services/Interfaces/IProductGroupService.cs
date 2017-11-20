using GoProShop.BLL.DTO;
using System.Collections.Generic;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductGroupService
    {
        IEnumerable<ProductGroupDTO> GetGroups();
    }
}
