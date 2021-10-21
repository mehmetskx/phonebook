using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.PersonOperationService.Models;
using PhoneBook.Services.PersonService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.PersonOperationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonService _personService;

        public PersonsController(ILogger<PersonsController> logger,
            IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _personService.GetPersons());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _personService.GetPersonByIdWithContactInfos(id);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PersonDto person)
        {
            var response = await _personService.AddPerson(person);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Created(string.Empty, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _personService.DeletePerson(id);

            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }
    }
}
