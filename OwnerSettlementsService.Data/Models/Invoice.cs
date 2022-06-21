using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnerSettlementsService.Data.Models
{
    public class Invoice : EntityBase<int>
    {
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
    }
}
