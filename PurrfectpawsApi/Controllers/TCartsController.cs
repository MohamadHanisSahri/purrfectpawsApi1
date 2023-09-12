using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurrfectpawsApi.DatabaseDbContext;
using PurrfectpawsApi.Models;

namespace PurrfectpawsApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TCartsController : ControllerBase
    {
        private readonly PurrfectpawsContext _context;

        public TCartsController(PurrfectpawsContext context)
        {
            _context = context;
        }

        // GET: api/TCarts
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TCart>>> GetTCarts()
        //{
        //  if (_context.TCarts == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.TCarts.ToListAsync();
        //}

        // GET: api/TCarts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TCart>> GetTCart(int id)
        //{
        //    if (_context.TCarts == null)
        //    {
        //        return NotFound();
        //    }
        //    var tCart = await _context.TCarts.FromSqlRaw()
               

        //    if (tCart == null)
        //    {
        //        return NotFound();
        //    }

        //    return tCart;
        //}

        // PUT: api/TCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCart(int id, int quantityToAmend)
        {

            try
            {
                var isCartExist = await _context.TCarts.FirstOrDefaultAsync(c => c.CartId == id);
                if (isCartExist == null)
                {
                    return NotFound("Cart not found");
                }
                isCartExist.Quantity = quantityToAmend;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TCartExists(id))
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

        // POST: api/TCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TCart>> PostTCart([FromBody] TCartPostDto tCartPostDto )
        {
          if (_context.TCarts == null)
          {
              return Problem("Entity set 'PurrfectpawsContext.TCarts'  is null.");
          }
          var isUserExist = await _context.TUsers.FirstOrDefaultAsync(u => u.UserId == tCartPostDto.UserId);
            if (isUserExist == null)
            {
                return NotFound("User not found");
            }
            var isProductExist = await _context.TProducts.FirstOrDefaultAsync(p => p.ProductId == tCartPostDto.ProductId);
            if (isProductExist == null)
            {
                return NotFound("Product not found");
            }
            var tCart = new TCart
            {
                ProductId = tCartPostDto.ProductId,
                UserId = tCartPostDto.UserId,
                Quantity = tCartPostDto.Quantity
            };
            _context.TCarts.Add(tCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTCart", new { id = tCart.CartId }, tCart);
        }

        // DELETE: api/TCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTCart(int id)
        {
            if (_context.TCarts == null)
            {
                return NotFound();
            }
            var tCart = await _context.TCarts.FindAsync(id);
            if (tCart == null)
            {
                return NotFound();
            }

            _context.TCarts.Remove(tCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TCartExists(int id)
        {
            return (_context.TCarts?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
