using Nibo.ExtractBank.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Domain.Interfaces.IRepository
{
    public interface IExtractBankRepository : IRepository<TransactionBank>
    {
        Task Add(List<TransactionBank> entity);

        Task<List<TransactionBank>> GetAll();

        Task<bool> CheckIfExistsTransaction(TransactionBank transaction);
    }
}
