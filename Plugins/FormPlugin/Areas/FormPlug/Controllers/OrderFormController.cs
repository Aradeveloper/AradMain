using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using FormPlugin.IService;
using FormPlugin.ViewModel;
using System.ComponentModel;
using System.Net;
using System.Web.Mvc;

namespace FormPlugin.Areas.FormPlug.Controllers
{
    public partial class OrderFormController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderService _orederservice;

        public OrderFormController(IUnitOfWork unitOfWork, IOrderService orederservice)
        {
            _uow = unitOfWork;
            _orederservice = orederservice;
        }

        // GET: FormPlug/OrderForm
        [HttpGet]
        [AradAuthorize("CanViewOrderItemList", AreaName = "FormPlug", IsMenu = true)]
        [DisplayName("مشاهده لیست فرم سفارشات")]
        public virtual ActionResult List()
        {
            var model = _orederservice.GetOrders();
            return View(FoMVC.FormPlug.OrderForm.Views.List, model);
        }

        [HttpGet]
        [AradAuthorize("CanEditOrderItem", AreaName = "FormPlug", IsMenu = true)]
        [DisplayName("ویرایش سفارشات")]
        public virtual ActionResult Edit(int Id)
        {
            var model = _orederservice.GetUpdateData(Id);
            return View(FoMVC.FormPlug.OrderForm.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual ActionResult Edit(AddOrUpdateOrderForm model)
        {
            var selectedmodel = new AddOrUpdateOrderForm
            {
                Id = model.Id,
                Status = model.Status
            };
            _orederservice.Update(selectedmodel);
            _uow.SaveChanges();
            return RedirectToAction(FoMVC.FormPlug.OrderForm.ActionNames.List, FoMVC.FormPlug.OrderForm.Name);
        }

        [HttpGet]
        [AradAuthorize("CanDeleteOrderItem", AreaName = "FormPlug", IsMenu = true)]
        [DisplayName("حذف سفارش")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _orederservice.Find(id.Value);
            return PartialView(FoMVC.FormPlug.OrderForm.Views.Delete, model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual ActionResult DeleteConfirmed(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _orederservice.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(FoMVC.FormPlug.OrderForm.ActionNames.List, FoMVC.FormPlug.OrderForm.Name, new { area = "FormPlug" });
            }
        }
    }
}