using IncidentService.Models.Services;

namespace IncidentService.Microservices
{
    public interface IServiceCall
    {
        UserDto SendGetRequest(string url, string token);
    }
}
