using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace S2__CA1_Web_API.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employer>>> GetEmployers()
        {
            return await _context.Employers.ToListAsync();
        }

        // GET: api/Employers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employer>> GetEmployer(Guid id)
        {
            var employer = await _context.Employers.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return employer;
        }

        // POST: api/Employers
        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        {
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployer), new { id = employer.Id }, employer);
        }

        // PUT: api/Employers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployer(Guid id, Employer employer)
        {
            if (id != employer.Id)
            {
                return BadRequest();
            }

            _context.Entry(employer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employers.Any(e => e.Id == id))
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

        // DELETE: api/Employers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var employer = await _context.Employers.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }

            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
