﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils.ExcelHelpers
{
    public class ExcelOperator : IExcelOperator
    {
        public void SaveToFile()
        {
            int id=0;
            byte[] excelFile;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Page 1");

                #region Tabel Header Style
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


                using (MemoryStream memoryStream = new MemoryStream())
                {

                    //workbook.SaveAs(memoryStream);
                    string fileName = $"{DateTime.Now.ToString("dd.MM.yyyy_"+id+"_HH.mm.ss")}.xlsx";
                    workbook.SaveAs(@"C:\Users\Rise Teknoloji\Desktop\"+ fileName);

                    // Stream olarak kaydettiğiniz dosyayı byte array olarak alıyoruz, bunu client'a response olarak döneceğiz 
                    excelFile = memoryStream.ToArray();
                }

            }
        }
    }
}