using Microsoft.EntityFrameworkCore;
using OwnerSettlementsService.Data.Models;
using OwnerSettlementsService.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        protected readonly OSSDbContext _dbContext;
        protected DbSet<TEntity> dbSet => _dbContext.Set<TEntity>();

        public RepositoryBase(OSSDbContext oSSDbContext)
        {
            _dbContext = oSSDbContext;
        }
        public void Insert(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> SelectAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> SelectById(TKey id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
