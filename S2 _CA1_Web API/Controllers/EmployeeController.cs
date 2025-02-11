using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S2__CA1_Web_API.Models;

namespace S2__CA1_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.employer)
                .Select(e => new EmployeeDTO
                {
                    empId = e.empId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    EmployerId = e.employer.Id
                })
                .ToListAsync();
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.Include(e => e.employer)
                .Where(e => e.empId == id)
                .Select(e => new EmployeeDTO
                {
                    empId = e.empId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    EmployerId = e.employer.Id
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDto)
        {
            var employer = await _context.Employers.FindAsync(employeeDto.EmployerId);
            if (employer == null)
            {
                return BadRequest("Invalid Employer ID");
            }

            var employee = new Employee
            {
                empId = Guid.NewGuid(),
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary,
                employer = employer
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.empId }, employeeDto);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.empId)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employer = await _context.Employers.FindAsync(employeeDto.EmployerId);
            if (employer == null)
            {
                return BadRequest("Invalid Employer ID");
            }

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Salary = employeeDto.Salary;
            employee.employer = employer;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.empId == id))
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

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
