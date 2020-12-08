using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab7.Models;

namespace Lab7.Controllers
{
   

    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly PhotoStudioContext _context;

        public OptionsController(PhotoStudioContext context)
        {
            _context = context;
        }

     
        [HttpGet("/api/options")]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptions()
        {
            return await _context.Options.ToListAsync();
        }

        // GET: api/Options/5
        [HttpGet("/api/options/{id?}")]
        public async Task<ActionResult<Option>> GetOption(Guid id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound();
            }

            return option;
        }

        // PUT: api/Options/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/options/{id?}")]
        public async Task<IActionResult> PutOption(Guid id, Option option)
        {
            if (id != option.Id)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Options
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/options")]
        public async Task<ActionResult<Option>> PostOption(Option option)
        {
            _context.Options.Add(option);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOption", new { id = option.Id }, option);
        }

        // DELETE: api/Options/5
        [HttpDelete("/api/options/{id?}")]
        public async Task<ActionResult<Option>> DeleteOption(Guid id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return option;
        }

        private bool OptionExists(Guid id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
