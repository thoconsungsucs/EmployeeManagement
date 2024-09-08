using ClosedXML.Excel;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Utilities
{
    public class ExcelExporter : IExporter
    {
        public async Task<byte[]> ExportEmployees(List<Employee> employees)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");

                // Add headers
                worksheet.Cell(1, 1).Value = "EmployeeId";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "DateOfBirth";
                worksheet.Cell(1, 4).Value = "Age";
                worksheet.Cell(1, 5).Value = "PhoneNumber";
                worksheet.Cell(1, 6).Value = "IdentityId";
                worksheet.Cell(1, 7).Value = "Job";
                worksheet.Cell(1, 8).Value = "Ethic";
                worksheet.Cell(1, 9).Value = "Ward";
                worksheet.Cell(1, 10).Value = "District";
                worksheet.Cell(1, 11).Value = "City";
                worksheet.Cell(1, 12).Value = "Address";
                worksheet.Cell(1, 13).Value = "Number of diplomas";

                // Add employee data
                for (int i = 0; i < employees.Count; i++)
                {
                    var employee = employees[i];
                    worksheet.Cell(i + 2, 1).Value = employee.EmployeeId;
                    worksheet.Cell(i + 2, 2).Value = employee.Name;
                    worksheet.Cell(i + 2, 3).Value = employee.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 4).Value = employee.Age;
                    worksheet.Cell(i + 2, 5).Value = employee.PhoneNumber;
                    worksheet.Cell(i + 2, 6).Value = employee.IdentityId;
                    worksheet.Cell(i + 2, 7).Value = employee.Job?.Title;
                    worksheet.Cell(i + 2, 8).Value = employee.Ethic?.Name;
                    worksheet.Cell(i + 2, 9).Value = employee.Ward?.Name;
                    worksheet.Cell(i + 2, 10).Value = employee.District?.Name;
                    worksheet.Cell(i + 2, 11).Value = employee.City?.Name;
                    worksheet.Cell(i + 2, 12).Value = employee.Address;
                    worksheet.Cell(i + 2, 13).Value = employee.Diplomas.Count();
                }

                // Save the workbook
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return await Task.FromResult(stream.ToArray());
                }
            }
        }
    }
}
