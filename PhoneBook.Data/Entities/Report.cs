using PhoneBook.Data.Entities.Base;
using PhoneBook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Entities
{
    public class Report : BaseEntity<int>, IAuditable
    {
        public string FilePath { get; set; }
        public ReportStatusType ReportStatusType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
