using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class Settlement
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
    }
}
