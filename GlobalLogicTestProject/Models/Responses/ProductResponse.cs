using GlobalLogicTestProject.Models.DTOs;
using System.Net;

namespace GlobalLogicTestProject.Models.Responses
{
    public class ProductResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Product Product { get; set; }
    }
}
