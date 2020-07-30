using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasMaxLength(36).ValueGeneratedOnAdd();
            builder.Property(_ => _.Balance).HasDefaultValue(decimal.Zero);

            builder.HasMany(_ => _.Transactions).WithOne(_ => _.Account).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
