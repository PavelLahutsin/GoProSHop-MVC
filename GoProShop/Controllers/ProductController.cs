using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IResponseService _responseService;

        public ProductController(
           IProductService productService,
           IResponseService responseService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }


        public async Task<ActionResult> ViewProduct(int productId, string tab = "description")
        {
            var productDTO = await _productService.GetAsync(productId);
            var productVM = Mapper.Map<ProductVM>(productDTO);

            ViewBag.Tab = tab;

            return View(productVM);
        }

        public async Task<ActionResult> Description(int productId)
        {
            var productDTO = await _productService.GetAsync(productId);
            var productVM = Mapper.Map<ProductVM>(productDTO);

            return PartialView("_Description", productVM.Description);
        }

        public ActionResult UserProductsContent(string sortCriteria = "asc", int id = 1, int page = 1, int pageSize = 12)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(_productService.GetGroupProducts(sortCriteria,id));

            var model = new UserProductsContentVM
            {
                Products = products?.ToPagedList(page, pageSize)
            };

            return PartialView("_UserProductsContent", model);
        }

        public ActionResult UserProducts(string sortCriteria = "rate", int id = 1, int page = 1, int pageSize = 12)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(_productService.GetGroupProducts(sortCriteria, id));

            return PartialView("_UserProducts", products?.ToPagedList(page, pageSize));
        }

        [Authorize(Roles = "admin")]
        public ActionResult SubGroupAdminProducts(string sortCriteria = "asc", int id = 1)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(_productService.GetGroupProducts(sortCriteria,id));

            return PartialView("_AdminProductsContent", products);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetAdminProducts(int id)
        {
            var products
                = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(await _productService.GetAdminProductsAsync(id));

            return PartialView("_AdminProducts", products);
        }

        public ActionResult ProductOfDay()
        {
            var products = _productService.GetProductsOfDay();
            var productsVm = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductVM>>(products);

            return PartialView("_UserProducts", productsVm.ToPagedList(1,8));
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

            await _productService.CreateAsync(Mapper.Map<ProductDTO>(model), uploadedFile);
            return Json(_responseService.Create(true, "Товар успешно добавлен в базу данных", Url.Action("GetAdminProducts", new { id = model.ProductSubGroupId })),         
            JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToRemove = Mapper.Map<ProductVM>(await _productService.GetAsync(id));

            await _productService.DeleteAsync(Mapper.Map<ProductDTO>(productToRemove));
            return RedirectToAction("SubGroupAdminProducts", new { id = productToRemove.ProductSubGroupId });
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

            await _productService.UpdateAsync(Mapper.Map<ProductDTO>(model), uploadedFile);

            return Json(_responseService.Create(true, "Товар успешно обновлен в базе данных", Url.Action("GetAdminProducts", "Product", new { id = model.ProductSubGroupId })), JsonRequestBehavior.AllowGet);
        }

        public async Task<FileContentResult> GetImage(int productId)
        {
            var prod = await _productService.GetAsync(productId);
            return File(prod?.Image, prod?.MimeType);
        }
    }
}