using System.Collections.Generic;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductGroupService : CrudService<ProductGroup, ProductGroupDTO>, IProductGroupService
    {
        public ProductGroupService(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
