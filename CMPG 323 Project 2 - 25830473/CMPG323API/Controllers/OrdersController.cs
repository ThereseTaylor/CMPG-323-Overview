using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMPG323API.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace CMPG323API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly cmpg323projectdevContext _context;

        public OrdersController(cmpg323projectdevContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        // Retrieves all order entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/int/5
        // Retrieves one order based on its id
        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(short id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
         }

        // GET: api/Orders/5
        // Retrieves call orders related to a customer
        [HttpGet("{customerid}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrder(short customerid)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Where(x => x.CustomerId == customerid).ToListAsync(); 

            if (order.Count() == 0)
            {
                return NotFound();
            }

            return order;
        }

        //PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(short id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // Create a new order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'cmpg323projectdevContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        // Remove a existing order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(short id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Check if order exists before DELETE or POST
        private bool OrderExists(short id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
