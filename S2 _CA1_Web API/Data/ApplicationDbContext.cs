using Microsoft.EntityFrameworkCore;
using S2__CA1_Web_API.Models;
using S2__CA1_Web_API.Models.Entities;
using System.Collections.Generic;
namespace S2__CA1_Web_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var employer1 = new Employer
            {
                Id = Guid.NewGuid(),
                Title = "Tech Solutions Inc.",
                Email = "contact@techsolutions.com",
                Phone = "123-456-7890",
                address = "123 Tech Street, Silicon Valley"
            };

            var employer2 = new Employer
            {
                Id = Guid.NewGuid(),
                Title = "Innovatech Ltd.",
                Email = "info@innovatech.com",
                Phone = "456-789-0123",
                address = "456 Innovation Road, New York"
            };

            modelBuilder.Entity<Employer>().HasData(employer1, employer2);

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    empId = Guid.NewGuid(),
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    Phone = "987-654-3210",
                    Salary = 60000m,
                    employer = employer1
                },
                new Employee
                {
                    empId = Guid.NewGuid(),
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    Phone = "654-321-0987",
                    Salary = 65000m,
                    employer = employer1
                },
                new Employee
                {
                    empId = Guid.NewGuid(),
                    Name = "Alice Johnson",
                    Email = "alicejohnson@example.com",
                    Phone = "321-098-7654",
                    Salary = 70000m,
                    employer = employer2
                },
                new Employee
                {
                    empId = Guid.NewGuid(),
                    Name = "Bob Williams",
                    Email = "bobwilliams@example.com",
                    Phone = "210-987-6543",
                    Salary = 72000m,
                    employer = employer2
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
