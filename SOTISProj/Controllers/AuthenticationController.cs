

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SOTISProj.DTO;
using SOTISProj.SeriveInterfaces;

namespace SOTISProj.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationServicee _service;
        public AuthenticationController(IAuthenticationServicee service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public ActionResult<AuthenticationTokensDTO> login(CredentialsDTO credentials)
        {
            return Ok(_service.Login(credentials).Value);
        }

    }
}
