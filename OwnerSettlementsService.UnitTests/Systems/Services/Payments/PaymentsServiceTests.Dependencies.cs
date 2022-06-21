using Moq;
using OwnerSettlementsService.Data.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OwnerSettlementsService.UnitTests.Systems.Services.Payments
{
    public partial class PaymentsServiceTests
    {
        [Fact]
        public async Task CreatePayment_Calls_Repository_And_DateTimeBroker()
        {
            var today = new DateTime(2022, 5, 21);
            var inputPayment = new Payment
            {
                Amount = 6600,
                Comment = "Some random comment",
                SentAt = DateTime.Now,
                DeliveredBy = "Sonia"
            };

            _paymentsRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _dateTimeBrokerMock.Setup(x => x.GetCurrentDateTime()).ReturnsAsync(today);

            await _paymentsService.CreatePayment(inputPayment);

            _paymentsRepositoryMock.Verify(broker => broker.Insert(It.Is(inputPayment, _comparer)), Times.Once);
            _paymentsRepositoryMock.Verify(broker => broker.SaveChangesAsync(), Times.Once);
            _dateTimeBrokerMock.Verify(broker => broker.GetCurrentDateTime(), Times.Once);

            _paymentsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RetrieveAllPayments_Calls_Repository()
        {
            await _paymentsService.RetrieveAllPayments();

            _paymentsRepositoryMock.Verify(broker => broker.SelectAll(), Times.Once);
            _paymentsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RetrievePaymentById_Calls_Repository()
        {
            var inputId = 3;
            await _paymentsService.RetrievePaymentById(inputId);

            _paymentsRepositoryMock.Verify(broker => broker.SelectById(inputId), Times.Once);
            _paymentsRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeletePayment_Calls_Repository()
        {
            // given
            var inputId = 6;
            var storagePayment = new Payment
            {
                Id = inputId,
                Amount = 2345,
                Confirmed = true,
                DeliveredBy = "Someone",
                SentAt = new DateTime(2025, 12, 6)
            };

            _paymentsRepositoryMock.Setup(x => x.SelectById(inputId)).ReturnsAsync(storagePayment);

            // when
            await _paymentsService.DeletePaymentById(inputId);

            //then
            _paymentsRepositoryMock.Verify(broker => broker.SelectById(inputId), Times.Once);
            _paymentsRepositoryMock.Verify(broker => broker.Delete(It.Is(storagePayment, _comparer)), Times.Once);
            _paymentsRepositoryMock.Verify(broker => broker.SaveChangesAsync(), Times.Once);
            _paymentsRepositoryMock.VerifyNoOtherCalls();

        }
    }
}
