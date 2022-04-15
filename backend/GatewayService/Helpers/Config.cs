using Microsoft.Extensions.Configuration;

namespace GatewayService.Helpers
{
    public class Config
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetIncidentsPath()
        {
            var incidentService = _configuration.GetValue<string>("Services:IncidentServicePath");
            return incidentService;
        }

        public string GetUsersPath()
        {
            var userService = _configuration.GetValue<string>("Services:UserServicePath");
            return userService;
        }

        public string GetApiKeyHeaderName()
        {
            var apiKeyHeaderName = _configuration["HeaderKey:HeaderName"].ToString();
            return apiKeyHeaderName;
        }

        public string GetApiKey()
        {
            var apiKey = _configuration["APIkey:Key"].ToString();
            return apiKey;
        }

        public string GetAuthorization()
        {
            var authorizationName = _configuration["Authorization:Authorization"].ToString();
            return authorizationName;
        }
    }
}
