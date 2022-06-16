using OwnerSettlementsService.Data.Models;
using OwnerSettlementsService.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.Repositories
{
    public class PaymentsRepository : RepositoryBase<Payment, int>, IPaymentsRepository
    {
        public PaymentsRepository(OSSDbContext oSSDbContext) : base(oSSDbContext)
        {

        }
    }
}
