using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Entities;
using PhoneBook.Data.Repository.Infrastructure;

namespace PhoneBook.Data.Repository.EFDerived
{
    public class ContactTypeRepository : EFCoreRepository<ContactType, int>, IContactTypeRepository
    {
        public ContactTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
