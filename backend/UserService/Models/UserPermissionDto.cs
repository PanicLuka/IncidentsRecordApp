
using System;

namespace UserService.Models
{
    public class UserPermissionDto
    {
        public Guid? UserId { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
