namespace AradCms.Core.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsSystemAccount { get; set; }
        public string Name { get; set; }
        public string RegisterDate { get; set; }
        public string LastActivityDate { get; set; }
        public string Roles { get; set; }
    }
}