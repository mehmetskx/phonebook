using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Data.Context;
using PhoneBook.Data.UnitOfWork;
using PhoneBook.Services.ReportService;
using PhoneBook.Utils.ExcelHelpers;
using PhoneBook.Utils.MQHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var Configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

                    

                    services.AddHostedService<Worker>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddTransient<IExcelOperator, ExcelOperator>();
                    services.AddTransient<IRabbitMqHelper, RabbitMqHelper>();
                    services.AddTransient<IReportService, ReporterService>();

                    services.AddDbContext<PhoneBookContext>(opt =>
                    {
                        opt.UseSqlServer(Configuration.GetConnectionString("AppConnection"),
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
                            })
                        .EnableSensitiveDataLogging();
                    });

                });
    }
}
