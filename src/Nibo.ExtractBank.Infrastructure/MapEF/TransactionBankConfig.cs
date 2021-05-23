using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nibo.ExtractBank.Domain.Entitie;

namespace Nibo.ExtractBank.Infrastructure.MapEF
{
    public class TransactionBankConfig : IEntityTypeConfiguration<TransactionBank>
    {
        public void Configure(EntityTypeBuilder<TransactionBank> builder)
        {
            var tableName = "Transaction";

            builder.ToTable(tableName);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.IdBank)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(c => c.CheckTransaction)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.InitDateTransaction)
                .IsRequired();

            builder.Property(c => c.EndtDateTransaction)
                .IsRequired();

            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Date)
               .IsRequired();

            builder.Property(c => c.Amount)
               .IsRequired();

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(150);

            //builder
            //.HasMany(answer => answer.Transactions);


        }
    }
}
