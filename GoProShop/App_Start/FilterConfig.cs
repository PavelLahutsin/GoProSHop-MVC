using GoProShop.Attributes;
using System.Web.Mvc;

namespace GoProShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidateModelAttribute());
        }
    }
}
