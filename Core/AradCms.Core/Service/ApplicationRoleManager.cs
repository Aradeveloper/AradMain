using AradCms.Core.Context;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.Utility;
using AradCms.Core.ViewModel.Role;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using RefactorThis.GraphDiff;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AradCms.Core.Service
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>, IApplicationRoleManager
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionService _permissionService;
        private readonly IDbSet<ApplicationRole> _roles;

        #endregion Fields

        #region Constructor

        public ApplicationRoleManager(IMapper mappingEngine, IPermissionService permissionService, IUnitOfWork unitOfWork, IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
            _unitOfWork = unitOfWork;
            _roles = _unitOfWork.Set<ApplicationRole>();
            _permissionService = permissionService;
            _mappingEngine = mappingEngine;
            AutoCommitEnabled = true;
        }

        #endregion Constructor

        #region FindRoleByName

        public ApplicationRole FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }

        #endregion FindRoleByName

        #region CreateRole

        public IdentityResult CreateRole(ApplicationRole role)
        {
            return this.Create(role); // RoleManagerExtensions
        }

        #endregion CreateRole

        #region GetUsersOfRole

        public IList<ApplicationUserRole> GetUsersOfRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }

        #endregion GetUsersOfRole

        #region GetApplicationUsersInRole

        public IList<ApplicationUser> GetApplicationUsersInRole(string roleName)
        {
            //var roleUserIdsQuery = from role in Roles
            //                       where role.Name == roleName
            //                       from user in role.Users
            //                       select user.UserId;

            return null; //_userManager.GetUsersWithRoleIds(roleUserIdsQuery.ToArray());
        }

        #endregion GetApplicationUsersInRole

        #region FindUserRoles

        public IList<string> FindUserRoles(string userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).Cacheable().ToList();
        }

        public IList<string> FindUserRoleIds(string userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.Select(a => a.Id).Cacheable().ToList();
        }

        public async Task<IList<string>> FindUserRoleIdsAsync(string userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return await userRolesQuery.Select(a => a.Id).Cacheable().ToListAsync();
        }

        #endregion FindUserRoles

        #region GetRolesForUser

        public string[] GetRolesForUser(string userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.ToArray();
        }

        #endregion GetRolesForUser

        #region IsUserInRole

        public bool IsUserInRole(string userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        #endregion IsUserInRole

        #region GetAllApplicationRolesAsync

        public async Task<IList<ApplicationRole>> GetAllApplicationRolesAsync()
        {
            return await Roles.ToListAsync();
        }

        #endregion GetAllApplicationRolesAsync

        #region GetPermissionsOfUser

        public async Task<IList<string>> GetPermissionsOfUser(string userId)
        {
            var rolesOfUser = GetRolesForUser(userId);
            var permissions = new List<string>();

            foreach (var role in rolesOfUser)
            {
                var roleName = role;
                var permissionsOfRole =
                    await
                        Roles.Where(a => a.Name == roleName)
                            .SelectMany(a => a.Permissions)
                            .Select(a => a.PName)
                            .ToListAsync();
                permissions.AddRange(permissionsOfRole);
            }

            return permissions;
        }

        #endregion GetPermissionsOfUser

        #region GetPermissionsOfRole

        public IList<string> GetPermissionsOfRole(string roleName)
        {
            return
                 Roles.Where(a => a.Name == roleName)
                    .SelectMany(a => a.Permissions)
                    .Select(a => a.PName)
                    .ToList();
        }

        #endregion GetPermissionsOfRole

        #region SetPermissionsToRole

        public void SetPermissionsToRole(ApplicationRole role, IEnumerable<ApplicationPermission> permissions)
        {
            role.Permissions = new Collection<ApplicationPermission>();
            foreach (var permission in permissions)
            {
                role.Permissions.Add(permission);
            }
            if (role.IsDefaultForRegister)
                ChangeDefaultRegisterRole(role.Id);
            _unitOfWork.Update(role, a => a.AssociatedCollection(b => b.Permissions));
            _unitOfWork.SaveChanges();
        }

        #endregion SetPermissionsToRole

        #region SeedDatabase

        /// <summary>
        /// for instal permissions with roles
        /// </summary>
        public void SeedDatabase(IEnumerable<ApplicationPermission> permissions)
        {
            var applicationPermissions = permissions as IList<ApplicationPermission> ?? permissions.ToList();
            _permissionService.SeedDatabase(applicationPermissions);

            var superAdministrators = new ApplicationRole
            {
                Name = SystemRoleNames.SuperAdministrators,
                IsActive = true,
                IsSystemRole = true,
                Description = "مدیران سیستمی برنامه",
                Permissions = new Collection<ApplicationPermission>()
            };

            _roles.AddOrUpdate(a => a.Name, superAdministrators);
            _unitOfWork.SaveChanges();
            _unitOfWork.MarkAsDetached(superAdministrators);

            foreach (var permission in applicationPermissions)
            {
                superAdministrators.Permissions.Add(permission);
            }
            _unitOfWork.Update(superAdministrators, a => a.AssociatedCollection(c => c.Permissions));
            _unitOfWork.SaveChanges();
        }

        #endregion SeedDatabase

        #region DeleteAll

        public async Task RemoteAll()
        {
            await Roles.DeleteAsync();
        }

        #endregion DeleteAll

        #region GetList

        public async Task<IEnumerable<RoleViewModel>> GetList()
        {
            return await _roles.ProjectTo<RoleViewModel>().ToListAsync();
        }

        #endregion GetList

        #region AddRoleWithPermissions

        public void AddRoleWithPermissions(AddRoleViewModel viewModel, params int[] ids)
        {
            var role = _mappingEngine.Map<ApplicationRole>(viewModel);
            var permissoinsIdsToAddeRole = _permissionService.GetActualPermissions(ids);

            SetPermissionsToRole(role, permissoinsIdsToAddeRole);
        }

        #endregion AddRoleWithPermissions

        #region GetRoleByPermissions

        public Task<EditRoleViewModel> GetRoleByPermissions(string id)
        {
            var viewModel = _roles.AsNoTracking()
                    .ProjectTo<EditRoleViewModel>()
                    .FirstOrDefaultAsync(a => a.Id == id);

            return viewModel;
        }

        #endregion GetRoleByPermissions

        #region EditRoleWithPermissions

        public void EditRoleWithPermissions(EditRoleViewModel viewModel, params int[] ids)
        {
            var role = _mappingEngine.Map<ApplicationRole>(viewModel);
            var permissoinsIdsToAddeRole = _permissionService.GetActualPermissions(ids);
            SetPermissionsToRole(role, permissoinsIdsToAddeRole);
        }

        #endregion EditRoleWithPermissions

        #region AddRange

        public void AddRange(IEnumerable<ApplicationRole> roles)
        {
            _unitOfWork.AddThisRange(roles);
        }

        #endregion AddRange

        #region AnyAsync

        public Task<bool> AnyAsync()
        {
            return _roles.AnyAsync();
        }

        public bool Any()
        {
            return _roles.Any();
        }

        #endregion AnyAsync

        #region AutoCommitEnabled

        public bool AutoCommitEnabled { get; set; }

        #endregion AutoCommitEnabled

        #region ChechForExisByName

        public bool ChechForExisByName(string name, string id)
        {
            var roles = _roles.ToList();
            return id == null ? roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName() && id != a.Id);
        }

        #endregion ChechForExisByName

        #region GetPageList

        public IEnumerable<RoleViewModel> GetPageList(out int total, string term, int page, int count = 10)
        {
            var roles = _roles.AsNoTracking().OrderBy(a => a.Id).AsQueryable();
            if (!string.IsNullOrEmpty(term))
                roles = roles.Where(a => a.Name.Contains(term));
            total = roles.FutureCount();
            var result =
                roles.Skip((page - 1) * count).Take(count).ProjectTo<RoleViewModel>().Future().ToList();

            return result;
        }

        #endregion GetPageList

        #region RemoveById

        public async Task RemoveById(string id)
        {
            await _roles.Where(a => a.Id == id).DeleteAsync();
        }

        #endregion RemoveById

        #region CheckRoleIsSystemRoleAsync

        public async Task<bool> CheckRoleIsSystemRoleAsync(string id)
        {
            return await _roles.AnyAsync(a => a.Id == id && a.IsSystemRole);
        }

        #endregion CheckRoleIsSystemRoleAsync

        #region SetRoleAsRegistrationDefaultRoleAsync

        public async Task SetRoleAsRegistrationDefaultRoleAsync(string id)
        {
            _unitOfWork.EnableFiltering("IsDefaultRegisteRole");
            var role = await _roles.FirstOrDefaultAsync();
            role.IsActive = false;
            _unitOfWork.DisableFiltering("IsDefaultRegisteRole");
            await _roles.Where(a => a.Id == id).UpdateAsync(a => new ApplicationRole { IsDefaultForRegister = true });
        }

        #endregion SetRoleAsRegistrationDefaultRoleAsync

        #region GetAllAsSelectList

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectList()
        {
            _unitOfWork.EnableFiltering(RoleFilters.ActiveList);
            var roles = await _roles.AsNoTracking().ProjectTo<SelectListItem>().Cacheable().ToListAsync();
            return roles;
        }

        #endregion GetAllAsSelectList

        #region GetDefaultRoleForRegister

        public Task<string> GetDefaultRoleForRegister()
        {
            return _roles.Where(a => a.IsDefaultForRegister).Select(a => a.Name).FirstOrDefaultAsync();
        }

        #endregion GetDefaultRoleForRegister

        #region ChangeDefaultRegisterRole

        public void ChangeDefaultRegisterRole(string id)
        {
            _roles.Where(a => a.Id != id).Update(a => new ApplicationRole { IsDefaultForRegister = false });
        }

        #endregion ChangeDefaultRegisterRole
    }
}