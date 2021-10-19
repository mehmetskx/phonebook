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
    [Route("api/Persons")]
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
        public async Task<ResponseModel<PersonDto>> GetAll()
        {
            return default(ResponseModel<PersonDto>);
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<PersonDto>> GetById(int id)
        {
            return default(ResponseModel<PersonDto>);
        }

        [HttpPost]
        public async Task<ResponseModel<PersonDto>> Add([FromBody] PersonDto person)
        {
            return await _personService.AddPerson(person);
        }
    }
}
