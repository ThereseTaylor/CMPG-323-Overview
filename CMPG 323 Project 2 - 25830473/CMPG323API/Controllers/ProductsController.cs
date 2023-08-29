using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMPG323API.Models;
using Microsoft.Extensions.Hosting;

namespace CMPG323API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly cmpg323projectdevContext _context;

        public ProductsController(cmpg323projectdevContext context)
        {
            _context = context;
        }

        // GET: api/Products
        // Get method that retrieves all Product entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        // Get method that retrieves one Product based on given id
        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(short id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/5
        // Get method that retrieves all the Products based on given order id
        [HttpGet("{orderid}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetOrderedProduct(short orderid)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Join(_context.OrderDetails, products => products.ProductId,
                 orderdetails => orderdetails.ProductId,
                (products, orderdetails) => new
                {
                    Products = products,
                    OrderDetails = orderdetails
                })
                .Where(entity => entity.OrderDetails.OrderId == orderid)
                .Select(entity => entity.Products)
                .ToListAsync();

            if (product.Count() == 0)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(short id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'cmpg323projectdevContext.Products'  is null.");
          }
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        // DELETE method to delete existing product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(short id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Check if product exists before DELETE or POST
        private bool ProductExists(short id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
