namespace BankManagement.Models.DTOs
{
    public class DTOAccount
    {
        public int AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public int? Balance { get; set; }
        public int? RateOfInterest { get; set; }
        public string? Currency { get; set; }
        public string? AccountStatus { get; set; }

        public int customerId { get; set; }
        public DTOCustomer DTOCustomer { get; set; }
    }
}
