using GoProShop.BLL.DTO;
using GoProShop.ViewModels;
using PagedList;

namespace GoProShop.Helpers.Interfaces
{
    public interface IPagedListHelper
    {
        SearchResultVM<TDestionation> MapSearchResult<TSource, TDestionation>(SearchResultDTO<TSource> searchResultDTO, int? page, int? pageSize)
         where TSource : class
            where TDestionation : class;
    }
}
