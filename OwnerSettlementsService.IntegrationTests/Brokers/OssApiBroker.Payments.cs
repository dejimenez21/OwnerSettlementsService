using OwnerSettlementsService.IntegrationTests.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OwnerSettlementsService.IntegrationTests.Brokers
{
    public partial class OssApiBroker
    {
        const string paymentsRelativeUrl = "api/payments";

        public async Task<HttpResponseMessage> PostPaymentAsync(Payment payment) =>
            await _baseClient.PostAsJsonAsync(paymentsRelativeUrl, payment);

        public async Task<HttpResponseMessage> GetPaymentById(int id) =>
            await _baseClient.GetAsync($"{paymentsRelativeUrl}/{id}");

        public async Task<HttpResponseMessage> GetAllPayments() =>
            await _baseClient.GetAsync(paymentsRelativeUrl);

        public async Task<HttpResponseMessage> DeletePayment(int id) =>
            await _baseClient.DeleteAsync($"{paymentsRelativeUrl}/{id}");
    }
}
