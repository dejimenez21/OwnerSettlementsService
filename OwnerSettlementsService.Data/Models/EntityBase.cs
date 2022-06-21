using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
