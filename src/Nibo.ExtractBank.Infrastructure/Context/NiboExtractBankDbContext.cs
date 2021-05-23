using Microsoft.EntityFrameworkCore;
using Nibo.ExtractBank.Domain.Entitie;
using Nibo.ExtractBank.Infrastructure.MapEF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.ExtractBank.Infrastructure.Context
{
    public class NiboExtractBankDbContext : DbContext
    {
        public NiboExtractBankDbContext(DbContextOptions<NiboExtractBankDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionBank> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlite(@$"Filename={Environment.CurrentDirectory}/Database/NiboDatabase.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NiboExtractBankDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
