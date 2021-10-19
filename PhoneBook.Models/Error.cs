using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Error
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Parameter { get; set; }

        public string CodeString()
        {
            return Code.ToString();
        }
    }
}
