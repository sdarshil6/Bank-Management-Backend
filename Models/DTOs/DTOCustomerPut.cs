namespace BankManagement.Models.DTOs
{
    public class DTOCustomerPut
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PostalCode { get; set; }
        public string? EmailAddress { get; set; }
        public string? MartitalStatus { get; set; }
    }
}
