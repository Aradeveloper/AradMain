using AradCms.Core.Controllers;
using System.Web.Mvc;

namespace ControlPannel.Areas.Administration.Controllers
{
    public partial class DefaultController : BaseController
    {
        [Authorize]
        // GET: Administration/Default
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}