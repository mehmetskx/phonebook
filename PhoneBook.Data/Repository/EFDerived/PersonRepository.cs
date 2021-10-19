using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Entities;
using PhoneBook.Data.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository.EFDerived
{
    public class PersonRepository : EFCoreRepository<Person, int>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
