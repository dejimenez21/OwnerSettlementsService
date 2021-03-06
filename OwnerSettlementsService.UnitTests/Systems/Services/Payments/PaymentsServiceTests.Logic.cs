using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OwnerSettlementsService.Core;
using OwnerSettlementsService.Core.Exceptions;
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
        public async Task RetrieveAllPayments_Returns_List_Of_Payments()
        {
            // given
            var storagePayments = GetAListOfPayments();
            var expectedPayments = storagePayments;

            _paymentsRepositoryMock.Setup(x => x.SelectAll()).ReturnsAsync(storagePayments);

            // when 
            var actualPayments = await _paymentsService.RetrieveAllPayments();

            // then
            actualPayments.Should().BeEquivalentTo(expectedPayments);
        }

        [Fact]
        public async Task RetrievePaymentById_When_Provided_Id_Exist_Returns_Payment()
        {
            // given
            var inputId = 2;
            var storagePayment = new Payment
            {
                Id = inputId,
                Amount = 2345,
                Confirmed = true,
                DeliveredBy = "Someone",
                SentAt = new DateTime(2025, 12, 6)
            };
            var expectedPayment = storagePayment;

            _paymentsRepositoryMock.Setup(x => x.SelectById(inputId)).ReturnsAsync(storagePayment);

            // when
            var actualPayment = await _paymentsService.RetrievePaymentById(inputId);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);
        }

        [Fact]
        public async Task RetrievePaymentById_When_Payment_Doesnt_Exists_Returns_Null()
        {
            // given
            var inputId = 20;

            _paymentsRepositoryMock.Setup(x => x.SelectById(inputId)).ReturnsAsync(value: null);

            // when
            var actualPayment = await _paymentsService.RetrievePaymentById(inputId);

            // then
            actualPayment.Should().BeNull();
        }

        [Fact]
        public async Task DeletePayment_Returns_Successful_Result()
        {
            // given
            var inputId = 6;
            var expectedResult = new OperationResult<int>(1);

            _paymentsRepositoryMock.Setup(x => x.SelectById(inputId)).ReturnsAsync(new Payment { Id = inputId });
            _paymentsRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // when
            var actualResult = await _paymentsService.DeletePaymentById(inputId);

            //then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task DeletePayment_When_Id_Doesnt_Exist_Returns_Unsuccessful_Result()
        {
            // given
            var inputId = 15;
            var expectedResult = new OperationResult<int>(new NotFoundException(nameof(Payment), inputId));

            _paymentsRepositoryMock.Setup(x => x.SelectById(inputId)).ReturnsAsync(value: null);

            // when
            var actualResult = await _paymentsService.DeletePaymentById(inputId);

            //then
            actualResult.Should().BeEquivalentTo(expectedResult);

            _paymentsRepositoryMock.Verify(broker => broker.SelectById(inputId), Times.Once);
            _paymentsRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
