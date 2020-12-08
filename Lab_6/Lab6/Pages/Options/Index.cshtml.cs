using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Pages.Options
{
    public class IndexModel : PageModel
    {
        private readonly Lab6.Models.PhotoStudioContext _context;

        public IndexModel(Lab6.Models.PhotoStudioContext context)
        {
            _context = context;
        }

        public IList<Option> Option { get;set; }

        public async Task OnGetAsync()
        {
            Option = await _context.Options.ToListAsync();
        }
    }
}
