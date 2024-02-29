using System;
using System.Collections.Generic;

namespace BankManagement.Models
{
    public partial class TransactionDetail
    {
        public int DetailId { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? AccountNumber { get; set; }

        public virtual Account? AccountNumberNavigation { get; set; }
    }
}
