using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IncidentService.Models.ServicesHelper;
using Newtonsoft.Json;

namespace IncidentService.Microservices
{
    public class ServiceCall : IServiceCall
    {
        public UserDto SendGetRequest(string url, string token)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = httpClient.Send(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ToString();
                    if (string.IsNullOrEmpty(content.ToString()))
                    {
                        return default;
                    }

                    return (UserDto)JsonConvert.DeserializeObject(content.ToString());
                }
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
