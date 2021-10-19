using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.Services.ContactService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.PersonOperationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactService _contactService;

        public ContactsController(ILogger<ContactsController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ResponseModel<ContactDto>> Add([FromBody] ContactDto contact)
        {
            return await _contactService.Add(contact);
        }

        [HttpDelete("{contactId}")]
        public async Task<ResponseModel<ContactDto>> Remove( int contactId)
        {
            return await _contactService.Remove(contactId);
        }
    }
}
