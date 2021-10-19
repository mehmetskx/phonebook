using PhoneBook.Data.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IPersonRepository PersonRepository { get; }
        IContactRepository ContactRepository { get; }
        IContactTypeRepository ContactTypeRepository { get; }
        Task<int> CommitAsync();
    }
}
