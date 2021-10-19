﻿using PhoneBook.Data.Context;
using PhoneBook.Data.Repository.EFDerived;
using PhoneBook.Data.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneBookContext _context;
        private PersonRepository _personRepository;
        private ContactRepository _contactRepository;

        public UnitOfWork(PhoneBookContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository => _personRepository = _personRepository ?? new PersonRepository(_context);

        public IContactRepository ContactRepository => _contactRepository = _contactRepository ?? new ContactRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}