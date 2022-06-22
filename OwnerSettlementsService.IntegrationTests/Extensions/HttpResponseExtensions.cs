using System.Net.Http;
using System.Threading.Tasks;

namespace OwnerSettlementsService.IntegrationTests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ToEntity<T>(this HttpResponseMessage response) =>
            await response.Content.ReadAsAsync<T>();
    }
}
