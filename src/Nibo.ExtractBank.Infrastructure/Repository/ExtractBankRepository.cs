using Microsoft.EntityFrameworkCore;
using Nibo.ExtractBank.Domain.Entitie;
using Nibo.ExtractBank.Domain.Interfaces.IRepository;
using Nibo.ExtractBank.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Infrastructure.Repository
{
    public class ExtractBankRepository : Repository<TransactionBank>, IExtractBankRepository
    {
        public ExtractBankRepository(NiboExtractBankDbContext context) : base(context) { }

        public async Task Add(List<TransactionBank> entity)
        {
            Db.AddRange(entity);
        }

        public async Task<bool> CheckIfExistsTransaction(TransactionBank transaction)
        {
            var count = await DbSet.Where(x =>
                x.IdBank == transaction.IdBank &&
                x.Type == transaction.Type &&
                x.Date == transaction.Date &&
                x.Amount == transaction.Amount &&
                x.Description == transaction.Description
            ).CountAsync();

            return count > 0;
        }
    }
}
