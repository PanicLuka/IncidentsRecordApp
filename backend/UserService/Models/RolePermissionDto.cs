using System;

namespace UserService.Models
{
    public class RolePermissionDto
    {
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
