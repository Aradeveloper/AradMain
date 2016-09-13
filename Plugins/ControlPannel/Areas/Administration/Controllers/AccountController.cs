using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.Helpers;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.Utility;
using AradCms.Core.ViewModel.Account;
using AradCms.Core.ViewModel.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Postal;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ControlPannel.Areas.Administration.Controllers
{
    public partial class AccountController : BaseController
    {
        #region Fields

        private readonly HttpContextBase _httpContextBase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ApplicationUser> _appuser;
        private readonly IUserMailer _userMailer;
        public static string _backurl = "";

        #endregion Fields

        #region Constructor

        public AccountController(HttpContextBase httpContextBase, IApplicationUserManager userManager, IUnitOfWork unitOfWork,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager, IUserMailer userMailer
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _userMailer = userMailer;
            _unitOfWork = unitOfWork;
            _httpContextBase = httpContextBase;
            _appuser = _unitOfWork.Set<ApplicationUser>();
        }

        #endregion Constructor

        #region ConfirmEmail

        [AllowAnonymous]
        public virtual ActionResult RegisterConfirmation(string Id)
        {
            if (ConfirmAccount(Id))
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        private bool ConfirmAccount(string confirmationToken)
        {
            //if(enable confirm email feature then show confirm page)
            //return view("info")
            if (confirmationToken == null)
                return false;

            ApplicationUser user = _appuser.SingleOrDefault(u => u.SecurityStamp == confirmationToken);
            //var result = await _userManager.ConfirmEmailAsync(userId.Value, code);
            if (user != null)
            {
                user.EmailConfirmed = true;
                EditUserViewModel userview = new EditUserViewModel()
                {
                    EmailConfirmed = user.EmailConfirmed
                };
                _userManager.EditUserWithRoles(userview);

                return true;
            }

            ToastrWarning("مشکلی در فعال سازی اکانت شما به وجود آمد");
            return false; /*RedirectToAction(PaMVC.Administration.Account.ActionNames.ReceiveActivatorEmail, PaMVC.Administration.Account.Name);*/
        }

        #endregion ConfirmEmail

        #region ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });

                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _authenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                this.AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion ExternalLogin

        #region ForgetPassword

        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            //if(enable forget feature then show forget page)
            //return view("info")
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View(PaMVC.Administration.Account.Views.ViewNames.ResetPasswordConfirmation);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            if (Request.Url == null) return View(PaMVC.Administration.Account.Views.ViewNames.ForgotPasswordConfirmation);
            var callbackUrl = Url.Action(PaMVC.Administration.Account.ActionNames.ResetPassword, PaMVC.Administration.Account.Name,
                new { userId = user.Id, code }, protocol: Request.Url.Scheme);
            await _userMailer.ResetPassword(new EmailViewModel
            {
                Message = "با سلام کاربر گرامی.برای بازیابی کلمه عبور خود لازم است بر روی لینک مقابل کلیک کنید",
                To = model.Email,
                Url = callbackUrl,
                UrlText = "بازیابی کلمه عبور",
                Subject = "بازیابی کلمه عبور",
                ViewName = PaMVC.Administration.UserMailer.Views.ViewNames.ResetPassword
            }
               ).SendAsync();

            return View(PaMVC.Administration.Account.ActionNames.ForgotPasswordConfirmation, PaMVC.Administration.Account.Name);
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion ForgetPassword

        #region Login,LogOff

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            //if(enable login feature then show login page)
            //return view("info")
            _backurl = returnUrl;
            ViewBag.ReturnUrl = _backurl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl, bool CaptchaValid)
        {
            if (!CaptchaValid)
            {
                ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
                return View(model);
            }
            if (_userManager.CheckIsUserBannedOrDelete(model.UserName))
            {
                this.AddErrors("UserName", "حساب کاربری شما مسدود شده است");
                return View(model);
            }
            if (!_userManager.IsEmailConfirmedByUserNameAsync(model.UserName))
            {
                ToastrWarning("برای ورود به سایت لازم است حساب خود را فعال کنید");
                return RedirectToAction(PaMVC.Administration.Account.ActionNames.ReceiveActivatorEmail, PaMVC.Administration.Account.Name);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync
                (model.UserName.ToLower(), model.Password, model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(_backurl);

                case SignInStatus.LockedOut:
                    ToastrError(
                        string.Format("دقیقه دوباره امتحان کنید {0} حساب شما قفل شد ! لطفا بعد از ",
                            _userManager.DefaultAccountLockoutTimeSpan), isSticky: true);
                    return View(model);

                case SignInStatus.Failure:
                    ToastrError(ModelState.GetListOfErrors());
                    return View(model);

                default:
                    ToastrError(
                        "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید",
                        isSticky: true);
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [CheckReferrer]
        [AradAuthorize]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion Login,LogOff

        #region Register

        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            // if("register is enable")
            // return RedirectToAction("info)
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model, bool CaptchaValid)
        {
            #region Validation

            if (!CaptchaValid)
            {
                ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
                return View(model);
            }
            else
            {
                if (_userManager.CheckEmailExist(model.Email, null))
                    this.AddErrors("Email", "این ایمیل قبلا در سیستم ثبت شده است");

                if (_userManager.CheckUserNameExist(model.UserName, null))
                    this.AddErrors("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");

                if (_userManager.CheckNameForShowExist(model.NameForShow, null))
                    this.AddErrors("NameForShow", "این نام نمایشی قبلا در سیستم ثبت شده است");

                if (!model.Password.IsSafePasword())
                    this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string confirmationToken = CreateConfirmationToken();

                #endregion Validation

                var userId = await _userManager.CreateAsync(model);
                SendConfirmationEmail(model.Email, model.UserName, confirmationToken);

                ToastrSuccess("حساب کاربری شما با موفقیت ایجاد شد. برای فعال سازی " +
                              "حساب خود به صندوق پستی خود مراجعه کنید",
                    isSticky: true);

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        private string CreateConfirmationToken()
        {
            return ShortGuid.NewGuid();
        }

        #endregion Register

        #region ResePassword

        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            //if(enable resetpass feature then show resetpass page)
            //return view("info")
            if (code == null) return HttpNotFound();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model, bool CaptchaValid)
        {
            if (!CaptchaValid)
            {
                ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
                return View(model);
            }
            if (!model.Password.IsSafePasword())
                this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email.ToLower());
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(PaMVC.Administration.Account.ActionNames.ResetPasswordConfirmation, PaMVC.Administration.Account.Name);
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false, false);
                return RedirectToAction(PaMVC.Administration.Account.ActionNames.ResetPasswordConfirmation, PaMVC.Administration.Account.Name);
            }
            this.AddErrors(result);
            ToastrError(ModelState.GetListOfErrors());
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion ResePassword

        #region ReceiveActivatorEmail

        [AllowAnonymous]
        public virtual ActionResult ReceiveActivatorEmail()
        {
            //if(enable receiveactivator feature then show receiveactivator page)
            //return view("info")
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ReceiveActivatorEmail(ActivationEmailViewModel viewModel, bool CaptchaValid)
        {
            if (!CaptchaValid)
            {
                ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
                return View(viewModel);
            }
            if (!_userManager.IsEmailAvailableForConfirm(viewModel.Email))
                this.AddErrors("Email", "ایمیل مورد نظر یافت نشد");
            if (_userManager.CheckIsUserBannedOrDeleteByEmail(viewModel.Email))
                this.AddErrors("Email", "اکانت شما مسدود شده است");
            if (!ModelState.IsValid)
                return View(viewModel);
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            //SendEmailConfirmation(viewModel.Email, viewModel.UserName, confirmationToken);
            //await SendConfirmationEmail(viewModel.Email, user.Id);
            ToastrSuccess("ایمیلی تحت عنوان فعال سازی اکانت به آدرس ایمیل شما ارسال گردید");
            return RedirectToAction(PaMVC.Administration.Account.ActionNames.ReceiveActivatorEmail, PaMVC.Administration.Account.Name);
        }

        #endregion ReceiveActivatorEmail

        #region PannelMenu

        [OverrideAuthorization]
        [Authorize]
        public virtual ActionResult PannelMenu()
        {
            //get user info
            //get PannelMneu page content from site setting

            return View();
        }

        #endregion PannelMenu

        #region Validation

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
        public virtual JsonResult CheckNewPassword(string NewPassword)
        {
            return NewPassword.IsSafePasword() ? Json(true) : Json(false);
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

        #endregion Validation

        #region Private

        public async Task SendConfirmationEmail(string to, string username, string confirmationToken)
        {
            dynamic email = new Email("RegEmail");
            email.To = to;
            email.UserName = username;
            email.ConfirmationToken = confirmationToken;
            email.Send();
        }

        #endregion Private

        #region Change Password

        [HttpGet]
        [Authorize]
        public virtual ActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(PaMVC.Administration.Account.Views.ChangePassword, model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(_userManager.FindByName(User.Identity.GetUserName()).Id, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.GetCurrentUserAsync();
                if (user != null)
                {
                    await signInAsync(user, isPersistent: false);
                }
                return RedirectToAction(PaMVC.Administration.Default.ActionNames.Index, PaMVC.Administration.Default.Name, new { area = PaMVC.Administration.Name, Message = ManageMessageId.ChangePasswordSuccess });
            }
            addErrors(result);
            return View(model);
        }

        private void addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task signInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await _userManager.GenerateUserIdentityAsync(user));
        }

        #endregion Change Password
    }
}