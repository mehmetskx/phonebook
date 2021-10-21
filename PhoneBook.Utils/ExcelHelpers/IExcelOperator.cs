using PhoneBook.Data.Entities;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils.ExcelHelpers
{
    public interface IExcelOperator
    {
        Task<ResponseModel<string>> SaveToFile(int reportId, List<Contact> contacts);
    }
}
