using System;

namespace Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Account Account { get; set; }
    }
}
