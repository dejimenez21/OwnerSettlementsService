using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace OwnerSettlementsService.IntegrationTests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ToEntityAsync<T>(this HttpResponseMessage response) =>
            await response.Content.ReadAsAsync<T>();

        public static async Task<ProblemDetails> ToProblemDetailsAsync(this HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProblemDetails>(json);
        }
    }
}
