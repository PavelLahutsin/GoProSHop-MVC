using GoProShop.Helpers.Interfaces;
using System;
using GoProShop.BLL.DTO;
using GoProShop.ViewModels;
using PagedList;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoProShop.BLL.Helpers
{
    public class PagedListHelper : IPagedListHelper
    {
        public SearchResultVM<TDestionation> MapSearchResult<TSource, TDestionation>(SearchResultDTO<TSource> searchResultDTO, int? page, int? pageSize)
            where TSource : class
            where TDestionation : class
        {
            var searchResultVM = Mapper.Map<SearchResultVM<TDestionation>>(searchResultDTO);
            searchResultVM.Count = searchResultDTO?.SearchedItems?.Count() ?? 0;

            if (!searchResultDTO.Response.IsSuccess)
                return searchResultVM;

            var searchedItems = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestionation>>(searchResultDTO?.SearchedItems);
            searchResultVM.PageNumber = page ?? 1;
            searchResultVM.PageSize = pageSize ?? 8;
            searchResultVM.PagedItems = searchedItems?.ToPagedList(searchResultVM.PageNumber, searchResultVM.PageSize);

            return searchResultVM;
        }
    }
}
