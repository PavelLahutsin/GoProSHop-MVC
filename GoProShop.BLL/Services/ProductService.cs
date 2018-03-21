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

namespace GoProShop.BLL.Services
{
    public class ProductService : CrudService<Product, ProductDTO>, IProductService
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

            await base.CreateAsync(product);
        }

        public IEnumerable<ProductDTO> GetGroupProducts(string sortCriteria, int id)
        {
            var products = GetAll(x => x.ProductSubGroupId == id);

            switch (sortCriteria)
            {
                case Desc:
                    products = products.OrderByDescending(x => x.Price);
                    break;

                case Asc:
                    products = products.OrderBy(x => x.Price);
                    break;

                default:
                    products = products.OrderByDescending(x => x.AverageRating);
                    break;
            }

            return products.ToList();
        }

        public async Task UpdateAsync(ProductDTO product, HttpPostedFileBase uploadedFile)
        {
            if(uploadedFile != null)
            {
                MapProductImage(product, uploadedFile);
            }

            await base.UpdateAsync(product, "Товар");
        }

        public IEnumerable<ProductDTO> GetProductsOfDay()
        {
            var products = GetAll(x => x.ProductSubGroup.Name.Contains("Экшн-камеры")).Take(4).ToList();
            products.AddRange(GetAll(x => x.ProductSubGroup.Name.Contains("Готовые комплекты")).Take(4).ToList());

            return products;
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
