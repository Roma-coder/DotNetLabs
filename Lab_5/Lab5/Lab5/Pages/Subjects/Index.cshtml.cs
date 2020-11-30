using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab5;
using Lab5.Models;

namespace Lab5.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly Lab5.ApplicationContext _context;

        public IndexModel(Lab5.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get;set; }

        public async Task OnGetAsync()
        {
            Subject = await _context.Subjects.ToListAsync();
        }
    }
}
