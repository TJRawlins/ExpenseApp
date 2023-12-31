﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Data;
using ExpenseApp.Models;

namespace ExpenseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenselinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenselinesController(AppDbContext context)
        {
            _context = context;
        }

        // RECALCULATE EXPENSE TOTAL
        private void RecalculateExpenseTotal(int expenseId)
        {

        }

        // GET: api/Expenselines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenseline>>> GetExpenselines()
        {
          if (_context.Expenselines == null)
          {
              return NotFound();
          }
            return await _context.Expenselines.ToListAsync();
        }

        // GET: api/Expenselines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenseline>> GetExpenseline(int id)
        {
          if (_context.Expenselines == null)
          {
              return NotFound();
          }
            var expenseline = await _context.Expenselines.FindAsync(id);

            if (expenseline == null)
            {
                return NotFound();
            }

            return expenseline;
        }

        // PUT: api/Expenselines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseline(int id, Expenseline expenseline)
        {
            if (id != expenseline.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenseline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenselineExists(id))
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

        // POST: api/Expenselines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expenseline>> PostExpenseline(Expenseline expenseline)
        {
          if (_context.Expenselines == null)
          {
              return Problem("Entity set 'AppDbContext.Expenselines'  is null.");
          }
            _context.Expenselines.Add(expenseline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseline", new { id = expenseline.Id }, expenseline);
        }

        // DELETE: api/Expenselines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseline(int id)
        {
            if (_context.Expenselines == null)
            {
                return NotFound();
            }
            var expenseline = await _context.Expenselines.FindAsync(id);
            if (expenseline == null)
            {
                return NotFound();
            }

            _context.Expenselines.Remove(expenseline);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenselineExists(int id)
        {
            return (_context.Expenselines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
