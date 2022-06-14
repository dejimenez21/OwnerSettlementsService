using Microsoft.EntityFrameworkCore;
using OwnerSettlementsService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data
{
    public class OSSDbContext : DbContext
    {
        public OSSDbContext(DbContextOptions options) : base(options)  { }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<OwnerContract> OwnerContracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

    }
}
