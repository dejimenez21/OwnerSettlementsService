using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Delivery { get; set; }
        public bool Confirmed { get; set; }
        public string Comment { get; set; }
        public bool ToOwner { get; set; }

    }
}
