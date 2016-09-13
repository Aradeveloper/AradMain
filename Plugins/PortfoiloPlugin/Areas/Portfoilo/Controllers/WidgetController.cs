using AradCms.Core.Context;
using AradCms.Core.Controllers;
using PortfoiloPlugin.IService;
using PortfoiloPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfoiloPlugin.Areas.Portfoilo.Controllers
{
    public partial class WidgetController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPortfoiloService _portfoiloservice;

        public WidgetController(IUnitOfWork uow, IPortfoiloService portfoiloservice)
        {
            _uow = uow;
            _portfoiloservice = portfoiloservice;
        }

        // GET: Portfoilo/Widget
        public virtual ActionResult Portfoilo()
        {
            var model = _portfoiloservice.GetVisiblePosts();
            return PartialView(PoMVC.Portfoilo.Widget.Views.Portfoilo, model);
        }
    }
}