using AutoMapper;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Entities;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.Utils;
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
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IMapper mapper, ILogger<ContactService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<ContactDto>> Add(ContactDto contact)
        {
            var response = new ResponseModel<ContactDto>();
            try
            {
                _logger.LogInformation($"PhoneBook.Services.ContactService => public async Task<ResponseModel<ContactDto>> Add(ContactDto contact) = {contact}");

                //TODO: fluent validation
                var findContactType = await _unitOfWork.ContactTypeRepository.GetAsync(x => x.Id == contact.ContactTypeId);

                if (findContactType is null)
                    throw new Exception("Contact type is not registered.");

                var contactEntity = _mapper.Map<Contact>(contact);

                contactEntity.CreatedDate = DateTime.Now;
                contactEntity.IsActive = true;

                await _unitOfWork.ContactRepository.AddAsync(contactEntity);
                var affected = await _unitOfWork.CommitAsync();

                _logger.LogInformation($"Contact is added {contactEntity}");

                response.Data = _mapper.Map<ContactDto>(contactEntity);
                response.TotalRowCount = affected;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding contact.");
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
