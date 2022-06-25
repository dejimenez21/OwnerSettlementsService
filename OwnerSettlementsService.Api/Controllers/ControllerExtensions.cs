using Microsoft.AspNetCore.Mvc;
using OwnerSettlementsService.Core;
using OwnerSettlementsService.Core.Exceptions;

namespace OwnerSettlementsService.Api.Controllers
{
    public static class ControllerExtensions
    {
        public static ProblemDetails ToProblemDetails(this BusinessException exception)
        {
            return new ProblemDetails
            {
                Detail = exception.Message,
                Title = exception.Title
            };
        }

        public static ActionResult ToNoContent<T>(this OperationResult<T> result)
        {
            if (result.Success)
                return new NoContentResult();
            else
                return new ObjectResult(result.Error.ToProblemDetails()) { StatusCode = result.Error.StatusCode };
        }
    }
}
