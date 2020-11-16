using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using creditCardBackend;
using creditCardBackend.Models;

namespace creditCardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public CreditCardController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CreditCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCard()
        {
            return await _context.CreditCard.ToListAsync();
        }

        // GET: api/CreditCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCard>> GetCreditCard(int id)
        {
            var CreditCard = await _context.CreditCard.FindAsync(id);

            if (CreditCard == null)
            {
                return NotFound();
            }

            return CreditCard;
        }

        // PUT: api/CreditCard/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCard(int id, CreditCard CreditCard)
        {
            if (id != CreditCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(CreditCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(id))
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

        // POST: api/CreditCard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CreditCard>> PostCreditCard(CreditCard CreditCard)
        {
            _context.CreditCard.Add(CreditCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditCard", new { id = CreditCard.Id }, CreditCard);
        }

        // DELETE: api/CreditCard/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreditCard>> DeleteCreditCard(int id)
        {
            var CreditCard = await _context.CreditCard.FindAsync(id);
            if (CreditCard == null)
            {
                return NotFound();
            }

            _context.CreditCard.Remove(CreditCard);
            await _context.SaveChangesAsync();

            return CreditCard;
        }

        private bool CreditCardExists(int id)
        {
            return _context.CreditCard.Any(e => e.Id == id);
        }
    }
}
