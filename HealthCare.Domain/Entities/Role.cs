using System;
using System.Collections.Generic;

namespace HealthCare.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        // Optional: define role code or system name
        public string Code { get; set; }
    }
}
