using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.ContactService
{
    public interface IContactService
    {
        Task<ResponseModel<ContactDto>> Add(ContactDto contact);
        Task<ResponseModel<ContactDto>> Remove(int contactId);
    }
}
