using System.Threading.Tasks;
using Db;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AccountService: IAccountService
    {
        private readonly AccountingDbContext _context;

        public AccountService(AccountingDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountAsync()
        {
            var account = await _context.Accounts.FirstOrDefaultAsync();

            if (account is null)
            {
                account = new Account();
                await _context.AddAsync(account);
                await _context.SaveChangesAsync();
            }

            return account;
        }
    }
}
