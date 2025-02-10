using Microsoft.EntityFrameworkCore;
using S2__CA1_Web_API.Models.Entities;
using System.Collections.Generic;
namespace S2__CA1_Web_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> employees { get; set; }
    }
}
