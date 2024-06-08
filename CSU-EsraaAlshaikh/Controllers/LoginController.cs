using CSU_Core.Models;
using CSU_Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSU_EsraaAlshaikh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly I_LoginServicecs _loginService;
        public LoginController(I_LoginServicecs loginServicecs)
        {
            _loginService = loginServicecs;
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Login([FromBody] Login userLogin)
        {
            var token = _loginService.UserLogin(userLogin);
            if (token == null)
            {
                return Unauthorized(new {Message = "User is unauthorized" });
            }
            else
            {
                return Ok(new { Message = "User retrieved successfully.", Token = $"{token}" });
            }
        }
    }
}
