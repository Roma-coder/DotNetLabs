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
    public class DetailsModel : PageModel
    {
        private readonly Lab6.Models.PhotoStudioContext _context;

        public DetailsModel(Lab6.Models.PhotoStudioContext context)
        {
            _context = context;
        }

        public Option Option { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Option = await _context.Options.FirstOrDefaultAsync(m => m.Id == id);

            if (Option == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
