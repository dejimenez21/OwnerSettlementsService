using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string DeliveredBy { get; set; }
        public bool Confirmed { get; set; } = false;
        public string Comment { get; set; }
        [Required]
        public DateTime SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}