using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3
{
    public class HtmlWriter
    {
        private ApplicationContext _dbContext;

        public HtmlWriter(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static async Task ShowPage(HttpContext httpContext, string pageName, string[] columns, List<string[]> rows)
        {
            await httpContext.Response.WriteAsync(File.ReadAllText(@".\wwwroot\templates\header.html"));

            string tableData =
                $"<h1>{pageName}</h1>" +
                "<table class='table table-striped'>" +
                "<tr>";

            foreach (var column in columns)
            {
                tableData += $"<th>{column}</th>";
            }

            await httpContext.Response.WriteAsync(tableData + "</tr>");

            foreach (var row in rows)
            {
                string htmlRow = "<tr>";

                foreach (var cell in row)
                {
                    htmlRow += $"<td>{cell}</td>";
                }

                await httpContext.Response.WriteAsync(htmlRow + "</tr>");
            }

            await httpContext.Response.WriteAsync("</table>");
            await httpContext.Response.WriteAsync(File.ReadAllText(@".\wwwroot\templates\footer.html"));
        }

        public async Task WriteMainPage(HttpContext httpContext)
        {
            string[] columns = new[] { "Ім'я студента", "Прізвище студента", "Список предметів", "Список викладачів" };

            var rows = _dbContext.Exams
                .Join(_dbContext.Students, o => o.StudentId, c => c.Id, (o, c) => new { SubjectId = o.SubjectId, TeacherId = o.TeacherId, SFirstName = c.FirstName, SLastName = c.LastName })
                .Join(_dbContext.Subjects, oc => oc.SubjectId, p => p.Id, (oc, p) => new { Subject = p.Name, TeacherId = oc.TeacherId, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .Join(_dbContext.Teachers, oc => oc.TeacherId, p => p.Id, (oc, p) => new { Subject = oc.Subject, Teacher = p, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .GroupBy(t => new { t.SFirstName, t.SLastName })
                .Where(g => g.Count() >= 2)
                .Select(e => new { e.Key, Count = e.Count(), Subjects = e.Select(e => e.Subject).ToArray(), Teachers = e.Select(e => e.Teacher.FirstName + " " + e.Teacher.LastName).ToArray() })
                .Select(x => new[] { x.Key.SFirstName, x.Key.SLastName, string.Join(", ", x.Subjects), string.Join(", ", x.Teachers) })
                .ToList();

            await ShowPage(httpContext, "Список студентів, які здали більше 2 екзаменів", columns, rows);
        }

        public async Task WriteStudentsPage(HttpContext httpContext)
        {
            string[] columns = new[] { "Прізвище", "Ім'я", "По батькові", "Адреса", "Номер телефону", "Дата народження", "Стипендія" };

            List<string[]> rows = _dbContext.Students
                    .Select(x => new[] { x.LastName, x.FirstName, x.MiddleName, x.Address, x.Phone, x.Birth.ToString(), x.Studentship.ToString() })
                    .ToList();

            await ShowPage(httpContext, "Студенти", columns, rows);
        }

        public async Task WriteTeachersPage(HttpContext httpContext)
        {
            string[] columns = new[] { "Прізвище", "Ім'я", "По батькові" };

            List<string[]> rows = _dbContext.Teachers
                    .Select(x => new[] { x.LastName, x.FirstName, x.MiddleName })
                    .ToList();

            await ShowPage(httpContext, "Викладачі", columns, rows);
        }

        public async Task WriteSubjectsPage(HttpContext httpContext)
        {
            string[] columns = new[] { "Назва", "Дата" };

            List<string[]> rows = _dbContext.Subjects
                    .Select(x => new[] { x.Name, x.Date.ToString() })
                    .ToList();

            await ShowPage(httpContext, "Предмети", columns, rows);
        }
    }
}
