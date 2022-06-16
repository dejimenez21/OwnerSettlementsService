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
        public decimal Amount { get; set; }
        public string DeliveredBy { get; set; }
        public bool Confirmed { get; set; } = false;
        public string Comment { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}