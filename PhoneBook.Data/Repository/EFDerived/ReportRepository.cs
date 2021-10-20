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
    public class ReportRepository : EFCoreRepository<Report, int>, IReportRepository
    {
        public ReportRepository(DbContext context) : base(context)
        {
        }
    }
}
