using System.Collections.Generic;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using AutoMapper;
using System.Linq;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductSubGroupService : BaseService, IProductSubGroupService
    {
        public ProductSubGroupService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public IEnumerable<ProductSubGroupDTO> GetProductSubGroups()
        {
           var productSubGroups = _uow.Repository<ProductSubGroup>().Entities.ToList();
           var productSubGroupsDto =  
                Mapper.Map<List<ProductSubGroup>, IEnumerable<ProductSubGroupDTO>>(productSubGroups);

           return productSubGroupsDto;
        }
    }
}
