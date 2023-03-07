using System.Web;
using System.Web.Mvc;

namespace NguyenHuuBang_2080600938
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
