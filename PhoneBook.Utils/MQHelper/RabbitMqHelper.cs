using PhoneBook.Data.Entities;
using PhoneBook.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.Utils.MQHelper
{
    public class RabbitMqHelper : IRabbitMqHelper
    {
        public async Task<ResponseModel<bool>> AddToQueue(Report report)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri("amqps://ktcgjehr:vVjEmsjk5LqHAlMHbjJBJj-6MhIEenMy@fish.rmq.cloudamqp.com/ktcgjehr");
                using var connection = factory.CreateConnection();

                var channel = connection.CreateModel();
                channel.QueueDeclare("excel-queue", true, false, false);

                var serilizedReport = JsonSerializer.Serialize(report);
                var bodyParseToByte = Encoding.UTF8.GetBytes(serilizedReport);

                channel.BasicPublish(string.Empty, "excel-queue", null, bodyParseToByte);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return response;
            }
        }
    }
}
