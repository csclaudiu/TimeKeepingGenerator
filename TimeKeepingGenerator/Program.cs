using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TimeKeepingGenerator.DomainModels;

namespace TimeKeepingGenerator
{
    class Program
    {


        static void Main(string[] args)
        {
            var employees = new List<string>();
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            try
            {
                using (var sr = new StreamReader(@"initial_data.txt"))
                {
                    string line;
                    int counter = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            fromDate = DateTime.Parse(line);
                            counter++;
                            continue;
                        }
                        if (counter == 1)
                        {
                            toDate = DateTime.Parse(line);
                            counter++;
                            continue;
                        }

                        employees.Add(line.Trim());

                        counter++;
                    }
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError($"Initial parameters in incorrect format. {ex}");
                throw;
            }
            var holidaysExplorer = new Holidays(fromDate, toDate);
            var holidays = holidaysExplorer.Get();
            var timeGenerator = new TimeGenerator(employees, fromDate, toDate, holidays);
            var result = timeGenerator.Generate();

            ExportExcel(result);
        }

        private static void ExportExcel(List<EmployeeTimes> data)
        {
            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("Worksheet1");
                    var worksheet = excel.Workbook.Worksheets["Worksheet1"];

                    var headers = data.Select(h => h.Name).ToArray();

                    for (var i = 1; i <= headers.Length; i++)
                    {
                        worksheet.Cells[$"{i.GetExcelFirstLetterForClient()}{1}"].Value = headers[i - 1];
                        worksheet.Cells[$"{i.GetExcelFirstLetterForClient()}{1}"].Style.Font.Bold = true;
                        worksheet.Cells[$"{i.GetExcelFirstLetterForClient()}1:{i.GetExcelSecondLetterForClient()}1"].Merge = true;
                    }

                    var currentEmployee = 1;
                    data.ForEach(e =>
                    {
                        var currentRowNumber = 2;
                        e.TimeEntries.ForEach(en =>
                        {
                            if (currentEmployee == 1)
                            {
                                worksheet.Cells[$"A{currentRowNumber}"].Style.Numberformat.Format = "dd/mm/yyyy";
                                worksheet.Cells[$"A{currentRowNumber}"].Style.Font.Bold = true;
                                worksheet.Cells[$"A{currentRowNumber}"].Value = en.In;
                            }
                            worksheet.Cells[$"{currentEmployee.GetExcelFirstLetterForClient()}{currentRowNumber}"].Style.Numberformat.Format = "hh:mm:ss";
                            worksheet.Cells[$"{currentEmployee.GetExcelFirstLetterForClient()}{currentRowNumber}"].Value = en.In;
                            worksheet.Cells[$"{currentEmployee.GetExcelSecondLetterForClient()}{currentRowNumber}"].Style.Numberformat.Format = "hh:mm:ss";
                            worksheet.Cells[$"{currentEmployee.GetExcelSecondLetterForClient()}{currentRowNumber}"].Value = en.Out;
                            ++currentRowNumber;
                        });

                        ++currentEmployee;
                    });

                    worksheet.Cells["A1:A2"].AutoFitColumns();

                    FileInfo excelFile = new FileInfo(@"employees_timestamps.xlsx");
                    excel.SaveAs(excelFile);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError($"Error durring excel generation. {ex}");
                throw;
            }
        }
    }
}
