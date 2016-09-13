using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Helpers;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using FormPlugin.IService;
using FormPlugin.Models;
using FormPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FormPlugin.Areas.FormPlug.Controllers
{
    public partial class WidgetController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderService _orderservice;
        private readonly IFileService _fileservice;
        private readonly IReceiptService _reciept;

        public WidgetController(IUnitOfWork unitOfWork, IOrderService orderservice, IFileService fileservice, IReceiptService reciept)
        {
            _uow = unitOfWork;
            _orderservice = orderservice;
            _fileservice = fileservice;
            _reciept = reciept;
        }

        // GET: FormPlug/Widget

        #region Order Form

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult CreateOrder()
        {
            return View(FoMVC.FormPlug.Widget.Views.CreateOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateOrder(AddOrUpdateOrderForm model)
        {
            string trackcode = _orderservice.Add(new AddOrUpdateOrderForm
            {
                Name = model.Name.ToSafeHtml(),
                Address = model.Address.ToSafeHtml(),

                Company = model.Company.ToSafeHtml(),
                Description = model.Description.ToSafeHtml(),
                Email = model.Email.ToSafeHtml(),
                Phone = model.Phone,
                Status = FormStatus.جدید,
                Subject = model.Subject,
                Website = model.Website
            });
            _uow.SaveAllChanges();

            return RedirectToAction(FoMVC.FormPlug.Widget.ActionNames.TrakCode, FoMVC.FormPlug.Widget.Name, new { Code = trackcode });
        }

        public virtual ActionResult TrakCode(string Code)
        {
            var model = new TrakCodeViewModel { TrakCode = Code };
            return View(FoMVC.FormPlug.Widget.Views.TrakCode, model);
        }

        #endregion Order Form

        #region Recipt Form

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult UploadRecipt()
        {
            return View(FoMVC.FormPlug.Widget.Views.UploadRecipt);
        }

        public virtual ActionResult UploadRecipt(AddOrUpdateReceipt model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Reciept/";

                path = _fileservice.Add(InputFile, path);

                _reciept.Add(new AddOrUpdateReceipt
                {
                    Name = model.Name.ToSafeHtml(),
                    BankName = model.BankName.ToSafeHtml(),
                    ReciptCode = model.ReciptCode.ToSafeHtml(),
                    ReciptImage = path,
                    ReciptionType = model.ReciptionType,
                    ReciptTime = model.ReciptTime,
                    TrackingCode = model.TrackingCode
                });
                _uow.SaveAllChanges();
            }
            else if (InputFile == null)
            {
                ToastrError("لطفا فایل را انتخاب نمایید");
                return RedirectToAction(FoMVC.FormPlug.Widget.Views.UploadRecipt, FoMVC.FormPlug.Widget.Name);
            }
            return RedirectToAction(FoMVC.FormPlug.Widget.ActionNames.UploadRecipt, FoMVC.FormPlug.Widget.Name);
        }

        #endregion Recipt Form
    }
}