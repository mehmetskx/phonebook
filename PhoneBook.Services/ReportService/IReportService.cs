using PhoneBook.Data.Entities;
using PhoneBook.Models;
using PhoneBook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.ReportService
{
    public interface IReportService
    {
        Task<ResponseModel<ReportStatusType>> CreateReportRequest();
        Task<ResponseModel<Report>> ConsumeRedisQueue();
    }
}
