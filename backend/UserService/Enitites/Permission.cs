using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Enitites
{
    public class Permission
    {
        [Key]
        public Guid PermissionId { get; set; }

        public string AccessPermissions { get; set; }


    }
}
