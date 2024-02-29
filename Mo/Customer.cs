using System;
using System.Collections.Generic;

namespace BankManagement.Mo
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }

        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PostalCode { get; set; }
        public string? EmailAddress { get; set; }
        public string? MartitalStatus { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
