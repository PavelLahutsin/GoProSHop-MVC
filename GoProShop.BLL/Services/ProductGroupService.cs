using System.Collections.Generic;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using AutoMapper;
using System.Linq;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _uow;

        public ProductGroupService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ProductGroupDTO> GetGroups()
        {
           var productGroups =  
                Mapper.Map<IQueryable<ProductGroup>, IEnumerable<ProductGroupDTO>>(_uow.ProductGroups.Entities);
            return productGroups;
        }
    }
}
