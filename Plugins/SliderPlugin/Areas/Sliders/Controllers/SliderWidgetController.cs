using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using SliderPlugin.IService;
using SliderPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SliderPlugin.Areas.Sliders.Controllers
{
    public partial class SliderWidgetController : BaseController
    {
        private readonly ISliderService _sliderservice;
        private readonly IUnitOfWork _uow;

        public SliderWidgetController(ISliderService sliderService, IUnitOfWork uow)
        {
            _sliderservice = sliderService;
            _uow = uow;
        }

        // GET: Sliders/SliderWidget
        public virtual ActionResult SliderShow()
        {
            var model = _sliderservice.GetVisibleSlider();
            return PartialView(SlMVC.Sliders.SliderWidget.Views.SliderShow, model);
        }
    }
}