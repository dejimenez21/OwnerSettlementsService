using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
