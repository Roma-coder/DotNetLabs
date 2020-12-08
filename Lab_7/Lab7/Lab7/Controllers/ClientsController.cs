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
    public class ClientsController : ControllerBase
    {
        private readonly PhotoStudioContext _context;

        public ClientsController(PhotoStudioContext context)
        {
            _context = context;
        }

        [HttpGet("/api/clients/orders")]
        public ActionResult<IEnumerable<ClientAndOrderKey>> GetOrders()
        {
            return _context.Orders
           .Join(_context.Clients, orders => orders.ClientId, clients => clients.Id,
           (o, c) => new
           {
               OptionId = o.OptionId,
               Name = c.Name,
               Surname = c.Surname,
               Quantity = o.Quantity,
               DateStart = o.DateStart,
               DateFinish = o.DateFinish,
               Id = c.Id
           })

           .Join(_context.Options, c => c.OptionId, o => o.Id, (c, o) => new
           {
               Option = o.Title,
               Name = c.Name,
               Surname = c.Surname,
               Quantity = c.Quantity,
               DateStart = c.DateStart,
               DateFinish = c.DateFinish,
               Price = c.Quantity * o.Price,
               Id = c.Id
           })
           .ToList()
           .GroupBy(e => new { e.Id, e.Name, e.Surname })
           .Select(e => new ClientAndOrderKey
           {
               Name = e.Key.Name,
               Surname = e.Key.Surname,
               Count = e.Count(),
               Id = e.Key.Id,
               Orders = e.Select(e => new ClientAndOrder
               {
                   DateStart = e.DateStart,
                   DateFinish = e.DateFinish,
                   Option = e.Option,
                   Quantity = e.Quantity,
                   Price = e.Price
               }).ToList()

           })
           .ToList();
        }
        // GET: api/Clients
        
        [HttpGet("/api/clients")]

        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("/api/clients/{id?}")]
        
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/clients/{id?}")]
        
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/clients")]
        
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("/api/clients/{id?}")]
        
        public async Task<ActionResult<Client>> DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
