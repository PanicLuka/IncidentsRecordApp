using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Enitites
{
    public class Permission
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string AccessPermissions { get; set; }

    }
}
