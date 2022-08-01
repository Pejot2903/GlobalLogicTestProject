using FluentAssertions;
using GlobalLogicTestProject.Builders;
using GlobalLogicTestProject.Models.DTOs;
using GlobalLogicTestProject.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Xunit;

namespace GlobalLogicTestProject
{
    [Collection("ApiFixture")]
    public class ProductTests
    {
        [Fact]
        public void ItShould_ReturnOK_When_RequestingGetAllEndpoint()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.getAll);
            var response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public void ItShould_ReturnBadRequest_When_PostingNewItemWithoutBody()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);
            var response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ItShould_ReturnBadRequest_When_PostingNewItemWithTooLongName()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);

            var productRequest = new PostProductRequestBuilder()
                .WithName("Mousenfamfnadlsjkfhadsljfnasdfbhadsjkfgasdkyhfgdkashfgadksfhgasdkfhasdgfkhasdgfkas")
                .WithDescription("Wireless")
                .WithPrice(17)
                .WithItemCount(10)
                .WithActive(true)
                .Build();

            request.AddJsonBody(JsonConvert.SerializeObject(productRequest.RequestBody));

            var response = client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ItShould_ReturnBadRequest_When_PostingNewItemWithEmptyName()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);

            var productRequest = new PostProductRequestBuilder()
                .WithName("")
                .WithDescription("Wireless")
                .WithPrice(17)
                .WithItemCount(10)
                .WithActive(true)
                .Build();

            request.AddJsonBody(JsonConvert.SerializeObject(productRequest.RequestBody));

            var response = client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ItShould_ReturnOK_When_PostingNewItem()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);

            var productRequest = new PostProductRequestBuilder()
                .WithName("Mouse")
                .WithDescription("Wireless")
                .WithPrice(17)
                .WithItemCount(10)
                .WithActive(true)
                .Build();

            request.AddJsonBody(JsonConvert.SerializeObject(productRequest.RequestBody));

            var response = client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void ItShould_ReturnOK_When_PostingNewItemWithoutOptionalParameters()
        {
            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);

            var productRequest = new PostProductRequestBuilder()
                .WithName("Mouse")
                .Build();

            request.AddJsonBody(JsonConvert.SerializeObject(productRequest.RequestBody));

            var response = client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void ItShould_ReturnCorrectResponseData_When_PostingNewItem()
        {
            var name = "Keyboard";
            var price = 17;
            var itemCount = 10;
            var active = true;

            var client = new RestClient(Urls.baseUrl);
            var request = new RestRequest(Urls.product, Method.Post);

            var productRequest = new PostProductRequestBuilder()
                .WithName(name)
                .WithPrice(price)
                .WithItemCount(itemCount)
                .WithActive(active)
                .Build();

            request.AddJsonBody(JsonConvert.SerializeObject(productRequest.RequestBody));

            var response = client.Execute<Product>(request);
            var productResponse = new ProductResponse
            {
                StatusCode = response.StatusCode,
                Product = response.Data
            };

            productResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productResponse.Product.Name.Should().Be(name);
            productResponse.Product.Price.Should().Be(price);
            productResponse.Product.ItemCount.Should().Be(itemCount);
            productResponse.Product.Active.Should().Be(active);
            productResponse.Product.Id.Should().NotBeEmpty();
            productResponse.Product.Created.Should().Be(productResponse.Product.Modified);
        }
    }
}
