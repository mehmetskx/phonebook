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
using Microsoft.EntityFrameworkCore;

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
                var affected = await _unitOfWork.CommitAsync();

                _logger.LogInformation($"Person added {createdUser}");

                response.Data = _mapper.Map<PersonDto>(createdUser);
                response.TotalRowCount = affected;
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

        public async Task<ResponseModel<PersonDto>> DeletePerson(int id)
        {
            var response = new ResponseModel<PersonDto>();
            try
            {
                _logger.LogInformation($"PhoneBook.Services.PersonService => public async Task<ResponseModel<PersonDto>> DeletePerson(int id) = {id}");

                var findPersonById = await _unitOfWork.PersonRepository.GetAsync(x => x.Id == id);

                if (findPersonById is null)
                    throw new Exception("Person is not find.");

                findPersonById.IsActive = false;
                findPersonById.ModifiedDate = DateTime.Now;

                await _unitOfWork.PersonRepository.UpdateAsync(findPersonById);
                var affected = await _unitOfWork.CommitAsync();

                _logger.LogInformation($"Person is removed {findPersonById}");

                response.Data = _mapper.Map<PersonDto>(findPersonById);
                response.TotalRowCount = affected;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while removing person.");
                ex.LogException(_logger, ex.Message);

                response.ErrorList.Add(new Error
                {
                    Description = ex.Message
                });

                return response;
            }
        }

        public async Task<ResponseModel<PersonDto>> GetPersonByIdWithContactInfos(int id)
        {
            var response = new ResponseModel<PersonDto>();
            try
            {
                _logger.LogInformation($"PhoneBook.Services.PersonService => Task<ResponseModel<List<PersonDto>>> GetPersonByIdWithContactInfos(int id)");

                var getPerson = await _unitOfWork.PersonRepository.TableNoTracking.Include(x => x.Contacts).ThenInclude(x => x.ContactType).FirstOrDefaultAsync(x => x.Id == id);

                _logger.LogInformation($"Persons list retrived {getPerson}");

                if (getPerson == null)
                    return response;

                response.Data = _mapper.Map<PersonDto>(getPerson);
                response.TotalRowCount = 1;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while getting persons.");
                ex.LogException(_logger, ex.Message);

                response.ErrorList.Add(new Error
                {
                    Description = ex.Message
                });

                return response;
            }
        }

        public async Task<ResponseModel<List<PersonDto>>> GetPersons()
        {
            var response = new ResponseModel<List<PersonDto>>();
            try
            {
                _logger.LogInformation($"PhoneBook.Services.PersonService => public async Task<ResponseModel<PersonDto>> GetPersons()");

                var getAllPersons = await _unitOfWork.PersonRepository.GetAllAsync(x => x.IsActive);

                _logger.LogInformation($"Persons list retrived {getAllPersons}");

                if (getAllPersons.Count == 0)
                    return response;

                response.Data = _mapper.Map<List<PersonDto>>(getAllPersons);
                response.TotalRowCount = getAllPersons.Count;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while getting persons.");
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
