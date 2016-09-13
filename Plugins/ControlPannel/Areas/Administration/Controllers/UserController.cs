using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.IService;
using AradCms.Core.Utility;
using AradCms.Core.ViewModel.User;
using Microsoft.AspNet.Identity;
using Postal;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using WebGrease.Css.Extensions;

namespace ControlPannel.Areas.Administration.Controllers
{
    public partial class UserController : BaseController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;

        #endregion Fields

        #region Constructor

        public UserController(IUnitOfWork unitOfWork, IPermissionService permissionService, IApplicationRoleManager roleManager,
            IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
        }

        #endregion Constructor

        #region List,ListAjax

        [HttpGet]
        [AradAuthorize(SystemPermissionNames.CanViewUsersList, AreaName = "Administration", IsMenu = true)]
        [DisplayName("مشاهده لیست کاربران")]
        public virtual async Task<ActionResult> List()
        {
            await _userManager.UpdateSecurityStampAsync(User.Identity.GetUserId<string>());

            await PopulatePermissions();
            await PopulateRoles();
            return View();
        }

        //[CheckReferrer]
        [AradAuthorize("")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult ListAjax(UserSearchViewModel search)
        {
            int total;
            var users = _userManager.GetPageList(out total, search);
            search.UsersTotal = total;
            var viewModel = new UserListViewModel
            {
                Users = users,
                UserSearchViewModel = search
            };
            return PartialView(PaMVC.Administration.User.Views.ViewNames._ListAjax, viewModel);
        }

        #endregion List,ListAjax

        #region Edit

        [Route("Edit/{id}")]
        [HttpGet]
        [DisplayName("ویرایش کاربر")]
        [AradAuthorize(SystemPermissionNames.CanEditUser, AreaName = "Administration")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetUserByRolesAsync(id);
            if (viewModel == null) return HttpNotFound();
            await PopulateRoles(viewModel.Roles.Select(a => a.RoleId).ToArray());
            var userPermissions = _permissionService.GetPermissionsWithUserId(id);
            await PopulatePermissions(userPermissions.ToArray());
            return View(viewModel);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        //[CheckReferrer]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditUserViewModel viewModel)
        {
            #region Validation

            if (_userManager.CheckUserNameExist(viewModel.UserName, viewModel.Id))
                this.AddErrors("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (_userManager.CheckNameForShowExist(viewModel.NameForShow, viewModel.Id))
                this.AddErrors("NameForShow", "این نام نمایشی قبلا  در سیستم ثبت شده است");
            if (viewModel.Password.IsNotEmpty() && !viewModel.Password.IsSafePasword())
                this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");
            if (_userManager.CheckEmailExist(viewModel.Email, viewModel.Id))
                this.AddErrors("Email", "این ایمیل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckFacebookIdExist(viewModel.FaceBookId, viewModel.Id))
                this.AddErrors("FaceBookId", "این آد دی قبلا در سیستم ثبت شده است");
            if (_userManager.CheckFacebookIdExist(viewModel.GooglePlusId, viewModel.Id))
                this.AddErrors("GooglePlusId", "این آد دی قبلا در سیستم ثبت شده است");

            #endregion Validation

            if (!ModelState.IsValid)
            {
                await PopulatePermissions(viewModel.PermissionIds);
                await PopulateRoles(viewModel.RoleIds);
                return View(viewModel);
            }
            if (viewModel.RoleIds == null || viewModel.RoleIds.Length < 1)
            {
                ToastrWarning("لطفا برای کاربر مورد نظر ، گروه کاربری تعیین کنید");
                await PopulateRoles();
                await PopulatePermissions(viewModel.PermissionIds);
                return View(viewModel);
            }
            var avatarName = "avatar.jpg";
            if (viewModel.AvatarImage != null && viewModel.AvatarImage.ContentLength > 0)
            {
                avatarName = this.UploadFile(viewModel.AvatarImage);
            }
            viewModel.AvatarFileName = avatarName;
            _userManager.EditUserWithRoles(viewModel);

            ToastrSuccess("عملیات  ویرایش کاربر با موفقیت انجام شد");
            return RedirectToAction(PaMVC.Administration.User.ActionNames.List, PaMVC.Administration.User.Name);
        }

        #endregion Edit

        #region Create

        [HttpGet]
        [DisplayName("ثبت کاربر جدید")]
        [AradAuthorize(SystemPermissionNames.CanCreateUser, AreaName = "Administration", IsMenu = true)]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateRoles();
            await PopulatePermissions();
            var viewModel = new AddUserViewModel
            {
                CanUploadFile = true,
                CanModifyFirsAndLastName = true,
                CanChangeProfilePicture = true
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        //[CheckReferrer]
        public virtual async Task<ActionResult> Create(AddUserViewModel viewModel)
        {
            #region Validation

            if (_userManager.CheckUserNameExist(viewModel.UserName, null))
                this.AddErrors("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (_userManager.CheckNameForShowExist(viewModel.NameForShow, null))
                this.AddErrors("NameForShow", "این نام نمایشی قبلا  در سیستم ثبت شده است");
            if (!viewModel.Password.IsSafePasword())
                this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");
            if (_userManager.CheckEmailExist(viewModel.Email, null))
                this.AddErrors("Email", "این ایمیل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckFacebookIdExist(viewModel.FaceBookId, null))
                this.AddErrors("FaceBookId", "این آد دی قبلا در سیستم ثبت شده است");
            if (_userManager.CheckFacebookIdExist(viewModel.GooglePlusId, null))
                this.AddErrors("GooglePlusId", "این آد دی قبلا در سیستم ثبت شده است");

            #endregion Validation

            if (!ModelState.IsValid)
            {
                await PopulateRoles(viewModel.RoleIds);
                await PopulatePermissions(viewModel.PermissionIds);
                return View(viewModel);
            }
            if (viewModel.RoleIds == null || viewModel.RoleIds.Length < 1)
            {
                ToastrWarning("لطفا برای  کاربر مورد نظر ، گروه کاربری تعیین کنید");
                await PopulateRoles();
                await PopulatePermissions();
                return View(viewModel);
            }

            var avatarName = "avatar.jpg";
            if (viewModel.AvatarImage != null && viewModel.AvatarImage.ContentLength > 0)
            {
                avatarName = this.UploadFile(viewModel.AvatarImage);
            }
            viewModel.AvatarFileName = avatarName;
            string confirmationToken = CreateConfirmationToken();
            viewModel.ConfirmationToken = confirmationToken;
            await _userManager.AddUser(viewModel);
            SendEmailConfirmation(viewModel.Email, viewModel.UserName, confirmationToken);
            ToastrSuccess("عملیات ثبت  کاربر جدید با موفقیت انجام شد");
            return RedirectToAction(PaMVC.Administration.User.ActionNames.List, PaMVC.Administration.User.Name);
        }

        #endregion Create

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [AradAuthorize(SystemPermissionNames.CanDeleteUser, AreaName = "Administration")]
        [DisplayName("حذف کاربر")]
        public virtual async Task<ActionResult> Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (!await _userManager.LogicalRemove(id))
            {
                ToastrWarning("این  کاربر ، کاربر سیستمی است و حذف آن باعث اختلال در سیستم خواهد شد");
                return RedirectToAction(PaMVC.Administration.User.ActionNames.List, PaMVC.Administration.User.Name);
            }
            ToastrSuccess("کاربر مورد نظر با موفقیت حذف شد");
            return RedirectToAction(PaMVC.Administration.User.ActionNames.List, PaMVC.Administration.User.Name);
        }

        #endregion Delete

        #region RemoteValidations

        [HttpPost]
        [AjaxOnly]
        // [CheckReferrer]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult PhoneNumberExist(string phoneNumber, string id)
        {
            return _userManager.CheckPhoneNumberExist(phoneNumber, id) ? Json(false) : Json(true);
        }

        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult GooglePlusIdExist(string googlePlusId, string id)
        {
            return _userManager.CheckGooglePlusIdExist(googlePlusId, id) ? Json(false) : Json(true);
        }

        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult FaceBookIdExist(string faceBookId, string id)
        {
            return _userManager.CheckFacebookIdExist(faceBookId, id) ? Json(false) : Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsEmailAvailable(string email)
        {
            return _userManager.IsEmailAvailableForConfirm(email) ? Json(true) : Json(false);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckPassword(string password)
        {
            return password.IsSafePasword() ? Json(true) : Json(false);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsNameForShowExist(string nameForShow, string id)
        {
            return _userManager.CheckNameForShowExist(nameForShow, id) ? Json(false) : Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsEmailExist(string email, string id)
        {
            var check = _userManager.CheckEmailExist(email, id);
            return check ? Json(false) : Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsUserNameExist(string userName, string id)
        {
            return _userManager.CheckUserNameExist(userName, id) ? Json(false) : Json(true);
        }

        #endregion RemoteValidations

        #region Private

        [NonAction]
        private async Task PopulateRoles(params string[] selectedIds)
        {
            var roles = await _roleManager.GetAllAsSelectList();

            if (selectedIds != null)
            {
                roles.ForEach(a => a.Selected = selectedIds.Any(b => (a.Value) == b));
            }

            ViewBag.Roles = roles;
        }

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

        #region EmailConfirmation

        private string CreateConfirmationToken()
        {
            return ShortGuid.NewGuid();
        }

        private void SendEmailConfirmation(string to, string username, string confirmationToken)
        {
            dynamic email = new Email("RegEmail");
            email.To = to;

            email.UserName = username;
            email.ConfirmationToken = confirmationToken;
            email.Send();
        }

        #endregion EmailConfirmation
    }
}