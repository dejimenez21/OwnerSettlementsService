using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnerSettlementsService.IntegrationTests.Models
{
    public class Payment : EntityBase<int>
    {
        public decimal Amount { get; set; }
        public string DeliveredBy { get; set; }
        public bool Confirmed { get; set; } = false;
        public string Comment { get; set; }
        public DateTime SentAt { get; set; }
    }
}