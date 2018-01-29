using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services
{
    public class ChosenService : BaseService, IChosenService
    {
        public ChosenService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<IEnumerable<ChosenItemDto>> GetProductsAsync()
        {
            var products = await _uow.Products.Entities.ToListAsync();

            var chosenItems = Mapper.Map<List<Product>, IEnumerable<ChosenItemDto>>(products);

            return chosenItems;
        }
    }
}
