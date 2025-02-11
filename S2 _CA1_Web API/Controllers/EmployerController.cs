using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S2__CA1_Web_API.Models;

namespace S2__CA1_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerDTO>>> GetEmployers()
        {
            return await _context.Employers
                .Select(e => new EmployerDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Email = e.Email,
                    Phone = e.Phone,
                    Address = e.address
                })
                .ToListAsync();
        }

        // GET: api/Employer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerDTO>> GetEmployer(Guid id)
        {
            var employer = await _context.Employers
                .Where(e => e.Id == id)
                .Select(e => new EmployerDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Email = e.Email,
                    Phone = e.Phone,
                    Address = e.address
                })
                .FirstOrDefaultAsync();

            if (employer == null)
            {
                return NotFound();
            }
            return employer;
        }

        // POST: api/Employer
        [HttpPost]
        public async Task<ActionResult<EmployerDTO>> PostEmployer(EmployerDTO employerDto)
        {
            var employer = new Employer
            {
                Id = Guid.NewGuid(),
                Title = employerDto.Title,
                Email = employerDto.Email,
                Phone = employerDto.Phone,
                address = employerDto.Address
            };

            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployer), new { id = employer.Id }, employerDto);
        }

        // PUT: api/Employer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployer(Guid id, EmployerDTO employerDto)
        {
            if (id != employerDto.Id)
            {
                return BadRequest();
            }

            var employer = await _context.Employers.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }

            employer.Title = employerDto.Title;
            employer.Email = employerDto.Email;
            employer.Phone = employerDto.Phone;
            employer.address = employerDto.Address;

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

        // DELETE: api/Employer/{id}
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
