using PhoneBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository.Infrastructure
{
    public interface IContactRepository : IRepository<Contact>
    {
    }
}
