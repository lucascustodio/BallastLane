using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Application.Commands;
using Person.Application.Services.Interfaces;

namespace Restaurant.Person.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var response = await _personService.GetAll();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var user = await _personService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand person)
        {
            var createdPerson = await _personService.Create(person);

            return Ok(createdPerson);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdatePersonCommand person)
        {
            var updatedPerson = await _personService.Update(person);

            if (updatedPerson == null)
            {
                return NotFound();
            }

            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedPerson = await _personService.Delete(id);

            if (!deletedPerson.IsSuccess)
            {
                return BadRequest(deletedPerson.Error);
            }

            return Ok(deletedPerson);
        }
    }
}
