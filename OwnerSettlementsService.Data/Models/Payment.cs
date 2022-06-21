using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnerSettlementsService.Data.Models
{
    public class Payment : EntityBase<int>
    {
        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string DeliveredBy { get; set; }
        public bool Confirmed { get; set; } = false;
        public string Comment { get; set; }
        [Required]
        public DateTime SentAt { get; set; }
    }
}