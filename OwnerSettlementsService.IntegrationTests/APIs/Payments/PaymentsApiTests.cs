using FluentAssertions;
using Force.DeepCloner;
using OwnerSettlementsService.IntegrationTests.Brokers;
using OwnerSettlementsService.IntegrationTests.Extensions;
using OwnerSettlementsService.IntegrationTests.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace OwnerSettlementsService.IntegrationTests.APIs.Payments
{
    [Collection(nameof(ApiTestCollection))]
    public class PaymentsApiTests
    {
        private readonly OssApiBroker _apiBroker;

        public PaymentsApiTests(OssApiBroker apiBroker) =>
            _apiBroker = apiBroker;

        [Fact]
        public async Task PostPayment_Inserts_Payment_In_Database_And_Returns_Created()
        {
            //given
            var inputPayment = new Payment
            {
                Amount = 6600,
                Comment = "Some random comment",
                SentAt = DateTime.Now,
                DeliveredBy = "Sonia"
            };
            var expectedPayment = inputPayment.DeepClone();
            expectedPayment.Confirmed = false;

            //when
            var response = await _apiBroker.PostPaymentAsync(inputPayment);

            //then
            var returnedPayment = await response.ToEntity<Payment>();
            var storedPayment = await (await _apiBroker.GetPaymentById(returnedPayment.Id)).ToEntity<Payment>();
            await _apiBroker.DeletePayment(storedPayment.Id);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            storedPayment.Should().BeEquivalentTo(expectedPayment, options => options
                .Excluding(p => p.Id)
                .Excluding(p => p.CreatedAt));

        }



    }
}
