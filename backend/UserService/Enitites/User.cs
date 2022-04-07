﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserService.Enitites
{
    public class User
    {
        #region
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
