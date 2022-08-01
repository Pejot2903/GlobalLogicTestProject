using RestSharp;
using Xunit;

namespace GlobalLogicTestProject.Fixtures
{
    public class ApiFixture
    {
        public ApiFixture()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.clearDb);
            client.Execute(request);
        }
    }

    [CollectionDefinition("ApiFixture")]
    public class ApiTestCollection : ICollectionFixture<ApiFixture>
    {

    }
}
