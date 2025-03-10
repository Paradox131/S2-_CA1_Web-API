﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace S2__CA1_Web_API.Models
{
    public class EmployeeDTO
    {
        public Guid empId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public Guid EmployerId { get; set; }
    }
}
