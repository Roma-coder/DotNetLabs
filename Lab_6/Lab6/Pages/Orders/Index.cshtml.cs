using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Lab6.Models.PhotoStudioContext _context;

        public IndexModel(Lab6.Models.PhotoStudioContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Option).ToListAsync();
        }
    }
}
