using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using IncidentService.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IncidentService.Controllers
{
    public abstract class AbstractController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                var token = Request.Headers["Authorization"];

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                try
                {
                    var strUserId = jwtSecurityToken.Claims.First(claim => claim.Type == "userId").Value;
                    return Guid.Parse(strUserId);
                }
                catch (Exception e)
                {
                    throw new Exception("Claims not provided! " + e.Message);
                }
            }
        }
    }
}
