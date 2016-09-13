using AradCms.Core.Caching;
using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.Utility;
using AradCms.Core.ViewModel.User;
using AutoMapper;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace AradCms.Core.Service
{
    public class ApplicationUserManager : UserManager<ApplicationUser, string>, IApplicationUserManager
    {
        #region Fields

        private readonly HttpContextBase _contextBase;
        private readonly IPermissionService _permissionService;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ApplicationUser> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMapper _mappingEngine;
        private readonly IIdentity _identity;
        private ApplicationUser _user;

        #endregion Fields

        #region Constructor

        public ApplicationUserManager(HttpContextBase contextBase, IPermissionService permissionService, IUserStore<ApplicationUser, string> userStore, IApplicationRoleManager roleManager, IUnitOfWork unitOfWork,
            IMapper mappingEngine, IDataProtectionProvider dataProtectionProvider,
             IIdentityMessageService smsService,
             IIdentity identity,
            IIdentityMessageService emailService)
            : base(userStore)
        {
            _permissionService = permissionService;
            AutoCommitEnabled = true;
            _dataProtectionProvider = dataProtectionProvider;
            _mappingEngine = mappingEngine;
            EmailService = emailService;
            SmsService = smsService;
            _unitOfWork = unitOfWork;
            _identity = identity;
            _users = _unitOfWork.Set<ApplicationUser>();
            _roleManager = roleManager;
            _contextBase = contextBase;
            CreateApplicationUserManager();
        }

        #endregion Constructor

        #region GenerateUserIdentityAsync

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion GenerateUserIdentityAsync

        #region HasPassword

        public async Task<bool> HasPassword(string userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        #endregion HasPassword

        #region HasPhoneNumber

        public async Task<bool> HasPhoneNumber(string userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }

        #endregion HasPhoneNumber

        #region OnValidateIdentity

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, string>(
                         validateInterval: TimeSpan.FromMinutes(30),
                         regenerateIdentityCallback: GenerateUserIdentityAsync,
                         getUserIdCallback: id => id.GetUserId<string>());
        }

        #endregion OnValidateIdentity

        #region SeedDatabase

        public void SeedDatabase()
        {
            try
            {
                const string systemAdminUserName = "SystemAdmin";
                const string systemAdminEmail = "info@aradeveloper.ir";
                const string systemAdminPassword = "info@aradeveloper.ir";
                const string systemAdminNameforShow = "مدیر سیستم";
                var newUser = this.FindByName(systemAdminUserName);
                if (newUser == null)
                {
                    newUser = new ApplicationUser
                    {
                        UserName = systemAdminUserName.ToLower(),
                        CanChangeProfilePicture = true,
                        CanModifyFirsAndLastName = true,
                        CanUploadFile = true,
                        EmailConfirmed = true,
                        IsSystemAccount = true,
                        LockoutEnabled = false,
                        PhoneNumberConfirmed = true,
                        Email = systemAdminEmail.FixGmailDots(),
                        RegisterDate = DateTime.Now,
                        LastActivityDate = DateTime.Now,
                        AvatarFileName = "avatar.jpg",

                        NameForShow = systemAdminNameforShow
                    };
                    var result = this.Create(newUser, systemAdminPassword);
                }
                var userRoles = _roleManager.FindUserRoles(newUser.Id);
                if (userRoles.Any(a => a == SystemRoleNames.SuperAdministrators)) return;
                this.AddToRole(newUser.Id, SystemRoleNames.SuperAdministrators);
            }
            catch (Exception ex)
            { }
        }

        #endregion SeedDatabase

        #region GenerateUserIdentityAsync

        private static async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }

        #endregion GenerateUserIdentityAsync

        #region GetAllUsersAsync

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }

        #endregion GetAllUsersAsync

        #region CreateApplicationUserManager

        private void CreateApplicationUserManager()
        {
            UserValidator = new CustomUserValidator<ApplicationUser, string>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            if (_dataProtectionProvider == null) return;

            var dataProtector = _dataProtectionProvider.Create("Asp.net Identity");
            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, string>(dataProtector);
        }

        #endregion CreateApplicationUserManager

        #region DeleteAll

        public async Task RemoveAll()
        {
            await Users.DeleteAsync();
        }

        #endregion DeleteAll

        #region GetUsersWithRoleIds

        public IList<ApplicationUser> GetUsersWithRoleIds(params string[] ids)
        {
            return Users.Where(applicationUser => ids.Contains(applicationUser.Id))
                .ToList();
        }

        #endregion GetUsersWithRoleIds

        #region AutoCommitEnabled

        public bool AutoCommitEnabled { get; set; }

        #endregion AutoCommitEnabled

        #region Any

        public bool Any()
        {
            return _users.Any();
        }

        #endregion Any

        #region AddRange

        public void AddRange(IEnumerable<ApplicationUser> users)
        {
            _unitOfWork.AddThisRange(users);
        }

        #endregion AddRange

        #region GetPageList

        public IList<UserViewModel> GetPageList(out int total, UserSearchViewModel search)
        {
            var users = _users.AsNoTracking().OrderBy(a => a.Id).AsQueryable();
            if (search.RoleIds != null && search.RoleIds.Length > 0)
            {
                users =
                    users.Include(a => a.Roles)
                        .Where(a => a.Roles.Select(r => r.RoleId).Any())
                        .AsQueryable();
            }
            if (search.SearchUserName.IsNotEmpty())
                users = users.SearchByUserName(search.SearchUserName);
            if (search.SearchEmail.IsNotEmpty())
                users = users.SearchByEmail(search.SearchEmail);
            if (search.SearchNameForShow.IsNotEmpty())
                users = users.SearchByNameForShow(search.SearchNameForShow);

            if (search.SearchIp.IsNotEmpty())
                users = users.SearchByIp(search.SearchIp);
            if (search.SearchCanChangeProfilePicture)
                _unitOfWork.EnableFiltering(UserFilters.CanChangeProfilePicList);
            if (search.SearchCanModifyFirsAndLastName)
                _unitOfWork.EnableFiltering(UserFilters.CanModifyFirsAndLastNameList);
            if (search.SearchCanUploadFile)
                _unitOfWork.EnableFiltering(UserFilters.CanUploadfileList);
            if (search.SearchIsBanned)
                _unitOfWork.EnableFiltering(UserFilters.BannedList);
            if (search.SearchIsDeleted)
                _unitOfWork.EnableFiltering(UserFilters.DeletedList);
            if (search.SearchIsEmailConfirmed)
                _unitOfWork.EnableFiltering(UserFilters.EmailConfirmedList);
            if (search.SearchIsSystemAccount)
                _unitOfWork.EnableFiltering(UserFilters.SystemAccountList);

            total = users.FutureCount();
            var query =
                users.OrderByUserName()
                    .SkipAndTake(search.PageIndex - 1, search.PageSize)
                    .Future().ToList();
            return _mappingEngine.Map<IList<UserViewModel>>(query);
        }

        #endregion GetPageList

        #region GetUserByRoles

        public async Task<EditUserViewModel> GetUserByRolesAsync(string id)
        {
            var userWithRoles = await
                 _users.AsNoTracking()
                     .Include(a => a.Roles)
                     .FirstOrDefaultAsync(a => a.Id == id);
            return _mappingEngine.Map<EditUserViewModel>(userWithRoles);
        }

        #endregion GetUserByRoles

        #region EditUserWithRoles

        public void EditUserWithRoles(EditUserViewModel viewModel)
        {
            _unitOfWork.ValidateOnSaveEnabled = false;
            var user = _users.Include(a => a.Roles).AsNoTracking().First(a => a.Id == viewModel.Id);

            if (viewModel.IsBanned)
                viewModel.IsBanned = !user.IsSystemAccount;
            if (viewModel.IsDeleted)
                viewModel.IsDeleted = !user.IsSystemAccount;
            viewModel.IsSystemAccount = user.IsSystemAccount;

            _mappingEngine.Map(viewModel, user);

            if (viewModel.Password.IsNotEmpty())
                user.PasswordHash = PasswordHasher.HashPassword(viewModel.Password);
            foreach (var roleId in from roleId in viewModel.RoleIds let userRoleRecord = user.Roles.FirstOrDefault(a => a.RoleId == roleId) where userRoleRecord == null select roleId)
            {
                user.Roles.Add(new ApplicationUserRole { RoleId = roleId, UserId = user.Id });
            }

            if (viewModel.PermissionIds != null && viewModel.PermissionIds.Length > 0)
            {
                user.OwnPermissions = new Collection<ApplicationPermission>();
                var permissions = _permissionService.GetActualPermissions(viewModel.PermissionIds).ToList();
                permissions.ForEach(a => user.OwnPermissions.Add(a));
                _unitOfWork.Update(user, a => a.AssociatedCollection(b => b.OwnPermissions));
            }

            if (user.IsDeleted || user.IsBanned)
                this.UpdateSecurityStamp(user.Id);
            else
            {
                _unitOfWork.SaveChanges();
            }
        }

        #endregion EditUserWithRoles

        #region SetRolesToUser

        public void SetRolesToUser(ApplicationUser user, IEnumerable<ApplicationRole> roles)
        {
            throw new NotImplementedException();
        }

        #endregion SetRolesToUser

        #region AddUser

        public async Task AddUser(AddUserViewModel viewModel)
        {
            var user = _mappingEngine.Map<ApplicationUser>(viewModel);
            viewModel.RoleIds.ToList().ForEach(roleId => user.Roles.Add(new ApplicationUserRole { RoleId = roleId }));
            this.Create(user, viewModel.Password);

            _unitOfWork.MarkAsDetached(user);

            if (viewModel.PermissionIds == null || viewModel.PermissionIds.Length <= 0) return;

            user.OwnPermissions = new Collection<ApplicationPermission>();
            var permissions = _permissionService.GetActualPermissions(viewModel.PermissionIds).ToList();
            permissions.ForEach(a => user.OwnPermissions.Add(a));
            _unitOfWork.Update(user, a => a.AssociatedCollection(b => b.OwnPermissions));
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion AddUser

        #region LogicalRemove

        public async Task<bool> LogicalRemove(string id)
        {
            var key = id.ToString(CultureInfo.InvariantCulture) + "_roles";
            _contextBase.InvalidateCache(key);
            _unitOfWork.EnableFiltering(UserFilters.NotSystemAccountList);
            var result = await _users.Where(a => a.Id == id).UpdateAsync(a => new ApplicationUser { IsDeleted = true });
            return result > 0;
        }

        #endregion LogicalRemove

        #region Validations

        public bool CheckUserNameExist(string userName, string id)
        {
            return id == null
                ? _users.Any(a => a.UserName == userName.ToLower())
                : _users.Any(a => a.UserName == userName.ToLower() && a.Id != id);
        }

        public bool CheckEmailExist(string email, string id)
        {
            email = email.FixGmailDots();
            return id == null
               ? _users.Any(a => a.Email == email.ToLower())
               : _users.Any(a => a.Email == email.ToLower() && a.Id != id);
        }

        public bool CheckNameForShowExist(string nameForShow, string id)
        {
            var namesForShow = _users.Select(a => new { Name = a.NameForShow, Id = a.Id }).ToList();
            nameForShow = nameForShow.GetFriendlyPersianName();
            return id == null
             ? namesForShow.Any(a => a.Name.GetFriendlyPersianName() == nameForShow)
             : namesForShow.Any(a => a.Name.GetFriendlyPersianName() == nameForShow && a.Id != id);
        }

        public bool CheckGooglePlusIdExist(string googlePlusId, string id)
        {
            return id == null
                    ? _users.Any(a => a.GooglePlusId == googlePlusId)
                    : _users.Any(a => a.GooglePlusId == googlePlusId && a.Id != id);
        }

        public bool CheckFacebookIdExist(string faceBookId, string id)
        {
            return id == null
              ? _users.Any(a => a.FaceBookId == faceBookId)
              : _users.Any(a => a.FaceBookId == faceBookId && a.Id != id);
        }

        public bool CheckPhoneNumberExist(string phoneNumber, string id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.Id != id);
        }

        #endregion Validations

        #region override GetRolesAsync

        public async override Task<IList<string>> GetRolesAsync(string userId)
        {
            var permissions = await GetUserPermissionsAsync(userId);

            return permissions;
        }

        #endregion override GetRolesAsync

        #region CreateAsync

        public async Task<string> CreateAsync(ViewModel.Account.RegisterViewModel viewModel)
        {
            var user = _mappingEngine.Map<ApplicationUser>(viewModel);
            await CreateAsync(user, viewModel.Password);
            var defultRoleName = await _roleManager.GetDefaultRoleForRegister();
            if (defultRoleName.IsNotEmpty())
                await AddToRoleAsync(user.Id, defultRoleName);
            return user.Id;
        }

        #endregion CreateAsync

        #region CustomValidatePasswordAsync

        public async Task<string> CustomValidatePasswordAsync(string pass)
        {
            var result = await PasswordValidator.ValidateAsync(pass);
            return result.Errors.GetUserManagerErros();
        }

        #endregion CustomValidatePasswordAsync

        #region ChechIsBanneduser

        public bool CheckIsUserBannedOrDelete(string id)
        {
            return _users.Any(a => a.Id == id && (a.IsBanned || a.IsDeleted));
        }

        public bool CheckIsUserBanned(string id)
        {
            _unitOfWork.EnableFiltering(UserFilters.BannedList);
            var result = _users.Any(a => a.Id == id);
            _unitOfWork.DisableFiltering(UserFilters.BannedList);
            return result;
        }

        public bool CheckIsUserBannedOrDeleteByEmail(string email)
        {
            string emt = email.FixGmailDots();
            return _users.Any(a => a.Email == emt && (a.IsBanned || a.IsDeleted));
        }

        public bool CheckIsUserBannedByEmail(string email)
        {
            _unitOfWork.EnableFiltering(UserFilters.BannedList);
            email = email.FixGmailDots();
            var result = _users.Any(a => a.Email == email);
            _unitOfWork.DisableFiltering(UserFilters.BannedList);
            return result;
        }

        public bool CheckIsUserBannedByUserName(string userName)
        {
            _unitOfWork.EnableFiltering(UserFilters.BannedList);
            userName = userName.ToLower();
            var result = _users.Any(a => a.UserName == userName);
            _unitOfWork.DisableFiltering(UserFilters.BannedList);
            return result;
        }

        #endregion ChechIsBanneduser

        #region GetEmailStore

        public IUserEmailStore<ApplicationUser, string> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<ApplicationUser, string>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }

        #endregion GetEmailStore

        #region Private

        private static string[] SplitString(string dependencies)
        {
            if (dependencies == null) return new string[0];
            var split = from dependency in dependencies.Split(',')
                        let lowerDependency = dependency.ToLower()
                        where !string.IsNullOrEmpty(lowerDependency)
                        select lowerDependency;
            return split.ToArray();
        }

        #endregion Private

        #region IsEmailConfirmedByUserNameAsync

        public bool IsEmailConfirmedByUserNameAsync(string userName)
        {
            _unitOfWork.EnableFiltering(UserFilters.EmailConfirmedList);
            var result = _users.Any(a => a.UserName == userName.ToLower());
            _unitOfWork.DisableFiltering(UserFilters.EmailConfirmedList);
            return result;
        }

        #endregion IsEmailConfirmedByUserNameAsync

        #region IsEmailAvailableForConfirm

        public bool IsEmailAvailableForConfirm(string email)
        {
            email = email.FixGmailDots();
            return _users.Any(a => a.Email == email);
        }

        #endregion IsEmailAvailableForConfirm

        #region GetUserPermissions

        public IList<string> GetUserPermissions(string userId)
        {
            var rolesOfUser = _roleManager.FindUserRoleIds(userId);
            var userPermissions = _permissionService.GetUserPermissions(rolesOfUser.ToArray(), userId);
            return userPermissions;
        }

        private async Task<IList<string>> GetUserPermissionsAsync(string userId)
        {
            var rolesOfUser = await _roleManager.FindUserRoleIdsAsync(userId);
            var userPermissions = _permissionService.GetUserPermissions(rolesOfUser.ToArray(), userId);
            return userPermissions;
        }

        #endregion GetUserPermissions

        #region IsModifiedRolesOrPermissions

        public bool IsModifiedRolesOrPermissions(string userId)
        {
            return _users.Any(a => a.Id == userId && a.PermissionsOrRolesModified);
        }

        #endregion IsModifiedRolesOrPermissions

        #region SetFalseModifyRolesOrPermissions

        public void SetFalseModifyRolesOrPermissions(string userId)
        {
            _users.Where(a => a.Id == userId).Update(a => new ApplicationUser { PermissionsOrRolesModified = false });
        }

        #endregion SetFalseModifyRolesOrPermissions

        #region EditSecurityStamp

        public void EditSecurityStamp(string userId)
        {
            this.UpdateSecurityStamp(userId);
        }

        #endregion EditSecurityStamp

        public string GetNameForShow(string id)
        {
            var user = _users.Find(id).NameForShow;
            return (user);
        }

        public string GetCurrentUserId()
        {
            return _identity.GetUserId<string>();
        }

        public ApplicationUser GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()));
        }

        public ApplicationUser FindById(int userId)
        {
            return _users.Find(userId);
        }

        Task<string> IApplicationUserManager.GetAccessFailedCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser FindByName(string userName)
        {
            return _users.Where(a => a.UserName == userName).SingleOrDefault();
        }
    }
}