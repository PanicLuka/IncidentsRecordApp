using System;

namespace UserService.Enitites
{
    public class UserPermission
    {
        #region
        public Guid? UserId { get; set; }
        public User user { get; set; }
        public Guid? PermissionId { get; set; }
        public Permission permission { get; set; }
        #endregion
    }
}
