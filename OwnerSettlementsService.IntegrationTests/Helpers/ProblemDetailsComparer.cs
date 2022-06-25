using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.IntegrationTests.Helpers
{
    public class ProblemDetailsComparer : IEquivalencyStep
    {
        public EquivalencyResult Handle(Comparands comparands, IEquivalencyValidationContext context, IEquivalencyValidator nestedValidator)
        {
            var actualProblemDetail = (ProblemDetails)comparands.Subject;
            var expectedProblemDetail = (ProblemDetails)comparands.Expectation;

            actualProblemDetail.Status.Should().Be(expectedProblemDetail.Status);
            actualProblemDetail.Title.Should().Be(expectedProblemDetail.Title);
            actualProblemDetail.Detail.Should().Be(expectedProblemDetail.Detail);

            return EquivalencyResult.AssertionCompleted;
        }
    }
}
