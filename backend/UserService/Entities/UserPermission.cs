﻿using System;

namespace UserService.Enitites
{
    public class UserPermission
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Guid? PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
