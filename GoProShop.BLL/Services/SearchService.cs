using GoProShop.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProShop.BLL.DTO;
using GoProShop.DAL.Interfaces;
using System.Text.RegularExpressions;
using GoProShop.DAL.Entities;
using AutoMapper;
using PagedList;

namespace GoProShop.BLL.Services
{
    public class SearchService : ISearchService
    {
        private const string Pattern = @"^[\p{L}a-zA-Z0-9\s?]*$";

        private readonly IUnitOfWork _uow;
        private readonly IResponseService _responseService;

        public SearchService(IUnitOfWork uow,
            IResponseService responseService)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }

        public SearchResultDTO<ProductDTO> SearchProducts(string searchString)
        {
            var searchResult = new SearchResultDTO<ProductDTO>();
            searchResult.Response = ValidateSearchString(searchString);
            searchResult.SearchString = searchString;

            if (!searchResult.Response.IsSuccess)
                return searchResult;

            var products = _uow.Products.Entities.Where(x => x.Name.Contains(searchString)
              || x.Description.Contains(searchString));

            searchResult.SearchedItems = Mapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(products);

            return searchResult;
        }

        private ResponseDTO ValidateSearchString(string searchString)
        {
            var regex = new Regex(Pattern);

            if (!regex.IsMatch(searchString) 
                || string.IsNullOrEmpty(searchString))
                return _responseService.Create(false, "Последовательность содержит недопустимые символы");

            return _responseService.Create(true, string.Empty);
        }
    }
}
