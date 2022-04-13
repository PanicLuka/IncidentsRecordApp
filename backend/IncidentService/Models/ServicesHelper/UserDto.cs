using System;

namespace IncidentService.Models.ServicesHelper
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
