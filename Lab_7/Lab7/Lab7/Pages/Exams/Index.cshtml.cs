using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab5;
using Lab5.Models;

namespace Lab5.Pages.Exams
{
    public class IndexModel : PageModel
    {
        private readonly Lab5.ApplicationContext _context;

        public IndexModel(Lab5.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Exam> Exam { get;set; }

        public async Task OnGetAsync()
        {
            Exam = await _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Include(e => e.Teacher).ToListAsync();
        }
    }
}
