using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AradCms.Core.Model
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual bool IsSystemRole { get; set; }
        public virtual bool IsDefaultForRegister { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual ICollection<ApplicationPermission> Permissions { get; set; }

        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}