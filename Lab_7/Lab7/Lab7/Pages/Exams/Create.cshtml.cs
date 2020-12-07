using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5;
using Lab5.Models;

namespace Lab5.Pages.Exams
{
    public class CreateModel : PageModel
    {
        private readonly Lab5.ApplicationContext _context;

        public CreateModel(Lab5.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
        ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public Exam Exam { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Exams.Add(Exam);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
