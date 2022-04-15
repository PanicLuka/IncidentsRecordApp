using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Enitites
{
    public class Permission
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string AccessPermission { get; set; }

    }
}
