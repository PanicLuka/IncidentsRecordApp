using System;

namespace UserService.Enitites
{
    public class RolePermission
    {
        #region
        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
        public Guid? PermissionId { get; set; }
        public Permission Permission { get; set; }
        #endregion
    }
}
