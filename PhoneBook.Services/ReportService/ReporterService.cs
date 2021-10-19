using PhoneBook.Models;
using PhoneBook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.ReportService
{
    public class ReporterService : IReportService
    {
        public Task<ResponseModel<ReportStatusType>> CreateReportRequest()
        {
            throw new NotImplementedException();
        }
    }
}
