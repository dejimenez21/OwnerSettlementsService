using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OwnerSettlementsService.Core;
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
        public async Task CreatePayment_Returns_Successful_Result()
        {
            var today = new DateTime(2022, 5, 21);
            var inputPayment = new Payment
            {
                Amount = 6600,
                Comment = "Some random comment",
                SentAt = DateTime.Now,
                DeliveredBy = "Sonia"
            };
            var expectedPayment = inputPayment.DeepClone();
            expectedPayment.CreatedAt = today;
            var expectedResult = new OperationResult<Payment>(expectedPayment);

            _paymentsRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _dateTimeBrokerMock.Setup(x => x.GetCurrentDateTime()).ReturnsAsync(today);

            var actualResult = await _paymentsService.CreatePayment(inputPayment);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task RetrieveAllPayments_Returns_Successful_Result()
        {
            // given
            var storagePayments = GetAListOfPayments();
            var expectedPayments = storagePayments;

            _paymentsRepositoryMock.Setup(x => x.SelectAll()).ReturnsAsync(storagePayments);

            // when 
            var actualPayments = await _paymentsService.RetrieveAll();

            // then
            actualPayments.Should().BeEquivalentTo(expectedPayments);
        }
    }
}
