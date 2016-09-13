namespace AradCms.Core.ViewModel.Role
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description
        { get; set; }

        public virtual bool IsActive { get; set; }
        public bool IsSystemRole { get; set; }
        public bool IsDefaultForRegister { get; set; }
    }
}