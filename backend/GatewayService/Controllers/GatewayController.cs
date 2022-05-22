using GatewayService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;

namespace GatewayService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Config _config;
        private readonly string backslash = "/";

        public GatewayController(IHttpClientFactory httpClientFactory, IConfiguration configuration, Config config)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _config = config;
        }

        [Authorize]
        [HttpGet("get/{path}")]
        public ActionResult Get([FromRoute] string path){
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                _httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":                    
                        var categories = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}{Request.QueryString}").Result;
                        return new ContentResult { Content = categories, ContentType = "application/json" };    

                    case "incidents":
                        var incidents = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}{Request.QueryString}").Result;
                        return new ContentResult { Content = incidents, ContentType = "application/json" };

                    case "users":
                        var users = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}{Request.QueryString}").Result;
                        return new ContentResult { Content = users, ContentType = "application/json" };

                    case "roles":
                        var roles = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}").Result;
                        return new ContentResult { Content = roles, ContentType = "application/json" };

                    case "permissions":
                        var permissions = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}").Result;
                        return new ContentResult { Content = permissions, ContentType = "application/json" };

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Ooops, something went wrong");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //[Authorize]
        [HttpGet("get/{path}/count")]
        public int GetCount([FromRoute] string path)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                //_httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":
                        var categories = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}/count").Result;
                        return Convert.ToInt32(categories);

                    case "incidents":
                        var incidents = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}/count").Result;
                        return Convert.ToInt32(incidents);

                    case "users":
                        var users = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}/count").Result;
                        return Convert.ToInt32(users);

                    default:
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //[Authorize]
        [HttpGet("get/{path}/{id}")]
        public ActionResult Get([FromRoute] string path, Guid id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                //_httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":

                        var category = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}").Result;
                        return new ContentResult { Content = category, ContentType = "application/json" };

                    case "incidents":
                        var incident = _httpClient.GetStringAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}").Result;
                        return new ContentResult { Content = incident, ContentType = "application/json" };

                    case "users":
                        var user = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        return new ContentResult { Content = user, ContentType = "application/json" };

                    case "roles":
                        var role = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        return new ContentResult { Content = role, ContentType = "application/json" };

                    case "permissions":
                        var permission = _httpClient.GetStringAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        return new ContentResult { Content = permission, ContentType = "application/json" };

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Ooops, something went wrong");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //[Authorize]
        [HttpDelete("delete/{path}/{id}")]
        public ActionResult Delete([FromRoute] string path, Guid id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                //_httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":
                        var category = _httpClient.DeleteAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}").Result;
                        if(category.IsSuccessStatusCode)
                        {
                            return Ok(category);
                        }
                        return NotFound();

                    case "incidents":
                        var incident = _httpClient.DeleteAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}").Result;
                        if(incident.IsSuccessStatusCode)
                        {
                            return Ok(incident);
                        }
                        return NotFound();
                    case "users":
                        var user = _httpClient.DeleteAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        if(user.IsSuccessStatusCode)
                        {
                            return Ok(user);
                        }
                        return NotFound();

                    case "roles":
                        var role = _httpClient.DeleteAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        if (role.IsSuccessStatusCode)
                        {
                            return Ok(role);
                        }
                        return NotFound();

                    case "permissions":
                        var permission = _httpClient.DeleteAsync($"{_config.GetUsersPath()}{path}{backslash}{id}").Result;
                        if (permission.IsSuccessStatusCode)
                        {
                            return Ok(permission);
                        }
                        return NotFound();

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Ooops, something went wrong");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        //[Authorize]
        [HttpPost("post/{path}")]
        public ActionResult Post([FromRoute] string path, [FromBody] object jsonObject)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                //_httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":
                        var categoriesContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var categoriesResult = _httpClient.PostAsync($"{_config.GetIncidentsPath()}{path}", categoriesContent).Result;
                        if (categoriesResult.IsSuccessStatusCode)
                        {
                            var jsonString = categoriesResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "incidents":
                        var incidentsContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var incidentsResult = _httpClient.PostAsync($"{_config.GetIncidentsPath()}{path}", incidentsContent).Result;
                        if (incidentsResult.IsSuccessStatusCode)
                        {
                            var jsonString = incidentsResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "users":
                        var usersContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var usersResult = _httpClient.PostAsync($"{_config.GetUsersPath()}{path}", usersContent).Result;
                        if (usersResult.IsSuccessStatusCode)
                        {
                            var jsonString = usersResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "roles":
                        var rolesContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var rolesResult = _httpClient.PostAsync($"{_config.GetUsersPath()}{path}", rolesContent).Result;
                        if (rolesResult.IsSuccessStatusCode)
                        {
                            var jsonString = rolesResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "permissions":
                        var permissionsContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var permissionsResult = _httpClient.PostAsync($"{_config.GetUsersPath()}{path}", permissionsContent).Result;
                        if (permissionsResult.IsSuccessStatusCode)
                        {
                            var jsonString = permissionsResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Ooops, something went wrong");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //[Authorize]
        [HttpPut("put/{path}/{id}")]
        public ActionResult Put([FromRoute] string path, [FromBody] object jsonObject, Guid id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(_config.GetApiKeyHeaderName(), _config.GetApiKey());
                //_httpClient.DefaultRequestHeaders.Add(_config.GetAuthorization(), Request.Headers[_config.GetAuthorization()].ToString());

                switch (path)
                {
                    case "categories":
                        var categoriesContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var categoriesResult = _httpClient.PutAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}", categoriesContent).Result;
                        if (categoriesResult.IsSuccessStatusCode)
                        {
                            var jsonString = categoriesResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "incidents":
                        var incidentsContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var incidentsResult = _httpClient.PutAsync($"{_config.GetIncidentsPath()}{path}{backslash}{id}", incidentsContent).Result;
                        if (incidentsResult.IsSuccessStatusCode)
                        {
                            var jsonString = incidentsResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "users":
                        var usersContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var usersResult = _httpClient.PutAsync($"{_config.GetUsersPath()}{path}{backslash}{id}", usersContent).Result;
                        if (usersResult.IsSuccessStatusCode)
                        {
                            var jsonString = usersResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "roles":
                        var rolesContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var rolesResult = _httpClient.PutAsync($"{_config.GetUsersPath()}{path}{backslash}{id}", rolesContent).Result;
                        if (rolesResult.IsSuccessStatusCode)
                        {
                            var jsonString = rolesResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    case "permissions":
                        var permissionsContent = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                        var permissionsResult = _httpClient.PutAsync($"{_config.GetUsersPath()}{path}{backslash}{id}", permissionsContent).Result;
                        if (permissionsResult.IsSuccessStatusCode)
                        {
                            var jsonString = permissionsResult.Content.ReadAsStringAsync().Result;
                            return new ContentResult { Content = jsonString, ContentType = "application/json" };
                        }
                        return BadRequest();

                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Ooops, something went wrong");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("Login/{path}")]
        public ActionResult Login([FromRoute] string path, [FromBody] object jsonObject)
        {
            try
            {
                var userService = _configuration["Services:UserServicePath"].ToString();

                if (userService != null && path == "login")
                {
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var result = _httpClient.PostAsync(userService + path, content).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;
                        return Ok(jsonString);
                    }
                    return BadRequest();
                }
                return null;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    } 
}
