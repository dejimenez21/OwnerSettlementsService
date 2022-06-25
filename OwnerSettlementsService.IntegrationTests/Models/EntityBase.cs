using System;

namespace OwnerSettlementsService.IntegrationTests.Models
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
