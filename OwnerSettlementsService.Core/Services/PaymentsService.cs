using OwnerSettlementsService.Core.Exceptions;
using OwnerSettlementsService.Core.Services.Abstractions;
using OwnerSettlementsService.Data.DateTimes;
using OwnerSettlementsService.Data.Models;
using OwnerSettlementsService.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Core.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IDateTimeBroker _dateTimeBroker;

        public PaymentsService(IPaymentsRepository paymentsRepository, IDateTimeBroker dateTimeBroker)
        {
            _paymentsRepository = paymentsRepository;
            _dateTimeBroker = dateTimeBroker;
        }

        public async Task<OperationResult<Payment>> CreatePayment(Payment inputPayment)
        {
            inputPayment.CreatedAt = await _dateTimeBroker.GetCurrentDateTime();
            _paymentsRepository.Insert(inputPayment);
            await _paymentsRepository.SaveChangesAsync();
            return inputPayment;
        }

        public async Task<OperationResult<int>> DeletePaymentById(int inputId)
        {
            var payment = await _paymentsRepository.SelectById(inputId);
            if (payment == null)
                return new NotFoundException(nameof(Payment), inputId);
            _paymentsRepository.Delete(payment);
            return await _paymentsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> RetrieveAllPayments()
        {
            return await _paymentsRepository.SelectAll();
        }

        public async Task<Payment> RetrievePaymentById(int inputId)
        {
            return await _paymentsRepository.SelectById(inputId);
        }
    }
}
