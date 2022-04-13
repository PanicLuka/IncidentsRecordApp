using IncidentService.Models.ServicesHelper;

namespace IncidentService.Microservices
{
    public interface IServiceCall
    {
        UserDto SendGetRequest(string url, string token);
    }
}

