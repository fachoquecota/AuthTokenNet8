using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginTokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginTokenController(IAuthService authService) 
        { 
            _authService = authService;
        }

        [HttpPost]
        [Route("GetTokenLogin")]
        public IActionResult GetToken([FromBody] LoginModel loginModel)
        {
            var (success, message, token) = _authService.Authenticate(loginModel);
            if (!success) { 
                return Unauthorized(new {message});
            }

            return Ok(new 
            { 
                Result = message,
                Token = token      
            });
        }
    }
}
