using GlobalLogicTestProject.Models.DTOs;
using GlobalLogicTestProject.Models.Requests;

namespace GlobalLogicTestProject.Builders
{
    public class PostProductRequestBuilder
    {
        private readonly ProductRequest _request;

        public PostProductRequestBuilder()
        {
            _request = new ProductRequest
            {
                RequestBody = new Product()
            };
        }

        public PostProductRequestBuilder WithName(string name)
        {
            _request.RequestBody.Name = name;
            return this;
        }

        public PostProductRequestBuilder WithDescription(string description)
        {
            _request.RequestBody.Description = description;
            return this;
        }

        public PostProductRequestBuilder WithPrice(decimal price)
        {
            _request.RequestBody.Price = price;
            return this;
        }

        public PostProductRequestBuilder WithItemCount(int itemCount)
        {
            _request.RequestBody.ItemCount = itemCount;
            return this;
        }

        public PostProductRequestBuilder WithActive(bool isActive)
        {
            _request.RequestBody.Active = isActive;
            return this;
        }

        public ProductRequest Build()
        {
            return _request;
        }
    }
}
