using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class AccountingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingDbContext).Assembly);
        }
    }
}
