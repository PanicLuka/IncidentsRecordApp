using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/register")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterRepository repository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        
        public RegisterController(IRegisterRepository repository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.repository = repository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<List<RegisterDto>>> GetUsersAsync()
        {
            var users = await repository.GetAllUsersAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            List<RegisterDto> userDtos = new List<RegisterDto>();
            
            foreach(var user in users)
            {
                RegisterDto registerDto = mapper.Map<RegisterDto>(user);

                userDtos.Add(registerDto);
            }

            return Ok(userDtos);
        }

    }
}
