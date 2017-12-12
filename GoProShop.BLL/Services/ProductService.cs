using System.Collections.Generic;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Linq;
using GoProShop.BLL.DTO;
using AutoMapper;
using GoProShop.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Web;

namespace GoProShop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateAsync(ProductDTO product)
        {
            _uow.Products.Create(Mapper.Map<Product>(product));
            await _uow.Commit();
        }

        public IEnumerable<ProductDTO> GetGroupProducts(int id)
        {
            var products = _uow.Products.Entities.Where(x => x.ProductSubGroupId == id);
            var productsDTO =
                 Mapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(products);
            return productsDTO;
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = Mapper.Map<ProductDTO>(await _uow.Products.GetAsync(id));
            return product;
        }

        public async Task UpdateAsync(ProductDTO product)
        {
            var updatedProduct = await _uow.Products.UpdateAsync(Mapper.Map<Product>(product));
            await _uow.Commit();
        }

        public ProductDTO MapImage(ProductDTO product, HttpPostedFileBase uploadedFile)
        {
            var count = uploadedFile.ContentLength;
            product.Image = new byte[count];
            uploadedFile.InputStream.Read(product.Image, 0, count);
            product.MimeType = uploadedFile.ContentType;
            return product;
        }

        public async Task DeleteAsync(ProductDTO product)
        {
            await _uow.Products.DeleteAsync(Mapper.Map<Product>(product));
            await _uow.Commit();
        }
    }
}
