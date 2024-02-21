using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Application.Commands;
using Person.Application.Services.Interfaces;

namespace Person.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _loginService.Login(username, password);
            if (user == null)
            {
                return NotFound("Invalid username or password");
            }
            return Ok(user);
        }
    }
}
