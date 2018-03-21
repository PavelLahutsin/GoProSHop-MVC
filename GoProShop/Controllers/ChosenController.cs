using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ChosenController : Controller
    {
        private readonly IChosenService _chosenService;

        public ChosenController(IChosenService chosenService)
        {
            _chosenService = chosenService;
        }    

        public async Task<ActionResult> GetChosenProducts()
        {
            var products = await _chosenService.GetAllAsync();

            var chosenProductsVm = Mapper.Map<IEnumerable<ChosenItemDto>, IEnumerable< ChosenItemVm>> (products);

            return Json(chosenProductsVm, JsonRequestBehavior.AllowGet);
        }
    }
}