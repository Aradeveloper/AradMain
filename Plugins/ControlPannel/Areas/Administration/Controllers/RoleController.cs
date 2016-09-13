using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.IService;
using AradCms.Core.ViewModel.Role;
using AutoMapper;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using WebGrease.Css.Extensions;

namespace ControlPannel.Areas.Administration.Controllers
{
    public partial class RoleController : BaseController
    {
        #region Fields IMappingEngine

        private readonly IMapper _mappingEngine;
        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;

        #endregion Fields IMappingEngine

        #region Const

        public RoleController(IUnitOfWork unitOfWork, IApplicationRoleManager roleManager, IPermissionService permissionService,
            IApplicationUserManager userManager, IMapper mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _permissionService = permissionService;
            _mappingEngine = mappingEngine;
        }

        #endregion Const

        #region ListAjax , List

        [HttpGet]
        [AradAuthorize(SystemPermissionNames.CanViewRolesList, AreaName = "Administration", IsMenu = true)]
        [DisplayName("مشاهده لیست گروه های کاربری")]
        public virtual ActionResult List()
        {
            return View();
        }

        //[CheckReferrer]
        [AradAuthorize(SystemPermissionNames.CanViewRolesList, AreaName = "Administration", IsMenu = true)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult ListAjax(string term = "", int page = 1)
        {
            int total;
            var roles = _roleManager.GetPageList(out total, term, page, 5);
            ViewBag.TotalRoles = total;
            ViewBag.PageNumber = page;
            return PartialView(PaMVC.Administration.Role.Views.ViewNames._ListAjax, roles);
        }

        #endregion ListAjax , List

        #region Create

        [HttpGet]
        [AradAuthorize(SystemPermissionNames.CanCreateRole, AreaName = "Administration", IsMenu = true)]
        [DisplayName("ثبت گروه کاربری جدید")]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = new AddRoleViewModel
            {
                IsActive = true
            };
            await PopulatePermissions();
            return View(PaMVC.Administration.Role.Views.Create, viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        public virtual async Task<ActionResult> Create(AddRoleViewModel viewModel, params int[] permissionIds)
        {
            if (!ModelState.IsValid)
            {
                ToastrError("لطفا فیلد های مورد نظر را با دقت وارد کنید");
                await PopulatePermissions(permissionIds);
                return View(viewModel);
            }
            if (permissionIds == null)
            {
                ToastrWarning("لطفا برای گروه کاربری مورد نظر ، دسترسی تعیین کنید");
                await PopulatePermissions();
                return View(viewModel);
            }
            _roleManager.AddRoleWithPermissions(viewModel, permissionIds);
            ToastrSuccess("عملیات ثبت گروه کاربری جدید با موفقیت انجام شد");
            return RedirectToAction(PaMVC.Administration.Role.ActionNames.List, PaMVC.Administration.Role.Name, new { area = PaMVC.Administration.Name });
        }

        #endregion Create

        #region Edit

        [HttpGet]
        [DisplayName("ویرایش گروه کاربری")]
        [AradAuthorize(SystemPermissionNames.CanEditRole, AreaName = "Administration", IsMenu = false)]

        //[Route("Edit/{id}")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _roleManager.GetRoleByPermissions(id);
            if (viewModel == null)
                return HttpNotFound();
            await PopulatePermissions(viewModel.Permissions.Select(a => a.Id).ToArray());
            return View(PaMVC.Administration.Role.Views.Edit, viewModel);
        }

        //[Route("Edit/{id}")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        public virtual async Task<ActionResult> Edit(EditRoleViewModel viewModel, params int[] permissionIds)
        {
            if (!ModelState.IsValid)
            {
                ToastrError("لطفا فیلد های مورد نظر را با دقت وارد کنید");
                await PopulatePermissions(permissionIds);
                return View(viewModel);
            }
            if (permissionIds == null || permissionIds.Length < 1)
            {
                ToastrWarning("لطفا برای گروه کاربری مورد نظر ، دسترسی تعیین کنید");
                await PopulatePermissions();
                return View(viewModel);
            }

            _roleManager.EditRoleWithPermissions(viewModel, permissionIds);
            await _unitOfWork.SaveChangesAsync();

            ToastrSuccess("عملیات ویرایش گروه کاربری  با موفقیت انجام شد");
            return RedirectToAction(PaMVC.Administration.Role.ActionNames.List, PaMVC.Administration.Role.Name, new { area = PaMVC.Administration.Name });
        }

        #endregion Edit

        #region Delete

        [HttpPost]
        //[Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [AradAuthorize(SystemPermissionNames.CanDeleteRole, AreaName = "Administration", IsMenu = false)]
        [DisplayName("حذف گروه کاربری")]
        public virtual async Task<ActionResult> Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (await _roleManager.CheckRoleIsSystemRoleAsync(id))
            {
                ToastrWarning("این گروه کاربری سیستمی است و حذف آن باعث اختلال در سیستم خواهد شد");
                return RedirectToAction(PaMVC.Administration.Role.ActionNames.List, PaMVC.Administration.Role.Name, new { area = PaMVC.Administration.Name });
            }
            await _roleManager.RemoveById(id);
            ToastrSuccess("گروه مورد نظر با موفقیت حذف شد");
            return RedirectToAction(PaMVC.Administration.Role.ActionNames.List, PaMVC.Administration.Role.Name, new { area = PaMVC.Administration.Name });
        }

        #endregion Delete

        #region SetAsDefaultRegisterRole

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [AradAuthorize(SystemPermissionNames.CanSetDefaultRoleForRegister, AreaName = "Administration", IsMenu = false)]
        [DisplayName("تغییر گروه کاربری پیش فرض")]
        [Route("SetForRegister/{id}")]
        public virtual async Task<ActionResult> SetRoleForRegister(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _roleManager.SetRoleAsRegistrationDefaultRoleAsync(id);
            await _unitOfWork.SaveChangesAsync();
            ToastrSuccess("گروه کاربری مورد نظر با موفقیت به عنوان گروه کاربری پیشفرض ثبت نام انتخاب شد");
            return RedirectToAction(PaMVC.Administration.Role.ActionNames.List, PaMVC.Administration.Role.Name, new { area = PaMVC.Administration.Name });
        }

        #endregion SetAsDefaultRegisterRole

        #region RemoteValidation

        [HttpPost]
        [AjaxOnly]
        // [CheckReferrer]
        [AradAuthorize("")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult RoleNameExist(string name, string id)
        {
            return _roleManager.ChechForExisByName(name, id) ? Json(false) : Json(true);
        }

        #endregion RemoteValidation

        #region Private

        [NonAction]
        private async Task PopulatePermissions(params int[] selectedIds)
        {
            var permissions = await _permissionService.GetAsSelectList();

            if (selectedIds != null)
            {
                permissions.ForEach(a => a.Selected = selectedIds.Any(b => int.Parse(a.Value) == b));
            }

            ViewBag.Permissions = permissions;
        }

        #endregion Private
    }
}