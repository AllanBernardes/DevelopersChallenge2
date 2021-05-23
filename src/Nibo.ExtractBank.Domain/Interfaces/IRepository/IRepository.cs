using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Domain.Interfaces.IRepository
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task<bool> AddRange(List<TEntity> entity);

        Task<List<TEntity>> GetAll();
    }
}
