using FluentAssertions;
using Force.DeepCloner;
using Microsoft.AspNetCore.Mvc;
using OwnerSettlementsService.IntegrationTests.Brokers;
using OwnerSettlementsService.IntegrationTests.Extensions;
using OwnerSettlementsService.IntegrationTests.Helpers;
using OwnerSettlementsService.IntegrationTests.Models;
using System;
using System.Collections.Generic;
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

        [Fact]
        public async Task GetAllPayments_Whithout_Any_Stored_Payment_Returns_Empty_List()
        {
            //given
            var expectedStatusCode = HttpStatusCode.OK;
            //when
            var response = await _apiBroker.GetAllPayments();

            //then
            var actualPayload = await response.ToEntityAsync<IEnumerable<Payment>>();

            response.StatusCode.Should().Be(expectedStatusCode);
            actualPayload.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllPayments_Returns_A_List_Of_Payments()
        {
            //given
            var expectedStatusCode = HttpStatusCode.OK;
            var inputPayments = new List<Payment>
            {
                new Payment
                {
                    Amount = 8000,
                    Comment = "Some random comment 1!!!",
                    SentAt = new DateTime(2021, 4, 21),
                    DeliveredBy = "Jason"
                },
                new Payment
                {
                    Amount = 435,
                    Comment = "Some random comment 2!!!",
                    SentAt = new DateTime(2021, 7, 21),
                    DeliveredBy = "Daniel"
                },
                new Payment
                {
                    Amount = 5460,
                    Comment = "Some random comment 3!!!",
                    SentAt = new DateTime(2021, 4, 15),
                    DeliveredBy = "Clara"
                }
            };
            var expectedPayments = new List<Payment>();
            foreach (var inputPayment in inputPayments)
            {
                var creationResponse = await _apiBroker.PostPaymentAsync(inputPayment);
                expectedPayments.Add(await creationResponse.ToEntityAsync<Payment>());
            }

            //when
            var actualResponse = await _apiBroker.GetAllPayments();

            //then
            var actualPayments = await actualResponse.ToEntityAsync<IEnumerable<Payment>>();

            actualResponse.StatusCode.Should().Be(expectedStatusCode);
            actualPayments.Should().BeEquivalentTo(expectedPayments);

            //finally
            foreach (var paymentId in actualPayments.Select(p => p.Id))
            {
                await _apiBroker.DeletePayment(paymentId);
            }
        }

        [Fact]
        public async Task DeletePayment_Removes_Payment_From_Database_And_Returns_NoContent()
        {
            //given
            var inputPayment = new Payment
            {
                Amount = 7500,
                Comment = "Some random comment!!!",
                SentAt = new DateTime(2020, 5, 12),
                DeliveredBy = "Jason"
            };
            var storedPayment = await (await _apiBroker.PostPaymentAsync(inputPayment)).ToEntityAsync<Payment>();
            var inputId = storedPayment.Id;
            var getBeforeResponse = await _apiBroker.GetPaymentById(inputId);
            var expectedStatusCode = HttpStatusCode.NoContent;

            //when
            var actualResponse = await _apiBroker.DeletePayment(inputId);

            //then
            var getAfterResponse = await _apiBroker.GetPaymentById(inputId);

            actualResponse.StatusCode.Should().Be(expectedStatusCode);
            getBeforeResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getAfterResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeletePayment_When_Payment_Doesnt_Exist_Returns_NotFound()
        {
            //given
            var inputId = 120;
            await _apiBroker.DeletePayment(inputId);
            var expectedStatusCode = HttpStatusCode.NotFound;
            var expectedPayload = new ProblemDetails { Status = 404, Title = "Payment Not Found", Detail = $"The {nameof(Payment)} with the id '{inputId}' doesn't exist." };

            //when
            var actualResponse = await _apiBroker.DeletePayment(inputId);

            //then
            var actualPayload = await actualResponse.ToProblemDetailsAsync();
            actualResponse.StatusCode.Should().Be(expectedStatusCode);
            actualPayload.Should().BeEquivalentTo(expectedPayload, options => options.Using(new ProblemDetailsComparer()));
        }
    }
}
