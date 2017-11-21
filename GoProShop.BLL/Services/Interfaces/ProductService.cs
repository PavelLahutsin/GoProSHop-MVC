using System.Collections.Generic;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Linq;
using GoProShop.BLL.DTO;
using AutoMapper;

namespace GoProShop.BLL.Services.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ProductDTO> GetGroupProducts(int id)
        {
           var products =
                Mapper.Map<IQueryable<Product>,IEnumerable<ProductDTO>>(_uow.Products.Entities.Where(x => x.ProductGroupId == id));
           return products;
        }
    }
}
