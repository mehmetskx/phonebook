using AutoMapper;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Entities;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.Models.Enums;
using PhoneBook.Utils;
using PhoneBook.Utils.ExcelHelpers;
using PhoneBook.Utils.MQHelper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.Services.ReportService
{
    public class ReporterService : IReportService
    {
        private readonly ILogger<ReporterService> _logger;
        private readonly IMapper _mapper;
        private readonly IRabbitMqHelper _rabbitMqHelper;
        private readonly IExcelOperator _excelOperator;
        private readonly IUnitOfWork _unitOfWork;

        public ReporterService(ILogger<ReporterService> logger, IRabbitMqHelper rabbitMqHelper, IExcelOperator excelOperator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _rabbitMqHelper = rabbitMqHelper;
            _excelOperator = excelOperator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ReportStatusType>> CreateReportRequest()
        {
            var response = new ResponseModel<ReportStatusType>();
            try
            {
                Report report = new Report
                {
                    ReportStatusType = ReportStatusType.Preparing,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.ReportRepository.AddAsync(report);
                var affected = await _unitOfWork.CommitAsync();
                _logger.LogInformation($"PhoneBook.Services.ReportService => Task<ResponseModel<ReportStatusType>> CreateReportRequest() = {report}");

                await _rabbitMqHelper.AddToQueue(report);
                response.Data = ReportStatusType.Preparing;
                response.TotalRowCount = affected;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding report.");
                ex.LogException(_logger, ex.Message);

                response.ErrorList.Add(new Error
                {
                    Description = ex.Message
                });
                return response;
            }
        }

        public async Task<ResponseModel<Report>> ConsumeRedisQueue()
        {
            var response = new ResponseModel<Report>();
            try
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri("amqps://ktcgjehr:vVjEmsjk5LqHAlMHbjJBJj-6MhIEenMy@fish.rmq.cloudamqp.com/ktcgjehr");

                using var connection = factory.CreateConnection();

                var channel = connection.CreateModel();

                //  channel.QueueDeclare("excel-queue", true, false, false);

                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume("excel-queue", false, consumer);
                //Gelecek data sayısı

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var queueReport = JsonSerializer.Deserialize<Report>(Encoding.UTF8.GetString(e.Body.ToArray()));

                    var saveFileResult = _excelOperator.SaveToFile(reportId: queueReport.Id);

                    Console.WriteLine($"id {queueReport.Id}");

                    if (saveFileResult.IsSuccess)
                    {
                        queueReport.ReportStatusType = ReportStatusType.Ready;
                        queueReport.ModifiedDate = DateTime.Now;
                        queueReport.FilePath = saveFileResult.Data;
                        response.Data = queueReport;
                        channel.BasicAck(e.DeliveryTag, false);
                    }
                };

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }

        public async Task<ResponseModel<List<ReportDto>>> GetAllReports()
        {
            var response = new ResponseModel<List<ReportDto>>();
            try
            {

                _logger.LogInformation($"PhoneBook.Services.ReportService => Task<ResponseModel<List<Report>>> GetAllReports()");
                var reportList = await _unitOfWork.ReportRepository.GetAllAsync(x => x.IsActive);

                if (reportList is null)
                    return default(ResponseModel<List<ReportDto>>);

                response.Data = _mapper.Map<List<ReportDto>>(reportList);
                response.TotalRowCount = reportList.Count(); 
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while getting report.");
                ex.LogException(_logger, ex.Message);

                response.ErrorList.Add(new Error
                {
                    Description = ex.Message
                });
                return response;
            }
        }
    }
}
