namespace S2__CA1_Web_API.Models
{
    public class Employer
    {
        public Guid empId { get; set; }//-- Guid Globe unique identifier--//
        public required string Title { get; set; } //-- input required --/
        public required string Email { get; set; } //-- input required --/
        public string? Phone { get; set; } //-- nullabe it can be left empty--//
        public required string address { get; set; }
    }
}
