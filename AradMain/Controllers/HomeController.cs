using System.Web.Mvc;

namespace AradMain.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}