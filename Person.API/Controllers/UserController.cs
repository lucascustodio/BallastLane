using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Application.Commands;
using Person.Application.Services.Interfaces;

namespace Person.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var response = await _userService.GetAll();

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand user)
        {
            var createdUser = await _userService.Create(user);
            return Ok(createdUser);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateUserCommand user)
        {
            var updateUser = await _userService.Update(user);

            if (updateUser == null)
            {
                return NotFound();
            }

            return Ok(updateUser);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedUser = await _userService.Delete(id);

            if (!deletedUser.IsSuccess)
            {
                return BadRequest(deletedUser.Error);
            }

            return Ok(deletedUser);
        }
    }
}
