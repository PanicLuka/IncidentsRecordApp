using System;

namespace EnumClassLibrary
{
    
    public class EnumPermissions
    {
        public enum Permissions 
        {
            CanAccessGetAllUsers = 0,
            CanAccessDeleteUser = 1,
            CanAccessUpdateUser = 2,
            CanAccessGetByIdUser = 3,
            CanAccessCreateUser = 4,
            CanAccessGetAllIncidents = 5,
            CanAccessGetIncidentById = 6,
            CanAccessUpdateIncident = 7,
            CanAccessDeleteIncident = 8,
            CanAccessCreateIncident = 9,
            CanPromoteToAdmin = 10
        }
    }
}
