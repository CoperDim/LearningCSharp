using GuessMyAge.Business.Services;
using GuessMyAge.Database.Entities;
using GuessMyAge.Database.Repositories;
using GuessMyAge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GuessMyAge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("")]
        public IEnumerable<Person> GetAll()
        {
            return _personService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Person> GetById([FromRoute] int id)
        {
            return _personService.GetById(id);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] Person person)
        {
            _personService.Create(person);

            return Ok();
        }

        [HttpPut("")]
        public IActionResult Update([FromBody] Person person)
        {
            _personService.Update(person);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _personService.Delete(id);

            return Ok();
        }
    }
}
