using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Db;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly AccountingDbContext _context;
        private readonly IMapper _mapper;

        public TransactionService(AccountingDbContext context, IAccountService accountService, IMapper mapper)
        {
            _context = context;
            _accountService = accountService;
            _mapper = mapper;
        }

        public Task<Transaction> GetByIdAsync(Guid id)
        {
            return _context.Transactions.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Transaction> CreateTransactionAsync(TransactionBody transactionBody)
        {
            var account = await _accountService.GetAccountAsync();

            var transaction = _mapper.Map<Transaction>(transactionBody);
            transaction.Account = account;
            transaction.AccountId = account.Id;
            account.Transactions.Add(transaction);

            switch (transaction.Type)
            {
                case TransactionType.Credit:
                    account.Balance += transaction.Amount;

                    break;
                case TransactionType.Debit:
                    if (transaction.Amount > account.Balance)
                    {
                        throw new InvalidAccountBalanceException("Not enough money left on account.");
                    }

                    account.Balance -= transaction.Amount;

                    break;
                default:
                    throw new InvalidEnumArgumentException(
                        nameof(transactionBody), (int)transaction.Type, typeof(TransactionType)
                    );
            }

            _context.Accounts.Update(account);
            // Don't know why but EffectiveDate doesn't automatically generated
            transaction.EffectiveDate = DateTime.Now;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<IList<Transaction>> GetTransactionsAsync()
        {
            // var dbTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted);

            var transactions = await _context.Transactions.ToListAsync();

            // await dbTransaction.CommitAsync();

            return transactions;
        }
    }
}
