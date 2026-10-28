using ClosedXML.Excel;
using StudentRegistrationAutomation_New.Models;

namespace StudentRegistrationAutomation_New.Utils
{
    public class ExcelUtils
    {
        public static List<Student> ReadStudents(string filePath)
        {
            List<Student> students = new List<Student>();

            using XLWorkbook workbook = new XLWorkbook(filePath);

            var sheet = workbook.Worksheet(1);
            var rows = sheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                Student student = new Student();

                student.FirstName = row.Cell(1).GetString();
                student.LastName = row.Cell(2).GetString();
                student.Email = row.Cell(3).GetString();
                student.Gender = row.Cell(4).GetString();
                student.Mobile = row.Cell(5).GetString();
                student.DateOfBirth = row.Cell(6).GetString();
                student.Subject = row.Cell(7).GetString();
                student.Hobby = row.Cell(8).GetString();
                student.Picture = row.Cell(9).GetString();
                student.Address = row.Cell(10).GetString();
                student.State = row.Cell(11).GetString();
                student.City = row.Cell(12).GetString();

                students.Add(student);
            }

            return students;
        }
    }
}