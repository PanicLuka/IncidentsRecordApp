using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Models
{
    public class UserDto
    {
        #region
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        
        #endregion
    }
}
