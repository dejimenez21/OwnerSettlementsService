using Microsoft.AspNetCore.Mvc;
using OwnerSettlementsService.Core.Services.Abstractions;
using OwnerSettlementsService.Data.Models;
using System;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment inputPayment)
        {
            var result = await _paymentsService.CreatePayment(inputPayment);
            return Created($"/api/payments/{result.Data.Id}", result);
        }
    }
}
