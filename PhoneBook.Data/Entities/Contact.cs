using PhoneBook.Data.Entities.Base;
using System;

namespace PhoneBook.Data.Entities
{
    public class Contact : BaseEntity<int>, IAuditable
    {
        public int ContactTypeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual ContactType ContactType { get; set; }
    }
}
