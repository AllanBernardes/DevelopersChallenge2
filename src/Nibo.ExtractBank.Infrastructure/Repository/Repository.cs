using Microsoft.EntityFrameworkCore;
using Nibo.ExtractBank.Domain.Entitie;
using Nibo.ExtractBank.Domain.Interfaces.IRepository;
using Nibo.ExtractBank.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Infrastructure.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : TransactionBank, new()
    {
        protected readonly NiboExtractBankDbContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(NiboExtractBankDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }


        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async Task<bool> AddRange(List<TEntity> entity)
        {
            try
            {
                await DbSet.AddRangeAsync(entity);
                await SaveChanges();
                return true;
                
            }
            catch (Exception e)
            {

                var message = e.Message;
                return false;
            }
            
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        
    }
}
