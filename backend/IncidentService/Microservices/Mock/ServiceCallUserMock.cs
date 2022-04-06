using IncidentService.Models.Services;

namespace IncidentService.Microservices.Mock
{
    public class ServiceCallUserMock
    {
        public UserDto SendGetRequest(string url, string token)
        {
            var user = new UserDto
            {

            };

            return user;
        }
    }
}
