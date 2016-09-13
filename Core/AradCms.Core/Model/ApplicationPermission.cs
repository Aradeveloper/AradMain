using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AradCms.Core.Model
{
    public class ApplicationPermission
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Description { get; set; }
        public virtual string PName { get; set; }
        public virtual string ActionName { get; set; }
        public virtual string ControllerName { get; set; }
        public virtual string AreaName { get; set; }
        public virtual bool IsMenu { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUser> AssignedUsers { get; set; }
    }
}