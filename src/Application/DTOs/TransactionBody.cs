using Domain.Entities;

namespace Application.DTOs
{
    public class TransactionBody
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
