using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductSubGroupService : CrudService<ProductSubGroup, ProductSubGroupDTO>, IProductSubGroupService
    {
        public ProductSubGroupService(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
