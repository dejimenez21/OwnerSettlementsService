using Xunit;
using Moq;
using System.Threading.Tasks;
using OwnerSettlementsService.Core.Services;
using OwnerSettlementsService.Core.Services.Abstractions;
using OwnerSettlementsService.Data.Repositories.Abstractions;
using OwnerSettlementsService.Data.DateTimes;
using OwnerSettlementsService.Data.Models;
using System;
using Force.DeepCloner;
using OwnerSettlementsService.Core;
using FluentAssertions;

namespace OwnerSettlementsService.UnitTests.Systems.Services;

public partial class PaymentsServiceTests
{
    private readonly Mock<IDateTimeBroker> _dateTimeBrokerMock;
    private readonly Mock<IPaymentsRepository> _paymentsRepositoryMock;
    private readonly IPaymentsService _paymentsService;


    public PaymentsServiceTests()
    {
        _dateTimeBrokerMock = new Mock<IDateTimeBroker>();
        _paymentsRepositoryMock = new Mock<IPaymentsRepository>();

        _paymentsService = new PaymentsService(_paymentsRepositoryMock.Object, _dateTimeBrokerMock.Object);
    }

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

}