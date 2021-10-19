using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PhoneBook.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IMapper mapper, ILogger<PersonService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }


    }
}
