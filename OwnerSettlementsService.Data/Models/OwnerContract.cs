using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Models
{
    public class OwnerContract
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public decimal TotalShare { get; set; }
        public DateTime RegisterAt { get; set; }
        public bool IsActive { get; set; }
    }
}
