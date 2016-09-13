using AradCms.Core.Context;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AradCms.Core.Service
{
    public class PermissionService : IPermissionService
    {
        #region Fields

        private readonly HttpContextBase _httpContextBase;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ApplicationPermission> _permissions;
        #endregion Fields

        #region Ctor

        public PermissionService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IMapper mappingEngine)
        {
            AutoCommitEnabled = true;
            _unitOfWork = unitOfWork;
            _permissions = _unitOfWork.Set<ApplicationPermission>();
            _mappingEngine = mappingEngine;
            _httpContextBase = httpContextBase;
        }

        #endregion Ctor

        #region Add

        public void Add(ApplicationPermission permission)
        {
            _permissions.Add(permission);
        }

        #endregion Add

        #region

        public void AddRange(IEnumerable<ApplicationPermission> permissions)
        {
            _unitOfWork.AddThisRange(permissions);
        }

        #endregion

        #region RemoveAll

        public async Task RemoveAll()
        {
            await _permissions.DeleteAsync();
        }

        #endregion

        #region GetActualPermissions

        /// <summary>
        /// get all permissions that it's id in ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<ApplicationPermission> GetActualPermissions(params int[] ids)
        {
            return _permissions
                    .Where(a => ids.Contains(a.Id))
                    .ToList();
        }

        #endregion

        #region GetAllPermissions

        public async Task<IList<ApplicationPermission>> GetAllPermissions()
        {
            return await _permissions.AsNoTracking().ToListAsync();
        }

        #endregion

        #region GetAsSelectList

        public async Task<IEnumerable<SelectListItem>> GetAsSelectList()
        {
            var items =
                await
                    _permissions.ProjectTo<SelectListItem>()
                        .AsNoTracking().Cacheable()
                        .ToListAsync();

            return items;
        }

        #endregion

        #region GetPermissionsWithIds

        /// <summary>
        /// get all permissionIds that it's id in ids or it's parentIs in ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int[]> GetPermissionsWithIds(params int[] ids)
        {
            var permissionsIds = await _permissions.AsNoTracking()
                    .Where(a => ids.Contains(a.Id)).Select(a => a.Id)
                    .ToListAsync();
            return permissionsIds.ToArray();
        }

        #endregion

        #region IsAnyPermissionInDb

        public bool IsAnyPermissionInDb()
        {
            return _permissions.Any();
        }

        #endregion

        #region AutoCommitEnabled
        public bool AutoCommitEnabled { get; set; }

        #endregion

        #region GetByName

        public ApplicationPermission GetByName(string name)
        {
            return _permissions.FirstOrDefault(a => a.PName == name);
        }

        #endregion

        #region GetActualPermissions

        public IEnumerable<ApplicationPermission> GetActualPermissions(List<ApplicationPermission> permissions)
        {
            var permissionNames = permissions.Select(a => a.PName).ToArray();
            var inDbPermissions = _permissions.Where(a => permissionNames.Any(p => p == a.PName)).ToList();
            var result = new List<ApplicationPermission>(inDbPermissions);
            var noInDbPermissions = permissions.Where(a => inDbPermissions.All(p => p.PName != a.PName)).ToList();
            result.AddRange(noInDbPermissions);
            return result;
        }

        #endregion

        #region GetUserPermissions,GetPermissionByRoleIdsAsync

        public async Task<IList<string>> GetPermissionByRoleIdsAync(string[] roleIds)
        {
            return await (from p in _permissions
                          from r in p.ApplicationRoles
                          where roleIds.Contains(r.Id)
                          select p.PName).ToListAsync();
        }

        public IList<string> GetUserPermissions(string[] roleIds, string userId)
        {
            var permissionsOfRoles = (from p in _permissions
                                      from r in p.ApplicationRoles
                                      where roleIds.Contains(r.Id)
                                      select p.PName).Future();

            var permissionsOfUser = (from p in _permissions
                                     from r in p.AssignedUsers
                                     where userId == r.Id
                                     select p.PName).Future().ToList();

            var result = permissionsOfUser.Union(permissionsOfRoles).ToList();
            return result;
        }

        #endregion

        #region SeedDatabase

        public void SeedDatabase(IEnumerable<ApplicationPermission> permissions)
        {
            var applicationPermissions = permissions as IList<ApplicationPermission> ?? permissions.ToList();

            foreach (var permission in applicationPermissions)
            {
                _permissions.AddOrUpdate(
                    x => new { x.PName, x.AreaName, x.ControllerName, x.ActionName }, permission);
            }
            _unitOfWork.SaveAllChanges();
        }

        #endregion

        #region HasDirectAccess

        public bool HasDirectAccess(string userId, string areaName, string controllerName,
         string[] dependencyActionNames)
        {
            var directAccessWithOwnPermissions =
                _permissions.Include(a => a.AssignedUsers).AsNoTracking().Any(
                    p =>
                        p.AssignedUsers.Any(u => u.Id == userId) & dependencyActionNames.Contains(p.ActionName) &
                        p.ControllerName == controllerName & p.AreaName == areaName);

            return directAccessWithOwnPermissions;
        }

        #endregion

        #region GetPermissionsByRoleId

        public async Task<IList<string>> GetPermissionsByRoleId(string roleId)
        {
            return await (from p in _permissions
                          from r in p.ApplicationRoles
                          where r.Id == roleId
                          select p.PName).ToListAsync();
        }

        #endregion

        #region GetPermissionIdsByRoleId

        public async Task<IList<int>> GetPermissionIdsByRoleId(string roleId)
        {
            return await (from p in _permissions
                          from r in p.ApplicationRoles
                          where r.Id == roleId
                          select p.Id).ToListAsync();
        }

        #endregion

        #region GetPermissionsWithUserId

        public IList<int> GetPermissionsWithUserId(string userId)
        {
            return (from p in _permissions
                    from r in p.AssignedUsers
                    where userId == r.Id
                    select p.Id).ToList();
        }

        #endregion
    }
}