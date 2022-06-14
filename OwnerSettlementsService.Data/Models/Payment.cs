using System;
using System.Collections.Generic;
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
        public string Delivery { get; set; }
        public bool Confirmed { get; set; }
        public string Comment { get; set; }
        public bool ToOwner { get; set; }

    }
}
