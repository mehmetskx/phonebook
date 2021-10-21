using ClosedXML.Excel;
using PhoneBook.Data.Entities;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils.ExcelHelpers
{
    public class ExcelOperator : IExcelOperator
    {
        public async Task<ResponseModel<string>> SaveToFile(int reportId, List<Contact> contacts)
        {
            var response = new ResponseModel<string>();
            try
            {
                byte[] excelFile;

                using var workbook = new XLWorkbook();

                var worksheet = workbook.Worksheets.Add("Page 1");

                #region Table Header Style
                worksheet.Cell("A1").Value = "Location";
                worksheet.Cell("B1").Value = "Registered User Count In Location";
                worksheet.Cell("C1").Value = "Registered User Phone Count In Location";
                worksheet.Column(1).Width = 8.71;//A
                worksheet.Column(2).Width = 34.14;//B
                worksheet.Column(3).Width = 41.14;//C
                worksheet.Cell("A1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Cell("A1").Style.Border.SetOutsideBorderColor(XLColor.Black);
                worksheet.Cell("B1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Cell("B1").Style.Border.SetOutsideBorderColor(XLColor.Black);
                worksheet.Cell("C1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Cell("C1").Style.Border.SetOutsideBorderColor(XLColor.Black);
                IXLRange titleStyle = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 3));
                titleStyle.Style.Font.SetBold();
                titleStyle.Style.Font.SetFontColor(XLColor.FromColor(Color.Black));
                titleStyle.Style.Font.SetFontSize(11);
                titleStyle.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                titleStyle.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                #endregion

                worksheet.Cell("A2").Value = contacts.Where(x => x.ContactTypeId == (int)Models.Enums.ContactType.Location).Count();
                worksheet.Cell("B2").Value = contacts.Where(x => x.ContactTypeId == (int)Models.Enums.ContactType.Email).Count();
                worksheet.Cell("C2").Value = contacts.Where(x => x.ContactTypeId == (int)Models.Enums.ContactType.Phone).Count();


                using MemoryStream memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);

                string fileName = $"{DateTime.Now.ToString("dd.MM.yyyy_" + reportId + "_HH.mm.ss")}.xlsx";
                excelFile = memoryStream.ToArray();

                var fileUrl = await SendToService(excelFile, fileName, reportId);

                response.Data = fileUrl;
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorList = new List<Error>() { new Error { Description = ex.Message } };
                return response;
            }

        }
        private void SaveToPath(XLWorkbook workbook, string fileName)
        {
            workbook.SaveAs(@"C:\Users\Rise Teknoloji\Desktop\" + fileName);
        }
        private async Task<string> SendToService(byte[] excelFile, string fileName, int reportId)
        {
            string baseUrl = "https://localhost:44358/api/reports";
            MultipartFormDataContent multipartFormDataContent = new();

            multipartFormDataContent
                .Add(new ByteArrayContent(excelFile), "file", fileName);

            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}?reportId={reportId}", multipartFormDataContent);

            return string.Empty;
        }
    }
}
