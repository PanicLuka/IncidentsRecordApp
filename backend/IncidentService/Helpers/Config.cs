using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IncidentService.Helpers
{
    public class Config
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetAuthorization()
        {
            var strAuthorization = _configuration["Authorization:Authorization"].ToString();
            return strAuthorization;
        }
    }
}