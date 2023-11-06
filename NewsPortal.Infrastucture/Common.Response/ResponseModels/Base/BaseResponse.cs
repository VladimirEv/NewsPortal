using System.Net;

namespace NewsPortal.Infrastucture.Common.Response.ResponseModels.Base
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
