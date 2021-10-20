using PhoneBook.Data.Entities;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils.MQHelper
{
    public interface IRabbitMqHelper
    {
        Task<ResponseModel<bool>> AddToQueue(Report report);
    }
}
