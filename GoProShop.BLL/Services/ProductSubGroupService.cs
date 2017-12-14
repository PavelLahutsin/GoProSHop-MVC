using System.Collections.Generic;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using AutoMapper;
using System.Linq;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductSubGroupService : IProductSubGroupService
    {
        private readonly IUnitOfWork _uow;

        public ProductSubGroupService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ProductSubGroupDTO> GetProductSubGroups()
        {
           var productSubGroups = _uow.ProductSubGroups.Entities.ToList();
           var productSubGroupsDto =  
                Mapper.Map<List<ProductSubGroup>, IEnumerable<ProductSubGroupDTO>>(productSubGroups);

           return productSubGroupsDto;
        }
    }
}
