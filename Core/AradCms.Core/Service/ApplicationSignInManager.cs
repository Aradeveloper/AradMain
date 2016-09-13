using AradCms.Core.IService;
using AradCms.Core.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AradCms.Core.Service
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>, IApplicationSignInManager
    {
        #region Fields

        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        #endregion Fields

        #region Constructor

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        #endregion Constructor
    }
}