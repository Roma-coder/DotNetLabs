using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Exams
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



            return View(data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
