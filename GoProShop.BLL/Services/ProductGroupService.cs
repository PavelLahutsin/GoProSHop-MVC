using System.Collections.Generic;
using System.Threading.Tasks;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using System;
using System.Data.Entity;
using AutoMapper;
using System.Linq;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services
{
    public class ProductGroupService : BaseService, IProductGroupService
    {
        public ProductGroupService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public IEnumerable<ProductGroupDTO> GetProductGroups()
        {
            var productGroups =
                 Mapper.Map<List<ProductGroup>, List<ProductGroupDTO>>(_uow.ProductGroups.Entities.ToList());

            return productGroups;
        }
    }
}
