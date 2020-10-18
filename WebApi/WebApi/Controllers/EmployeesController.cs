using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeConext _context;

        public EmployeesController(EmployeeConext context)
        {
            _context = context;
        }

        // GET: api/Emploees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmploees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Emploees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmploee(int id)
        {
            var emploee = await _context.Employees.FindAsync(id);

            if (emploee == null)
            {
                return NotFound();
            }

            return emploee;
        }

        // PUT: api/Emploees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploee(int id, Employee emploee)
        {
            if (id != emploee.id)
            {
                return BadRequest();
            }

            _context.Entry(emploee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmploeeExists(id))
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

        // POST: api/Emploees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmploee(Employee emploee)
        {
            _context.Employees.Add(emploee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmploee", new { id = emploee.id }, emploee);
        }

        // DELETE: api/Emploees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmploee(int id)
        {
            var emploee = await _context.Employees.FindAsync(id);
            if (emploee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(emploee);
            await _context.SaveChangesAsync();

            return emploee;
        }

        private bool EmploeeExists(int id)
        {
            return _context.Employees.Any(e => e.id == id);
        }
    }
}
