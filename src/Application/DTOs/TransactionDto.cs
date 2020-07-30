using System;
using Domain.Entities;

namespace Application.Mappings
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
