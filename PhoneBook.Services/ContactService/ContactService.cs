using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IMapper mapper, ILogger<ContactService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

    }
}
