using OwnerSettlementsService.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    {
        private readonly OSSDbContext _dbContext;

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
    }
}
