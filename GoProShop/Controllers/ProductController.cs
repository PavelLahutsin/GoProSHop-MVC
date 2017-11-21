using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public ActionResult GroupProducts(int id)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(_productService.GetGroupProducts(id));
            return PartialView(products);
        }
    }
}