using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab6.Models;

namespace Lab6.Pages.Options
{
    public class CreateModel : PageModel
    {
        private readonly Lab6.Models.PhotoStudioContext _context;

        public CreateModel(Lab6.Models.PhotoStudioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Option Option { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Options.Add(Option);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
