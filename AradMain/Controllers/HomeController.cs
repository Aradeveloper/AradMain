using AradCms.Core.Filters;
using System.Web.Mvc;

namespace AradMain.Controllers
{
    public partial class HomeController : Controller
    {
        [CompressFilter]
        [OutputCache(Duration = 600, VaryByParam = "*", VaryByContentEncoding = "gzip;deflate")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}