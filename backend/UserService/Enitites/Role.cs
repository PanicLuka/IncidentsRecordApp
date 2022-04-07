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
        
        #endregion
    }
}
