using AradCms.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AradCms.Core.IService
{
    public interface IPermissionService
    {
        void Add(ApplicationPermission permission);

        void AddRange(IEnumerable<ApplicationPermission> permissions);

        Task RemoveAll();

        IEnumerable<ApplicationPermission> GetActualPermissions(params int[] ids);

        Task<IList<ApplicationPermission>> GetAllPermissions();

        Task<IEnumerable<SelectListItem>> GetAsSelectList();

        Task<int[]> GetPermissionsWithIds(params int[] ids);

        bool IsAnyPermissionInDb();

        bool AutoCommitEnabled { get; set; }

        ApplicationPermission GetByName(string name);

        IEnumerable<ApplicationPermission> GetActualPermissions(List<ApplicationPermission> permissions);

        Task<IList<string>> GetPermissionByRoleIdsAync(string[] roleIds);

        IList<string> GetUserPermissions(string[] roleIds, string userId);

        void SeedDatabase(IEnumerable<ApplicationPermission> permissions);

        bool HasDirectAccess(string userId, string areaName, string controllerName, string[] actions);

        Task<IList<int>> GetPermissionIdsByRoleId(string roleId);

        IList<int> GetPermissionsWithUserId(string userId);
    }
}