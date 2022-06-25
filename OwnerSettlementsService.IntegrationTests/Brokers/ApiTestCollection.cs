using Xunit;

namespace OwnerSettlementsService.IntegrationTests.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<OssApiBroker>
    {

    }
}
