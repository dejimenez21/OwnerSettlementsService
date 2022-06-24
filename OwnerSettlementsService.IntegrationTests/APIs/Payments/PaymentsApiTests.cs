using FluentAssertions;
using Force.DeepCloner;
using Microsoft.AspNetCore.Mvc;
using OwnerSettlementsService.IntegrationTests.Brokers;
using OwnerSettlementsService.IntegrationTests.Extensions;
using OwnerSettlementsService.IntegrationTests.Helpers;
using OwnerSettlementsService.IntegrationTests.Models;
using System;
using System.Linq;
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
            var expectedStatusCode = HttpStatusCode.Created;
            var expectedPayment = inputPayment.DeepClone();
            expectedPayment.Confirmed = false;

            //when
            var response = await _apiBroker.PostPaymentAsync(inputPayment);

            //then
            var returnedPayment = await response.ToEntityAsync<Payment>();
            var storedPayment = await (await _apiBroker.GetPaymentById(returnedPayment.Id)).ToEntityAsync<Payment>();
            await _apiBroker.DeletePayment(storedPayment.Id);

            response.StatusCode.Should().Be(expectedStatusCode);
            storedPayment.Should().BeEquivalentTo(expectedPayment, options => options
                .Excluding(p => p.Id)
                .Excluding(p => p.CreatedAt));
            storedPayment.Id.Should().NotBe(0);
            storedPayment.CreatedAt.Should().BeSameDateAs(DateTime.Now);
        }

        [Fact]
        public async Task PostPayment_With_Null_DeliveredBy_Returns_BadRequest()
        {
            //given
            var inputPayment = new Payment
            {
                Amount = 6600,
                Comment = "Some random comment",
                SentAt = DateTime.Now,
                DeliveredBy = null
            };
            var expectedStatusCode = HttpStatusCode.BadRequest;

            //when
            var response = await _apiBroker.PostPaymentAsync(inputPayment);

            //then
            response.StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public async Task GetPaymentById_Returns_Ok_And_Requested_Payment()
        {
            //given
            var inputPayment = new Payment
            {
                Amount = 8000,
                Comment = "Some random comment!!!",
                SentAt = DateTime.Now,
                DeliveredBy = "Jason"
            };
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedPayment = await (await _apiBroker.PostPaymentAsync(inputPayment)).ToEntityAsync<Payment>();
            var inputId = expectedPayment.Id;

            //when
            var response = await _apiBroker.GetPaymentById(inputId);

            //then
            await _apiBroker.DeletePayment(inputId);

            var actualPayment = await response.ToEntityAsync<Payment>();

            response.StatusCode.Should().Be(expectedStatusCode);
            actualPayment.Should().BeEquivalentTo(expectedPayment);
        }

        [Fact]
        public async Task GetPaymentById_When_Payment_Doesnt_Exist_Returns_NotFound()
        {
            //given
            var inputId = 100;
            var expectedStatusCode = HttpStatusCode.NotFound;

            await _apiBroker.DeletePayment(inputId);

            //when
            var response = await _apiBroker.GetPaymentById(inputId);

            //then
            response.StatusCode.Should().Be(expectedStatusCode);
            await response.Invoking(async r => await r.ToEntityAsync<Payment>()).Should().ThrowAsync<Exception>();
        }
    }
}
