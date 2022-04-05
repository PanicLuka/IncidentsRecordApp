using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
