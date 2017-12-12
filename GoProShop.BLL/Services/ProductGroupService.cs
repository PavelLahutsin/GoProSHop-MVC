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
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _uow;

        public ProductGroupService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public IEnumerable<ProductGroupDTO> GetProductGroups()
        {
            var productGroups =
                 Mapper.Map<List<ProductGroup>, List<ProductGroupDTO>>(_uow.ProductGroups.Entities.ToList());

            return productGroups;
        }
    }
}
