using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OwnerSettlementsService.Api;
using System.IO;
using System.Net.Http;

namespace OwnerSettlementsService.IntegrationTests.Brokers;

public partial class OssApiBroker
{
    private readonly WebApplicationFactory<Startup> _webApplicationFactory;
    private readonly HttpClient _baseClient;

    public OssApiBroker()
    {
        var dirPath = Directory.GetCurrentDirectory();
        var configPath = Path.Combine(dirPath, "appsettings-tests.json");

        _webApplicationFactory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder => builder.ConfigureAppConfiguration((context, cfg) => cfg.AddJsonFile(configPath)));
        _baseClient = _webApplicationFactory.CreateClient();

    }
}