using GoProShop.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class SearchService : BaseService, ISearchService
    {
        private const string Pattern = @"^[\p{L}a-zA-Z0-9\s?]*$";

        public SearchService(IUnitOfWork uow)
         : base(uow)
        {
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

            searchResult.SearchedItems = Mapper.Map<List<Product>, IEnumerable<ProductDTO>>(products.ToList());

            return searchResult;
        }

        public IEnumerable<ProductDTO> GetProducts(string searchString)
        {
            var products = _uow.Products.Entities.Where(x => x.Name.Contains(searchString)).Take(5).ToList();
            var productsDto = Mapper.Map<List<Product>, List<ProductDTO>>(products);

            return productsDto;
        }

        private ResponseDTO ValidateSearchString(string searchString)
        {
            var regex = new Regex(Pattern);

            if (!regex.IsMatch(searchString)
                || string.IsNullOrEmpty(searchString))
                return new ResponseDTO(false, "Последовательность содержит недопустимые символы");

            return new ResponseDTO(true, string.Empty);
        }
    }
}
