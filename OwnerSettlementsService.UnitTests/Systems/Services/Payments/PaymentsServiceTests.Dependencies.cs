using Moq;
using OwnerSettlementsService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        [Fact]
        public async Task RetrieveAllPayments_Calls_Repository()
        {
            await _paymentsService.RetrieveAllPayments();

            _paymentsRepositoryMock.Verify(broker => broker.SelectAll(), Times.Once);
        }
    }
}
