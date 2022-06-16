using Microsoft.AspNetCore.Mvc;
using OwnerSettlementsService.Data.Models;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Api.Controllers
{
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostPayment(Payment inputPayment)
        {
            return null;
        }
    }
}
