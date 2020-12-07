using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Lab5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }


        public IEnumerable<StundentData> StudentData { get; set; }

        public void OnGet()
        {
            StudentData = _context.Exams
                .Join(_context.Students, o => o.StudentId, c => c.Id, (o, c) => new { SubjectId = o.SubjectId, TeacherId = o.TeacherId, SFirstName = c.FirstName, SLastName = c.LastName })
                .Join(_context.Subjects, oc => oc.SubjectId, p => p.Id, (oc, p) => new { Subject = p.Name, TeacherId = oc.TeacherId, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .Join(_context.Teachers, oc => oc.TeacherId, p => p.Id, (oc, p) => new { Subject = oc.Subject, Teacher = p, SFirstName = oc.SFirstName, SLastName = oc.SLastName }).ToList()
                .GroupBy(t => new { t.SFirstName, t.SLastName })
                .Where(g => g.Count() >= 2)
                .Select(r => new StundentData
                {
                    FirstName = r.Key.SFirstName,
                    LastName = r.Key.SLastName,
                    Teachers = string.Join(", ", r.Select(q => q.Teacher)),
                    Subjects = string.Join(", ", r.Select(q => q.Subject))
                });
        }
    }
}
