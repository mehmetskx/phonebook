using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual PersonDto Person { get; set; }
        public virtual ContactTypeDto ContactType { get; set; }
    }
}
