using OwnerSettlementsService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Core.Services.Abstractions
{
    public interface IPaymentsService
    {
        Task<OperationResult<Payment>> CreatePayment(Payment inputPayment);
        Task<IEnumerable<Payment>> RetrieveAllPayments();
        Task<Payment> RetrievePaymentById(int inputId);
    }
}
