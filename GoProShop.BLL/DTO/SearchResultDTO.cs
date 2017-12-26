using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace GoProShop.BLL.DTO
{
    public class SearchResultDTO<T> 
        where T : class
    {
        public IEnumerable<T> SearchedItems { get; set; }

        public string SearchString { get; set; }

        public ResponseDTO Response { get; set; }
    }
}
