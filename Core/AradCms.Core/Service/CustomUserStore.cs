using AradCms.Core.IService;
using AradCms.Core.Model;
using Microsoft.AspNet.Identity;

namespace AradCms.Core.Service
{
    public class CustomUserStore : ICustomUserStore
    {
        private readonly IUserStore<ApplicationUser, string> _userStore;

        public CustomUserStore(IUserStore<ApplicationUser, string> userStore)
        {
            _userStore = userStore;
        }
    }
}