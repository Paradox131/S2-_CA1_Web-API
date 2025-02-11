using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace S2__CA1_Web_API.Models
{
    public class EmployerDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
    }
}
