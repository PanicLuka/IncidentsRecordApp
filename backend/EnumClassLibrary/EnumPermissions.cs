
namespace EnumClassLibrary
{
    
    public class EnumPermissions
    {
        public enum Permissions 
        {
            UserGetAll = 0,
            UserDelete = 1,
            UserUpdate = 2,
            UserGetById = 3,
            UserCreateUser = 4,
            IncidentsGetAll = 5,
            IncidentsGetById = 6,
            IncidentsUpdate = 7,
            IncidentsDelete = 8,
            IncidentsCreate = 9,
            PromoteToAdmin = 10
        }
    }
}
