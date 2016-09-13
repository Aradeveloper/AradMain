using AradCms.Core.Model;
using AradCms.Core.ViewModel.Role;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AradCms.Core.IService
{
    public interface IApplicationRoleManager : IDisposable
    {
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<ApplicationRole> RoleValidator { get; set; }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(ApplicationRole role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(ApplicationRole role);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(ApplicationRole role);

        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<ApplicationRole> FindByIdAsync(string roleId);

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<ApplicationRole> FindByNameAsync(string roleName);

        // Our new custom methods

        ApplicationRole FindRoleByName(string roleName);

        IdentityResult CreateRole(ApplicationRole role);

        IList<ApplicationUserRole> GetUsersOfRole(string roleName);

        IList<ApplicationUser> GetApplicationUsersInRole(string roleName);

        IList<string> FindUserRoles(string userId);

        string[] GetRolesForUser(string userId);

        bool IsUserInRole(string userId, string roleName);

        Task<IList<string>> GetPermissionsOfUser(string userId);

        IList<string> GetPermissionsOfRole(string roleName);

        Task<IList<ApplicationRole>> GetAllApplicationRolesAsync();

        void SetPermissionsToRole(ApplicationRole role, IEnumerable<ApplicationPermission> permissions);

        void SeedDatabase(IEnumerable<ApplicationPermission> permissions);

        Task RemoteAll();

        Task<IEnumerable<RoleViewModel>> GetList();

        void AddRoleWithPermissions(AddRoleViewModel viewModel, params int[] ids);

        void EditRoleWithPermissions(EditRoleViewModel viewModel, params int[] ids);

        Task<EditRoleViewModel> GetRoleByPermissions(string id);

        void AddRange(IEnumerable<ApplicationRole> roles);

        Task<bool> AnyAsync();

        bool AutoCommitEnabled { get; set; }

        bool Any();

        bool ChechForExisByName(string name, string id);

        Task RemoveById(string id);

        Task<bool> CheckRoleIsSystemRoleAsync(string id);

        Task SetRoleAsRegistrationDefaultRoleAsync(string id);

        IEnumerable<RoleViewModel> GetPageList(out int total, string term, int page, int count = 10);

        Task<IEnumerable<SelectListItem>> GetAllAsSelectList();

        IList<string> FindUserRoleIds(string userId);

        Task<string> GetDefaultRoleForRegister();

        void ChangeDefaultRegisterRole(string id);

        Task<IList<string>> FindUserRoleIdsAsync(string userId);
    }
}