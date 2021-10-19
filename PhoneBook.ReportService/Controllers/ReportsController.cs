using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.ReportService.Models;
using PhoneBook.Utils.ExcelHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IExcelOperator _excelOperator;

        public ReportsController(ILogger<ReportsController> logger, IExcelOperator excelOperator = null)
        {
            _logger = logger;
            _excelOperator = excelOperator;
        }
        [HttpGet]
        public string Index()
        {
            _excelOperator.SaveToFile();
            return "";
        }

    }
}
