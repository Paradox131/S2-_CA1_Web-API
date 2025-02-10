namespace S2__CA1_Web_API.Models.Entities
{
    public class Employee
    {
        public Guid empId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        

    }
}
