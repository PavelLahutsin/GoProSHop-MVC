using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.Helpers.Interfaces;
using GoProShop.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPagedListHelper _pagedListHelper;

        public SearchController(ISearchService searchService,
            IPagedListHelper pagedListHelper)
        {
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
            _pagedListHelper = pagedListHelper ?? throw new ArgumentNullException(nameof(pagedListHelper));
        }

        public ActionResult Index(string searchString)
        {
            return View((object)searchString);
        }

        public ActionResult SearchProducts(string searchString, int? page, int? pageSize)
        {
            var searchResult = _searchService.SearchProducts(searchString);

            return PartialView("~/Views/Product/_PagedUserProducts.cshtml",
                _pagedListHelper.MapSearchResult<ProductDTO,ProductVM>(searchResult, page,pageSize));
        }
    }
}