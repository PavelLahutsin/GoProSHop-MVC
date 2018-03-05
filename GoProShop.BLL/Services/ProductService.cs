using System;
using System.Collections.Generic;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Linq;
using GoProShop.BLL.DTO;
using AutoMapper;
using GoProShop.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace GoProShop.BLL.Services
{
    public class ProductService : BaseService, IProductService
    {
        private const string Desc = "desc";
        private const string Asc = "asc";

        public ProductService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task CreateAsync(ProductDTO product, HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null)
            {
                MapProductImage(product, uploadedFile);
            }

            _uow.Repository<Product>().Create(Mapper.Map<Product>(product));
            await _uow.Commit();
        }

        public IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id)
        {
            var productsDTO = GetSortedSubGroupProducts(sortCriteria, id);

            return productsDTO;
        }

        private IEnumerable<ProductDTO> GetSortedSubGroupProducts(string sortCriteria, int id)
        {
            var products = _uow.Repository<Product>().Entities.Where(x => x.ProductSubGroupId == id);

            switch (sortCriteria)
            {
                case Desc:
                    products = products.OrderByDescending(x => x.Price);
                    break;

                case Asc:
                    products = products.OrderBy(x => x.Price);
                    break;

                default:
                    products = products.OrderByDescending(x => x.Feedbacks.Average(y => y.Rating));
                    break;
            }

            var productsDto =
                 Mapper.Map<List<Product>, IEnumerable<ProductDTO>>(products.ToList());

            return productsDto;
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = Mapper.Map<ProductDTO>(await _uow.Repository<Product>().GetAsync(id));
            return product;
        }

        public async Task UpdateAsync(ProductDTO product, HttpPostedFileBase uploadedFile)
        {
            if(uploadedFile != null)
            {
                MapProductImage(product, uploadedFile);
            }

            await _uow.Repository<Product>().UpdateAsync(Mapper.Map<Product>(product));
            await _uow.Commit();
        }

        public IEnumerable<ProductDTO> GetProductsOfDay()
        {
            var products = _uow.Repository<Product>().Entities.Where(x => x.ProductSubGroup.Name.Contains("Экшн-камеры")).Take(4).ToList();
            products.AddRange(_uow.Repository<Product>().Entities.Where(x => x.ProductSubGroup.Name.Contains("Готовые комплекты")).Take(4).ToList());

            var productsDto = Mapper.Map<List<Product>, IEnumerable<ProductDTO>>(products);
            return productsDto;
        }

        public async Task DeleteAsync(ProductDTO product)
        {
            await _uow.Repository<Product>().DeleteAsync(Mapper.Map<Product>(product));
            await _uow.Commit();
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _uow.Repository<Product>().Entities.ToList();

            return Mapper.Map<List<Product>, IEnumerable<ProductDTO>>(products);

        }

        public async Task<IEnumerable<ProductDTO>> GetAdminProductsAsync(int id)
        {
            var products = await _uow.Repository<Product>().Entities.Where(x => x.ProductSubGroupId == id).ToListAsync();

            return Mapper.Map<List<Product>, IEnumerable<ProductDTO>>(products);
        }

        private void MapProductImage(ProductDTO product, HttpPostedFileBase uploadedFile)
        {
            var count = uploadedFile.ContentLength;
            product.Image = new byte[count];
            uploadedFile.InputStream.Read(product.Image, 0, count);
            product.MimeType = uploadedFile.ContentType;
        }
    }
}
