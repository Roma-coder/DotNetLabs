﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly Lab5.ApplicationContext _context;

        public DetailsModel(Lab5.ApplicationContext context)
        {
            _context = context;
        }

        public Exam Exam { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exam = await _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Include(e => e.Teacher).FirstOrDefaultAsync(m => m.Id == id);

            if (Exam == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
