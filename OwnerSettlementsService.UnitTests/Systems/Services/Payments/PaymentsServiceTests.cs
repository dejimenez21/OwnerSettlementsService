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
using OwnerSettlementsService.UnitTests.Helpers;
using System.Collections.Generic;

namespace OwnerSettlementsService.UnitTests.Systems.Services.Payments;

public partial class PaymentsServiceTests
{
    private readonly Mock<IDateTimeBroker> _dateTimeBrokerMock;
    private readonly Mock<IPaymentsRepository> _paymentsRepositoryMock;
    private readonly IPaymentsService _paymentsService;
    private readonly Comparer<Payment, int> _comparer;

    public PaymentsServiceTests()
    {
        _dateTimeBrokerMock = new Mock<IDateTimeBroker>();
        _paymentsRepositoryMock = new Mock<IPaymentsRepository>();

        _paymentsService = new PaymentsService(_paymentsRepositoryMock.Object, _dateTimeBrokerMock.Object);

        _comparer = new Comparer<Payment, int>();
    }

    
    private List<Payment> GetAListOfPayments()
    {
        return new List<Payment>
        {
            new Payment
            {
                Id = 1,
                Amount = 123,
                Comment = "Any comment",
                Confirmed = true,
                CreatedAt = new DateTime(2021, 4, 12),
                DeliveredBy = "Hector",
                SentAt = new DateTime(2021, 1, 02)
            },
            new Payment
            {
                Id = 2,
                Amount = 4356,
                Comment = "Any comment 2",
                Confirmed = false,
                CreatedAt = new DateTime(2021, 12, 12),
                DeliveredBy = "Javier",
                SentAt = new DateTime(2020, 1, 02)
            },
            new Payment
            {
                Id = 3,
                Amount = 123,
                Comment = "Any comment",
                Confirmed = true,
                CreatedAt = new DateTime(2021, 4, 25),
                DeliveredBy = "Anyone",
                SentAt = new DateTime(2020, 1, 10)
            }
        };
    }
    
}