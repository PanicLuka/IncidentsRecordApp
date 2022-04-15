using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Enitites
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string UserType { get; set; }
      
    }
}
