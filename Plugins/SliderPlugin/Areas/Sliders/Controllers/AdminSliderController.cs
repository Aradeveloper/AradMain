using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using SliderPlugin.IService;
using SliderPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SliderPlugin.Areas.Sliders.Controllers
{
    public partial class AdminSliderController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly ISliderService _sliderservice;
        private readonly IFileService _fileservice;

        public AdminSliderController(IUnitOfWork unitOfWork, ISliderService sliderservice, IFileService fileservice)
        {
            _uow = unitOfWork;
            _sliderservice = sliderservice;
            _fileservice = fileservice;
        }

        #region مشاهده لیست اسلایدرها

        [HttpGet]
        [AradAuthorize(SliderPermition.CanViewSliderList, AreaName = "Sliders", IsMenu = false)]
        [DisplayName("نمایش اسلاید")]
        public virtual ActionResult List()
        {
            var model = _sliderservice.GetSliders();
            return View(SlMVC.Sliders.AdminSlider.Views.List, model);
        }

        #endregion مشاهده لیست اسلایدرها

        #region افزودن اسلاید

        [HttpGet]
        [AradAuthorize(SliderPermition.CanCreateSlider, AreaName = "Sliders", IsMenu = false)]
        [DisplayName("افزودن اسلاید")]
        public virtual ActionResult Create()
        {
            return View(SlMVC.Sliders.AdminSlider.Views.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdate model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Slider/";

                path = _fileservice.Add(InputFile, path);

                _sliderservice.Add(new AddOrUpdate
                {
                    Body = model.Body.ToSafeHtml(),
                    Published = model.Published,

                    ImagePath = path,
                    TitleOne = model.TitleOne.ToSafeHtml(),
                    TitleThree = model.TitleThree.ToSafeHtml(),
                    TitleTwo = model.TitleTwo.ToSafeHtml()
                });
                _uow.SaveAllChanges();
            }
            else if (InputFile == null)
            {
                ToastrError("لطفا فایل را انتخاب نمایید");
                return RedirectToAction(SlMVC.Sliders.AdminSlider.Views.Create, SlMVC.Sliders.AdminSlider.Name);
            }
            return RedirectToAction(SlMVC.Sliders.AdminSlider.ActionNames.List, SlMVC.Sliders.AdminSlider.Name);
        }

        #endregion افزودن اسلاید

        #region ویرایش اسلاید

        [HttpGet]
        [AradAuthorize(SliderPermition.CanEditSlider, AreaName = "Sliders", IsMenu = false)]
        [DisplayName("ویرایش اسلاید")]
        public virtual ActionResult Edit(int id)
        {
            var model = _sliderservice.GetSlide(id);

            return View(SlMVC.Sliders.AdminSlider.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdate model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Slider/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _sliderservice.Find(model.Id).ImagePath;
            }
            var selectedmodel = new AddOrUpdate
            {
                Body = model.Body.ToSafeHtml(),
                Published = model.Published,

                ImagePath = path,
                TitleOne = model.TitleOne.ToSafeHtml(),
                TitleThree = model.TitleThree.ToSafeHtml(),
                TitleTwo = model.TitleTwo.ToSafeHtml(),
                Id = model.Id
            };
            _sliderservice.Edit(selectedmodel);
            _uow.SaveAllChanges();
            return RedirectToAction(SlMVC.Sliders.AdminSlider.ActionNames.List, SlMVC.Sliders.AdminSlider.Name);
        }

        #endregion ویرایش اسلاید

        #region حذف اسلاید

        [HttpGet]
        [AradAuthorize(SliderPermition.CanDeleteSlider, AreaName = "Sliders", IsMenu = false)]
        [DisplayName("حذف اسلاید")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _sliderservice.Find(id.Value);
            return PartialView(SlMVC.Sliders.AdminSlider.Views.Delete, model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _sliderservice.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(SlMVC.Sliders.AdminSlider.ActionNames.List, SlMVC.Sliders.AdminSlider.Name);
            }
        }

        #endregion حذف اسلاید
    }
}