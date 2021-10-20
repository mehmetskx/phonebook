using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Services.ReportService;
using PhoneBook.Utils.ExcelHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var _reportService = scope.ServiceProvider.GetService<IReportService>();
                var _unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

                var createdExcel = await _reportService.ConsumeRedisQueue();

                if (createdExcel.IsSuccess)
                    await _unitOfWork.ReportRepository.UpdateAsync(createdExcel.Data);

            }
        }
    }
}
