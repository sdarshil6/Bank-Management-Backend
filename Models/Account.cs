using System;
using System.Collections.Generic;

namespace BankManagement.Models
{
    public partial class Account
    {
        public Account()
        {
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        public int AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public int? Balance { get; set; }
        public int? RateOfInterest { get; set; }
        public string? Currency { get; set; }
        public string? AccountStatus { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
