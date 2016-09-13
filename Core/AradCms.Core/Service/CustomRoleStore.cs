using AradCms.Core.IService;
using AradCms.Core.Model;
using Microsoft.AspNet.Identity;

namespace AradCms.Core.Service
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<ApplicationRole, string> _roleStore;

        public CustomRoleStore(IRoleStore<ApplicationRole, string> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}