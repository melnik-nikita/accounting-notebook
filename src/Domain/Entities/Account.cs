using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }

        public IList<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}
