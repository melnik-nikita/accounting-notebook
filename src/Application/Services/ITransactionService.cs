using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(TransactionBody transactionBody);

        Task<IList<Transaction>> GetTransactionsAsync();

        Task<Transaction> GetByIdAsync(Guid id);
    }
}
