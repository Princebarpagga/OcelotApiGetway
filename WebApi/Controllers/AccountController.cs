using JwtAuthenticationManger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }
        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authentication([FromBody]AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse =_jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
          
        }
      
    }
}
