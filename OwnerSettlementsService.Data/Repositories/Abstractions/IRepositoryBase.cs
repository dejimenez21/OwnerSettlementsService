using OwnerSettlementsService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Repositories.Abstractions
{
    public interface IRepositoryBase<TEntity, TKey>
    {
        void Insert(TEntity entity);

        Task<IEnumerable<TEntity>> SelectAll();

        Task<int> SaveChangesAsync();
    }
}
