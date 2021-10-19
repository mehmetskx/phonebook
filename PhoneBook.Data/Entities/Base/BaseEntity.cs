using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Entities.Base
{
    public class BaseEntity <T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
    }
}
