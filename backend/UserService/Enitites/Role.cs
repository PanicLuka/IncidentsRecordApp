using System;
using System.ComponentModel.DataAnnotations;


namespace UserService.Enitites
{
    
    
    public class Role
    {
        #region
        [Key]
        public Guid RoleId { get; set; }

        public string UserType { get; set; }

        public Guid PermissionId { get; set; }

        public Permission permission { get; set; }

        #endregion
    }
}
