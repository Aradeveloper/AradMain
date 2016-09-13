using System.Collections.Generic;

namespace AradCms.Core.ViewModel.User
{
    public class UserListViewModel
    {
        public IList<UserViewModel> Users { get; set; }
        public UserSearchViewModel UserSearchViewModel { get; set; }
    }
}