using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using PagedList;
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

        public async Task<ActionResult> ViewProduct(int productId)
        {
            var productDTO = await _productService.GetAsync(productId);
            var productVM = Mapper.Map<ProductVM>(productDTO);

            return View(productVM);
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

        public ActionResult UserProducts(string sortCriteria = "asc", int id = 1, int page = 1, int pageSize = 12)
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