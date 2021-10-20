using PhoneBook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public ReportStatusType ReportStatusType { get; set; }
    }
}
