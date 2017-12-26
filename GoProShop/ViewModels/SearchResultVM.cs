using PagedList;
using System.Linq;


namespace GoProShop.ViewModels
{
    public class SearchResultVM<T>
        where T : class
    {
        public IPagedList<T> PagedItems { get; set; }

        public string SearchString { get; set; }

        public ResponseVM Response { get; set; }

        public int PageSize { get; set; }
               
        public int PageNumber { get; set; }

        public int Count { get; set; }
    }
}
