using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.Models.Enums;
using PhoneBook.ReportService.Models;
using PhoneBook.Services.ReportService;
using PhoneBook.Utils.ExcelHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private readonly IReportService _reportService;

        public ReportsController(ILogger<ReportsController> logger, IExcelOperator excelOperator, IReportService reportService)
        {
            _logger = logger;
            _excelOperator = excelOperator;
            _reportService = reportService;
        }
        [HttpGet("getall")]
        public async Task<ResponseModel<List<ReportDto>>> GetAll()
        {
            return await _reportService.GetAllReports();
        }

        [HttpGet]
        public async Task<ResponseModel<ReportStatusType>> CreateReport()
        {
            return await _reportService.CreateReportRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int reportId)
        {
            if (file is not { Length: > 0 }) return BadRequest();

            string filePath = file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Reports", filePath);
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok(await _reportService.UpdateExcelStatus(path, reportId));
        }

    }
}
