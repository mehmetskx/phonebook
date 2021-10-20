using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils.ExcelHelpers
{
    public interface IExcelOperator
    {
        Task<ResponseModel<string>> SaveToFile(int reportId);
    }
}
