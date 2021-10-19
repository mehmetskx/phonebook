using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Data.Entities;
using PhoneBook.Utils;

namespace PhoneBook.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IMapper mapper, ILogger<PersonService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<PersonDto>> AddPerson(PersonDto person)
        {
            var response = new ResponseModel<PersonDto>();
            try
            {
                _logger.LogInformation($"PhoneBook.Services.PersonService => public async Task<ResponseModel<PersonDto>> AddPerson(PersonDto person) data parameter = {person}");

                var personEntity = _mapper.Map<Person>(person);
                personEntity.CreatedDate = DateTime.Now;
                personEntity.IsActive = true;

                var createdUser = await _unitOfWork.PersonRepository.AddAsync(personEntity);
                var fetchCount = await _unitOfWork.CommitAsync();

                _logger.LogInformation($"Person added {createdUser}");

                response.Data = _mapper.Map<PersonDto>(createdUser);
                response.TotalRowCount = fetchCount;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding person.");
                ex.LogException(_logger, ex.Message);

                response.ErrorList.Add(new Error
                {
                    Description = ex.Message
                });

                return response;
            }
        }
    }
}
