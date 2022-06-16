using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class OwnerContract
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalShare { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
