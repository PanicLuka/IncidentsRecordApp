﻿using System;

namespace UserService.Enitites
{
    public class RolePermission
    {
        #region
        public Guid? RoleId { get; set; }
        public Role role { get; set; }
        public Guid? PermissionId { get; set; }
        public Permission permission { get; set; }
        #endregion
    }
}
