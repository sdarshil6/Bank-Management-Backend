namespace BankManagement.Models.DTOs
{
    public class DTOAccountAdd
    {
        
        public string? AccountType { get; set; }
        public int? Balance { get; set; }
        public string? Currency { get; set; }
        public string? AccountStatus { get; set; }
        public int? CustomerId { get; set; }
    }
}
