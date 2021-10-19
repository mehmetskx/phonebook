using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.PersonService
{
    public interface IPersonService
    {
        Task<ResponseModel<PersonDto>> AddPerson(PersonDto person);
    }
}
