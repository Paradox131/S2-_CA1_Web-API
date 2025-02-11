namespace S2__CA1_Web_API.Models
{
    public class Employee
    {
        public Guid empId { get; set; }//-- Guid Globe unique identifier--//
        public required string Name { get; set; }  //-- input required --/
        public required string Email { get; set; } //-- input required --/
        public string? Phone { get; set; } //-- nullabe it can be left empty--//
        public decimal Salary { get; set; }

        public required Employer employer { get; set; }  //-- input required --/


    }
}
