﻿using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ActionResult SubGroupProducts(int id = 1)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(_productService.GetGroupProducts(id));

            return User.IsInRole("admin")
                ? PartialView("_AdminProducts", products)
                : PartialView("_UserProducts", products);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create() => PartialView("_Create");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(ProductVM model, HttpPostedFileBase uploadedFile)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model);
            }

            if (uploadedFile != null)
            {
                model = Mapper.Map<ProductVM>(
                    _productService.MapImage(
                        Mapper.Map<ProductDTO>(model), uploadedFile));
            }

            await _productService.CreateAsync(Mapper.Map<ProductDTO>(model));
            return Json(new
            {
                success = true,
                groupId = model.ProductSubGroupId
            }, 
            
            JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRemove = Mapper.Map<ProductVM>(await _productService.GetAsync(id));

            if (productToRemove == null)
            {
                return HttpNotFound();
            }

            await _productService.DeleteAsync(Mapper.Map<ProductDTO>(productToRemove));
            return Json(new
            {
                success = true,
                groupId = productToRemove.ProductSubGroupId
            },
            JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var product = Mapper.Map<ProductVM>(await _productService.GetAsync(id));
            return PartialView("_Edit", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(ProductVM model, HttpPostedFileBase uploadedFile)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model);
            }

            if (uploadedFile != null)
            {
                model = Mapper.Map<ProductVM>(
                    _productService.MapImage(
                        Mapper.Map<ProductDTO>(model), uploadedFile));
            }

            await _productService.UpdateAsync(Mapper.Map<ProductDTO>(model));
            return Json(new
            {
                success = true,
                groupId = model.ProductSubGroupId
            },
            JsonRequestBehavior.AllowGet);
        }

        public async Task<FileContentResult> GetImage(int productId)
        {
            var prod = await _productService.GetAsync(productId);
            return File(prod?.Image, prod?.MimeType);
        }
    }
}