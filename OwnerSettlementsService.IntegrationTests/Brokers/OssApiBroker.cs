using Microsoft.AspNetCore.Mvc.Testing;
using OwnerSettlementsService.Api;
using System.Net.Http;

namespace OwnerSettlementsService.IntegrationTests.Brokers;

public partial class OssApiBroker
{
    private readonly WebApplicationFactory<Startup> _webApplicationFactory;
    private readonly HttpClient _baseClient;

    public OssApiBroker()
    {
        _webApplicationFactory = new WebApplicationFactory<Startup>();
        _baseClient = _webApplicationFactory.CreateClient();
    }
}