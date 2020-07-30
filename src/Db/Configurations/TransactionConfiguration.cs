using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasMaxLength(36).ValueGeneratedOnAdd();
            builder.Property(_ => _.AccountId).HasMaxLength(36).IsRequired();
            builder.Property(_ => _.Type).IsRequired();
            builder.Property(_ => _.Amount).IsRequired();
            builder.Property(_ => _.EffectiveDate).HasDefaultValue(DateTime.Now);

            builder.HasOne(_ => _.Account).WithMany(_ => _.Transactions).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
